using System;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Model
{
    [Serializable]
    public class NoteNode : TreeNode
    {
        public string Key { get { return base.Name; } set { base.Name = value; } }
        public new string Name { get { return base.Text; } set { base.Text = value; } }
        public string Note { get; set; }


        public NoteNode(string key, string name, string note)
        {
            Key = key;
            Name = name;
            Note = note;
        }
    }
}
