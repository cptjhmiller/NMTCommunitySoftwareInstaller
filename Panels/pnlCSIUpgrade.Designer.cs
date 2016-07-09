namespace com.nmtinstaller.csi.Panels
{
    partial class pnlCSIUpgrade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlCSIUpgrade));
            this.rtbChangelog = new System.Windows.Forms.RichTextBox();
            this.lblNewUpdateAvailable = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.lblAvailableVersion = new System.Windows.Forms.Label();
            this.tbCurrentVersion = new System.Windows.Forms.TextBox();
            this.tbAvailableVersion = new System.Windows.Forms.TextBox();
            this.cbxDoNotUpdate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // rtbChangelog
            // 
            this.rtbChangelog.AccessibleDescription = null;
            this.rtbChangelog.AccessibleName = null;
            resources.ApplyResources(this.rtbChangelog, "rtbChangelog");
            this.rtbChangelog.BackgroundImage = null;
            this.rtbChangelog.Name = "rtbChangelog";
            this.rtbChangelog.ReadOnly = true;
            this.rtbChangelog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbChangelog_LinkClicked);
            this.rtbChangelog.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbChangelog_KeyUp);
            // 
            // lblNewUpdateAvailable
            // 
            this.lblNewUpdateAvailable.AccessibleDescription = null;
            this.lblNewUpdateAvailable.AccessibleName = null;
            resources.ApplyResources(this.lblNewUpdateAvailable, "lblNewUpdateAvailable");
            this.lblNewUpdateAvailable.Font = null;
            this.lblNewUpdateAvailable.Name = "lblNewUpdateAvailable";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AccessibleDescription = null;
            this.lblCurrentVersion.AccessibleName = null;
            resources.ApplyResources(this.lblCurrentVersion, "lblCurrentVersion");
            this.lblCurrentVersion.Font = null;
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            // 
            // lblAvailableVersion
            // 
            this.lblAvailableVersion.AccessibleDescription = null;
            this.lblAvailableVersion.AccessibleName = null;
            resources.ApplyResources(this.lblAvailableVersion, "lblAvailableVersion");
            this.lblAvailableVersion.Name = "lblAvailableVersion";
            // 
            // tbCurrentVersion
            // 
            this.tbCurrentVersion.AccessibleDescription = null;
            this.tbCurrentVersion.AccessibleName = null;
            resources.ApplyResources(this.tbCurrentVersion, "tbCurrentVersion");
            this.tbCurrentVersion.BackgroundImage = null;
            this.tbCurrentVersion.Font = null;
            this.tbCurrentVersion.Name = "tbCurrentVersion";
            this.tbCurrentVersion.ReadOnly = true;
            // 
            // tbAvailableVersion
            // 
            this.tbAvailableVersion.AccessibleDescription = null;
            this.tbAvailableVersion.AccessibleName = null;
            resources.ApplyResources(this.tbAvailableVersion, "tbAvailableVersion");
            this.tbAvailableVersion.BackgroundImage = null;
            this.tbAvailableVersion.Name = "tbAvailableVersion";
            this.tbAvailableVersion.ReadOnly = true;
            // 
            // cbxDoNotUpdate
            // 
            this.cbxDoNotUpdate.AccessibleDescription = null;
            this.cbxDoNotUpdate.AccessibleName = null;
            resources.ApplyResources(this.cbxDoNotUpdate, "cbxDoNotUpdate");
            this.cbxDoNotUpdate.BackgroundImage = null;
            this.cbxDoNotUpdate.Font = null;
            this.cbxDoNotUpdate.Name = "cbxDoNotUpdate";
            this.cbxDoNotUpdate.UseVisualStyleBackColor = true;
            // 
            // pnlCSIUpgrade
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.cbxDoNotUpdate);
            this.Controls.Add(this.tbAvailableVersion);
            this.Controls.Add(this.tbCurrentVersion);
            this.Controls.Add(this.lblAvailableVersion);
            this.Controls.Add(this.lblNewUpdateAvailable);
            this.Controls.Add(this.rtbChangelog);
            this.Controls.Add(this.lblCurrentVersion);
            this.Font = null;
            this.Name = "pnlCSIUpgrade";
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblCurrentVersion, 0);
            this.Controls.SetChildIndex(this.rtbChangelog, 0);
            this.Controls.SetChildIndex(this.lblNewUpdateAvailable, 0);
            this.Controls.SetChildIndex(this.lblAvailableVersion, 0);
            this.Controls.SetChildIndex(this.tbCurrentVersion, 0);
            this.Controls.SetChildIndex(this.tbAvailableVersion, 0);
            this.Controls.SetChildIndex(this.cbxDoNotUpdate, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChangelog;
        private System.Windows.Forms.Label lblNewUpdateAvailable;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label lblAvailableVersion;
        private System.Windows.Forms.TextBox tbCurrentVersion;
        private System.Windows.Forms.TextBox tbAvailableVersion;
        private System.Windows.Forms.CheckBox cbxDoNotUpdate;
    }
}
