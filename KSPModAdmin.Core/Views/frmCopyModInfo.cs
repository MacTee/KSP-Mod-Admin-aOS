using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Views
{
    public partial class frmCopyModInfo : frmBase
    {
        #region Properties

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

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ModNode DestMod 
        {
            get 
            {
                return (cbDestMod.SelectedItem != null) ? (ModNode)cbDestMod.SelectedItem : null;
            }
        }

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
                DestMod.Version = tempKey;
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
