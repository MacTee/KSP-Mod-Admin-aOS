using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Model
{
    /// <summary>
    /// The TreeNode for the PartsTab TreeViewAdv of the UcPartsTabView.
    /// </summary>
    public class PartNode : Node
    {
        /// <summary>
        /// Name of the part.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Title of the part.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Relative path to the file on HD.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Category of the part.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// The mod which introduces this part..
        /// </summary>
        public string Mod { get; set; }


        /// <summary>
        /// Creates a instance of a PartNode.
        /// </summary>
        public PartNode()
        {
            Name = string.Empty;
            Title = string.Empty;
            FilePath = string.Empty;
            Category = string.Empty;
            Mod = string.Empty;
        }
    }
}
