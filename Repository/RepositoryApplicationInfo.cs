using TransmissionApplicationUploader;

namespace com.nmtinstaller.csi.RemoteFileInfo
{
    public class RepositoryApplicationInfo : RepositoryFileInfoBase
    {
        protected string _installScript;
        protected string _gayaInterfaceURL;
        protected string _webInterfaceURL;

        public string InstallScript { get { return _installScript; } }
        public string GayaInterfaceURL { get { return _gayaInterfaceURL; } }
        public string WebInterfaceURL { get { return _webInterfaceURL; } }



        public RepositoryApplicationInfo(string Name, string Author, string Maintainer, string Version, string Homepage, string Forum, string Description, string Changelog, string UsageInstructions, string DownloadURL, string[] Screenshots, string InstallScript, string GayaInterfaceURL, string WebInterfaceURL)
            : base(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, Screenshots)
        {
            this._installScript = InstallScript;
            this._gayaInterfaceURL = GayaInterfaceURL;
            this._webInterfaceURL = WebInterfaceURL;
        }
    }
}