using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.FlagsTab
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_FLAGS_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FLAGS_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FLAGS_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FLAGS_VIEW_TITLE = "Flags";

        // Add more messages here and use them in your code to support localization.
        // When you add new messages here you have to also add new line to all language files (here: KSPMA.TemplatePlugin.eng.lang and KSPMA.TemplatePlugin.fake.lang)
        // See the MSG_PLUGIN_VIEW_TITLE for a sample.
        public static string MSG_FLAG_SCAN_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FLAG_SCAN_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FLAG_SCAN_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FLAG_SCAN_STARTED = "Flag scan started ...";

        public static string MSG_ERROR_DURING_FLAG_SCAN
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_FLAG_SCAN"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_FLAG_SCAN).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_FLAG_SCAN = "Error during flag scan! Scan aborded!";

        public static string MSG_FLAG_0_ADDED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FLAG_0_ADDED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FLAG_0_ADDED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FLAG_0_ADDED = "Flag \"{0}\" added!";

        public static string MSG_ERROR_FLAG_0_ADD_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_FLAG_0_ADD_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_FLAG_0_ADD_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_FLAG_0_ADD_FAILED = "Error! Adding of flag \"{0}\" aborded!";

        public static string MSG_DELETE_EXISTING_FLAG_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETE_EXISTING_FLAG_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETE_EXISTING_FLAG_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DELETE_EXISTING_FLAG_0 = "Deleting existing flag \"{0}\".";

        public static string MSG_ADJUSTING_FLAG_SIZE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADJUSTING_FLAG_SIZE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADJUSTING_FLAG_SIZE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ADJUSTING_FLAG_SIZE = "Adjusting flag size to 256x160...";

        public static string MSG_SAVING_FLAG_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SAVING_FLAG_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SAVING_FLAG_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SAVING_FLAG_0 = "Saving new flag \"{0}\"";

        public static string MSG_COPY_FLAG_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_COPY_FLAG_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_COPY_FLAG_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_COPY_FLAG_0 = "Copy flag \"{0}\"";

        public static string MSG_ERROR_FLAG_CREATION_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_FLAG_CREATION_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_FLAG_CREATION_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_FLAG_CREATION_FAILED = "Error! Flag creation faild!";

        public static string MSG_CREATING_DIR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CREATING_DIR_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CREATING_DIR_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CREATING_DIR_0 = "Creating directory \"{0}\".";

        public static string MSG_FLAG_0_DELETED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FLAG_0_DELETED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FLAG_0_DELETED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FLAG_0_DELETED = "Flag \"{0}\" deleted.";

        public static string MSG_ERROR_DELETE_FLAG_0_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DELETE_FLAG_0_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DELETE_FLAG_0_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DELETE_FLAG_0_FAILED = "Error! Deleting flag \"{0}\" failed!";

        public static string MSG_REALY_DELETE_FLAG_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_REALY_DELETE_FLAG_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REALY_DELETE_FLAG_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REALY_DELETE_FLAG_0 = "Do you realy want to delete the flag \"{0}\"?";
    }
}