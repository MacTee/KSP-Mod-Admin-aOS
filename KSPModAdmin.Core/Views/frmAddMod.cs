using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.SiteHandler;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmAddMod : frmBase
    {
        /// <summary>
        /// Creates a new instance of the frmAddMod class.
        /// </summary>
        public frmAddMod()
        {
            InitializeComponent();
        }

        private void frmAddMod_Load(object sender, EventArgs e)
        {
            tbModPath.Select();
            tbModPath.Focus();
        }

        private void frmAddMod_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = picLoading.Visible;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            tbModName.Enabled = false;
            tbModPath.Enabled = false;
            btnAdd.Enabled = false;
            btnAddAndClose.Enabled = false;
            btnClose.Enabled = false;
            btnFolderSearch.Enabled = false;
            cbInstallAfterAdd.Enabled = false;
            picLoading.Visible = true;
            progressBar1.Visible = true;
            ModSelectionController.View.ShowBusy = true;

            string modPath = tbModPath.Text;
            new AsyncTask<bool>(() =>
            {
                ModNode newMod = null;
                ISiteHandler handler = SiteHandlerManager.GetSiteHandlerByURL(modPath);

                if (handler != null)
                {
                    InvokeIfRequired(() =>
                    {
                        if (!OptionsController.HasValidDownloadPath)
                        {
                            Messenger.AddInfo(Messages.MSG_DOWNLOAD_PATH_MISSING_PLEASE_SELECT_ONE); 
                            OptionsController.SelectNewDownloadPath();
                        }
                    });

                    if (!OptionsController.HasValidDownloadPath)
                        return false;

                    Messenger.AddInfo(Messages.MSG_URL_DETECTED_STARTING_DOWNLOAD);
                    newMod = handler.HandleAdd(modPath, tbModName.Text, cbInstallAfterAdd.Checked, UpdateProgressBar);
                }

                else if (ValidModPath(modPath))
                    newMod = ModSelectionController.HandleModAddViaPath(modPath, tbModName.Text, cbInstallAfterAdd.Checked);

                else
                {
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_MOD_PATH_URL_0_INVALID, modPath));
                    InvokeIfRequired(() => MessageBox.Show(this, Messages.MSG_PLS_ENTER_VALID_ARCHIVE_URL, Messages.MSG_TITLE_ATTENTION));
                }

                return (newMod != null);
            }, (success, ex) =>
            {
                if (ex != null)
                {
                    Messenger.AddError(ex.Message, ex);
                    MessageBox.Show(this, ex.Message, Messages.MSG_TITLE_ERROR);
                }

                tbModName.Enabled = true;
                tbModPath.Enabled = true;
                btnAdd.Enabled = true;
                btnAddAndClose.Enabled = true;
                btnClose.Enabled = true;
                btnFolderSearch.Enabled = true;
                cbInstallAfterAdd.Enabled = true;
                picLoading.Visible = false;
                progressBar1.Visible = false;
                ModSelectionController.View.ShowBusy = false;

                if (success && sender == btnAddAndClose)
                    Close();
                else
                {
                    tbModName.Text = string.Empty;
                    tbModPath.Text = string.Empty;
                }
            }).Run();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!picLoading.Visible)
                Close();
        }

        private void btnFolderSearch_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Constants.ADD_DLG_FILTER;
            dlg.Multiselect = false;

            string path = OptionsController.DownloadPath;
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
                dlg.InitialDirectory = path;

            if (dlg.ShowDialog() == DialogResult.OK)
                tbModPath.Text = dlg.FileName;
        }

        private bool ValidModPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            string ext = Path.GetExtension(path);
            if (string.IsNullOrEmpty(ext))
                return false;

            ext = ext.ToLower();
            return ((ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR ||
                     ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT) && 
                     File.Exists(tbModPath.Text));
        }

        private void UpdateProgressBar(long bytesReceived, long fileSize)
        {
            InvokeIfRequired(() =>
                {
                    int res = (int)(bytesReceived / 1000);
                    int max = (int)(fileSize / 1000);
                    progressBar1.Minimum = 0;
                    if (max > progressBar1.Minimum)
                        progressBar1.Maximum = max;
                    if (res >= progressBar1.Minimum && res <= progressBar1.Maximum)
                        progressBar1.Value = res;
                });
        }
    }
}
