using System.Diagnostics.CodeAnalysis;

namespace KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class NamedTreeColumn : TreeColumn
    {
        public string Name { get { return mName; } set { mName = value; } }
        private string mName = string.Empty;
    }
}