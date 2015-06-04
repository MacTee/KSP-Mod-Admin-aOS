using System;
using KSPModAdmin.Core.Utils;

namespace KSPModAdmin.Core
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// All constants used for the KSP Mod Admin.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class Constants
    {
        // General
        // ReSharper disable InconsistentNaming
        public const string DOWNLOAD_FILENAME_TEMPLATE = "KSPModAdmin-v{0}.zip";

        public const string SERVICE_DOWNLOAD_LINK_WIN = "http://www.services.mactee.de/KSP/getKSP_MA_Zip2.php5?build=win";
        public const string SERVICE_DOWNLOAD_LINK_MONO = "http://www.services.mactee.de/KSP/getKSP_MA_Zip2.php5?build=mono";
        public const string SERVICE_ADMIN_VERSION = "http://www.services.mactee.de/KSP/getKSP_MA_Version2.php5";

        public const string WIKIURL = "https://github.com/MacTee/KSP-Mod-Admin-aOS/wiki";

        public const string TRUE = "true";
        public const string FALSE = "false";

        public const string FOLDER_DOWNLOADS = "Downloads";

        public const int WIN_MIN_WIDTH = 705;
        public const int WIN_MIN_HEIGHT = 575;

        public const string SEPARATOR = "------------------------------------------------------------";

        // KSP Setting.cfg parameter
        public const string SCREEN_WIDTH = "SCREEN_RESOLUTION_WIDTH";
        public const string SCREEN_HEIGHT = "SCREEN_RESOLUTION_HEIGHT";
        public const string FULLSCREEN = "FULLSCREEN";

        // KSP MA folders
        public const string PLUGIN_FOLDER = "Plugins";
        public const string LANGUAGE_FOLDER = "lang"; // "Languages";

        // KSP folders
        public const string KSP_ROOT = "KSP_Root";
        public const string PARTS = "Parts";
        public const string PLUGINS = "Plugins";
        public const string PLUGINDATA = "PluginData";
        public const string RESOURCES = "Resources";
        public const string INTERNALS = "Internals";
        public const string SHIPS = "Ships";
        public const string VAB = "VAB";
        public const string SPH = "SPH";
        public const string KSPDATA = "KSP_Data";
        public const string SAVES = "Saves";
        public const string GAMEDATA = "GameData";
        public static string[] KSPFolders
        {
            get
            {
                return new string[] 
                { 
                    Constants.KSP_ROOT, Constants.GAMEDATA, Constants.PLUGINS, 
                    Constants.PLUGINDATA, Constants.PARTS, Constants.RESOURCES,
                    Constants.INTERNALS, Constants.KSPDATA, Constants.SAVES, 
                    Constants.SHIPS, Constants.VAB, Constants.SPH
                };
            }
        }

        // Files
        public static string KSP_EXE 
        {
            get
            {
                switch (PlatformHelper.GetPlatform())
                {
                    case Platform.Linux:
                        return KSP_EXE_LINUX;
                    case Platform.OsX:
                        return KSP_EXE_MAC;
                    default:
                        return KSP_EXE_WIN;
                }
            }
        }
        public static string KSP_X64_EXE
        {
            get
            {
                switch (PlatformHelper.GetPlatform())
                {
                    case Platform.Linux:
                        return KSP_X64_EXE_LINUX;
                    case Platform.OsX:
                        return KSP_X64_EXE_MAC;
                    default:
                        return KSP_X64_EXE_WIN;
                }
            }
        }
        public const string KSP_EXE_LINUX = "KSP.x86";
        public const string KSP_EXE_WIN = "KSP.exe";
        public const string KSP_EXE_MAC = "KSP";
        public const string KSP_X64_EXE_LINUX = "KSP.x86_64";
        public const string KSP_X64_EXE_WIN = "KSP_x64.exe";
        public const string KSP_X64_EXE_MAC = KSP_EXE_MAC;
        public const string APP_CONFIG_FILE = "KSPModAdmin_aOS.cfg";
        public const string MODS_CONFIG_FILE = "KSPModAdmin_aOS.cfg";
        public const string LINUX_PATH = ".ksp_mod_admin";
        public const string MAC_EXE_PATH = "KSP.app\\Contents\\MacOS";
        public const string MAC_EXE_FOLDER_NAME = "KSP.app";

        // XMLNode names
        public const string ROOTNODE = "ModAdminConfig";
        public const string ROOT = "Root";
        public const string VERSION = "Version";
        public const string GAMEVERSION = "Game_Version";
        public const string MESSAGE = "Message";
        public const string GENERAL = "General";
        public const string LANGUAGE = "Language";
        public const string KSP_PATH = "KSP_Path";
        public const string KNOWN_KSP_PATHS = "Known_KSP_Paths";
        public const string KNOWN_KSP_PATH = "Known_KSP_Path";
        public const string CHECKFORUPDATES = "checkforupdates";
        public const string LASTMODUPDATETRY = "LastModUpdateTry";
        public const string MODUPDATEINTERVAL = "ModUpdateInterval";
        public const string MODUPDATEBEHAVIOR = "ModUpdateBehavior";
        public const string DELETEOLDARCHIVES = "DeleteOldArchivesAfterUpdate";
        public const string COLOR4OUTDATEDMODS = "Color4OudatedMods";
        public const string TABORDER = "TabOrder";
        public const string TAB = "Tab";
        public const string DOWNLOAD_PATH = "DownloadPath";
        public const string NAME = "Name";
        public const string LONGNAME = "LongName";
        public const string SORTORDER = "SortOrder";
        public const string TOOLTIPTEXT = "TooltipText";
        public const string CONTROL = "Control";
        public const string STRING = "String";
        public const string KEY = "key";
        public const string FULLPATH = "FullPath";
        public const string CHECKED = "Checked";
        public const string NODETYPE = "NodeType";
        public const string DESTINATION = "Destination";
        public const string INSTALLROOTKEY = "InstallRootKey";
        public const string MODS = "Mods";
        public const string MOD = "Mod";
        public const string MOD_ENTRY = "ModEntry";
        public const string POSTDOWNLOADACTION = "PostDownloadAction";
        public const string OVERRRIDE = "Override";
        public const string VALUE = "Value";
        public const string UNIQUEIDENTIFIER = "UniqueIdentifier";
        public const string ADDDATE = "AddDate";
        public const string NOTE = "Note";
        public const string PRODUCTID = "ProductID";
        public const string CREATIONDATE = "CreationDate";
        public const string CHANGEDATE = "ChangeDate";
        public const string AUTHOR = "Author";
        public const string RATING = "Rating";
        public const string DOWNLOADS = "Downloads";
        public const string MODURL = "ModURL";
        public const string AVCURL = "AvcURL";
        public const string ADDITIONALURL = "AdditionalURL";
        public const string FORUMURL = "ForumURL";
        public const string CURSEFORGEURL = "CurseForgeURL";
        public const string VERSIONCONTROL = "VersionControl";
        public const string VERSIONCONTROLERNAME = "VersionControllerName";
        public const string FILENAME = "Filename";
        public const string KSPSTARTUPOPTIONS = "KSPStartupOptions";
        public const string BORDERLESSWINDOW = "BorderlessWindow";
        public const string ISFILE = "IsFile";
        public const string INSTALL = "Install";
        public const string INSTALLDIR = "InstallDir";

        public const string DESTINATIONDETECTIONOPTIONS = "DestinationDetectionOptions";
        public const string FALLBACK = "FallBack";
        public const string TOOLTIPOPTIONS = "ToolTipOptions";
        public const string CONFLICTDETECTIONOPTIONS = "ConflictDetectionOptions";
        public const string AVCSUPPORT = "AVCSupport";
        public const string AVCIGNORENAME = "AVCIgnoreName";
        public const string AVCIGNOREURL = "AVCIgnoreURL";
        public const string ONOFF = "OnOff";
        public const string DELAY = "Delay";
        public const string DISPLAYTIME = "DisplayTime";
        public const string SHOWCONFLICTSOLVER = "ShowConflictSolver";
        public const string NODECOLORS = "NodeColors";
        public const string COLOR = "Color";
        public const string DESTINATIONDETECTED = "DestinationDetection";
        public const string DESTINATIONMISSING = "DetstinationMissing";
        public const string DESTINATIONCONFLICT = "DestinationConflict";
        public const string MODINSTALLED = "ModInstalled";
        public const string MODARCHIVEMISSING = "ModArchiveMissing";
        public const string MODOUTDATED = "ModOutdated";
        public const string LAUNCHPARAMETER = "LaunchParameter";
        public const string USE64BIT = "Use64Bit";
        public const string FORCEOPENGL = "ForceOpenGL";
        public const string TYPE = "Type";
        public const string DATAPROPERTYNAME = "DataPropertyName";
        public const string EDITENABLED = "EditEnabled";
        public const string INCREMENTALSEARCHENABLED = "IncrementalSearchEnabled";
        public const string LEFTMARGIN = "LeftMargin";
        public const string SCALEMODE = "ScaleMode";

        // Form related
        public const string POSITION = "Position";
        public const string X = "x";
        public const string Y = "y";
        public const string SIZE = "Size";
        public const string WIDTH = "Width";
        public const string HEIGHT = "Height";
        public const string WINDOWSTATE = "WindowState";
        public const string MINIM = "minimized";
        public const string MAXIM = "maximized";
        public const string TREEVIEWADVCOLUMNSINFO = "TreeViewAdvColumnsInfo";
        public const string MODINFOCOLUMNS = "ModInfoColumns";
        public const string MODINFOSSPLITTERPOS = "ModInfoSplitterPos";
        public const string COLUMN = "Column";
        public const string ID = "ID";

        // File extensions
        public const string EXT_ZIP = ".zip";
        public const string EXT_RAR = ".rar";
        public const string EXT_7ZIP = ".7z";
        public const string EXT_CRAFT = ".craft";
        public const string EXT_KSP_SAVE = ".sfs";
        public const string EXT_CFG = ".cfg";

        // Filter
        public const string ZIP_FILTER1 = "Zip-Files|*.zip";
        public const string ARCHIVE_FILTER = "All|*.zip;*.7z;*.rar;*.craft|Archives|*.zip;*.7z;*.rar|Zip-Files|*.zip|7Zip-Files|*.7z|Rar-Files|*.rar";
        public const string ADD_DLG_FILTER = ARCHIVE_FILTER + "|Craft|*.craft";
        public const string IMAGE_FILTER = "Image files|*.jpeg;*.jpg;*.png;*.gif|JPEG Files (*.jpeg)|*.jpeg;|JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|GIF Files (*.gif)|*.gif";
        public const string MODPACK_FILTER = "ModPack|*.modpack";


        public const string KSPFOLDERTAG = "<KSPFolder>";

        public const string HOME = "HOME";
    }
}