using System.Collections.Generic;

namespace KSPMODAdmin.Core.Utils.Ckan
{
    /// <summary>
    /// Class that represents a mod entry in a CKAN Repository archive.
    /// </summary>
    public class CkanMod
    {
        /// <summary>
        /// Name of the mod.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The all known CKAN ModInfos for this mod.
        /// </summary>
        public List<CkanModInfo> ModInfos { get; set; }

        /// <summary>
        /// Path to the CKAN Repository archive.
        /// </summary>
        public string ArchivePath { get; set; }

        /// <summary>
        /// Creates a new instance of the class CkanMod.
        /// </summary>
        public CkanMod()
        {
            ModInfos = new List<CkanModInfo>();
        }
    }
}