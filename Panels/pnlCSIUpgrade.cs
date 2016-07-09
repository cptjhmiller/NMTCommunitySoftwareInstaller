using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.nmtinstaller.csi.Utilities;
using System.Reflection;
using System.IO;
using System.Diagnostics;
using com.nmtinstaller.csi.Properties;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlCSIUpgrade : BasePanel
    {

        public override bool Execute()
        {
            if (!cbxDoNotUpdate.Checked)
            {
                try
                {
                    //remove update file if still present
                    if (File.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + Settings.Default.CSIUpdateArchiveURL.Substring(Settings.Default.CSIUpdateArchiveURL.LastIndexOf('/') + 1)))
                    {
                        File.Delete(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar + Settings.Default.CSIUpdateArchiveURL.Substring(Settings.Default.CSIUpdateArchiveURL.LastIndexOf('/') + 1));
                    }
                    
                    //moveout all current exe and dll's
                    foreach(string fileName in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.exe", SearchOption.AllDirectories))
                    {
                        File.Move(fileName, fileName + ".old");
                    }
                    foreach (string fileName in Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "*.dll", SearchOption.AllDirectories))
                    {
                        File.Move(fileName, fileName + ".old");
                    }
                    
                    HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.CSIUpdateArchiveURL, Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + Path.DirectorySeparatorChar, false, OnProgress);

                    Process.Start(Assembly.GetEntryAssembly().Location);
                    ((Form)this.Parent.Parent).Close();
                }
                catch (Exception ex) 
                { 
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Exception during update process", ex);
                    MessageBox.Show(Resources.MessageBox_CsiUpdateFailed, Resources.MessageBox_CsiUpdatesCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public pnlCSIUpgrade(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo OnProgress)
            : base(Resources.PanelTitle_CSIUpgrade, panelResults, OnProgress)
        {
            InitializeComponent();
            tbCurrentVersion.Text = Assembly.GetEntryAssembly().GetName().Version.ToString();
            tbAvailableVersion.Text = ((Version)panelResults["OnlineVersion"]).ToString();

            try
            {
                rtbChangelog.Rtf = Functions.HtmlToRtf(HttpCommands.GetHttpFileContent(Settings.Default.CSIUpdateChangelogURL, null), true);
            }
            catch (Exception ex) { Logger.GetInstance().AddLogLine(LogLevel.Error, "Invalid response on changelog request", ex); }
        }

        private void rtbChangelog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void rtbChangelog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(rtbChangelog.SelectedText);
                }
            }
        }
    }
}
