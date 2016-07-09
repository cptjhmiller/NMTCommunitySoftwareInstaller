using System;
using System.Collections.Generic;
using System.Text;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.Properties;
using System.Text.RegularExpressions;
using com.nmtinstaller.csi.RemoteFileInfo;

namespace com.nmtinstaller.csi.InstalledApps
{
    public delegate void UpdateComplete();

    public class InstalledAppsManager
    {
        public List<InstalledAppInfo> InstalledApps = new List<InstalledAppInfo>();
        private UpdateComplete OnUpdateComplete;
        private Repository repository;

        public void SetUpdateDelegate(UpdateComplete newdelegate)
        {
            OnUpdateComplete = newdelegate;
        }

        private string GetJSONValue(string Application, string Name)
        {
            return Regex.Match(Application, "\\s*\"?" + Name + "\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
        }

        public void UpdateAppInfo()
        {
            InstalledApps = new List<InstalledAppInfo>();
            string Info = string.Empty;

            try
            {
                Info = HttpCommands.StartScript(Settings.Default.Server, "Apps/AppInit/appinit.cgi?info");
            }
            catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Warning, "", ex); }
            //catch { } my edit

            string appinfo = string.Empty;

            try
            {
                appinfo = Info.Substring(Info.IndexOf('{'));
                appinfo = appinfo.Substring(0, appinfo.LastIndexOf('}') + 1);
            }
            catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Warning, "", ex); }
            //catch { } my edit

            foreach (string app in appinfo.Split(new char[]{'}'}, StringSplitOptions.RemoveEmptyEntries))
            {
                try
                {
                    string path = GetJSONValue(app,"path");// Regex.Match(app, "\\s*\"?path\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    string name = GetJSONValue(app, "name");//Regex.Match(app, "\\s*\"?name\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    bool started = GetJSONValue(app, "started") == "1";//Regex.Match(app, "\\s*\"?started\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value == "1";

                    int appinfo_format = int.Parse(GetJSONValue(app, "appinfo_format"));//Regex.Match(app, "\\s*\"?appinfo_format\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value);
                    string version = GetJSONValue(app, "version");//Regex.Match(app, "\\s*\"?version\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    bool enabled = GetJSONValue(app, "enabled") == "1";//Regex.Match(app, "\\s*\"?enabled\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value == "1";
                    string daemon_script = GetJSONValue(app, "daemon_script");//Regex.Match(app, "\\s*\"?daemon_script\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    string crontab = GetJSONValue(app, "crontab");//Regex.Match(app, "\\s*\"?crontab\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    string setup_script = GetJSONValue(app, "setup_script");//Regex.Match(app, "\\s*\"?setup_script\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    string gayaui_path = GetJSONValue(app, "gayaui_path");//Regex.Match(app, "\\s*\"?gayaui_path\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;
                    string webui_path = GetJSONValue(app, "webui_path");//Regex.Match(app, "\\s*\"?webui_path\"?\\s*[=:]{1}\\s*\"?([^\",]+)\"?\\s*").Groups[1].Value;

                    InstalledApps.Add(new InstalledAppInfo(started, path, appinfo_format, name, version, enabled, daemon_script, crontab, setup_script, gayaui_path, webui_path));
                }
                catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Warning, "", ex); }
                //catch { } my edit
            }

            UpdateRepositoryStates();

            if (OnUpdateComplete != null)
            {
                OnUpdateComplete();
            }
        }

        private Version StringToVersion(string strVersion)
        {
            //trip all characters
            //20090501-BETA1-2 will be translated in version v20090501-1-2
            strVersion = Regex.Replace(strVersion, @"[^0-9\-._]", "");

            string[] splitted = strVersion.Split(new char[] { '.', '_', '-' });
            int[] versionparts = new int[4];

            //try parsing the individual integers, skip invalid values
            List<int> splittedParts = new List<int>();
            foreach (string part in splitted)
            {
                int value;
                if (int.TryParse(part, out value))
                {
                    splittedParts.Add(value);
                }
            }

            for (int i = 0; ((i < splittedParts.Count) && (i<versionparts.Length)); i++)
            {
                versionparts[i] = splittedParts[i];
            }

            return new Version(versionparts[0], versionparts[1], versionparts[2], versionparts[3]);
        }

        private void UpdateRepositoryStates()
        {
            //set updated state of repository items
            List<RepositoryFileInfoBase> allItems = new List<RepositoryFileInfoBase>();
            allItems.AddRange(repository.Applications.ToArray());
            allItems.AddRange(repository.CustomMenus.ToArray());
            allItems.AddRange(repository.Themes.ToArray());
            allItems.AddRange(repository.WaitImages.ToArray());
            allItems.AddRange(repository.Webservices.ToArray());


            foreach (RepositoryFileInfoBase item in allItems)
            {
                
                int i = 0;
                while ((i < InstalledApps.Count) && (!InstalledApps[i].Name.Equals(item.Name,StringComparison.CurrentCultureIgnoreCase)))
                    i++;

                if (i < InstalledApps.Count)
                {
                    item.UpdateAvailable = (StringToVersion(InstalledApps[i].Version) < StringToVersion(item.Version));
                    item.Installed = true;
                }
                else
                {
                    item.UpdateAvailable = false;
                    item.Installed = false;
                }
            }
        }

        public InstalledAppsManager(UpdateComplete OnUpdateComplete, Repository repository)
        {
            this.OnUpdateComplete = OnUpdateComplete;
            this.repository = repository;
        }
    }
}
