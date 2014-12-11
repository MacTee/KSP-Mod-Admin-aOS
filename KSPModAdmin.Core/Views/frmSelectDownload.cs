using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
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
            set
            {
                if (value != null)
                {
                    foreach (DownloadInfo info in cbLinks.Items)
                    {
                        if (info.DownloadURL == value.DownloadURL)
                            cbLinks.SelectedItem = info;
                    }
                }
                else
                {
                    cbLinks.SelectedItem = null;
                }
            }
        }


        public frmSelectDownload()
        {
            InitializeComponent();
        }

        public frmSelectDownload(List<DownloadInfo> links)
        {
            InitializeComponent();

            Links = links;
        }

        public frmSelectDownload(List<DownloadInfo> links, DownloadInfo selectedLink)
        {
            InitializeComponent();

            Links = links;
            SelectedLink = selectedLink;
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


        public override KSPDialogResult GetKSPDialogResults()
        {
            return new KSPDialogResult(DialogResult, SelectedLink);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainController.SelectedKSPPath = "test";
        }
    }
}
