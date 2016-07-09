using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using com.nmtinstaller.csi.Properties;
using System.Diagnostics;
using com.nmtinstaller.csi.Panels;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;
using Com.Tytte.Dk;
using System.Reflection;
using System.Threading;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Forms
{
    

    public partial class frmInstaller : Form
    {
        private BasePanel CurrentPanel;
        private List<BasePanel> Panels = new List<BasePanel>();
        private Dictionary<string, object> panelResults = new Dictionary<string, object>();

        public frmInstaller()
        {
            InitializeComponent();
            FixPanelDimentions();

            BasePanel control;

            control = new pnlWelcome(panelResults, ProgressChanges);
            control.Visible = false;
            control.Parent = pnlContainer;
            control.Dock = DockStyle.Fill;
            Panels.Add(control);

            if (Settings.Default.FirstRun || !Enum.IsDefined(typeof(HardwareTypeEnum), Settings.Default.HardwareType))
            {
                control = new pnlServerSettings(panelResults, ProgressChanges);
                control.Visible = false;
                control.Parent = pnlContainer;
                control.Dock = DockStyle.Fill;
                Panels.Add(control);

                panelResults.Add("FirstRun", true);
            }
            else
            {
                panelResults.Add("FirstRun", false);
            }

            control = new pnlSelectApplication(panelResults, ProgressChanges);
            control.Visible = false;
            control.Parent = pnlContainer;
            control.Dock = DockStyle.Fill;
            Panels.Add(control);

            control = new pnlUploadApplication(panelResults, ProgressChanges);
            control.Visible = false;
            control.Parent = pnlContainer;
            control.Dock = DockStyle.Fill;
            Panels.Add(control);

            control = new pnlCleanup(panelResults, ProgressChanges);
            control.Visible = false;
            control.Parent = pnlContainer;
            control.Dock = DockStyle.Fill;
            Panels.Add(control);

            control = new pnlDone(panelResults, ProgressChanges);
            control.Visible = false;
            control.Parent = pnlContainer;
            control.Dock = DockStyle.Fill;
            Panels.Add(control);

            CSIUpdateAvailable();

            if (Settings.Default.UILanguage == "Select")
            {
                ShowTempPanelAtLocation(0, new pnlChangeLanguage(panelResults, ProgressChanges));
            }

            if (!(CurrentPanel is pnlCSIUpgrade))
            {
                CurrentPanel = Panels[0];
                CurrentPanel.Visible = true;
                UpdateButtonState();
            }

        }

        private bool CSIUpdateAvailable()
        {
            try
            {
                string OnlineVersion = HttpCommands.GetHttpFileContent(Settings.Default.CSIUpdateVersionURL, null);
                panelResults.Remove("OnlineVersion");
                panelResults.Add("OnlineVersion", new Version(OnlineVersion));

                if (new Version(OnlineVersion) != Assembly.GetEntryAssembly().GetName().Version)
                {
                    ShowTempPanelAtLocation(0, new pnlCSIUpgrade(panelResults, ProgressChanges));
                    return true;
                }
            }
            catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Error, "Primary check for new update version failed.", ex); }
            try
            {
                string OnlineVersion = HttpCommands.GetHttpFileContent(Settings.Default.CSIUpdate2VersionURL, null);
                panelResults.Remove("OnlineVersion");
                panelResults.Add("OnlineVersion", new Version(OnlineVersion));

                if (new Version(OnlineVersion) != Assembly.GetEntryAssembly().GetName().Version)
                {
                    ShowTempPanelAtLocation(0, new pnlCSIUpgrade(panelResults, ProgressChanges));
                    return true;
                }
            }
            catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Error, "Can't check for new update version", ex); }
            return false;
        }

        private void ProgressChanges(int progress, string Description)
        {

            //if (this.InvokeRequired)
            //{
            //    pbProgress.Invoke(new MethodInvoker(delegate { ProgressChanges(progress, Description); }));
            //    return;
            //}

            if (!this.Created)
            {
                return;
            }

            if ((!this.Disposing) && (!this.IsDisposed))
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    if (progress == -1)
                    {
                        this.Text = this.Text.Split('-')[0].Trim() + " - " + Description;
                        pbProgress.Visible = true;
                        if (pbProgress.Style == ProgressBarStyle.Continuous)
                        {
                            pbProgress.Style = ProgressBarStyle.Marquee;
                        }

                        btnNext.Enabled = btnPrev.Enabled = false;
                        return;
                    }

                    if (pbProgress.Style == ProgressBarStyle.Marquee)
                    {
                        pbProgress.Style = ProgressBarStyle.Continuous;
                    }

                    if (progress == 100)
                    {
                        this.Text = this.Text.Split('-')[0].Trim();
                        pbProgress.Visible = false;
                        pbProgress.Value = 0;

                        UpdateButtonState();
                    }
                    else
                    {
                        this.Text = this.Text.Split('-')[0].Trim() + " - " + Description;
                        pbProgress.Value = progress;
                        pbProgress.Visible = true;

                        btnNext.Enabled = btnPrev.Enabled = false;
                    }
                }));
            }

        }

        private void FixPanelDimentions()
        {
            pnlContainer.Left = -1;
            pnlContainer.Width = this.ClientSize.Width + 2;
            pnlContainer.Height = this.ClientSize.Height - (pnlButtonBackground.Height + menuStrip1.Height) + 2;

            pnlButtonBackground.Top = this.ClientSize.Height - (pnlButtonBackground.Height - 1);
            pnlButtonBackground.Width = this.ClientSize.Width + 2;
        }

        private void PrevStep()
        {
            PrevStep(1);
        }

        private void PrevStep(int skip)
        {
            int index = Panels.IndexOf(CurrentPanel);
            if (index >= skip)
            {
                CurrentPanel.Visible = false;
                CurrentPanel = Panels[index - skip];
            }

            RemoveTemporaryPanels();
            CurrentPanel.Visible = true;
            UpdateButtonState();
        }

        private void UpdateButtonState()
        {
            btnPrev.Enabled = ((Panels.IndexOf(CurrentPanel) > 0) && (Panels.IndexOf(CurrentPanel) < Panels.Count - 1));
            btnNext.Enabled = true;
            btnExit.Enabled = true;

            if (Panels.IndexOf(CurrentPanel) == (Panels.Count - 1))
            {
                btnNext.Text = Resources.Text_MainButtonsMore;
            }
            else
            {
                btnNext.Text = Resources.Text_MainButtonsNext;
            }

            btnNext.Focus();
        }

        private void NextStep()
        {
            NextStep(1);
        }

        private void NextStep(int skip)
        {
            if (btnNext.Text == Resources.Text_MainButtonsExit)
            {
                this.Close();
                return;
            }

            int index = Panels.IndexOf(CurrentPanel);
            if (index < (Panels.Count - skip))
            {
                CurrentPanel.Visible = false;
                CurrentPanel = Panels[index + skip];
            }

            RemoveTemporaryPanels();
            CurrentPanel.Visible = true;
            UpdateButtonState();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {

        }


        private void btnPrev_Click(object sender, EventArgs e)
        {
            if ((CurrentPanel is pnlDone) && (panelResults["SelectedItem"] is RepositoryThemeInfo))
            {
                PrevStep(2);
            }
            else
            {
                PrevStep(1);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (btnNext.Text == Resources.Text_MainButtonsMore)
            {
                //search for select software pannel
                int i = 0;
                while ((i < Panels.Count) && (!(Panels[i] is pnlSelectApplication)))
                    i++;

                CurrentPanel.Visible = false;
                CurrentPanel = Panels[i];
                CurrentPanel.Visible = true;

                UpdateButtonState();
            }
            else
            {
                btnNext.Enabled = false;
                btnPrev.Enabled = false;
                btnExit.Enabled = false;

                Application.DoEvents();


                if (((BasePanel)CurrentPanel).Execute())
                {
                    if ((CurrentPanel is pnlUploadApplication) && ((panelResults["SelectedItem"] is RepositoryThemeInfo) || (panelResults["SelectedItem"] is RepositoryWaitImagesInfo) || (panelResults["SelectedItem"] is RepositoryCustomMenuInfo)))
                    {
                        NextStep(2);
                    }
                    else
                    {
                        NextStep();
                    }
                }
                else
                {
                    UpdateButtonState();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void removeThemeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.MessageBox_ThemeDeleteConfirm, Resources.MessageBox_ThemeDeleteCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FtpCommands.DeleteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "/Photo/_theme_/.config");
                    FtpCommands.DeleteFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "/Photo/_theme_/");
                    MessageBox.Show(Resources.MessageBox_ThemeDeleted, Resources.MessageBox_ThemeDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Theme cleanup failed", ex);
                    MessageBox.Show(Resources.MessageBox_ThemeDeleteFailed, Resources.MessageBox_ThemeDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void fTPSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTempPanelAtLocation(1, new pnlServerSettings(panelResults, ProgressChanges));
        }


        private void advancedsettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.MessageBox_AdvancedSettingsWarning, Resources.MessageBox_AdvancedSettingsCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                    ShowTempPanelAtLocation(1, new pnlAdvSettings(panelResults, ProgressChanges));
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if ((t != null) && (!t.ReadOnly))
            {
                t.Undo();
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if ((rt != null) && (!rt.ReadOnly))
            {
                rt.Undo();
            }
        }


        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if ((t != null) && (t.SelectionLength > 0))
            {
                if (t.ReadOnly)
                {
                    t.Copy();
                }
                else
                {
                    t.Cut();
                }
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if ((rt != null) && (rt.SelectionLength > 0))
            {
                if (rt.ReadOnly)
                {
                    rt.Copy();
                }
                else
                {
                    rt.Cut();
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if ((t != null) && (t.SelectionLength > 0))
            {
                t.Copy();
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if ((rt != null) && (rt.SelectionLength > 0))
            {
                rt.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if ((t != null) && (!t.ReadOnly))
            {
                t.Paste();
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if ((rt != null) && (!rt.ReadOnly))
            {
                rt.Paste();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {

            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if (t != null)
            {
                t.SelectionStart = 0;
                t.SelectionLength = t.Text.Length;
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if (rt != null)
            {
                rt.SelectionStart = 0;
                rt.SelectionLength = rt.Text.Length;
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox t = CurrentPanel.ActiveControl as TextBox;
            if ((t != null) && (!t.ReadOnly))
            {
                t.Text = string.Empty;
            }

            RichTextBox rt = CurrentPanel.ActiveControl as RichTextBox;
            if ((rt != null) && (!rt.ReadOnly))
            {
                rt.Text = string.Empty;
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.URL_Help);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmAbout().ShowDialog();
        }

        private void removeCustomMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.MessageBox_CustomMenuDeleteConfirm, Resources.MessageBox_CustomMenuDeleteCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FtpCommands.DeleteFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "/Photo/_index_/");
                    FtpCommands.DeleteFile(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "/index.htm");
                    MessageBox.Show(Resources.MessageBox_CustomMenuDeleted, Resources.MessageBox_CustomMenuDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Custom menu cleanup failed", ex);
                    MessageBox.Show(Resources.MessageBox_CustomMenuDeleteFailed, Resources.MessageBox_CustomMenuDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void RemoveTemporaryPanels()
        {
            int i = 0;
            while (i < Panels.Count)
            {
                if ((Panels[i] is pnlRepositories) || 
                    (Panels[i] is pnlInstallFromZIP) || 
                    (Panels[i] is pnlAddWebservice) || 
                    (Panels[i] is pnlChangeLanguage) || 
                    (Panels[i] is pnlCSIUpgrade) ||
                    ((Panels[i] is pnlAdvSettings) && (!Settings.Default.FirstRun)) ||
                    ((Panels[i] is pnlServerSettings) && (!Settings.Default.FirstRun)))
                {    
                    Panels.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }


        private void installFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            while ((i < Panels.Count) && (!(Panels[i] is pnlUploadApplication)))
            {
                i++;
            }

            if (i < Panels.Count)
            {
                ShowTempPanelAtLocation(i,new pnlInstallFromZIP(panelResults, ProgressChanges));
            }
        }

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            installFromFileToolStripMenuItem.Enabled = !Settings.Default.FirstRun;
        }

        private void removeWaitImageSetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.MessageBox_WaitImageDeleteConfirm, Resources.MessageBox_WaitImageDeleteCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    FtpCommands.DeleteFolder(Settings.Default.Server, Settings.Default.Username, Settings.Default.Password, "/Photo/_waitimages_/");
                    MessageBox.Show(Resources.MessageBox_WaitImageDeleted, Resources.MessageBox_WaitImageDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Wait Image Set cleanup failed", ex);
                    MessageBox.Show(Resources.MessageBox_WaitImageDeleteFailed, Resources.MessageBox_WaitImageDeleteCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.URL_ReportBug);
        }

        private void homepageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(Resources.URL_Homepage);
        }

        private void extraToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            uninstallToolStripMenuItem.Enabled = !Settings.Default.FirstRun;
            advancedToolStripMenuItem.Enabled = !Settings.Default.FirstRun;
            addManualWebserviceToolStripMenuItem.Enabled = !Settings.Default.FirstRun;
        }

        private void addManualWebserviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;
            while ((i < Panels.Count) && (!(Panels[i] is pnlUploadApplication)))
            {
                i++;
            }

            if (i < Panels.Count)
            {
                ShowTempPanelAtLocation(i, new pnlAddWebservice(panelResults, ProgressChanges));
            }
        }

        private void cleanupNMTToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            string Warning = "";
            string Command = Settings.Default.CommandsScriptURL.Substring(Settings.Default.CommandsScriptURL.LastIndexOf('/')+1);
            Command = Command.Substring(0, Command.Length - 3) + "cgi";
            string WarningTitle = "";

            if (sender == shutdownNMTToolStripMenuItem)
            {
                Warning = Resources.MessageBox_HaltWarning;
                WarningTitle = Resources.MessageBox_HaltWarningCaption;
                Command += "?halt";
            }
            else
            if (sender == cleanupNMTDeepToolStripMenuItem)
            {
                Warning = Resources.MessageBox_DeepCleanWarning;
                WarningTitle = Resources.MessageBox_DeepCleanCaption;
                Command += "?deepclean";
            }
            else
            if (sender == cleanupNMTToolStripMenuItem)
            {
                Warning = Resources.MessageBox_CleanWarning;
                WarningTitle = Resources.MessageBox_CleanCaption;
                Command += "?clean";
            }
            else
            {
                Warning += Resources.MessageBox_RestartNMTConfirm;
                WarningTitle = Resources.MessageBox_RestartNMTCaption;
                Command += "?reboot";

            }

            if (MessageBox.Show(Warning, WarningTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {

                try
                {
                    btnPrev.Enabled = btnNext.Enabled = btnExit.Enabled = menuStrip1.Enabled = false;
                    Application.DoEvents();
                    string LocalFolder = HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.CommandsScriptURL, Constants.TemporaryFolder, true, null);

                    RepositoryApplicationInfo app = new RepositoryApplicationInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new string[0], Command, string.Empty, string.Empty);
                    UploadInstaller ulinst = new UploadInstaller(app, ProgressChanges, OnInstallComplete);
                    ulinst.Install();
                }
                catch (Exception ex)
                {
                    menuStrip1.Enabled = true;
                    UpdateButtonState();
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Cleanup or restart of NMT failed", ex);
                    MessageBox.Show(Resources.MessageBox_CleanupActionFailed, Resources.MessageBox_CleanupActionCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnInstallComplete(bool NoErrors, string InstallResult)
        {
            
            //if (this.InvokeRequired)
            //{
            //    menuStrip1.Invoke(new MethodInvoker(delegate { OnInstallComplete(NoErrors, InstallResult); }));
            //    return;
            //}

            this.Invoke(new MethodInvoker(delegate
            {

                if (Directory.Exists(Constants.TemporaryFolder))
                {
                    Directory.Delete(Constants.TemporaryFolder, true);
                }

                menuStrip1.Enabled = true;
                UpdateButtonState();
                pbProgress.Visible = false;
                MessageBox.Show(Resources.MessageBox_CleanupActionSuccess, Resources.MessageBox_CleanupActionCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }));
        }


        private void installFirmwareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.MessageBox_InstallFirmwareWarning, Resources.MessageBox_InstallFirmwareCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OpenFileDialog odf = new OpenFileDialog();
                odf.Title = Resources.FileDialog_SelectFirmwareFileCaption;
                odf.Filter = Resources.FileDialog_SelectFirmwareFileFilter;
                if (odf.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string LocalFolder = Constants.TemporaryFolder;
                        if (Directory.Exists(LocalFolder))
                        {
                            Directory.Delete(LocalFolder, true);
                        }
                        Directory.CreateDirectory(LocalFolder);

                        File.Copy(odf.FileName, LocalFolder + "firmware.zip");
                        SimpleUnZipper.UnZipTo(LocalFolder + "firmware.zip", LocalFolder);
                
                        File.Delete(LocalFolder + "firmware.zip");

                        if ((Directory.GetFiles(LocalFolder).Length == 0) && (Directory.GetDirectories(LocalFolder).Length == 1))
                        {
                            LocalFolder = Directory.GetDirectories(LocalFolder)[0] + Path.DirectorySeparatorChar;
                        }
                        HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.CommandsScriptURL, LocalFolder, false, ProgressChanges);

                        btnPrev.Enabled = btnNext.Enabled = btnExit.Enabled = menuStrip1.Enabled = false;
                        Application.DoEvents();
                        RepositoryApplicationInfo app = new RepositoryApplicationInfo(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new string[0], "commands.cgi?firmwareinstall", string.Empty, string.Empty);
                        UploadInstaller ulinst = new UploadInstaller(app, ProgressChanges, OnInstallComplete);
                        ulinst.Install();
                    }
                    catch (Exception ex)
                    {
                        menuStrip1.Enabled = true;
                        UpdateButtonState();
                        Logger.GetInstance().AddLogLine(LogLevel.Error, "Installing firmware failed", ex);
                        MessageBox.Show(Resources.MessageBox_InstallFirmwareFailed, Resources.MessageBox_InstallFirmwareCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void frmInstaller_FormClosing(object sender, FormClosingEventArgs e)
        {
            //always try to clear the temp folder on exit
            if (Directory.Exists(Constants.TemporaryFolder))
            {
                Directory.Delete(Constants.TemporaryFolder, true);
            }
        }

        private void checkForUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CSIUpdateAvailable())
            {
                MessageBox.Show(Resources.MessageBox_CsiUpdateNoneAvailable, Resources.MessageBox_CsiUpdatesCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ShowTempPanelAtLocation(int i, BasePanel panel)
        {
            RemoveTemporaryPanels();
            if (Panels.IndexOf(panel) == -1)
            {
                Panels.Insert(i, panel);

                if (CurrentPanel != null)
                {
                    CurrentPanel.Visible = false;
                }
                
                CurrentPanel = Panels[i];
                CurrentPanel.Parent = pnlContainer;
                CurrentPanel.Dock = DockStyle.Fill;
                CurrentPanel.Visible = false;
                CurrentPanel.Visible = true;
                UpdateButtonState();
            }
            else
            {
                if (CurrentPanel!=Panels[Panels.IndexOf(panel)])
                {
                    if (CurrentPanel != null)
                    {
                        CurrentPanel.Visible = false;
                    }

                    CurrentPanel = Panels[Panels.IndexOf(panel)];
                    CurrentPanel.Parent = pnlContainer;
                    CurrentPanel.Dock = DockStyle.Fill;
                    CurrentPanel.Visible = true;
                    UpdateButtonState();
                }
            }
        }

        private void changeLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTempPanelAtLocation(1, new pnlChangeLanguage(panelResults, ProgressChanges));
        }

        private void mainRepositoriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowTempPanelAtLocation(1, new pnlRepositories(panelResults, ProgressChanges));
        }

    }
}
