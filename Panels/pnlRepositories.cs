using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using com.nmtinstaller.csi.Properties;
using com.nmtinstaller.csi.Utilities;
using com.nmtinstaller.csi.RemoteFileInfo;

namespace com.nmtinstaller.csi.Panels
{
    public partial class pnlRepositories : BasePanel
    {
        public pnlRepositories(Dictionary<string, object> panelResults, Functions.UpdateProgressInfo OnProgress)
            : base(Resources.PanelTitle_Repositories, panelResults, OnProgress)
        {
            InitializeComponent();

            mainRepositoryBindingSource.DataSource = new RepositoryLocations();
        }

        public override bool Execute()
        {
            if (btnSave.Enabled)
            {
                MessageBox.Show(Resources.MessageBox_RepositorySaveCancel, Resources.MessageBox_RepositorySaveCancelCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return btnSave.Enabled==false;
        }

        private void mainRepositoryBindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {
            //List<RepositoryLocationInfo> current = new RepositoryLocations().GetMainRepositories();
            //current.Add(new RepositoryLocationInfo("", "", true));
            //new RepositoryLocations().MainRepositories = current;

            //mainRepositoryBindingSource.DataSource = new RepositoryLocations();
            RepositoryLocations re = ((RepositoryLocations)mainRepositoryBindingSource.DataSource);
            re.MainRepositories.Add(new RepositoryLocationInfo("", "", true));

            mainRepositoryBindingSource.ResetBindings(false);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns.Count - 1)
            {
                ((RepositoryLocations)mainRepositoryBindingSource.DataSource).RemoveMainRepository(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());

                mainRepositoryBindingSource.ResetBindings(false);
                //mainRepositoryBindingSource.DataSource = new RepositoryLocations();
                btnSave.Enabled = btnCancel.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string result = string.Empty;
            if (DataIsValid(out result))
            {
                if (mainRepositoryBindingSource.DataSource != null)
                {
                    RepositoryLocations reps = (RepositoryLocations)mainRepositoryBindingSource.DataSource;
                    reps.SaveRepository();

                    btnSave.Enabled = btnCancel.Enabled = false;

                    if (MessageBox.Show(Resources.MessageBox_RepositoryRestartConfirm, Resources.MessageBox_RepositoryRestartCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Application.Restart();
                    }
                }
            }
            else
            {
                MessageBox.Show(Resources.MessageBox_RepositoryInfoInvalid + result, Resources.MessageBox_RepositoryInfoInvalidCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool DataIsValid(out string errors)
        {
            errors = string.Empty;
            bool isValid = true;

            if (dataGridView1.Rows.Count < 2)
            {
                isValid = false;
                errors += Resources.Text_RepositoriesCheckNoRepositories + Environment.NewLine;
            }

            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString().Length == 0)
                {
                    isValid = false;
                    errors += Resources.Text_RepositoriesCheckNoName + Environment.NewLine;
                }

                int j = 0;

                while (j<dataGridView1.Rows.Count-1)
                {
                    DataGridViewRow row = dataGridView1.Rows[j];
                    if (row.Index != i)
                    {
                        if ((row.Cells[1].Value == null) || (dataGridView1.Rows[i].Cells[1].Value==null) || (row.Cells[1].Value.ToString().ToLower() == dataGridView1.Rows[i].Cells[1].Value.ToString().ToLower()))
                        {
                            isValid = false;
                            errors += string.Format(Resources.Text_RepositoriesCheckNotUniqueName, dataGridView1.Rows[i].Cells[1]) + Environment.NewLine;
                        }
                    }

                    j++;
                }


                if (isValid)
                {
                    string url = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    isValid = Uri.IsWellFormedUriString(url, UriKind.Absolute);
                    isValid = (url.EndsWith(".zip", StringComparison.InvariantCultureIgnoreCase) || url.EndsWith(".xml", StringComparison.InvariantCultureIgnoreCase)) && isValid;

                    if (!isValid)
                    {
                        errors += string.Format(Resources.Text_RepositoriesCheckInvalidURL, dataGridView1.Rows[i].Cells[1].Value) + Environment.NewLine;
                    }
                }

            }


            if ((isValid) && (mainRepositoryBindingSource.DataSource!=null))
            {
                List<RepositoryLocationInfo> invalidLocations = ((RepositoryLocations)mainRepositoryBindingSource.DataSource).GetInvalidLocations();
                foreach(RepositoryLocationInfo loc in invalidLocations)
                {
                    errors += string.Format(Resources.Text_RepositoriesCheckCantConnectUrl, loc.Name) + Environment.NewLine;
                }

                isValid = isValid & invalidLocations.Count == 0;
            }



            return isValid;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mainRepositoryBindingSource.DataSource = new RepositoryLocations();
            btnSave.Enabled = btnCancel.Enabled = false;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            btnSave.Enabled = btnCancel.Enabled = true;
        }
    }
}
