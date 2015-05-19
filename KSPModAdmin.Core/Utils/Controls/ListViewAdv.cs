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
        /// <summary>
        /// Flag to determine if reordering is allowed.
        /// </summary>
        [DefaultValue(false)]
        public bool AllowReorder { get; set; }

        #region ActionKeyManager

        private ActionKeyManager actionKeyManager = new ActionKeyManager();

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the flag ListView.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            actionKeyManager.AddActionKey(key, callback, modifierKeys, once);
        }

        /// <summary>
        /// Removes all ActionKey CallbackFunction binding for the passed VirtualKey.
        /// </summary>
        /// <param name="key">The action key that raises the callbacks.</param>
        public void RemoveActionKey(VirtualKey key)
        {
            actionKeyManager.RemoveActionKey(key);
        }

        /// <summary>
        /// Removes all ActionKey CallbackFunction binding.
        /// </summary>
        public void RemoveAllActionKeys()
        {
            actionKeyManager.RemoveAllActionKeys();
        }

        /// <summary>
        /// Redirect the message loop messages to the ActionKeyManager.
        /// </summary>
        protected override void WndProc(ref Message msg)
        {
            if (actionKeyManager != null && actionKeyManager.HandleKeyMessage(ref msg))
                return;

            base.WndProc(ref msg);
        }

        #endregion

        /// <summary>
        /// Handles the ItemDrag event.
        /// </summary>
        protected override void OnItemDrag(ItemDragEventArgs e)
        {
            base.OnItemDrag(e);

            if (!AllowReorder)
                return;

            // Begins a drag-and-drop operation in the ListView control.
            this.DoDragDrop(this.SelectedItems, DragDropEffects.Move);
        }

        /// <summary>
        /// Handles the DragEnter event.
        /// </summary>
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
                    // The data from the drag source is moved to the target.
                    drgevent.Effect = DragDropEffects.Move;
                }
            }
        }

        /// <summary>
        /// Handles the DragDrop event.
        /// </summary>
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
