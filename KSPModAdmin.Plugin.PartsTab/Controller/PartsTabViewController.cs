using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.PartsTab.Model;
using KSPModAdmin.Plugin.PartsTab.Views;

namespace KSPModAdmin.Plugin.PartsTab.Controller
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class PartsTabViewController
    {
        #region Members

        private static PartsTabViewController instance = null;
        private static PartsTreeModel model = new PartsTreeModel();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static PartsTabViewController Instance
        {
            get { return instance ?? (instance = new PartsTabViewController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucPartsTabView View { get; protected set; }

        #endregion

        internal static void Initialize(ucPartsTabView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;

            // Add your stuff to initialize here.
            View.Model = model;
            View.AddActionKey(VirtualKey.VK_DELETE, (x) =>
            {
                RemovePart();
                return true;
            });
            View.AddActionKey(VirtualKey.VK_BACK, (x) =>
            {
                RemovePart();
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

        #endregion

        public static void RefreshPartsTab()
        {
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            View.ShowProcessingIcon = true;
            model.Nodes.Clear();
            AsyncTask<bool>.DoWork(
                () =>
                {
                    System.Threading.Thread.Sleep(3000);
                    return true;
                },
                (b, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    View.ShowProcessingIcon = false;

                    var node = new PartNode() { Name = "sensorAtmosphere", Category = "Science", Mod = "Squad", Title = "Atmospheric Fluid Spectro-Variometer" };
                    node.Nodes.Add(new PartNode() { Name = "", Category = "", Mod = "", Title = "Ion-Powered Space Probe" });
                    node.Nodes.Add(new PartNode() { Name = "", Category = "", Mod = "", Title = "Rover + Skycrane" });
                    model.Nodes.Add(node);
                    model.Nodes.Add(new PartNode() { Name = "GooExperiment", Category = "Science", Mod = "Squad", Title = "Mystery Goo™ Containment Unit" });
                });
        }

        public static void RemovePart()
        {
        }

        public static void EditPart()
        {
        }

        public static void ChangeCategory()
        {
        }

        public static void SelectionChanged()
        {
            PartNode selNode = View.SelectedPart;
        }
    }
}
