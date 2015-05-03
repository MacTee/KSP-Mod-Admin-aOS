using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public enum ColumnItemType
    {
        NodeCheckBox,
        NodeIcon,
        NodeTextBox
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ColumnData
    {
        public string Name { get; set; }
        public string Header { get; set; }
        public SortOrder SortOrder { get; set; }
        public string TooltipText { get; set; }
        public int Width { get; set; }
        public List<ColumnItemData> Items { get; set; }

        public ColumnData()
        {
            Name = string.Empty;
            Header = string.Empty;
            SortOrder = SortOrder.None;
            TooltipText = string.Empty;
            Width = 80;
            Items = new List<ColumnItemData>();
        }
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
    public class ColumnItemData
    {
        public ColumnItemType Type { get; set; }
        public string DataPropertyName { get; set; }
        public bool EditEnabled { get; set; }
        public bool IncrementalSearchEnabled { get; set; }
        public int LeftMargin { get; set; }
        public ImageScaleMode ImageScaleMode { get; set; }

        public ColumnItemData()
        {
            Type = ColumnItemType.NodeTextBox;
            DataPropertyName = string.Empty;
            EditEnabled = false;
            IncrementalSearchEnabled = false;
            LeftMargin = 0;
            ImageScaleMode = ImageScaleMode.Clip;
        }
    }
}