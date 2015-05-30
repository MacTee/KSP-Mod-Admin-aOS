using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using KSPModAdmin.Core;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;
using KSPModAdmin.Plugin.PartsTab.Model;
using KSPModAdmin.Plugin.PartsTab.Views;

namespace KSPModAdmin.Plugin.PartsTab.Controller
{
    using System.Linq;
    using System.Text;

    using KSPModAdmin.Core.Model;

    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class PartsTabViewController
    {
        #region Members

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

        #endregion

        internal static void Initialize(ucPartsTabView view)
        {
            View = view;

            EventDistributor.AsyncTaskStarted += AsyncTaskStarted;
            EventDistributor.AsyncTaskDone += AsyncTaskDone;

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
                    // Get part.cfg files from GameData folder.
                    string gameDatePath = KSPPathHelper.GetPath(KSPPaths.GameData);
                    string[] files = Directory.GetFiles(gameDatePath, "*.cfg", SearchOption.AllDirectories);

                    // Get part.cfg files from additional folders.
                    string partsPath = KSPPathHelper.GetPath(KSPPaths.Parts);
                    string[] addPaths = new[] { partsPath };
                    foreach (var path in addPaths)
                    {
                        string[] files2 = Directory.GetFiles(path, "*.cfg", SearchOption.AllDirectories);
                        int oldLength = files.Length;
                        Array.Resize<string>(ref files, oldLength + files2.Length);
                        Array.Copy(files2, 0, files, oldLength, files2.Length);
                    }

                    // Create PartNodes from each file.
                    var nodes = new List<PartNode>();
                    if (files.Length > 0)
                        foreach (string file in files)
                        {
                            var newNodes = CreatePartNodes(file);
                            foreach (var newNode in newNodes)
                            {
                                if (newNode != null && !string.IsNullOrEmpty(newNode.Name) && !nodes.Contains(newNode)) 
                                    nodes.Add(newNode);
                            }
                        }
                    else
                        Messenger.AddInfo(string.Format("No part.cfg files found in \"{0}\".", gameDatePath));

                    allNodes.Clear();
                    foreach (PartNode node in nodes)
                        allNodes.Add(node);

                    return true;
                },
                (result, ex) =>
                {
                    View.ShowProcessingIcon = false;
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        MessageBox.Show("Error during part reading! \"" + ex.Message + "\"");
                    else
                        RefreshTreeView();
                    
                    ////if (ScanComplete != null)
                    ////    ScanComplete(GetListOfAllParts());
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
            {
                Messenger.AddInfo(string.Format("Error during part reading.{0}Path is empty.", Environment.NewLine));
                return result;
            }

            string[] lines = File.ReadLines(file).ToArray();
            if (lines.Length == 0)
            {
                Messenger.AddInfo(string.Format("Error during part reading \"{0}\"{1}File content empty.", file, Environment.NewLine));
                return result;
            }

            int braceCount = 0;
            bool isPartFile = false;
            bool isWithinPartDev = false;
            PartNode partNode = null;
            foreach (string line in lines)
            {
                if (line == null)
                {
                    Messenger.AddError(string.Format("Error during part reading \"{0}\"{1}Enexpected 'null' line.", file, Environment.NewLine));
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
                list.Add(node);
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
            if (file.Contains("GameData"))
            {
                string mod = file.Substring(file.IndexOf("GameData") + 9);
                mod = mod.Substring(0, mod.IndexOf("\\"));
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
                    Messenger.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

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
                    Messenger.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

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
                    Messenger.AddError(string.Format("Error during part reading \"{0}\"{1}Name / title parameter missmatch.", file, Environment.NewLine));

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
                    return "Propulsion";
                case 1:
                    return "Control";
                case 2:
                    return "Structural";
                case 3:
                    return "Aero";
                case 4:
                    return "Utility";
                case 5:
                    return "Science";
                case 6:
                    return "Pods";
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
                dlgResult = MessageBox.Show(View.ParentForm, "The part you are trying to delete is not from a mod.\n\rDo you want to delete the part permanetly?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (partNode.Nodes != null && partNode.Nodes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("The part you are trying to delete is used by the following craft(s):");
                foreach (var tempNode in partNode.Nodes)
                    sb.AppendFormat("- {0}{1}", tempNode.Text, Environment.NewLine);
                sb.AppendLine();
                sb.AppendLine("Delete it anyway?");
                dlgResult = MessageBox.Show(View.ParentForm, sb.ToString(), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }

            if ((node != null || dlgResult == DialogResult.Yes) && Directory.Exists(partPath))
            {
                Directory.Delete(partPath, true);

                if (node != null)
                {
                    if (partNode.Nodes != null)
                    {
                        //// TODO: Remove attached craft nodes.
                        ////foreach (var n in partNode.Nodes)
                        ////    ((TreeNodeCraft)n.Tag).RemovePartRelation(partNode);
                    }

                    node = node.Parent as ModNode;
                    node.SetChecked(false);
                    node.IsInstalled = false;
                    node.NodeType = NodeType.UnknownFolder;
                    foreach (ModNode child in node.Nodes)
                    {
                        child.SetChecked(false);
                        child.IsInstalled = false;
                        child.NodeType = child.IsFile ? NodeType.UnknownFile : NodeType.UnknownFolder;
                    }
                }

                model.Nodes.Remove(partNode);
                allNodes.Remove(partNode);
            }
        }
        
        #endregion
        
        #region Edit/Rename Part

        /// <summary>
        /// Opens the PartEditor with the currently selected part.
        /// </summary>
        public static void EditSelectedPart()
        {
            RenamePart(View.SelectedPart);
        }

        /// <summary>
        /// Asks the user for a new name for the part and renames it. 
        /// </summary>
        /// <param name="partNode">The part node to rename.</param>
        public static void RenamePart(PartNode partNode)
        {
            frmNameSelection dlg = new frmNameSelection();
            dlg.Description = "Please choose a new name (ATTANTION: This may corrupting crafts!).";
            dlg.NewTitle = partNode.Title;
            dlg.NewName = partNode.Name;
            dlg.KnownNames = (from PartNode part in allNodes select part.Name).ToList();
            if (dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
            {
                string fullPath = KSPPathHelper.GetAbsolutePath(partNode.FilePath);
                if (File.Exists(fullPath))
                {
                    string allText = File.ReadAllText(fullPath);
                    string newText = allText.Replace("name = " + partNode.Name, "name = " + dlg.NewName);
                    newText = newText.Replace("title = " + partNode.Title, "title = " + dlg.NewTitle);
                    File.WriteAllText(fullPath, newText);
                    partNode.Name = dlg.NewName;
                    partNode.Title = dlg.NewTitle;
                    ////partNode.Text = partNode.ToString();
                }
            }
        }
        
        #endregion

        #region ChangeCategory

        /// <summary>
        /// Opens the ChangeCategory dialog to change the category of a part.
        /// </summary>
        public static void ChangeCategoryOfSelectedPart()
        {
            ChangeCategory(View.SelectedPart);
        }

        /// <summary>
        /// Changes the category of the part.
        /// </summary>
        /// <param name="partNode">The node of the part to change the category from.</param>
        /// <param name="newCategory">The new category to set the partNode to, if != empty the dialog will be skipped.</param>
        public static void ChangeCategory(PartNode partNode, string newCategory = "")
        {
            frmPartCategorySelection dlg = new frmPartCategorySelection();
            dlg.Category = partNode.Category;
            if (newCategory != string.Empty || dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
            {
                string category = newCategory;
                if (newCategory == string.Empty)
                    category = dlg.Category;
                string fullPath = KSPPathHelper.GetAbsolutePath(partNode.FilePath);
                if (File.Exists(fullPath))
                {
                    string allText = File.ReadAllText(fullPath);
                    string newText = allText.Replace("category = " + partNode.Category, "category = " + category);
                    File.WriteAllText(fullPath, newText);
                    partNode.Category = category;

                    foreach (var node in partNode.Nodes)
                    {
                        if (node.Text.StartsWith("Category = "))
                        {
                            node.Text = "Category = " + partNode.Category;
                            break;
                        }
                    }
                }
            }
            View.InvalidateView();
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
