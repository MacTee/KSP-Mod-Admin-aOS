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

        /// <summary>
        /// Adds a related craft to this part.
        /// </summary>
        /// <param name="craft">The related craft to add.</param>
        public void AddRelatedCraft(CraftNode craft)
        {
            if (!ContainsCraft(craft.Name))
            {
                PartNode cNode = new PartNode() { Title = craft.Name, FilePath = craft.FilePath };
                cNode.Tag = craft;
                Nodes.Add(cNode);
            }
        }

        /// <summary>
        /// Checks of this part has a related craft with the passed name.
        /// </summary>
        /// <param name="craftName">The name to look for.</param>
        /// <returns>True if a craft with the passed name was found.</returns>
        public bool ContainsCraft(string craftName)
        {
            foreach (PartNode craft in Nodes)
            {
                if (craft.Title == craftName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Removes a craft relation.
        /// </summary>
        /// <param name="craftNode">The CraftNode to remove.</param>
        public void RemoveCraft(CraftNode craftNode)
        {
            PartNode temp = null;
            foreach (PartNode craft in Nodes)
            {
                if (craft.Tag != null && ((CraftNode)craft.Tag).FilePath == craftNode.FilePath)
                {
                    temp = craft;
                    break;
                }
            }

            if (temp != null)
                Nodes.Remove(temp);
        }
    }
}
