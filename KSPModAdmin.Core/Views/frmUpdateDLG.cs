using System;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core.Views
{
    public partial class frmUpdateDLG : frmBase
    {
        public PostDownloadAction PostDownloadAction
        {
            get
            {
                return (PostDownloadAction)cbPostDownloadAction.SelectedIndex;
            }
            set
            {
                cbPostDownloadAction.SelectedIndex = (int)value;
            }
        }

        public string DownloadPath
        {
            get
            {
                return tbDownloadPath.Text;
            }
            set
            {
                tbDownloadPath.Text = value;
            }
        }

        public string Message 
        {
            get
            {
                return tbMessage.Text;
            }
            set
            {
                tbMessage.Text = value;
            }
        }


        public frmUpdateDLG()
        {
            InitializeComponent();
        }


        private void UpdateDLG_Load(object sender, EventArgs e)
        {
            if (cbPostDownloadAction.SelectedIndex < 0)
                cbPostDownloadAction.SelectedIndex = 0;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnDownloadPath_Click(object sender, EventArgs e)
        {
            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = Messages.MSG_DOWNLOAD_FOLDER_SELECTION_TITLE;
            dlg.InitialDirectory = DownloadPath;
            if (dlg.ShowDialog(this.Handle))
            {
                DownloadPath = dlg.FileName;
                Messenger.AddInfo(string.Format(Messages.MSG_DOWNLOAD_PATH_CHANGED_0, dlg.FileName));
            }

            //FolderBrowserDialog dlg = new FolderBrowserDialog();
            //dlg.SelectedPath = DownloadPath;
            //if (dlg.ShowDialog(this) == DialogResult.OK)
            //{
            //    DownloadPath = dlg.SelectedPath;
            //    Messenger.AddInfo(string.Format(Messages.MSG_DOWNLOAD_PATH_CHANGED_0, dlg.SelectedPath));
            //}
        }
    }
}
