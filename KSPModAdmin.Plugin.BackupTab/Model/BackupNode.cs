using System;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.BackupTab.Model
{
    /// <summary>
    /// The TreeNode for the backup TreeViewAdv of the UcBackupView.
    /// </summary>
    public class BackupNode : Node
    {
        /// <summary>
        /// The displayed text for the Node.
        /// </summary>
        public string Name
        {
            get { return Text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentNullException();

                Text = value;
            }
        }

        /// <summary>
        /// The identifier of the Node.
        /// </summary>
        public string Key { get { return Tag as string; } set { Tag = value; } }

        /// <summary>
        /// The note of the Node.
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// Creates a instance of the NoteNode.
        /// </summary>
        public BackupNode(string key, string name, string note)
        {
            Key = key;
            Name = name;
            Note = note;
        }
    }
}
