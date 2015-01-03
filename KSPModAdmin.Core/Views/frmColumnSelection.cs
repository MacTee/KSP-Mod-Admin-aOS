using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;

namespace KSPModAdmin.Core.Views
{
    public partial class frmColumnSelection : frmBase
    {
        private ListView DragStartListView = null;


        public ModSelectionColumnsInfo ModSelectionColumns
        {
            get
            {
                List<ColumnData> columns = new List<ColumnData>();
                foreach (ListViewItem item in lvDisplayedColumns.Items)
                    columns.Add((ColumnData)item.Tag);

                return new ModSelectionColumnsInfo() { Columns = columns };
            }
            set
            {
                if (value == null)
                    return;

                lvDisplayedColumns.Items.Clear();
                foreach (ColumnData column in value.Columns)
                    lvDisplayedColumns.Items.Add(new ListViewItem() { Text = column.Header, Tag = column });

                lvAvailableColumns.Items.Clear();
                foreach (ColumnData column in ModSelectionColumnsInfo.AllDefaultColumns)
                {
                    if (!lvDisplayedColumns.Items.Cast<ListViewItem>().Any(item => item.Text == column.Header))
                        lvAvailableColumns.Items.Add(new ListViewItem() { Text = column.Header, Tag = column });
                }
            }
        }


        public frmColumnSelection()
        {
            InitializeComponent();
        }


        private void btnApply_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void listView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView == null)
                return;

            if (listView == lvDisplayedColumns && listView.SelectedItems.Contains(lvDisplayedColumns.Items[0]))
            {
                MessageBox.Show(this, string.Format("The \"{0}\" column can't be moved!", ((ColumnData)lvDisplayedColumns.Items[0].Tag).Header));
                return;
            }

            DragStartListView = listView;
            DragStartListView.DoDragDrop(listView.SelectedItems, DragDropEffects.Move | DragDropEffects.Copy);
        }

        private void listView_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            for (int i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    //The data from the drag source is moved to the target.	
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void listView_DragDrop(object sender, DragEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView == null)
                return;

            if (DragStartListView != listView)
                InsertDropedItem(listView, e);
            else
                MoveDropedItem(listView, e);

            DragStartListView = null;
        }

        private void listView_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView.SelectedListViewItemCollection)))
                e.Effect = e.AllowedEffect;
        }


        private void InsertDropedItem(ListView listView, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof (ListView.SelectedListViewItemCollection)))
            {
                if ((e.Effect & DragDropEffects.Copy) == DragDropEffects.Copy)
                {
                    // Obtain the index of the item at the mouse pointer.
                    var dragToItem = GetListViewItemAt(listView, new Point(e.X, e.Y));
                    int dragIndex = (dragToItem == null) ? -1 : dragToItem.Index;

                    if (listView == lvDisplayedColumns && dragIndex == 0)
                    {
                        MessageBox.Show(this, string.Format("You can't drop columns in front of \"{0}\" column!", ((ColumnData)lvDisplayedColumns.Items[0].Tag).Header));
                        return;
                    }

                    var items = (ListView.SelectedListViewItemCollection)e.Data.GetData(typeof (ListView.SelectedListViewItemCollection));
                    foreach (ListViewItem item in items)
                    {
                        // Obtain the ListViewItem to be dragged to the target location.
                        if (dragIndex == -1)
                        {
                            // Add to bottom.
                            listView.Items.Add((ListViewItem) item.Clone());
                        }
                        else
                        {
                            // Insert the item at the mouse pointer.
                            listView.Items.Insert(dragIndex, (ListViewItem) item.Clone());
                            dragIndex++;
                        }

                        //Removes the item from the initial location while the item is moved to the new location.
                        if (listView == lvDisplayedColumns)
                            lvAvailableColumns.Items.Remove(item);
                        else
                            lvDisplayedColumns.Items.Remove(item);

                    }
                }
            }
        }

        private void MoveDropedItem(ListView listView, DragEventArgs e)
        {
            // Return if the items are not selected in the ListView control.
            if (listView.SelectedItems.Count == 0)
                return;

            // Obtain the index of the item at the mouse pointer.
            var dragToItem = GetListViewItemAt(listView, new Point(e.X, e.Y));
            int dragIndex = (dragToItem == null) ? -1 : dragToItem.Index;

            if (listView == lvDisplayedColumns && dragIndex == 0)
            {
                MessageBox.Show(this, string.Format("You can't drop columns in front of \"{0}\" column!", ((ColumnData)lvDisplayedColumns.Items[0].Tag).Header));
                return;
            }

            // Copy SelectedItems cause the SelectedItems list will change when we move items around
            ListViewItem[] sel = listView.SelectedItems.Cast<ListViewItem>().ToArray();

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
                    listView.Items.Add((ListViewItem)dragItem.Clone());
                    listView.Items.Remove(dragItem);
                    continue;
                }

                if (dragItem.Index < itemIndex)
                    itemIndex++;
                else
                    itemIndex = dragIndex + i;

                // Insert the item at the mouse pointer.
                listView.Items.Insert(itemIndex, (ListViewItem)dragItem.Clone());

                //Removes the item from the initial location while the item is moved to the new location.
                listView.Items.Remove(dragItem);
            }
        }

        private ListViewItem GetListViewItemAt(ListView listView, Point pos)
        {
            //Returns the location of the mouse pointer in the ListView control.
            Point cp = listView.PointToClient(pos);

            //Obtain the item that is located at the specified location of the mouse pointer.
            return listView.GetItemAt(cp.X, cp.Y);;
        }
    }
}
