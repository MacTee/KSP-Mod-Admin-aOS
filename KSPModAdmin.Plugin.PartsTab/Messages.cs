using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.PartsTab
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Messages
    {
        public static string MSG_PARTSTAB_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PARTSTAB_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PARTSTAB_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PARTSTAB_VIEW_TITLE = "Parts";


        // Add more messages here and use them in your code to support localization.
        // When you add new messages here you have to also add new line to all language files (here: KSPMA.TemplatePlugin.eng.lang and KSPMA.TemplatePlugin.fake.lang)
        // See the MSG_PLUGIN_VIEW_TITLE for a sample.
        public static string MSG_PARTS_COUNT_TEXT
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PARTS_COUNT_TEXT"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PARTS_COUNT_TEXT).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PARTS_COUNT_TEXT = "{0} ({1}) Parts";

        public static string MSG_PART_SCAN_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_SCAN_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_SCAN_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_SCAN_STARTED = "Part scan started...";

        public static string MSG_PART_SCAN_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_SCAN_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_SCAN_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_SCAN_DONE = "Part scan done.";

        public static string MSG_SCAN_FILE_0_FOR_PARTS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SCAN_FILE_0_FOR_PARTS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SCAN_FILE_0_FOR_PARTS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SCAN_FILE_0_FOR_PARTS = "Scan file \"{0}\" for parts...";

        public static string MSG_NO_PARTCFG_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_PARTCFG_FOUND_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_PARTCFG_FOUND_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_PARTCFG_FOUND_0 = "No part.cfg files found in \"{0}\".";

        public static string MSG_ERROR_DURING_PART_READING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_PART_READING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_PART_READING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_PART_READING_0 = "Error during part reading! \"{0}\"";

        public static string MSG_ERROR_DURING_PART_READING_0_UNEXPECTED_EMPTY_LINE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_PART_READING_0_UNEXPECTED_EMPTY_LINE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_PART_READING_0_UNEXPECTED_EMPTY_LINE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_PART_READING_0_UNEXPECTED_EMPTY_LINE = "Error during part reading \"{0}\"^Enexpected 'null' line.";

        public static string MSG_PART_FOUND_AND_ADDED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_FOUND_AND_ADDED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_FOUND_AND_ADDED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_FOUND_AND_ADDED_0 = "Part found and added \"{0}\".";

        public static string MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH = "Error during part reading \"{0}\"^Name / title / category parameter missmatch.";

        public static string MSG_PART_NOT_FROM_MOD_DELETE_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_NOT_FROM_MOD_DELETE_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_NOT_FROM_MOD_DELETE_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_NOT_FROM_MOD_DELETE_WARNING = "The part you are trying to delete is not from a mod.^Do you want to delete the part permanetly?";

        public static string MSG_PART_USED_DELETE_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_USED_DELETE_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_USED_DELETE_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_USED_DELETE_WARNING = "The part you are trying to delete is used by the following craft(s):";

        public static string MSG_DELETE_ANYWAY
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETE_ANYWAY"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETE_ANYWAY).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DELETE_ANYWAY = "Delete it anyway?";

        public static string MSG_DIR_0_OF_PART_1_DELETED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_0_OF_PART_1_DELETED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_0_OF_PART_1_DELETED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_0_OF_PART_1_DELETED = "Directory \"{0}\" of part \"{1}\" deleted.";

        public static string MSG_MODNODE_0_UNCHECKED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MODNODE_0_UNCHECKED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODNODE_0_UNCHECKED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODNODE_0_UNCHECKED = "ModNode \"{0}\" unchecked.";

        public static string MSG_NAME_OF_PART_0_CHANGED_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NAME_OF_PART_0_CHANGED_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NAME_OF_PART_0_CHANGED_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NAME_OF_PART_0_CHANGED_1 = "Name of part \"{0}\" changed to \"{1}\".";

        public static string MSG_TITLE_OF_PART_0_CHANGED_FROM_1_TO_2
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_TITLE_OF_PART_0_CHANGED_FROM_1_TO_2"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_TITLE_OF_PART_0_CHANGED_FROM_1_TO_2).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_TITLE_OF_PART_0_CHANGED_FROM_1_TO_2 = "Title of part \"{0}\" changed from \"{1}\" to \"{2}\".";

        public static string MSG_CATEGORY_OF_PART_0_CHANGED_FROM_1_TO_2
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CATEGORY_OF_PART_0_CHANGED_FROM_1_TO_2"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CATEGORY_OF_PART_0_CHANGED_FROM_1_TO_2).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CATEGORY_OF_PART_0_CHANGED_FROM_1_TO_2 = "Category of part \"{0}\" changed from \"{1}\" to \"{2}\".";
    }
}