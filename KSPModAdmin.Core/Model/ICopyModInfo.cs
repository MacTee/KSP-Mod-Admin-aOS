using System.Collections.Generic;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Interface for ModNodeHandler.TryCopyDestToMatchingNodes.
    /// </summary>
    public interface ICopyModInfo
    {
        /// <summary>
        /// Gets flat list of all file nodes this tree containing.
        /// </summary>
        /// <returns>A flat list of all file nodes this tree containing.</returns>
        List<ICopyModInfo> GetAllFileNodesAsICopyModInfo();

        /// <summary>
        /// Gets the full path this node within the tree.
        /// </summary>
        /// <returns>The full path this node within the tree.</returns>
        string GetFullTreePath();

        /// <summary>
        /// Gets the parent node.
        /// </summary>
        /// <returns>The parent node.</returns>
        ICopyModInfo GetParent();

        /// <summary>
        /// Gets the root node of this node (top most parent).
        /// </summary>
        /// <returns>The root node of this node (top most parent).</returns>
        ICopyModInfo GetRoot();

        /// <summary>
        /// Gets the destination for this file.
        /// </summary>
        string Destination { get; set; }

        /// <summary>
        /// Gets the checked state of the mod.
        /// </summary>
        bool Checked { get; set; }

        /// <summary>
        /// Gets the flag if one of the childes is checked.
        /// </summary>
        bool HasCheckedChilds { get; }
    }
}
