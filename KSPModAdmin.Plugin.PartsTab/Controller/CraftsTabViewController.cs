using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Model;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Views;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Controller
{
    /// <summary>
    /// Controller class for the CraftsTab view.
    /// </summary>
    public class CraftsTabViewController
    {
        private static CraftsTabViewController instance = null;
        private static List<CraftNode> allCrafts = new List<CraftNode>();
        private static CraftsTreeModel model = new CraftsTreeModel();

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static CraftsTabViewController Instance
        {
            get { return instance ?? (instance = new CraftsTabViewController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucCraftsTabView View { get; protected set; }

        #endregion

        internal static void Initialize(ucCraftsTabView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
            View.Model = model;
            View.AddActionKey(VirtualKey.VK_DELETE, (x) =>
            {
                RemoveSelectedCraft();
                return true;
            });
            View.AddActionKey(VirtualKey.VK_BACK, (x) =>
            {
                RemoveSelectedCraft();
                return true;
            });
        }

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        private static void KSPRootChanged(string kspPath)
        {
            model.Nodes.Clear();
            allCrafts.Clear();
            View.SelectedBuildingFilter = PartsTabViewController.All;

            View.InvalidateView();
        }

        #endregion

        public static void RefreshCraftsTab()
        {
            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            AsyncTask<bool>.DoWork(() =>
                {
                    System.Threading.Thread.Sleep(3000);
                    return true;
                },
                (result, ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        MessageBox.Show(ex.Message, "Error");
                });

        }

        public static void ValidateCrafts()
        {
            RefreshCraftsTab();
        }

        public static void RenameSelectedCraft()
        {
        }

        public static void SwapBuildingOfSelectedCraft()
        {
        }

        public static void RemoveSelectedCraft()
        {
        }

        public static void RefreshTreeView()
        {
        }
    }
}
