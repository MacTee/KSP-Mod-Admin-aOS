using System;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Plugin.PartsTab.Views
{
    public partial class frmPartCategorySelection : frmBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the selected part category.
        /// </summary>
        public string Category
        { 
            get { return (string)cbCategory.SelectedItem; } 
            set 
            {
                foreach (string item in cbCategory.Items)
                {
                    if (item == value)
                        cbCategory.SelectedItem = item;
                }
            } 
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the frmPartCategorySelection class.
        /// </summary>
        public frmPartCategorySelection()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

        /// <summary>
        /// Handel the Load event of the frmPartCategorySelection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPartCategorySelection_Load(object sender, EventArgs e)
        {
            if (cbCategory.SelectedIndex < 0)
                cbCategory.SelectedIndex = 0;
            cbCategory.Select();
            cbCategory.Focus();
        }

        /// <summary>
        /// Handel the Click event of the btnOK.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Handel the Click event of the btnCancel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        #endregion
    }
}
