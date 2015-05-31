using System.Collections.Generic;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Model
{
    /// <summary>
    /// Model for UcPartsTabView TreeViewAdv.
    /// </summary>
    public class PartsTreeModel : TreeModel
    {
        /// <summary>
        /// Adds a BackupDataNode range to the Model.
        /// </summary>
        /// <param name="nodes">The nodes to add.</param>
        /// <returns>A list of the added nodes.</returns>
        public List<PartNode> AddRange(List<PartNode> nodes)
        {
            List<PartNode> addedNodes = new List<PartNode>();
            foreach (var node in nodes)
            {
                Nodes.Add(node);
                addedNodes.Add(node);
            }

            return addedNodes;
        }
    }
}
