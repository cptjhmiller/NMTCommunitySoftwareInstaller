namespace com.nmtinstaller.csi.Panels
{
    partial class pnlChangeLanguage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pnlChangeLanguage));
            this.lblSelectApplication = new System.Windows.Forms.Label();
            this.lvSelectLanguage = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgLanguages = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectApplication
            // 
            resources.ApplyResources(this.lblSelectApplication, "lblSelectApplication");
            this.lblSelectApplication.Name = "lblSelectApplication";
            // 
            // lvSelectLanguage
            // 
            resources.ApplyResources(this.lvSelectLanguage, "lvSelectLanguage");
            this.lvSelectLanguage.BackColor = System.Drawing.SystemColors.Control;
            this.lvSelectLanguage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvSelectLanguage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lvSelectLanguage.FullRowSelect = true;
            this.lvSelectLanguage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvSelectLanguage.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvSelectLanguage.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvSelectLanguage.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvSelectLanguage.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvSelectLanguage.Items3"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("lvSelectLanguage.Items4")))});
            this.lvSelectLanguage.LargeImageList = this.imgLanguages;
            this.lvSelectLanguage.Name = "lvSelectLanguage";
            this.lvSelectLanguage.SmallImageList = this.imgLanguages;
            this.lvSelectLanguage.UseCompatibleStateImageBehavior = false;
            this.lvSelectLanguage.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // imgLanguages
            // 
            this.imgLanguages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgLanguages.ImageStream")));
            this.imgLanguages.TransparentColor = System.Drawing.Color.Transparent;
            this.imgLanguages.Images.SetKeyName(0, "United States.png");
            this.imgLanguages.Images.SetKeyName(1, "Russia.png");
            this.imgLanguages.Images.SetKeyName(2, "Netherlands.png");
            this.imgLanguages.Images.SetKeyName(3, "Turkey.png");
            this.imgLanguages.Images.SetKeyName(4, "France.png");
            // 
            // pnlChangeLanguage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvSelectLanguage);
            this.Controls.Add(this.lblSelectApplication);
            this.Name = "pnlChangeLanguage";
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.lblSelectApplication, 0);
            this.Controls.SetChildIndex(this.lvSelectLanguage, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSelectApplication;
        private System.Windows.Forms.ListView lvSelectLanguage;
        private System.Windows.Forms.ImageList imgLanguages;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
