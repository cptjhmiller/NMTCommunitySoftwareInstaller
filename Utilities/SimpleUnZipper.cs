using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace Com.Tytte.Dk
{
  class SimpleUnZipper
  {
    /// <summary>
    /// Class representing an uncompressed file from a zip archive.
    /// </summary>
    public class UncompressedFile
    {
      private string name;
      private Stream stream;

      public string Name { get { return name; } }
      public Stream Stream { get { return stream; } }

      public UncompressedFile(string name, Stream stream)
      {
        this.name = name;
        this.stream = stream;
      }
    }

    private class LimitedStream : Stream
    {
      private Stream baseStream;
      private long limit, position;

      public LimitedStream(Stream baseStream, long limit)
      {
        this.baseStream = baseStream;
        this.Limit = limit;
      }

      public long Limit { set { limit = value; position = 0; } }

      public override bool CanRead { get { return true; } }
      public override bool CanSeek { get { return false; } }
      public override bool CanWrite { get { return false; } }
      public override long Length { get { return limit; } }

      public override long Position
      {
        get { return position; }
        set { throw new NotImplementedException(); }
      }

      public override void Flush() { baseStream.Flush(); }
      public override long Seek(long offset, SeekOrigin origin) { throw new NotImplementedException(); }
      public override void SetLength(long value) { throw new NotImplementedException(); }
      public override void Write(byte[] buffer, int offset, int count) { throw new NotImplementedException(); }
        
      public void SeekToEnd()
      {
        int cnt = (int) (limit - position);
        byte[] buffer = new byte[1024];

        while (cnt > 0)
        {
          int n = baseStream.Read(buffer, 0, Math.Min(buffer.Length, cnt));

          if (n <= 0)
          {
            throw new ApplicationException("Zip-file is broken");
          }

          cnt -= n;
        }

        position = limit;
      }

      public override int Read(byte[] buffer, int offset, int count)
      {
        int ncount = (int)Math.Min(count, limit - position);
        int n = baseStream.Read(buffer, offset, ncount);
        position += n;
        return n;
      }
    }

    private static void CopyStream(Stream dest, Stream source)
    {
      byte[] buffer = new byte[8 * 1024];
      int n;

      while ((n = source.Read(buffer, 0, buffer.Length)) > 0)
      {
        dest.Write(buffer, 0, n);
      }
    }

    /// <summary>
    /// UnZip files from zip archive in a stream.
    /// </summary>
    /// <param name="zipStream">Zip archive stream.</param>
    /// <returns>Each file in the archive.</returns>
    public static IEnumerable<UncompressedFile> UnZip(Stream zipStream)
    {
      BinaryReader binaryReader = new BinaryReader(zipStream);
      LimitedStream limitedStream = new LimitedStream(zipStream, 0);

      while (true)
      {
        uint signature = binaryReader.ReadUInt32();

        if (signature == 0x02014b50)
        {
          break;
        }

        if (signature != 0x04034b50)
        {
          throw new ApplicationException("Zip file is broken or corrupt");
        }

        binaryReader.ReadBytes(2);
        ushort flags = binaryReader.ReadUInt16();

        if ((flags & (1 << 3)) != 0)
        {
          throw new ApplicationException("Unsupported Zip file format (compressed size not available in header");
        }

        ushort method = binaryReader.ReadUInt16();
        binaryReader.ReadBytes(8);
        uint compressedSize = binaryReader.ReadUInt32();
        binaryReader.ReadBytes(4);
        ushort fileNameLength = binaryReader.ReadUInt16();
        ushort extraFieldLength = binaryReader.ReadUInt16();

        string filename = Encoding.ASCII.GetString(binaryReader.ReadBytes(fileNameLength)).Replace('/', Path.DirectorySeparatorChar);
        binaryReader.ReadBytes(extraFieldLength);

        limitedStream.Limit = compressedSize;

        switch (method)
        {
          case 0:
            // No compression
            yield return new UncompressedFile(filename, limitedStream);
            break;

          case 8:
            // Deflate
            DeflateStream deflateStream = new DeflateStream(limitedStream, CompressionMode.Decompress, true);
            yield return new UncompressedFile(filename, deflateStream);
            break;

          default:
            // Ignore...
            break;
        }

        limitedStream.SeekToEnd();
      }

      limitedStream.Close();
      binaryReader.Close();
    }

    /// <summary>
    /// UnZip files from zip archive on disk.
    /// </summary>
    /// <param name="zipFilePath">Path to zip-file.</param>
    /// <returns>Each file in the archive.</returns>
    public static IEnumerable<UncompressedFile> UnZip(string zipFilePath)
    {
      using (Stream stream = File.OpenRead(zipFilePath))
      {
        foreach (UncompressedFile file in UnZip(stream))
        {
          yield return file;
        }
        stream.Close();
      }
    }

    /// <summary>
    /// UnZip all files in a zip archive to a file system directory.
    /// </summary>
    /// <param name="zipStream">Zip archive stream.</param>
    /// <param name="destinationPath">Destination file system base path.</param>
    public static void UnZipTo(Stream zipStream, string destinationPath)
    {
      if (!Directory.Exists(destinationPath))
      {
        Directory.CreateDirectory(destinationPath);
      }

      foreach (UncompressedFile file in UnZip(zipStream))
      {
        FileInfo fullPath = new FileInfo(Path.Combine(destinationPath, file.Name));

        if (!Directory.Exists(fullPath.DirectoryName))
        {
          Directory.CreateDirectory(fullPath.DirectoryName);
        }

        if (fullPath.Name != "")
        {
          using (Stream output = File.Open(fullPath.FullName, FileMode.Create, FileAccess.Write))
          {
            CopyStream(output, file.Stream);
            output.Close();
          }
        }
      };
    }

    /// <summary>
    /// UnZip all files in a zip archive on disk to a file system directory.
    /// </summary>
    /// <param name="zipFilePath">Path to zip-file.</param>
    /// <param name="destinationPath">Destination file system base path.</param>
    public static void UnZipTo(string zipFilePath, string destinationPath)
    {
      using (Stream stream = File.OpenRead(zipFilePath))
      {
        UnZipTo(stream, destinationPath);
        stream.Close();
      }
    }
  }
}
