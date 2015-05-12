using FolderSelect;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin.Plugin.BackupTab
{
    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class UcBackupViewController
    {
        private static BackupTreeModel model = new BackupTreeModel();

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static UcBackupView View { get; protected set; }

        internal static void Initialize(UcBackupView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
            View.Model = model;
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

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        private static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        private static void KSPRootChanged(string kspPath)
        {

        }

        #endregion

        public static void SelectNewBackupPath()
        {
            var dlg = new FolderSelectDialog();
            dlg.Title = "Please select a new Backup path.";
            dlg.InitialDirectory = "";

            if (dlg.ShowDialog(View.ParentForm.Handle))
                View.BackupPath = dlg.FileName;

            // TODO: Save new backuppath.
        }

        public static void OpenBackupPath()
        {
            if (!string.IsNullOrEmpty(View.BackupPath))
                OptionsController.OpenFolder(View.BackupPath);
        }

        public static void NewBackup()
        {

        }

        public static void BackupSaves()
        {

        }

        public static void RemoveBackup()
        {

        }

        public static void RemoveAllBackups()
        {

        }

        public static void RecoverBackup()
        {

        }
    }
}
