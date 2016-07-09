using System;
using System.Collections.Generic;
using System.Text;

namespace com.nmtinstaller.csi.Panels
{
    partial class pnlInstallFromZIP
    {
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlInstallFromZIP));
            this.lblOpenZipFile = new System.Windows.Forms.Label();
            this.tbZipFile = new System.Windows.Forms.TextBox();
            this.btnOpenZip = new System.Windows.Forms.Button();
            this.gbxApplication = new System.Windows.Forms.GroupBox();
            this.tbInstallScriptParameters = new System.Windows.Forms.TextBox();
            this.lblInstallScriptParameters = new System.Windows.Forms.Label();
            this.lblInstallerScript = new System.Windows.Forms.Label();
            this.cbxInstallScript = new System.Windows.Forms.ComboBox();
            this.gbxTheme = new System.Windows.Forms.GroupBox();
            this.lblThemeFormat = new System.Windows.Forms.Label();
            this.cbxThemeFormat = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.gbxApplication.SuspendLayout();
            this.gbxTheme.SuspendLayout();
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
            // lblOpenZipFile
            // 
            this.lblOpenZipFile.AccessibleDescription = null;
            this.lblOpenZipFile.AccessibleName = null;
            resources.ApplyResources(this.lblOpenZipFile, "lblOpenZipFile");
            this.lblOpenZipFile.Font = null;
            this.lblOpenZipFile.Name = "lblOpenZipFile";
            // 
            // tbZipFile
            // 
            this.tbZipFile.AccessibleDescription = null;
            this.tbZipFile.AccessibleName = null;
            resources.ApplyResources(this.tbZipFile, "tbZipFile");
            this.tbZipFile.BackgroundImage = null;
            this.tbZipFile.Font = null;
            this.tbZipFile.Name = "tbZipFile";
            this.tbZipFile.ReadOnly = true;
            // 
            // btnOpenZip
            // 
            this.btnOpenZip.AccessibleDescription = null;
            this.btnOpenZip.AccessibleName = null;
            resources.ApplyResources(this.btnOpenZip, "btnOpenZip");
            this.btnOpenZip.BackgroundImage = null;
            this.btnOpenZip.Font = null;
            this.btnOpenZip.Name = "btnOpenZip";
            this.btnOpenZip.UseVisualStyleBackColor = true;
            this.btnOpenZip.Click += new System.EventHandler(this.btnOpenZip_Click);
            // 
            // gbxApplication
            // 
            this.gbxApplication.AccessibleDescription = null;
            this.gbxApplication.AccessibleName = null;
            resources.ApplyResources(this.gbxApplication, "gbxApplication");
            this.gbxApplication.BackgroundImage = null;
            this.gbxApplication.Controls.Add(this.tbInstallScriptParameters);
            this.gbxApplication.Controls.Add(this.lblInstallScriptParameters);
            this.gbxApplication.Controls.Add(this.lblInstallerScript);
            this.gbxApplication.Controls.Add(this.cbxInstallScript);
            this.gbxApplication.Font = null;
            this.gbxApplication.Name = "gbxApplication";
            this.gbxApplication.TabStop = false;
            // 
            // tbInstallScriptParameters
            // 
            this.tbInstallScriptParameters.AccessibleDescription = null;
            this.tbInstallScriptParameters.AccessibleName = null;
            resources.ApplyResources(this.tbInstallScriptParameters, "tbInstallScriptParameters");
            this.tbInstallScriptParameters.BackgroundImage = null;
            this.tbInstallScriptParameters.Font = null;
            this.tbInstallScriptParameters.Name = "tbInstallScriptParameters";
            // 
            // lblInstallScriptParameters
            // 
            this.lblInstallScriptParameters.AccessibleDescription = null;
            this.lblInstallScriptParameters.AccessibleName = null;
            resources.ApplyResources(this.lblInstallScriptParameters, "lblInstallScriptParameters");
            this.lblInstallScriptParameters.Font = null;
            this.lblInstallScriptParameters.Name = "lblInstallScriptParameters";
            // 
            // lblInstallerScript
            // 
            this.lblInstallerScript.AccessibleDescription = null;
            this.lblInstallerScript.AccessibleName = null;
            resources.ApplyResources(this.lblInstallerScript, "lblInstallerScript");
            this.lblInstallerScript.Font = null;
            this.lblInstallerScript.Name = "lblInstallerScript";
            // 
            // cbxInstallScript
            // 
            this.cbxInstallScript.AccessibleDescription = null;
            this.cbxInstallScript.AccessibleName = null;
            resources.ApplyResources(this.cbxInstallScript, "cbxInstallScript");
            this.cbxInstallScript.BackgroundImage = null;
            this.cbxInstallScript.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxInstallScript.Font = null;
            this.cbxInstallScript.FormattingEnabled = true;
            this.cbxInstallScript.Name = "cbxInstallScript";
            // 
            // gbxTheme
            // 
            this.gbxTheme.AccessibleDescription = null;
            this.gbxTheme.AccessibleName = null;
            resources.ApplyResources(this.gbxTheme, "gbxTheme");
            this.gbxTheme.BackgroundImage = null;
            this.gbxTheme.Controls.Add(this.lblThemeFormat);
            this.gbxTheme.Controls.Add(this.cbxThemeFormat);
            this.gbxTheme.Font = null;
            this.gbxTheme.Name = "gbxTheme";
            this.gbxTheme.TabStop = false;
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
            // 
            // pnlInstallFromZIP
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.gbxTheme);
            this.Controls.Add(this.gbxApplication);
            this.Controls.Add(this.lblOpenZipFile);
            this.Controls.Add(this.tbZipFile);
            this.Controls.Add(this.btnOpenZip);
            this.Font = null;
            this.Name = "pnlInstallFromZIP";
            this.VisibleChanged += new System.EventHandler(this.pnlInstallFromZIP_VisibleChanged);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.btnOpenZip, 0);
            this.Controls.SetChildIndex(this.tbZipFile, 0);
            this.Controls.SetChildIndex(this.lblOpenZipFile, 0);
            this.Controls.SetChildIndex(this.gbxApplication, 0);
            this.Controls.SetChildIndex(this.gbxTheme, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.gbxApplication.ResumeLayout(false);
            this.gbxApplication.PerformLayout();
            this.gbxTheme.ResumeLayout(false);
            this.gbxTheme.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private System.Windows.Forms.Label lblOpenZipFile;
        private System.Windows.Forms.TextBox tbZipFile;
        private System.Windows.Forms.Button btnOpenZip;
        private System.Windows.Forms.GroupBox gbxApplication;
        private System.Windows.Forms.TextBox tbInstallScriptParameters;
        private System.Windows.Forms.Label lblInstallScriptParameters;
        private System.Windows.Forms.Label lblInstallerScript;
        private System.Windows.Forms.ComboBox cbxInstallScript;
        private System.Windows.Forms.GroupBox gbxTheme;
        private System.Windows.Forms.Label lblThemeFormat;
        private System.Windows.Forms.ComboBox cbxThemeFormat;
    }
}
