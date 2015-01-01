using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using KSPModAdmin.Core.Properties;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    public class ModNode : Node
    {
        /// <summary>
        /// Gets the child node with the given key.
        /// </summary>
        /// <param name="key">The key the child should match to.</param>
        /// <returns>The child node with the given key.</returns>
        public ModNode this[string key]
        {
            get
            {
                return GetChildByKey(key);
            }
        }


        #region Properties

        /// <summary>
        /// Type of the Node.
        /// </summary>
        public NodeType NodeType
        {
            get { return mNodeType; }
            set
            {
                mNodeType = value;
                switch (mNodeType)
                {
                    case NodeType.KSPFolder:
                    case NodeType.UnknownFolder:
                        Icon = Resources.folder;
                        break;
                    case NodeType.UnknownFolderInstalled:
                    case NodeType.KSPFolderInstalled:
                        Icon = Resources.folder_add;
                        break;
                    case NodeType.UnknownFile:
                        Icon = Resources.page;
                        break;
                    case NodeType.UnknownFileInstalled:
                        Icon = Resources.page_add;
                        break;
                }
            }
        }
        private NodeType mNodeType = NodeType.UnknownFolder;


        /// <summary>
        /// The displayed text for the Node.
        /// </summary>
        public string Name
        {
            get { return base.Text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                base.Text = value;
            }
        }

        /// <summary>
        /// The identifier of the Node.
        /// </summary>
        public string Key { get { return base.Tag as string; } set { base.Tag = value; } }


        /// <summary>
        /// Gets or sets the mod related NodeInfos.
        /// </summary>
        public ModInfo ModInfo
        {
            get
            {
                ModInfo modInfo = new ModInfo();
                modInfo.Author = Author;
                modInfo.CreationDate = CreationDate;
                modInfo.ChangeDate = ChangeDate;
                //modInfo.DownloadDate = DownloadDate;
                modInfo.Downloads = Downloads;
                modInfo.LocalPath = LocalPath;
                modInfo.Name = Name;
                modInfo.ProductID = ProductID;
                modInfo.Rating = Rating;
                modInfo.SiteHandlerName = SiteHandlerName;
                modInfo.ModURL = ModURL;
                modInfo.AvcURL = AvcURL;
                modInfo.AdditionalURL = AdditionalURL;
                modInfo.Version = Version;
                modInfo.KSPVersion = KSPVersion;
                return modInfo;
            }
            set
            {
                if (value != null)
                {
                    Author = value.Author;
                    CreationDate = value.CreationDate;
                    ChangeDate = value.ChangeDate;
                    //DownloadDate = value.DownloadDate;
                    Downloads = value.Downloads;
                    //LocalPath = value.LocalPath;
                    Key = value.LocalPath;
                    Name = value.Name;
                    ProductID = value.ProductID;
                    Rating = value.Rating;
                    SiteHandlerName = value.SiteHandlerName;
                    ModURL = value.ModURL;
                    AvcURL = value.AvcURL;
                    AdditionalURL = value.AdditionalURL;
                    Version = value.Version;
                    KSPVersion = value.KSPVersion;
                }
            }
        }

        /// <summary>
        /// Forum or ModRepository identification number.
        /// </summary>
        public string ProductID { get; set; }

        /// <summary>
        /// Version of the mod.
        /// </summary>
        public string Version { get; set; }

		/// <summary>
		/// Version of the game this mod is for
		/// </summary>
		public string KSPVersion { get; set; }

        /// <summary>
        /// The author of the mod.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// The creation date of the mod.
        /// </summary>
        public string CreationDate { get; set; }

        /// <summary>
        /// The creation date of the mod as a DateTime object..
        /// </summary>
        public DateTime CreationDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(CreationDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(CreationDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    CreationDate = string.Empty;
                else
                    CreationDate = value.ToString();
            }
        }

        /// <summary>
        /// The last change date of the mod.
        /// </summary>
        public string ChangeDate { get; set; }

        /// <summary>
        /// The last change date of the mod as a DateTime object..
        /// </summary>
        public DateTime ChangeDateAsDateTime
        {
            get
            {
                if (string.IsNullOrEmpty(ChangeDate))
                    return DateTime.MinValue;
                else
                {
                    DateTime value = DateTime.MinValue;
                    if (DateTime.TryParse(ChangeDate, out value))
                        return value;
                    else
                        return DateTime.MinValue;
                }
            }
            set
            {
                if (value == DateTime.MinValue)
                    ChangeDate = string.Empty;
                else
                    ChangeDate = value.ToString();
            }
        }

        /// <summary>
        /// The date of adding the mod to the ModSelection.
        /// </summary>
        public string AddDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the SiteHandler for this mod.
        /// </summary>
        public string SiteHandlerName 
        { 
            get { return mSiteHandlerName; } 
            set { mSiteHandlerName = (string.IsNullOrEmpty(value)) ? Messages.NONE : value; } 
        }
        private string mSiteHandlerName = Messages.NONE;

        /// <summary>
        /// Gets the SiteHandler name (Used by TreeViewAdv).
        /// </summary>
        public string SiteHandlerNameUI
        {
            get { return (ZipRoot == this) ? SiteHandlerName : string.Empty; }
        }

        /// <summary>
        /// Gets or sets the SiteHandler of the mod. Can be null!
        /// </summary>
        public ISiteHandler SiteHandler
        {
            get { return (SiteHandlerName != Messages.NONE) ? SiteHandlerManager.GetSiteHandlerByName(SiteHandlerName) : null; }
            set { SiteHandlerName = (value == null) ? Messages.NONE : value.Name; }
        }

        /// <summary>
        /// URL to the site of the mod.
        /// </summary>
        public string ModURL { get; set; }

        /// <summary>
        /// The URL to the AVC Plugin version file.
        /// </summary>
        public string AvcURL { get; set; }

        /// <summary>
        /// URL to a additional site.
        /// </summary>
        public string AdditionalURL { get; set; }

        /// <summary>
        /// The rating of the mod.
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// The download count of the mod.
        /// </summary>
        public string Downloads { get; set; }

        /// <summary>
        /// The user notes for the mod.
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// Root node of the mod.
        /// </summary>
        public ModNode ZipRoot
        {
            get
            {
                ModNode root = this;
                while (root.HasParent)
                    root = (ModNode)root.Parent;

                return root;
            }
        }

        /// <summary>
        /// Flag to determine if the mods zip archive exists.
        /// </summary>
        public bool ZipExists
        {
            get
            {
                try
                {
                    var root = ZipRoot;
                    if (root != null)
                        return File.Exists(root.Key);
                }
                catch (Exception) { }

                return false;
            }
        }

        /// <summary>
        /// Local path of the mod zip archive.
        /// </summary>
        public string LocalPath { get { return Key; } }

        /// <summary>
        /// Local path of the mod zip archive.
        /// </summary>
        public string ArchivePath { get { return ZipRoot.LocalPath; } }

        /// <summary>
        /// The install destination of the node.
        /// </summary>
        public string Destination
        {
            get
            {
                return mDestination;
            }
            set
            {
                if (mDestination != value)
                {
                    ModRegister.RemoveRegisteredModFile(this);
                    mDestination = value;

                    if (!string.IsNullOrEmpty(value) && Text.ToLower() != Constants.GAMEDATA)
                        ModRegister.RegisterModFile(this);
                }
                else
                {
                    mDestination = value;
                }
            }
        }
        private string mDestination = string.Empty;


        /// <summary>
        /// Flag to determine if the node has a destination.
        /// </summary>
        public bool HasDestination { get { return !string.IsNullOrEmpty(Destination); } }
        
        /// <summary>
        /// Flag to determine if a child of this node has a destination.
        /// </summary>
        public bool HasDestinationForChilds
        {
            get
            {
                if (HasDestination) return true;

                foreach (ModNode child in this.Nodes)
                    if (child.HasDestinationForChilds)
                        return true;

                return false;
            }
        }

        public bool HasChildesWithoutDestination
        {
            get
            {
                foreach (ModNode child in this.Nodes)
                    if (!child.HasDestination || child.HasChildesWithoutDestination)
                        return true;

                return false;
            }
        }
        
        /// <summary>
        /// Flag to determine if the destination of the node collides with other nodes.
        /// </summary>
        public bool HasCollision { get; set; }
        
        /// <summary>
        /// Flag to determine if the destination of a child of this node collides with other nodes.
        /// </summary>
        public bool HasChildCollision
        {
            get
            {
                foreach (ModNode node in Nodes)
                {
                    if (node.HasCollision || node.HasChildCollision)
                        return true;
                }

                return false;
            }
        }
        
        /// <summary>
        /// Flag to determine if the node is a KSP folder.
        /// </summary>
        public bool IsKSPFolder
        {
            get
            {
                return (this.NodeType == NodeType.KSPFolder ||
                        this.NodeType == NodeType.KSPFolderInstalled);
            }
        }
        
        /// <summary>
        /// Flag to determine if the node is installed.
        /// </summary>
        public bool IsInstalled { get; set; }
        public bool IsModNodeInstalled()
        {
            if (IsFile)
                return (!String.IsNullOrEmpty(Destination) && File.Exists(KSPPathHelper.GetAbsolutePath(Destination)));
            else
                return (!String.IsNullOrEmpty(Destination) && Directory.Exists(KSPPathHelper.GetAbsolutePath(Destination)));
        }
        
        /// <summary>
        /// Flag to determine if a child of this node is installed.
        /// </summary>
        public bool HasInstalledChilds
        {
            get
            {
                foreach (ModNode child in Nodes)
                    if (child.IsInstalled || child.HasInstalledChilds)
                    {
                        if (child.Text.Equals(Constants.GAMEDATA, StringComparison.CurrentCultureIgnoreCase) ||
                            child.Text.Equals(Constants.SHIPS, StringComparison.CurrentCultureIgnoreCase) ||
                            child.Text.Equals(Constants.VAB, StringComparison.CurrentCultureIgnoreCase) ||
                            child.Text.Equals(Constants.SPH, StringComparison.CurrentCultureIgnoreCase))
                        {
                            if (child.HasInstalledChilds)
                                return true;
                        }
                        else
                            return true;
                    }

                return false;
            }
        }
        
        /// <summary>
        /// Flag to determine if the node is a file.
        /// </summary>
        public bool IsFile
        {
            get
            {
                return (this.NodeType == NodeType.UnknownFile ||
                        this.NodeType == NodeType.UnknownFileInstalled);
            }
        }
        
        /// <summary>
        /// Flag to determine if the mod is outdated.
        /// </summary>
        public bool IsOutdated { get; set; }

        /// <summary>
        /// Depth level of the node within the mod tree.
        /// </summary>
        public int Depth
        {
            get
            {
                int depth = 0;
                ModNode node = Parent as ModNode;
                while (node != null)
                {
                    ++depth;
                    node = node.Parent as ModNode;
                }
                return depth;
            }
        }


        #region Checked property

        /// <summary>
        /// Flag that determines if the node is checked or not.
        /// Checked state will be propagate up and down the tree.
        /// </summary>
        public bool Checked
        {
            get { return mChecked; }
            set
            {
                if (mChecked == value)
                    return;

                var args = ModSelectionTreeModel.InvokeBeforeCheckedChange(this, value);
                if (args.Cancel)
                    return;

                mChecked = args.NewValue;

                ModSelectionTreeModel.InvokeAfterCheckedChange(this);

                CheckAllChildes(value);
                CheckParents(value);
            }
        }
        private bool mChecked = false;

        /// <summary>
        /// Gets or sets the CheckedState without fireing events.
        /// </summary>
        internal bool _Checked
        {
            get { return mChecked; }
            set
            {
                mChecked = value;

                CheckAllChildes(value);
                CheckParents(value);
            }
        }

        /// <summary>
        /// Sets the checked state without propagating the state up and down the tree.
        /// </summary>
        /// <param name="value">The value to set to the Checked property.</param>
        /// <param name="forceSet">Flag to force a check even if node or its childes don't have a destination.</param>
        public void SetChecked(bool value, bool forceSet = false)
        {
            if (Checked == value)
                return;

            if (value)
                mChecked = forceSet || HasDestination || HasDestinationForChilds;
            else
                mChecked = value;
        }

        /// <summary>
        /// Sets the value to all child, childchildes, ... 
        /// </summary>
        /// <param name="value">The value to set to the Checked property.</param>
        public void CheckAllChildes(bool value)
        {
            foreach (ModNode node in Nodes)
            {
                if (node.Checked == value)
                    continue;

                node.SetChecked(value);
                node.CheckAllChildes(value);
            }
        }

        /// <summary>
        /// Checks all patents if value is true, 
        /// otherwise parents will be unchecked if no sibling is checked.
        /// </summary>
        /// <param name="value">The value to set to the Checked property to.</param>
        public void CheckParents(bool value)
        {
            if (!HasParent)
                return;

            ModNode parent = (Parent as ModNode);
            if (parent.Checked == value)
                return;

            if (value)
            {
                parent.SetChecked(true);
                parent.CheckParents(true);
            }
            else
            {
                if (!IsSiblingChecked)
                {
                    parent.SetChecked(false);
                    parent.CheckParents(false);
                }
            }
        }

        /// <summary>
        /// True if any sibling is checked.
        /// </summary>
        public bool IsSiblingChecked
        {
            get
            {
                foreach (ModNode sibling in Siblings)
                {
                    if (sibling.Checked)
                        return true;
                }

                return false;
            }
        }

        #endregion


        /// <summary>
        /// The icon that should be displayed in the TreeView.
        /// </summary>
        public Image Icon { get { return mIcon; } set { mIcon = value; } }
        private Image mIcon = Resources.folder;


        /// <summary>
        /// True if this ModNode has a parent.
        /// </summary>
        public bool HasParent
        {
            get { return (Parent as ModNode) != null; }
        }

        /// <summary>
        /// Enumerable list of all siblings of this ModNode.
        /// </summary>
        public IEnumerable<ModNode> Siblings
        {
            get
            {
                List<ModNode> result = new List<ModNode>();
                if (Parent as ModNode != null)
                {
                    foreach (ModNode child in Parent.Nodes)
                    {
                        if (child != this)
                            result.Add(child);
                    }
                }

                return result;
            }
        }

        #endregion

        #region Constructors

        public ModNode()
        {
            SiteHandlerName = Messages.NONE;
        }

        public ModNode(ModInfo modInfo)
        {
            ModInfo = modInfo;
        }

        public ModNode(string fileName, string modName, NodeType nodeType)
        {
            Key = fileName;
            Name = modName;
            NodeType = nodeType;
            SiteHandlerName = Messages.NONE;
        }

        public ModNode(string key, string text)
        {
            Key = key;
            Name = text;
            SiteHandlerName = Messages.NONE;
        }

        #endregion


        /// <summary>
        /// Adds a ModNode as a child to this node.
        /// </summary>
        /// <param name="node">the node to add as child.</param>
        /// <returns>The added child node.</returns>
        public ModNode AddChild(ModNode node)
        {
            Nodes.Add(node);

            return node;
        }


        /// <summary>
        /// Checks of this node has a child with the given key.
        /// </summary>
        /// <param name="key">The key the child should match to.</param>
        /// <returns>True if a child with the given key was found.</returns>
        public bool ContainsChild(string key)
        {
            return GetChildByKey(key) != null;
        }

        /// <summary>
        /// Gets the child node with the given key.
        /// </summary>
        /// <param name="key">The key the child should match to.</param>
        /// <returns>The child node with the given key.</returns>
        public ModNode GetChildByKey(string key)
        {
            foreach (ModNode child in Nodes.Cast<ModNode>())
            {
                if (child.Key == key)
                    return child;
            }

            return null;
        }


        /// <summary>
        /// Builds the full path of the node to the highest parent.
        /// </summary>
        /// <returns>The full node path.</returns>
        public string GetFullTreePath()
        {
            return (((this.Parent as ModNode) != null) ? (this.Parent as ModNode).GetFullTreePath() : string.Empty) + "/" + this.Name;
        }

        /// <summary>
        /// Returns a list of TreeNodeMod that represents a file entry.
        /// </summary>
        /// <param name="fileNodes">For recursive calls! List of already found file nodes.</param>
        /// <returns>A list of TreeNodeMod that represents a file entry.</returns>
        public List<ModNode> GetAllFileNodes(List<ModNode> fileNodes = null)
        {
            if (fileNodes == null)
                fileNodes = new List<ModNode>();

            if (IsFile)
                fileNodes.Add(this);

            foreach (ModNode childNode in Nodes)
                childNode.GetAllFileNodes(fileNodes);

            return fileNodes;
        }

        /// <summary>
        /// Builds and sets the destination path to the passed node and its childes.
        /// </summary>
        /// <param name="destPath">The destination path.</param>
        /// <param name="copyContent"></param>
        public void SetDestinationPaths(string destPath, bool copyContent = false)
        {
            if (!copyContent)
            {
                Destination = (destPath != string.Empty) ? Path.Combine(destPath, Text) : string.Empty;

                destPath = (Destination != string.Empty) ? Destination : string.Empty;
            }

            SetToolTips();

            foreach (ModNode child in Nodes)
                child.SetDestinationPaths(destPath, copyContent);
        }

        /// <summary>
        /// Sets the ToolTip text of the node and all its childes.
        /// </summary>
        private void SetToolTips()
        {
            //if (Destination != string.Empty)
            //    ToolTipText = Destination.ToLower().Replace(KSPPathHelper.GetPath(KSPPaths.KSPRoot).ToLower(), "KSP install folder");
            //else
            //    ToolTipText = "<No path selected>";

            foreach (ModNode child in Nodes)
                child.SetToolTips();
        }

        /// <summary>
        /// Unchecks this and all child and childchild ... ModNodes.
        /// </summary>
        public void UncheckAll()
        {
            UncheckAllInternal(this);
        }

        /// <summary>
        /// Unchecks the modNode and all child and child child ... ModNodes.
        /// </summary>
        private void UncheckAllInternal(ModNode modNode)
        {
            modNode.SetChecked(false);

            foreach (ModNode child in modNode.Nodes)
                UncheckAllInternal(child);
        }
    }
}
