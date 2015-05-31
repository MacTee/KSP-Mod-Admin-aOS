using System;
using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab
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


        public static string MSG_CRAFTSTAB_VIEW_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFTSTAB_VIEW_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFTSTAB_VIEW_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFTSTAB_VIEW_TITLE = "Crafts";

        public static string MSG_CRAFTS_COUNT_TEXT
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFTS_COUNT_TEXT"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFTS_COUNT_TEXT).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFTS_COUNT_TEXT = "{0} ({1}) Crafts";

        public static string MSG_CRAFT_SCAN_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_SCAN_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_SCAN_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_SCAN_STARTED = "Crafts scan started...";

        public static string MSG_CRAFT_SCAN_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_SCAN_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_SCAN_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_SCAN_DONE = "Crafts scan done.";

        public static string MSG_SCAN_FILE_0_FOR_CRAFTS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SCAN_FILE_0_FOR_CRAFTS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SCAN_FILE_0_FOR_CRAFTS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SCAN_FILE_0_FOR_CRAFTS = "Scan file \"{0}\" for crafts...";

        public static string MSG_NO_CRAFTCFG_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_CRAFTCFG_FOUND_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_CRAFTCFG_FOUND_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_CRAFTCFG_FOUND_0 = "No *.craft files found in \"{0}\".";

        public static string MSG_ERROR_DURING_CRAFT_READING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_CRAFT_READING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_CRAFT_READING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_CRAFT_READING_0 = "Error during part reading! \"{0}\"";


        public static string MSG_ERROR_DURING_CRAFT_READING_0_UNEXPECTED_EMPTY_LINE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_CRAFT_READING_0_UNEXPECTED_EMPTY_LINE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_CRAFT_READING_0_UNEXPECTED_EMPTY_LINE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_CRAFT_READING_0_UNEXPECTED_EMPTY_LINE = "Error during craft reading \"{0}\"^Enexpected 'null' line.";

        public static string MSG_PART_0_ADDED_TO_CRAFT_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PART_0_ADDED_TO_CRAFT_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PART_0_ADDED_TO_CRAFT_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PART_0_ADDED_TO_CRAFT_1 = "Part \"{0}\" added to craft \"{1}\".";

        public static string MSG_PARTCOUNT_FOR_PART_0_IN_CRAFT_1_CHANGED_TO_2
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PARTCOUNT_FOR_PART_0_IN_CRAFT_1_CHANGED_TO_2"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PARTCOUNT_FOR_PART_0_IN_CRAFT_1_CHANGED_TO_2).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PARTCOUNT_FOR_PART_0_IN_CRAFT_1_CHANGED_TO_2 = "Part count of part \"{0}\" in craft \"{1}\" changed to {2}.";

        public static string MSG_CRAFT_VALIDATION_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_VALIDATION_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_VALIDATION_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_VALIDATION_STARTED = "Crafts validation started...";

        public static string MSG_CRAFT_VALIDATION_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_VALIDATION_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_VALIDATION_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_VALIDATION_DONE = "Crafts validation done.";

        public static string MSG_VALIDATING_CRAFT_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_VALIDATING_CRAFT_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_VALIDATING_CRAFT_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_VALIDATING_CRAFT_0 = "Validating craft \"{0}\"...";

        public static string MSG_VALIDATING_CRAFT_0_SUCCESSFUL
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_VALIDATING_CRAFT_0_SUCCESSFUL"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_VALIDATING_CRAFT_0_SUCCESSFUL).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_VALIDATING_CRAFT_0_SUCCESSFUL = "Validation successful for craft \"{0}\".";

        public static string MSG_VALIDATING_CRAFT_0_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_VALIDATING_CRAFT_0_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_VALIDATING_CRAFT_0_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_VALIDATING_CRAFT_0_FAILED = "Validation failed for craft \"{0}\"!";

        public static string MSG_ERROR_DURING_CRAFT_VALIDATION_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_CRAFT_VALIDATION_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_CRAFT_VALIDATION_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_CRAFT_VALIDATION_0 = "Error during craft validation. \"{0}\"";

        public static string MSG_BUILDING_OF_CRAFT_0_SWAPPED_1_2
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_BUILDING_OF_CRAFT_0_SWAPPED_1_2"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_BUILDING_OF_CRAFT_0_SWAPPED_1_2).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_BUILDING_OF_CRAFT_0_SWAPPED_1_2 = "Building of craft \"{0}\" swapped from \"{1}\" to \"{2}\".";

        public static string MSG_CRAFT_0_DELETED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_0_DELETED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_0_DELETED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_0_DELETED = "Craft \"{0}\" deleted.";

        public static string MSG_CRAFT_NOT_FROM_MOD_DELETE_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CRAFT_NOT_FROM_MOD_DELETE_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CRAFT_NOT_FROM_MOD_DELETE_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CRAFT_NOT_FROM_MOD_DELETE_WARNING = "The craft you are trying to delete is not from a mod.^Do you want to delete the craft permanetly?";
        
        public static string MSG_PARTTAB_UPDATED_PART_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PARTTAB_UPDATED_PART_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PARTTAB_UPDATED_PART_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_PARTTAB_UPDATED_PART_0 = "Part \"{0}\" on PartTab updated.";

        public static string MSG_MODSELECTION_UPDATED_PART_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MODSELECTION_UPDATED_PART_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODSELECTION_UPDATED_PART_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODSELECTION_UPDATED_PART_0 = "Part \"{0}\" in ModSelection updated.";
    }
}