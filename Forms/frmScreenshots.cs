using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using com.nmtinstaller.csi.Properties;

namespace com.nmtinstaller.csi.Forms
{
    public partial class frmScreenshots : Form
    {
        private List<Control> ScreenShots;
        private int CurrentScreenshot;

        public frmScreenshots()
        {
            InitializeComponent();
        }

        private void DisplayScreenshot()
        {
            int Skip = CurrentScreenshot;
            foreach (PictureBox pb in ScreenShots)
            {
                if (Skip == 0)
                {
                    picScreenshot.Image = pb.Image;
                    break;
                }
                else
                {
                    Skip--;
                }
            }

            UpdateButtons();
        }


        public void SetScreenshot(List<Control> screenshots, int CurrentScreenshot)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                ScreenShots = screenshots;
                this.CurrentScreenshot = CurrentScreenshot;
                DisplayScreenshot();
            }));
        }

        private void UpdateButtons()
        {
            btnPrev.Enabled = (CurrentScreenshot > 0);
            btnNext.Enabled = CurrentScreenshot < (ScreenShots.Count - 1);
        }

        private void btnRealSize_Click(object sender, EventArgs e)
        {
            if (picScreenshot.Image != null)
            {
                int growX = picScreenshot.Image.Width - this.ClientSize.Width;
                int growY = picScreenshot.Image.Height - this.ClientSize.Height;

                this.Width += growX;
                this.Height += growY;
            }
        }

        private void btnZoomNormal_Click(object sender, EventArgs e)
        {
            this.Width = 520;
            this.Height = 380;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveScreenshotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (picScreenshot.Image != null)
            {
                SaveFileDialog sv = new SaveFileDialog();
                sv.Title = Resources.FileDialog_SaveScreenshotCaption;
                sv.Filter = Resources.FileDialog_SaveScreenshotFilter;

                if (sv.ShowDialog() == DialogResult.OK)
                {
                    picScreenshot.Image.Save(sv.FileName);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentScreenshot < (ScreenShots.Count - 1))
            {
                CurrentScreenshot++;
                DisplayScreenshot();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentScreenshot > 0)
            {
                CurrentScreenshot--;
                DisplayScreenshot();
            }
        }
    }
}
