using System.Drawing;
using System.Linq;
using KSPMODAdmin.Core.Utils.Ckan;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Plugin.ModBrowserTab.Properties;

namespace KSPModAdmin.Plugin.ModBrowserTab.Model
{
    /// <summary>
    /// Enumeration of possible CkanNode types.
    /// </summary>
    public enum CkanNodeType
    {
        Unknown,
        Mod,
        ModInfo
    }

    /// <summary>
    /// Model class for the CkanTreeModel.
    /// </summary>
    public class CkanNode : Node
    {
        private CkanMod mod;
        private CkanModInfo modInfo;
        private CkanNodeType type;

        /// <summary>
        /// Name of the node (display text).
        /// </summary>
        public string Name { get { return GetName(); } }

        /// <summary>
        /// Version of the mod.
        /// </summary>
        public string Version { get { return GetVersion(); } }

        /// <summary>
        /// Author of the mod.
        /// </summary>
        public string Author { get { return GetAuthor(); } }

        /// <summary>
        /// Description of the mod.
        /// </summary>
        public string Description { get { return GetDescription(); } }

        /// <summary>
        /// Flag to determine if this mod should be installed or not.
        /// </summary>
        public bool Checked { get; set; }

        /// <summary>
        /// Flag to determine if this mod is already added to the ModSelection.
        /// </summary>
        public bool Added { get; set; }

        /// <summary>
        /// Flag to determine if a child of this mod is added.
        /// </summary>
        public bool ChildAdded
        {
            get { return Nodes.Cast<CkanNode>().Any(c => c.Added); }
        }

        /// <summary>
        /// Gets the icon that should be used in the TreeView.
        /// </summary>
        public Image Icon
        {
            get
            {
                if (Added)
                    return (modInfo != null) ? Resources.component_add : Resources.folder_add;
                else
                    return (modInfo != null) ? Resources.component : Resources.folder;
            }
        }


        /// <summary>
        /// Creates a empty instance of the class CkanNode.
        /// </summary>
        public CkanNode()
        {
            type = CkanNodeType.Unknown;
        }

        /// <summary>
        /// Creates instance of the class CkanNode as a CkanMod representation.
        /// </summary>
        public CkanNode(CkanMod mod)
        {
            this.mod = mod;
            this.type = CkanNodeType.Mod;

            foreach (var version in mod.ModInfos)
                Nodes.Add(new CkanNode(version));
        }

        /// <summary>
        /// Creates instance of the class CkanNode as a CkanModInfo representation.
        /// </summary>
        public CkanNode(CkanModInfo version)
        {
            this.modInfo = version;
            this.type = CkanNodeType.ModInfo;
        }


        private string GetName()
        {
            switch (type)
            {
                case CkanNodeType.Mod:
                    return mod != null ? mod.Name : string.Empty;
                case CkanNodeType.ModInfo:
                    return modInfo != null ? modInfo.name : string.Empty;
            }

            return string.Empty;
        }

        private string GetVersion()
        {
            switch (type)
            {
                case CkanNodeType.Mod:
                    return string.Empty;
                case CkanNodeType.ModInfo:
                    return modInfo != null ? modInfo.version : string.Empty;
            }

            return string.Empty;
        }

        private string GetAuthor()
        {
            switch (type)
            {
                case CkanNodeType.Mod:
                    return string.Empty;
                case CkanNodeType.ModInfo:
                    return this.modInfo == null ? string.Empty : string.Join(", ", this.modInfo.author);
            }

            return string.Empty;
        }

        private string GetDescription()
        {
            switch (type)
            {
                case CkanNodeType.Mod:
                    return string.Empty;
                case CkanNodeType.ModInfo:
                    return modInfo != null ? modInfo.@abstract : string.Empty;
            }

            return string.Empty;
        }
    }
}
