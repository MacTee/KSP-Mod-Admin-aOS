using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Plugin.PartsTab.Model
{
    public class PartNode : Node
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public string Category { get; set; }
        public string Mod { get; set; }

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
