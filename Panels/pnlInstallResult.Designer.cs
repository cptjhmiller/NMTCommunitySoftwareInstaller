namespace com.nmtinstaller.csi.Panels
{
    partial class pnlCleanup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlCleanup));
            this.lblDone = new System.Windows.Forms.Label();
            this.rtbInstallResult = new System.Windows.Forms.RichTextBox();
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
            // lblDone
            // 
            this.lblDone.AccessibleDescription = null;
            this.lblDone.AccessibleName = null;
            resources.ApplyResources(this.lblDone, "lblDone");
            this.lblDone.Font = null;
            this.lblDone.Name = "lblDone";
            // 
            // rtbInstallResult
            // 
            this.rtbInstallResult.AccessibleDescription = null;
            this.rtbInstallResult.AccessibleName = null;
            resources.ApplyResources(this.rtbInstallResult, "rtbInstallResult");
            this.rtbInstallResult.BackColor = System.Drawing.SystemColors.Control;
            this.rtbInstallResult.BackgroundImage = null;
            this.rtbInstallResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbInstallResult.Font = null;
            this.rtbInstallResult.Name = "rtbInstallResult";
            this.rtbInstallResult.ReadOnly = true;
            this.rtbInstallResult.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.rtbInstallResult_LinkClicked);
            this.rtbInstallResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.rtbInstallResult_KeyPress);
            this.rtbInstallResult.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbInstallResult_KeyUp);
            // 
            // pnlCleanup
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.rtbInstallResult);
            this.Controls.Add(this.lblDone);
            this.Font = null;
            this.Name = "pnlCleanup";
            this.VisibleChanged += new System.EventHandler(this.pnlCleanup_VisibleChanged);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblDone, 0);
            this.Controls.SetChildIndex(this.rtbInstallResult, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDone;
        private System.Windows.Forms.RichTextBox rtbInstallResult;
    }
}
