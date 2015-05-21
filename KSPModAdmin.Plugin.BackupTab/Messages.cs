using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.BackupTab
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_BACKUPTAB_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUPTAB_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUPTAB_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUPTAB_VIEW_TITLE = "Backup";


        //// Add more messages here and use them in your code to support localization.
        //// When you add new messages here you have to also add new line to all language files (here: KSPMA.TemplatePlugin.eng.lang and KSPMA.TemplatePlugin.fake.lang)
        //// See the MSG_PLUGIN_VIEW_TITLE for a sample.

        public static string MSG_BACKUP_FOLDER_NOT_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_FOLDER_NOT_FOUND_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_FOLDER_NOT_FOUND_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_FOLDER_NOT_FOUND_0 = "Backup folder not found! \"{0}\"";

        public static string MSG_BACKUP_LOAD_ERROR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_LOAD_ERROR_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_LOAD_ERROR_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_LOAD_ERROR_0 = "Error during backup loading: \"{0}\"";

        public static string MSG_BACKUP_SELECT_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_SELECT_FOLDER"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_SELECT_FOLDER).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_SELECT_FOLDER = "Please select a backup folder.";

        public static string MSG_BACKUP_SRC_FOLDER_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_SRC_FOLDER_NOT_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_SRC_FOLDER_NOT_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_SRC_FOLDER_NOT_FOUND = "Folder to backup not found! \"{0}\"";

        public static string MSG_BACKUP_ERROR
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_ERROR"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_ERROR).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_ERROR = "Error during backup creation: \"{0}\"";

        public static string MSG_BACKUP_CREATION_ERROR
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_CREATION_ERROR"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_CREATION_ERROR).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_CREATION_ERROR = "Error during backup creation.";

        public static string MSG_BACKUP_COMPLETE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_COMPLETE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_COMPLETE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_COMPLETE = "Backup of \"{0}\" complete.";

        public static string MSG_BACKUP_DELETE_QUESTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_DELETE_QUESTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_DELETE_QUESTION).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_DELETE_QUESTION = "Are you sure to delete the backup \"{0}\"?";

        public static string MSG_BACKUP_DELETED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_DELETED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_DELETED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_DELETED = "Backup deleted: \"{0}\"";

        public static string MSG_BACKUP_DELETED_ERROR
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_DELETED_ERROR"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_DELETED_ERROR).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_DELETED_ERROR = "Can not delete backup \"{0}\".";

        public static string MSG_BACKUP_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_NOT_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_NOT_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_NOT_FOUND = "Backup file not found! \"{0}\"";

        public static string MSG_BACKUP_DELETE_ALL_QUESTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_DELETE_ALL_QUESTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_DELETE_ALL_QUESTION).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_DELETE_ALL_QUESTION = "Are you sure to delete all backups?";


        public static string MSG_BACKUP_REINSTALLED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_REINSTALLED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_REINSTALLED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_REINSTALLED = "Backup reinstalled! \"{0}\"";

        public static string MSG_FOLDER_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FOLDER_NOT_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FOLDER_NOT_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FOLDER_NOT_FOUND = "Folder not found \"{0}\".";

        public static string MSG_BACKUP_SRC_MISSING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_SRC_MISSING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_SRC_MISSING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_SRC_MISSING = "Select a Backup first!";

        public static string MSG_CREATE_NEW_BACKUP_CFG
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CREATE_NEW_BACKUP_CFG"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CREATE_NEW_BACKUP_CFG).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CREATE_NEW_BACKUP_CFG = "Create new Backup configuration file \"{0}\".";

        public static string MSG_BACKUP_LOAD_CFG
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_LOAD_CFG"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_LOAD_CFG).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_LOAD_CFG = "Loading backup configuration file \"{0}\" ...";

        public static string MSG_BACKUP_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BACKUP_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BACKUP_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BACKUP_STARTED = "Backup started...";
    }
}