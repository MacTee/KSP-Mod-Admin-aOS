using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// Extended TabControl that allows the user the re-oder the TabPages via drag and drop.
    /// </summary>
    public class TabControlEx : TabControl
    {
        private TabPage selectedTab = null;
        private TabPage lastPointedTab = null;
        private bool allowTabDrag = false;

        /// <summary>
        /// Gets or sets the flag to determine if the user is able to rearrange the TabPage order by dragging a TabPage.
        /// </summary>
        [DefaultValue(false)]
        public bool AllowTabDrag
        {
            get { return allowTabDrag; }
            set
            {
                allowTabDrag = value;
                if (allowTabDrag)
                    AllowDrop = true;
            }
        }

        /// <summary>
        /// Handles the MouseDown event.
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e) 
        {
            selectedTab = GetTabAt(Cursor.Position);

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Handles the MouseUp event.
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e) 
        {
            selectedTab = null;

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Handles the MouseMove event.
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e) 
        {
            if (AllowTabDrag && e.Button == MouseButtons.Left && selectedTab != null)
                DoDragDrop(selectedTab, DragDropEffects.Move);

            base.OnMouseMove(e);
        }

        /// <summary>
        /// Handles the DragOver event.
        /// </summary>
        protected override void OnDragOver(DragEventArgs e) 
        {
            if (AllowTabDrag)
            {
                var draggedTab = e.Data.GetData(typeof(TabPage)) as TabPage;
                var pointedTab = GetTabAt(Cursor.Position);

                if (draggedTab == selectedTab && pointedTab != null)
                {
                    // this prevents flickering of the two last swaped taps when they have diferent width
                    // it's still doesn't work properly but it's fine for now...
                    if (lastPointedTab != pointedTab)
                    {
                        e.Effect = DragDropEffects.Move;

                        lastPointedTab = pointedTab;

                        if (pointedTab != draggedTab)
                            SwapTabPages(draggedTab, pointedTab);
                    }
                }
            }

            base.OnDragOver(e);
        }

        private TabPage GetTabAt(Point cursorPosition) 
        {
            for (int i = 0; i < TabPages.Count; i++)
            {
                if (GetTabRect(i).Contains(PointToClient(cursorPosition)))
                    return TabPages[i];
            }

            return null;
        }

        private void SwapTabPages(TabPage src, TabPage dst) 
        {
            int srcIndex = TabPages.IndexOf(src);
            int dstIndex = TabPages.IndexOf(dst);

            if (PlatformHelper.GetPlatform() == Platform.Linux || PlatformHelper.GetPlatform() == Platform.OsX)
            {
                TabPages.Insert(dstIndex, src);
                TabPages.Insert(srcIndex, dst);
            }
            else
            {
                TabPages[dstIndex] = src;
                TabPages[srcIndex] = dst;
            }

            if (SelectedIndex == srcIndex)
               SelectedIndex = dstIndex;
            else if (SelectedIndex == dstIndex)
                SelectedIndex = srcIndex;

            Refresh();
        }
    }
}
