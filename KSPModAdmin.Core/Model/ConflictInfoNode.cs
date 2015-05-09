using System.Collections.Generic;
using System.Linq;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Node for the frmConflictSolver TreeViewAdv tree.
    /// </summary>
    public class ConflictInfoNode : Node
    {
        private const string LINE = "----------------------------------------------------------------------------------------------------------------------------------";
        private List<ModNode> conflictingNodes = null;

        /// <summary>
        /// The conflicting ModNode.
        /// </summary>
        public ModNode ConflictingNode { get; private set; }

        /// <summary>
        /// The file name of the conflicting ModNode file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The file destination of the conflicting ModNode file.
        /// </summary>
        public string Destination { get; private set; }

        /// <summary>
        /// Gets or sets the Checked state of this ConflictInfoNode.
        /// Will uncheck siblings if this node will be checked (only one selection is allowed!).
        /// Fires the BeforeCheckedChange and the AfterCheckedChange events.
        /// </summary>
        public bool Checked
        {
            get { return mChecked; }
            set
            {
                if (mChecked == value)
                    return;

                var args = ConflicDataTreeModel.InvokeBeforeCheckedChange(this, value);
                if (args.Cancel)
                    return;

                mChecked = args.NewValue;

                if (mChecked)
                    UncheckSiblings();

                ConflicDataTreeModel.InvokeAfterCheckedChange(this);
            }
        }
        private bool mChecked = false;

        /// <summary>
        /// Gets or sets the Checked state of this ConflictInfoNode.
        /// Will uncheck siblings if this node will be checked (only one selection is allowed!).
        /// DON'T fires the BeforeCheckedChange and the AfterCheckedChange events.
        /// </summary>
        internal bool _Checked
        {
            get { return mChecked; }
            set
            {
                mChecked = value;

                if (mChecked)
                    UncheckSiblings();
            }
        }

        /// <summary>
        /// Name of the mod from the conflicting ModNode.
        /// </summary>
        public string ModName { get; private set; }

        /// <summary>
        /// The version of the mod.
        /// </summary>
        public string ModVersion { get; private set; }

        /// <summary>
        /// The complete path within the tree.
        /// </summary>
        public string TreePath { get; private set; }

        /// <summary>
        /// The complete path to the mod archive.
        /// </summary>
        public string ArchivePath { get; private set; }

        /// <summary>
        /// Gets the flag if this ConflictInfoNode has a Parent.
        /// </summary>
        public bool HasParent
        {
            get { return (Parent as ConflictInfoNode) != null; }
        }


        /// <summary>
        /// Creates a instance of the ConflictInfoNode.
        /// This node will be a Parent ConflictInfoNode for the passed conflictingNodesWithSameDestination.
        /// Build the corresponding child ConflictInfoNodes from the passed conflictingNodesWithSameDestination.
        /// </summary>
        /// <param name="conflictingNodesWithSameDestination">The conflicting node files with the same destination.</param>
        public ConflictInfoNode(List<ModNode> conflictingNodesWithSameDestination)
        {
            conflictingNodes = conflictingNodesWithSameDestination;

            FileName = string.Empty;
            Destination = string.Empty;

            var node = conflictingNodes.FirstOrDefault();
            if (node != null)
                FileName = node.Name;
            if (node != null)
                Destination = node.Destination;

            foreach (var cNode in conflictingNodes)
                Nodes.Add(new ConflictInfoNode(cNode));
        }

        /// <summary>
        /// Creates a instance of the ConflictInfoNode.
        /// This node will be a child ConflictInfoNode and represents one conflicting ModNode file.
        /// </summary>
        /// <param name="conflictingNode">The conflicting ModNode file.</param>
        private ConflictInfoNode(ModNode conflictingNode)
        {
            FileName = LINE;
            Destination = LINE;

            ConflictingNode = conflictingNode;
            ModName = ConflictingNode.ZipRoot.Name;
            ModVersion = ConflictingNode.ZipRoot.Version;
            TreePath = ConflictingNode.GetFullTreePath();
            ArchivePath = ConflictingNode.ZipRoot.ArchivePath;
        }


        /// <summary>
        /// Unchecks sibling nodes if this node is checked.
        /// </summary>
        private void UncheckSiblings()
        {
            if (!HasParent)
                return;

            var siblings = Parent.Nodes.Cast<ConflictInfoNode>();
            foreach (var sibling in siblings)
            {
                if (sibling == this)
                    continue;

                sibling._Checked = false;
            }
        }
    }
}
