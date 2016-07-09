using TransmissionApplicationUploader;

namespace com.nmtinstaller.csi.RemoteFileInfo
{
    public class RepositoryThemeInfo : RepositoryFileInfoBase
    {
        protected bool _forHD;
        protected bool _forSD;

        public bool ForHD { get { return _forHD; } }
        public bool ForSD { get { return _forSD; } }

        public RepositoryThemeInfo(string Name, string Author, string Maintainer, string Version, string Homepage, string Forum, string Description, string Changelog, string UsageInstructions, string DownloadURL, string[] Screenshots, bool ForHD, bool ForSD)
            : base(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, Screenshots)
        {
            this._forHD = ForHD;
            this._forSD = ForSD;
        }
    }
}