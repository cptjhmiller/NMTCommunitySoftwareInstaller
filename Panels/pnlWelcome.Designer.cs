namespace com.nmtinstaller.csi.Panels
{
    partial class pnlWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlWelcome));
            this.lblDescription = new System.Windows.Forms.Label();
            this.gbxRepositoryInfo = new System.Windows.Forms.GroupBox();
            this.tbNumberWebservices = new System.Windows.Forms.TextBox();
            this.lblWebservices = new System.Windows.Forms.Label();
            this.tbNumberWaitImageSets = new System.Windows.Forms.TextBox();
            this.lblWaitImages = new System.Windows.Forms.Label();
            this.tbRepositoryDate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbNumberCustomMenus = new System.Windows.Forms.TextBox();
            this.lblCustomMenus = new System.Windows.Forms.Label();
            this.tbNumberThemes = new System.Windows.Forms.TextBox();
            this.tbNumberApplications = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picDonate = new System.Windows.Forms.PictureBox();
            this.gbxUpdateRepository = new System.Windows.Forms.GroupBox();
            this.lblUpdatingRepositoryName = new System.Windows.Forms.Label();
            this.pbRepositoryUpdate = new System.Windows.Forms.ProgressBar();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblNZBVortex = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbxRepositoryInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).BeginInit();
            this.gbxUpdateRepository.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // gbxRepositoryInfo
            // 
            resources.ApplyResources(this.gbxRepositoryInfo, "gbxRepositoryInfo");
            this.gbxRepositoryInfo.Controls.Add(this.tbNumberWebservices);
            this.gbxRepositoryInfo.Controls.Add(this.lblWebservices);
            this.gbxRepositoryInfo.Controls.Add(this.tbNumberWaitImageSets);
            this.gbxRepositoryInfo.Controls.Add(this.lblWaitImages);
            this.gbxRepositoryInfo.Controls.Add(this.tbRepositoryDate);
            this.gbxRepositoryInfo.Controls.Add(this.label4);
            this.gbxRepositoryInfo.Controls.Add(this.tbNumberCustomMenus);
            this.gbxRepositoryInfo.Controls.Add(this.lblCustomMenus);
            this.gbxRepositoryInfo.Controls.Add(this.tbNumberThemes);
            this.gbxRepositoryInfo.Controls.Add(this.tbNumberApplications);
            this.gbxRepositoryInfo.Controls.Add(this.label2);
            this.gbxRepositoryInfo.Controls.Add(this.label1);
            this.gbxRepositoryInfo.Name = "gbxRepositoryInfo";
            this.gbxRepositoryInfo.TabStop = false;
            // 
            // tbNumberWebservices
            // 
            resources.ApplyResources(this.tbNumberWebservices, "tbNumberWebservices");
            this.tbNumberWebservices.Name = "tbNumberWebservices";
            this.tbNumberWebservices.ReadOnly = true;
            // 
            // lblWebservices
            // 
            resources.ApplyResources(this.lblWebservices, "lblWebservices");
            this.lblWebservices.Name = "lblWebservices";
            this.lblWebservices.Click += new System.EventHandler(this.lblWebservices_Click);
            // 
            // tbNumberWaitImageSets
            // 
            resources.ApplyResources(this.tbNumberWaitImageSets, "tbNumberWaitImageSets");
            this.tbNumberWaitImageSets.Name = "tbNumberWaitImageSets";
            this.tbNumberWaitImageSets.ReadOnly = true;
            // 
            // lblWaitImages
            // 
            resources.ApplyResources(this.lblWaitImages, "lblWaitImages");
            this.lblWaitImages.Name = "lblWaitImages";
            // 
            // tbRepositoryDate
            // 
            resources.ApplyResources(this.tbRepositoryDate, "tbRepositoryDate");
            this.tbRepositoryDate.Name = "tbRepositoryDate";
            this.tbRepositoryDate.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // tbNumberCustomMenus
            // 
            resources.ApplyResources(this.tbNumberCustomMenus, "tbNumberCustomMenus");
            this.tbNumberCustomMenus.Name = "tbNumberCustomMenus";
            this.tbNumberCustomMenus.ReadOnly = true;
            // 
            // lblCustomMenus
            // 
            resources.ApplyResources(this.lblCustomMenus, "lblCustomMenus");
            this.lblCustomMenus.Name = "lblCustomMenus";
            // 
            // tbNumberThemes
            // 
            resources.ApplyResources(this.tbNumberThemes, "tbNumberThemes");
            this.tbNumberThemes.Name = "tbNumberThemes";
            this.tbNumberThemes.ReadOnly = true;
            // 
            // tbNumberApplications
            // 
            resources.ApplyResources(this.tbNumberApplications, "tbNumberApplications");
            this.tbNumberApplications.Name = "tbNumberApplications";
            this.tbNumberApplications.ReadOnly = true;
            this.tbNumberApplications.TextChanged += new System.EventHandler(this.tbNumberApplications_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // picDonate
            // 
            this.picDonate.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDonate.Image = global::com.nmtinstaller.csi.Properties.Resources.PaypalDonate;
            resources.ApplyResources(this.picDonate, "picDonate");
            this.picDonate.Name = "picDonate";
            this.picDonate.TabStop = false;
            this.picDonate.Click += new System.EventHandler(this.picDonate_Click);
            // 
            // gbxUpdateRepository
            // 
            this.gbxUpdateRepository.Controls.Add(this.lblUpdatingRepositoryName);
            this.gbxUpdateRepository.Controls.Add(this.pbRepositoryUpdate);
            resources.ApplyResources(this.gbxUpdateRepository, "gbxUpdateRepository");
            this.gbxUpdateRepository.Name = "gbxUpdateRepository";
            this.gbxUpdateRepository.TabStop = false;
            // 
            // lblUpdatingRepositoryName
            // 
            resources.ApplyResources(this.lblUpdatingRepositoryName, "lblUpdatingRepositoryName");
            this.lblUpdatingRepositoryName.Name = "lblUpdatingRepositoryName";
            // 
            // pbRepositoryUpdate
            // 
            resources.ApplyResources(this.pbRepositoryUpdate, "pbRepositoryUpdate");
            this.pbRepositoryUpdate.Name = "pbRepositoryUpdate";
            this.pbRepositoryUpdate.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Image = global::com.nmtinstaller.csi.Properties.Resources.NZBVortex;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.lblNZBVortex_Click);
            // 
            // lblNZBVortex
            // 
            resources.ApplyResources(this.lblNZBVortex, "lblNZBVortex");
            this.lblNZBVortex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblNZBVortex.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.lblNZBVortex.Name = "lblNZBVortex";
            this.lblNZBVortex.Click += new System.EventHandler(this.lblNZBVortex_Click);
            // 
            // pnlWelcome
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblNZBVortex);
            this.Controls.Add(this.gbxUpdateRepository);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.picDonate);
            this.Controls.Add(this.gbxRepositoryInfo);
            this.Controls.Add(this.lblDescription);
            this.Name = "pnlWelcome";
            this.VisibleChanged += new System.EventHandler(this.pnlWelcome_VisibleChanged);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.gbxRepositoryInfo, 0);
            this.Controls.SetChildIndex(this.picDonate, 0);
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            this.Controls.SetChildIndex(this.gbxUpdateRepository, 0);
            this.Controls.SetChildIndex(this.lblNZBVortex, 0);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbxRepositoryInfo.ResumeLayout(false);
            this.gbxRepositoryInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDonate)).EndInit();
            this.gbxUpdateRepository.ResumeLayout(false);
            this.gbxUpdateRepository.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.GroupBox gbxRepositoryInfo;
        private System.Windows.Forms.TextBox tbNumberThemes;
        private System.Windows.Forms.TextBox tbNumberApplications;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNumberCustomMenus;
        private System.Windows.Forms.Label lblCustomMenus;
        private System.Windows.Forms.TextBox tbRepositoryDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbNumberWaitImageSets;
        private System.Windows.Forms.Label lblWaitImages;
        private System.Windows.Forms.TextBox tbNumberWebservices;
        private System.Windows.Forms.Label lblWebservices;
        private System.Windows.Forms.PictureBox picDonate;
        private System.Windows.Forms.GroupBox gbxUpdateRepository;
        private System.Windows.Forms.Label lblUpdatingRepositoryName;
        private System.Windows.Forms.ProgressBar pbRepositoryUpdate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblNZBVortex;
    }
}
