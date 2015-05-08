using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Views;
using KSPModAdmin.Template.Plugin.Properties;

namespace KSPModAdmin.Template.Plugin
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucPluginView : ucBase
    {
        private string mLastSelectedItemName = string.Empty;





        /// <summary>
        /// Creates a new instance of the ucTranslationView class.
        /// </summary>
        public ucPluginView()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            PluginController.Initialize(this);
        }


        #region Event handling

        private void ucPluginView_Load(object sender, EventArgs e)
        {

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
            return Messages.MSG_PLUGIN_VIEW_TITLE;
        }

        /// <summary>
        /// Sets the enabled state of some view controls.
        /// </summary>
        public void SetEnabledOfAllControls(bool enable)
        {

        }

    }
}
