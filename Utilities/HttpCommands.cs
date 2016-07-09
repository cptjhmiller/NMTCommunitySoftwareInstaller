using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using com.nmtinstaller.csi.Properties;
using System.Text.RegularExpressions;
using TransmissionApplicationUploader;
using Com.Tytte.Dk;
using System.Windows.Forms;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Utilities
{
    public static class HttpCommands
    {
        public static string DownloadHTTPFileAndExtract(string completeURL, string LocalFolder, bool ClearTemp, Functions.UpdateProgressInfo OnProgressChanged)
        {
            if (ClearTemp)
            {
                if (Directory.Exists(LocalFolder))
                {
                    Directory.Delete(LocalFolder, true);
                }
            }

            if (!Directory.Exists(LocalFolder))
            {
                Directory.CreateDirectory(LocalFolder);
            }

            DownloadHTTPFile(completeURL, LocalFolder, OnProgressChanged);

            if (completeURL.EndsWith("zip", StringComparison.CurrentCultureIgnoreCase))
            {
                string LocalFile = LocalFolder + completeURL.Substring(completeURL.LastIndexOf('/') + 1);
                SimpleUnZipper.UnZipTo(LocalFile, LocalFolder);
                File.Delete(LocalFile);
            }

            if ((Directory.GetFiles(LocalFolder).Length == 0) && (Directory.GetDirectories(LocalFolder).Length==1))
            {
                LocalFolder = Directory.GetDirectories(LocalFolder)[0] + Path.DirectorySeparatorChar;
            }

            return LocalFolder;
        }

        public static string GetHttpFileContent(string completeURL, Functions.UpdateProgressInfo OnProgressChanged)
        {
            MemoryStream mems = new MemoryStream();

            DownloadHttpFileToStream(completeURL, mems, null);

            return new ASCIIEncoding().GetString(mems.ToArray(), 0, mems.ToArray().Length);
        }

        private static void DownloadHttpFileToStream(string completeURL, Stream target, Functions.UpdateProgressInfo OnProgressChanged)
        {
            int totalSize = 0;

            if (completeURL == Settings.Default.AppInitScriptURL)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(completeURL);



                    WebResponse resp = request.GetResponse();
                    BinaryReader br = new BinaryReader(resp.GetResponseStream());

                    if (totalSize == 0)
                    {
                        totalSize = (int)resp.ContentLength;
                    }

                    int downloadedSize = 0;
                    byte[] buffer;
                    do
                    {
                        buffer = br.ReadBytes(10240);
                        target.Write(buffer, 0, buffer.Length);

                        downloadedSize += buffer.Length;

                        if (OnProgressChanged != null)
                        {
                            OnProgressChanged((int)((downloadedSize / (float)totalSize) * 100), Resources.Text_DownloadingFile);
                        }
                    }
                    while (buffer.Length != 0);

                    br.Close();
                    resp.Close();
                    target.Close();
                }
                catch
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Settings.Default.AppInitScript2URL);



                    WebResponse resp = request.GetResponse();
                    BinaryReader br = new BinaryReader(resp.GetResponseStream());

                    if (totalSize == 0)
                    {
                        totalSize = (int)resp.ContentLength;
                    }

                    int downloadedSize = 0;
                    byte[] buffer;
                    do
                    {
                        buffer = br.ReadBytes(10240);
                        target.Write(buffer, 0, buffer.Length);

                        downloadedSize += buffer.Length;

                        if (OnProgressChanged != null)
                        {
                            OnProgressChanged((int)((downloadedSize / (float)totalSize) * 100), Resources.Text_DownloadingFile);
                        }
                    }
                    while (buffer.Length != 0);

                    br.Close();
                    resp.Close();
                    target.Close();
                }
            }
            else if (completeURL == Settings.Default.InstallPrepareScriptURL)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(completeURL);



                    WebResponse resp = request.GetResponse();
                    BinaryReader br = new BinaryReader(resp.GetResponseStream());

                    if (totalSize == 0)
                    {
                        totalSize = (int)resp.ContentLength;
                    }

                    int downloadedSize = 0;
                    byte[] buffer;
                    do
                    {
                        buffer = br.ReadBytes(10240);
                        target.Write(buffer, 0, buffer.Length);

                        downloadedSize += buffer.Length;

                        if (OnProgressChanged != null)
                        {
                            OnProgressChanged((int)((downloadedSize / (float)totalSize) * 100), Resources.Text_DownloadingFile);
                        }
                    }
                    while (buffer.Length != 0);

                    br.Close();
                    resp.Close();
                    target.Close();
                }
                catch
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(Settings.Default.InstallPrepareScript2URL);



                    WebResponse resp = request.GetResponse();
                    BinaryReader br = new BinaryReader(resp.GetResponseStream());

                    if (totalSize == 0)
                    {
                        totalSize = (int)resp.ContentLength;
                    }

                    int downloadedSize = 0;
                    byte[] buffer;
                    do
                    {
                        buffer = br.ReadBytes(10240);
                        target.Write(buffer, 0, buffer.Length);

                        downloadedSize += buffer.Length;

                        if (OnProgressChanged != null)
                        {
                            OnProgressChanged((int)((downloadedSize / (float)totalSize) * 100), Resources.Text_DownloadingFile);
                        }
                    }
                    while (buffer.Length != 0);

                    br.Close();
                    resp.Close();
                    target.Close();
                }
            }
            else
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(completeURL);



                WebResponse resp = request.GetResponse();
                BinaryReader br = new BinaryReader(resp.GetResponseStream());

                if (totalSize == 0)
                {
                    totalSize = (int)resp.ContentLength;
                }

                int downloadedSize = 0;
                byte[] buffer;
                do
                {
                    buffer = br.ReadBytes(10240);
                    target.Write(buffer, 0, buffer.Length);

                    downloadedSize += buffer.Length;

                    if (OnProgressChanged != null)
                    {
                        OnProgressChanged((int)((downloadedSize / (float)totalSize) * 100), Resources.Text_DownloadingFile);
                    }
                }
                while (buffer.Length != 0);

                br.Close();
                resp.Close();
                target.Close();
            }
        }

        public static void DownloadHTTPFile(string completeURL, string localFolder, Functions.UpdateProgressInfo OnProgressChanged)
        {
            FileStream fs = File.Create(localFolder + completeURL.Substring(completeURL.LastIndexOf("/")));
            try
            {
                DownloadHttpFileToStream(completeURL, fs, OnProgressChanged);
            }
            finally
            {
                fs.Close();
            }
        }

        private static int GetHTTPFileSize(string completeURL)
        {
            int totalSize = 0;
            HttpWebRequest getsize = (HttpWebRequest)HttpWebRequest.Create(completeURL);
            
            getsize.Method = WebRequestMethods.Http.Head;
            WebResponse sizeResponse = getsize.GetResponse();
            totalSize = int.Parse(sizeResponse.Headers[HttpResponseHeader.ContentLength]);
            sizeResponse.Close();

            return totalSize;
        }

        public static string UrlEncode(string input)
        {
            ASCIIEncoding ascenc = new ASCIIEncoding();
            string result = input;
            int startIndex = 0;
            Regex regex = new Regex("[^a-zA-Z0-9\\-_.!*'()/]{1}", RegexOptions.Compiled);
            Match rem = regex.Match(result, startIndex);
            while (rem.Success)
            {
                byte b = ascenc.GetBytes(new char[]{result[rem.Index]}, 0,1)[0];
                result = result.Substring(0, rem.Index) + string.Format("%{0:x}", b) + result.Substring(rem.Index + 1);

                rem = regex.Match(result, rem.Index + 3);
            }

            return result;
        }

        public static string StartScript(string server, string script)
        {
            FtpCommands.SetExecutePermissions(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, script.Split('?')[0], false);

            HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));
            string resquest = string.Empty;
            var port = 0;
            if (det.RepositoryType == "z")
            {
                script = "/share/" + script;
                script = Regex.Replace(script, @"\?", " ");
                resquest = Regex.Replace(script, @"\&", " ");
                resquest += "\n";
                port = 23;
            }
            else
            {
                resquest += "GET " + ((Settings.Default.WebserverSubFolder.Length > 0) ? "/" + Settings.Default.WebserverSubFolder + "/" : "/") + script + " HTTP/1.1" + "\r\n";
                resquest += "Host: localhost.drives:8883" + "\r\n";
                resquest += "\r\n";
                port = 8883;
            }
            

            TcpClient clnt = new TcpClient();
            clnt.Connect(server, port);

            ASCIIEncoding ascend = new ASCIIEncoding();
            byte[] send = ascend.GetBytes(resquest);
            clnt.GetStream().Write(send, 0, send.Length);
            clnt.GetStream().Flush();

            string response = string.Empty;
            byte[] readbuffer = new byte[1024];
            int readbytes = 0;
            //clnt.GetStream().ReadTimeout = 30000;
            do
            {
                readbytes = 0;
                try
                {
                    readbytes = clnt.GetStream().Read(readbuffer, 0, readbuffer.Length);
                    response += ascend.GetString(readbuffer, 0, readbytes);
                }
                catch (IOException)
                {
                }


                Thread.Sleep(100);
            }
            while ((readbytes != 0) && (!response.ToLower().Contains("</html>") && (!response.ToLower().Contains(new string(new char[]{(char)4})))));
            return response;
        }

        public static DateTime GetHTTPFileLastModified(string completeURL)
        {
            DateTime result = DateTime.MinValue;

            try
            {
                HttpWebRequest getLastModified = (HttpWebRequest)HttpWebRequest.Create(completeURL);
                
                getLastModified.Method = WebRequestMethods.Http.Head;
                WebResponse sizeResponse = getLastModified.GetResponse();

                result = DateTime.Parse(sizeResponse.Headers[HttpResponseHeader.LastModified]);
                sizeResponse.Close();
            }
            catch { }

            return result;
        }
    }
}
