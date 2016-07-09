using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using com.nmtinstaller.csi.Properties;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using TransmissionApplicationUploader;
using System.Text.RegularExpressions;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Utilities
{
    public delegate void UpdateRepositoryState(int Procent, string CurrentName);

    public class Repository
    {
        private readonly string RepositoryFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Repository" + Path.DirectorySeparatorChar;

        public DateTime RepositoryDate { get; set; }
        private List<string> RepositoryFiles;

        private List<RepositoryApplicationInfo> _applications = new List<RepositoryApplicationInfo>();
        private List<RepositoryThemeInfo> _themes = new List<RepositoryThemeInfo>();
        private List<RepositoryCustomMenuInfo> _indexes = new List<RepositoryCustomMenuInfo>();
        private List<RepositoryWaitImagesInfo> _waitImagesSets = new List<RepositoryWaitImagesInfo>();
        private List<RepositoryWebserviceInfo> _webservices = new List<RepositoryWebserviceInfo>();

        public List<RepositoryApplicationInfo> Applications { get { return _applications; } }
        public List<RepositoryThemeInfo> Themes { get { return _themes; } }
        public List<RepositoryCustomMenuInfo> CustomMenus { get { return _indexes; } }
        public List<RepositoryWaitImagesInfo> WaitImages { get { return _waitImagesSets; } }
        public List<RepositoryWebserviceInfo> Webservices { get { return _webservices; } }
        
        private UpdateRepositoryState OnUpdate;
        private int CurrentUpdatingIndex;
        private int FoundNumberOfRepositories;

        private string ReplaceKeywords(string text)
        {
            text = text.Replace("[NMT_IP]", Settings.Default.Server);
            return text;
        }

        private static void SelectMainRepository()
        {
            RepositoryLocations _repositoryLocations = new RepositoryLocations();

            string Language = Settings.Default.UILanguage;

            if (!Enum.IsDefined(typeof(HardwareTypeEnum), Settings.Default.HardwareType))
            {
                return;
            }

            HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));

            bool found = false;
            int i = 0;
            while ((i < _repositoryLocations.MainRepositories.Count) && (!found))
            {
                if (_repositoryLocations.MainRepositories[i].Name.Equals("Main_Type" + det.RepositoryType + "_" + Language, StringComparison.CurrentCultureIgnoreCase))
                {
                    found = true;
                }
                else
                {
                    i++;
                }
            }



            if (found)
            {
                bool changed = false;
                foreach (RepositoryLocationInfo info in _repositoryLocations.MainRepositories)
                {
                    if (info.Name.StartsWith("Main_Type" + det.RepositoryType + "_" + Language, StringComparison.CurrentCultureIgnoreCase))
                    {
                        if (!info.Enabled)
                        {
                            info.Enabled = true;
                            changed = true;
                        }
                    }
                    else
                        if (info.Name.StartsWith("Main_", StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (info.Enabled)
                            {
                                info.Enabled = false;
                                changed = true;
                            }
                        }
                }

                if (changed)
                {
                    _repositoryLocations.SaveRepository();
                }
            }
            else
            {
                foreach (RepositoryLocationInfo info in _repositoryLocations.MainRepositories)
                {
                    if (info.Name.StartsWith("Main_Type" + det.RepositoryType + "_English", StringComparison.CurrentCultureIgnoreCase))
                    {
                        info.Enabled = true;
                    }
                    else
                        if (info.Name.StartsWith("Main_", StringComparison.CurrentCultureIgnoreCase))
                        {
                            info.Enabled = false;
                        }
                }

                _repositoryLocations.SaveRepository();
            }
        }

        public void LoadRepositoryFiles()
        {
            _applications.Clear();
            _themes.Clear();
            _indexes.Clear();
            _waitImagesSets.Clear();
            _webservices.Clear();

            RepositoryDate = DateTime.MinValue;

            string[] RepositoryFiles = Directory.GetFiles(RepositoryFolder, "*.xml");

            foreach (string Filename in RepositoryFiles)
            {

                try
                {
                    LoadRepositoryFile(Filename, _applications, _indexes, _themes, _webservices, _waitImagesSets);


                    //all was okay, get change date
                    if (RepositoryDate < File.GetLastWriteTime(Filename))
                    {
                        RepositoryDate = File.GetLastWriteTime(Filename);
                    }
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Repository file " + Filename + " is invalid", ex);
                    MessageBox.Show(string.Format(Resources.MessageBox_RepositoryUpdateFileInvalid, Path.GetFileName(Filename)), Resources.MessageBox_RepositoryUpdate, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void LoadRepositoryFile(string Filename, List<RepositoryApplicationInfo> Applications, List<RepositoryCustomMenuInfo> CustomMenus, List<RepositoryThemeInfo> Themes, List<RepositoryWebserviceInfo> Webservices, List<RepositoryWaitImagesInfo> WaitimageSets)
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(Filename);

            XmlNodeList selection = null;

            //applications
            if (doc.SelectSingleNode("Repository/Applications/Application") != null)
            {
                selection = doc.SelectNodes("Repository/Applications/Application");
                foreach (XmlNode node in selection)
                {
                    string Name;
                    string Author;
                    string Maintainer;
                    string Version;
                    string Description;
                    string UsageInstructions;
                    string Homepage;
                    string DownloadURL;
                    string[] ScreenshotUrls;
                    string Changelog;
                    string Forum;
                    ParseFileInformation(node, out Name, out Author, out Maintainer, out Version, out Description, out Changelog, out UsageInstructions, out Homepage, out Forum, out DownloadURL, out ScreenshotUrls);

                    string InstallScript = node.SelectSingleNode("InstallScript").InnerText.Trim();
                    string GayaInterfaceURL = FixStrings(node.SelectSingleNode("GayaInterfaceURL").InnerText.Trim());
                    string WebInterfaceURL = FixStrings(node.SelectSingleNode("WebInterfaceURL").InnerText.Trim());

                    RepositoryApplicationInfo rmf = new RepositoryApplicationInfo(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, ScreenshotUrls, InstallScript, GayaInterfaceURL, WebInterfaceURL);
                    Applications.Add(rmf);
                }
            }


            //indexes
            if (doc.SelectSingleNode("Repository/Indexes/Index") != null)
            {
                selection = doc.SelectNodes("Repository/Indexes/Index");
                foreach (XmlNode node in selection)
                {
                    string Name;
                    string Author;
                    string Maintainer;
                    string Version;
                    string Description;
                    string UsageInstructions;
                    string Homepage;
                    string DownloadURL;
                    string[] ScreenshotUrls;
                    string Changelog;
                    string Forum;
                    ParseFileInformation(node, out Name, out Author, out Maintainer, out Version, out Description, out Changelog, out UsageInstructions, out Homepage, out Forum, out DownloadURL, out ScreenshotUrls);

                    RepositoryCustomMenuInfo rmf = new RepositoryCustomMenuInfo(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, ScreenshotUrls);
                    CustomMenus.Add(rmf);
                }
            }


            //themes
            if (doc.SelectSingleNode("Repository/Themes/Theme") != null)
            {
                selection = doc.SelectNodes("Repository/Themes/Theme");
                foreach (XmlNode node in selection)
                {
                    string Name;
                    string Author;
                    string Maintainer;
                    string Version;
                    string Description;
                    string UsageInstructions;
                    string Homepage;
                    string DownloadURL;
                    string[] ScreenshotUrls;
                    string Changelog;
                    string Forum;
                    ParseFileInformation(node, out Name, out Author, out Maintainer, out Version, out Description, out Changelog, out UsageInstructions, out Homepage, out Forum, out DownloadURL, out ScreenshotUrls);

                    bool ForHD = node.SelectSingleNode("Formats/HD").InnerText.Trim().ToLower() == "true";
                    bool ForSD = node.SelectSingleNode("Formats/SD").InnerText.Trim().ToLower() == "true";


                    RepositoryThemeInfo rmf = new RepositoryThemeInfo(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, ScreenshotUrls, ForHD, ForSD);
                    Themes.Add(rmf);
                }
            }

            //waitimageset
            if (doc.SelectSingleNode("Repository/WaitImageSets/WaitImageSet") != null)
            {
                selection = doc.SelectNodes("Repository/WaitImageSets/WaitImageSet");
                foreach (XmlNode node in selection)
                {
                    string Name;
                    string Author;
                    string Maintainer;
                    string Version;
                    string Description;
                    string UsageInstructions;
                    string Homepage;
                    string DownloadURL;
                    string[] ScreenshotUrls;
                    string Changelog;
                    string Forum;
                    ParseFileInformation(node, out Name, out Author, out Maintainer, out Version, out Description, out Changelog, out UsageInstructions, out Homepage, out Forum, out DownloadURL, out ScreenshotUrls);

                    RepositoryWaitImagesInfo rmf = new RepositoryWaitImagesInfo(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, ScreenshotUrls);
                    WaitimageSets.Add(rmf);
                }
            }

            //webservices
            if (doc.SelectSingleNode("Repository/Webservices/Webservice") != null)
            {
                selection = doc.SelectNodes("Repository/Webservices/Webservice");
                foreach (XmlNode node in selection)
                {
                    string Name;
                    string Author;
                    string Maintainer;
                    string Version;
                    string Description;
                    string Changelog;
                    string Forum;
                    string UsageInstructions;
                    string Homepage;
                    string DownloadURL;
                    string[] ScreenshotUrls;
                    ParseFileInformation(node, out Name, out Author, out Maintainer, out Version, out Description, out Changelog, out UsageInstructions, out Homepage, out Forum, out DownloadURL, out ScreenshotUrls);

                    string WebserviceUrl = node.SelectSingleNode("WebserviceUrl").InnerText.Trim();

                    RepositoryWebserviceInfo wes = new RepositoryWebserviceInfo(Name, Author, Maintainer, Version, Homepage, Forum, Description, Changelog, UsageInstructions, DownloadURL, ScreenshotUrls, WebserviceUrl);
                    Webservices.Add(wes);
                }
            }
        }

        private void ParseFileInformation(XmlNode node, out string Name, out string Author, out string Maintainer,out string Version, out string Description, out string Changelog, out string UsageInstructions, out string Homepage, out string Forum, out string DownloadURL, out string[] ScreenshotUrls)
        {
            Name = node.SelectSingleNode("Name").InnerText.Trim();
            Author = node.SelectSingleNode("Author").InnerText.Trim();
            Maintainer = FixStrings(node.SelectSingleNode("Maintainer").InnerText.Trim());
            Version = node.SelectSingleNode("Version").InnerText.Trim();
            Description = FixStrings(node.SelectSingleNode("Description").InnerText.Trim());
            UsageInstructions = FixStrings(node.SelectSingleNode("UsageInstructions").InnerText.Trim());
            Homepage = FixStrings(node.SelectSingleNode("Homepage").InnerText.Trim());
            DownloadURL = node.SelectSingleNode("DownloadURL").InnerText.Trim();

            if (node.SelectSingleNode("Forum") != null)
            {
                Forum = FixStrings(node.SelectSingleNode("Forum").InnerText.Trim());
            }
            else
            {
                Forum = string.Empty;
            }

            if (node.SelectSingleNode("Changelog") != null)
            {
                Changelog = FixStrings(node.SelectSingleNode("Changelog").InnerText.Trim());
            }
            else
            {
                Changelog = string.Empty;
            }

            List<string> tmpScreenshotUrls = new List<string>();
            foreach (XmlNode item in node.SelectNodes("Screenshots/URL"))
            {
                tmpScreenshotUrls.Add(item.InnerText.Trim());
            }
            ScreenshotUrls = tmpScreenshotUrls.ToArray();


        }

        private string FixStrings(string input)
        {
            input = input.Replace("\n", "\r\n");
            input = Regex.Replace(input, "^\\s+", "", RegexOptions.Multiline);

            input = ReplaceKeywords(input);
            return input;
        }

        private void DownloadRepositoryFiles(string repositoryName, string repositoryUrl)
        {
            try
            {
                CurrentUpdatingIndex++;
                string LocalXmlFile = repositoryUrl.Substring(repositoryUrl.LastIndexOf('/') + 1);
                LocalXmlFile = repositoryName + "_" + LocalXmlFile.Substring(0, LocalXmlFile.Length - 3) + "xml";
                DateTime serverModifiedDate = HttpCommands.GetHTTPFileLastModified(repositoryUrl);

                if ((!File.Exists(RepositoryFolder + LocalXmlFile)) || ((serverModifiedDate!=DateTime.MinValue) && (File.GetLastWriteTime(RepositoryFolder + LocalXmlFile) != HttpCommands.GetHTTPFileLastModified(repositoryUrl))))
                {
                    if (File.Exists(RepositoryFolder + LocalXmlFile))
                    {
                        File.Delete(RepositoryFolder + LocalXmlFile);
                    }

                    string LocalDownloadFile = string.Empty;
                    try
                    {

                        LocalDownloadFile = repositoryUrl.Substring(repositoryUrl.LastIndexOf('/') + 1);
                        LocalDownloadFile = LocalDownloadFile.Substring(0, LocalDownloadFile.Length - 3) + "xml";
                        HttpCommands.DownloadHTTPFileAndExtract(repositoryUrl, RepositoryFolder, false, null);


                        File.Move(RepositoryFolder + LocalDownloadFile, RepositoryFolder + LocalXmlFile);
                        File.SetLastWriteTime(RepositoryFolder + LocalXmlFile, HttpCommands.GetHTTPFileLastModified(repositoryUrl));
                    }
                    catch (Exception ex)
                    {
                        if (!string.IsNullOrEmpty(LocalDownloadFile))
                        {
                            File.Delete(RepositoryFolder + LocalDownloadFile);
                        }
                        throw ex;
                    }

                    
                }


                //this is a valid repositoryFile!
                RepositoryFiles.Add(RepositoryFolder + LocalXmlFile);


                //now recersively parse distributes repositories
                XmlDocument doc = new XmlDocument();
                doc.Load(RepositoryFolder + LocalXmlFile);

                XmlNodeList Names = doc.SelectNodes("/Repository/DistributedRepositories/Repository/Name");
                XmlNodeList URLs = doc.SelectNodes("/Repository/DistributedRepositories/Repository/URL");
                FoundNumberOfRepositories += Names.Count;

                if (OnUpdate != null)
                {
                    OnUpdate((int)((100 / (float)FoundNumberOfRepositories) * (float)CurrentUpdatingIndex), repositoryName);
                }

                for (int i = 0; i < Names.Count; i++)
                {
                    string name = Names[i].InnerText.Trim();
                    string url = URLs[i].InnerText.Trim();

                    if ((name != repositoryName) && (url != repositoryUrl))
                    {
                        DownloadRepositoryFiles(name, url);
                    }
                }                
            }
            catch (Exception ex)
            {
                Logger.GetInstance().AddLogLine(LogLevel.Error, "Can't connect to "+repositoryName+" repository server", ex);
                MessageBox.Show(string.Format(Resources.MessageBox_RepsitoryUpdateCantConnect, repositoryName), Resources.MessageBox_RepositoryUpdate, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateRepository()
        {
            if (!Directory.Exists(RepositoryFolder))
            {
                Directory.CreateDirectory(RepositoryFolder);
            }
            RepositoryFiles = new List<string>();

            //remove old folder
            if (Directory.Exists(RepositoryFolder + "previous"))
            {
                Directory.Delete(RepositoryFolder + "previous", true);
            }
            Directory.CreateDirectory(RepositoryFolder + "previous");
            foreach (string filename in Directory.GetFiles(RepositoryFolder, "*.xml"))
            {
                File.Copy(filename, Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar + "previous" + Path.DirectorySeparatorChar + Path.GetFileName(filename));
            }

            CurrentUpdatingIndex = 0;
            Dictionary<string, string> MainRepositories = new RepositoryLocations().GetEnabledMainRepositories();
            FoundNumberOfRepositories = MainRepositories.Count;

            foreach (KeyValuePair<string, string> kv in MainRepositories)
            {
                DownloadRepositoryFiles(kv.Key, kv.Value);
            }

            CleanupOldRepositoryFiles();
            LoadRepositoryFiles();

            UpdateNewState();

            Directory.Delete(RepositoryFolder + "previous", true);
            OnUpdate(100, "Done");
        }

        private void UpdateNewState()
        {
            List<RepositoryApplicationInfo> prevApps = new List<RepositoryApplicationInfo>();
            List<RepositoryCustomMenuInfo> prevMenus = new List<RepositoryCustomMenuInfo>();
            List<RepositoryThemeInfo> prevThemes = new List<RepositoryThemeInfo>();
            List<RepositoryWaitImagesInfo> prevWaitImages = new List<RepositoryWaitImagesInfo>();
            List<RepositoryWebserviceInfo> prevWebservices = new List<RepositoryWebserviceInfo>();

            foreach (string repositoryFile in Directory.GetFiles(RepositoryFolder + "previous" + Path.DirectorySeparatorChar, "*.xml"))
            {
                LoadRepositoryFile(repositoryFile, prevApps, prevMenus, prevThemes, prevWebservices, prevWaitImages);
            }

            //now compare every item!
            foreach (RepositoryApplicationInfo current in _applications)
            {
                int i = 0;
                while ((i < prevApps.Count) && ((current.Name != prevApps[i].Name) || (current.Version != prevApps[i].Version)))
                    i++;

                if (i >= prevApps.Count)
                {
                    current.IsNew = true;
                }
            }
            foreach (RepositoryThemeInfo current in _themes)
            {
                int i = 0;
                while ((i < prevThemes.Count) && ((current.Name != prevThemes[i].Name) || (current.Version != prevThemes[i].Version)))
                    i++;

                if (i >= prevThemes.Count)
                {
                    current.IsNew = true;
                }
            }
            foreach (RepositoryWaitImagesInfo current in _waitImagesSets)
            {
                int i = 0;
                while ((i < prevWaitImages.Count) && ((current.Name != prevWaitImages[i].Name) || (current.Version != prevWaitImages[i].Version)))
                    i++;

                if (i >= prevWaitImages.Count)
                {
                    current.IsNew = true;
                }
            }
            foreach (RepositoryWebserviceInfo current in _webservices)
            {
                int i = 0;
                while ((i < prevWebservices.Count) && ((current.Name != prevWebservices[i].Name) || (current.Version != prevWebservices[i].Version)))
                    i++;

                if (i >= prevWebservices.Count)
                {
                    current.IsNew = true;
                }
            }
            foreach (RepositoryCustomMenuInfo current in _indexes)
            {
                int i = 0;
                while ((i < prevMenus.Count) && ((current.Name != prevMenus[i].Name) || (current.Version != prevMenus[i].Version)))
                    i++;

                if (i >= prevMenus.Count)
                {
                    current.IsNew = true;
                }
            }
        }

        private void CleanupOldRepositoryFiles()
        {
            foreach (string file in Directory.GetFiles(RepositoryFolder, "*_*.xml"))
            {
                if (!RepositoryFiles.Contains(file))
                {
                    File.Delete(file);
                }
            }
        }

        public Repository(UpdateRepositoryState updateDelegate)
        {
            SelectMainRepository();
            OnUpdate = updateDelegate;
        }
    }
}
