using System;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core
{
    public class Messages
    {
        public static string MSG_CURRENT_VERSION_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CURRENT_VERSION_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CURRENT_VERSION_0;
            }
        }
        private const string DEFAULT_MSG_CURRENT_VERSION_0 = "Current version: {0}";

        public static string MSG_PLS_ENTER_VALID_ARCHIVE_URL
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PLS_ENTER_VALID_ARCHIVE_URL"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PLS_ENTER_VALID_ARCHIVE_URL;
            }
        }
        private const string DEFAULT_MSG_PLS_ENTER_VALID_ARCHIVE_URL = "Please enter a valid archive path or URL.";

        public static string MSG_TITLE_ATTENTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_TITLE_ATTENTION"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_TITLE_ATTENTION;
            }
        }
        private const string DEFAULT_MSG_TITLE_ATTENTION = "Attention!";

        public static string MSG_TITLE_ERROR
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_TITLE_ERROR"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_TITLE_ERROR;
            }
        }
        private const string DEFAULT_MSG_TITLE_ERROR = "Error!";

        public static string MSG_DOWNLOAD_PATH_CHANGED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_PATH_CHANGED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_PATH_CHANGED_0;
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_PATH_CHANGED_0 = "Download path changed to \"{0}\".";

        public static string MSG_STARTING_KSP
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_STARTING_KSP"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_STARTING_KSP;
            }
        }
        private const string DEFAULT_MSG_STARTING_KSP = "Starting KSP...";

        public static string MSG_CANT_FIND_KSP_EXE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CANT_FIND_KSP_EXE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CANT_FIND_KSP_EXE;
            }
        }
        private const string DEFAULT_MSG_CANT_FIND_KSP_EXE = "Can't find KSP.exe.";

        public static string MSG_KSP_LAUNCH_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_LAUNCH_FAILED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_LAUNCH_FAILED;
            }
        }
        private const string DEFAULT_MSG_KSP_LAUNCH_FAILED = "Launching KSP failed.";

        public static string MSG_UPDATE_KSP_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UPDATE_KSP_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UPDATE_KSP_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_UPDATE_KSP_SETTINGS = "KSP settings.cfg file updated.";

        public static string MSG_CANT_FIND_KSP_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CANT_FIND_KSP_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CANT_FIND_KSP_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_CANT_FIND_KSP_SETTINGS = "Can't find KSP settings.cfg file.";

        public static string MSG_ADDING_MOD_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADDING_MOD_FAILED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADDING_MOD_FAILED;
            }
        }
        private const string DEFAULT_MSG_ADDING_MOD_FAILED = "Adding Mod aborted.";

        public static string MSG_ADD_MODS_FAILED_PARAM_EMPTY_MODINFOS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADD_MODS_FAILED_PARAM_EMPTY_MODINFOS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADD_MODS_FAILED_PARAM_EMPTY_MODINFOS;
            }
        }
        private const string DEFAULT_MSG_ADD_MODS_FAILED_PARAM_EMPTY_MODINFOS = "Add mod(s) failed! Parameter \"modInfos\" is empty.";

        public static string MSG_MOD_ALREADY_ADDED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ALREADY_ADDED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ALREADY_ADDED;
            }
        }
        private const string DEFAULT_MSG_MOD_ALREADY_ADDED = "Mod already added: \"{0}\"";

        public static string MSG_SHOULD_MOD_REPLACED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SHOULD_MOD_REPLACED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SHOULD_MOD_REPLACED;
            }
        }
        private const string DEFAULT_MSG_SHOULD_MOD_REPLACED = "Should the mod be replaced?";

        public static string MSG_REPLACING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_REPLACING_MOD_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REPLACING_MOD_0;
            }
        }
        private const string DEFAULT_MSG_REPLACING_MOD_0 = "Replacing mod \"{0}\"";

        public static string MSG_MOD_0_REPLACED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_0_REPLACED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_0_REPLACED;
            }
        }
        private const string DEFAULT_MSG_MOD_0_REPLACED = "Mod \"{0}\" replaced.";

        public static string MSG_ADD_MOD_FAILED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADD_MOD_FAILED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADD_MOD_FAILED_0;
            }
        }
        private const string DEFAULT_MSG_ADD_MOD_FAILED_0 = "Mod add failed \"{0}\"!";

        public static string MSG_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DONE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DONE;
            }
        }
        private const string DEFAULT_MSG_DONE = "Done!";

        public static string MSG_ADD_MODS_FAILED_PARAM_EMPTY_FILENAMES
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADD_MODS_FAILED_PARAM_EMPTY_FILENAMES"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADD_MODS_FAILED_PARAM_EMPTY_FILENAMES;
            }
        }
        private const string DEFAULT_MSG_ADD_MODS_FAILED_PARAM_EMPTY_FILENAMES = "Add mod(s) failed! Parameter \"filenames\" is empty.";

        public static string MSG_MOD_ADDED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ADDED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ADDED_0;
            }
        }
        private const string DEFAULT_MSG_MOD_ADDED_0 = "Mod added: \"{0}\"";

        public static string MSG_MOD_ERROR_WHILE_READ_ZIP_0_ERROR_MSG_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ERROR_WHILE_READ_ZIP_0_ERROR_MSG_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ERROR_WHILE_READ_ZIP_0_ERROR_MSG_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MOD_ERROR_WHILE_READ_ZIP_0_ERROR_MSG_1 = "Error while reading Mod Zip-File \"{0}\"^Error message: {1}";

        public static string MSG_ROOT_NOT_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ROOT_NOT_FOUND_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ROOT_NOT_FOUND_0;
            }
        }
        private const string DEFAULT_MSG_ROOT_NOT_FOUND_0 = "Root of Zip not found! \"{0}\".";

        public static string MSG_MOD_ZIP_NOT_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ZIP_NOT_FOUND_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ZIP_NOT_FOUND_0;
            }
        }
        private const string DEFAULT_MSG_MOD_ZIP_NOT_FOUND_0 = "Mod Zip-File not found \"{0}\"";

        public static string MSG_FILE_ADDED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_ADDED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_ADDED_0;
            }
        }
        private const string DEFAULT_MSG_FILE_ADDED_0 = "File added \"{0}\".";

        public static string MSG_DIR_ADDED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_ADDED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_ADDED_0;
            }
        }
        private const string DEFAULT_MSG_DIR_ADDED_0 = "Directory added \"{0}\".";

        public static string MSG_REMOVING_OUTDATED_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_REMOVING_OUTDATED_MOD_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REMOVING_OUTDATED_MOD_0;
            }
        }
        private const string DEFAULT_MSG_REMOVING_OUTDATED_MOD_0 = "Removing outdated mod \"{0}\"";

        public static string MSG_ADDING_UPDATED_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADDING_UPDATED_MOD_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADDING_UPDATED_MOD_0;
            }
        }
        private const string DEFAULT_MSG_ADDING_UPDATED_MOD_0 = "Adding updated mod \"{0}\"";

        public static string MSG_DIR_DELETED_ERROR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_DELETED_ERROR_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_DELETED_ERROR_0;
            }
        }
        private const string DEFAULT_MSG_DIR_DELETED_ERROR_0 = "Can not delete directory \"{0}\".";

        public static string MSG_DIR_DELETED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_DELETED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_DELETED_0;
            }
        }
        private const string DEFAULT_MSG_DIR_DELETED_0 = "Directory deleted \"{0}\".";

        public static string MSG_MOD_ERROR_CANT_DELETE_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ERROR_CANT_DELETE_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ERROR_CANT_DELETE_0;
            }
        }
        private const string DEFAULT_MSG_MOD_ERROR_CANT_DELETE_0 = "Can't delete. \"{0}\"";

        public static string MSG_MOD_ERROR_ZIP_CREATION_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_ERROR_ZIP_CREATION_FAILED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_ERROR_ZIP_CREATION_FAILED;
            }
        }
        private const string DEFAULT_MSG_MOD_ERROR_ZIP_CREATION_FAILED = "ZipFile creation for download failed.";

        public static string MSG_SELECT_0_FOLDER_FIRST
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECT_0_FOLDER_FIRST"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECT_0_FOLDER_FIRST;
            }
        }
        private const string DEFAULT_MSG_SELECT_0_FOLDER_FIRST = "Please select a {0} folder first.";

        public static string MSG_SELECT_KSP_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECT_KSP_FOLDER"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECT_KSP_FOLDER;
            }
        }
        private const string DEFAULT_MSG_SELECT_KSP_FOLDER = "Select a KSP install folder please.";

        public static string MSG_SELECTED_KSP_FOLDER_CHANGED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECTED_KSP_FOLDER_CHANGED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECTED_KSP_FOLDER_CHANGED_0;
            }
        }
        private const string DEFAULT_MSG_SELECTED_KSP_FOLDER_CHANGED_0 = "Selected KSP install path changed to \"{0}\"";
        
        public static string MSG_NOT_KSP_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NOT_KSP_FOLDER"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NOT_KSP_FOLDER;
            }
        }
        private const string DEFAULT_MSG_NOT_KSP_FOLDER = "This is not a KSP install path.";

        public static string MSG_OPENING_0_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_OPENING_0_FOLDER"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_OPENING_0_FOLDER;
            }
        }
        private const string DEFAULT_MSG_OPENING_0_FOLDER = "Opening {0} folder...";

        public static string MSG_OPEN_0_FOLDER_FAILD
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_OPEN_0_FOLDER_FAILD"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_OPEN_0_FOLDER_FAILD;
            }
        }
        private const string DEFAULT_MSG_OPEN_0_FOLDER_FAILD = "Open {0} folder faild.";

        public static string MSG_SELECT_DOWNLOAD_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECT_DOWNLOAD_FOLDER"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECT_DOWNLOAD_FOLDER;
            }
        }
        private const string DEFAULT_MSG_SELECT_DOWNLOAD_FOLDER = "Select the folder to download the KSP mods to.";

        public static string MSG_DOWNLOADPATH_CHANGED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOADPATH_CHANGED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOADPATH_CHANGED_0;
            }
        }
        private const string DEFAULT_MSG_DOWNLOADPATH_CHANGED_0 = "Download path changed to \"{0}\".";

        public static string MSG_KSP_FOLDER_ADDED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_FOLDER_ADDED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_FOLDER_ADDED_0;
            }
        }
        private const string DEFAULT_MSG_KSP_FOLDER_ADDED_0 = "KSP install folder added. \"{0}\"";

        public static string MSG_KSP_FOLDER_REMOVED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_FOLDER_REMOVED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_FOLDER_REMOVED_0;
            }
        }
        private const string DEFAULT_MSG_KSP_FOLDER_REMOVED_0 = "KSP install folder removed. \"{0}\"";

        public static string MSG_STEAM_SEARCH_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_STEAM_SEARCH_STARTED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_STEAM_SEARCH_STARTED;
            }
        }
        private const string DEFAULT_MSG_STEAM_SEARCH_STARTED = "Steam - KSP install folder search started...";

        public static string MSG_STEAM_FOLDER_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_STEAM_FOLDER_NOT_FOUND"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_STEAM_FOLDER_NOT_FOUND;
            }
        }
        private const string DEFAULT_MSG_STEAM_FOLDER_NOT_FOUND = "Steam folder not found!";

        public static string MSG_STEAM_SEARCH_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_STEAM_SEARCH_DONE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_STEAM_SEARCH_DONE;
            }
        }
        private const string DEFAULT_MSG_STEAM_SEARCH_DONE = "Steam - KSP install folder search done.";

        public static string MSG_KSP_SEARCH_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_SEARCH_STARTED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_SEARCH_STARTED;
            }
        }
        private const string DEFAULT_MSG_KSP_SEARCH_STARTED = "KSP install folder search started...";

        public static string MSG_KSP_FOLDER_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_FOLDER_NOT_FOUND"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_FOLDER_NOT_FOUND;
            }
        }
        private const string DEFAULT_MSG_KSP_FOLDER_NOT_FOUND = "KSP install folder not found!";

        public static string MSG_KSP_SEARCH_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_SEARCH_DONE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_SEARCH_DONE;
            }
        }
        private const string DEFAULT_MSG_KSP_SEARCH_DONE = "KSP install folder search done.";

        public static string MSG_FOLDER_SEARCH_ABORTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FOLDER_SEARCH_ABORTED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FOLDER_SEARCH_ABORTED;
            }
        }
        private const string DEFAULT_MSG_FOLDER_SEARCH_ABORTED = "KSP install folder search aborted.";

        public static string MSG_ERROR_DURING_FOLDER_SEARCH_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_FOLDER_SEARCH_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_FOLDER_SEARCH_0;
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_FOLDER_SEARCH_0 = "Error during folder search \"{0}\"";

        public static string MSG_ERROR_ADD_FOLDER_FAILED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_ADD_FOLDER_FAILED_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_ADD_FOLDER_FAILED_0;
            }
        }
        private const string DEFAULT_MSG_ERROR_ADD_FOLDER_FAILED_0 = "KSP install folder add failed! \"{0}\"";

        public static string MSG_ERROR_NOT_A_KSP_FOLDER_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_NOT_A_KSP_FOLDER_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_NOT_A_KSP_FOLDER_0;
            }
        }
        private const string DEFAULT_MSG_ERROR_NOT_A_KSP_FOLDER_0 = "The folder is not a KSP install directory. \"{0}\"";

        public static string MSG_KSP_FOLDER_FOUND_DIALOG_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_FOLDER_FOUND_DIALOG_TITLE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_FOLDER_FOUND_DIALOG_TITLE;
            }
        }
        private const string DEFAULT_MSG_KSP_FOLDER_FOUND_DIALOG_TITLE = "KSP folder found.";

        public static string MSG_KSP_FOLDER_FOUND_DIALOG_TEXT
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_FOLDER_FOUND_DIALOG_TEXT"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_FOLDER_FOUND_DIALOG_TEXT).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_KSP_FOLDER_FOUND_DIALOG_TEXT = "KSP folder found.^\"{0}\"^^Should this folder be added (yes)?^continue search (no)?^or cancel search(cancel)?";

        public static string MSG_NEW_VERSION_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NEW_VERSION_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NEW_VERSION_0;
            }
        }
        private const string DEFAULT_MSG_NEW_VERSION_0 = "New version {0} available.";

        public static string MSG_KSP_UPTODATE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_UPTODATE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_UPTODATE;
            }
        }
        private const string DEFAULT_MSG_KSP_UPTODATE = "KSP Mod Admin is up to date.";
        
        public static string MSG_ERROR_WHILE_STEAM_SEARCH
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_WHILE_STEAM_SEARCH"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_WHILE_STEAM_SEARCH;
            }
        }
        private const string DEFAULT_MSG_ERROR_WHILE_STEAM_SEARCH = "Error during Steam folder search.";
        
        public static string MSG_ERROR_WHILE_FOLDER_SEARCH
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_WHILE_FOLDER_SEARCH"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_WHILE_FOLDER_SEARCH;
            }
        }
        private const string DEFAULT_MSG_ERROR_WHILE_FOLDER_SEARCH = "Error during folder search.";
        
        public static string MSG_DOWNLOAD_COMPLETE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_COMPLETE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_COMPLETE;
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_COMPLETE = "Download complete.";

        public static string MSG_DOWNLOAD_COMPLETE_INSTALL
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_COMPLETE_INSTALL"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_COMPLETE_INSTALL).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_COMPLETE_INSTALL = "The download is complete.^Do you want to auto install the update?";
        
        public static string MSG_KSPMA_UPDATE_CHECK_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSPMA_UPDATE_CHECK_STARTED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSPMA_UPDATE_CHECK_STARTED;
            }
        }
        private const string DEFAULT_MSG_KSPMA_UPDATE_CHECK_STARTED = "KSP Mod Admin update check started.";

        public static string MSG_CONFLICT_DETECTION_ON
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CONFLICT_DETECTION_ON"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CONFLICT_DETECTION_ON;
            }
        }
        private const string DEFAULT_MSG_CONFLICT_DETECTION_ON = "ConflictDetection On";

        public static string MSG_CONFLICT_DETECTION_OFF
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CONFLICT_DETECTION_OFF"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CONFLICT_DETECTION_OFF;
            }
        }
        private const string DEFAULT_MSG_CONFLICT_DETECTION_OFF = "ConflictDetection Off";

        public static string MSG_CHECKING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CHECKING"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CHECKING;
            }
        }
        private const string DEFAULT_MSG_CHECKING = "Checking ...";

        public static string MSG_DOWNLOADING_UPDATE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOADING_UPDATE"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOADING_UPDATE;
            }
        }
        private const string DEFAULT_MSG_DOWNLOADING_UPDATE = "Downloading update ...";

        public static string MSG_CHECK_FOR_KSPMA_UPDATES
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CHECK_FOR_KSPMA_UPDATES"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CHECK_FOR_KSPMA_UPDATES;
            }
        }
        private const string DEFAULT_MSG_CHECK_FOR_KSPMA_UPDATES = "Check for KSP MA updates";
        
        public static string MSG_ONLY_ONE_INSTANCE_OF_CONTROLLER_ALLOWED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ONLY_ONE_INSTANCE_OF_CONTROLLER_ALLOWED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ONLY_ONE_INSTANCE_OF_CONTROLLER_ALLOWED;
            }
        }
        private const string DEFAULT_MSG_ONLY_ONE_INSTANCE_OF_CONTROLLER_ALLOWED = "Only one instance of the Controller is allowed!";

        public static string MSG_CANT_CREATE_KSPMA_LOG
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CANT_CREATE_KSPMA_LOG"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CANT_CREATE_KSPMA_LOG;
            }
        }
        private const string DEFAULT_MSG_CANT_CREATE_KSPMA_LOG = "Can't create KSP MA log file. Error logging will be turned off.";

        public static string MSG_LOADING_KSPMA_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_LOADING_KSPMA_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_LOADING_KSPMA_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_LOADING_KSPMA_SETTINGS = "Loading KSPModAdmin settings ...";

        public static string MSG_LOADING_KSPMA_SETTINGS_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_LOADING_KSPMA_SETTINGS_FAILED"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_LOADING_KSPMA_SETTINGS_FAILED;
            }
        }
        private const string DEFAULT_MSG_LOADING_KSPMA_SETTINGS_FAILED = "Loading of KSPModAdmin settings failed!";

        public static string MSG_KSPMA_SETTINGS_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSPMA_SETTINGS_NOT_FOUND"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSPMA_SETTINGS_NOT_FOUND;
            }
        }
        private const string DEFAULT_MSG_KSPMA_SETTINGS_NOT_FOUND = "No KSP Mod Admin settings found!";

        public static string MSG_LOADING_KSP_MOD_CONFIGURATION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_LOADING_KSP_MOD_CONFIGURATION"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_LOADING_KSP_MOD_CONFIGURATION;
            }
        }
        private const string DEFAULT_MSG_LOADING_KSP_MOD_CONFIGURATION = "Loading KSP mod configuration ...";

        public static string MSG_KSP_MOD_CONFIGURATION_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_MOD_CONFIGURATION_NOT_FOUND"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_MOD_CONFIGURATION_NOT_FOUND;
            }
        }
        private const string DEFAULT_MSG_KSP_MOD_CONFIGURATION_NOT_FOUND = "KSP mod configuration not found!";

        public static string MSG_SAVING_KSPMA_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SAVING_KSPMA_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SAVING_KSPMA_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_SAVING_KSPMA_SETTINGS = "Saving new KSP Mod Admin settings.";

        public static string MSG_KSPMA_SETTINGS_PATH_INVALID
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSPMA_SETTINGS_PATH_INVALID"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSPMA_SETTINGS_PATH_INVALID;
            }
        }
        private const string DEFAULT_MSG_KSPMA_SETTINGS_PATH_INVALID = "KSP Mod Admin settings path is invalid!";

        public static string MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_SAVING_KSPMA_SETTINGS = "Error during saving of KSP Mod Admin settings!";

        public static string MSG_SAVING_KSP_MOD_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SAVING_KSP_MOD_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SAVING_KSP_MOD_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_SAVING_KSP_MOD_SETTINGS = "Saving new KSP Mod settings.";

        public static string MSG_KSP_MOD_SETTINGS_PATH_INVALID
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_MOD_SETTINGS_PATH_INVALID"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_MOD_SETTINGS_PATH_INVALID;
            }
        }
        private const string DEFAULT_MSG_KSP_MOD_SETTINGS_PATH_INVALID = "KSP Mod settings path is invalid!";

        public static string MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS;
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_SAVING_KSP_MOD_SETTINGS = "Error during saving of KSP Mod settings!";

        public static string MSG_ERROR_MESSAGE_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_MESSAGE_0"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_MESSAGE_0;
            }
        }
        private const string DEFAULT_MSG_ERROR_MESSAGE_0 = "Error message: {0}";

        public static string MSG_ACCESS_DENIED_DIALOG_MESSAGE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ACCESS_DENIED_DIALOG_MESSAGE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ACCESS_DENIED_DIALOG_MESSAGE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ACCESS_DENIED_DIALOG_MESSAGE = "KSP Mod Admin can't save its config file. The access to the KSP path was denied.^If you have install KSP into the Program folder of Windows,^KSP MA needs admin rights to manipulate (save/change KSP MA config file).^You can move you install dir of KSP to another spot or start KSPMA in admin mode with:^  - a right click on the KSPModAdmin.exe and choose \"Run as Admin\"^or for a permanent change:^  - right click the KSPModAdmin.exe^  - choose properties^  - choose compatibility^  - check the \"Run as Admin\" CheckBox at the bottom.^  - press OK.";

        public static string MSG_DELETE_ALL_MODS_QUESTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETE_ALL_MODS_QUESTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETE_ALL_MODS_QUESTION);
            }
        }
        private const string DEFAULT_MSG_DELETE_ALL_MODS_QUESTION = "Are you sure to uninstall all Mod?";

        public static string MSG_DELETE_MOD_0_QUESTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETE_MOD_0_QUESTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETE_MOD_0_QUESTION);
            }
        }
        private const string DEFAULT_MSG_DELETE_MOD_0_QUESTION = "Are you sure to uninstall the mod \"{0}\"?";

        public static string MSG_DELETE_MODS_0_QUESTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETE_MODS_0_QUESTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETE_MODS_0_QUESTION);
            }
        }
        private const string DEFAULT_MSG_DELETE_MODS_0_QUESTION = "Are you sure to uninstall the mods: {0}";

        public static string MSG_INVALID_URL_SAVE_ANYWAY
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_INVALID_URL_SAVE_ANYWAY"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_INVALID_URL_SAVE_ANYWAY);
            }
        }
        private const string DEFAULT_MSG_INVALID_URL_SAVE_ANYWAY = "Invalid URL save anyway?";

        public static string MSG_ENTER_VALID_URL_FIRST
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ENTER_VALID_URL_FIRST"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ENTER_VALID_URL_FIRST);
            }
        }
        private const string DEFAULT_MSG_ENTER_VALID_URL_FIRST = "Enter a URL first please.";

        public static string MSG_ERROR_DURING_MODINFO_UPDATE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_MODINFO_UPDATE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_MODINFO_UPDATE);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_MODINFO_UPDATE = "Error during load of URL \"{0}\"{1}{1}Error message:{1}{2}";

        public static string MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FOLDER_INSTALLED_UNINSTALL_IT_TO_CHANGE_DESTINATION = "The folder or file is installed.^Unistall it before changeing the destination.";

        public static string MSG_SELECT_DEST
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECT_DEST"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECT_DEST);
            }
        }
        private const string DEFAULT_MSG_SELECT_DEST = "Please select a destination folder fist.";

        public static string MSG_SELECT_SCOURCE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SELECT_SCOURCE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SELECT_SCOURCE);
            }
        }
        private const string DEFAULT_MSG_SELECT_SCOURCE = "Please select a source folder fist.";

        public static string MSG_SOURCE_NODE_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SOURCE_NODE_NOT_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SOURCE_NODE_NOT_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SOURCE_NODE_NOT_FOUND = "Source node not found!^Can not set destination paths.";

        public static string MSG_ZIP_CREATION_ABORTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ZIP_CREATION_ABORTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ZIP_CREATION_ABORTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ZIP_CREATION_ABORTED = "Zip creation aborted!";

        public static string MSG_ZIP_0_CREATED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ZIP_0_CREATED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ZIP_0_CREATED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ZIP_0_CREATED = "Zip \"{0}\" created.";

        public static string MSG_ZIP_CREATION_FAILED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ZIP_CREATION_FAILED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ZIP_CREATION_FAILED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ZIP_CREATION_FAILED_0 = "Ceating of zip failed. Error message: {0}";

        public static string MSG_ROOT_IDENTIFIED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ROOT_IDENTIFIED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ROOT_IDENTIFIED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ROOT_IDENTIFIED = "Root of Zip identified as \"{0}\".";

        public static string MSG_DIR_CREATED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_CREATED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_CREATED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_CREATED_0 = "Directory created \"{0}\".";

        public static string MSG_DIR_CREATED_ERROR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_CREATED_ERROR_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_CREATED_ERROR_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_CREATED_ERROR_0 = "Can not create directory \"{0}\".";


        public static string MSG_FILE_DELETED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_DELETED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_DELETED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_DELETED_0 = "File deleted \"{0}\".";

        public static string MSG_FILE_DELETED_ERROR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_DELETED_ERROR_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_DELETED_ERROR_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_DELETED_ERROR_0 = "Can not delete file \"{0}\".";

        public static string MSG_FILE_EXTRACTED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_EXTRACTED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_EXTRACTED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_EXTRACTED_0 = "File extracted \"{0}\".";

        public static string MSG_FILE_EXTRACTED_ERROR_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_EXTRACTED_ERROR_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_EXTRACTED_ERROR_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_EXTRACTED_ERROR_0 = "Can not extract file \"{0}\".";

        public static string MSG_DOWNLOAD_SELECTION_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_SELECTION_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_SELECTION_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_SELECTION_TITLE = "Download/Mod folder selection";

        public static string MSG_KSP_INSTALL_FOLDER_SELECTION_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_INSTALL_FOLDER_SELECTION_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_INSTALL_FOLDER_SELECTION_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_KSP_INSTALL_FOLDER_SELECTION_TITLE = "KSP install folder selection";

        public static string MSG_DESTINATION_FOLDER_SELECTION_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DESTINATION_FOLDER_SELECTION_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DESTINATION_FOLDER_SELECTION_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DESTINATION_FOLDER_SELECTION_TITLE = "Destination folder selection";

        public static string MSG_DOWNLOAD_FOLDER_SELECTION_TITLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_FOLDER_SELECTION_TITLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_FOLDER_SELECTION_TITLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_FOLDER_SELECTION_TITLE = "Download folder selection";

        public static string MSG_UNCHECK_NO_ZIPARCHIVE_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UNCHECK_NO_ZIPARCHIVE_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UNCHECK_NO_ZIPARCHIVE_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_UNCHECK_NO_ZIPARCHIVE_WARNING = "No Zip archive found!^If you uncheck and uninstall this ModNode you can't reinstall it!^^Uncheck anyway?";

        public static string MSG_CHECK_NO_ZIPARCHIVE_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CHECK_NO_ZIPARCHIVE_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CHECK_NO_ZIPARCHIVE_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CHECK_NO_ZIPARCHIVE_WARNING = "No Zip archive found!^You can't check a ModNode which don't have a Zip archive!";


        public static string MSG_CANT_INSTALL_MODNODE_0_ZIP_MISSING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CANT_INSTALL_MODNODE_0_ZIP_MISSING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CANT_INSTALL_MODNODE_0_ZIP_MISSING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CANT_INSTALL_MODNODE_0_ZIP_MISSING = "Can't install \"{0}\"! Mod archive is missing.";

        public static string MSG_0_HAS_CHILDES_WITHOUT_DESTINATION_WARNING
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_0_HAS_CHILDES_WITHOUT_DESTINATION_WARNING"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_0_HAS_CHILDES_WITHOUT_DESTINATION_WARNING).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_0_HAS_CHILDES_WITHOUT_DESTINATION_WARNING = "The ModNode \"{0}\" or one or more child ModNodes don't have a destination!^Those ModNodes won't be checked!^^ Set a destination manually to install them.";

        public static string YES
        {
            get
            {
                string msg = Localizer.GlobalInstance["YES"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_YES);
            }
        }
        private const string DEFAULT_YES = "Yes";

        public static string NO
        {
            get
            {
                string msg = Localizer.GlobalInstance["NO"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_NO);
            }
        }
        private const string DEFAULT_NO = "No";

        public static string NONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["NONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_NONE);
            }
        }
        private const string DEFAULT_NONE = "None";

        public static string ON
        {
            get
            {
                string msg = Localizer.GlobalInstance["ON"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_ON);
            }
        }
        private const string DEFAULT_ON = "On";

        public static string OFF
        {
            get
            {
                string msg = Localizer.GlobalInstance["OFF"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_OFF);
            }
        }
        private const string DEFAULT_OFF = "Off";

        public static string MSG_PLS_SELECT_VALID_KSP_INSTALL_FOLDER
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_PLS_SELECT_VALID_KSP_INSTALL_FOLDER"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_PLS_SELECT_VALID_KSP_INSTALL_FOLDER);
            }
        }
        private const string DEFAULT_MSG_PLS_SELECT_VALID_KSP_INSTALL_FOLDER = "This is not a KSP install folder.^Please select a valid KSP install folder.";

        public static string MSG_KSP_INSTALL_FOLDER_SELECTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_KSP_INSTALL_FOLDER_SELECTION"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_KSP_INSTALL_FOLDER_SELECTION;
            }
        }
        private const string DEFAULT_MSG_KSP_INSTALL_FOLDER_SELECTION = "KSP install folder selection.";

        
        public static string MSG_SCAN_NO_NEW_MODS_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SCAN_NO_NEW_MODS_FOUND"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SCAN_NO_NEW_MODS_FOUND;
            }
        }

        private const string DEFAULT_MSG_SCAN_NO_NEW_MODS_FOUND = "No new Mods found.";
        
        public static string MSG_SCAN_ERROR_DURING_SCAN
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SCAN_ERROR_DURING_SCAN"];
                return !string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SCAN_ERROR_DURING_SCAN;
            }
        }

        private const string DEFAULT_MSG_SCAN_ERROR_DURING_SCAN = "Error during Gamedata folder scan.";

        public static string MSG_START_ADDING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_START_ADDING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_START_ADDING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_START_ADDING_0 = "Adding of mod \"{0}\" started.";

        public static string MSG_START_PROCESSING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_START_PROCESSING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_START_PROCESSING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_START_PROCESSING_0 = "Processing of mod \"{0}\" started.";

        public static string MSG_ERROR_DURING_PROCESSING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_PROCESSING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_PROCESSING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_PROCESSING_MOD_0 = "Error during processing mod \"{0}\".";
        
        public static string MSG_REMOVING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_REMOVING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REMOVING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REMOVING_MOD_0 = "Removing mod \"{0}\".";

        public static string MSG_ERROR_DURING_REMOVING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_REMOVING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_REMOVING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_REMOVING_MOD_0 = "Error during removing mod \"{0}\".";

        public static string MSG_MOD_RMODVED_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_RMODVED_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_RMODVED_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MOD_RMODVED_0 = "Mod \"{0}\" removed.";

        public static string MSG_DELETING_REMAINING_EMTPY_DIRS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DELETING_REMAINING_EMTPY_DIRS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DELETING_REMAINING_EMTPY_DIRS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DELETING_REMAINING_EMTPY_DIRS = "Deleting remaining empty directorys.";

        public static string MSG_DIR_0_NOT_EXISTS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_0_NOT_EXISTS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_0_NOT_EXISTS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_0_NOT_EXISTS = "Directory \"{0}\" don't exists.";

        public static string MSG_DIR_0_IS_KSPDIR
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_0_IS_KSPDIR"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_0_IS_KSPDIR).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_0_IS_KSPDIR = "Directory \"{0}\" is a KSP directory.";

        public static string MSG_DIR_0_IS_NOT_EMPTY
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIR_0_IS_NOT_EMPTY"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIR_0_IS_NOT_EMPTY).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIR_0_IS_NOT_EMPTY = "Directory \"{0}\" is not empty.";

        public static string MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_REFRESH_CHECKED_STATE_0 = "Error During RefreshCheckedState \"{0}\"";

        public static string MSG_REFRESHING_CHECKEDSTATE_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_REFRESHING_CHECKEDSTATE_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_REFRESHING_CHECKEDSTATE_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_REFRESHING_CHECKEDSTATE_0 = "Refreshing CheckedState of \"{0}\"...";

        public static string MSG_UNCHECKING_ALL_MODS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UNCHECKING_ALL_MODS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UNCHECKING_ALL_MODS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_UNCHECKING_ALL_MODS = "Unchecking all mods.";

        public static string MSG_UNCHECKING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UNCHECKING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UNCHECKING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_UNCHECKING_MOD_0 = "Unchecking mod \"{0}\".";

        public static string MSG_CHECKING_ALL_MODS
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CHECKING_ALL_MODS"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CHECKING_ALL_MODS).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CHECKING_ALL_MODS = "Checking all mods.";
        
        public static string MSG_CHECKING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CHECKING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CHECKING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CHECKING_MOD_0 = "Checking mod \"{0}\".";

        public static string MSG_ERROR_NO_DOWNLOAD_FOLDER_SELECTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_NO_DOWNLOAD_FOLDER_SELECTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_NO_DOWNLOAD_FOLDER_SELECTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_NO_DOWNLOAD_FOLDER_SELECTED = "No dowload folder selected.";

        public static string MSG_SCAN_GAMDATA_FOLDER_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SCAN_GAMDATA_FOLDER_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SCAN_GAMDATA_FOLDER_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SCAN_GAMDATA_FOLDER_STARTED = "Scan of GameData folder started.";

        public static string MSG_DIRECTORY_0_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DIRECTORY_0_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DIRECTORY_0_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DIRECTORY_0_FOUND = "Directory \"{0}\" found.";

        public static string MSG_FILE_0_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_0_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_0_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_0_FOUND = "File \"{0}\" found.";

        public static string MSG_SKIPPING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_SKIPPING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_SKIPPING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_SKIPPING_0 = "Skipping \"{0}\".";

        public static string MSG_MODNODE_0_CREATED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MODNODE_0_CREATED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODNODE_0_CREATED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODNODE_0_CREATED = "ModNode created for \"{0}\".";

        public static string MSG_ERROR_MOD_PATH_URL_0_INVALID
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_MOD_PATH_URL_0_INVALID"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_MOD_PATH_URL_0_INVALID).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_MOD_PATH_URL_0_INVALID = "Path or url \"{0}\" is not valid!";
        
        public static string MSG_ERROR_0_NO_VERSIONCONTROL
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_0_NO_VERSIONCONTROL"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_0_NO_VERSIONCONTROL).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_0_NO_VERSIONCONTROL = "The mod \"{0}\" has no VersionControl ... update check skipped.";

        public static string MSG_UPDATECHECK_FOR_MOD_0_VIA_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UPDATECHECK_FOR_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UPDATECHECK_FOR_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_UPDATECHECK_FOR_MOD_0 = "Update check for mod \"{0}\" via \"{1}\" started...";

        public static string MSG_MOD_0_IS_UPTODATE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_0_IS_UPTODATE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_0_IS_UPTODATE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MOD_0_IS_UPTODATE = "The mod \"{0}\" is up to date.";

        public static string MSG_MOD_0_IS_OUTDATED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_0_IS_OUTDATED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_0_IS_OUTDATED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MOD_0_IS_OUTDATED = "The mod \"{0}\" is outdated.";

        public static string MSG_ERROR_DURING_UPDATECHECK_0_ERRORMSG_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_UPDATECHECK_0_ERRORMSG_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_UPDATECHECK_0_ERRORMSG_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_UPDATECHECK_0_ERRORMSG_1 = "Error during update check for mod \"{0}\". Error: \"{1}\"";

        public static string MSG_UPDATING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_UPDATING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_UPDATING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_UPDATING_MOD_0 = "Updating mod \"{0}\"...";

        public static string MSG_MOD_0_UPDATED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MOD_0_UPDATED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MOD_UPDATED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MOD_UPDATED = "Mod \"{0}\" updated";

        public static string MSG_ERROR_WHILE_UPDATING_MOD_0_ERROR_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_WHILE_UPDATING_MOD_0_ERROR_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_WHILE_UPDATING_MOD_0_ERROR_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_WHILE_UPDATING_MOD_0_ERROR_1 = "Error during updating mod \"{0}\". Error: \"{1}\"";

        public static string MSG_ERROR_UPDATING_MOD_0_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_UPDATING_MOD_0_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_UPDATING_MOD_0_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_UPDATING_MOD_0_FAILED = "Updating of the mod \"{0}\" failed.^Manualy update required.";

        public static string MSG_ADD_MOD_0_TO_MODPACK
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ADD_MOD_0_TO_MODPACK"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ADD_MOD_0_TO_MODPACK).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ADD_MOD_0_TO_MODPACK = "Add mod {0} to ModPack...";

        public static string MSG_MODPACK_INFOFILE_NOT_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_MODPACK_INFOFILE_NOT_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_MODPACK_INFOFILE_NOT_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_MODPACK_INFOFILE_NOT_FOUND = "ModPack info file not found!";

        public static string MSG_DOWNLOAD_0_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_0_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_0_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_0_STARTED = "Download of {0} started ...";

        public static string MSG_DOWNLOAD_0_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_0_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_0_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_0_DONE = "Download of {0} done.";

        public static string MSG_DOWNLOAD_0_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_0_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_0_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOAD_0_FAILED = "Download of {0} failed!";

        public static string MSG_IMPORT_0_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORT_0_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORT_0_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORT_0_STARTED = "Import of {0} started ...";

        public static string MSG_IMPORT_0_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORT_0_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORT_0_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORT_0_DONE = "Import of {0} done.";

        public static string MSG_IMPORT_0_FAILED_ERROR_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORT_0_FAILED_ERROR_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORT_0_FAILED_ERROR_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORT_0_FAILED_ERROR_1 = "Import of {0} failed! Error: {1}";

        public static string MSG_IMPORT_0_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORT_0_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORT_0_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORT_0_FAILED = "Import of {0} failed!";

        public static string MSG_NO_MODS_TO_EXPORT
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_MODS_TO_EXPORT"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_MODS_TO_EXPORT).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_MODS_TO_EXPORT = "There are no mods to export.";
        
        public static string MSG_EXPORT_TO_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_EXPORT_TO_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_EXPORT_TO_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_EXPORT_TO_0 = "Exporting to \"{0}\".";

        public static string MSG_EXPORT_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_EXPORT_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_EXPORT_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_EXPORT_DONE = "Export done.";

        public static string MSG_EXPORT_ABORTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_EXPORT_ABORTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_EXPORT_ABORTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_EXPORT_ABORTED = "Export aborted.";

        public static string MSG_IMPORT_STARTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORT_STARTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORT_STARTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORT_STARTED = "Starting import.";

        public static string MSG_CLEARING_MODSELECTION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_CLEARING_MODSELECTION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_CLEARING_MODSELECTION).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_CLEARING_MODSELECTION = "Clearing ModSelection.";

        public static string MSG_IMPORTING_FROM_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORTING_FROM_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORTING_FROM_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORTING_FROM_0 = "Importing from \"{0}\".";

        public static string MSG_IMPORTING_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORTING_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORTING_FAILED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORTING_FAILED = "Import failed!";

        public static string MSG_IMPORTING_DONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORTING_DONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORTING_DONE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORTING_DONE = "Import done.";

        public static string MSG_IMPORTING_ABORTED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORTING_ABORTED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORTING_ABORTED).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORTING_ABORTED = "Import aborted.";

        public static string MSG_ERROR_DURING_MODUPDATE_0_ERROR_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_MODUPDATE_0_ERROR_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_MODUPDATE_0_ERROR_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_MODUPDATE_0_ERROR_1 = "Error during mod update \"{0}\". Error: \"{1}\"";

        public static string MSG_DOWNLOADING_MOD_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOADING_MOD_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOADING_MOD_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_DOWNLOADING_MOD_0 = "Downloading mod update \"{0}\"...";

        public static string MSG_ERROR_DURING_MOD_UPDATE_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DURING_MOD_UPDATE_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DURING_MOD_UPDATE_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_DURING_MOD_UPDATE_0 = "Error during mod update check \"{0}\"";

        public static string MSG_ERROR_PLUGIN_LOADING_TABVIEWS_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_PLUGIN_LOADING_TABVIEWS_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_PLUGIN_LOADING_TABVIEWS_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_PLUGIN_LOADING_TABVIEWS_0 = "Plugin loading error: TabView \"{0}\" already exists!";

        public static string MSG_ERROR_PLUGIN_LOADING_OPTIONVIEWS_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_PLUGIN_LOADING_OPTIONVIEWS_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_PLUGIN_LOADING_OPTIONVIEWS_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_PLUGIN_LOADING_OPTIONVIEWS_0 = "Plugin loading error: Option TabView \"{0}\" already exists!";

        public static string MSG_NOT_AVAILABLE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NOT_AVAILABLE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NOT_AVAILABLE).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NOT_AVAILABLE = "Not available";

        public static string MSG_NO_DESTINATION
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_DESTINATION"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_DESTINATION).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_DESTINATION = "Not destination";
        
        public static string MSG_FILE_NOT_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_FILE_NOT_FOUND_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_FILE_NOT_FOUND_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_FILE_NOT_FOUND_0 = "File not found \"{0}\".";

        public static string MSG_ERROR_WHILE_READING_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_WHILE_READING_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_WHILE_READING_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_ERROR_WHILE_READING_0 = "Error while reading \"{0}\".";

        public static string MSG_AVC_VERSIONFILE_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_AVC_VERSIONFILE_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_AVC_VERSIONFILE_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_AVC_VERSIONFILE_FOUND = "AVC Plugin version file found.";

        public static string MSG_NO_AVC_VERSIONFILE_FOUND
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_AVC_VERSIONFILE_FOUND"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_AVC_VERSIONFILE_FOUND).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_AVC_VERSIONFILE_FOUND = "No AVC Plugin version file found!";

        public static string MSG_READING_AVC_VERSIONFILE_INFO
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_READING_AVC_VERSIONFILE_INFO"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_READING_AVC_VERSIONFILE_INFO).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_READING_AVC_VERSIONFILE_INFO = "Reading AVC Plugin version file...";

        public static string MSG_IMPORTING_AVC_VERSIONFILE_INFO_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_IMPORTING_AVC_VERSIONFILE_INFO_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_IMPORTING_AVC_VERSIONFILE_INFO_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_IMPORTING_AVC_VERSIONFILE_INFO_0 = "Importing AVC Plugin version file information for \"{0}\"...";

        public static string MSG_NO_COMPATIBLE_SITEHANDLER_FOUND_0
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_NO_COMPATIBLE_SITEHANDLER_FOUND_0"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_NO_COMPATIBLE_SITEHANDLER_FOUND_0).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_NO_COMPATIBLE_SITEHANDLER_FOUND_0 = "No compatible SiteHandler found for \"{0}\"! Mod update support not available for this mod.";

        public static string MSG_COMPATIBLE_SITEHANDLER_0_FOUND_1
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_COMPATIBLE_SITEHANDLER_0_FOUND_1"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_COMPATIBLE_SITEHANDLER_0_FOUND_1).Replace("^", Environment.NewLine);
            }
        }
        private const string DEFAULT_MSG_COMPATIBLE_SITEHANDLER_0_FOUND_1 = "Compatible SiteHandler ({0}) found for \"{1}\"! Mod update support active!";
    
        public static string MSG_ERROR_WHILE_READING_AVC_VERION_FILE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_WHILE_READING_AVC_VERION_FILE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_WHILE_READING_AVC_VERION_FILE).Replace("^", Environment.NewLine);
            }
        }

        private const string DEFAULT_MSG_ERROR_WHILE_READING_AVC_VERION_FILE = "Error while reading AVC Plugin version file!";

        public static string MSG_ERROR_DOWNLOADING_NEW_AVC_VERION_FILE_FAILED
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_ERROR_DOWNLOADING_NEW_AVC_VERION_FILE_FAILED"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_ERROR_DOWNLOADING_NEW_AVC_VERION_FILE_FAILED).Replace("^", Environment.NewLine);
            }
        }

        private const string DEFAULT_MSG_ERROR_DOWNLOADING_NEW_AVC_VERION_FILE_FAILED = "Downloading of new AVC Plugin version file failded!";

        public static string MSG_DOWNLOAD_PATH_MISSING_PLEASE_SELECT_ONE
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DOWNLOAD_PATH_MISSING_PLEASE_SELECT_ONE"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DOWNLOAD_PATH_MISSING_PLEASE_SELECT_ONE).Replace("^", Environment.NewLine);
            }
        }

        private const string DEFAULT_MSG_DOWNLOAD_PATH_MISSING_PLEASE_SELECT_ONE = "Download path missing. Please select a download folder.";

        public static string MSG_URL_DETECTED_STARTING_DOWNLOAD
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_URL_DETECTED_STARTING_DOWNLOAD"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_URL_DETECTED_STARTING_DOWNLOAD).Replace("^", Environment.NewLine);
            }
        }

        private const string DEFAULT_MSG_URL_DETECTED_STARTING_DOWNLOAD = "Url detected starting download...";

        public static string MSG_DESTINATION_0_SET_TO_GAMEDATA
        {
            get
            {
                string msg = Localizer.GlobalInstance["MSG_DESTINATION_0_SET_TO_GAMEDATA"];
                return (!string.IsNullOrEmpty(msg) ? msg : DEFAULT_MSG_DESTINATION_0_SET_TO_GAMEDATA).Replace("^", Environment.NewLine);
            }
        }

        private const string DEFAULT_MSG_DESTINATION_0_SET_TO_GAMEDATA = "Destination of \"{0}\" will be set to GameData folder.";
    }
}
