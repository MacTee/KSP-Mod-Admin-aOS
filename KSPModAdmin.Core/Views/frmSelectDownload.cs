using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    public partial class frmSelectDownload : frmBase
    {
        public List<DownloadInfo> Links
        {
            get
            {
                if (cbLinks.Items.Count > 0)
                    return cbLinks.Items.Cast<DownloadInfo>().ToList();
                else
                    return new List<DownloadInfo>();
            }
            set
            {
                cbLinks.Items.Clear();
                if (value != null && value.Count > 0)
                {
                    foreach (var e in value)
                        cbLinks.Items.Add(e);
                    cbLinks.SelectedItem = cbLinks.Items[0];
                }
                else
                    cbLinks.SelectedItem = null;
            }
        }

        public DownloadInfo SelectedLink
        {
            get
            {
                return (DownloadInfo)cbLinks.SelectedItem;
            }
        }


        public frmSelectDownload()
        {
            InitializeComponent();
        }


        private void frmSelectDownloadURL_Load(object sender, EventArgs e)
        {
            cbLinks.Select();
            cbLinks.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
