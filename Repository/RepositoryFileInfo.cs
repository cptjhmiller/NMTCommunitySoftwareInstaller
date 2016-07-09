using System;
using System.Collections.Generic;
using System.Text;

namespace com.nmtinstaller.csi.RemoteFileInfo
{
    public abstract class RepositoryFileInfoBase
    {
        protected string _name;
        protected string _author;
        protected string _maintainer;
        protected string _version;
        protected string _homepage;
        protected string _forum;
        protected string _description;
        protected string _usageInstructions;
        protected string _downloadURL;
        protected string[] _screenshots;
        protected bool _isNew;
        protected bool _updateAvailable;
        protected string _changelog;
        private bool _installed;


        public string Name { get { return _name; } }
        public string Author { get { return _author; } }
        public string Maintainer { get { return _maintainer; } }
        public string Version { get { return _version; } }
        public string Homepage { get { return _homepage; } }
        public string Forum { get { return _forum; } }
        public string Description { get { return _description; } }
        public string UsageInstructions { get { return _usageInstructions; } }
        public string DownloadURL { get { return _downloadURL; } }
        public string[] Screenshots { get { return _screenshots; } }
        public bool IsNew { get { return _isNew; } set { _isNew = value; } }
        public bool UpdateAvailable { get { return _updateAvailable; } set { _updateAvailable = value; } }
        public bool Installed { get { return _installed; } set { _installed = value; } }
        public string Changelog { get { return _changelog; } }

        public override string ToString()
        {
            return Name + " " + Version;
        }

        public RepositoryFileInfoBase(string Name, string Author, string Maintainer, string Version, string Homepage, string Forum, string Description, string Changelog, string UsageInstructions, string DownloadURL, string[] Screenshots)
        {
            _name = Name;
            _author = Author;
            _maintainer = Maintainer;
            _version = Version;
            _homepage = Homepage;
            _forum = Forum;
            _description = Description;
            _changelog = Changelog;
            _usageInstructions = UsageInstructions;
            _downloadURL = DownloadURL;
            _screenshots = Screenshots;
            _isNew = false;
            _updateAvailable = false;
            _installed = false;
        }
    }
}
