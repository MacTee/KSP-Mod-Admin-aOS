using System.Collections.Generic;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Model
{
    /// <summary>
    /// Model for UcPartsTabView TreeViewAdv.
    /// </summary>
    public class CraftsTreeModel : TreeModel
    {
        /// <summary>
        /// Adds a CraftDataNode range to the Model.
        /// </summary>
        /// <param name="nodes">The nodes to add.</param>
        /// <returns>A list of the added nodes.</returns>
        public List<CraftNode> AddRange(List<CraftNode> nodes)
        {
            List<CraftNode> addedNodes = new List<CraftNode>();
            foreach (var node in nodes)
            {
                Nodes.Add(node);
                addedNodes.Add(node);
            }

            return addedNodes;
        }
    }
}
