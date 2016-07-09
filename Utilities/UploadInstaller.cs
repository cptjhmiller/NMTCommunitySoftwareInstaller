using System;
using System.Collections.Generic;
using System.Text;
using TransmissionApplicationUploader;
using com.nmtinstaller.csi.Properties;
using System.IO;
using System.Threading;
using System.Security.Policy;
using System.Web;
using com.nmtinstaller.csi.RemoteFileInfo;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Utilities
{

    public delegate void InstallationFinished(bool NoErrors, string InstallResult);


    public class UploadInstaller
    {
        private string LocalFolder;
        private RepositoryFileInfoBase InstallItem;
        private bool CleanupAfterInstall;
        private string UploadLocation;
        private List<string> ExecuteScriptSequence;
        private List<string> DeleteFoldersBeforeInstall;
        private List<string> DeleteFilesBeforeInstall;

        public bool InstallationFinished { get; private set; }
        public string InstallResult { get; private set; }
        private InstallationFinished OnInstallationFinished;
        private Functions.UpdateProgressInfo OnProgressChanged;

        private void FillMetaInfo()
        {
            HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));
            if ((Directory.GetFiles(LocalFolder).Length == 0) && (Directory.GetDirectories(LocalFolder).Length == 1))
            {
                LocalFolder = Directory.GetDirectories(LocalFolder)[0] + Path.DirectorySeparatorChar;
            }

            if (InstallItem is RepositoryApplicationInfo)
            {
                RepositoryApplicationInfo app = (RepositoryApplicationInfo)InstallItem;
                UploadLocation = "/";
                CleanupAfterInstall = (app.Name != string.Empty);

                //executing commands (firmare, clean etc) will not have an appname
                //and will not require a appinit or installprepare
                if (app.Name != string.Empty)
                {
                    ExecuteScriptSequence.Add(Settings.Default.AppInitScriptURL.Substring(Settings.Default.AppInitScriptURL.LastIndexOf("/") + 1));
                    if (!app.InstallScript.StartsWith("Apps/"))
                    {
                        ExecuteScriptSequence.Add(string.Format(Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1) + "?autostart_add={0}&webservice_name={1}&webservice_url={2}", "", HttpCommands.UrlEncode(app.Name), HttpCommands.UrlEncode(app.GayaInterfaceURL)));
                    }
                    else
                    {
                        ExecuteScriptSequence.Add(string.Format(Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1) + "?autostart_add={0}&webservice_name={1}&webservice_url={2}", "", string.Empty, string.Empty));
                    }
                }
                ExecuteScriptSequence.Add(app.InstallScript);

                DeleteFoldersBeforeInstall.Clear();
                DeleteFilesBeforeInstall.Clear();
            }

            if (InstallItem is RepositoryThemeInfo)
            {
                RepositoryThemeInfo theme = (RepositoryThemeInfo)InstallItem;
                UploadLocation = "/Photo/_theme_/";
                CleanupAfterInstall = false;

                ExecuteScriptSequence.Clear();

                DeleteFoldersBeforeInstall.Add(UploadLocation);
                DeleteFilesBeforeInstall.Add("/Photo/_theme_/.config");
            }

            if (InstallItem is RepositoryCustomMenuInfo)
            {

                if (det.RepositoryType == "AB")
                {
                    RepositoryCustomMenuInfo customMenu = (RepositoryCustomMenuInfo)InstallItem;

                    if (Directory.GetDirectories(LocalFolder, "Photo").Length > 0)
                    {
                        UploadLocation = "/";
                    }
                    else
                    {
                        UploadLocation = "/Photo/_index_/";
                    }

                    CleanupAfterInstall = false;

                    ExecuteScriptSequence.Clear();

                    DeleteFoldersBeforeInstall.Add("/Photo/_index_/");
                    DeleteFilesBeforeInstall.Add("/index.htm");
                }

                if (det.RepositoryType == "A4")
                {
                    RepositoryCustomMenuInfo customMenu = (RepositoryCustomMenuInfo)InstallItem;

                    UploadLocation = "/.home/";
                    CleanupAfterInstall = false;

                    ExecuteScriptSequence.Clear();

                    DeleteFoldersBeforeInstall.Add("/.home/firmwareupdate/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/images/widgets/sevenDaysForecast/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/images/widgets/weatherbug/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/images/menu/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/images/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/xml/.profile/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/xml/data/");
                    DeleteFoldersBeforeInstall.Add("/.home/source/");
                    DeleteFoldersBeforeInstall.Add("/.home/components/popMsg/xml/key/");
                    DeleteFoldersBeforeInstall.Add("/.home/components/popMsg/");
                    DeleteFoldersBeforeInstall.Add("/.home/");
                    
                }

                if (InstallItem is RepositoryWaitImagesInfo)
                {
                    RepositoryWaitImagesInfo waitImages = (RepositoryWaitImagesInfo)InstallItem;
                    UploadLocation = "/Photo/_waitimages_/";
                    CleanupAfterInstall = false;

                    ExecuteScriptSequence.Add(Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1));

                    DeleteFoldersBeforeInstall.Add("/Photo/_waitimages_/");
                    DeleteFilesBeforeInstall.Clear();
                }

                if (InstallItem is RepositoryWebserviceInfo)
                {
                    RepositoryWebserviceInfo webservice = (RepositoryWebserviceInfo)InstallItem;
                    UploadLocation = "/";
                    CleanupAfterInstall = true;

                    ExecuteScriptSequence.Add(string.Format(Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1) + "?autostart_add={0}&webservice_name={1}&webservice_url={2}", "", HttpCommands.UrlEncode(webservice.Name), HttpCommands.UrlEncode(webservice.WebserviceUrl)));

                    DeleteFoldersBeforeInstall.Clear();
                    DeleteFilesBeforeInstall.Clear();
                }
            }
        }


        public void Install()
        {
            new Thread(new ThreadStart(InstallThread)).Start();
        }


        private void InstallThread()
        {
            try
            {
                string installResult = string.Empty;

                //DELETE FILES BEFORE INSTALL
                foreach (string deleteFile in DeleteFilesBeforeInstall)
                {
                    FtpCommands.DeleteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, deleteFile);
                }

                //DELETE FOLDERS BEFORE INSTALL
                foreach (string deleteFolder in DeleteFoldersBeforeInstall)
                {
                    FtpCommands.DeleteFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, deleteFolder);
                }

                //UPLOAD CONTENT
                FtpCommands.UploadFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, LocalFolder, UploadLocation, OnProgressChanged);


                //MOVE FILES TO ROOT WHEN REQUIRED
                if ((InstallItem is RepositoryCustomMenuInfo) && (UploadLocation != "/"))
                {
                    FtpCommands.RenameRemoteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, UploadLocation + "index.htm", "/index.htm", true);
                }
                if (((InstallItem is RepositoryApplicationInfo) || (InstallItem is RepositoryWaitImagesInfo)) && (UploadLocation != "/"))
                {
                    FtpCommands.RenameRemoteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, UploadLocation + Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1), "/" + Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1), true);
                }

                //during installation of an application the appinit should be moved to the root of the HDD
                //when target dir it not the root
                if ((InstallItem is RepositoryApplicationInfo) && (UploadLocation != "/"))
                {
                    FtpCommands.RenameRemoteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "RNFR " + UploadLocation + Settings.Default.AppInitScriptURL.Substring(Settings.Default.AppInitScriptURL.LastIndexOf("/") + 1),"/" + Settings.Default.AppInitScriptURL.Substring(Settings.Default.AppInitScriptURL.LastIndexOf("/") + 1), true);
                }

                //EXECUTE SCRIPTS
                OnProgressChanged(-1, Resources.Text_Installing+" "+InstallItem.Name);
                foreach (string script in ExecuteScriptSequence)
                {
                    //execute start scripts but do not log the output of installing appinit.cgi
                    if (script == Settings.Default.AppInitScriptURL.Substring(Settings.Default.AppInitScriptURL.LastIndexOf('/') + 1))
                    {
                        HttpCommands.StartScript(Settings.Default.Server, script);
                    }
                    else
                    {
                        installResult += HttpCommands.StartScript(Settings.Default.Server, script);
                    }
                }

                //CLEANUP AFTER INSTALL
                if (CleanupAfterInstall)
                {
                    if (Directory.Exists(LocalFolder))
                    {
                        FtpCommands.CleanupFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, LocalFolder, UploadLocation);
                    }
                }
                if ((InstallItem is RepositoryApplicationInfo) || (InstallItem is RepositoryWaitImagesInfo))
                {
                    FtpCommands.DeleteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, Settings.Default.InstallPrepareScriptURL.Substring(Settings.Default.InstallPrepareScriptURL.LastIndexOf("/") + 1));
                }
                if (InstallItem is RepositoryApplicationInfo)
                {
                    FtpCommands.DeleteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, Settings.Default.AppInitScriptURL.Substring(Settings.Default.AppInitScriptURL.LastIndexOf("/") + 1));
                }

                FtpCommands.CloseCachedConnection();
                OnProgressChanged(100, string.Empty);

                OnInstallationFinished(true, installResult);
            }
            catch (Exception ex)
            {
                Logger.GetInstance().AddLogLine(LogLevel.Error, "WebException during install method of UploadInstaller", ex);
                MessageBox.Show(Resources.MessageBox_InstallActionFailed, Resources.MessageBox_InstallationActionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnInstallationFinished(false, InstallResult);
            }
        }

        public UploadInstaller(RepositoryFileInfoBase InstallItem, Functions.UpdateProgressInfo OnUploadProgress, InstallationFinished OnInstallationFinished)
        {
            this.InstallItem = InstallItem;
            this.OnInstallationFinished = OnInstallationFinished;
            this.OnProgressChanged = OnUploadProgress;

            LocalFolder = Constants.TemporaryFolder;
            ExecuteScriptSequence = new List<string>();
            DeleteFoldersBeforeInstall = new List<string>();
            UploadLocation = "/";
            DeleteFilesBeforeInstall = new List<string>();

            CleanupAfterInstall = true;

            InstallationFinished = false;
            InstallResult = string.Empty;

            FillMetaInfo();
        }
    }
}
