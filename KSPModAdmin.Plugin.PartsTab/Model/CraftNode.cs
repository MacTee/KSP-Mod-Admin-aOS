using System.Collections.Generic;
using System.Linq;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Model
{
    /// <summary>
    /// The TreeNode for the CraftNode TreeViewAdv of the UcPartsTabView.
    /// </summary>
    public class CraftNode : Node
    {
        private List<string> modList = new List<string>();

        #region Properties

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
        /// The related PartNode to this part.
        /// </summary>
        public PartNode RelatedPart { get; set; }

        /// <summary>
        /// Gets the flag that determines if this Part is valid (has a RelatedPart).
        /// </summary>
        public bool ValidPart { get { return (RelatedPart != null); } }

        /// <summary>
        /// Gets the flag that determines if this Part or one of its childs is invalid.
        /// </summary>
        public bool IsInvalidOrHasInvalidChilds
        {
            get
            {
                if (Parent as CraftNode == null)
                {
                    foreach (CraftNode child in Nodes)
                    {
                        if (child.IsInvalidOrHasInvalidChilds)
                            return true;
                    }

                    return false;
                }

                if (!ValidPart)
                    return true;

                else
                {
                    foreach (CraftNode child in Nodes)
                    {
                        if (child.IsInvalidOrHasInvalidChilds)
                            return true;
                    }
                }

                return false;
            }
        }

        #endregion

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

        /// <summary>
        /// Checks of this craft contains the part with the passed name.
        /// </summary>
        /// <param name="partName">The part name to look for.</param>
        /// <returns>True if the craft uses the part, otherwise false.</returns>
        public bool ContainsPart(string partName)
        {
            return GetPart(partName) != null;
        }

        /// <summary>
        /// Gets the part with the passed name, if available otherwise null.
        /// </summary>
        /// <param name="partName">Name of the part to get.</param>
        /// <returns>the part with the passed name, if available otherwise null.</returns>
        public CraftNode GetPart(string partName)
        {
            return Nodes.Cast<CraftNode>().FirstOrDefault(part => part.FilePath == partName);
        }

        /// <summary>
        /// Adds a mod to the mod list of this craft.
        /// </summary>
        /// <param name="modName">The mod name to add.</param>
        public void AddMod(string modName)
        {
            if (!modList.Contains(modName))
                modList.Add(modName);

            UpdateMods();
        }

        /// <summary>
        /// Removes a mod to the mod list of this craft.
        /// </summary>
        /// <param name="modName">The mod name to remove.</param>
        public void RemoveMod(string modName)
        {
            if (modList.Contains(modName))
                modList.Remove(modName);

            UpdateMods();
        }

        /// <summary>
        /// Removes the relation part.
        /// </summary>
        /// <param name="partNode">The part to remove.</param>
        public void RemovePartRelation(PartNode partNode)
        {
            foreach (CraftNode part in Nodes)
            {
                if (part.RelatedPart != null && part.RelatedPart.Title == partNode.Title)
                {
                    part.Text = part.FilePath; // FilePath is here the name of the part.
                    part.RelatedPart = null;
                }
            }
        }

        /// <summary>
        /// Updates the Mods Property.
        /// </summary>
        private void UpdateMods()
        {
            Mods = string.Empty;
            foreach (string mod in modList)
                Mods += mod + ", ";

            if (Mods.Length > 2)
                Mods = Mods.Substring(0, Mods.Length - 2);
        }
    }
}
