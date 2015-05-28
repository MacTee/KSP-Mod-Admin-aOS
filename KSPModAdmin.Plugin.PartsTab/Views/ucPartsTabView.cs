using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Plugin.PartsTab.Controller;
using KSPModAdmin.Plugin.PartsTab.Properties;

namespace KSPModAdmin.Plugin.PartsTab.Views
{
    using KSPModAdmin.Plugin.PartsTab.Controller;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucPartsTabView : ucBase
    {
        /// <summary>
        /// Creates a new instance of the ucTranslationView class.
        /// </summary>
        public ucPartsTabView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            PartsTabViewController.Initialize(this);
        }

        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {
            // do View related init here or in the PluginViewController.Initialize(...) methode.
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
            return Messages.MSG_PARTSTAB_VIEW_TITLE;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {
            // Enable/Disable your View Controls here.
            // Normally when KSP MA calls this methode with enable = false, all controls should be disabled.
            this.Enabled = enable;
        }
    }
}
