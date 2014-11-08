using System;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    public partial class frmWelcome : frmBase
    {
        public string KSPPath { get { return tbKSPPath.Text; } }


        public frmWelcome()
        {
            InitializeComponent();
        }


        private void frmWelcome_Load(object sender, EventArgs e)
        {
            cbWelcomeLanguages.Items.Clear();
            cbWelcomeLanguages.Items.AddRange(Localizer.GlobalInstance.AvailableLanguages.ToArray());
            cbWelcomeLanguages.SelectedItem = Localizer.GlobalInstance.DefaultLanguage;
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderSelectDialog dlg = new FolderSelectDialog();
            dlg.Title = Messages.MSG_KSP_INSTALL_FOLDER_SELECTION;
            if (dlg.ShowDialog(this.Handle))
            {
                if (KSPPathHelper.IsKSPInstallFolder(dlg.FileName))
                {
                    btnFinish.Enabled = true;
                    tbKSPPath.Text = dlg.FileName;
                }
                else
                {
                    MessageBox.Show(this, Messages.MSG_PLS_SELECT_VALID_KSP_INSTALL_FOLDER, Messages.MSG_TITLE_ATTENTION);
                }
            }
            else
            {
                btnFinish.Enabled = false;
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbWelcomeLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lang = cbWelcomeLanguages.SelectedItem as string;
            if (!string.IsNullOrEmpty(lang))
            {
                OptionsController.SelectedLanguage = Localizer.GlobalInstance.GetLanguageNameByLongName(lang);
                ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
                EventDistributor.InvokeLanguageChanged(this);
            }
        }
    }
}
