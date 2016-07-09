namespace com.nmtinstaller.csi.Panels
{
    partial class pnlServerSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlServerSettings));
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtServerName = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblHardwareType = new System.Windows.Forms.Label();
            this.cbxHardwareType = new System.Windows.Forms.ComboBox();
            this.gbxFtpserverSettings = new System.Windows.Forms.GroupBox();
            this.gbxHardwareSettings = new System.Windows.Forms.GroupBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbxFtpserverSettings.SuspendLayout();
            this.gbxHardwareSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // txtUserName
            // 
            resources.ApplyResources(this.txtUserName, "txtUserName");
            this.txtUserName.Name = "txtUserName";
            // 
            // txtServerName
            // 
            resources.ApplyResources(this.txtServerName, "txtServerName");
            this.txtServerName.Name = "txtServerName";
            // 
            // lblServer
            // 
            resources.ApplyResources(this.lblServer, "lblServer");
            this.lblServer.Name = "lblServer";
            // 
            // lblUserName
            // 
            resources.ApplyResources(this.lblUserName, "lblUserName");
            this.lblUserName.Name = "lblUserName";
            // 
            // lblPassword
            // 
            resources.ApplyResources(this.lblPassword, "lblPassword");
            this.lblPassword.Name = "lblPassword";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // lblHardwareType
            // 
            resources.ApplyResources(this.lblHardwareType, "lblHardwareType");
            this.lblHardwareType.Name = "lblHardwareType";
            // 
            // cbxHardwareType
            // 
            this.cbxHardwareType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxHardwareType.FormattingEnabled = true;
            this.cbxHardwareType.Items.AddRange(new object[] {
            resources.GetString("cbxHardwareType.Items"),
            resources.GetString("cbxHardwareType.Items1"),
            resources.GetString("cbxHardwareType.Items2"),
            resources.GetString("cbxHardwareType.Items3"),
            resources.GetString("cbxHardwareType.Items4"),
            resources.GetString("cbxHardwareType.Items5"),
            resources.GetString("cbxHardwareType.Items6"),
            resources.GetString("cbxHardwareType.Items7"),
            resources.GetString("cbxHardwareType.Items8"),
            resources.GetString("cbxHardwareType.Items9"),
            resources.GetString("cbxHardwareType.Items10"),
            resources.GetString("cbxHardwareType.Items11"),
            resources.GetString("cbxHardwareType.Items12"),
            resources.GetString("cbxHardwareType.Items13"),
            resources.GetString("cbxHardwareType.Items14"),
            resources.GetString("cbxHardwareType.Items15"),
            resources.GetString("cbxHardwareType.Items16"),
            resources.GetString("cbxHardwareType.Items17"),
            resources.GetString("cbxHardwareType.Items18"),
            resources.GetString("cbxHardwareType.Items19"),
            resources.GetString("cbxHardwareType.Items20"),
            resources.GetString("cbxHardwareType.Items21"),
            resources.GetString("cbxHardwareType.Items22"),
            resources.GetString("cbxHardwareType.Items23"),
            resources.GetString("cbxHardwareType.Items24"),
            resources.GetString("cbxHardwareType.Items25"),
            resources.GetString("cbxHardwareType.Items26"),
            resources.GetString("cbxHardwareType.Items27"),
            resources.GetString("cbxHardwareType.Items28"),
            resources.GetString("cbxHardwareType.Items29"),
            resources.GetString("cbxHardwareType.Items30"),
            resources.GetString("cbxHardwareType.Items31"),
            resources.GetString("cbxHardwareType.Items32"),
            resources.GetString("cbxHardwareType.Items33")});
            resources.ApplyResources(this.cbxHardwareType, "cbxHardwareType");
            this.cbxHardwareType.Name = "cbxHardwareType";
            this.cbxHardwareType.SelectedIndexChanged += new System.EventHandler(this.cbxHardwareType_SelectedIndexChanged);
            // 
            // gbxFtpserverSettings
            // 
            this.gbxFtpserverSettings.Controls.Add(this.txtServerName);
            this.gbxFtpserverSettings.Controls.Add(this.txtUserName);
            this.gbxFtpserverSettings.Controls.Add(this.txtPassword);
            this.gbxFtpserverSettings.Controls.Add(this.lblServer);
            this.gbxFtpserverSettings.Controls.Add(this.lblPassword);
            this.gbxFtpserverSettings.Controls.Add(this.lblUserName);
            resources.ApplyResources(this.gbxFtpserverSettings, "gbxFtpserverSettings");
            this.gbxFtpserverSettings.Name = "gbxFtpserverSettings";
            this.gbxFtpserverSettings.TabStop = false;
            // 
            // gbxHardwareSettings
            // 
            this.gbxHardwareSettings.Controls.Add(this.cbxHardwareType);
            this.gbxHardwareSettings.Controls.Add(this.lblHardwareType);
            resources.ApplyResources(this.gbxHardwareSettings, "gbxHardwareSettings");
            this.gbxHardwareSettings.Name = "gbxHardwareSettings";
            this.gbxHardwareSettings.TabStop = false;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // pnlServerSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxHardwareSettings);
            this.Controls.Add(this.gbxFtpserverSettings);
            this.Controls.Add(this.lblDescription);
            this.Name = "pnlServerSettings";
            this.VisibleChanged += new System.EventHandler(this.pnlServerSettings_VisibleChanged);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.gbxFtpserverSettings, 0);
            this.Controls.SetChildIndex(this.gbxHardwareSettings, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbxFtpserverSettings.ResumeLayout(false);
            this.gbxFtpserverSettings.PerformLayout();
            this.gbxHardwareSettings.ResumeLayout(false);
            this.gbxHardwareSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtServerName;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblHardwareType;
        private System.Windows.Forms.ComboBox cbxHardwareType;
        private System.Windows.Forms.GroupBox gbxFtpserverSettings;
        private System.Windows.Forms.GroupBox gbxHardwareSettings;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}
