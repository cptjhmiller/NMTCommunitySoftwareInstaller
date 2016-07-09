namespace com.nmtinstaller.csi.Panels
{
    partial class pnlSelectApplication
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlSelectApplication));
            this.lblSelectApplication = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.tabSelectCategory = new System.Windows.Forms.TabControl();
            this.tabNew = new System.Windows.Forms.TabPage();
            this.tabApplications = new System.Windows.Forms.TabPage();
            this.tabThemes = new System.Windows.Forms.TabPage();
            this.tabCustomMenus = new System.Windows.Forms.TabPage();
            this.tabWaitImages = new System.Windows.Forms.TabPage();
            this.tabWebservices = new System.Windows.Forms.TabPage();
            this.tabInstalled = new System.Windows.Forms.TabPage();
            this.tabUpdates = new System.Windows.Forms.TabPage();
            this.lblThemeFormat = new System.Windows.Forms.Label();
            this.cbxThemeFormat = new System.Windows.Forms.ComboBox();
            this.flowScreenshots = new System.Windows.Forms.FlowLayoutPanel();
            this.pbLoadingScreenshots = new System.Windows.Forms.ProgressBar();
            this.pnlTabContent = new System.Windows.Forms.Panel();
            this.lvSelectItem = new System.Windows.Forms.ListView();
            this.clmHeader = new System.Windows.Forms.ColumnHeader();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlItemDetails = new System.Windows.Forms.Panel();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbForumTopic = new System.Windows.Forms.TextBox();
            this.lblForumTopic = new System.Windows.Forms.Label();
            this.gbxUsageInstructions = new System.Windows.Forms.GroupBox();
            this.llWebbrowserInterface = new System.Windows.Forms.LinkLabel();
            this.tbAdditionalUsageInstrictions = new System.Windows.Forms.TextBox();
            this.lblWebInterfaceUrl = new System.Windows.Forms.Label();
            this.lblAdditionalInstructions = new System.Windows.Forms.Label();
            this.lblGayaInterfaceName = new System.Windows.Forms.Label();
            this.tbWebserviceName = new System.Windows.Forms.TextBox();
            this.tbChangelog = new System.Windows.Forms.TextBox();
            this.lblChangelog = new System.Windows.Forms.Label();
            this.tbItemType = new System.Windows.Forms.TextBox();
            this.lblItemType = new System.Windows.Forms.Label();
            this.lblHomepageStatic = new System.Windows.Forms.Label();
            this.tbHomepageURL = new System.Windows.Forms.TextBox();
            this.lblInstalled = new System.Windows.Forms.Label();
            this.tbInstalledVersion = new System.Windows.Forms.TextBox();
            this.tbAuthor = new System.Windows.Forms.TextBox();
            this.lblMaintainerStatic = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.tbMaintainer = new System.Windows.Forms.TextBox();
            this.pnlActionButtons = new System.Windows.Forms.Panel();
            this.btnStartApplication = new System.Windows.Forms.Button();
            this.cbxStartAppOnBoot = new System.Windows.Forms.CheckBox();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.btnRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabSelectCategory.SuspendLayout();
            this.pnlTabContent.SuspendLayout();
            this.pnlItemDetails.SuspendLayout();
            this.gbxUsageInstructions.SuspendLayout();
            this.pnlActionButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.AccessibleDescription = null;
            this.pictureBox1.AccessibleName = null;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.BackgroundImage = null;
            this.pictureBox1.Font = null;
            this.pictureBox1.ImageLocation = null;
            // 
            // lblSelectApplication
            // 
            this.lblSelectApplication.AccessibleDescription = null;
            this.lblSelectApplication.AccessibleName = null;
            resources.ApplyResources(this.lblSelectApplication, "lblSelectApplication");
            this.lblSelectApplication.Font = null;
            this.lblSelectApplication.Name = "lblSelectApplication";
            // 
            // tbDescription
            // 
            this.tbDescription.AcceptsReturn = true;
            this.tbDescription.AccessibleDescription = null;
            this.tbDescription.AccessibleName = null;
            resources.ApplyResources(this.tbDescription, "tbDescription");
            this.tbDescription.BackgroundImage = null;
            this.tbDescription.Font = null;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            // 
            // tabSelectCategory
            // 
            this.tabSelectCategory.AccessibleDescription = null;
            this.tabSelectCategory.AccessibleName = null;
            resources.ApplyResources(this.tabSelectCategory, "tabSelectCategory");
            this.tabSelectCategory.BackgroundImage = null;
            this.tabSelectCategory.Controls.Add(this.tabNew);
            this.tabSelectCategory.Controls.Add(this.tabApplications);
            this.tabSelectCategory.Controls.Add(this.tabThemes);
            this.tabSelectCategory.Controls.Add(this.tabCustomMenus);
            this.tabSelectCategory.Controls.Add(this.tabWaitImages);
            this.tabSelectCategory.Controls.Add(this.tabWebservices);
            this.tabSelectCategory.Controls.Add(this.tabInstalled);
            this.tabSelectCategory.Controls.Add(this.tabUpdates);
            this.tabSelectCategory.Font = null;
            this.tabSelectCategory.Name = "tabSelectCategory";
            this.tabSelectCategory.SelectedIndex = 0;
            this.tabSelectCategory.SelectedIndexChanged += new System.EventHandler(this.tabSelectCategory_SelectedIndexChanged);
            // 
            // tabNew
            // 
            this.tabNew.AccessibleDescription = null;
            this.tabNew.AccessibleName = null;
            resources.ApplyResources(this.tabNew, "tabNew");
            this.tabNew.BackgroundImage = null;
            this.tabNew.Name = "tabNew";
            this.tabNew.UseVisualStyleBackColor = true;
            // 
            // tabApplications
            // 
            this.tabApplications.AccessibleDescription = null;
            this.tabApplications.AccessibleName = null;
            resources.ApplyResources(this.tabApplications, "tabApplications");
            this.tabApplications.BackColor = System.Drawing.Color.Transparent;
            this.tabApplications.BackgroundImage = null;
            this.tabApplications.Font = null;
            this.tabApplications.Name = "tabApplications";
            this.tabApplications.UseVisualStyleBackColor = true;
            // 
            // tabThemes
            // 
            this.tabThemes.AccessibleDescription = null;
            this.tabThemes.AccessibleName = null;
            resources.ApplyResources(this.tabThemes, "tabThemes");
            this.tabThemes.BackColor = System.Drawing.Color.Transparent;
            this.tabThemes.BackgroundImage = null;
            this.tabThemes.Font = null;
            this.tabThemes.Name = "tabThemes";
            this.tabThemes.UseVisualStyleBackColor = true;
            // 
            // tabCustomMenus
            // 
            this.tabCustomMenus.AccessibleDescription = null;
            this.tabCustomMenus.AccessibleName = null;
            resources.ApplyResources(this.tabCustomMenus, "tabCustomMenus");
            this.tabCustomMenus.BackColor = System.Drawing.Color.Transparent;
            this.tabCustomMenus.BackgroundImage = null;
            this.tabCustomMenus.Font = null;
            this.tabCustomMenus.Name = "tabCustomMenus";
            this.tabCustomMenus.UseVisualStyleBackColor = true;
            // 
            // tabWaitImages
            // 
            this.tabWaitImages.AccessibleDescription = null;
            this.tabWaitImages.AccessibleName = null;
            resources.ApplyResources(this.tabWaitImages, "tabWaitImages");
            this.tabWaitImages.BackColor = System.Drawing.Color.Transparent;
            this.tabWaitImages.BackgroundImage = null;
            this.tabWaitImages.Font = null;
            this.tabWaitImages.Name = "tabWaitImages";
            this.tabWaitImages.UseVisualStyleBackColor = true;
            // 
            // tabWebservices
            // 
            this.tabWebservices.AccessibleDescription = null;
            this.tabWebservices.AccessibleName = null;
            resources.ApplyResources(this.tabWebservices, "tabWebservices");
            this.tabWebservices.BackgroundImage = null;
            this.tabWebservices.Font = null;
            this.tabWebservices.Name = "tabWebservices";
            this.tabWebservices.UseVisualStyleBackColor = true;
            // 
            // tabInstalled
            // 
            this.tabInstalled.AccessibleDescription = null;
            this.tabInstalled.AccessibleName = null;
            resources.ApplyResources(this.tabInstalled, "tabInstalled");
            this.tabInstalled.BackgroundImage = null;
            this.tabInstalled.Font = null;
            this.tabInstalled.Name = "tabInstalled";
            this.tabInstalled.UseVisualStyleBackColor = true;
            // 
            // tabUpdates
            // 
            this.tabUpdates.AccessibleDescription = null;
            this.tabUpdates.AccessibleName = null;
            resources.ApplyResources(this.tabUpdates, "tabUpdates");
            this.tabUpdates.BackgroundImage = null;
            this.tabUpdates.Font = null;
            this.tabUpdates.Name = "tabUpdates";
            this.tabUpdates.UseVisualStyleBackColor = true;
            // 
            // lblThemeFormat
            // 
            this.lblThemeFormat.AccessibleDescription = null;
            this.lblThemeFormat.AccessibleName = null;
            resources.ApplyResources(this.lblThemeFormat, "lblThemeFormat");
            this.lblThemeFormat.Font = null;
            this.lblThemeFormat.Name = "lblThemeFormat";
            // 
            // cbxThemeFormat
            // 
            this.cbxThemeFormat.AccessibleDescription = null;
            this.cbxThemeFormat.AccessibleName = null;
            resources.ApplyResources(this.cbxThemeFormat, "cbxThemeFormat");
            this.cbxThemeFormat.BackgroundImage = null;
            this.cbxThemeFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxThemeFormat.Font = null;
            this.cbxThemeFormat.FormattingEnabled = true;
            this.cbxThemeFormat.Items.AddRange(new object[] {
            resources.GetString("cbxThemeFormat.Items"),
            resources.GetString("cbxThemeFormat.Items1")});
            this.cbxThemeFormat.Name = "cbxThemeFormat";
            this.cbxThemeFormat.SelectedIndexChanged += new System.EventHandler(this.cbxThemeFormat_SelectedIndexChanged);
            // 
            // flowScreenshots
            // 
            this.flowScreenshots.AccessibleDescription = null;
            this.flowScreenshots.AccessibleName = null;
            resources.ApplyResources(this.flowScreenshots, "flowScreenshots");
            this.flowScreenshots.BackgroundImage = null;
            this.flowScreenshots.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowScreenshots.Font = null;
            this.flowScreenshots.Name = "flowScreenshots";
            // 
            // pbLoadingScreenshots
            // 
            this.pbLoadingScreenshots.AccessibleDescription = null;
            this.pbLoadingScreenshots.AccessibleName = null;
            resources.ApplyResources(this.pbLoadingScreenshots, "pbLoadingScreenshots");
            this.pbLoadingScreenshots.BackgroundImage = null;
            this.pbLoadingScreenshots.Font = null;
            this.pbLoadingScreenshots.Name = "pbLoadingScreenshots";
            this.pbLoadingScreenshots.Step = 0;
            this.pbLoadingScreenshots.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // pnlTabContent
            // 
            this.pnlTabContent.AccessibleDescription = null;
            this.pnlTabContent.AccessibleName = null;
            resources.ApplyResources(this.pnlTabContent, "pnlTabContent");
            this.pnlTabContent.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnlTabContent.BackgroundImage = null;
            this.pnlTabContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTabContent.Controls.Add(this.lvSelectItem);
            this.pnlTabContent.Controls.Add(this.pnlItemDetails);
            this.pnlTabContent.Controls.Add(this.pnlActionButtons);
            this.pnlTabContent.Font = null;
            this.pnlTabContent.Name = "pnlTabContent";
            // 
            // lvSelectItem
            // 
            this.lvSelectItem.AccessibleDescription = null;
            this.lvSelectItem.AccessibleName = null;
            resources.ApplyResources(this.lvSelectItem, "lvSelectItem");
            this.lvSelectItem.BackgroundImage = null;
            this.lvSelectItem.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmHeader});
            this.lvSelectItem.Font = null;
            this.lvSelectItem.FullRowSelect = true;
            this.lvSelectItem.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelectItem.LargeImageList = this.ilIcons;
            this.lvSelectItem.MultiSelect = false;
            this.lvSelectItem.Name = "lvSelectItem";
            this.lvSelectItem.SmallImageList = this.ilIcons;
            this.lvSelectItem.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lvSelectItem.UseCompatibleStateImageBehavior = false;
            this.lvSelectItem.View = System.Windows.Forms.View.Details;
            this.lvSelectItem.Resize += new System.EventHandler(this.lvSelectItem_Resize);
            this.lvSelectItem.SelectedIndexChanged += new System.EventHandler(this.lvSelectItem_SelectedIndexChanged);
            // 
            // clmHeader
            // 
            resources.ApplyResources(this.clmHeader, "clmHeader");
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.ilIcons.Images.SetKeyName(0, "New.png");
            this.ilIcons.Images.SetKeyName(1, "Update.png");
            this.ilIcons.Images.SetKeyName(2, "Started.png");
            this.ilIcons.Images.SetKeyName(3, "Stopped.png");
            this.ilIcons.Images.SetKeyName(4, "Application.png");
            this.ilIcons.Images.SetKeyName(5, "Theme.png");
            this.ilIcons.Images.SetKeyName(6, "WaitImage.png");
            this.ilIcons.Images.SetKeyName(7, "CustomMenu.png");
            this.ilIcons.Images.SetKeyName(8, "Webservice.png");
            // 
            // pnlItemDetails
            // 
            this.pnlItemDetails.AccessibleDescription = null;
            this.pnlItemDetails.AccessibleName = null;
            resources.ApplyResources(this.pnlItemDetails, "pnlItemDetails");
            this.pnlItemDetails.BackgroundImage = null;
            this.pnlItemDetails.Controls.Add(this.lblDescription);
            this.pnlItemDetails.Controls.Add(this.tbForumTopic);
            this.pnlItemDetails.Controls.Add(this.lblForumTopic);
            this.pnlItemDetails.Controls.Add(this.gbxUsageInstructions);
            this.pnlItemDetails.Controls.Add(this.tbChangelog);
            this.pnlItemDetails.Controls.Add(this.lblChangelog);
            this.pnlItemDetails.Controls.Add(this.tbItemType);
            this.pnlItemDetails.Controls.Add(this.lblItemType);
            this.pnlItemDetails.Controls.Add(this.lblHomepageStatic);
            this.pnlItemDetails.Controls.Add(this.tbHomepageURL);
            this.pnlItemDetails.Controls.Add(this.lblThemeFormat);
            this.pnlItemDetails.Controls.Add(this.cbxThemeFormat);
            this.pnlItemDetails.Controls.Add(this.lblInstalled);
            this.pnlItemDetails.Controls.Add(this.tbInstalledVersion);
            this.pnlItemDetails.Controls.Add(this.tbAuthor);
            this.pnlItemDetails.Controls.Add(this.lblMaintainerStatic);
            this.pnlItemDetails.Controls.Add(this.lblAuthor);
            this.pnlItemDetails.Controls.Add(this.tbMaintainer);
            this.pnlItemDetails.Controls.Add(this.tbDescription);
            this.pnlItemDetails.Font = null;
            this.pnlItemDetails.Name = "pnlItemDetails";
            // 
            // lblDescription
            // 
            this.lblDescription.AccessibleDescription = null;
            this.lblDescription.AccessibleName = null;
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.Font = null;
            this.lblDescription.Name = "lblDescription";
            // 
            // tbForumTopic
            // 
            this.tbForumTopic.AccessibleDescription = null;
            this.tbForumTopic.AccessibleName = null;
            resources.ApplyResources(this.tbForumTopic, "tbForumTopic");
            this.tbForumTopic.BackgroundImage = null;
            this.tbForumTopic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbForumTopic.ForeColor = System.Drawing.Color.Blue;
            this.tbForumTopic.Name = "tbForumTopic";
            this.tbForumTopic.ReadOnly = true;
            this.tbForumTopic.DoubleClick += new System.EventHandler(this.tbForumTopic_DoubleClick);
            // 
            // lblForumTopic
            // 
            this.lblForumTopic.AccessibleDescription = null;
            this.lblForumTopic.AccessibleName = null;
            resources.ApplyResources(this.lblForumTopic, "lblForumTopic");
            this.lblForumTopic.BackColor = System.Drawing.Color.Transparent;
            this.lblForumTopic.Font = null;
            this.lblForumTopic.Name = "lblForumTopic";
            // 
            // gbxUsageInstructions
            // 
            this.gbxUsageInstructions.AccessibleDescription = null;
            this.gbxUsageInstructions.AccessibleName = null;
            resources.ApplyResources(this.gbxUsageInstructions, "gbxUsageInstructions");
            this.gbxUsageInstructions.BackgroundImage = null;
            this.gbxUsageInstructions.Controls.Add(this.llWebbrowserInterface);
            this.gbxUsageInstructions.Controls.Add(this.tbAdditionalUsageInstrictions);
            this.gbxUsageInstructions.Controls.Add(this.lblWebInterfaceUrl);
            this.gbxUsageInstructions.Controls.Add(this.lblAdditionalInstructions);
            this.gbxUsageInstructions.Controls.Add(this.lblGayaInterfaceName);
            this.gbxUsageInstructions.Controls.Add(this.tbWebserviceName);
            this.gbxUsageInstructions.Font = null;
            this.gbxUsageInstructions.Name = "gbxUsageInstructions";
            this.gbxUsageInstructions.TabStop = false;
            // 
            // llWebbrowserInterface
            // 
            this.llWebbrowserInterface.AccessibleDescription = null;
            this.llWebbrowserInterface.AccessibleName = null;
            resources.ApplyResources(this.llWebbrowserInterface, "llWebbrowserInterface");
            this.llWebbrowserInterface.Name = "llWebbrowserInterface";
            this.llWebbrowserInterface.TabStop = true;
            this.llWebbrowserInterface.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebbrowserInterface_LinkClicked);
            // 
            // tbAdditionalUsageInstrictions
            // 
            this.tbAdditionalUsageInstrictions.AcceptsReturn = true;
            this.tbAdditionalUsageInstrictions.AccessibleDescription = null;
            this.tbAdditionalUsageInstrictions.AccessibleName = null;
            resources.ApplyResources(this.tbAdditionalUsageInstrictions, "tbAdditionalUsageInstrictions");
            this.tbAdditionalUsageInstrictions.BackgroundImage = null;
            this.tbAdditionalUsageInstrictions.Font = null;
            this.tbAdditionalUsageInstrictions.Name = "tbAdditionalUsageInstrictions";
            this.tbAdditionalUsageInstrictions.ReadOnly = true;
            // 
            // lblWebInterfaceUrl
            // 
            this.lblWebInterfaceUrl.AccessibleDescription = null;
            this.lblWebInterfaceUrl.AccessibleName = null;
            resources.ApplyResources(this.lblWebInterfaceUrl, "lblWebInterfaceUrl");
            this.lblWebInterfaceUrl.Font = null;
            this.lblWebInterfaceUrl.Name = "lblWebInterfaceUrl";
            // 
            // lblAdditionalInstructions
            // 
            this.lblAdditionalInstructions.AccessibleDescription = null;
            this.lblAdditionalInstructions.AccessibleName = null;
            resources.ApplyResources(this.lblAdditionalInstructions, "lblAdditionalInstructions");
            this.lblAdditionalInstructions.Font = null;
            this.lblAdditionalInstructions.Name = "lblAdditionalInstructions";
            // 
            // lblGayaInterfaceName
            // 
            this.lblGayaInterfaceName.AccessibleDescription = null;
            this.lblGayaInterfaceName.AccessibleName = null;
            resources.ApplyResources(this.lblGayaInterfaceName, "lblGayaInterfaceName");
            this.lblGayaInterfaceName.Font = null;
            this.lblGayaInterfaceName.Name = "lblGayaInterfaceName";
            // 
            // tbWebserviceName
            // 
            this.tbWebserviceName.AccessibleDescription = null;
            this.tbWebserviceName.AccessibleName = null;
            resources.ApplyResources(this.tbWebserviceName, "tbWebserviceName");
            this.tbWebserviceName.BackgroundImage = null;
            this.tbWebserviceName.Font = null;
            this.tbWebserviceName.Name = "tbWebserviceName";
            this.tbWebserviceName.ReadOnly = true;
            // 
            // tbChangelog
            // 
            this.tbChangelog.AcceptsReturn = true;
            this.tbChangelog.AccessibleDescription = null;
            this.tbChangelog.AccessibleName = null;
            resources.ApplyResources(this.tbChangelog, "tbChangelog");
            this.tbChangelog.BackgroundImage = null;
            this.tbChangelog.Font = null;
            this.tbChangelog.Name = "tbChangelog";
            this.tbChangelog.ReadOnly = true;
            // 
            // lblChangelog
            // 
            this.lblChangelog.AccessibleDescription = null;
            this.lblChangelog.AccessibleName = null;
            resources.ApplyResources(this.lblChangelog, "lblChangelog");
            this.lblChangelog.Font = null;
            this.lblChangelog.Name = "lblChangelog";
            // 
            // tbItemType
            // 
            this.tbItemType.AccessibleDescription = null;
            this.tbItemType.AccessibleName = null;
            resources.ApplyResources(this.tbItemType, "tbItemType");
            this.tbItemType.BackgroundImage = null;
            this.tbItemType.Name = "tbItemType";
            this.tbItemType.ReadOnly = true;
            // 
            // lblItemType
            // 
            this.lblItemType.AccessibleDescription = null;
            this.lblItemType.AccessibleName = null;
            resources.ApplyResources(this.lblItemType, "lblItemType");
            this.lblItemType.BackColor = System.Drawing.Color.Transparent;
            this.lblItemType.Font = null;
            this.lblItemType.Name = "lblItemType";
            // 
            // lblHomepageStatic
            // 
            this.lblHomepageStatic.AccessibleDescription = null;
            this.lblHomepageStatic.AccessibleName = null;
            resources.ApplyResources(this.lblHomepageStatic, "lblHomepageStatic");
            this.lblHomepageStatic.BackColor = System.Drawing.Color.Transparent;
            this.lblHomepageStatic.Font = null;
            this.lblHomepageStatic.Name = "lblHomepageStatic";
            // 
            // tbHomepageURL
            // 
            this.tbHomepageURL.AccessibleDescription = null;
            this.tbHomepageURL.AccessibleName = null;
            resources.ApplyResources(this.tbHomepageURL, "tbHomepageURL");
            this.tbHomepageURL.BackgroundImage = null;
            this.tbHomepageURL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbHomepageURL.ForeColor = System.Drawing.Color.Blue;
            this.tbHomepageURL.Name = "tbHomepageURL";
            this.tbHomepageURL.ReadOnly = true;
            this.tbHomepageURL.DoubleClick += new System.EventHandler(this.tbHomepageURL_DoubleClick);
            // 
            // lblInstalled
            // 
            this.lblInstalled.AccessibleDescription = null;
            this.lblInstalled.AccessibleName = null;
            resources.ApplyResources(this.lblInstalled, "lblInstalled");
            this.lblInstalled.BackColor = System.Drawing.Color.Transparent;
            this.lblInstalled.Font = null;
            this.lblInstalled.Name = "lblInstalled";
            // 
            // tbInstalledVersion
            // 
            this.tbInstalledVersion.AccessibleDescription = null;
            this.tbInstalledVersion.AccessibleName = null;
            resources.ApplyResources(this.tbInstalledVersion, "tbInstalledVersion");
            this.tbInstalledVersion.BackgroundImage = null;
            this.tbInstalledVersion.Font = null;
            this.tbInstalledVersion.Name = "tbInstalledVersion";
            this.tbInstalledVersion.ReadOnly = true;
            // 
            // tbAuthor
            // 
            this.tbAuthor.AccessibleDescription = null;
            this.tbAuthor.AccessibleName = null;
            resources.ApplyResources(this.tbAuthor, "tbAuthor");
            this.tbAuthor.BackgroundImage = null;
            this.tbAuthor.Font = null;
            this.tbAuthor.Name = "tbAuthor";
            this.tbAuthor.ReadOnly = true;
            // 
            // lblMaintainerStatic
            // 
            this.lblMaintainerStatic.AccessibleDescription = null;
            this.lblMaintainerStatic.AccessibleName = null;
            resources.ApplyResources(this.lblMaintainerStatic, "lblMaintainerStatic");
            this.lblMaintainerStatic.BackColor = System.Drawing.Color.Transparent;
            this.lblMaintainerStatic.Font = null;
            this.lblMaintainerStatic.Name = "lblMaintainerStatic";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AccessibleDescription = null;
            this.lblAuthor.AccessibleName = null;
            resources.ApplyResources(this.lblAuthor, "lblAuthor");
            this.lblAuthor.BackColor = System.Drawing.Color.Transparent;
            this.lblAuthor.Font = null;
            this.lblAuthor.Name = "lblAuthor";
            // 
            // tbMaintainer
            // 
            this.tbMaintainer.AccessibleDescription = null;
            this.tbMaintainer.AccessibleName = null;
            resources.ApplyResources(this.tbMaintainer, "tbMaintainer");
            this.tbMaintainer.BackgroundImage = null;
            this.tbMaintainer.Font = null;
            this.tbMaintainer.Name = "tbMaintainer";
            this.tbMaintainer.ReadOnly = true;
            // 
            // pnlActionButtons
            // 
            this.pnlActionButtons.AccessibleDescription = null;
            this.pnlActionButtons.AccessibleName = null;
            resources.ApplyResources(this.pnlActionButtons, "pnlActionButtons");
            this.pnlActionButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.pnlActionButtons.BackgroundImage = null;
            this.pnlActionButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlActionButtons.Controls.Add(this.btnStartApplication);
            this.pnlActionButtons.Controls.Add(this.cbxStartAppOnBoot);
            this.pnlActionButtons.Controls.Add(this.btnUninstall);
            this.pnlActionButtons.Controls.Add(this.btnRestart);
            this.pnlActionButtons.Font = null;
            this.pnlActionButtons.Name = "pnlActionButtons";
            // 
            // btnStartApplication
            // 
            this.btnStartApplication.AccessibleDescription = null;
            this.btnStartApplication.AccessibleName = null;
            resources.ApplyResources(this.btnStartApplication, "btnStartApplication");
            this.btnStartApplication.BackgroundImage = null;
            this.btnStartApplication.Font = null;
            this.btnStartApplication.Name = "btnStartApplication";
            this.btnStartApplication.UseVisualStyleBackColor = true;
            this.btnStartApplication.Click += new System.EventHandler(this.btnStartApplication_Click);
            // 
            // cbxStartAppOnBoot
            // 
            this.cbxStartAppOnBoot.AccessibleDescription = null;
            this.cbxStartAppOnBoot.AccessibleName = null;
            resources.ApplyResources(this.cbxStartAppOnBoot, "cbxStartAppOnBoot");
            this.cbxStartAppOnBoot.BackgroundImage = null;
            this.cbxStartAppOnBoot.Font = null;
            this.cbxStartAppOnBoot.Name = "cbxStartAppOnBoot";
            this.cbxStartAppOnBoot.UseVisualStyleBackColor = true;
            this.cbxStartAppOnBoot.CheckedChanged += new System.EventHandler(this.cbxStartAppOnBoot_CheckedChanged);
            // 
            // btnUninstall
            // 
            this.btnUninstall.AccessibleDescription = null;
            this.btnUninstall.AccessibleName = null;
            resources.ApplyResources(this.btnUninstall, "btnUninstall");
            this.btnUninstall.BackgroundImage = null;
            this.btnUninstall.Font = null;
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // btnRestart
            // 
            this.btnRestart.AccessibleDescription = null;
            this.btnRestart.AccessibleName = null;
            resources.ApplyResources(this.btnRestart, "btnRestart");
            this.btnRestart.BackgroundImage = null;
            this.btnRestart.Font = null;
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.UseVisualStyleBackColor = true;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // pnlSelectApplication
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.pbLoadingScreenshots);
            this.Controls.Add(this.flowScreenshots);
            this.Controls.Add(this.tabSelectCategory);
            this.Controls.Add(this.lblSelectApplication);
            this.Controls.Add(this.pnlTabContent);
            this.Font = null;
            this.Name = "pnlSelectApplication";
            this.VisibleChanged += new System.EventHandler(this.pnlSelectApplication_VisibleChanged);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.pnlTabContent, 0);
            this.Controls.SetChildIndex(this.lblSelectApplication, 0);
            this.Controls.SetChildIndex(this.tabSelectCategory, 0);
            this.Controls.SetChildIndex(this.flowScreenshots, 0);
            this.Controls.SetChildIndex(this.pbLoadingScreenshots, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabSelectCategory.ResumeLayout(false);
            this.pnlTabContent.ResumeLayout(false);
            this.pnlItemDetails.ResumeLayout(false);
            this.pnlItemDetails.PerformLayout();
            this.gbxUsageInstructions.ResumeLayout(false);
            this.gbxUsageInstructions.PerformLayout();
            this.pnlActionButtons.ResumeLayout(false);
            this.pnlActionButtons.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectApplication;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.TabControl tabSelectCategory;
        private System.Windows.Forms.TabPage tabApplications;
        private System.Windows.Forms.TabPage tabThemes;
        private System.Windows.Forms.TabPage tabCustomMenus;
        private System.Windows.Forms.Label lblThemeFormat;
        private System.Windows.Forms.ComboBox cbxThemeFormat;
        private System.Windows.Forms.FlowLayoutPanel flowScreenshots;
        private System.Windows.Forms.ProgressBar pbLoadingScreenshots;
        private System.Windows.Forms.TabPage tabWaitImages;
        private System.Windows.Forms.TabPage tabWebservices;
        private System.Windows.Forms.Panel pnlTabContent;
        private System.Windows.Forms.TabPage tabNew;
        private System.Windows.Forms.TabPage tabUpdates;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Label lblInstalled;
        private System.Windows.Forms.TextBox tbInstalledVersion;
        private System.Windows.Forms.TextBox tbAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.TextBox tbMaintainer;
        private System.Windows.Forms.Label lblMaintainerStatic;
        private System.Windows.Forms.TextBox tbHomepageURL;
        private System.Windows.Forms.Label lblHomepageStatic;
        private System.Windows.Forms.TextBox tbItemType;
        private System.Windows.Forms.Label lblItemType;
        private System.Windows.Forms.TabPage tabInstalled;
        private System.Windows.Forms.Button btnStartApplication;
        private System.Windows.Forms.CheckBox cbxStartAppOnBoot;
        private System.Windows.Forms.Button btnRestart;
        private System.Windows.Forms.Panel pnlItemDetails;
        private System.Windows.Forms.Label lblWebInterfaceUrl;
        private System.Windows.Forms.TextBox tbChangelog;
        private System.Windows.Forms.Label lblChangelog;
        private System.Windows.Forms.LinkLabel llWebbrowserInterface;
        private System.Windows.Forms.Label lblGayaInterfaceName;
        private System.Windows.Forms.TextBox tbWebserviceName;
        private System.Windows.Forms.TextBox tbAdditionalUsageInstrictions;
        private System.Windows.Forms.Label lblAdditionalInstructions;
        private System.Windows.Forms.GroupBox gbxUsageInstructions;
        private System.Windows.Forms.TextBox tbForumTopic;
        private System.Windows.Forms.Label lblForumTopic;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Panel pnlActionButtons;
        private System.Windows.Forms.ImageList ilIcons;
        private System.Windows.Forms.ListView lvSelectItem;
        private System.Windows.Forms.ColumnHeader clmHeader;
    }
}
