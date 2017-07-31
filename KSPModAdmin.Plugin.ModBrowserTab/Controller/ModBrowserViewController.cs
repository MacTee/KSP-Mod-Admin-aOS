using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.ModBrowserTab.Views;

namespace KSPModAdmin.Plugin.ModBrowserTab.Controller
{
    /// <summary>
    /// Delegate for the ModBrowserInitComplete event
    /// </summary>
    public delegate void ModBrowserInitCompleteHandler();

    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class ModBrowserViewController
    {
        /// <summary>
        /// Event ModBrowserInitComplete occurs when the initialization of the ModBrowserViewController is complete.
        /// </summary>
        public static event ModBrowserInitCompleteHandler ModBrowserInitComplete;

        private static ModBrowserRegister modBrowserRegister = new ModBrowserRegister();


        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucModBrowserView View { get; protected set; }


        internal static void Initialize(ucModBrowserView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;

            // Add your stuff to initialize here.
            if (ModBrowserInitComplete != null)
                ModBrowserInitComplete();

            var modBrowser = new UcModBrowserCkan();
            RegisterModBrowser(modBrowser);

            View.Load += ViewOnLoad;
        }

        private static void ViewOnLoad(object sender, EventArgs eventArgs)
        {
            View.SelectedModBrowser = modBrowserRegister.ModBrowserList.Values.FirstOrDefault();
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

        /// <summary>
        /// Adds a ModBrowser to the register of all known ModBrowser.
        /// </summary>
        /// <param name="modBrowser">The ModBrowser to add.</param>
        public static void RegisterModBrowser(IKSPMAModBrowser modBrowser)
        {
            if (!modBrowserRegister.Add(modBrowser))
                return;

            View.AddModBrowser(modBrowser);
        }

        /// <summary>
        /// Removes a ModBrowser from the register.
        /// </summary>
        /// <param name="modBrowser">The ModBrowser to remove.</param>
        public static void UnregisterModBrowser(IKSPMAModBrowser modBrowser)
        {
            if (!modBrowserRegister.Remove(modBrowser))
                return;

            View.RemoveModBrowser(modBrowser);
        }

        /// <summary>
        /// Removes the named ModBrowser from the register.
        /// </summary>
        /// <param name="modBrowserName">Name of the ModBrowser to remove.</param>
        public static void UnregisterModBrowserByName(string modBrowserName)
        {
            if (!modBrowserRegister.RemoveByName(modBrowserName))
                return;

            View.RemoveModBrowserByName(modBrowserName);
        }
    }
}
