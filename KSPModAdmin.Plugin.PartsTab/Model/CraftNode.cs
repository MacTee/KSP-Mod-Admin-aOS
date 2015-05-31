using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Model
{
    /// <summary>
    /// The TreeNode for the CraftNode TreeViewAdv of the UcPartsTabView.
    /// </summary>
    public class CraftNode : Node
    {
        /// <summary>
        /// Name of the craft.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the craft.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Relative path to the file on HD.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Folder of the craft.
        /// </summary>
        public string Folder { get; set; }

        /// <summary>
        /// Version of the craft.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The mods which are used by this craft..
        /// </summary>
        public string Mods { get; set; }


        /// <summary>
        /// Creates a instance of a CraftNode.
        /// </summary>
        public CraftNode()
        {
            Name = string.Empty;
            Type = string.Empty;
            FilePath = string.Empty;
            Folder = string.Empty;
            Version = string.Empty;
            Mods = string.Empty;
        }
    }
}
