using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.RemoteFileInfo;
using System.Threading;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlServerSettings : BasePanel
    {
        public pnlServerSettings(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_ServerSettings, panelResults, progress)
        {
            InitializeComponent();
        }

        

        public override bool Execute()
        {
            if (cbxHardwareType.SelectedIndex == 0)
            {
                errorProvider.Clear();
                errorProvider.SetError(cbxHardwareType, cbxHardwareType.Items[0].ToString());
                return false;
            }

            if (FtpCommands.TestFTPserver(txtServerName.Text, txtUserName.Text, txtPassword.Text))
            {
                HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)cbxHardwareType.SelectedIndex);
                Settings.Default.Server = txtServerName.Text;
                Settings.Default.Username = txtUserName.Text;
                Settings.Default.Password = txtPassword.Text;
                Settings.Default.FTPSubFolder = det.DefaultFTPSubFolder;
                Settings.Default.WebserverSubFolder = det.DefaultWebserverSubFolder;
                
                string prevHardwareType = Settings.Default.HardwareType;
                Settings.Default.HardwareType = ((HardwareTypeEnum)cbxHardwareType.SelectedIndex).ToString();
                Settings.Default.HardwareName = cbxHardwareType.Items[cbxHardwareType.SelectedIndex].ToString();

                Settings.Default.FirstRun = false;
                Settings.Default.Save();
                if (prevHardwareType != Settings.Default.HardwareType)
                {
                    //reload the repository to fix the magic tokens
                    Repository newRep = new Repository(UpdateRepositoryState);

                    panelResults["Repository"] = newRep;

                    Thread t = new Thread(new ThreadStart(newRep.UpdateRepository));
                    t.Start();

                    while (t.ThreadState != ThreadState.Stopped)
                    {
                        Application.DoEvents();
                        Thread.Sleep(50);
                    }
                }
                else
                {
                    ((Repository)panelResults["Repository"]).LoadRepositoryFiles();
                }

                //this will trigger an appinfo update
                panelResults["SelectedItem"] = 1;

                return true;
            }
            else
            {
                Logger.GetInstance().AddLogLine(LogLevel.Warning, "FTP test failed. Error while trying to connect to the FTP server.");
                MessageBox.Show(Resources.MessageBox_FtpConnectionTestFailed, Resources.MessageBox_FtpConnectionTestCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void UpdateRepositoryState(int Procent, string RepositoryName)
        {
            OnProgress(Procent, RepositoryName);
        }


        private void pnlServerSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                cbxHardwareType.SelectedIndex = 0;

                int i = 0;
                while ((i < cbxHardwareType.Items.Count) && (cbxHardwareType.Items[i].ToString() != Settings.Default.HardwareName))
                {
                    i++;
                }

                if (i < cbxHardwareType.Items.Count)
                {
                    cbxHardwareType.SelectedIndex = i;
                }

                txtServerName.Text = Settings.Default.Server;
                txtUserName.Text = Settings.Default.Username;
                txtPassword.Text = Settings.Default.Password;

            }
        }

        private void cbxHardwareType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxHardwareType.SelectedIndex > 0)
            {
                HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)cbxHardwareType.SelectedIndex);

                txtServerName.Text = det.DefaultServerName;
                txtUserName.Text = det.DefaultFtpUsername;
                txtPassword.Text = det.DefaultFtpPassword;
            }
        }

    }
}
