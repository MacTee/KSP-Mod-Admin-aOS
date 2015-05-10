using KSPModAdmin.Core;

namespace KSPModAdmin.Plugin.BackupTab
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class PluginController
    {
        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcBackupView View { get; protected set; }

        internal static void Initialize(UcBackupView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;

            // Add your stuff to initialize here.
        }

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        #endregion
    }
}
