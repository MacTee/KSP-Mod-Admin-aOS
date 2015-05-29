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

        public const string All = "All";
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
                RemovePart();
                return true;
            });
            View.AddActionKey(VirtualKey.VK_BACK, (x) =>
            {
                RemovePart();
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

        public static void RefreshPartsTab()
        {
            ScanDir();
            //EventDistributor.InvokeAsyncTaskStarted(Instance);
            //View.ShowProcessingIcon = true;
            //model.Nodes.Clear();
            //AsyncTask<bool>.DoWork(
            //    () =>
            //    {
            //        System.Threading.Thread.Sleep(3000);
            //        return true;
            //    },
            //    (b, ex) =>
            //    {
            //        EventDistributor.InvokeAsyncTaskDone(Instance);
            //        View.ShowProcessingIcon = false;

            //        var node = new PartNode() { Name = "sensorAtmosphere", Category = "Science", Mod = "Squad", Title = "Atmospheric Fluid Spectro-Variometer" };
            //        node.Nodes.Add(new PartNode() { Name = "", Category = "", Mod = "", Title = "Ion-Powered Space Probe" });
            //        node.Nodes.Add(new PartNode() { Name = "", Category = "", Mod = "", Title = "Rover + Skycrane" });
            //        model.Nodes.Add(node);
            //        model.Nodes.Add(new PartNode() { Name = "GooExperiment", Category = "Science", Mod = "Squad", Title = "Mystery Goo™ Containment Unit" });
            //    });
        }

        public static void RemovePart()
        {
        }

        public static void EditPart()
        {
        }

        public static void ChangeCategory()
        {
        }

        public static void SelectionChanged()
        {
            PartNode selNode = View.SelectedPart;
        }

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
                    
                    //if (ScanComplete != null)
                    //    ScanComplete(GetListOfAllParts());
                });
        }

        /// <summary>
        /// Adds the passe part to the internal part list.
        /// </summary>
        /// <param name="file">fullpath to the part file.</param>
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
                    partNode = GetNewPartNode(file);
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
                        partNode = GetNewPartNode(file);
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
        /// <returns></returns>
        private static PartNode GetNewPartNode(string file)
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

        /////// <summary>
        /////// Removes the part from KSP and unchecks it in the mod selection.
        /////// </summary>
        /////// <param name="partNode">The part node to remove.</param>
        ////private void RemovePart(PartNode partNode)
        ////{
        ////    string partFolder = GetPartFolder(partNode);
        ////    string partPath = KSPPathHelper.GetRelativePath(Path.GetDirectoryName(partNode.FilePath));
        ////    ModNode node = ModSelectionController.SearchNode(partFolder);

        ////    DialogResult dlgResult = DialogResult.Cancel;
        ////    if (node == null)
        ////        dlgResult = MessageBox.Show(this, "The part you are trying to delete is not from a mod.\n\rDo you want to delete the part permanetly?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        ////    if (partNode.Nodes != null && partNode.Nodes.Count > 0)
        ////    {
        ////        StringBuilder sb = new StringBuilder();
        ////        sb.AppendLine("The part you are trying to delete is used by the following craft(s):");
        ////        foreach (var tempNode in partNode.Nodes)
        ////            sb.AppendFormat("- {0}{1}", tempNode.Text, Environment.NewLine);
        ////        sb.AppendLine();
        ////        sb.AppendLine("Delete it anyway?");
        ////        dlgResult = MessageBox.Show(this, sb.ToString(), "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        ////    }

        ////    if ((node != null || dlgResult == DialogResult.Yes) && Directory.Exists(partPath))
        ////    {
        ////        Directory.Delete(partPath, true);

        ////        if (node != null)
        ////        {
        ////            if (partNode.Nodes != null)
        ////            {
        ////                foreach (var n in partNode.Nodes)
        ////                    ((TreeNodeCraft)n.Tag).RemovePartRelation(partNode);
        ////            }

        ////            node.Checked = false;
        ////            node.NodeType = NodeType.UnknownFolder;
        ////            foreach (ModNode child in node.Nodes)
        ////            {
        ////                child.Checked = false;
        ////                child.NodeType = child.IsFile ? NodeType.UnknownFile : NodeType.UnknownFolder;
        ////            }
        ////        }

        ////        model.Nodes.Remove(partNode);
        ////        allNodes.Remove(partNode);
        ////    }
        ////}

        /////// <summary>
        /////// Returns the part folder where the part.cfg lies.
        /////// </summary>
        /////// <param name="partNode">The part to get the folder from.</param>
        /////// <returns>The part folder where the part.cfg lies.</returns>
        ////private string GetPartFolder(PartNode partNode)
        ////{
        ////    string path = partNode.FilePath.Substring(0, partNode.FilePath.LastIndexOf(Path.DirectorySeparatorChar));
        ////    path = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);

        ////    return path;
        ////}
        
        #endregion
        
        #region RenameCraft

        /////// <summary>
        /////// Asks the user for a new name for the part and renames it. 
        /////// </summary>
        /////// <param name="partNode">The part node to rename.</param>
        ////private void RenameCraft(PartNode partNode)
        ////{
        ////    frmNameSelection dlg = new frmNameSelection();
        ////    dlg.Description = "Please choose a new name (ATTANTION: This may corrupting crafts!).";
        ////    dlg.NewName = partNode.Name;
        ////    dlg.KnownNames = GetListOfPartNames();
        ////    if (dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
        ////    {
        ////        string fullPath = KSPPathHelper.GetRelativePath(partNode.FilePath);
        ////        if (File.Exists(fullPath))
        ////        {
        ////            string allText = File.ReadAllText(fullPath);
        ////            string newText = allText.Replace("name = " + partNode.Name, "name = " + dlg.NewName);
        ////            File.WriteAllText(fullPath, newText);
        ////            partNode.Name = dlg.NewName;
        ////            partNode.Text = partNode.ToString();
        ////        }
        ////    }
        ////}

        /////// <summary>
        /////// Returns a list of all part names.
        /////// </summary>
        /////// <returns>A list of all part names.</returns>
        ////private List<string> GetListOfPartNames()
        ////{
        ////    return (from PartNode part in allNodes select part.Name).ToList();
        ////}
        
        #endregion

        #region ChangeCategory

        /////// <summary>
        /////// Changes the category of the part.
        /////// </summary>
        /////// <param name="partNode">The node of the part to change the category from.</param>
        /////// <param name="newCategory">The new category to set the partNode to.</param>
        ////private void ChangeCategory(PartNode partNode, string newCategory = "")
        ////{
        ////    frmPartCategorySelection dlg = new frmPartCategorySelection();
        ////    dlg.Category = partNode.Category;
        ////    if (newCategory != string.Empty || dlg.ShowDialog(View.ParentForm) == DialogResult.OK)
        ////    {
        ////        string category = newCategory;
        ////        if (newCategory == string.Empty)
        ////            category = dlg.Category;
        ////        string fullPath = KSPPathHelper.GetRelativePath(partNode.FilePath);
        ////        if (File.Exists(fullPath))
        ////        {
        ////            string allText = File.ReadAllText(fullPath);
        ////            string newText = allText.Replace("category = " + partNode.Category, "category = " + category);
        ////            File.WriteAllText(fullPath, newText);
        ////            partNode.Category = category;

        ////            foreach (var node in partNode.Nodes)
        ////            {
        ////                if (node.Text.StartsWith("Category = "))
        ////                {
        ////                    node.Text = "Category = " + partNode.Category;
        ////                    break;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    View.InvalidateView();
        ////}
        
        #endregion

        /// <summary>
        /// Fills the TreView dependent on the filter settings.
        /// </summary>
        private static void FillTreeView(List<PartNode> nodes)
        {
            if (nodes == null || model == null || View == null || filling)
                return;

            filling = true;
            View.InvokeIfRequired(() => model.Nodes.Clear());

            // TODO: Sort by mod and name
            //allNodes.Nodes.Sort((p1, p2) => p1.Title.CompareTo(p2.Title));

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
