using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlChangeLanguage : BasePanel
    {
        private RepositoryLocations _repositoryLocations = new RepositoryLocations();

        public pnlChangeLanguage(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_ChangeLanguage, panelResults, progress)
        {
            InitializeComponent();
        }

        public override bool Execute()
        {
            if (lvSelectLanguage.SelectedItems.Count > 0)
            {
                Settings.Default.UILanguage = lvSelectLanguage.SelectedItems[0].Text;
                Settings.Default.Save();

                EnableMainRepositorie(Settings.Default.UILanguage);

                this.Hide();
                Application.Restart();
               
                return false;
            }
            else
            {
                return false;
            }
        }

        private void EnableMainRepositorie(string Language)
        {
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
                foreach (RepositoryLocationInfo info in _repositoryLocations.MainRepositories)
                {
                    if (info.Name.StartsWith("Main_Type" + det.RepositoryType + "_" + Language, StringComparison.CurrentCultureIgnoreCase))
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
    }
}
