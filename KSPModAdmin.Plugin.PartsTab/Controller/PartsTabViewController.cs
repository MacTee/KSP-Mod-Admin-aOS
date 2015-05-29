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

    /// <summary>
    /// Controller class for the Translation view.
    /// </summary>
    public class PartsTabViewController
    {
        #region Members

        private static PartsTabViewController instance = null;
        private static PartsTreeModel model = new PartsTreeModel();

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


        /// <summary>
        /// Scans the KSP install directory and sub directories for *.craft files.
        /// </summary>
        private static void ScanDir()
        {
            //cbModFilter.Items.Clear();
            //cbModFilter.Items.Add("All");
            //cbModFilter.Items.Add("Squad");
            //cbModFilter.SelectedIndex = 0;

            model.Nodes.Clear();

            View.ShowProcessingIcon = true;
            EventDistributor.InvokeAsyncTaskStarted(Instance);
            AsyncTask<List<PartNode>>.DoWork(() =>
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

                    return nodes;
                },
                (result, ex) =>
                {
                    EventDistributor.InvokeAsyncTaskDone(Instance);

                    if (ex != null)
                        MessageBox.Show("Error during part reading! \"" + ex.Message + "\"");
                    else
                        FillTreeView(result);

                    //tvParts.FocusedNode = null;

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

                //View.InvokeIfRequired(() => { if (!cbModFilter.Items.Contains(mod)) cbModFilter.Items.Add(mod); });
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
                    //partNode.Text = string.Format("{0} - {1}", partNode.PartName, partNode.Category);
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

        /// <summary>
        /// Fills the TreView dependent on the filter settings.
        /// </summary>
        private static void FillTreeView(List<PartNode> nodes)
        {
            model.Nodes.Clear();
            foreach (PartNode node in nodes)
                model.Nodes.Add(node);


            // Sort by mod and name
            //model.Nodes.Sort((p1, p2) => p1.PartTitle.CompareTo(p2.PartTitle));

            //int count = 0;
            //foreach (PartNode node in mPartNodes)
            //    if ((cbCategoryFilter.SelectedIndex == 0 || node.Category.ToLower() == cbCategoryFilter.SelectedItem.ToString().ToLower()) &&
            //        (cbModFilter.SelectedIndex == 0 || node.Mod.ToLower() == cbModFilter.SelectedItem.ToString().ToLower()))
            //    {
            //        InvokeIfRequired(() => tvParts.Nodes.Add(node));
            //        ++count;
            //    }

            //lblCount.Text = string.Format("{0} ({1}) Parts", count, mPartNodes.Count);
        }
    }
}
