using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.ModBrowserTab
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_MODBROWSER_VIEW_TITLE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_MODBROWSER_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODBROWSER_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODBROWSER_VIEW_TITLE = "Mod Browser";
        public static string MSG_MODBROWSER_CKAN_VIEW_TITLE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_MODBROWSER_CKAN_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODBROWSER_CKAN_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODBROWSER_CKAN_VIEW_TITLE = "CKAN";
        public static string MSG_MODBROWSER_KERBALSTUFF_VIEW_TITLE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_MODBROWSER_KERBALSTUFF_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODBROWSER_KERBALSTUFF_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODBROWSER_KERBALSTUFF_VIEW_TITLE = "KerbalStuff";


        // Add more messages here and use them in your code to support localization.
        // When you add new messages here you have to also add new line to all language files (here: KSPMA.TemplatePlugin.eng.lang and KSPMA.TemplatePlugin.fake.lang)
        // See the MSG_PLUGIN_VIEW_TITLE for a sample.
        public static string MSG_MODBROWSER_CKAN_COUNT_TEXT
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_MODBROWSER_CKAN_COUNT_TEXT"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODBROWSER_CKAN_COUNT_TEXT).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODBROWSER_CKAN_COUNT_TEXT = "{0} ({1}) Mods";



        public static string MSG_REGISTER_MODBROWSER_0
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REGISTER_MODBROWSER_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REGISTER_MODBROWSER_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REGISTER_MODBROWSER_0 = "Register ModBrowser \"{0}\".";

        public static string MSG_REMOVING_MODBROWSER_0
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REMOVING_MODBROWSER_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REMOVING_MODBROWSER_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REMOVING_MODBROWSER_0 = "Removing ModBrowser \"{0}\".";

        public static string MSG_REFRESHING_REPOSITORIES
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REFRESHING_REPOSITORIES"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REFRESHING_REPOSITORIES).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REFRESHING_REPOSITORIES = "Refreshing repositories ...";

        public static string MSG_ERROR_DURING_REFRESH_REPOSITORIES_0
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_ERROR_DURING_REFRESH_REPOSITORIES_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_REFRESH_REPOSITORIES_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_REFRESH_REPOSITORIES_0 = "Error during refreshing repositories! \"{0}\"";

        public static string MSG_REFRESHING_REPOSITORIES_DONE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REFRESHING_REPOSITORIES_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REFRESHING_REPOSITORIES_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REFRESHING_REPOSITORIES_DONE = "Refreshing repositories done.";

        public static string MSG_DOWNLOADPATH_MISSING
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_DOWNLOADPATH_MISSING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOADPATH_MISSING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOADPATH_MISSING = "Download path missing.";

        public static string MSG_REFRESHING_REPOSITORY_ARCHIVE_0
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REFRESHING_REPOSITORY_ARCHIVE_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REFRESHING_REPOSITORY_ARCHIVE_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REFRESHING_REPOSITORY_ARCHIVE_0 = "Refreshing repository archive \"{0}\"...";

        public static string MSG_USING_CACHED_ARCHIVE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_USING_CACHED_ARCHIVE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_USING_CACHED_ARCHIVE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_USING_CACHED_ARCHIVE = "Using cached archive.";

        public static string MSG_CREATE_CKAN_ARCHIVE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_CREATE_CKAN_ARCHIVE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CREATE_CKAN_ARCHIVE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CREATE_CKAN_ARCHIVE = "Create CKAN archives folder.";

        public static string MSG_ERROR_DURING_REFRESH_REPOSITORY_ARCHIVE_0
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_ERROR_DURING_REFRESH_REPOSITORY_ARCHIVE_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_REFRESH_REPOSITORY_ARCHIVE_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_REFRESH_REPOSITORY_ARCHIVE_0 = "Error during refreshing repository archive! \"{0}\"";

        public static string MSG_REFRESH_REPOSITORY_DONE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_REFRESH_REPOSITORY_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REFRESH_REPOSITORY_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REFRESH_REPOSITORY_DONE = "Refreshing repository archive done.";

        public static string MSG_PROCESSING_STARTED
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_PROCESSING_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PROCESSING_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PROCESSING_STARTED = "Processing changes started...";

        public static string MSG_PROCESSING_DONE
        {
            get
            {
                var msg = Localizer.GlobalInstance["MSG_PROCESSING_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PROCESSING_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PROCESSING_DONE = "Processing changes done.";
    }
}