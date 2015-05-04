using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Event fires when the checked state of a ModNode is changing.
    /// </summary>
    /// <param name="sender">Invoker of the BeforeCheckedChange event.</param>
    /// <param name="newCheckedState">The new checked state to set.</param>
    /// <returns>True if the change should be continued, otherwise false.</returns>
    public delegate bool BeforeCheckedChangeHandler(object sender, bool newCheckedState);

    /// <summary>
    /// Event fires when the checked state of a ModNode has changed.
    /// </summary>
    /// <param name="sender">Invoker of the AfterCheckedChange event.</param>
    public delegate void AfterCheckedChangeHandler(object sender);


    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class BeforeCheckedChangeEventArgs : EventArgs
    {
        public Node Node { get; set; }

        public bool Cancel { get; set; }

        public bool NewValue { get; set; }

        public BeforeCheckedChangeEventArgs(Node node, bool newValue)
        {
            Node = node;
            Cancel = false;
            NewValue = newValue;
        }
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ModSelectionTreeModel : TreeModel
    {
        public static event EventHandler<BeforeCheckedChangeEventArgs> BeforeCheckedChange = null;

        public static event AfterCheckedChangeHandler AfterCheckedChange = null;


        public ModNode this[string localPath]
        {
            get
            {
                return GetModByLocalPath(localPath);
            }
        }

        public ModNode this[string productID, string vControllerName]
        {
            get
            {
                return GetModByProductID(productID, vControllerName);
            }
        }


        /// <summary>
        /// Adds a ModNode to the ModSelection (model).
        /// </summary>
        /// <param name="modNode">The ModNode to add.</param>
        /// <returns>The added ModNode.</returns>
        public ModNode AddMod(ModNode modNode)
        {
            Nodes.Add(modNode);

            return modNode;
        }

        /// <summary>
        /// Removes a ModNode from the ModSelection (model).
        /// </summary>
        /// <param name="modNode">The ModNode to add.</param>
        public void RemoveMod(ModNode modNode)
        {
            Nodes.Remove(modNode);
        }


        /// <summary>
        /// Checks if a mod with the local path already exists.
        /// </summary>
        /// <param name="localPath">The local path to search for.</param>
        /// <returns>True if the list contains a mod with the local path.</returns>
        public bool ContainsLocalPath(string localPath)
        {
            return (GetModByLocalPath(localPath) != null);
        }

        /// <summary>
        /// Checks if a mod with the productID and VersionControl already exists.
        /// </summary>
        /// <param name="productID">The local path to search for.</param>
        /// <param name="vControllerName">The VersionController to search for.</param>
        /// <returns>True if the list contains a mod with the productID and VersionControl.</returns>
        public bool ContainsProductID(string productID, string vControllerName)
        {
            return (GetModByProductID(productID, vControllerName) != null);
        }


        /// <summary>
        /// Splits the filename (at '\') and searches the tree for the Node where name and path matches.
        /// </summary>
        /// <param name="filename">Zip-File path of the node to search for.</param>
        /// <param name="startNode">Node to start the search from.</param>
        /// <param name="pathSeparator">Separator of the path in filename.</param>
        /// <returns>The matching TreeNodeMod.</returns>
        public static ModNode SearchNodeByPath(string filename, Node startNode, char pathSeparator)
        {
            string[] dirs = filename.Split(new[] { pathSeparator }, StringSplitOptions.RemoveEmptyEntries);

            ModNode result = null;
            foreach (string dir in dirs)
            {
                startNode = SearchNode(dir, startNode);

                if (startNode == null)
                    break;

                result = startNode as ModNode;
            }

            if (result != null && result.GetFullTreePath().Contains(filename))
                return result;

            return null;
        }

        /// <summary>
        /// Searches the tree for the first Node where the name matches the search text.
        /// </summary>
        /// <param name="searchText">The search string.</param>
        /// <param name="startNode">Node to start the search from.</param>
        /// <returns>The first Node where the name matches the search text.</returns>
        public static ModNode SearchNode(string searchText, Node startNode)
        {
            ModNode node = null;
            if (startNode.Text.Equals(searchText, StringComparison.CurrentCultureIgnoreCase))
            {
                node = (ModNode)startNode;
            }
            else
            {
                foreach (ModNode child in startNode.Nodes)
                {
                    node = SearchNode(searchText, child);
                    if (node != null)
                        break;
                }
            }

            return node;
        }

        /// <summary>
        /// Splits the filename (at '\') and searches the tree for the Node where name and path matches.
        /// </summary>
        /// <param name="nodePath">Tree path of the node.</param>
        /// <param name="startNode">Node to start the search from.</param>
        /// <param name="pathSeparator">Separator of the path in filename.</param>
        /// <returns>The matching TreeNodeMod.</returns>
        public static ModNode SearchNodeByPathNew(string nodePath, ModNode startNode, char pathSeparator)
        {
            string[] pathNodeNames = nodePath.Split(new[] { pathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            var node = SearchNodeByPath(pathNodeNames, startNode);
            return node != null && node.GetFullTreePath().Contains(nodePath) ? node : null;
        }

        private static ModNode SearchNodeByPath(string[] pathNodeNames, ModNode startNode, int depth = 0, bool parentMatches = false)
        {
            // Are we deeper than we should search?
            if (depth >= pathNodeNames.Length)
                return null;

            // Does the node match?
            bool thisMatches = startNode.Text.Equals(pathNodeNames[depth], StringComparison.CurrentCultureIgnoreCase);

            // if yes and we are at the lowest level of the search, we have found our match!
            if (thisMatches && depth == pathNodeNames.Length - 1)
                return startNode;

            // if parent matches to last pathNodeNames entry and this pathNodeNames entry doesn't match with child, all child childes will mismatch too!
            if (parentMatches && !thisMatches)
                return null;

            // Move to next search pathNodeName if node matches
            int newdpeth = thisMatches ? depth += 1 : depth;
            foreach (ModNode child in startNode.Nodes)
            {
                var node = SearchNodeByPath(pathNodeNames, child, newdpeth, thisMatches);
                if (node != null)
                    return node;
                
                // if childs don't match then search deeper for conplete path.
                if (thisMatches)
                {
                    node = SearchNodeByPath(pathNodeNames, child, newdpeth - 1, false);
                    if (node != null)
                        return node;
                }
            }

            return null;
        }


        /// <summary>
        /// Searches the passed node for ksp folder.
        /// </summary>
        /// <param name="node">Node to start the search from.</param>
        /// <param name="kspFolders">List of found KSP folders.</param>
        /// <param name="craftFiles">List of found craft files.</param>
        public static void GetAllKSPFolders(ModNode node, ref List<ModNode> kspFolders, ref List<ModNode> craftFiles)
        {
            if (node.IsKSPFolder && !IsChildOfAny(node, kspFolders))
                kspFolders.Add(node);

            if (node.Text.EndsWith(Constants.EXT_CRAFT, StringComparison.CurrentCultureIgnoreCase))
                craftFiles.Add(node);

            foreach (ModNode child in node.Nodes)
                GetAllKSPFolders(child, ref kspFolders, ref craftFiles);
        }

        /// <summary>
        /// Checks if the possibleChild is a child of one of the ModNodes in modNodes
        /// </summary>
        /// <param name="possibleChild">The possible child ModNode.</param>
        /// <param name="modNodes">A list of possible parent ModNodes of possibleChild.</param>
        /// <returns>True if possible child is a child of one of the ModNodes in modNodes.</returns>
        protected static bool IsChildOfAny(ModNode possibleChild, List<ModNode> modNodes)
        {
            foreach (var modNode in modNodes)
            {
                if (IsParentOf(modNode, possibleChild))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the parent ModeNode or any of its childes is a parent of child ModeNode.
        /// </summary>
        /// <param name="parent">The parent ModNode.</param>
        /// <param name="child">The child ModNode.</param>
        /// <returns>true if the parent ModeNode or any of its childes is a parent of child ModeNode.</returns>
        /// <remarks>Recursive method.</remarks>
        private static bool IsParentOf(ModNode parent, ModNode child)
        {
            if (parent == child)
                return false;

            if (parent == child.Parent)
                return true;

            foreach (ModNode newParent in parent.Nodes)
            {
                if (IsParentOf(newParent, child))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Returns the ModNode (ZipRoot) with the passed productID or null.
        /// </summary>
        /// <param name="productID">The productID to search for.</param>
        /// <param name="vControllerName">The VersionController of the mod.</param>
        /// <returns>The ModNode (ZipRoot) with the passed productID or null.</returns>
        protected ModNode GetModByProductID(string productID, string vControllerName)
        {
            return Nodes.Cast<ModNode>().FirstOrDefault(node => node.ProductID == productID && node.SiteHandlerName == vControllerName);
        }

        /// <summary>
        /// Returns the ModNode (ZipRoot) with the passed local path or null.
        /// </summary>
        /// <param name="localPath">The local path to search for.</param>
        /// <returns>The ModNode (ZipRoot) with the passed local path or null.</returns>
        protected ModNode GetModByLocalPath(string localPath)
        {
            foreach (ModNode node in Nodes)
            {
                if (!string.IsNullOrEmpty(node.Key) && node.Key.Equals(localPath, StringComparison.CurrentCultureIgnoreCase))
                    return node;
            }

            return null;
            ////return Nodes.Cast<ModNode>().FirstOrDefault(node => node.LocalPath.Equals(localPath, StringComparison.CurrentCultureIgnoreCase));
        }


        /// <summary>
        /// Returns the count of all nodes and sub node and sub sub...
        /// </summary>
        /// <param name="nodeList">The list to count the nodes from.</param>
        /// <returns>The count of nodes.</returns>
        public static int GetFullNodeCount(List<ModNode> nodeList)
        {
            if (nodeList == null || nodeList.Count == 0) return 0;

            ModNode[] nodes = new ModNode[nodeList.Count];
            for (int i = 0; i < nodeList.Count; ++i)
                nodes[i] = nodeList[i];

            return GetFullNodeCount(nodes);
        }

        /// <summary>
        /// Returns the count of all nodes and sub node and sub sub...
        /// </summary>
        /// <param name="nodeArray">The array to count the nodes from.</param>
        /// <returns>The count of nodes.</returns>
        /// <remarks>Recursive method.</remarks>
        public static int GetFullNodeCount(ModNode[] nodeArray)
        {
            if (nodeArray == null || nodeArray.Length == 0) return 0;

            int count = 0;
            foreach (ModNode node in nodeArray)
            {
                ++count;

                ModNode[] nodes = new ModNode[node.Nodes.Count];
                for (int i = 0; i < node.Nodes.Count; ++i)
                    nodes[i] = (ModNode)node.Nodes[i];

                count += GetFullNodeCount(nodes);
            }

            return count;
        }


        /// <summary>
        /// Invokes the BeforeCheckedChange event.
        /// </summary>
        /// <param name="invokingModNode">The invoking ModNode that will be passed as sender.</param>
        /// <param name="newCheckedState">The new checked state that should be applied.</param>
        /// <returns>True if continue with the change.</returns>
        internal static BeforeCheckedChangeEventArgs InvokeBeforeCheckedChange(ModNode invokingModNode, bool newCheckedState)
        {
            var args = new BeforeCheckedChangeEventArgs(invokingModNode, newCheckedState);

            if (BeforeCheckedChange != null)
                BeforeCheckedChange(invokingModNode, args);

            return args;
        }

        /// <summary>
        /// Invokes the AfterCheckedChange event.
        /// </summary>
        /// <param name="obj">The object that will be passed as sender.</param>
        internal static void InvokeAfterCheckedChange(object obj)
        {
            if (AfterCheckedChange != null)
                AfterCheckedChange(obj);
        }
    }
}