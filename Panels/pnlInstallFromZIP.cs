using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using TransmissionApplicationUploader;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;
using Com.Tytte.Dk;
using com.nmtinstaller.csi.Forms;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlInstallFromZIP : BasePanel
    {
        private RepositoryFileInfoBase SelectedItem = null;
        private string LocalFolder = null;

        public override bool Execute()
        {

            if (gbxApplication.Visible)
            {
                SelectedItem = new RepositoryApplicationInfo(Resources.InstallItem_ApplicationString, "", "", "", "", "", "", "", "", "", new string[0], cbxInstallScript.Text + tbInstallScriptParameters.Text, "", "");
            }

            if (SelectedItem != null)
            {
                if (SelectedItem is RepositoryThemeInfo)
                {
                    if (cbxThemeFormat.Text == Resources.Text_HighDefinition)
                    {
                        panelResults.Add("ThemeFormat", "HD");
                    }
                    else
                    {
                        panelResults.Add("ThemeFormat", "SD");
                    }
                }

                panelResults.Add("SelectedItem", SelectedItem);
            }


            if ((SelectedItem is RepositoryApplicationInfo) || (SelectedItem is RepositoryWaitImagesInfo))
            {
                HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.InstallPrepareScriptURL, Constants.TemporaryFolder, false, null);
            }

            if (SelectedItem is RepositoryApplicationInfo)
            {
                HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.AppInitScriptURL, Constants.TemporaryFolder, false, null);
            }

            return (SelectedItem != null);
        }

        public pnlInstallFromZIP(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_InstallFromZip, panelResults, progress)
        {
            InitializeComponent();
            LocalFolder = Constants.TemporaryFolder;
        }


        private void AutoDetectContent()
        {
            if ((Directory.GetFiles(LocalFolder).Length == 0) && (Directory.GetDirectories(LocalFolder).Length == 1))
            {
                LocalFolder = Directory.GetDirectories(LocalFolder)[0] + Path.DirectorySeparatorChar;
            }
            

            //first, is it a theme?
            if (Directory.Exists(LocalFolder+"_theme_")
                || (Directory.Exists(LocalFolder+"SD")
                || (Directory.Exists(LocalFolder+"HD"))))
            {
                gbxTheme.Visible = true;
                gbxApplication.Visible = false;

                cbxThemeFormat.Items.Clear();
                lblOpenZipFile.Text = Resources.InstallItem_ThemeString;

                if (Directory.Exists(LocalFolder+"_theme_"))
                {
                    LocalFolder+=Path.DirectorySeparatorChar+"_theme_";
                }

                if (Directory.Exists(LocalFolder+Path.DirectorySeparatorChar+"HD"))
                {
                    cbxThemeFormat.Items.Add(Resources.Text_HighDefinition);
                }

                if (Directory.Exists(LocalFolder+Path.DirectorySeparatorChar+"SD"))
                {
                    cbxThemeFormat.Items.Add(Resources.Text_StandardDefinition);
                }

                SelectedItem = new RepositoryThemeInfo(Resources.InstallItem_ThemeString,"","","", "", "", "","","", "", new string[0], cbxThemeFormat.Items.Contains("High Definition"), cbxThemeFormat.Items.Contains("Standard Definition"));

                cbxThemeFormat.SelectedIndex=0;
            }

            else

            //does it contain an cgi file?
                if ((Directory.GetFiles(LocalFolder, "*.cgi").Length > 0) || (Directory.GetFiles(LocalFolder, "*.tar").Length > 0))
            {
                lblOpenZipFile.Text = Resources.InstallItem_ApplicationString;
                cbxInstallScript.Items.Clear();
                

                string[] files = Directory.GetFiles(LocalFolder, "*.tar");
                foreach (string file in files)
                {
                    cbxInstallScript.Items.Add(Settings.Default.AppInitLocation + "?install&" + Path.GetFileName(file).Replace(" ", "%20"));
                }

                files = Directory.GetFiles(LocalFolder, "*.cgi");
                foreach (string file in files)
                {
                    cbxInstallScript.Items.Add(Path.GetFileName(file));
                }
                cbxInstallScript.SelectedIndex = 0;
                gbxApplication.Visible = true;
                gbxTheme.Visible = false;
            }

            else

            //is it an index?
            if (File.Exists(LocalFolder+"index.htm"))
            {
                lblOpenZipFile.Text = Resources.InstallItem_IndexString;
                SelectedItem = new RepositoryCustomMenuInfo(Resources.InstallItem_IndexString, "","","","", "", "", "", "","", new string[0]);
                gbxTheme.Visible = gbxApplication.Visible = false;
            }

            else

            if (File.Exists(LocalFolder + "demo.jpg"))
            {
                lblOpenZipFile.Text = Resources.InstallItem_WaitImageSetString;
                SelectedItem = new RepositoryWaitImagesInfo(Resources.InstallItem_WaitImageSetString, "","", "","","", "", "", "", "", new string[0]);
            }

            else

            if (Directory.Exists(LocalFolder + ".home"))
            {
                lblOpenZipFile.Text = Resources.InstallItem_IndexString;
                SelectedItem = new RepositoryCustomMenuInfo(Resources.InstallItem_IndexString, "", "", "", "", "", "", "", "", "", new string[0]);
                gbxTheme.Visible = gbxApplication.Visible = false;
            }

            else

            {
                lblOpenZipFile.Text = Resources.InstallItem_Package;
                MessageBox.Show(Resources.MessageBox_InstallFromFileInvalidZIP, Resources.MessageBox_InstallFromFileCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                SelectedItem = null;
            }
        }
        


        private void btnOpenZip_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = Resources.FileDialog_OpenAppZipCaption;
            ofd.Filter = Resources.FileDialog_OpenAppZipFilter;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbZipFile.Text = ofd.FileName;
                if (Directory.Exists(LocalFolder))
                {
                    Directory.Delete(LocalFolder, true);
                }

                Directory.CreateDirectory(LocalFolder);
                File.Copy(ofd.FileName, LocalFolder + Path.GetFileName(ofd.FileName));

                String LocalFile = LocalFolder + Path.GetFileName(ofd.FileName);
                SimpleUnZipper.UnZipTo(LocalFile, LocalFolder);
                
                File.Delete(LocalFile);

                AutoDetectContent();
            }
        }


        private void pnlInstallFromZIP_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                panelResults.Remove("SelectedItem");
                panelResults.Remove("ThemeFormat");
                lblOpenZipFile.Text = Resources.InstallItem_Package;
            }
        }

    }
}
