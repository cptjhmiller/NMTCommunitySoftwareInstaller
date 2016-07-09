using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Windows.Forms;
using System.Threading;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Utilities
{


    public class FtpCommands
    {

        private static TcpClient cachedClient = null;
        private static string FtpFolderPrefix = null;


        private static string FixFtpFolderPrefix(string server, string username, string password, string location)
        {
            if (FtpFolderPrefix != "/" + Settings.Default.FTPSubFolder + "/")
            {
                HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));

                if (Settings.Default.FTPSubFolder.Length > 0)
                {
                    FtpFolderPrefix = "/" + Settings.Default.FTPSubFolder + "/";
                }
                else
                {
                    FtpFolderPrefix = "/";
                }
            }
            if (Settings.Default.FTPSubFolder != "")
            {
                GuessFolder = IsFtpCorrect(server, username, password);
                if (GuessFolder == FtpFolderPrefix)
                {
                    return FtpFolderPrefix + ((location[0] == '/') ? location.Substring(1) : location);
                }
                else
                {
                    return GuessFolder + ((location[0] == '/') ? location.Substring(1) : location);
                }
            }
            else
            {
                return FtpFolderPrefix + ((location[0] == '/') ? location.Substring(1) : location);
            }
        }

        public static void CloseCachedConnection()
        {
            if ((cachedClient != null) && (cachedClient.Connected))
            {
                cachedClient.Close();
            }
            cachedClient = null;
        }

        private static string IsFtpCorrect(string server, string username, string password)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.KeepAlive = false;

            WebResponse resp = request.GetResponse();
            StreamReader sr = new StreamReader(resp.GetResponseStream());


            string[] comp = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            sr.Close();
            resp.Close();
            //List<string> returnValue = new List<string>();
            foreach (string line in comp)
            {
                // Windows FTP Server Response Format
                // DateCreated    IsDirectory    Name
                string data = line;

                // Parse date
                //string date = data.Substring(0, 17);
                //DateTime dateTime = DateTime.Parse(date);
                
                //data = data.Remove(0, 24);
                //data = data.Remove(0, 5);
                //data = data.Remove(0, 10);
                //data = data.Remove(0, 23);

                // Parse <DIR>
                //string dir = data.Substring(0, 5);
                //bool isDirectory = dir.Equals("<dir>", StringComparison.InvariantCultureIgnoreCase);
                data = data.Remove(0, 62);

                int index = data.IndexOf(" ");
                if (index > 0)
                {
                    data = data.Substring(0, index);
                }
                data = "/" + data + "/";

                if (data != "/NETWORK_SHARE/")
                {
                    if (IsFtpFolderCorrect(server, username, password, data + "Video/"))
                    {
                        return data;
                    }
                }
                
            }

            return null;
        }
        

        private static void GetSubFolders(string server, string username, string password, string root, List<string> foundItems)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + root);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.KeepAlive = false;

            WebResponse resp = request.GetResponse();
            StreamReader str = new StreamReader(resp.GetResponseStream());

            string line;
            do
            {
                line = str.ReadLine();

                if ((line != null) && (line.StartsWith("d")))
                {

                    string pathname = Regex.Match(line, @".*?\s\d{2}:\d{2}\s(.+)$").Groups[1].Value;

                    foundItems.Add(root + pathname + "/");
                    GetSubFolders(server, username, password, root + pathname + "/", foundItems);
                }
            }
            while (!string.IsNullOrEmpty(line));

            str.Close();
            resp.Close();
        }

        private static void DeleteFolderFiles(string server, string username, string password, string folder)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + folder);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.KeepAlive = false;


            WebResponse resp = request.GetResponse();
            StreamReader str = new StreamReader(resp.GetResponseStream());
            List<string> tobedeleted = new List<string>();
            string line;
            do
            {
                line = str.ReadLine();

                if ((line != null) && (!line.StartsWith("d")))
                {
                    string pathname = Regex.Match(line, @".*?\s\d{2}:\d{2}\s(.+)$").Groups[1].Value;
                    tobedeleted.Add(folder + pathname);
                }
            }
            while (!string.IsNullOrEmpty(line));
            str.Close();
            resp.Close();

            foreach (string file in tobedeleted)
            {
                FtpCommands.SendCommand(server, username, password, "DELE " + file, (tobedeleted[tobedeleted.Count-1]!=file));
            }
        }

        private static void RemoveFolder(string server, string username, string password, string folder)
        {
            //FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + folder);
            //request.Credentials = new NetworkCredential(username,password);
            //request.Method = WebRequestMethods.Ftp.RemoveDirectory;

            //try
            //{
            //    request.GetResponse().Close();
            //}
            //catch (WebException)
            //{
            //}
            FtpCommands.SendCommand(server, username, password, "RMD " + folder, false);
        }

        private static bool IsFtpFolderCorrect(string server, string username, string password, string folder)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + folder);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.KeepAlive = false;

            try
            {
                WebResponse resp = request.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());

                string comp = sr.ReadToEnd();
                sr.Close();
                resp.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool IsFtpFolderPresent(string server, string username, string password, string folder)
        {
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + folder);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.KeepAlive = false;

            try
            {
                WebResponse resp = request.GetResponse();
                StreamReader sr = new StreamReader(resp.GetResponseStream());

                string comp = sr.ReadToEnd();
                sr.Close();
                resp.Close();

                return comp.Length > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void DeleteFolder(string server, string username, string password, string folder)
        {
            folder = FixFtpFolderPrefix(server,username,password,folder);

            //check first of folder there
            if (IsFtpFolderCorrect(server, username, password, folder))
            {

                List<string> foundItems = new List<string>();

                try
                {
                    GetSubFolders(server, username, password, folder, foundItems);
                }
                catch { }

                foreach (string path in foundItems)
                {
                    DeleteFolderFiles(server, username, password, path);
                }


                //sort by number of slahes!
                for (int current = 0; current < foundItems.Count - 1; current++)
                {
                    int biggest = current;
                    for (int check = current + 1; check < foundItems.Count; check++)
                    {
                        if (foundItems[biggest].Split('/').Length < foundItems[check].Split('/').Length)
                        {
                            biggest = check;
                        }
                    }

                    if (biggest != current)
                    {
                        string temp = foundItems[current];
                        foundItems[current] = foundItems[biggest];
                        foundItems[biggest] = temp;
                    }
                }



                foreach (string path in foundItems)
                {
                    RemoveFolder(server, username, password, path);
                }


                //now delete the root folder!
                try
                {
                    FtpCommands.DeleteFolderFiles(server, username, password, folder);
                }
                catch { }

                FtpCommands.SendCommand(server, username, password, "RMD "+folder, false);
            }
        }

        public static bool TestFTPserver(string server, string username, string password)
        {
            FtpWebRequest upload = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server);
            upload.Credentials = new NetworkCredential(username, password);
            upload.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            upload.KeepAlive = false;

            try
            {
                WebResponse resp = upload.GetResponse();
                resp.Close();
                return true;
            }
            catch (WebException wex)
            {
                Logger.GetInstance().AddLogLine(LogLevel.Error, "FTP connection test failed with:", wex);
                return false;
            }
        }

        public static bool UploadFolder(string server, string username, string password, string localFolder, string remoteFolder, Functions.UpdateProgressInfo OnProgressChange)
        {
            remoteFolder = FixFtpFolderPrefix(server, username, password, remoteFolder);

            List<string> createdFTPFolders = new List<string>();
            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(localFolder, "*.*", SearchOption.AllDirectories));
            int totalFileSize = 0;
            int totalUploaded = 0;

            int c = 0;
            while (c < files.Count)
            {
                
                if (files[c].Contains("\\.svn\\"))
                {
                    files.RemoveAt(c);
                } 
                else
                if ((!files[c].EndsWith(Path.DirectorySeparatorChar+".config")) && (Regex.Match(files[c], @"\\\.[^\\]*$").Success))
                {
                    files.RemoveAt(c);
                }
                else
                if (files[c].EndsWith("\\Thumbs.db"))
                {
                    files.RemoveAt(c);
                }
                else
                {
                    totalFileSize += (int)new FileInfo(files[c]).Length;
                    c++;
                }
            }


            foreach (string file in files)
            {
                string relativeRemotePath = "/";
                if (file.Length > localFolder.Length)
                {
                    relativeRemotePath = file.Substring(localFolder.Length, (file.Length - localFolder.Length));
                }


                relativeRemotePath = remoteFolder + relativeRemotePath.Replace('\\', '/');
                relativeRemotePath = relativeRemotePath.Substring(0, relativeRemotePath.LastIndexOf('/') + 1);


                //create all folders
                string[] paths = relativeRemotePath.Substring(1).Split(new char[]{'/'}, StringSplitOptions.RemoveEmptyEntries);
                string create = string.Empty;
                for (int i = 0; i < paths.Length; i++)
                {
                    create += paths[i] + "/";

                    if (!createdFTPFolders.Contains(create))
                    {
                        FtpCommands.SendCommand(server, username, password, "MKD " + create, i < (paths.Length-1));
                        createdFTPFolders.Add(create);
                    }
                }

                FtpWebRequest upload = null;
                Stream str = null;

                //work around for the 503 when the parent folder was deleted just before uploading again
                bool done = false;
                int Retries = Settings.Default.FileUploadRetries553;
                while (!done)
                {
                    try
                    {
                        upload = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + relativeRemotePath + Path.GetFileName(file));
                        upload.Credentials = new NetworkCredential(username, password);
                        upload.UsePassive = true;
                        upload.UseBinary = true;
                        upload.KeepAlive = false;

                        upload.Method = WebRequestMethods.Ftp.UploadFile;
                        str = upload.GetRequestStream();
                        done = true;
                        
                    }
                    catch (WebException ex)
                    {
                        try
                        {
                            str.Close();
                        }
                        catch { }

                        if ((!ex.Message.Contains("553")) || (Retries == 0))
                        {
                            throw ex;
                        }
                        else
                        {
                            Retries--;
                            Thread.Sleep(1000);
                        }
                    }
                }

                byte[] uploaddata = new byte[10240];
                FileStream uploadFile = File.OpenRead(file);
                int PrevValue = -1;

                while (uploadFile.Position < uploadFile.Length)
                {
                    int read = uploadFile.Read(uploaddata, 0, uploaddata.Length);
                    totalUploaded += read;

                    str.Write(uploaddata, 0, read);

                    if (OnProgressChange != null)
                    {
                        int Procent = (int)((totalUploaded / (float)totalFileSize) * 100);
                        if (PrevValue != Procent)
                        {
                            OnProgressChange(Procent, "Uploading files");
                            PrevValue = Procent;
                        }
                    }
                }

                str.Close();
                uploadFile.Close();
                upload.GetResponse().Close();
            }

            return true;
        }

        public static bool DeleteFile(string server, string username, string password, string remoteFile)
        {
            remoteFile = FixFtpFolderPrefix(server, username, password, remoteFile);

            try
            {
                FtpCommands.SendCommand(server,username,password, "DELE "+remoteFile, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool CleanupFolder(string server, string username, string password, string localFolder, string remoteFolder)
        {
            remoteFolder = FixFtpFolderPrefix(server, username, password, remoteFolder);

            List<string> files = new List<string>();
            files.AddRange(Directory.GetFiles(localFolder, "*.*", SearchOption.AllDirectories));

            int c = 0;
            while (c < files.Count)
            {
                if (files[c].Contains("\\.svn\\"))
                {
                    files.RemoveAt(c);
                }
                else
                {
                    c++;
                }
            }


            List<string> deleteFolders = new List<string>();
            foreach (string file in files)
            {
                string relativeRemotePath = "/";
                if (file.Length > localFolder.Length)
                {
                    relativeRemotePath = file.Substring(localFolder.Length + 1, (file.Length - (localFolder.Length + 1)));
                }


                relativeRemotePath = remoteFolder + relativeRemotePath.Replace('\\', '/');
                relativeRemotePath = relativeRemotePath.Substring(0, relativeRemotePath.LastIndexOf('/') + 1);


                FtpWebRequest upload = (FtpWebRequest)FtpWebRequest.Create("ftp://" + server + relativeRemotePath + Path.GetFileName(file));
                upload.Credentials = new NetworkCredential(username, password);
                upload.UsePassive = true;
                upload.UseBinary = true;
                upload.KeepAlive = false;

                upload.Method = WebRequestMethods.Ftp.DeleteFile;

                try
                {
                    upload.GetResponse().Close();
                }
                catch (WebException)
                {
                    //don't matter if file can't be deleted
                }

                //paranoide? I think so ;-)
                if (relativeRemotePath != "/")
                {
                    if (!deleteFolders.Contains(relativeRemotePath))
                    {
                        deleteFolders.Add(relativeRemotePath);
                    }
                }
            }

            //sort by number of slahes!
            for (int current = 0; current < deleteFolders.Count - 1; current++)
            {
                int biggest = current;
                for (int check = current + 1; check < deleteFolders.Count; check++)
                {
                    if (deleteFolders[biggest].Split('/').Length < deleteFolders[check].Split('/').Length)
                    {
                        biggest = check;
                    }
                }

                if (biggest != current)
                {
                    string temp = deleteFolders[current];
                    deleteFolders[current] = deleteFolders[biggest];
                    deleteFolders[biggest] = temp;
                }
            }

            foreach (string folder in deleteFolders)
            {
                FtpCommands.SendCommand(server, username, password, "RMD " + folder, (deleteFolders[deleteFolders.Count-1]!=folder));
            }


            return true;
        }

        private static string SendNetworkCommand(TcpClient client, string command)
        {
            string commandResponse = string.Empty;

            byte[] response = new byte[1024];
            ASCIIEncoding ascenc = new ASCIIEncoding();
            byte[] data = ascenc.GetBytes(command + Environment.NewLine);
            client.GetStream().Write(data, 0, data.Length);
            client.GetStream().Flush();

            int bytesRead = 0;
            do
            {
                bytesRead = client.GetStream().Read(response, 0, response.Length);
                commandResponse += ascenc.GetString(response, 0, bytesRead);
            }
            while (bytesRead == response.Length);

            return (commandResponse);
        }

        private static string Connect(string server, int port, out TcpClient client)
        {
            client = new TcpClient();
            client.Connect(server, port);
            ASCIIEncoding ascenc = new ASCIIEncoding();
            string commandResponse = string.Empty;
            byte[] response = new byte[1024];
            int bytesRead = 0;
            do
            {
                bytesRead = client.GetStream().Read(response, 0, response.Length);
                commandResponse += ascenc.GetString(response, 0, bytesRead);
            }
            while (bytesRead == response.Length);

            return (commandResponse);
        }

        public static void RenameRemoteFile(string server, string username, string password, string oldfilename, string newfilename, bool keepOpen)
        {
            newfilename = FixFtpFolderPrefix(server, username, password, newfilename);
            oldfilename = FixFtpFolderPrefix(server, username, password, oldfilename);

            SendCommand(server, username, password, "RNFR " + oldfilename, keepOpen);
            SendCommand(server, username, password, "RNTO " + newfilename, keepOpen);
        }

        public static void SetExecutePermissions(string server, string username, string password, string filename, bool keepOpen)
        {
            filename = FixFtpFolderPrefix(server, username, password, filename);
            FtpCommands.SendCommand(server, username, password, "SITE chmod 777 " + filename, false);
        }

        private static void SendCommand(string server, string username, string password, string command, bool keepOpen)
        {
            string response;
            TcpClient client = cachedClient;

            try
            {
                if ((client == null) || (!client.Connected))
                {
                    response = Connect(server, 21, out client);
                    response = SendNetworkCommand(client, "user " + username);
                    response = SendNetworkCommand(client, "pass " + password);
                }

                response = SendNetworkCommand(client, command);

                if (!keepOpen)
                {
                    cachedClient = null;
                    response = SendNetworkCommand(client, "quit");
                    client.Close();
                }
                else
                {
                    cachedClient = client;
                }
            }
            catch (Exception e)
            {
                try
                {
                    client.Close();
                }
                catch { }

                cachedClient = null;
                throw e;
            }
        }

        public static string GuessFolder { get; set; }
    }
}
