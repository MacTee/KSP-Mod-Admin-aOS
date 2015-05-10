using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.Translation.Properties;

namespace KSPModAdmin.Plugin.Translation
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucTranslationView : ucBase
    {
        private string mLastSelectedItemName = string.Empty;


        #region Properties

        /// <summary>
        /// Gets or sets the content of the language file to display/edit.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LanguageFileContent LanguageFileContent
        {
            get { return mLanguageFileContent; }
            set
            {
                mLanguageFileContent = value;

                if (value != null)
                {
                    tbTransToolFileName.Text = mLanguageFileContent.FileName;
                    tbTransToolLanguageName.Text = mLanguageFileContent.Name;
                    tbTransToolLanguageShortName.Text = mLanguageFileContent.ShortName;
                    dgvTransToolLanguageFileEntries.DataSource = mLanguageFileContent.Entries;
                }
                else
                {
                    tbTransToolFileName.Text = string.Empty;
                    tbTransToolLanguageName.Text = string.Empty;
                    tbTransToolLanguageShortName.Text = string.Empty;
                    dgvTransToolLanguageFileEntries.DataSource = null;
                }
            }
        }
        private LanguageFileContent mLanguageFileContent = null;

        #endregion


        /// <summary>
        /// Creates a new instance of the ucTranslationView class.
        /// </summary>
        public ucTranslationView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            TranslationController.Initialize(this);
        }


        #region Event handling

        private void ucTranslationView_Load(object sender, EventArgs e)
        {
            cbTransToolLanguages.Items.Clear();
            cbTransToolLanguages.Items.AddRange(TranslationController.GetAvailableLanguages());
        }

        private void cbTransToolLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename = GetFilePathByName(cbTransToolLanguages.SelectedItem);
            LanguageFileContent content = TranslationController.LoadSelectedLanguage(filename);

            if (content != null)
            {
                SetEnabledOfAllControls(cbTransToolEdit.Checked);
                LanguageFileContent = content;
            }
            else
            {
                SetEnabledOfAllControls(false);
            }
        }

        private void cbTransToolEdit_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTransToolEdit.Checked && cbTransToolLanguages.SelectedItem == null)
                cbTransToolEdit.Checked = false;

            if (!cbTransToolEdit.Checked)
                cbTransToolEdit.Image = Resources.pencil2_delete;
            else
                cbTransToolEdit.Image = Resources.pencil2;

            cbTransToolLanguages_SelectedIndexChanged(null, null);
        }

        private void btnTransToolSave_Click(object sender, EventArgs e)
        {
            if (mLanguageFileContent != null)
            {
                mLanguageFileContent.FileName = tbTransToolFileName.Text;
                mLanguageFileContent.Name = tbTransToolLanguageName.Text;
                mLanguageFileContent.ShortName = tbTransToolLanguageShortName.Text;
                mLanguageFileContent.Save();

                mLastSelectedItemName = mLanguageFileContent.FileName;

                cbTransToolLanguages.Items.Clear();
                cbTransToolLanguages.Items.AddRange(TranslationController.GetAvailableLanguages());

                foreach (LanguageSelectInfo item in cbTransToolLanguages.Items)
                {
                    if (item.Name == mLastSelectedItemName)
                    {
                        cbTransToolLanguages.SelectedItem = item;
                        break;
                    }
                }
            }
        }

        #endregion


        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public override void InvalidateView()
        {
            Invalidate();
            Update();
            Refresh();
        }

        /// <summary>
        /// Gets the Name for the parent TabPage.
        /// </summary>
        /// <returns>The Name for the parent TabPage.</returns>
        public override string GetTabCaption()
        {
            return Messages.MSG_TRANSLATION_VIEW_TITLE;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            gbTransToolLanguageFile.Enabled = enable;
            dgvTransToolLanguageFileEntries.Columns[0].ReadOnly = !enable;
            dgvTransToolLanguageFileEntries.Columns[1].ReadOnly = !enable;
            btnTransToolSave.Enabled = enable;
        }


        private string GetFilePathByName(object selectedItem)
        {
            LanguageSelectInfo lfInfo = selectedItem as LanguageSelectInfo;
            if (lfInfo == null)
                return string.Empty;

            return lfInfo.Path;
        }
    }
}
