using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using com.nmtinstaller.csi.Properties;
using System.Diagnostics;
using com.nmtinstaller.csi.Utilities;
using System.Threading;
using com.nmtinstaller.csi.InstalledApps;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.Forms;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlWelcome : BasePanel
    {
        private Repository repository;

        public pnlWelcome(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_Welcome, panelResults, progress)
        {
            InitializeComponent();
        }

        private void OnRepositoryUpdate(int Procent, string name)
        {
            //if (this.InvokeRequired)
            //{
            //    this.Invoke(new MethodInvoker(delegate { OnRepositoryUpdate(Procent, name); }));
            //    return;
            //}

            if (!this.Created)
            {
                return;
            }

            this.Invoke(new MethodInvoker(delegate
            {
                if (pbRepositoryUpdate.Style == ProgressBarStyle.Marquee)
                {
                    pbRepositoryUpdate.Style = ProgressBarStyle.Continuous;
                }

                name = name.ToLower();
                name = name.Substring(0, 1).ToUpper() + name.Substring(1);
                lblUpdatingRepositoryName.Text = Resources.Text_CheckingRepository+": " + name;
                pbRepositoryUpdate.Value = Procent;

                if (Procent == 100)
                {
                    gbxUpdateRepository.Visible = false;
                    gbxRepositoryInfo.Visible = (!string.IsNullOrEmpty(Settings.Default.HardwareType));
                    UpdateRepositoryUIInfo();

                    //not in execute because when users directly go from
                    //welcome to server settings might skip execute.
                    panelResults.Remove("Repository");
                    panelResults.Add("Repository", repository);
                }
            }));
        }


        public override bool Execute()
        {
            return gbxRepositoryInfo.Visible || string.IsNullOrEmpty(Settings.Default.HardwareType);
        }

        private void pnlWelcome_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (repository == null)
                {
                    repository = new Repository(OnRepositoryUpdate);
                    new Thread(new ThreadStart(repository.UpdateRepository)).Start();
                }

                //if (cbxChooseLanguage.Items.IndexOf(Settings.Default.UILanguage) > -1)
                //{
                //    cbxChooseLanguage.Tag = 1;
                //    cbxChooseLanguage.SelectedIndex = cbxChooseLanguage.Items.IndexOf(Settings.Default.UILanguage);
                //    cbxChooseLanguage.Tag = null;
                //}
                //else
                //{
                //    cbxChooseLanguage.SelectedIndex = 0;
                //}
            }
        }

        private void UpdateRepositoryUIInfo()
        {
            tbNumberApplications.Text = repository.Applications.Count.ToString();
            tbNumberThemes.Text = repository.Themes.Count.ToString();
            tbNumberCustomMenus.Text = repository.CustomMenus.Count.ToString();
            tbNumberWaitImageSets.Text = repository.WaitImages.Count.ToString();
            tbNumberWebservices.Text = repository.Webservices.Count.ToString();

            if (repository.RepositoryDate > DateTime.MinValue)
            {
                tbRepositoryDate.Text = repository.RepositoryDate.ToString();
            }
            else
            {
                tbRepositoryDate.Text = "";
            }
        }


        private void picDonate_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.URL_Donate);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tbNumberApplications_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblWebservices_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbxChooseLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (cbxChooseLanguage.Tag == null)
            //{
            //    Settings.Default.UILanguage = cbxChooseLanguage.Text;
            //    Settings.Default.Save();

            //    Application.Restart();
            //}
        }

        private void lblNZBVortex_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.URL_NZBVortex);
        }





    }
}
