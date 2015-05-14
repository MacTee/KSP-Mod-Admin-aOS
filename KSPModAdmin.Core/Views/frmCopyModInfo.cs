using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmCopyModInfo : frmBase
    {
        #region Properties

        /// <summary>
        /// The source mod.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModNode SourceMod 
        {
            get
            {
                return (tbSourceMod.Tag != null) ? (ModNode)tbSourceMod.Tag : null;
            }
            set
            {
                tbSourceMod.Tag = value;
                if (value != null)
                    tbSourceMod.Text = value.ToString();
            }
        }

        /// <summary>
        /// The destination mod.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModNode DestMod 
        {
            get 
            {
                return (cbDestMod.SelectedItem != null) ? (ModNode)cbDestMod.SelectedItem : null;
            }
        }

        /// <summary>
        /// Array of all mods that could be copied to.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModNode[] Mods
        {
            set
            {
                if (value != null)
                {
                    foreach (ModNode mod in value)
                    {
                        if (SourceMod != null && SourceMod.Name != mod.Name)
                            cbDestMod.Items.Add(mod);
                    }

                    if (cbDestMod.Items.Count > 0)
                        cbDestMod.SelectedItem = cbDestMod.Items[0];
                }
            } 
        }

        #endregion


        /// <summary>
        /// Creates a new instance of the frmCopyModInfo class.
        /// </summary>
        public frmCopyModInfo()
        {
            InitializeComponent();

            SourceMod = null;
        }


        private void btnCopy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            if (SourceMod != null)
            {
                // keep version!
                var tempVersion = DestMod.Version;
                var tempKey = DestMod.Key;
                DestMod.ModInfo = SourceMod.ModInfo;
                DestMod.Note = SourceMod.Note;
                DestMod.Version = tempVersion;
                DestMod.Key = tempKey;
                DestMod.IsOutdated = false;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
