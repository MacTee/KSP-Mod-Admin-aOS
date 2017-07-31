using KSPMODAdmin.Core.Utils.Ckan;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.ModBrowserTab.Model
{
    /// <summary>
    /// The TreeModel for the ModBrowserCkan TreeView.
    /// </summary>
    public class CkanTreeModel : TreeModel
    {
        /// <summary>
        /// Adds the content of the CkanArchive to the CkanTreeModel.
        /// </summary>
        /// <param name="archive">The archive to add.</param>
        public void AddArchive(CkanArchive archive)
        {
            foreach (var mod in archive.Mods)
                Nodes.Add(new CkanNode(mod.Value));
        }
    }
}