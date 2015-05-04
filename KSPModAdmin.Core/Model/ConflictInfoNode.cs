using System.Collections.Generic;
using System.Linq;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    public class ConflictInfoNode : Node
    {
        private const string LINE = "----------------------------------------------------------------------------------------------------------------------------------";
        private List<ModNode> conflictingNodes = null;

        public ModNode ConflictingNode { get; private set; }

        public string FileName { get; private set; }
        public string Destination { get; private set; }
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
        internal bool _Checked
        {
            get { return mChecked; }
            set
            {
                mChecked = value;
            }
        }
        public string ModName { get; private set; }
        public string TreePath { get; private set; }
        public bool HasParent
        {
            get { return (Parent as ConflictInfoNode) != null; }
        }


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

        private ConflictInfoNode(ModNode conflictingNode)
        {
            FileName = LINE;
            Destination = LINE;

            ConflictingNode = conflictingNode;
            ModName = ConflictingNode.ZipRoot.Name;
            TreePath = ConflictingNode.GetFullTreePath();
        }


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
