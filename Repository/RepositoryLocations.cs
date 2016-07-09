using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using com.nmtinstaller.csi.Utilities;
using System;

namespace com.nmtinstaller.csi.RemoteFileInfo
{
    public class RepositoryLocations
    {
        private List<RepositoryLocationInfo> _locations;


        public void RemoveMainRepository(string Name)
        {
            int i = 0;
            while (i<_locations.Count)
            {
                if (_locations[i].Name == Name)
                {
                    _locations.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }


        public List<RepositoryLocationInfo> GetInvalidLocations()
        {
            List<RepositoryLocationInfo> result = new List<RepositoryLocationInfo>();
            foreach (RepositoryLocationInfo loc in _locations)
            {
                if (HttpCommands.GetHTTPFileLastModified(loc.URL) == DateTime.MinValue)
                {
                    result.Add(loc);
                }
            }

            return result;
        }


        public List<RepositoryLocationInfo> GetMainRepositories()
        {
            List<RepositoryLocationInfo> result = new List<RepositoryLocationInfo>();
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Repositories.xml");

            Dictionary<string, string> ResultUrls = new Dictionary<string, string>();
            foreach (XmlNode node in doc.SelectNodes("Repositories/Repository"))
            {
                result.Add(new RepositoryLocationInfo(node.SelectSingleNode("Name").InnerText.Trim(), node.SelectSingleNode("URL").InnerText.Trim(), node.SelectSingleNode("Enabled").InnerText.Trim() == "1"));
            }

            return result;
        }

        public void SaveRepository()
        {
            List<RepositoryLocationInfo> result = new List<RepositoryLocationInfo>();
            XmlDocument doc = new XmlDocument();

            XmlElement root = doc.CreateElement("Repositories");
            doc.AppendChild(root);

            foreach(RepositoryLocationInfo mi in _locations)
            {
                XmlElement elem = doc.CreateElement("Repository");
                root.AppendChild(elem);

                XmlElement name = doc.CreateElement("Name");
                XmlElement url = doc.CreateElement("URL");
                XmlElement enabled = doc.CreateElement("Enabled");
                elem.AppendChild(name);
                elem.AppendChild(url);
                elem.AppendChild(enabled);

                name.InnerText = mi.Name;
                url.InnerText = mi.URL;
                enabled.InnerText = mi.Enabled ? "1" : "0";
            }

            doc.Save(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "Repositories.xml");
        }

        public Dictionary<string, string> GetEnabledMainRepositories()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (RepositoryLocationInfo info in _locations)
            {
                if (info.Enabled)
                {
                    result.Add(info.Name, info.URL);
                }
            }

            return result;
        }

        public List<RepositoryLocationInfo> MainRepositories
        {
            get { return _locations; }
            set { _locations = value; }
        }

        public RepositoryLocations()
        {
            _locations = GetMainRepositories();
        }
    }
}
        