namespace com.nmtinstaller.csi.Panels
{
    partial class pnlUploadApplication
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlUploadApplication));
            this.lblUploadConfirm = new System.Windows.Forms.Label();
            this.lblSelectedApplication = new System.Windows.Forms.Label();
            this.lblSelectedServer = new System.Windows.Forms.Label();
            this.txtApplication = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.gbxThemeBackgroundSelect = new System.Windows.Forms.GroupBox();
            this.lblBackgroundSelection = new System.Windows.Forms.Label();
            this.pictBackgroundPreview = new System.Windows.Forms.PictureBox();
            this.lbBackgrounds = new System.Windows.Forms.ListBox();
            this.gbxToBeInstalledItem = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbxThemeBackgroundSelect.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackgroundPreview)).BeginInit();
            this.gbxToBeInstalledItem.SuspendLayout();
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
            // lblUploadConfirm
            // 
            this.lblUploadConfirm.AccessibleDescription = null;
            this.lblUploadConfirm.AccessibleName = null;
            resources.ApplyResources(this.lblUploadConfirm, "lblUploadConfirm");
            this.lblUploadConfirm.Font = null;
            this.lblUploadConfirm.Name = "lblUploadConfirm";
            // 
            // lblSelectedApplication
            // 
            this.lblSelectedApplication.AccessibleDescription = null;
            this.lblSelectedApplication.AccessibleName = null;
            resources.ApplyResources(this.lblSelectedApplication, "lblSelectedApplication");
            this.lblSelectedApplication.Font = null;
            this.lblSelectedApplication.Name = "lblSelectedApplication";
            this.lblSelectedApplication.Click += new System.EventHandler(this.lblSelectedApplication_Click);
            // 
            // lblSelectedServer
            // 
            this.lblSelectedServer.AccessibleDescription = null;
            this.lblSelectedServer.AccessibleName = null;
            resources.ApplyResources(this.lblSelectedServer, "lblSelectedServer");
            this.lblSelectedServer.Font = null;
            this.lblSelectedServer.Name = "lblSelectedServer";
            // 
            // txtApplication
            // 
            this.txtApplication.AccessibleDescription = null;
            this.txtApplication.AccessibleName = null;
            resources.ApplyResources(this.txtApplication, "txtApplication");
            this.txtApplication.BackgroundImage = null;
            this.txtApplication.Font = null;
            this.txtApplication.Name = "txtApplication";
            this.txtApplication.ReadOnly = true;
            // 
            // txtServer
            // 
            this.txtServer.AccessibleDescription = null;
            this.txtServer.AccessibleName = null;
            resources.ApplyResources(this.txtServer, "txtServer");
            this.txtServer.BackgroundImage = null;
            this.txtServer.Font = null;
            this.txtServer.Name = "txtServer";
            this.txtServer.ReadOnly = true;
            // 
            // gbxThemeBackgroundSelect
            // 
            this.gbxThemeBackgroundSelect.AccessibleDescription = null;
            this.gbxThemeBackgroundSelect.AccessibleName = null;
            resources.ApplyResources(this.gbxThemeBackgroundSelect, "gbxThemeBackgroundSelect");
            this.gbxThemeBackgroundSelect.BackgroundImage = null;
            this.gbxThemeBackgroundSelect.Controls.Add(this.lblBackgroundSelection);
            this.gbxThemeBackgroundSelect.Controls.Add(this.pictBackgroundPreview);
            this.gbxThemeBackgroundSelect.Controls.Add(this.lbBackgrounds);
            this.gbxThemeBackgroundSelect.Name = "gbxThemeBackgroundSelect";
            this.gbxThemeBackgroundSelect.TabStop = false;
            // 
            // lblBackgroundSelection
            // 
            this.lblBackgroundSelection.AccessibleDescription = null;
            this.lblBackgroundSelection.AccessibleName = null;
            resources.ApplyResources(this.lblBackgroundSelection, "lblBackgroundSelection");
            this.lblBackgroundSelection.Name = "lblBackgroundSelection";
            // 
            // pictBackgroundPreview
            // 
            this.pictBackgroundPreview.AccessibleDescription = null;
            this.pictBackgroundPreview.AccessibleName = null;
            resources.ApplyResources(this.pictBackgroundPreview, "pictBackgroundPreview");
            this.pictBackgroundPreview.BackgroundImage = null;
            this.pictBackgroundPreview.Font = null;
            this.pictBackgroundPreview.ImageLocation = null;
            this.pictBackgroundPreview.Name = "pictBackgroundPreview";
            this.pictBackgroundPreview.TabStop = false;
            // 
            // lbBackgrounds
            // 
            this.lbBackgrounds.AccessibleDescription = null;
            this.lbBackgrounds.AccessibleName = null;
            resources.ApplyResources(this.lbBackgrounds, "lbBackgrounds");
            this.lbBackgrounds.BackgroundImage = null;
            this.lbBackgrounds.FormattingEnabled = true;
            this.lbBackgrounds.Name = "lbBackgrounds";
            this.lbBackgrounds.SelectedIndexChanged += new System.EventHandler(this.lbBackgrounds_SelectedIndexChanged);
            // 
            // gbxToBeInstalledItem
            // 
            this.gbxToBeInstalledItem.AccessibleDescription = null;
            this.gbxToBeInstalledItem.AccessibleName = null;
            resources.ApplyResources(this.gbxToBeInstalledItem, "gbxToBeInstalledItem");
            this.gbxToBeInstalledItem.BackgroundImage = null;
            this.gbxToBeInstalledItem.Controls.Add(this.txtApplication);
            this.gbxToBeInstalledItem.Controls.Add(this.lblSelectedApplication);
            this.gbxToBeInstalledItem.Controls.Add(this.txtServer);
            this.gbxToBeInstalledItem.Controls.Add(this.lblSelectedServer);
            this.gbxToBeInstalledItem.Font = null;
            this.gbxToBeInstalledItem.Name = "gbxToBeInstalledItem";
            this.gbxToBeInstalledItem.TabStop = false;
            // 
            // pnlUploadApplication
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.gbxToBeInstalledItem);
            this.Controls.Add(this.gbxThemeBackgroundSelect);
            this.Controls.Add(this.lblUploadConfirm);
            this.Font = null;
            this.Name = "pnlUploadApplication";
            this.VisibleChanged += new System.EventHandler(this.pnlUploadApplication_VisibleChanged);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblUploadConfirm, 0);
            this.Controls.SetChildIndex(this.gbxThemeBackgroundSelect, 0);
            this.Controls.SetChildIndex(this.gbxToBeInstalledItem, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbxThemeBackgroundSelect.ResumeLayout(false);
            this.gbxThemeBackgroundSelect.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBackgroundPreview)).EndInit();
            this.gbxToBeInstalledItem.ResumeLayout(false);
            this.gbxToBeInstalledItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUploadConfirm;
        private System.Windows.Forms.Label lblSelectedApplication;
        private System.Windows.Forms.Label lblSelectedServer;
        private System.Windows.Forms.TextBox txtApplication;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.GroupBox gbxThemeBackgroundSelect;
        private System.Windows.Forms.PictureBox pictBackgroundPreview;
        private System.Windows.Forms.ListBox lbBackgrounds;
        private System.Windows.Forms.Label lblBackgroundSelection;
        private System.Windows.Forms.GroupBox gbxToBeInstalledItem;
    }
}
