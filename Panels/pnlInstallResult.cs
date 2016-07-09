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
using com.nmtinstaller.csi.Properties;
using System.Text.RegularExpressions;
using System.Diagnostics;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.Utilities;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlCleanup : BasePanel
    {

        public pnlCleanup(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_InstallResult, panelResults, progress)
        {
            InitializeComponent();
        }


        public override bool Execute()
        {
            return true;
        }


        private void pnlCleanup_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                string InstallResult = panelResults["InstallResult"] as string;

                //remove standard header from HTTP server
                InstallResult = Regex.Replace(InstallResult, "HTTP/1.1 200 OK\nServer: Syabas .* \n", "");
                bool ContainsHTML = Regex.Match(InstallResult, "<br\\s*/?>").Success;

                //replace the eof char to space
                InstallResult = InstallResult.Replace((char)4, ' ');

                //strip header when html is returned
                int findHtmlIndex = InstallResult.IndexOf("<html>", StringComparison.CurrentCultureIgnoreCase);
                if (findHtmlIndex > -1)
                {
                    InstallResult = InstallResult.Substring(findHtmlIndex);
                }
                findHtmlIndex = InstallResult.IndexOf("</html>", StringComparison.CurrentCultureIgnoreCase);
                if (findHtmlIndex > -1)
                {
                    InstallResult = InstallResult.Substring(0, findHtmlIndex);
                }
                findHtmlIndex = InstallResult.IndexOf("<body>", StringComparison.CurrentCultureIgnoreCase);
                if (findHtmlIndex > -1)
                {
                    InstallResult = InstallResult.Substring(findHtmlIndex);
                }
                findHtmlIndex = InstallResult.IndexOf("</body>", StringComparison.CurrentCultureIgnoreCase);
                if (findHtmlIndex > -1)
                {
                    InstallResult = InstallResult.Substring(0, findHtmlIndex);
                }


                InstallResult = Functions.HtmlToRtf(InstallResult, !ContainsHTML);
                rtbInstallResult.Rtf = InstallResult;
            }
        }


        private void rtbInstallResult_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void rtbInstallResult_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void rtbInstallResult_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    Clipboard.SetText(rtbInstallResult.SelectedText);
                }
            }
        }

    }
}
