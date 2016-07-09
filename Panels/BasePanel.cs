using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Forms;
using com.nmtinstaller.csi.Utilities;

namespace com.nmtinstaller.csi.Panels
{
    public partial class BasePanel : UserControl
    {
        private Label lblCaption;
        protected PictureBox pictureBox1;
        protected Dictionary<string, object> panelResults;
        protected Functions.UpdateProgressInfo OnProgress;

        public virtual bool Execute() { throw new NotImplementedException(); }

        public BasePanel(string Caption, Dictionary<string, object> panelResults, Functions.UpdateProgressInfo OnProgress)
        {
            InitializeComponent();
            lblCaption.Text = Caption;
            this.OnProgress = OnProgress;
            this.panelResults = panelResults;
        }

        public BasePanel()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}