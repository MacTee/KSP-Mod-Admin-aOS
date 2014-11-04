using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;

namespace KSPModAdmin.Core.Utils
{
    public delegate void MessageCallbackHandler(object sender, string message);

    /// <summary>
    /// The ModPackHandler handles everything related to ModPack.
    /// Import / Export.
    /// </summary>
    public static class ModPackHandler
    {
        #region Constants

        public const string KSPTEMPDIR = "KSPTemp";
        public const string MODPACKXML = "ModPack.xml";
        public const string ZERO = "0";
        public const string ONE = "1";
        public const string XMLVERSION = "1.0";
        public const string XMLUTF8 = "UTF-8";
        public const string MODS_FOLDER = "/Mods/";
        public const string MODS_FOLDER_WIN = "\\Mods\\";

        #endregion

        #region Export

        /// <summary>
        /// Starts the export process.
        /// </summary>
        /// <param name="modsToExport">List of mods to export.</param>
        /// <param name="fileName">Filename for the new created ModPack.</param>
        /// <param name="includeMods">Flag to determine if the mod archives should be included to.</param>
        public static void Export(List<ModNode> modsToExport, string fileName, bool includeMods = false, MessageCallbackHandler messageCallback = null)
        {
            XmlNode modsNode = CreateXmlDocument();
            XmlDocument doc = modsNode.OwnerDocument;
            string tempDocPath = Path.Combine(Path.GetTempPath(), KSPTEMPDIR, Path.GetTempFileName());

            using (var archive = ZipArchive.Create())
            {
                foreach (var mod in modsToExport)
                {
                    if (messageCallback != null)
                        messageCallback(null, string.Format(Messages.MSG_ADD_MOD_0_TO_MODPACK, mod.Text));

                    if (includeMods && mod.ZipExists)
                        archive.AddEntry(Path.Combine(Constants.MODS, Path.GetFileName(mod.Name)), mod.Name);

                    modsNode.AppendChild(CreateModXmlNode(doc, mod));
                }

                doc.Save(tempDocPath);
                archive.AddEntry(MODPACKXML, tempDocPath);
                archive.SaveTo(fileName, CompressionType.Deflate);
            }

            if (File.Exists(tempDocPath))
                File.Delete(tempDocPath);
        }

        /// <summary>
        /// Creates a XmlDocument with version header, a Root node and a Mods node.
        /// </summary>
        /// <returns>The created XmlDocument.</returns>
        private static XmlNode CreateXmlDocument()
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration(XMLVERSION, XMLUTF8, null);
            doc.AppendChild(docNode);

            XmlNode root = doc.CreateElement(Constants.ROOTNODE);
            doc.AppendChild(root);

            XmlNode modsNode = doc.CreateElement(Constants.MODS);
            root.AppendChild(modsNode);

            return modsNode;
        }

        /// <summary>
        /// Creates a XmlNode for the mods and all its childes from an XmlFile.
        /// </summary>
        /// <param name="doc">The XmlDocument for XmlNode creation.</param>
        /// <param name="mod">The mod node to get the information for the XMLNode.</param>
        /// <returns>A XmlNode with the infos from the mod.</returns>
        private static XmlNode CreateModXmlNode(XmlDocument doc, ModNode mod)
        {
            XmlNode modNode = doc.CreateElement(Constants.MOD);

            XmlAttribute nodeAttribute = doc.CreateAttribute(Constants.KEY);
            nodeAttribute.Value = Path.GetFileName(mod.Key);
            modNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.NAME);
            nodeAttribute.Value = mod.Name;
            modNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.VERSIONCONTROLERNAME);
            nodeAttribute.Value = mod.SiteHandlerName;
            modNode.Attributes.Append(nodeAttribute);

            if (!string.IsNullOrEmpty(mod.ProductID))
            {
                nodeAttribute = doc.CreateAttribute(Constants.PRODUCTID);
                nodeAttribute.Value = mod.ProductID;
                modNode.Attributes.Append(nodeAttribute);
            }

            if (!string.IsNullOrEmpty(mod.ModURL))
            {
                nodeAttribute = doc.CreateAttribute(Constants.MODURL);
                nodeAttribute.Value = mod.ModURL;
                modNode.Attributes.Append(nodeAttribute);
            }

            if (!string.IsNullOrEmpty(mod.AdditionalURL))
            {
                nodeAttribute = doc.CreateAttribute(Constants.ADDITIONALURL);
                nodeAttribute.Value = mod.AdditionalURL;
                modNode.Attributes.Append(nodeAttribute);
            }

            foreach (ModNode child in mod.Nodes)
                modNode.AppendChild(CreateModChildEntryXmlNode(doc, child));

            return modNode;
        }

        /// <summary>
        /// Creates a XmlNode for the child nodes of the mod.
        /// </summary>
        /// <param name="doc">The XmlDocument for XmlNode creation.</param>
        /// <param name="mod">The mod node to get the information for the XMLNode.</param>
        /// <returns>A XmlNode with the infos from the mod.</returns>
        private static XmlNode CreateModChildEntryXmlNode(XmlDocument doc, ModNode mod)
        {
            XmlNode modEntryNode = doc.CreateElement(Constants.MOD_ENTRY);

            XmlAttribute nodeAttribute = doc.CreateAttribute(Constants.NAME);
            nodeAttribute.Value = mod.Text;
            modEntryNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.ISFILE);
            nodeAttribute.Value = mod.IsFile ? ONE : ZERO;
            modEntryNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.INSTALL);
            nodeAttribute.Value = mod.IsInstalled ? ONE : ZERO;
            modEntryNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.INSTALLDIR);
            nodeAttribute.Value = mod.Destination;
            modEntryNode.Attributes.Append(nodeAttribute);

            foreach (ModNode child in mod.Nodes)
                modEntryNode.AppendChild(CreateModChildEntryXmlNode(doc, child));

            return modEntryNode;
        }

        #endregion

        #region Import

        /// <summary>
        /// Starts the import process of a ModPack.
        /// </summary>
        /// <param name="fileName">Path to the ModPack.</param>
        /// <param name="modExtractDir">Destination directory of mod within the ModPack or for the downloaded mods.</param>
        /// <param name="extractMods">Flag to determine if the mods within a ModPack should be extracted.</param>
        /// <param name="downloadMods">Flag to determine if the missing mods should be downloaded.</param>
        /// <param name="copyDest">Flag to determine if the destination should be copied or if the auto destination detection should be used.</param>
        /// <param name="addOnly">Flag to determine if the mod should be installed or only added to the ModSelection.</param>
        /// <param name="messageCallback">Callback function for messages during the import process.</param>
        public static void Import(string fileName, string modExtractDir, bool extractMods, bool downloadMods, bool copyDest, bool addOnly, MessageCallbackHandler messageCallback = null)
        {
            string tempDocPath = Path.Combine(Path.GetTempPath(), KSPTEMPDIR);
            Directory.CreateDirectory(tempDocPath);

            bool found = false;
            using (var archive = ArchiveFactory.Open(fileName))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FilePath == MODPACKXML)
                    {
                        entry.WriteToDirectory(tempDocPath);
                        found = true;
                        if (!extractMods)
                            break;
                    }
                    else if (extractMods && (entry.FilePath.Contains(MODS_FOLDER) || entry.FilePath.Contains(MODS_FOLDER_WIN)))
                    {
                        if (messageCallback != null)
                            messageCallback(null, string.Format("Extracting mod \"{0}\"", entry.FilePath));

                        entry.WriteToDirectory(modExtractDir);
                    }
                }
            }

            if (found)
            {
                List<ImportInfo> importQueue = new List<ImportInfo>();

                XmlDocument doc = new XmlDocument();
                doc.Load(Path.Combine(tempDocPath, MODPACKXML));
                XmlNodeList nodeList = doc.GetElementsByTagName(Constants.MOD);
                foreach (XmlNode mod in nodeList)
                {
                    ImportInfo importInfo = GetImportInfo(mod);
                    importInfo.LocalPath = Path.Combine(modExtractDir, importInfo.LocalPath);
                    if (downloadMods && !File.Exists(importInfo.LocalPath))
                    {
                        if (messageCallback != null)
                            messageCallback(importInfo, string.Format(Messages.MSG_DOWNLOADING_MOD_0, importInfo.Name));

                        if (importInfo.SiteHandler == null || !DownloadMod(ref importInfo))
                            continue;

                        importQueue.Add(importInfo);
                    }
                    else if (File.Exists(importInfo.LocalPath))
                    {
                        if (messageCallback != null)
                            messageCallback(importInfo, string.Format(Messages.MSG_DOWNLOADING_MOD_0, importInfo.Name));

                        importInfo.ModInfo = GetModInfo(importInfo);
                        importInfo.DownloadSuccessfull = true;
                        importQueue.Add(importInfo);
                    }
                    else
                    {
                        if (messageCallback != null)
                            messageCallback(importInfo, string.Format("Import skipped! Mod Archive \"{0}\"not found.", importInfo.LocalPath));
                    }
                }

                if (importQueue.Count > 0)
                    ImportMods(importQueue, copyDest, addOnly, messageCallback);
            }
            else
            {
                if (messageCallback != null)
                    messageCallback(null, Messages.MSG_MODPACK_INFOFILE_NOT_FOUND);
            }

            if (Directory.Exists(tempDocPath))
                Directory.Delete(tempDocPath, true);
        }

        /// <summary>
        /// Downloads the mod from ModURL.
        /// </summary>
        /// <param name="importInfo">The ImportInfo of the mod to download.</param>
        /// <returns>True if download was successful, otherwise false.</returns>
        private static bool DownloadMod(ref ImportInfo importInfo)
        {
            ISiteHandler siteHandler = importInfo.SiteHandler;
            if (siteHandler != null)
            {
                ModInfo modInfo = siteHandler.GetModInfo(importInfo.ModURL);
                importInfo.DownloadSuccessfull = siteHandler.DownloadMod(ref modInfo);
                importInfo.ModInfo = modInfo;
            }

            return importInfo.DownloadSuccessfull;
        }

        /// <summary>
        /// Gets the ImportInfos from a XmlNode.
        /// </summary>
        /// <param name="mod">The XmlNode to get the ImportInfos from.</param>
        /// <returns>The ImportInfos from a XmlNode.</returns>
        private static ImportInfo GetImportInfo(XmlNode mod)
        {
            ImportInfo importInfo = new ImportInfo();
            if (mod.Attributes == null)
                return null;

            foreach (XmlAttribute att in mod.Attributes)
            {
                if (att.Name == Constants.KEY)
                    importInfo.LocalPath = att.Value;
                else if (att.Name == Constants.NAME)
                    importInfo.Name = att.Value;
                else if (att.Name == Constants.VERSIONCONTROLERNAME)
                    importInfo.SiteHandlerName = att.Value;
                else if (att.Name == Constants.PRODUCTID)
                    importInfo.ProductID = att.Value;
                else if (att.Name == Constants.MODURL)
                    importInfo.ModURL = att.Value;
                else if (att.Name == Constants.FORUMURL)
                    importInfo.AdditionalURL = att.Value;
                else if (att.Name == Constants.ISFILE)
                    importInfo.IsFile = (att.Value == ONE);
                else if (att.Name == Constants.INSTALL)
                    importInfo.Install = (att.Value == ONE);
                else if (att.Name == Constants.INSTALLDIR)
                    importInfo.InstallDir = att.Value;
            }

            foreach (XmlNode child in mod.ChildNodes)
                importInfo.AddChild(GetImportInfo(child));

            return importInfo;
        }

        /// <summary>
        /// Gets the ModInfo from a ImportInfo class.
        /// </summary>
        /// <param name="importInfo">The ImportInfo class to get the ModInfos from.</param>
        /// <returns>The ModInfo from a ImportInfo class.</returns>
        private static ModInfo GetModInfo(ImportInfo importInfo)
        {
            ModInfo modInfo = new ModInfo();
            if (importInfo.ModInfo != null)
                modInfo = importInfo.ModInfo;
            else
            {
                modInfo.ModURL = importInfo.ModURL;
                modInfo.AdditionalURL = importInfo.AdditionalURL;
                modInfo.LocalPath = importInfo.LocalPath;
                modInfo.Name = importInfo.Name;
                modInfo.ProductID = importInfo.ProductID;
                modInfo.SiteHandlerName = importInfo.SiteHandlerName;
            }

            return modInfo;
        }

        /// <summary>
        /// Imports all mod from the importQueue to the current selected KSP install path.
        /// </summary>
        /// <param name="importQueue">A list of ImportInfos of the mods to import.</param>
        /// <param name="copyDest">Flag to determine if the destination should be copied or if the auto destination detection should be used.</param>
        /// <param name="addOnly">Flag to determine if the mod should be installed or only added to the ModSelection.</param>
        /// <param name="messageCallback">Callback function for messages during the import process.</param>
        private static void ImportMods(List<ImportInfo> importQueue, bool copyDest, bool addOnly, MessageCallbackHandler messageCallback = null)
        {
            foreach (ImportInfo importInfo in importQueue)
            {
                if (!importInfo.DownloadSuccessfull)
                    continue;

                if (messageCallback != null)
                    messageCallback(importInfo, string.Format(Messages.MSG_IMPORT_0_STARTED, importInfo.Name));

                try
                {
                    if (importInfo.DownloadSuccessfull)
                        ImportMod(importInfo, copyDest, addOnly, messageCallback);

                    if (messageCallback != null)
                        messageCallback(importInfo, string.Format(Messages.MSG_IMPORT_0_DONE, importInfo.Name));
                }
                catch (Exception ex)
                {
                    string errMsg = ex.Message;
                    if (messageCallback != null)
                        messageCallback(importInfo, string.Format(Messages.MSG_IMPORT_0_FAILED_ERROR_1, importInfo.Name, ex.Message));
                }
            }
        }

        /// <summary>
        /// Imports a mod to the current selected KSP install path.
        /// </summary>
        /// <param name="importInfo">The ImportInfo </param>
        /// <param name="copyDest">Flag to determine if the destination should be copied or if the auto destination detection should be used.</param>
        /// <param name="addOnly">Flag to determine if the mod should be installed or only added to the ModSelection.</param>
        /// <param name="messageCallback">Callback function for messages during the import process.</param>
        private static void ImportMod(ImportInfo importInfo, bool copyDest, bool addOnly, MessageCallbackHandler messageCallback = null)
        {
            ModInfo modInfo = importInfo.ModInfo;
            ModNode addedMod = ModSelectionController.AddMods(new ModInfo[] { modInfo }, false).FirstOrDefault();

            if (addedMod != null)
            {
                if (copyDest)
                {
                    // remove all destinations and uncheck all nodes.
                    ModNodeHandler.SetDestinationPaths(addedMod, string.Empty);
                    addedMod._Checked = false;

                    // copy destination
                    if (messageCallback != null)
                        messageCallback(null, string.Format("Copy desitnations of mod \"{0}\"", addedMod.Name));

                    TryCopyDestToMatchingNodes(importInfo, addedMod);
                }

                // install the mod.
                if (!addOnly)
                {
                    if (messageCallback != null)
                        messageCallback(null, string.Format("Installing mod \"{0}\"", addedMod.Name));

                    ModSelectionController.ProcessMods(new ModNode[] { addedMod });
                }
            }
            else
            {
                if (messageCallback != null)
                    messageCallback(importInfo, string.Format(Messages.MSG_IMPORT_0_FAILED, importInfo.Name));
            }
        }

        /// <summary>
        /// Tries to find notes in the new mod, that matches to the outdated mod.
        /// If a matching node was found the destination and/or the checked state of the node will be copied.
        /// </summary>
        /// <param name="importInfo">The outdated mod.</param>
        /// <param name="newMod">The new (updated) mod.</param>
        /// <returns>True if matching files where found, otherwise false.</returns>
        private static bool TryCopyDestToMatchingNodes(ImportInfo importInfo, ModNode newMod)
        {
            bool matchFound = false;
            List<ImportInfo> childs = importInfo.GetChildes();
            if (childs.Count == 0)
                return matchFound;

            foreach (var importFile in childs)
            {
                string path = GetTreePathToRootNode(importFile);
                ModNode matchingNew = ModSelectionTreeModel.SearchNodeByPath(path, newMod, '/');
                if (matchingNew != null)
                {
                    matchFound = true;
                    matchingNew.Destination = GetDestination(importFile);
                    matchingNew._Checked = importFile.Install;
                }

                if (TryCopyDestToMatchingChildNodes(importFile.GetChildes(), newMod))
                    matchFound = true;
            }

            #region old code

            //foreach (var importFile in childs)
            //{
            //    ImportInfo parentImport = importFile.Parent;
            //    if (parentImport == null)
            //        continue;

            //    string path = parentImport.Name + '\\' + importFile.Name;
            //    ModNode matchingNew = MainForm.Instance.ModSelection.SearchNodeByPath(path, newMod, '\\');
            //    if (matchingNew == null)
            //        continue;

            //    matchFound = true;
            //    matchingNew.Destination = GetDestination(importFile);
            //    ((ModNode)matchingNew.Parent).Destination = GetDestination(importFile.Parent);
            //    MainForm.Instance.ModSelection.tvModSelection.ChangeCheckedState(matchingNew, importFile.Install, true, true);

            //    ModNode parentNew = matchingNew;
            //    while (parentImport != null)
            //    {
            //        if (parentImport.Parent == null)
            //            break;

            //        path = parentImport.Parent.Name + '\\' + path;
            //        if (MainForm.Instance.ModSelection.SearchNodeByPath(path, newMod, '\\') == null)
            //            break;

            //        parentNew = (ModNode)parentNew.Parent;
            //        if (parentNew == null)
            //            break;

            //        if (MainForm.Instance.Options.ModUpdateBehavior == ModUpdateBehavior.CopyDestination)
            //            parentNew.Destination = GetDestination(parentImport);
            //        parentNew.Checked = parentImport.Install;
            //        parentImport = parentImport.Parent;
            //    }
            //}

            #endregion

            return matchFound;
        }

        private static bool TryCopyDestToMatchingChildNodes(List<ImportInfo> childImportInfo, ModNode newMod)
        {
            bool matchFound = false;
            foreach (var importFile in childImportInfo)
            {
                string path = GetTreePathToRootNode(importFile);
                ModNode matchingNew = ModSelectionTreeModel.SearchNodeByPath(path, newMod, '/');
                if (matchingNew != null)
                {
                    matchFound = true;
                    matchingNew.Destination = GetDestination(importFile);
                    matchingNew._Checked = importFile.Install;
                }

                if (TryCopyDestToMatchingChildNodes(importFile.GetChildes(), newMod))
                    matchFound = true;
            }

            return matchFound;
        }

        /// <summary>
        /// Gets the tree path up from the passed node up to its root node (last node with destination).
        /// </summary>
        /// <param name="importInfo"></param>
        /// <returns></returns>
        private static string GetTreePathToRootNode(ImportInfo importInfo)
        {
            string path = string.Empty;

            path = "/" + importInfo.Name;
            ImportInfo parent = importInfo.Parent;
            while (parent != null)
            {
                if (string.IsNullOrEmpty(parent.InstallDir))
                    break;

                path = "/" + parent.Name + path;

                parent = parent.Parent;
            }

            return path;
        }

        /// <summary>
        /// Builds the destination path from the relative ImportInfo.Installdir.
        /// </summary>
        /// <param name="importInfo">The ImportInfo to build the destination path from.</param>
        /// <returns>The new destination path.</returns>
        private static string GetDestination(ImportInfo importInfo)
        {
            if (string.IsNullOrEmpty(importInfo.InstallDir))
                return string.Empty;

            return importInfo.InstallDir;
        }

        #region internal classes

        /// <summary>
        /// ImportInfo contains information for the import of a mod.
        /// </summary>
        public class ImportInfo
        {
            #region Properties

            public string LocalPath { get; set; }

            public string Name { get; set; }

            public ISiteHandler SiteHandler { get { return SiteHandlerManager.GetSiteHandlerByName(SiteHandlerName); } }

            public string SiteHandlerName { get; set; }

            public string ProductID { get; set; }

            public string ModURL { get; set; }

            public string AdditionalURL { get; set; }

            public ImportInfo Parent { get; set; }

            private List<ImportInfo> Childs { get; set; }

            public ModInfo ModInfo { get; set; }

            public bool IsFile { get; set; }

            public bool Install { get; set; }

            public string InstallDir { get; set; }

            public bool DownloadSuccessfull { get; set; }

            #endregion


            /// <summary>
            /// Creates a instance of the ImportInfo class.
            /// </summary>
            public ImportInfo()
            {
                LocalPath = string.Empty;
                Name = string.Empty;
                SiteHandlerName = Messages.NONE;
                ProductID = string.Empty;
                ModURL = string.Empty;
                AdditionalURL = string.Empty;
                Parent = null;
                Childs = new List<ImportInfo>();
                ModInfo = null;
                IsFile = false;
                Install = false;
                InstallDir = string.Empty;
                DownloadSuccessfull = false;
            }


            /// <summary>
            /// Adds a child ImportInfo to this ImportInfo.
            /// </summary>
            /// <param name="importInfo">The child ImportInfo to add.</param>
            /// <returns></returns>
            public ImportInfo AddChild(ImportInfo importInfo)
            {
                Childs.Add(importInfo);
                importInfo.Parent = this;

                return importInfo;
            }

            /// <summary>
            /// Gets a list of all childes of this node.
            /// </summary>
            /// <returns>A list of all childes of this node.</returns>
            public List<ImportInfo> GetChildes()
            {
                return Childs;
            }
        }

        #endregion

        #endregion
    }
}
