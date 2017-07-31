using System.Collections.Generic;

namespace KSPMODAdmin.Core.Utils.Ckan
{
    /// <summary>
    /// Class that contains all information from a Ckan Repository archive.
    /// </summary>
    public class CkanArchive
    {
        /// <summary>
        /// The CKAN Repository information from which this Archive comes from.
        /// </summary>
        public CkanRepository Repository { get; set; }

        /// <summary>
        /// The full path to the CKAN Repository archive.
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// A List of all Mods that this CKAN Repository Archive contains.
        /// </summary>
        public Dictionary<string, CkanMod> Mods { get; set; }

        /// <summary>
        /// Creates a instance of the class CkanArchive.
        /// </summary>
        public CkanArchive()
        {
            Mods = new Dictionary<string, CkanMod>();
        }
    }
}