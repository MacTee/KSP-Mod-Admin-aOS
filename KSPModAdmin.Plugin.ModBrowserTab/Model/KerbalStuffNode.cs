using System.Drawing;
using System.Linq;
using KSPModAdmin.Core.Utils.KerbalStuff;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Plugin.ModBrowserTab.Properties;

namespace KSPModAdmin.Plugin.ModBrowserTab.Model
{

    /// <summary>
    /// Enumeration of possible CkanNode types.
    /// </summary>
    public enum KerbalStuffNodeType
    {
        Unknown,
        Mod,
        ModInfo
    }

    /// <summary>
    /// Model class for the CkanTreeModel.
    /// </summary>
    public class KerbalStuffNode : Node
    {
        private KsMod mod;
        private KsVersion modVersionInfo;
        private KerbalStuffNodeType type;

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
            get { return Nodes.Cast<KerbalStuffNode>().Any(c => c.Added); }
        }

        /// <summary>
        /// Gets the icon that should be used in the TreeView.
        /// </summary>
        public Image Icon
        {
            get
            {
                return Resources.component;
            }
        }


        /// <summary>
        /// Creates a empty instance of the class CkanNode.
        /// </summary>
        public KerbalStuffNode(KsMod mod)
        {
            this.type = KerbalStuffNodeType.Mod;
            this.mod = mod;
            this.modVersionInfo = null;

            foreach (KsVersion version in mod.versions)
                Nodes.Add(new KerbalStuffNode(mod, version));
        }

        /// <summary>
        /// Creates a empty instance of the class CkanNode.
        /// </summary>
        public KerbalStuffNode(KsMod mod, KsVersion versionInfo)
        {
            this.type = KerbalStuffNodeType.ModInfo;
            this.mod = mod;
            this.modVersionInfo = versionInfo;
        }


        private string GetName()
        {
            return mod.name;
        }

        private string GetVersion()
        {
            if (type == KerbalStuffNodeType.ModInfo)
                return modVersionInfo != null ? modVersionInfo.friendly_version : string.Empty;

            return string.Empty;
        }

        private string GetAuthor()
        {
            if (type == KerbalStuffNodeType.Mod)
                return mod.author;
            
            return string.Empty;
        }

        private string GetDescription()
        {
            if (type == KerbalStuffNodeType.Mod)
                return mod.short_description;

            return string.Empty;
        }
    }
}
