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
using System.Threading;
using System.Text.RegularExpressions;
using com.nmtinstaller.csi;
using System.Diagnostics;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.InstalledApps;
using com.nmtinstaller.csi.HardwareTypes;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlSelectApplication : BasePanel
    {

        private Repository repository;
        private RepositoryFileInfoBase SelectedItem = null;
        private frmScreenshots ScreenShotsPreview = new frmScreenshots();
        List<PictureBox> pictureBoxes = new List<PictureBox>();
        Thread DownloadScreenshots = null;
        private InstalledAppsManager installedAppsManager;



        public override bool Execute()
        {

            if ((SelectedItem != null) && (SelectedItem is RepositoryFileInfoBase) && ((SelectedItem.DownloadURL.Length>0) || (SelectedItem is RepositoryWebserviceInfo)))
            {
                string LocalFolder = Constants.TemporaryFolder;

                try
                {

                    if (!(SelectedItem is RepositoryWebserviceInfo))
                    {
                        string DownloadURL = ((RepositoryFileInfoBase)SelectedItem).DownloadURL;
                        LocalFolder = HttpCommands.DownloadHTTPFileAndExtract(DownloadURL, Constants.TemporaryFolder, true, OnProgress);
                    }

                    //download installprepare when installing an application, waitimage or webservice
                    if ((SelectedItem is RepositoryApplicationInfo) || (SelectedItem is RepositoryWaitImagesInfo) || (SelectedItem is RepositoryWebserviceInfo))
                    {
                        HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.InstallPrepareScriptURL, LocalFolder, false, null);
                    }

                    //download AppInit during installation of Application
                    if (SelectedItem is RepositoryApplicationInfo)
                    {
                        HttpCommands.DownloadHTTPFileAndExtract(Settings.Default.AppInitScriptURL, LocalFolder, false, null);
                    }

                    panelResults.Remove("SelectedItem");
                    panelResults.Remove("ThemeFormat");
                    panelResults.Add("SelectedItem", SelectedItem);
                    if (SelectedItem is RepositoryThemeInfo)
                    {
                        if (cbxThemeFormat.SelectedIndex == 0)
                        {
                            panelResults.Add("ThemeFormat", "HD");
                        }
                        else
                        {
                            panelResults.Add("ThemeFormat", "SD");
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Logger.GetInstance().AddLogLine(LogLevel.Error, "Error while trying to download installation files", ex);
                    MessageBox.Show(Resources.MessageBox_DownloadPackageFailed, Resources.MessageBox_DownloadPackageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                return (SelectedItem is RepositoryWebserviceInfo);
            }

            
        }


        private void DisplayInfo()
        {
            if ((SelectedItem != null) && (SelectedItem is RepositoryFileInfoBase))
            {

                llWebbrowserInterface.Links.Clear();
                llWebbrowserInterface.Text = string.Empty;
                tbWebserviceName.Text = string.Empty;
                tbAdditionalUsageInstrictions.Text = string.Empty;


                if (SelectedItem is RepositoryApplicationInfo)
                {
                    tbItemType.Text = Resources.InstallItem_ApplicationString;

                    if (((RepositoryApplicationInfo)SelectedItem).WebInterfaceURL.Length > 0)
                    {
                        llWebbrowserInterface.Links.Clear();
                        llWebbrowserInterface.Text = ((RepositoryApplicationInfo)SelectedItem).WebInterfaceURL;
                        llWebbrowserInterface.Links.Add(0, llWebbrowserInterface.Text.Length, ((RepositoryApplicationInfo)SelectedItem).WebInterfaceURL);
                    }

                    if (((RepositoryApplicationInfo)SelectedItem).GayaInterfaceURL.Length > 0)
                    {
                        tbWebserviceName.Text = ((RepositoryApplicationInfo)SelectedItem).Name;
                    }
                }

                if (SelectedItem is RepositoryThemeInfo)
                {
                    tbItemType.Text = Resources.InstallItem_ThemeString;
                }
                if (SelectedItem is RepositoryCustomMenuInfo)
                {
                    tbItemType.Text = Resources.InstallItem_IndexString;
                }
                if (SelectedItem is RepositoryWaitImagesInfo)
                {
                    tbItemType.Text = Resources.InstallItem_WaitImageSetString;
                }
                if (SelectedItem is RepositoryWebserviceInfo)
                {
                    tbItemType.Text = Resources.InstallItem_WebserviceString;
                }

                tbAuthor.Text = ((RepositoryFileInfoBase)SelectedItem).Author;
                string Description = ((RepositoryFileInfoBase)SelectedItem).Description;
                tbDescription.Text = Description;
                tbHomepageURL.Text = ((RepositoryFileInfoBase)SelectedItem).Homepage;
                tbMaintainer.Text = ((RepositoryFileInfoBase)SelectedItem).Maintainer;
                tbForumTopic.Text = ((RepositoryFileInfoBase)SelectedItem).Forum;
                tbChangelog.Text = ((RepositoryFileInfoBase)SelectedItem).Changelog;
                tbAdditionalUsageInstrictions.Text = ((RepositoryFileInfoBase)SelectedItem).UsageInstructions;

                AddScreenshots(((RepositoryFileInfoBase)SelectedItem).Screenshots);
                
                cbxStartAppOnBoot.Tag = 1;

                
                if (tabSelectCategory.SelectedTab == tabInstalled)
                {
                    ShowApplicationActionPanel();
                }
                else
                {
                    HideApplicationActionPanel();
                }


                int i = 0;
                while ((i < installedAppsManager.InstalledApps.Count) && (!installedAppsManager.InstalledApps[i].Name.Equals(((RepositoryFileInfoBase)SelectedItem).Name, StringComparison.CurrentCultureIgnoreCase)))
                    i++;
                if (i < installedAppsManager.InstalledApps.Count)
                {
                    tbInstalledVersion.Text = Resources.Text_Installed+", v" + installedAppsManager.InstalledApps[i].Version;
                    tbInstalledVersion.Text += ", " + (installedAppsManager.InstalledApps[i].Started ? Resources.Text_Started: Resources.Text_Stopped);
                    btnUninstall.Enabled = true;
                    btnStartApplication.Text = installedAppsManager.InstalledApps[i].Started ? Resources.Text_Stop : Resources.Text_Start;
                    btnStartApplication.Enabled = true;

                    btnRestart.Enabled = installedAppsManager.InstalledApps[i].Started;

                    cbxStartAppOnBoot.Checked = installedAppsManager.InstalledApps[i].Enabled;
                    cbxStartAppOnBoot.Enabled = true;
                }
                else
                {

                    tbInstalledVersion.Text = (SelectedItem is RepositoryApplicationInfo) ? Resources.Text_NotInstalled : Resources.Text_StateUnknown;
                    btnUninstall.Enabled = false;
                    btnStartApplication.Enabled = false;
                    btnRestart.Enabled = false;
                    btnRestart.Enabled = false;
                    cbxStartAppOnBoot.Checked = false;
                    cbxStartAppOnBoot.Enabled = false;
                }

                cbxStartAppOnBoot.Tag = null;
            }
        }

        private void HideApplicationActionPanel()
        {
            if (pnlActionButtons.Visible == true)
            {
                pnlActionButtons.Visible = false;
                pnlItemDetails.Height += pnlActionButtons.Height;
                lvSelectItem.Height += pnlActionButtons.Height;

                lvSelectItem.Invalidate();
            }
        }

        private void ShowApplicationActionPanel()
        {
            if (pnlActionButtons.Visible == false)
            {
                pnlActionButtons.Visible = true;
                pnlItemDetails.Height -= pnlActionButtons.Height;
                lvSelectItem.Height -= pnlActionButtons.Height;

                lvSelectItem.Invalidate();
            }
        }

        private void ShowApplicationSuggestion()
        {
            if ((bool)panelResults["FirstRun"])
            {
                int i = 0;
                while ((i < repository.Applications.Count) && (string.Compare("lighttpd", repository.Applications[i].Name, true) != 0))
                {
                    i++;
                }

                if (i < repository.Applications.Count)
                {
                    i = 0;
                    while ((i < installedAppsManager.InstalledApps.Count) && (string.Compare(installedAppsManager.InstalledApps[i].Name, "lighttpd", true) != 0))
                    {
                        i++;
                    }

                    if (i >= installedAppsManager.InstalledApps.Count)
                    {
                        ToolTip tt = new ToolTip();
                        tt.IsBalloon = true;
                        tt.UseFading = true;

                        tt.ToolTipTitle = Resources.Tooltip_ApplicationSuggestionCaption;
                        tt.Show(Resources.Tooltip_ApplicationSuggestionText, lvSelectItem, new Point(0, int.Parse(Resources.Tooltip_ApplicationSuggestionHorizontalOffset)), Settings.Default.ApplicationSuggestionDuration);
                    }
                }
            }
        }

        private void pnlSelectApplication_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {

                repository = (Repository)panelResults["Repository"];

                if (installedAppsManager == null)
                {
                    installedAppsManager = new InstalledAppsManager(null, repository);
                }

                Thread upd = new Thread(new ThreadStart(UpdateAppInfo));
                upd.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                upd.Start();

                panelResults.Remove("SelectedItem");
                panelResults.Remove("ThemeFormat");

                this.Parent.Parent.Width += 150;
                this.Parent.Parent.MinimumSize = new Size(this.Parent.Parent.MinimumSize.Width + 150, this.Parent.Parent.MinimumSize.Height);

                AddScreenshots(new string[0]);

                tabSelectCategory.Tag = 1;
                ShowHideNewTab();
                ShowHideUpdateTab();
                ShowHideSupportedTabs();

                tabSelectCategory.Tag = null;
                tabSelectCategory_SelectedIndexChanged(tabSelectCategory, EventArgs.Empty);
            }
            else
            {
                if (ScreenShotsPreview.Visible)
                {
                    ScreenShotsPreview.Visible = false;
                }

                if (this.Parent != null)
                {
                    this.Parent.Parent.MinimumSize = new Size(this.Parent.Parent.MinimumSize.Width - 150, this.Parent.Parent.MinimumSize.Height);
                    this.Parent.Parent.Width -= 150;
                }
            }
        }

        private void ShowHideSupportedTabs()
        {
            HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));
            if (det.RepositoryType == "A4")
            {
                if (!tabSelectCategory.TabPages.Contains(tabCustomMenus))
                {
                    tabSelectCategory.TabPages.Add(tabCustomMenus);
                }

                if (tabSelectCategory.TabPages.Contains(tabThemes))
                {
                    tabSelectCategory.TabPages.Remove(tabThemes);
                }

                if (tabSelectCategory.TabPages.Contains(tabWaitImages))
                {
                    tabSelectCategory.TabPages.Remove(tabWaitImages);
                }

                if (tabSelectCategory.TabPages.Contains(tabWebservices))
                {
                    tabSelectCategory.TabPages.Remove(tabWebservices);
                }
            }
            if (det.RepositoryType == "C" | det.RepositoryType == "V")
            {
                if (tabSelectCategory.TabPages.Contains(tabThemes))
                {
                    tabSelectCategory.TabPages.Remove(tabThemes);
                }

                if (tabSelectCategory.TabPages.Contains(tabCustomMenus))
                {
                    tabSelectCategory.TabPages.Remove(tabCustomMenus);
                }

                if (tabSelectCategory.TabPages.Contains(tabWaitImages))
                {
                    tabSelectCategory.TabPages.Remove(tabWaitImages);
                }

                if (tabSelectCategory.TabPages.Contains(tabWebservices))
                {
                    tabSelectCategory.TabPages.Remove(tabWebservices);
                }
            }
            if (det.RepositoryType == "AB")
            {
                if (!tabSelectCategory.TabPages.Contains(tabThemes))
                {
                    tabSelectCategory.TabPages.Add(tabThemes);
                }

                if (!tabSelectCategory.TabPages.Contains(tabWaitImages))
                {
                    tabSelectCategory.TabPages.Add(tabWaitImages);
                }
            }
        }

        private void AddSelectItemItem(RepositoryFileInfoBase item)
        {
            AddSelectItemItem(item, null);
        }

        private void AddSelectItemItem(RepositoryFileInfoBase item, int? imageIndex)
        {
            if (imageIndex == null)
            {
                if (item is RepositoryApplicationInfo)
                {
                    imageIndex = 4;
                }

                if (item is RepositoryThemeInfo)
                {
                    imageIndex = 5;
                }

                if (item is RepositoryWaitImagesInfo)
                {
                    imageIndex = 6;
                }

                if (item is RepositoryCustomMenuInfo)
                {
                    imageIndex = 7;
                }

                if (item is RepositoryWebserviceInfo)
                {
                    imageIndex = 8;
                }

                if (imageIndex == null)
                {
                    imageIndex = 4;
                }
            }

            ListViewItem lvi = new ListViewItem(item.ToString(), (int)imageIndex);
            lvi.Tag = item;

            lvSelectItem.Items.Add(lvi);
        }


        private void UpdateUI()
        {
            string PrevSelection = "";
            
            if (lvSelectItem.SelectedItems.Count>0)
            {
                PrevSelection = lvSelectItem.SelectedItems[0].Text;
            }
            
            lvSelectItem.Items.Clear();
            lvSelectItem.Tag = 1;

            if (tabSelectCategory.SelectedTab == tabNew)
            {


                HardwareDetails det = HardwareDetailsFactory.CreateDetails((HardwareTypeEnum)Enum.Parse(typeof(HardwareTypeEnum), Settings.Default.HardwareType));
                foreach (RepositoryApplicationInfo app in repository.Applications)
                {
                    if (app.IsNew)
                    {
                        //lbSelectedItem.Items.Add(app);
                        AddSelectItemItem(app,0);
                    }
                }
                foreach (RepositoryCustomMenuInfo menu in repository.CustomMenus)
                {
                    if ((menu.IsNew) && (det.RepositoryType == "AB" | det.RepositoryType == "A4"))
                    {
                        //lbSelectedItem.Items.Add(menu);
                        AddSelectItemItem(menu, 0);
                    }
                }
                foreach (RepositoryThemeInfo theme in repository.Themes)
                {
                    if (theme.IsNew && det.RepositoryType == "AB")
                    {
                        //lbSelectedItem.Items.Add(theme);
                        AddSelectItemItem(theme, 0);
                    }
                }
                foreach (RepositoryWaitImagesInfo waitImages in repository.WaitImages)
                {
                    if (waitImages.IsNew && det.RepositoryType == "AB")
                    {
                        //lbSelectedItem.Items.Add(waitImages);
                        AddSelectItemItem(waitImages, 0);
                    }
                }
                foreach (RepositoryWebserviceInfo web in repository.Webservices)
                {
                    if (web.IsNew && det.RepositoryType == "AB")
                    {
                        //lbSelectedItem.Items.Add(web);
                        AddSelectItemItem(web, 0);
                    }
                }
            }

            if (tabSelectCategory.SelectedTab == tabApplications)
            {
                foreach (RepositoryApplicationInfo app in repository.Applications)
                {
                    //lbSelectedItem.Items.Add(app);
                    AddSelectItemItem(app);
                }
            }

            if (tabSelectCategory.SelectedTab == tabCustomMenus)
            {
                foreach (RepositoryCustomMenuInfo index in repository.CustomMenus)
                {
                    //lbSelectedItem.Items.Add(index);
                    AddSelectItemItem(index);
                }
            }

            if (tabSelectCategory.SelectedTab == tabThemes)
            {

                foreach (RepositoryThemeInfo them in repository.Themes)
                {

                    if ((cbxThemeFormat.SelectedIndex == 0) && (them.ForHD))
                    {
                        //lbSelectedItem.Items.Add(them);
                        AddSelectItemItem(them);
                    }
                    if ((cbxThemeFormat.SelectedIndex == 1) && (them.ForSD))
                    {
                        //lbSelectedItem.Items.Add(them);
                        AddSelectItemItem(them);
                    }
                }
            }

            if (tabSelectCategory.SelectedTab == tabWaitImages)
            {
                foreach (RepositoryWaitImagesInfo wi in repository.WaitImages)
                {
                    //lbSelectedItem.Items.Add(wi);
                    AddSelectItemItem(wi);
                }
            }

            if (tabSelectCategory.SelectedTab == tabWebservices)
            {
                foreach (RepositoryWebserviceInfo wsi in repository.Webservices)
                {
                    //lbSelectedItem.Items.Add(wsi);
                    AddSelectItemItem(wsi);
                }
            }

            if (tabSelectCategory.SelectedTab == tabUpdates)
            {
                List<RepositoryFileInfoBase> allItems = new List<RepositoryFileInfoBase>();
                allItems.AddRange(repository.Applications.ToArray());
                allItems.AddRange(repository.CustomMenus.ToArray());
                allItems.AddRange(repository.Themes.ToArray());
                allItems.AddRange(repository.WaitImages.ToArray());
                allItems.AddRange(repository.Webservices.ToArray());

                foreach (RepositoryFileInfoBase item in allItems)
                {
                    if (item.UpdateAvailable)
                    {
                        //lbSelectedItem.Items.Add(item);
                        AddSelectItemItem(item, 1);
                    }

                }
            }

            if (tabSelectCategory.SelectedTab == tabInstalled)
            {
                foreach (InstalledAppInfo appinfo in installedAppsManager.InstalledApps)
                {
                    List<RepositoryFileInfoBase> allItems = new List<RepositoryFileInfoBase>();
                    allItems.AddRange(repository.Applications.ToArray());
                    allItems.AddRange(repository.CustomMenus.ToArray());
                    allItems.AddRange(repository.Themes.ToArray());
                    allItems.AddRange(repository.WaitImages.ToArray());
                    allItems.AddRange(repository.Webservices.ToArray());

                    int i = 0;
                    while ((i < allItems.Count) && (!allItems[i].Name.Equals(appinfo.Name, StringComparison.CurrentCultureIgnoreCase)))
                        i++;

                    if (i < allItems.Count)
                    {
                        AddSelectItemItem(new RepositoryApplicationInfo(appinfo.Name, allItems[i].Author, allItems[i].Maintainer, appinfo.Version, allItems[i].Homepage, allItems[i].Forum, allItems[i].Description, allItems[i].Changelog, allItems[i].UsageInstructions, allItems[i].DownloadURL, allItems[i].Screenshots, (allItems[i] is RepositoryApplicationInfo) ? ((RepositoryApplicationInfo)allItems[i]).InstallScript : string.Empty, (allItems[i] is RepositoryApplicationInfo) ? ((RepositoryApplicationInfo)allItems[i]).GayaInterfaceURL : string.Empty, (allItems[i] is RepositoryApplicationInfo) ? ((RepositoryApplicationInfo)allItems[i]).WebInterfaceURL : string.Empty), appinfo.Started ? 2 : 3);
                    }
                    else
                    {
                        //lbSelectedItem.Items.Add(new RepositoryApplicationInfo(appinfo.Name, string.Empty, string.Empty, appinfo.Version, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new string[0], string.Empty, string.Empty, string.Empty));
                        AddSelectItemItem(new RepositoryApplicationInfo(appinfo.Name, string.Empty, string.Empty, appinfo.Version, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, new string[0], string.Empty, string.Empty, string.Empty),appinfo.Started?2:3);
                    }
                    
                }

                //retrigger the scrollbar, needed for some UI quirck
                //where the horizontal scrollbar appeared for no reason
                lvSelectItem.Scrollable = false;
                lvSelectItem.Scrollable = true;
            }


            lvSelectItem.Tag = null;
            lvSelectItem.SelectedItems.Clear();

            int j = 0;
            while ((j < lvSelectItem.Items.Count) && (lvSelectItem.Items[j].Text != PrevSelection))
                j++;

            if (j < lvSelectItem.Items.Count)
            {
                lvSelectItem.Items[j].Selected=true;
                lvSelectItem.Items[j].BackColor = SystemColors.Highlight;
                lvSelectItem.Items[j].ForeColor = SystemColors.HighlightText;
                lvSelectItem.TopItem = j >= 2 ? lvSelectItem.Items[j - 2] : lvSelectItem.Items[0];
            }
            else
            {

                if (lvSelectItem.Items.Count > 0)
                {
                    lvSelectItem.Items[0].Selected = true;
                    lvSelectItem.Items[0].BackColor = SystemColors.Highlight;
                    lvSelectItem.Items[0].ForeColor = SystemColors.HighlightText;
                }
                else
                {
                    tbAuthor.Text = tbDescription.Text = tbHomepageURL.Text = tbInstalledVersion.Text =
                        tbItemType.Text = tbMaintainer.Text = tbAdditionalUsageInstrictions.Text =
                        tbChangelog.Text = tbWebserviceName.Text = string.Empty;

                    llWebbrowserInterface.Text = string.Empty;
                }
            }
        }

        private bool ShowHideUpdateTab()
        {
            List<RepositoryFileInfoBase> allItems = new List<RepositoryFileInfoBase>();
            allItems.AddRange(repository.Applications.ToArray());
            allItems.AddRange(repository.CustomMenus.ToArray());
            allItems.AddRange(repository.Themes.ToArray());
            allItems.AddRange(repository.WaitImages.ToArray());
            allItems.AddRange(repository.Webservices.ToArray());

            int i = 0;
            while ((i < allItems.Count) && (!allItems[i].UpdateAvailable))
                i++;

            if (i < allItems.Count)
            {
                if (!tabSelectCategory.TabPages.Contains(tabUpdates))
                {
                    tabSelectCategory.TabPages.Add(tabUpdates);
                    tabSelectCategory.Invalidate();
                }
                return false;
            }
            else
            {
                if (tabSelectCategory.TabPages.Contains(tabUpdates))
                {
                    tabSelectCategory.TabPages.Remove(tabUpdates);
                    tabSelectCategory.Invalidate();
                }
                return true;
            }
        }

        private bool ShowHideNewTab()
        {
            bool newItems = false;
            
            int i=0;
            while ((i<repository.Applications.Count) && (!repository.Applications[i].IsNew))
                i++;
            newItems = ((newItems) || (i < repository.Applications.Count));

            i = 0;
            while ((i < repository.Themes.Count) && (!repository.Themes[i].IsNew))
                i++;
            newItems = ((newItems) || (i < repository.Themes.Count));

            i = 0;
            while ((i < repository.WaitImages.Count) && (!repository.WaitImages[i].IsNew))
                i++;
            newItems = ((newItems) || (i < repository.WaitImages.Count));

            i = 0;
            while ((i < repository.CustomMenus.Count) && (!repository.CustomMenus[i].IsNew))
                i++;
            newItems = ((newItems) || (i < repository.CustomMenus.Count));

            i = 0;
            while ((i < repository.Webservices.Count) && (!repository.Webservices[i].IsNew))
                i++;
            newItems = ((newItems) || (i < repository.Webservices.Count));

            if ((!newItems || Settings.Default.FirstRun) && (tabSelectCategory.TabPages.Contains(tabNew)))
            {
                tabSelectCategory.TabPages.Remove(tabNew);
                tabSelectCategory.Invalidate();
                return true;
            }
            else
            {
                return false;
            }
        }


        private void linkHomepage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }


        private void tbHomepageURL_DoubleClick(object sender, EventArgs e)
        {
            if (tbHomepageURL.Text.Length > 0)
            {
                Process.Start(tbHomepageURL.Text);
            }
        }


        private void cbxThemeFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxThemeFormat.Tag == null)
            {
                UpdateUI();
            }
        }


        private void ScreenShotClick(object sender, EventArgs args)
        {
            if ((ScreenShotsPreview == null) || (!ScreenShotsPreview.Visible) || (ScreenShotsPreview.IsDisposed))
            {
                ScreenShotsPreview = new frmScreenshots();
                ScreenShotsPreview.Show();
            }

            int i = 0;
            int CurrentIndex = 0;
            bool skip = true;
            foreach (PictureBox pb in flowScreenshots.Controls)
            {
                if (!skip)
                {
                    if (pb.Image == ((PictureBox)sender).Image)
                    {
                        CurrentIndex = i;
                    }

                    i++;
                }
                else
                {
                    skip = false;
                }
            }

            List<Control> col = new List<Control>();
            i = 0;
            foreach(Control c in (Control.ControlCollection)flowScreenshots.Controls)
            {
                if (i > 0)
                {
                    col.Add(c);
                }
                else
                {
                    i = 1;
                }
            }

            ScreenShotsPreview.SetScreenshot(col, CurrentIndex);
        }



        private void AddScreenshotsThread(object URLs)
        {
            try
            {
                string[] URLlist = (URLs as string[]);
                for (int i = 0; i < URLlist.Length; i++)
                {
                    string item = URLlist[i];
                    try
                    {
                        PictureBox pb = new PictureBox();
                        pb.Load(item);
                        pb.Width = 125;
                        pb.Height = 90;
                        pb.Cursor = Cursors.Hand;
                        pb.Click += new EventHandler(ScreenShotClick);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Margin = new Padding(0, 5, 0, 5);

                        pictureBoxes.Add(pb);

                        SetScreenshotPictureBoxParent(pb);

                        UpdateScreenshotProgressBar((int)(((i + 1) / (float)URLlist.Length) * 100));
                    }
                    catch (Exception ex)
                    {
                        if (ex is ThreadAbortException)
                        {
                            throw ex;
                        }
                    }

                    if ((this.Disposing) || (this.IsDisposed))
                    {
                        break;
                    }
                }

                if ((this.Disposing) || (this.IsDisposed))
                {
                    return;
                }

                if (pictureBoxes.Count == 0)
                {
                    PictureBox pb = new PictureBox();
                    pb.Image = Resources.NoScreenshots;
                    pb.Width = 125;
                    pb.Height = 100;
                    pb.Cursor = Cursors.Hand;
                    pb.Click += new EventHandler(ScreenShotClick);
                    pb.SizeMode = PictureBoxSizeMode.Zoom;
                    pb.Margin = new Padding(0, 5, 0, 5);

                    pictureBoxes.Add(pb);
                    SetScreenshotPictureBoxParent(pb);
                }



                DisableScreenshotProgressBar();
                DownloadScreenshots = null;
                SetDefaultScreenshot();
            }
            catch (ThreadAbortException)
            {
                Thread.ResetAbort();
            }
        }

        public void AddScreenshots(string[] Screenshots)
        {
           
            if (DownloadScreenshots != null)
            {
                try
                {
                    DownloadScreenshots.Abort();
                    DownloadScreenshots.Join();
                    DownloadScreenshots = null;
                }
                catch
                {
                }
            }

            foreach (PictureBox item in pictureBoxes)
            {
                item.Hide();
                item.Dispose();
            }
            pictureBoxes.Clear();
            

            pbLoadingScreenshots.Visible = true;
            pbLoadingScreenshots.Value = 0;
            pbLoadingScreenshots.Style = ProgressBarStyle.Marquee;

            DownloadScreenshots = new Thread(new ParameterizedThreadStart(AddScreenshotsThread));
            DownloadScreenshots.Start(Screenshots);
        }

        private void SetScreenshotPictureBoxParent(PictureBox pb)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                pb.Parent = flowScreenshots;
            }));
        }

        public void UpdateScreenshotProgressBar(int procent)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                if (pbLoadingScreenshots.Style == ProgressBarStyle.Marquee)
                {
                    pbLoadingScreenshots.Style = ProgressBarStyle.Continuous;
                }

                pbLoadingScreenshots.Value = procent;
                Application.DoEvents();
                Thread.Sleep(0);
            }));
        }

        public void DisableScreenshotProgressBar()
        {
            this.Invoke(new MethodInvoker(delegate
            {
                pbLoadingScreenshots.Visible = false;
                pbLoadingScreenshots.Style = ProgressBarStyle.Continuous;
            }));
        }

        private void SetDefaultScreenshot()
        {
            if ((ScreenShotsPreview.Visible) && (!ScreenShotsPreview.IsDisposed))
            {
                ScreenShotClick(pictureBoxes[0], EventArgs.Empty);
            }
        }


        private void tabSelectCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabSelectCategory.Tag == null)
            {
                lblThemeFormat.Visible = cbxThemeFormat.Visible = (tabSelectCategory.SelectedTab == tabThemes);

                if (cbxThemeFormat.Visible)
                {
                    cbxThemeFormat.Tag = 1;
                    cbxThemeFormat.SelectedIndex = 0;
                    cbxThemeFormat.Tag = null;
                }

                UpdateUI();
            }
        }


        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format(Resources.MessageBox_UninstallConfirm, SelectedItem.Name), Resources.MessageBox_UninstallCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OnProgress(-1, Resources.Text_Uninstalling + " "+ SelectedItem.Name);
                cbxStartAppOnBoot.Enabled = btnStartApplication.Enabled = btnUninstall.Enabled = btnRestart.Enabled = false;
                Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
                t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                t.Start("Apps/AppInit/appinit.cgi?uninstall&" + SelectedItem.Name);
            }
        }

        private void btnStartApplication_Click(object sender, EventArgs e)
        {
            cbxStartAppOnBoot.Enabled = btnStartApplication.Enabled = btnUninstall.Enabled = btnRestart.Enabled = false;
            if (btnStartApplication.Text == Resources.Text_Start)
            {
                OnProgress(-1, Resources.Text_Starting + " " + SelectedItem.Name);
                Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
                t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                t.Start("Apps/AppInit/appinit.cgi?start&" + SelectedItem.Name);
            }
            else
            {
                OnProgress(-1, Resources.Text_Stopping + " " + SelectedItem.Name);
                Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
                t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                t.Start("Apps/AppInit/appinit.cgi?stop&" + SelectedItem.Name);
            }
        }

        private void UpdateAppInfo()
        {
            OnProgress(-1, Resources.Text_FetchingApplicationInformation);
            installedAppsManager.UpdateAppInfo();
            try
            {
                lvSelectItem.Invoke(new MethodInvoker(delegate 
                    { 
                        UpdateUI();

                        ShowApplicationSuggestion();

                        if (!ShowHideUpdateTab())
                        {
                            MessageBox.Show(Resources.MessageBox_ApplicationUpdatesAvailable,Resources.MessageBox_ApplicationUpdatesAvailableCaption , MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }));
            }
            catch { }
            OnProgress(100, Resources.Text_FetchingApplicationInformation);
        }

        private void ExecuteAppInitCommand(object command)
        {
            string result = HttpCommands.StartScript(Settings.Default.Server, command.ToString().Replace(" ", "%20"));
            UpdateAppInfo();
        }


        private void cbxStartAppOnBoot_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxStartAppOnBoot.Tag == null)
            {
                cbxStartAppOnBoot.Enabled = btnStartApplication.Enabled = btnUninstall.Enabled = btnRestart.Enabled = false;

                if (cbxStartAppOnBoot.Checked)
                {
                    OnProgress(-1, Resources.Text_EnabelingBootStart+" " + SelectedItem.Name);
                    Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
                    t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                    t.Start("Apps/AppInit/appinit.cgi?enable&" + SelectedItem.Name);
                }
                else
                {
                    OnProgress(-1, Resources.Text_DisablingBootStart+" " + SelectedItem.Name);
                    Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
                    t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                    t.Start("Apps/AppInit/appinit.cgi?disable&" + SelectedItem.Name);
                }
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            OnProgress(-1, Resources.Text_Restarting+" " + SelectedItem.Name);
            cbxStartAppOnBoot.Enabled = btnStartApplication.Enabled = btnUninstall.Enabled = btnRestart.Enabled = false;
            Thread t = new Thread(new ParameterizedThreadStart(ExecuteAppInitCommand));
            t.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
            t.Start("Apps/AppInit/appinit.cgi?restart&" + SelectedItem.Name);
        }

        private void llWebbrowserInterface_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void tbForumTopic_DoubleClick(object sender, EventArgs e)
        {
            if (tbHomepageURL.Text.Length > 0)
            {
                Process.Start(tbForumTopic.Text);
            }
        }

        public pnlSelectApplication(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo progress)
            : base(Resources.PanelTitle_SelectApplication, panelResults, progress)
        {
            InitializeComponent();
        }

        private void lvSelectItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lvSelectItem.Items)
            {
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;
            }

            if (lvSelectItem.Tag == null)
            {
                if (lvSelectItem.SelectedItems.Count > 0)
                {
                    SelectedItem = (RepositoryFileInfoBase)lvSelectItem.SelectedItems[0].Tag;
                    DisplayInfo();
                }
            }
        }

        private void lvSelectItem_Resize(object sender, EventArgs e)
        {
            lvSelectItem.Columns[0].Width = lvSelectItem.ClientSize.Width;
        }


    }
}
