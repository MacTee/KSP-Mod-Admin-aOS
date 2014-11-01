using System.Reflection;
using KSPModAdmin.Core.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
                if (path == dir.ToLower() || GetPathByName(path.ToLower()).ToLower() == dir.ToLower())
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
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PARTS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.KSPDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.PLUGINDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.GAMEDATA)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.RESOURCES)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.INTERNALS)))
                //    return false;
                //if (!Directory.Exists(Path.Combine(kspPath, Constants.SHIPS)))
                //    return false;
                if (!Directory.Exists(kspPath)) 
                    return false;
                if (!File.Exists(Path.Combine(kspPath, Constants.KSP_EXE)) && !File.Exists(Path.Combine(kspPath, Constants.KSP_X64_EXE)))
                    return false;

                return true;
            }
            catch (Exception ex)
            {
                string NoWarningPLS = ex.Message;
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
                path = Path.Combine(Application.CommonAppDataPath.Replace(VersionHelper.GetAssemblyVersion(), string.Empty), Constants.APP_CONFIG_FILE);

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
                        path = Path.Combine(installPath, Constants.KSP_EXE);
                        break;
                    case KSPPaths.KSPX64Exe:
                        path = Path.Combine(installPath, Constants.KSP_X64_EXE);
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
                        path = Path.Combine(Path.Combine(installPath, Constants.SHIPS), Constants.VAB);
                        break;
                    case KSPPaths.SPH:
                        path = Path.Combine(Path.Combine(installPath, Constants.SHIPS), Constants.SPH);
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
                switch (pathName.ToLower())
                {
                    case Constants.SAVES:
                        path = Path.Combine(installPath, Constants.SAVES);
                        break;
                    case Constants.PARTS:
                        path = Path.Combine(installPath, Constants.PARTS);
                        break;
                    case Constants.PLUGINS:
                        path = Path.Combine(installPath, Constants.PLUGINS);
                        break;
                    case Constants.PLUGINDATA:
                        path = Path.Combine(installPath, Constants.PLUGINDATA);
                        break;
                    case Constants.RESOURCES:
                        path = Path.Combine(installPath, Constants.RESOURCES);
                        break;
                    case Constants.GAMEDATA:
                        path = Path.Combine(installPath, Constants.GAMEDATA);
                        break;
                    case Constants.SHIPS:
                        path = Path.Combine(installPath, Constants.SHIPS);
                        break;
                    case Constants.INTERNALS:
                        path = Path.Combine(installPath, Constants.INTERNALS);
                        break;
                    case Constants.VAB:
                        path = Path.Combine(installPath, Path.Combine(Constants.SHIPS, Constants.VAB));
                        break;
                    case Constants.SPH:
                        path = Path.Combine(installPath, Path.Combine(Constants.SHIPS, Constants.SPH));
                        break;
                    case Constants.KSPDATA:
                        path = Path.Combine(installPath, Constants.KSPDATA);
                        break;
                    case Constants.KSP_ROOT:
                        path = installPath;
                        break;
                    case Constants.KSP_EXE:
                        path = Path.Combine(installPath, Constants.KSP_EXE);
                        break;
                    case Constants.KSP_X64_EXE:
                        path = Path.Combine(installPath, Constants.KSP_X64_EXE);
                        break;
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
