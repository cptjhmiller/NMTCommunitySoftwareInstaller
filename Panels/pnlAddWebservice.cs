using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TransmissionApplicationUploader;
using System.IO;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.Forms;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlAddWebservice : BasePanel
    {
        public override bool Execute()
        {
            errorProvider.Clear();
            if (!Regex.Match(tbName.Text, @"^[0-9A-Za-z\s]+$").Success)
            {
                errorProvider.SetError(tbName, Resources.Validate_InvalidWebserviceName);
                return false;
            }

            if (!Regex.Match(tbURL.Text, @"^http://(?<www>.*?\.)?(?<name>.*)\.(?<tld>.{2,})").Success)
            {
                errorProvider.SetError(tbURL, Resources.Validate_InvalidWebserviceURL);
                return false;
            }

            string LocalFolder =  HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.InstallPrepareScriptURL, Constants.TemporaryFolder, true, null);

            panelResults.Remove("SelectedItem");
            panelResults.Add("SelectedItem", new RepositoryWebserviceInfo(tbName.Text, "", "", "", "", "", "", "", "", "", new string[0], tbURL.Text));
            return true;
        }

        public pnlAddWebservice(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_Webservice, panelResults, progress)
        {
            InitializeComponent();
        }

        private void pnlAddWebservice_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                panelResults.Remove("SelectedItem");
            }
        }



    }
}
