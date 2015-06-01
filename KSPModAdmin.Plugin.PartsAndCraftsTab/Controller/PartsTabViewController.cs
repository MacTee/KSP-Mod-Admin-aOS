using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Helper;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Model;
using KSPModAdmin.Plugin.PartsAndCraftsTab.Views;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Controller
{
    /// <summary>
    /// Delegate for the ScanComplete event
    /// </summary>
    /// <param name="partList">The list of found parts.</param>
    public delegate void ScanCompleteHandler(List<PartNode> partList);

    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class PartsTabViewController
    {
        /// <summary>
        /// Event ScanComplete occurs when the scan of parts is complete.
        /// </summary>
        public static event ScanCompleteHandler ScanComplete;

        #region Members

        private const string PROPULSION = "Propulsion";
        private const string CONTROL = "Control";
        private const string STRUCTURAL = "Structural";
        private const string AERO = "Aero";
        private const string UTILITY = "Utility";
        private const string SCIENCE = "Science";
        private const string PODS = "Pods";
        private const string EXTENSION_CFG = "*.cfg";

        private const string PARAMETER_REGEX = "({NAME})[ ]{0,1}[=]{1}[ ]{0,1}({VALUE})";
        private const string NAMEPARAMETER = "{NAME}";
        private const string VALUEPARAMETER = "{VALUE}";
        private const string NAME = "name";
        private const string TITLE = "title";
        private const string CATEGORY = "category";

        /// <summary>
        /// Default filter to display all content.
        /// </summary>
        public const string All = "All";

        /// <summary>
        /// Filter for Squat related content.
        /// </summary>
        public const string Squad = "Squad";

        private static PartsTabViewController instance = null;
        private static List<PartNode> allNodes = new List<PartNode>();
        private static PartsTreeModel model = new PartsTreeModel();
        private static List<string> allModFilter = new List<string>();
        private static bool filling = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton of this class.
        /// </summary>
        protected static PartsTabViewController Instance
        {
            get { return instance ?? (instance = new PartsTabViewController()); }
        }

        /// <summary>
        /// Gets or sets the view of the controller.
        /// </summary>
        public static ucPartsTabView View { get; protected set; }

        /// <summary>
        /// Gets or sets the list of all known parts.
        /// </summary>
        public static List<PartNode> Parts { get { return allNodes; } set { allNodes = value; } }


        #endregion

        internal static void Initialize(ucPartsTabView view)
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
                RemoveSelectedPart();
                return true;
            });
            View.AddActionKey(VirtualKey.VK_BACK, (x) =>
            {
                RemoveSelectedPart();
                return true;
            });

            allModFilter.Add(All);
            allModFilter.Add(Squad);
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
            allNodes.Clear();
            model.Nodes.Clear();

            allModFilter.Clear();
            allModFilter.Add(All);
            allModFilter.Add(Squad);

            View.SelectedCategoryFilter = All;
            View.SelectedModFilter = All;

            View.InvalidateView();
        }

        #endregion

        /// <summary>
        /// Clears the TreeView and starts a new scan for parts.
        /// </summary>
        public static void RefreshPartsTab()
        {
            ScanDir();
        }

        /// <summary>
        /// Refreshes the TreeView without a new scan.
        /// </summary>
        public static void RefreshTreeView()
        {
            FillTreeView(allNodes);
        }

        #region Scan

        /// <summary>
        /// Scans the KSP install directory and sub directories for *.craft files.
        /// </summary>
        private static void ScanDir()
        {
            allModFilter.Clear();
            allModFilter.Add(All);
            allModFilter.Add(Squad);
            View.SelectedModFilter = All;

            model.Nodes.Clear();

            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            AsyncTask<bool>.DoWork(() =>
                {
                    Messenger.AddInfo(Messages.MSG_PART_SCAN_STARTED);

                    // Get part.cfg files from GameData folder.
                    string gameDatePath = KSPPathHelper.GetPath(KSPPaths.GameData);
                    string[] files = Directory.GetFiles(gameDatePath, EXTENSION_CFG, SearchOption.AllDirectories);

                    // Get part.cfg files from additional folders.
                    string partsPath = KSPPathHelper.GetPath(KSPPaths.Parts);
                    string[] addPaths = new[] { partsPath };
                    foreach (var path in addPaths)
                    {
                        string[] files2 = Directory.GetFiles(path, EXTENSION_CFG, SearchOption.AllDirectories);
                        int oldLength = files.Length;
                        Array.Resize<string>(ref files, oldLength + files2.Length);
                        Array.Copy(files2, 0, files, oldLength, files2.Length);
                    }

                    // Create PartNodes from each file.
                    var nodes = new List<PartNode>();
                    if (files.Length > 0)
                        foreach (string file in files)
                        {
                            Messenger.AddInfo(string.Format(Messages.MSG_SCAN_FILE_0_FOR_PARTS, file));
                            var newNodes = CreatePartNodes(file);
                            foreach (var newNode in newNodes)
                            {
                                if (newNode != null && !string.IsNullOrEmpty(newNode.Name) && !nodes.Contains(newNode)) 
                                    nodes.Add(newNode);
                            }
                        }
                    else
                        Messenger.AddInfo(string.Format(Messages.MSG_NO_PARTCFG_FOUND_0, gameDatePath));

                    allNodes.Clear();
                    foreach (PartNode node in nodes)
                        allNodes.Add(node);

                    Messenger.AddInfo(Messages.MSG_PART_SCAN_DONE);

                    return true;
                },
                (result, ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PART_READING_0, ex.Message), ex);
                    else
                        RefreshTreeView();

                    if (ScanComplete != null)
                        ScanComplete(Parts);
                });
        }

        /// <summary>
        /// Parses the file content and creates a PartNode foreach found part.
        /// </summary>
        /// <param name="file">Full path to the part file.</param>
        /// <returns>A list of PartNodes from the passed file.</returns>
        private static List<PartNode> CreatePartNodes(string file)
        {
            var result = new List<PartNode>();
            if (string.IsNullOrEmpty(file))
                return result;

            string[] lines = File.ReadLines(file).ToArray();
            if (lines.Length == 0)
                return result;

            int braceCount = 0;
            bool isPartFile = false;
            bool isWithinPartDev = false;
            PartNode partNode = null;
            foreach (string line in lines)
            {
                if (line == null)
                {
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PART_READING_0_UNEXPECTED_EMPTY_LINE, file));
                    continue;
                }

                if (line.ToLower().Trim().StartsWith("part {") || line.ToLower().Trim().StartsWith("part{"))
                {
                    isPartFile = true;
                    isWithinPartDev = true;
                    braceCount += 1;
                    AddNode(partNode, result);
                    partNode = CreateNewPartNode(file);
                }

                else if (line.ToLower().Trim().StartsWith("part"))
                    isPartFile = true;

                else if (line.Trim().StartsWith("{") || (!line.Contains("//") && line.Contains("{")))
                {
                    braceCount += 1;
                    if (braceCount == 1 && isPartFile)
                    {
                        isWithinPartDev = true;
                        AddNode(partNode, result);
                        partNode = CreateNewPartNode(file);
                    }
                    else
                        isWithinPartDev = false;
                }

                else if (line.Trim().StartsWith("}"))
                {
                    braceCount -= 1;
                    if (braceCount == 1 && isPartFile)
                        isWithinPartDev = true;
                    else if (braceCount > 1)
                        isWithinPartDev = false;
                    else if (braceCount == 0)
                    {
                        isWithinPartDev = false;
                        AddNode(partNode, result);
                    }
                }

                if (isPartFile && isWithinPartDev)
                    ParsePartLine(file, line, ref partNode);
            }

            AddNode(partNode, result);

            return result;
        }

        /// <summary>
        /// Adds a node to a list if the node and node name is not null and if the list doesn't contains the node already.
        /// </summary>
        /// <param name="node">The node to add.</param>
        /// <param name="list">The list to add to.</param>
        private static void AddNode(PartNode node, List<PartNode> list)
        {
            if (node != null && !string.IsNullOrEmpty(node.Name) && !list.Contains(node))
            {
                Messenger.AddInfo(string.Format(Messages.MSG_PART_FOUND_AND_ADDED_0, node.Name));
                list.Add(node);
            }
        }

        /// <summary>
        /// Creates a new default TreeNodePart.
        /// </summary>
        /// <param name="file">The full path of the part cfg file.</param>
        /// <returns>The new created PartNode from the passed file.</returns>
        private static PartNode CreateNewPartNode(string file)
        {
            PartNode partNode = new PartNode();
            partNode.FilePath = KSPPathHelper.GetRelativePath(file);
            if (file.Contains(Constants.GAMEDATA))
            {
                string mod = file.Substring(file.IndexOf(Constants.GAMEDATA) + 9);
                mod = mod.Substring(0, mod.IndexOf(Path.DirectorySeparatorChar));
                partNode.Mod = mod;

                if (!allModFilter.Contains(mod)) 
                    allModFilter.Add(mod);
            }

            return partNode;
        }

        /// <summary>
        /// Parses the line for part informations.
        /// </summary>
        /// <param name="file">Full path to the part cfg file.</param>
        /// <param name="line">The line to parse.</param>
        /// <param name="partNode">The node to write the informations to.</param>
        private static void ParsePartLine(string file, string line, ref PartNode partNode)
        {
            string tempLine = line.Trim();

            // TODO: change name to title!
            if (tempLine.ToLower().StartsWith("name =") || tempLine.ToLower().StartsWith("name="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH, file));

                else
                {
                    string name = nameValuePair[1].Trim();
                    partNode.Title = name;
                    partNode.Name = name;
                }
            }

            else if (tempLine.ToLower().StartsWith("title =") || tempLine.ToLower().StartsWith("title="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH, file));

                else
                {
                    string name = nameValuePair[1].Trim();
                    partNode.Title = name;
                }
            }

            else if (tempLine.ToLower().StartsWith("category =") || tempLine.ToLower().StartsWith("category="))
            {
                string[] nameValuePair = tempLine.Split('=');
                if (nameValuePair.Length != 2)
                    Messenger.AddError(string.Format(Messages.MSG_ERROR_DURING_PART_READING_0_NAME_TITLE_MISSMATCH, file));

                else
                {
                    int categoryIndex = -1;
                    string category = nameValuePair[1].Trim();
                    if (int.TryParse(category, out categoryIndex))
                        category = TranslateCategoryIndex(categoryIndex);
                    partNode.Category = category;
                }
            }
        }

        /// <summary>
        /// Translate the category number to a category string.
        /// </summary>
        /// <returns>The translated string of the category index.</returns>
        private static string TranslateCategoryIndex(int categoryIndex)
        {
            switch (categoryIndex)
            {
                case 0:
                    return PROPULSION;
                case 1:
                    return CONTROL;
                case 2:
                    return STRUCTURAL;
                case 3:
                    return AERO;
                case 4:
                    return UTILITY;
                case 5:
                    return SCIENCE;
                case 6:
                    return PODS;
            }

            return string.Empty;
        }

        #endregion
        
        #region RemovePart

        /// <summary>
        /// Removes the selected part from KSP and unchecks it in the mod selection.
        /// </summary>
        public static void RemoveSelectedPart()
        {
            RemovePart(View.SelectedPart);
        }

        /// <summary>
        /// Removes the part from KSP and unchecks it in the mod selection.
        /// </summary>
        /// <param name="partNode">The part node to remove.</param>
        public static void RemovePart(PartNode partNode)
        {
            if (partNode == null)
                return;

            string partPath = Path.GetDirectoryName(KSPPathHelper.GetAbsolutePath(partNode.FilePath));
            ModNode node = ModSelectionTreeModel.SearchNodeByDestination(partNode.FilePath, ModSelectionController.Model);

            DialogResult dlgResult = DialogResult.Cancel;
            if (node == null)
                dlgResult = MessageBox.Show(View.ParentForm, Messages.MSG_PART_NOT_FROM_MOD_DELETE_WARNING, string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (partNode.Nodes != null && partNode.Nodes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Messages.MSG_PART_USED_DELETE_WARNING);
                foreach (var tempNode in partNode.Nodes)
                    sb.AppendFormat("- {0}{1}", tempNode.Text, Environment.NewLine);
                sb.AppendLine();
                sb.AppendLine(Messages.MSG_DELETE_ANYWAY);
                dlgResult = MessageBox.Show(View.ParentForm, sb.ToString(), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if ((node != null || dlgResult == DialogResult.Yes) && Directory.Exists(partPath))
            {
                Messenger.AddInfo(string.Format(Messages.MSG_DIR_0_OF_PART_1_DELETED, partPath, node.Name));
                Directory.Delete(partPath, true);

                if (node != null)
                {
                    if (partNode.Nodes != null)
                    {
                        foreach (var n in partNode.Nodes)
                        {
                            var craft = n.Tag as CraftNode;
                            if (craft == null)
                                continue;

                            craft.RemovePartRelation(partNode);
                        }
                    }

                    node = node.Parent as ModNode;
                    node.SetChecked(false);
                    node.IsInstalled = false;
                    node.NodeType = NodeType.UnknownFolder;
                    Messenger.AddInfo(string.Format(Messages.MSG_MODNODE_0_UNCHECKED, node.Name));
                    foreach (ModNode child in node.Nodes)
                    {
                        child.SetChecked(false);
                        child.IsInstalled = false;
                        child.NodeType = child.IsFile ? NodeType.UnknownFile : NodeType.UnknownFolder;
                        Messenger.AddInfo(string.Format(Messages.MSG_MODNODE_0_UNCHECKED, child.Name));
                    }
                }

                model.Nodes.Remove(partNode);
                allNodes.Remove(partNode);
            }
        }
        
        #endregion
        
        #region Edit Part

        /// <summary>
        /// Opens the PartEditor with the currently selected part.
        /// </summary>
        public static void EditSelectedPart()
        {
            EditPart(View.SelectedPart);
        }

        /// <summary>
        /// Opens the Part Editor with the passed PartNode. 
        /// </summary>
        /// <param name="partNode">The part node to edit.</param>
        public static void EditPart(PartNode partNode)
        {
            frmPartEditor dlg = new frmPartEditor();
            dlg.Title = partNode.Title;
            dlg.PartName = partNode.Name;
            dlg.Category = partNode.Category;
            dlg.KnownNames = (from PartNode part in allNodes select part.Name).ToList();
            if (dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
            {
                string fullPath = KSPPathHelper.GetAbsolutePath(partNode.FilePath);
                if (File.Exists(fullPath))
                {
                    string allText = File.ReadAllText(fullPath);
                    if (partNode.Name != dlg.NewName)
                    {
                        if (!ChangeParameter(ref allText, partNode.Name, NAME, partNode.Name, dlg.NewName))
                            return;

                        Messenger.AddInfo(string.Format(Messages.MSG_NAME_OF_PART_0_CHANGED_1, partNode.Name, dlg.NewName));
                        partNode.Name = dlg.NewName;
                    }
                    if (partNode.Title != dlg.NewTitle)
                    {
                        if (!ChangeParameter(ref allText, partNode.Name, TITLE, partNode.Title, dlg.NewTitle))
                            return;

                        Messenger.AddInfo(string.Format(Messages.MSG_TITLE_OF_PART_0_CHANGED_FROM_1_TO_2, partNode.Name, partNode.Title, dlg.NewTitle));
                        partNode.Title = dlg.NewTitle;
                    }
                    if (partNode.Category != dlg.NewCategory)
                    {
                        if (!ChangeParameter(ref allText, partNode.Name, CATEGORY, partNode.Category, dlg.NewCategory))
                            return;

                        Messenger.AddInfo(string.Format(Messages.MSG_CATEGORY_OF_PART_0_CHANGED_FROM_1_TO_2, partNode.Name, partNode.Category, dlg.NewCategory));
                        partNode.Category = dlg.NewCategory;

                        foreach (var node in partNode.Nodes)
                        {
                            if (node.Text.StartsWith("Category = "))
                            {
                                node.Text = "Category = " + partNode.Category;
                                break;
                            }
                        }
                    }
                    File.WriteAllText(fullPath, allText);
                }
            }
        }

        /// <summary>
        /// Changes the oldValue of the passed parameter to the passed newValue.
        /// Only for the named part.
        /// </summary>
        /// <param name="text">The text to search and replace in.</param>
        /// <param name="partName">The name of the part to change a parameter from.</param>
        /// <param name="parameterName">The parameter to change the value of.</param>
        /// <param name="oldValue">The old value of the parameter.</param>
        /// <param name="newValue">The new value for the parameter.</param>
        /// <returns>True if the text was changed.</returns>
        private static bool ChangeParameter(ref string text, string partName, string parameterName, string oldValue, string newValue)
        {
            var martch = Regex.Match(text, PARAMETER_REGEX.Replace(NAMEPARAMETER, NAME).Replace(VALUEPARAMETER, partName));
            if (martch.Success)
            {
                int index = CfgFileHelper.GetIndexOfParameter(text, NAME, partName, 0, false);
                if (index < 0) 
                    return false;

                index = CfgFileHelper.GetIndexOfParameter(text, parameterName, oldValue, index);
                if (index < 0)
                    return false;

                text = text.Substring(0, index) + newValue + text.Substring(index + oldValue.Length);

                return true;
            }

            return false;
        }
        
        #endregion

        /// <summary>
        /// Fills the TreeView dependent on the filter settings.
        /// </summary>
        private static void FillTreeView(List<PartNode> nodes)
        {
            if (nodes == null || model == null || View == null || filling)
                return;

            filling = true;
            View.InvokeIfRequired(() => model.Nodes.Clear());

            //// TODO: Sort by mod and name
            ////allNodes.Nodes.Sort((p1, p2) => p1.Title.CompareTo(p2.Title));

            int count = 0;
            string catFilter = View.SelectedCategoryFilter;
            string modFilter = View.SelectedModFilter;
            foreach (PartNode node in nodes)
                if ((catFilter == All || node.Category.Equals(catFilter, StringComparison.CurrentCultureIgnoreCase)) &&
                    (modFilter == All || node.Mod.Equals(modFilter, StringComparison.CurrentCultureIgnoreCase)))
                {
                    View.InvokeIfRequired(() => model.Nodes.Add(node));
                    ++count;
                }

            View.ModFilter = allModFilter.ToArray();
            View.SelectedModFilter = modFilter;
            View.PartCountText = string.Format(Messages.MSG_PARTS_COUNT_TEXT, model.Nodes.Count, allNodes.Count);

            filling = false;
        }
    }
}
