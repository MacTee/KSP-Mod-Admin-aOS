using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using KSPModAdmin.Core.Views;
using SharpCompress.Archive;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using KSPModAdmin.Core.Controller;

namespace KSPModAdmin.Core.Utils
{
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

        #endregion

        #region Export

        /// <summary>
        /// Starts the export process.
        /// </summary>
        /// <param name="modsToExport">List of mods to export.</param>
        /// <param name="fileName">Filename for the new created ModPack.</param>
        /// <param name="includeMods">Flag to determine if the mod archives should be included to.</param>
        public static void Export(List<ModNode> modsToExport, string fileName, bool includeMods = false)
        {
            XmlNode modsNode = CreateXmlDocument();
            XmlDocument doc = modsNode.OwnerDocument;
            string tempDocPath = Path.Combine(Path.GetTempPath(), KSPTEMPDIR, Path.GetTempFileName());

            using (var archive = ZipArchive.Create())
            {
                foreach (var mod in modsToExport)
                {
                    Messenger.AddInfo(string.Format("Add mod {0} to ModPack...", mod.Text));

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
            nodeAttribute.Value = Path.GetFileName(mod.Name);
            modNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.NAME);
            nodeAttribute.Value = mod.Text;
            modNode.Attributes.Append(nodeAttribute);

            nodeAttribute = doc.CreateAttribute(Constants.VERSIONCONTROL);
            nodeAttribute.Value = ((int)mod.VersionControl).ToString();
            modNode.Attributes.Append(nodeAttribute);

            if (!string.IsNullOrEmpty(mod.ProductID))
            {
                nodeAttribute = doc.CreateAttribute(Constants.PRODUCTID);
                nodeAttribute.Value = mod.ProductID;
                modNode.Attributes.Append(nodeAttribute);
            }

            if (!string.IsNullOrEmpty(mod.SpaceportURL))
            {
                nodeAttribute = doc.CreateAttribute(Constants.MODURL);
                nodeAttribute.Value = mod.SpaceportURL;
                modNode.Attributes.Append(nodeAttribute);
            }

            if (!string.IsNullOrEmpty(mod.KSPForumURL))
            {
                nodeAttribute = doc.CreateAttribute(Constants.FORUMURL);
                nodeAttribute.Value = mod.KSPForumURL;
                modNode.Attributes.Append(nodeAttribute);
            }

            if (!string.IsNullOrEmpty(mod.CurseForgeURL))
            {
                nodeAttribute = doc.CreateAttribute(Constants.CURSEFORGEURL);
                nodeAttribute.Value = mod.CurseForgeURL;
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
            nodeAttribute.Value = mod.Destination.ToLower().Replace((OptionsController.SelectedKSPPath + "\\").ToLower(), string.Empty);
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
        public static void Import(frmBase parentWindow, string fileName, string modExtractDir, bool extractMods, bool downloadMods, bool copyDest, bool addOnly)
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
                    else if (extractMods && (entry.FilePath.Contains("/Mods/") || entry.FilePath.Contains("\\Mods\\")))
                    {
                        entry.WriteToDirectory(modExtractDir);
                    }
                }
            }

            if (found)
            {
                List<ImportInfo> downloadQueue = new List<ImportInfo>();
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
                        if (!AddDownloadInfos(parentWindow, ref importInfo))
                            continue;

                        downloadQueue.Add(importInfo);
                        importQueue.Add(importInfo);
                    }
                    else if (File.Exists(importInfo.LocalPath))
                    {
                        importInfo.ModInfo = GetModInfo(importInfo);
                        importQueue.Add(importInfo);
                        importInfo.DownloadSuccessfull = true;
                    }
                }

                if (downloadQueue.Count > 0)
                    DownloadMods(downloadQueue);

                if (importQueue.Count > 0)
                    ImportMods(importQueue, copyDest, addOnly);
            }
            else
            {
                Messenger.AddInfo("ModPack info file not found!");
            }

            if (Directory.Exists(tempDocPath))
                Directory.Delete(tempDocPath, true);
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
                else if (att.Name == Constants.VERSIONCONTROL)
                    importInfo.VersionControl = (VersionControl)int.Parse(att.Value);
                else if (att.Name == Constants.PRODUCTID)
                    importInfo.ProductID = att.Value;
                else if (att.Name == Constants.MODURL)
                    importInfo.SpaceportURL = att.Value;
                else if (att.Name == Constants.FORUMURL)
                    importInfo.ForumURL = att.Value;
                else if (att.Name == Constants.CURSEFORGEURL)
                    importInfo.CurseForgeURL = att.Value;
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
        /// Gets the download infos of the mod (gets website and parses it to get the ModInfos).
        /// </summary>
        /// <param name="parentWindow"></param>
        /// <param name="importInfo">The ImportInfo with the URL to the mod website.</param>
        /// <returns>True if the ModInfos could be downloaded and parsed.</returns>
        private static bool AddDownloadInfos(frmBase parentWindow, ref ImportInfo importInfo)
        {
            switch (importInfo.VersionControl)
            {
                case VersionControl.KSPForum:
                    if (KSPForum.IsValidURL(importInfo.ForumURL))
                    {
                        string forumURL = importInfo.ForumURL;
                        string downloadURL = string.Empty;
                        parentWindow.InvokeIfRequired(() => downloadURL = KSPForum.GetDownloadURL(forumURL));
                        importInfo.DownloadURL = downloadURL;

                        if (!string.IsNullOrEmpty(importInfo.DownloadURL))
                            importInfo.ModInfo = KSPForum.GetModInfo(importInfo.ForumURL);
                    }
                    break;

                case VersionControl.CurseForge:
                    if (CurseForge.IsValidURL(importInfo.CurseForgeURL))
                        importInfo.ModInfo = CurseForge.GetModInfo(importInfo.CurseForgeURL);
                    break;
            }

            return (importInfo.ModInfo != null);
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
                modInfo.CurseForgeURL = importInfo.CurseForgeURL;
                modInfo.ForumURL = importInfo.ForumURL;
                modInfo.LocalPath = importInfo.LocalPath;
                modInfo.Name = importInfo.Name;
                modInfo.ProductID = importInfo.ProductID;
                modInfo.SpaceportURL = importInfo.SpaceportURL;
                modInfo.VersionControl = importInfo.VersionControl;
            }

            return modInfo;
        }

        /// <summary>
        /// Downloads all mods in the downloadQueue.
        /// </summary>
        /// <param name="downloadQueue">A list of ImportInfos of the mods to download.</param>
        private static void DownloadMods(List<ImportInfo> downloadQueue)
        {
            foreach (ImportInfo importInfo in downloadQueue)
            {
                Messenger.AddInfo(string.Format("Download of {0} started ...", importInfo.Name));

                ModInfo modInfo = importInfo.ModInfo;
                switch (importInfo.VersionControl)
                {
                    case VersionControl.KSPForum:
                        importInfo.DownloadSuccessfull = KSPForum.DownloadMod(importInfo.DownloadURL, ref modInfo);
                        break;

                    case VersionControl.CurseForge:
                        importInfo.DownloadSuccessfull = CurseForge.DownloadMod(ref modInfo);
                        break;

                    default:
                        importInfo.DownloadSuccessfull = false;
                        break;
                }

                if (importInfo.DownloadSuccessfull)
                    Messenger.AddInfo(string.Format("Download of {0} done.", importInfo.Name));
                else
                    Messenger.AddInfo(string.Format("Download of {0} failed!", importInfo.Name));
            }
        }

        /// <summary>
        /// Imports all mod from the importQueue to the current selected KSP install path.
        /// </summary>
        /// <param name="importQueue">A list of ImportInfos of the mods to import.</param>
        /// <param name="copyDest">Flag to determine if the destination should be copied or if the auto destination detection should be used.</param>
        /// <param name="addOnly">Flag to determine if the mod should be installed or only added to the ModSelection.</param>
        private static void ImportMods(List<ImportInfo> importQueue, bool copyDest, bool addOnly)
        {
            foreach (ImportInfo importInfo in importQueue)
            {
                if (!importInfo.DownloadSuccessfull)
                    continue;

                Messenger.AddInfo(string.Format("Import of {0} started ...", importInfo.Name));

                try
                {
                    if (importInfo.DownloadSuccessfull)
                        ImportMod(importInfo, copyDest, addOnly);

                    Messenger.AddInfo(string.Format("Import of {0} done.", importInfo.Name));
                }
                catch (Exception ex)
                {
                    Messenger.AddInfo(string.Format("Import of {0} failed! Error: {1}", importInfo.Name, ex.Message));
                }
            }
        }

        /// <summary>
        /// Imports a mod to the current selected KSP install path.
        /// </summary>
        /// <param name="importInfo">The ImportInfo </param>
        /// <param name="copyDest">Flag to determine if the destination should be copied or if the auto destination detection should be used.</param>
        /// <param name="addOnly">Flag to determine if the mod should be installed or only added to the ModSelection.</param>
        private static void ImportMod(ImportInfo importInfo, bool copyDest, bool addOnly)
        {
            ModInfo modInfo = importInfo.ModInfo;
            ModNode addedMod = ModSelectionController.AddMods(new ModInfo[] { modInfo }, false).FirstOrDefault();

            if (addedMod != null)
            {
                if (copyDest)
                {
                    // remove all destinations and uncheck all nodes.
                    addedMod.SetDestinationPaths("");
                    TreeViewEx.ChangeCheckedState(addedMod, false, true, true);
                    // copy destination
                    TryCopyDestToMatchingNodes(importInfo, addedMod);
                }

                // install the mod.
                if (!addOnly)
                    ModSelectionController.ProcessNodes(new ModNode[] { addedMod });
            }
            else
            {
                Messenger.AddInfo(string.Format("Import of {0} failed!", importInfo.Name));
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
                ImportInfo parentImport = importFile.Parent;

                string path = parentImport.Name + '\\' + importFile.Name;
                ModNode matchingNew = ModNode.SearchNodeByPath(path, newMod, '\\');
                if (matchingNew != null)
                {
                    matchFound = true;
                    matchingNew.Destination = GetDestination(importFile);
                    TreeViewEx.ChangeCheckedState(matchingNew, importFile.Install, true, true);
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
            //    TreeNodeMod matchingNew = MainForm.Instance.ModSelection.SearchNodeByPath(path, newMod, '\\');
            //    if (matchingNew == null)
            //        continue;

            //    matchFound = true;
            //    matchingNew.Destination = GetDestination(importFile);
            //    ((TreeNodeMod)matchingNew.Parent).Destination = GetDestination(importFile.Parent);
            //    MainForm.Instance.ModSelection.tvModSelection.ChangeCheckedState(matchingNew, importFile.Install, true, true);

            //    TreeNodeMod parentNew = matchingNew;
            //    while (parentImport != null)
            //    {
            //        if (parentImport.Parent == null)
            //            break;

            //        path = parentImport.Parent.Name + '\\' + path;
            //        if (MainForm.Instance.ModSelection.SearchNodeByPath(path, newMod, '\\') == null)
            //            break;

            //        parentNew = (TreeNodeMod)parentNew.Parent;
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

        /// <summary>
        /// Tries to find notes in the new mod, that matches to one of the child nodes.
        /// If a matching node was found the destination and/or the checked state of the node will be copied.
        /// </summary>
        /// <param name="childImportInfo">The child list of outdated mod ImportInfo.</param>
        /// <param name="newMod">The new (updated) mod.</param>
        /// <returns>True if matching files where found, otherwise false.</returns>
        private static bool TryCopyDestToMatchingChildNodes(List<ImportInfo> childImportInfo, ModNode newMod)
        {
            bool matchFound = false;
            foreach (var importFile in childImportInfo)
            {
                ImportInfo parentImport = importFile.Parent;

                string path = parentImport.Name + '\\' + importFile.Name;
                ModNode matchingNew = ModNode.SearchNodeByPath(path, newMod, '\\');
                if (matchingNew != null)
                {
                    matchFound = true;
                    matchingNew.Destination = GetDestination(importFile);
                    TreeViewEx.ChangeCheckedState(matchingNew, importFile.Install, true, true);
                }

                if (TryCopyDestToMatchingChildNodes(importFile.GetChildes(), newMod))
                    matchFound = true;
            }

            return matchFound;
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

            return Path.Combine(KSPPathHelper.GetPath(KSPPaths.KSPRoot), importInfo.InstallDir);
        }

        #region Internal classes

        /// <summary>
        /// ImportInfo contains information for the import of a mod.
        /// </summary>
        public class ImportInfo
        {
            #region Properties

            public string LocalPath { get; set; }

            public string Name { get; set; }

            public VersionControl VersionControl { get; set; }

            public string ProductID { get; set; }

            public string SpaceportURL { get; set; }

            public string DownloadURL { get; set; }

            public string ForumURL { get; set; }

            public string CurseForgeURL { get; set; }

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
                VersionControl = VersionControl.None;
                ProductID = string.Empty;
                SpaceportURL = string.Empty;
                DownloadURL = string.Empty;
                ForumURL = string.Empty;
                CurseForgeURL = string.Empty;
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
