using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Reflection;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using com.nmtinstaller.csi.Properties;
using System.Threading;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.Forms;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlUploadApplication : BasePanel
    {
        string InstallResult = string.Empty;
        private List<String> Backgrounds = new List<String>();
        private bool? InstallationSuccessful = null;
        private string InstallationResult = string.Empty;

        public pnlUploadApplication(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_UploadApplication, panelResults, progress)
        {
            InitializeComponent();
        }

        private void OnUploadProgress(int Procent)
        {
            OnProgress(Procent, Resources.Text_Installing+" "+((RepositoryFileInfoBase)panelResults["SelectedItem"]).Name);

            Application.DoEvents();
            Thread.Sleep(0); //switch thread
        }

        private void OnInstallationComplete(bool NoErrors, string InstallResult)
        {
            this.InstallationResult = InstallResult;
            InstallationSuccessful = NoErrors;
        }

        public override bool Execute()
        {
            InstallationSuccessful = null;
            InstallationResult = string.Empty;


            //remove unselected background; if required
            if (panelResults["SelectedItem"] is RepositoryThemeInfo)
            {
                //fix background selection
                if (gbxThemeBackgroundSelect.Visible)
                {
                    SetBackgroundSelection();
                }
            }


            UploadInstaller upli = new UploadInstaller(panelResults["SelectedItem"] as RepositoryFileInfoBase, OnProgress, OnInstallationComplete);
            panelResults.Remove("InstallResult");


            upli.Install();

            while (InstallationSuccessful==null)
            {
                Thread.Sleep(10);
                Application.DoEvents();
            }

            panelResults["InstallResult"] = InstallationResult;

            if ((bool)InstallationSuccessful)
            {
                Directory.Delete(Constants.TemporaryFolder, true);
            }

            return (bool)InstallationSuccessful;
        }



        private void ProcessMultipleBackgrounds(RepositoryThemeInfo SelectedTheme)
        {

            string LocalFolder = Constants.TemporaryFolder;
            if (Directory.GetDirectories(LocalFolder, "_theme_").Length > 0)
            {
                LocalFolder += "_theme_" + Path.DirectorySeparatorChar;
            }

            lbBackgrounds.Items.Clear();
            Backgrounds.Clear();


            List<string> backgrounds = new List<string>();

            string[] folders = Directory.GetDirectories(LocalFolder);
            int i = 0;
            while ((i<folders.Length) && (Path.GetFileName(folders[i]).ToLower()!=panelResults["ThemeFormat"].ToString().ToLower()))
                i++;

            if (i<folders.Length)
            {
                foreach (string file in Directory.GetFiles(folders[i] + Path.DirectorySeparatorChar))
                {
                    if ((Path.GetFileName(file).ToLower().StartsWith("bg")) && Path.GetFileName(file).ToLower().EndsWith(".jpg"))
                    {
                        backgrounds.Add(file);
                    }
                }
            }

            backgrounds.Sort();
            

            if (backgrounds.Count > 1)
            {
                foreach (string item in backgrounds)
                {
                    Backgrounds.Add(item);
                    lbBackgrounds.Items.Add(Resources.Text_Background+" "+Backgrounds.Count.ToString());
                }

                lbBackgrounds.SelectedIndex = 0;
            }

            gbxThemeBackgroundSelect.Visible = lbBackgrounds.Items.Count > 0;
            gbxThemeBackgroundSelect.Enabled = lbBackgrounds.Items.Count > 0;
        }

        private void SetBackgroundSelection()
        {
            gbxThemeBackgroundSelect.Enabled = false;
            foreach (string file in Backgrounds)
            {
                if (file != Backgrounds[lbBackgrounds.SelectedIndex])
                {
                    File.Delete(file);
                }
            }

            File.Move(Backgrounds[lbBackgrounds.SelectedIndex], Path.GetDirectoryName(Backgrounds[lbBackgrounds.SelectedIndex]) + Path.DirectorySeparatorChar+ "bg.jpg");
        }


        private void pnlUploadApplication_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (panelResults != null)
                {
                    RepositoryFileInfoBase SelectedItem = panelResults["SelectedItem"] as RepositoryFileInfoBase;

                    txtApplication.Text = ((RepositoryFileInfoBase)SelectedItem).ToString();
                    txtServer.Text = Settings.Default.Server;
                    gbxThemeBackgroundSelect.Visible = false;

                    if (SelectedItem is RepositoryApplicationInfo)
                    {
                        lblSelectedApplication.Text = Resources.InstallItem_ApplicationString;
                    }
                    else
                        if (SelectedItem is RepositoryThemeInfo)
                        {
                            lblSelectedApplication.Text = Resources.InstallItem_ThemeString;
                            ProcessMultipleBackgrounds(SelectedItem as RepositoryThemeInfo);
                        }
                        else if (SelectedItem is RepositoryWaitImagesInfo)
                        {
                            lblSelectedApplication.Text = Resources.InstallItem_WaitImageSetString;
                        }
                        else if (SelectedItem is RepositoryCustomMenuInfo)
                        {
                            lblSelectedApplication.Text = Resources.InstallItem_IndexString;
                        }
                        else
                        {
                            lblSelectedApplication.Text = Resources.InstallItem_WebserviceString;
                        }
                }
            }
        }

        private void lbBackgrounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictBackgroundPreview.Load(Backgrounds[lbBackgrounds.SelectedIndex]);
        }

        private void lblSelectedApplication_Click(object sender, EventArgs e)
        {

        }
    }
}
