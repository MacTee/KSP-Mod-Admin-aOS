using System.Collections.Generic;

namespace KSPModAdmin.Core.Model
{
    /// <summary>
    /// Class that holds ModSelectionView related informations.
    /// </summary>
    public class ModSelectionViewInfo
    {
        /// <summary>
        /// The position of the splitter from the SlipControl.
        /// </summary>
        public double ModInfosSplitterPos { get { return mModInfosSplitterPos; } set { mModInfosSplitterPos = value; } }
        private double mModInfosSplitterPos = 0.0d;

        /// <summary>
        /// List of width of all columns.
        /// </summary>
        public List<int> ModInfosColumnWidths { get { return mModInfosColumnWidths; } set { mModInfosColumnWidths = value; } }
        private List<int> mModInfosColumnWidths = new List<int>();

        /// <summary>
        /// Informations of the ModSelection columns.
        /// </summary>
        public ModSelectionColumnsInfo ModSelectionColumnsInfo { get; set; }

        /// <summary>
        /// Flag to determine if this object is empty.
        /// </summary>
        public bool IsEmpty { get { return (ModInfosSplitterPos == 0.0d && ModInfosColumnWidths.Count == 0 && ModSelectionColumnsInfo == null); } }
    }
}