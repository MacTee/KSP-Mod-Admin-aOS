using System.Diagnostics.CodeAnalysis;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;

namespace KSPModAdmin.Core.Model
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ModSelectionTreeColumn : TreeColumn
    {
        public string Name { get { return mName; } set { mName = value; } }
        private string mName = string.Empty;
    }
}