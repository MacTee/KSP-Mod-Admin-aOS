using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPMODAdmin.Core.Utils.Ckan;
using KSPModAdmin.Plugin.ModBrowserTab.Model;
using KSPModAdmin.Plugin.ModBrowserTab.Views;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin.Plugin.ModBrowserTab.Controller
{
    using KSPModAdmin.Core.Utils.KerbalStuff;

    /// <summary>
    /// Controller class for the ModBrowser KerbalStuff view.
    /// </summary>
    public class ModBrowserKerbalStuffController
    {
        #region Member

        private static ModBrowserKerbalStuffController instance = null;
        private static KerbalStuffTreeModel model = new KerbalStuffTreeModel();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static ModBrowserKerbalStuffController Instance
        {
            get { return instance ?? (instance = new ModBrowserKerbalStuffController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcModBrowserKerbalStuff View { get; protected set; }

        private static RefreshType LastRefresh { get; set; }

        #endregion

        internal static void Initialize(UcModBrowserKerbalStuff view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;

            // Add your stuff to initialize here.
            View.Model = model;
            LastRefresh = RefreshType.New;
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

        #endregion

        public static void Refresh(RefreshType refreshType = RefreshType.Last)
        {
            model.Nodes.Clear();
            if (refreshType == RefreshType.Last)
                refreshType = LastRefresh;

            View.ShowProcessing = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            AsyncTask<List<KsMod>>.DoWork(
                () =>
                {
                    List<KsMod> mods = null;
                    switch (refreshType)
                    {
                        default:
                        case RefreshType.New:
                            mods = KerbalStuff.BrowseNew(View.Page);
                            break;
                        case RefreshType.Top:
                            mods = KerbalStuff.BrowseTop(View.Page);
                            break;
                        case RefreshType.Featured:
                            mods = KerbalStuff.BrowseFeatured(View.Page);
                            break;
                        case RefreshType.Browse:
                            mods = KerbalStuff.Browse(View.Page);
                            break;
                    }

                    return mods;        
                },
                (result, ex) =>
                {
                    View.ShowProcessing = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null) 
                        Messenger.AddError(string.Format("Error during browsing KerbalStuff! {0}", ex), ex);
                    else
                    {
                        if (KerbalStuff.LastResponse == null || result == null)
                        {
                            Messenger.AddError("Error during browsing KerbalStuff! Empty result");
                            return;
                        }

                        View.MaxPages = KerbalStuff.LastResponse.pages;
                        View.CountLabelText = string.Format("Mods per page: {0}", KerbalStuff.LastResponse.count);

                        foreach (var mod in result) 
                            model.Nodes.Add(new KerbalStuffNode(mod));
                    }
                });
        }
    }

    public enum RefreshType
    {
        Last,
        New,
        Top,
        Featured,
        Browse
    }
}
