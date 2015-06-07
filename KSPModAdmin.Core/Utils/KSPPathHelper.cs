using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Utils
{
    /// <summary>
    /// Helper class to get or identify KSP paths.
    /// </summary>
    public static class KSPPathHelper
    {
        /// <summary>
        /// Checks if the passed path is a KSP folder path.
        /// </summary>
        /// <param name="dir">The directory to check.</param>
        /// <returns>True if the passed path is a KSP folder path.</returns>
        public static bool IsKSPDir(string dir)
        {
            if (string.IsNullOrEmpty(dir))
                return false;

            foreach (var path in Constants.KSPFolders)
            {
                if (path.Equals(dir, StringComparison.CurrentCultureIgnoreCase) || 
                    GetPathByName(path).Equals(dir, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed folder is the install folder of KSP.
        /// </summary>
        /// <param name="kspPath">The path to the KSP install folder.</param>
        /// <returns>True if the passed folder is the install folder of KSP otherwise false.</returns>
        public static bool IsKSPInstallFolder(string kspPath)
        {
            try
            {
                if (string.IsNullOrEmpty(kspPath))
                    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.PARTS)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.KSPDATA)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINS)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINDATA)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.GAMEDATA)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.RESOURCES)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.INTERNALS)))
                ////    return false;
                ////if (!Directory.Exists(Path.Combine(kspPath, Constants.SHIPS)))
                ////    return false;
                if (!Directory.Exists(kspPath)) 
                    return false;
                if (!File.Exists(Path.Combine(kspPath, Constants.KSP_EXE)) &&
                    !File.Exists(Path.Combine(kspPath, Constants.KSP_X64_EXE)) &&
                    !File.Exists(Path.Combine(kspPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_EXE_MAC)) &&
                    !File.Exists(Path.Combine(kspPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_X64_EXE_MAC)))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                Messenger.AddError("IsKSPInstallFolder() failed! Error: " + ex.Message, ex);
            }

            return false;
        }

        /// <summary>
        /// Gets the path of the passed FolderType.
        /// </summary>
        /// <param name="kspPath">FolderType of the folder to get the path of.</param>
        /// <returns>The HD path to the passed folder name.</returns>
        public static string GetPath(KSPPaths kspPath)
        {
            string installPath = OptionsController.SelectedKSPPath;
            string path = string.Empty;
            if (kspPath == KSPPaths.AppConfig)
            {
                switch (PlatformHelper.GetPlatform())
                {
                    case Platform.Linux:
                        path = Path.Combine(Environment.GetEnvironmentVariable(Constants.HOME), Constants.LINUX_PATH, Constants.APP_CONFIG_FILE);
                        break;
                    case Platform.OsX:
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Constants.APP_CONFIG_FILE);
                        break;
                    default:
                        path = Path.Combine(Application.CommonAppDataPath.Replace(VersionHelper.GetAssemblyVersion(), string.Empty), Constants.APP_CONFIG_FILE);
                        break;
                }
            }

            else if (kspPath == KSPPaths.LanguageFolder ||
                     kspPath == KSPPaths.KSPMA_Plugins)
            {
                switch (kspPath)
                {
                    case KSPPaths.LanguageFolder:
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Constants.LANGUAGE_FOLDER);
                        break;
                    case KSPPaths.KSPMA_Plugins:
                        path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), Constants.PLUGIN_FOLDER);
                        break;
                }
            }

            else if (!string.IsNullOrEmpty(installPath) && Directory.Exists(installPath))
            {
                switch (kspPath)
                {
                    case KSPPaths.KSPRoot:
                        path = installPath;
                        break;
                    case KSPPaths.KSPExe:
                        if (PlatformHelper.GetPlatform() != Platform.OsX)
                            path = Path.Combine(installPath, Constants.KSP_EXE);
                        else
                            path = Path.Combine(installPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_EXE);
                        break;
                    case KSPPaths.KSPX64Exe:
                        if (PlatformHelper.GetPlatform() != Platform.OsX)
                            path = Path.Combine(installPath, Constants.KSP_X64_EXE);
                        else
                            path = Path.Combine(installPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_X64_EXE);
                        break;
                    case KSPPaths.KSPConfig:
                        path = Path.Combine(installPath, Constants.MODS_CONFIG_FILE);
                        break;
                    case KSPPaths.Saves:
                        path = Path.Combine(installPath, Constants.SAVES);
                        break;
                    case KSPPaths.Parts:
                        path = Path.Combine(installPath, Constants.PARTS);
                        break;
                    case KSPPaths.Plugins:
                        path = Path.Combine(installPath, Constants.PLUGINS);
                        break;
                    case KSPPaths.PluginData:
                        path = Path.Combine(installPath, Constants.PLUGINDATA);
                        break;
                    case KSPPaths.Resources:
                        path = Path.Combine(installPath, Constants.RESOURCES);
                        break;
                    case KSPPaths.GameData:
                        path = Path.Combine(installPath, Constants.GAMEDATA);
                        break;
                    case KSPPaths.Ships:
                        path = Path.Combine(installPath, Constants.SHIPS);
                        break;
                    case KSPPaths.VAB:
                        path = Path.Combine(installPath, Constants.SHIPS, Constants.VAB);
                        break;
                    case KSPPaths.SPH:
                        path = Path.Combine(installPath, Constants.SHIPS, Constants.SPH);
                        break;
                    case KSPPaths.Internals:
                        path = Path.Combine(installPath, Constants.INTERNALS);
                        break;
                    case KSPPaths.KSPData:
                        path = Path.Combine(installPath, Constants.KSPDATA);
                        break;
                }
            }

            return path;
        }

        /// <summary>
        /// Gets the HD path of the passed folder name.
        /// </summary>
        /// <param name="pathName">Name of the folder to get the path of.</param>
        /// <returns>The HD path to the passed folder name.</returns>
        public static string GetPathByName(string pathName)
        {
            string installPath = OptionsController.SelectedKSPPath;
            string path = string.Empty;
            if (!string.IsNullOrEmpty(installPath) && Directory.Exists(installPath))
            {
                if (pathName.Equals(Constants.SAVES, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.SAVES);
                else if (pathName.Equals(Constants.PARTS, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.PARTS);
                else if (pathName.Equals(Constants.PLUGINS, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.PLUGINS);
                else if (pathName.Equals(Constants.PLUGINDATA, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.PLUGINDATA);
                else if (pathName.Equals(Constants.RESOURCES, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.RESOURCES);
                else if (pathName.Equals(Constants.GAMEDATA, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.GAMEDATA);
                else if (pathName.Equals(Constants.SHIPS, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.SHIPS);
                else if (pathName.Equals(Constants.INTERNALS, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.INTERNALS);
                else if (pathName.Equals(Constants.VAB, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.VAB);
                else if (pathName.Equals(Constants.SPH, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.SPH);
                else if (pathName.Equals(Constants.KSPDATA, StringComparison.CurrentCultureIgnoreCase))
                    path = Path.Combine(installPath, Constants.KSPDATA);
                else if (pathName.Equals(Constants.KSP_ROOT, StringComparison.CurrentCultureIgnoreCase))
                    path = installPath;
                else if (pathName.Equals(Constants.KSP_EXE, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (PlatformHelper.GetPlatform() != Platform.OsX)
                        path = Path.Combine(installPath, Constants.KSP_EXE);
                    else
                        path = Path.Combine(installPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_EXE);
                }
                else if (pathName.Equals(Constants.KSP_X64_EXE, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (PlatformHelper.GetPlatform() != Platform.OsX)
                        path = Path.Combine(installPath, Constants.KSP_X64_EXE);
                    else
                        path = Path.Combine(installPath, Path.Combine(Constants.MAC_EXE_PATH.Split('\\')), Constants.KSP_X64_EXE);
                }
            }

            return path;
        }

        /// <summary>
        /// Returns the absolute path of the ModNode.
        /// </summary>
        /// <param name="node">The ModNode to get the absolute path from.</param>
        /// <returns>The absolute path of the ModNode.</returns>
        public static string GetAbsolutePath(ModNode node)
        {
            if (node.HasDestination)
                return GetAbsolutePath(node.Destination);
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the absolute path of the srcPath.
        /// </summary>
        /// <param name="srcPath">The source path to get the absolute path from.</param>
        /// <returns>The absolute path of the srcPath.</returns>
        public static string GetAbsolutePath(string srcPath)
        {
            return srcPath.Replace(Constants.KSPFOLDERTAG, OptionsController.SelectedKSPPath);
        }

        /// <summary>
        /// Returns the relative path of the srcPath.
        /// </summary>
        /// <param name="srcPath">The source path to get the relative path from.</param>
        /// <returns>The relative path of the srcPath.</returns>
        public static string GetRelativePath(string srcPath)
        {
            string resultPath = srcPath;
            string kspRoot = GetPath(KSPPaths.KSPRoot).ToLower();
            int index = srcPath.ToLower().IndexOf(kspRoot);
            if (index >= 0)
                resultPath = Constants.KSPFOLDERTAG + srcPath.Substring(kspRoot.Length);
                    
            return resultPath;
        }
    }
}
