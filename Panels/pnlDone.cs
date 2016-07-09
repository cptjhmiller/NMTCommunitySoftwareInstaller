using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlDone : BasePanel
    {
        public pnlDone(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_Done, panelResults, progress)
        {
            InitializeComponent();
        }


        public override bool Execute()
        {
            return true;
        }

        private void lblDone_VisibleChanged(object sender, EventArgs e)
        {
        }

        private void gbxApplicationInstructions_Enter(object sender, EventArgs e)
        {

        }

        private void pnlDone_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType)); 
                gbxWebbrowserUI.Visible = gbxUsageInstructions.Visible = gbxGayaInterface.Visible = false;


                if (((RepositoryFileInfoBase)panelResults["SelectedItem"]).UsageInstructions.Length > 0)
                {
                    tbUsageInstructions.Text = ((RepositoryFileInfoBase)panelResults["SelectedItem"]).UsageInstructions;
                    gbxUsageInstructions.Visible = true;
                }
     

                if (panelResults["SelectedItem"] is RepositoryApplicationInfo)
                {
                    RepositoryApplicationInfo app = panelResults["SelectedItem"] as RepositoryApplicationInfo;

                    if (!string.IsNullOrEmpty(app.WebInterfaceURL))
                    {
                        gbxWebbrowserUI.Visible = true;

                        linkWebInterfaceURL.Text = app.WebInterfaceURL;
                        linkWebInterfaceURL.Links.Clear();
                        linkWebInterfaceURL.Links.Add(0, linkWebInterfaceURL.Text.Length, app.WebInterfaceURL);
                    }


                    if (!string.IsNullOrEmpty(app.GayaInterfaceURL))
                    {
                        gbxGayaInterface.Visible = true;

                        lblNewWebserviceName.Text = app.Name;
                    }

                }


                if (panelResults["SelectedItem"] is RepositoryApplicationInfo)
                {
                    lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_Application.Replace("\\n", "\n");
                }

                if (panelResults["SelectedItem"] is RepositoryThemeInfo)
                {
                    lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_Theme.Replace("\\n", "\n");
                }

                if (panelResults["SelectedItem"] is RepositoryCustomMenuInfo)
                {
                    if (det.RepositoryType == "AB")
                    {
                        lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_CustomMenu.Replace("\\n", "\n");
                    }

                    if (det.RepositoryType == "A4")
                    {
                        string Warning = "";
                        string Command = Settings.Default.CommandsScriptURL.Substring(Settings.Default.CommandsScriptURL.LastIndexOf('/') + 1);
                        Command = Command.Substring(0, Command.Length - 3) + "cgi";
                        string WarningTitle = "";
                        Warning += Resources.MessageBox_RestartNMTConfirm;
                        WarningTitle = Resources.MessageBox_RestartNMTCaption;
                        Command += "?reboot";
                        lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_CustomMenuA4.Replace("\\n", "\n");

                    }
                }

                if (panelResults["SelectedItem"] is RepositoryWaitImagesInfo)
                {
                    lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_WaitImageSet.Replace("\\n", "\n");
                }

                if (panelResults["SelectedItem"] is RepositoryWebserviceInfo)
                {
                    lblSelectionSpecificInstructions.Text = Resources.DoneInstalling_Webservice.Replace("\\n", "\n");
                }
                
            }
        }

        private void linkWebInterfaceURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void tbUsageInstructions_TextChanged(object sender, EventArgs e)
        {

        }


    }
}
