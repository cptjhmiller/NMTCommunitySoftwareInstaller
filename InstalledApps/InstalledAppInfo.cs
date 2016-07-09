using System;
using System.Collections.Generic;
using System.Text;

namespace com.nmtinstaller.csi.InstalledApps
{
    public struct InstalledAppInfo
    {
        private string _path;
        private int _appinfo_format;
        private string _name;
        private string _version;
        private bool _enabled;
        private string _daemon_script;
        private string _crontab;
        private string _setup_script;
        private bool _started;
        private string _gayaui_path;
        private string _webui_path;

        public bool Started { get { return _started; } }
        public string Path { get { return _path; } }
        public int Appinfo_format { get { return _appinfo_format; } }
        public string Name { get { return _name; } }
        public string Version { get { return _version; } }
        public bool Enabled { get { return _enabled; } }
        public string Daemon_script { get { return _daemon_script; } }
        public string Crontab { get { return _crontab; } }
        public string Setup_script { get { return _setup_script; } }
        public string Gayaui_path { get { return _gayaui_path; } }
        public string Webui_path { get { return _webui_path; } }

        public InstalledAppInfo(bool started, string path, int appinfo_format, string name, 
            string version, bool enabled, string daemon_script, string crontab, 
            string setup_script, string gayaui_path, string webui_path)
        {
            _started = started;
            _path = path;
            _appinfo_format = appinfo_format;
            _name = name;
            _version = version;
            _enabled = enabled;
            _daemon_script = daemon_script;
            _crontab = crontab;
            _setup_script = setup_script;
            _gayaui_path = gayaui_path;
            _webui_path = webui_path;
        }
    }
}
