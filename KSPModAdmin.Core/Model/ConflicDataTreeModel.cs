using System;
using System.Collections.Generic;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Model for frmConflictSolver TreeViewAdv.
    /// </summary>
    public class ConflicDataTreeModel : TreeModel
    {
        /// <summary>
        /// Event fires before a Node.Checked will be changed.
        /// </summary>
        public static event EventHandler<BeforeCheckedChangeEventArgs> BeforeCheckedChange = null;

        /// <summary>
        /// Event fires after a Node.Checked has been changed.
        /// </summary>
        public static event AfterCheckedChangeHandler AfterCheckedChange = null;


        /// <summary>
        /// Adds a ConflictInfoNode range to the Model.
        /// </summary>
        /// <param name="nodes">The nodes to add.</param>
        /// <returns>A list of the added nodes.</returns>
        public List<ConflictInfoNode> AddRange(List<ConflictInfoNode> nodes)
        {
            List<ConflictInfoNode> addedNodes = new List<ConflictInfoNode>();
            foreach (var node in nodes)
            {
                Nodes.Add(node);
                addedNodes.Add(node);
            }

            return addedNodes;
        }


        /// <summary>
        /// Invokes the BeforeCheckedChange event.
        /// </summary>
        /// <param name="invokingModNode">The invoking ModNode that will be passed as sender.</param>
        /// <param name="newCheckedState">The new checked state that should be applied.</param>
        /// <returns>True if continue with the change.</returns>
        internal static BeforeCheckedChangeEventArgs InvokeBeforeCheckedChange(ConflictInfoNode invokingModNode, bool newCheckedState)
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