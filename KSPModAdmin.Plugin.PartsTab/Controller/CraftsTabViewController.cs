using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Model;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Views;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Controller
{
    /// <summary>
    /// Controller class for the CraftsTab view.
    /// </summary>
    public class CraftsTabViewController
    {
        private const string EXTENSION_CRAFT = "*.craft";

        private static CraftsTabViewController instance = null;
        private static List<CraftNode> allCrafts = new List<CraftNode>();
        private static CraftsTreeModel model = new CraftsTreeModel();

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static CraftsTabViewController Instance
        {
            get { return instance ?? (instance = new CraftsTabViewController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucCraftsTabView View { get; protected set; }

        #endregion

        internal static void Initialize(ucCraftsTabView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;
            EventDistributor.LanguageChanged += LanguageChanged;
            EventDistributor.KSPRootChanged += KSPRootChanged;

            // Add your stuff to initialize here.
            View.Model = model;
            View.AddActionKey(VirtualKey.VK_DELETE, (x) =>
            {
                RemoveSelectedCraft();
                return true;
            });
            View.AddActionKey(VirtualKey.VK_BACK, (x) =>
            {
                RemoveSelectedCraft();
                return true;
            });
        }

        #region EventDistributor callback functions.

        /// <summary>
        /// Callback function for the AsyncTaskStarted event.
        /// Should disable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskStarted(object sender)
        {
            View.SetEnabledOfAllControls(false);
        }

        /// <summary>
        /// Callback function for the AsyncTaskDone event.
        /// Should enable all controls of the BaseView.
        /// </summary>
        protected static void AsyncTaskDone(object sender)
        {
            View.SetEnabledOfAllControls(true);
        }

        /// <summary>
        /// Callback function for the LanguageChanged event.
        /// This is the place where you can translate non accessible controls.
        /// </summary>
        protected static void LanguageChanged(object sender)
        {
            View.LanguageChanged();
        }

        /// <summary>
        /// Callback function for the KSPRootChanged event.
        /// This is the place to handle a change of the selected KSP installation path..
        /// </summary>
        private static void KSPRootChanged(string kspPath)
        {
            ResetView();
        }

        #endregion

        /// <summary>
        /// Refreshes the crafts tab.
        /// Clears the TreeView and starts a new part and craft scan.
        /// </summary>
        public static void RefreshCraftsTab()
        {
            ScanDir();
        }

        /// <summary>
        /// Checks all crafts if they uses a unknown part.
        /// Searches in the parts list from the PartsTab.
        /// </summary>
        public static void ValidateCrafts()
        {
            PartsTabViewController.ScanComplete += Parts_ScanComplete;
            PartsTabViewController.RefreshPartsTab();
        }

        /// <summary>
        /// Opens the Craft Editor with the selected craft.
        /// </summary>
        public static void RenameSelectedCraft()
        {
            MessageBox.Show("Not implemented yet!", string.Empty);
        }

        /// <summary>
        /// Swaps the start building of the selected craft.
        /// </summary>
        public static void SwapBuildingOfSelectedCraft()
        {
            MessageBox.Show("Not implemented yet!", string.Empty);
        }

        /// <summary>
        /// Removes the selected craft
        /// </summary>
        public static void RemoveSelectedCraft()
        {
            MessageBox.Show("Not implemented yet!", string.Empty);
        }

        /// <summary>
        /// Clears and refills the TreeView.
        /// </summary>
        public static void RefreshTreeView()
        {
            int count = 0;
            View.InvokeIfRequired(() => model.Nodes.Clear());

            ////allCrafts.Sort((c1, c2) => c1.Text.CompareTo(c2.Text));

            foreach (CraftNode node in allCrafts)
                if (View.SelectedBuildingFilter == PartsTabViewController.All || node.Type.ToLower().Contains(View.SelectedBuildingFilter.ToLower()))
                {
                    View.InvokeIfRequired(() => model.Nodes.Add(node));
                    ++count;
                }

            View.CraftCountText = string.Format(Messages.MSG_CRAFTS_COUNT_TEXT, count, allCrafts.Count);
        }

        /// <summary>
        /// Clears all lists and TreeView.
        /// </summary>
        public static void ResetView()
        {
            model.Nodes.Clear();
            allCrafts.Clear();
            View.SelectedBuildingFilter = PartsTabViewController.All;

            View.InvalidateView();
        }

        #region Scan

        /// <summary>
        /// Scans the KSP install directory and sub directories for *.craft files.
        /// </summary>
        private static void ScanDir()
        {
            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            ResetView();

            AsyncTask<bool>.DoWork(() =>
                {
                    Messenger.AddInfo(Messages.MSG_CRAFT_SCAN_STARTED);

                    // Get part.cfg files from GameData folder.
                    string gameDatePath = KSPPathHelper.GetPath(KSPPaths.GameData);
                    string[] files = Directory.GetFiles(gameDatePath, EXTENSION_CRAFT, SearchOption.AllDirectories);

                    // Get part.cfg files from additional folders.
                    string path1 = KSPPathHelper.GetPath(KSPPaths.VAB);
                    string path2 = KSPPathHelper.GetPath(KSPPaths.SPH);
                    string[] addPaths = new[] { path1, path2 };
                    foreach (var path in addPaths)
                    {
                        string[] files2 = Directory.GetFiles(path, EXTENSION_CRAFT, SearchOption.AllDirectories);
                        int oldLength = files.Length;
                        Array.Resize<string>(ref files, oldLength + files2.Length);
                        Array.Copy(files2, 0, files, oldLength, files2.Length);
                    }

                    // Create CraftNodes from each file.
                    var nodes = new List<CraftNode>();
                    if (files.Length > 0)
                        foreach (string file in files)
                        {
                            Messenger.AddInfo(string.Format(Messages.MSG_SCAN_FILE_0_FOR_CRAFTS, file));
                            var newNodes = CreateCraftEntry(file);
                            foreach (var newNode in newNodes)
                            {
                                if (newNode != null && !string.IsNullOrEmpty(newNode.Name) && !nodes.Contains(newNode))
                                    nodes.Add(newNode);
                            }
                        }
                    else
                        Messenger.AddInfo(string.Format(Messages.MSG_NO_CRAFTCFG_FOUND_0, gameDatePath));

                    allCrafts.Clear();
                    foreach (CraftNode node in nodes)
                        allCrafts.Add(node);

                    Messenger.AddInfo(Messages.MSG_CRAFT_SCAN_DONE);

                    return true;
                },
                (bool result, Exception ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_CRAFT_READING_0, ex.Message), ex);
                    else
                    {
                        RefreshTreeView();
                        ValidateCrafts();
                    }
                });
        }

        /// <summary>
        /// Creates crafts from the passed file.
        /// </summary>
        /// <param name="file">Full path to the craft file.</param>
        /// <returns>The crafts from the passed file.</returns>
        private static List<CraftNode> CreateCraftEntry(string file)
        {
            string adjustedPath = KSPPathHelper.GetAbsolutePath(file);

            var result = new List<CraftNode>();
            if (string.IsNullOrEmpty(file) || !File.Exists(adjustedPath))
                return result;

            CraftNode craftNode = new CraftNode();
            craftNode.Name = file;
            craftNode.FilePath = file;
            craftNode.Folder = GetCraftFolder(adjustedPath);
            result.Add(craftNode);

            bool partInfo = false;
            int bracetCount = 0;
            string[] lines = File.ReadLines(file).ToArray<string>();
            foreach (string line in lines)
            {
                string tempLine = line.Trim();
                if (!partInfo)
                {
                    if (tempLine.ToLower().StartsWith("ship =") || tempLine.ToLower().StartsWith("ship="))
                    {
                        string name = tempLine.Split('=')[1];
                        craftNode.Text = name.Trim();
                        craftNode.Name = name.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("type =") || tempLine.ToLower().StartsWith("type="))
                    {
                        string type = tempLine.Split('=')[1];
                        craftNode.Type = type.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("version =") || tempLine.ToLower().StartsWith("version="))
                    {
                        string version = tempLine.Split('=')[1];
                        craftNode.Version = version.Trim();
                    }

                    else if (tempLine.ToLower().StartsWith("part"))
                    {
                        partInfo = true;
                    }
                }
                else
                {
                    if (tempLine.StartsWith("{"))
                        ++bracetCount;

                    else if (tempLine.StartsWith("}"))
                    {
                        --bracetCount;
                        if (bracetCount < 1)
                            partInfo = false;
                    }

                    else if (tempLine.ToLower().StartsWith("part =") || tempLine.ToLower().StartsWith("part="))
                    {
                        string partName = tempLine.Split('=')[1].Trim();
                        partName = partName.Substring(0, partName.LastIndexOf("_"));
                        if (!craftNode.ContainsPart(partName))
                            craftNode.Nodes.Add(new CraftNode() { Name = partName + " (1)", FilePath = partName });
                        else
                        {
                            try
                            {
                                CraftNode part = craftNode.GetPart(partName);
                                int i1 = part.Name.LastIndexOf('(') + 1;
                                if (i1 < 0)
                                    continue;

                                int length = part.Name.Length - part.Name.LastIndexOf(')');
                                if (length < 0)
                                    continue;

                                string str = part.Name.Substring(i1, length);
                                int i = int.Parse(str) + 1;
                                part.Name = string.Format("{0} ({1})", partName, i);
                            }
                            catch (Exception ex)
                            {
                                Messenger.AddError(ex.Message, ex);
                            }
                        }
                    }
                }
            }
            ////craftNode.SortPartsByDisplayText();

            return result;
        }

        /// <summary>
        /// Extracts the first folder name of the path.
        /// </summary>
        /// <param name="path">The path to get the first folder name of.</param>
        /// <returns>The first folder name of the path.</returns>
        private static string GetCraftFolder(string path)
        {
            string folder = KSPPathHelper.GetRelativePath(path).Replace(Constants.KSPFOLDERTAG + Path.DirectorySeparatorChar, string.Empty);
            folder = folder.Substring(0, folder.IndexOf(Path.DirectorySeparatorChar));
            return folder;
        }

        #endregion

        #region Validate

        /// <summary>
        /// Checks if all parts of the craft are installed.
        /// </summary>
        /// <param name="partList">The list of installed parts.</param>
        private static void Parts_ScanComplete(List<PartNode> partList)
        {
            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);

            AsyncTask<bool>.DoWork(() =>
                {
                    View.InvokeIfRequired(() => model.Nodes.Clear());

                    foreach (CraftNode craft in allCrafts)
                    {
                        Dictionary<string, CraftNode> alreadyCheckedParts = new Dictionary<string, CraftNode>();
                        foreach (CraftNode part in craft.Nodes)
                        {
                            string partName = part.Name.Substring(0, part.Name.IndexOf(" ("));
                            string count = part.Name.Substring(partName.Length);
                            if (alreadyCheckedParts.ContainsKey(part.Name))
                            {
                                if (alreadyCheckedParts[part.Name] != null)
                                {
                                    part.RelatedPart = alreadyCheckedParts[part.Name].RelatedPart;
                                    part.Name = part.RelatedPart.Title + count;
                                    craft.AddMod(part.RelatedPart.Mod);
                                }
                                continue;
                            }
                            else
                            {
                                bool found = false;
                                foreach (PartNode instPart in partList)
                                {
                                    if (instPart.Name.Replace("_", ".") == partName)
                                    {
                                        if (!alreadyCheckedParts.ContainsKey(instPart.Name))
                                            alreadyCheckedParts.Add(instPart.Name, part);
                                        part.Name = instPart.Title + count;
                                        part.RelatedPart = instPart;
                                        part.AddMod(instPart.Mod);
                                        View.InvokeIfRequired(() => part.RelatedPart.AddRelatedCraft(craft));
                                        craft.AddMod(instPart.Mod);
                                        found = true;
                                        break;
                                    }
                                }

                                if (!found)
                                    alreadyCheckedParts.Add(part.Name, null);
                            }
                        }

                        ////craft.SortPartsByDisplayText();
                    }

                    return true;
                },
                (result, ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);
                    PartsTabViewController.ScanComplete -= Parts_ScanComplete;

                    if (ex != null)
                        MessageBox.Show(View.ParentForm, string.Format("Error during craft validating. \"{0}\"", ex.Message));
                    else
                        RefreshTreeView();
                });
        }

        #endregion
    }
}
