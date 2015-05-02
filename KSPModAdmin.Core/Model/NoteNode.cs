using System;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Representation of a TreeNode with a note.
    /// </summary>
    [Serializable]
    public class NoteNode : TreeNode
    {
        /// <summary>
        /// The identifier of the Node.
        /// </summary>
        public string Key { get { return base.Name; } set { base.Name = value; } }

        /// <summary>
        /// The displayed text for the Node.
        /// </summary>
        public new string Name { get { return Text; } set { Text = value; } }

        /// <summary>
        /// The note of the Node.
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// Creates a instance of the NoteNode.
        /// </summary>
        public NoteNode(string key, string name, string note)
        {
            Key = key;
            Name = name;
            Note = note;
        }
    }
}
