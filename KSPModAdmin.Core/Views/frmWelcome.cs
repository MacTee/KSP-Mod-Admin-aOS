using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using FolderSelect;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmWelcome : frmBase
    {
        /// <summary>
        /// Gets the selected KSP install folder path.
        /// </summary>
        public string KSPPath { get { return tbKSPPath.Text; } }


        /// <summary>
        /// Creates a new instance of the frmWelcome class.
        /// </summary>
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
            Language lang = cbWelcomeLanguages.SelectedItem as Language;
            if (lang != null)
            {
                OptionsController.SelectedLanguage = Localizer.GlobalInstance.GetLanguageNameByLongName(lang.LongName);
                ControlTranslator.TranslateControls(Localizer.GlobalInstance, this);
                EventDistributor.InvokeLanguageChanged(this);
            }
        }
    }
}
