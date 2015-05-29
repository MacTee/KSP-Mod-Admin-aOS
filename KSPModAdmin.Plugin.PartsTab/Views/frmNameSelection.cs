using System;
using System.Collections.Generic;
using System.Windows.Forms;
using KSPModAdmin.Core.Views;

namespace KSPModAdmin.Plugin.PartsTab.Views
{
    public partial class frmNameSelection : frmBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the tbNewName Text property.
        /// </summary>
        public string NewName { get { return tbNewName.Text; } set { tbNewName.Text = value; } }

        /// <summary>
        /// Gets or sets the tbNewTitle Text property.
        /// </summary>
        public string NewTitle { get { return tbNewTitle.Text; } set { tbNewTitle.Text = value; } }

        /// <summary>
        /// Gets or sets the Description Text.
        /// </summary>
        public string Description { get { return lblNameSelectionDescription.Text; } set { lblNameSelectionDescription.Text = value; } }

        /// <summary>
        /// Gets or sets the known (forbidden) names.
        /// </summary>
        public List<string> KnownNames { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of the frmNameSelection class.
        /// </summary>
        public frmNameSelection()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling

        /// <summary>
        /// Handels the Load event of the frmNameSelection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmNameSelection_Load(object sender, EventArgs e)
        {
            tbNewName.Select();
            tbNewName.SelectAll();
            tbNewName.Focus();
        }

        /// <summary>
        /// Handels the Click event of the btnOK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (tbNewName.Text.Length == 0)
                MessageBox.Show(this, "Please enter a new name first.");

            else if (KnownNames.Contains(NewName))
                MessageBox.Show(this, "Name already exists!");

            else
            {
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
        }

        /// <summary>
        /// Handels the Click event of the btnCancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Handels the KeyDown event of the tbNewName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOK_Click(null, null);
        }

        #endregion
    }
}
