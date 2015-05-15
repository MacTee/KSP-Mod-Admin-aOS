using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmMain : frmBase
    {
        /// <summary>
        /// Gets or sets the known KSP install paths.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<NoteNode> KnownKSPPaths
        {
            get { return (from e in cbKSPPath.Items.Cast<string>() select new NoteNode(e, e, string.Empty)).ToList(); }
            set
            {
                cbKSPPath.Items.Clear();
                if (value != null)
                {
                    cbKSPPath.SelectedIndexChanged -= cbKSPPath_SelectedIndexChanged;
                    foreach (NoteNode path in value)
                        cbKSPPath.Items.Add(path.Name);
                    cbKSPPath.SelectedIndexChanged += cbKSPPath_SelectedIndexChanged;
                }
            }
        }

        /// <summary>
        /// Gets or sets the selected KSP path.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedKSPPath
        {
            get
            {
                return (cbKSPPath.Items.Count > 0) ? cbKSPPath.SelectedItem.ToString() : string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    cbKSPPath.SelectedItem = null;
                
                if (cbKSPPath.Items.Count <= 0)
                    return;

                foreach (string path in cbKSPPath.Items)
                {
                    if (value != path)
                        continue;

                    cbKSPPath.SelectedItem = path;
                    break;
                }
            }
        }

        /// <summary>
        /// Gets the MainTab control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TabControl TabControl { get { return tabControl1; } }

        /// <summary>
        /// Gets the KSPStartup user control.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ucKSPStartup UcKSPStartup { get { return ucKSPStartup1; } }


        /// <summary>
        /// Creates a new instance of the frmMain class.
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;
        }


        /// <summary>
        /// Handles the Load event of the form.
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Event handler for the FormClosing from frmMain.
        /// </summary>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainController.ShutDown(false);
        }

        /// <summary>
        /// Event handler for the SelectedIndexChanged from cbKSPPath.
        /// </summary>
        private void cbKSPPath_SelectedIndexChanged(object sender, EventArgs e)
        {
            OptionsController.SelectedKSPPath = cbKSPPath.SelectedItem.ToString();
        }

        /// <summary>
        /// Sets the selected KSP path without raising event SelectedIndexChanged.
        /// </summary>
        /// <param name="kspPath">The new selected KSP path.</param>
        internal void SilentSetSelectedKSPPath(string kspPath)
        {
            cbKSPPath.SelectedIndexChanged -= cbKSPPath_SelectedIndexChanged;
            SelectedKSPPath = null;
            SelectedKSPPath = kspPath;
            cbKSPPath.SelectedIndexChanged += cbKSPPath_SelectedIndexChanged;
        }

        /// <summary>
        /// Options tab will be the last TabPage.
        /// </summary>
        internal void OrderTabPages()
        {
            tabControl1.TabPages.Remove(tabPageOptions);
            tabControl1.TabPages.Add(tabPageOptions);
        }
    }
}
