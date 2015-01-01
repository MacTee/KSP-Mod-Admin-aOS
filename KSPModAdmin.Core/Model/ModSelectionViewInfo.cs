using System.Collections.Generic;

namespace KSPModAdmin.Core.Model
{
    public class ModSelectionViewInfo
    {
        public double ModInfosSplitterPos = 0.0d;

        public List<int> ModInfosColumnWidths = new List<int>();

        public ModSelectionColumnsInfo ModSelectionColumnsInfo = null;

        public bool IsEmpty { get { return (ModInfosSplitterPos == 0.0d && ModInfosColumnWidths.Count == 0 && ModSelectionColumnsInfo == null); } }
    }
}