using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KSPModAdmin.Core.Utils.Controls
{
    /// <summary>
    /// A ListView with DragDrop reordering.
    /// See http://support.microsoft.com/kb/822483/en-us
    /// </summary>
    public class ListViewAdv : ListView
    {
        [DefaultValue(false)]
        public bool AllowReorder { get; set; }


        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (!AllowReorder)
                return;

            //Begins a drag-and-drop operation in the ListView control.
            this.DoDragDrop(this.SelectedItems, DragDropEffects.Move);
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);

            if (!AllowReorder)
                return;

            int len = drgevent.Data.GetFormats().Length - 1;
            for (int i = 0; i <= len; i++)
            {
                if (drgevent.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    drgevent.Effect = DragDropEffects.Move;
                }
            }
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            if (!AllowReorder)
                return;

            // Return if the items are not selected in the ListView control.
            if (this.SelectedItems.Count == 0)
                return;

            // Returns the location of the mouse pointer in the ListView control.
            Point cp = this.PointToClient(new Point(drgevent.X, drgevent.Y));
            // Obtain the item that is located at the specified location of the mouse pointer.
            ListViewItem dragToItem = this.GetItemAt(cp.X, cp.Y);

            // Obtain the index of the item at the mouse pointer.
            int dragIndex = -1;
            if (dragToItem != null)
                dragIndex = dragToItem.Index;

            // Copy SelectedItems cause the SelectedItems list will change when we move items around
            ListViewItem[] sel = this.SelectedItems.Cast<ListViewItem>().ToArray();

            // Move items
            for (int i = 0; i < sel.GetLength(0); i++)
            {
                // Obtain the ListViewItem to be dragged to the target location.
                ListViewItem dragItem = sel[i];
                int itemIndex = dragIndex;
                if (itemIndex == dragItem.Index)
                    return;

                if (itemIndex == -1)
                {
                    // Add to bottom
                    this.Items.Add((ListViewItem)dragItem.Clone());
                    this.Items.Remove(dragItem);
                    continue;
                }

                if (dragItem.Index < itemIndex)
                    itemIndex++;
                else
                    itemIndex = dragIndex + i;

                // Insert the item at the mouse pointer.
                ListViewItem insertItem = (ListViewItem)dragItem.Clone();
                this.Items.Insert(itemIndex, insertItem);

                // Removes the item from the initial location while 
                // the item is moved to the new location.
                this.Items.Remove(dragItem);
            }
        }
    }
}
