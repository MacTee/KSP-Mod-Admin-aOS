using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmConflictSolver : frmBase
    {
        private ConflicDataTreeModel model = new ConflicDataTreeModel();

        public List<ConflictInfoNode> ConflictData { get; set; }

        public frmConflictSolver()
        {
            InitializeComponent();

            ConflicDataTreeModel.BeforeCheckedChange += BeforeCheckedChange;
        }

        private void frmConflictSolver_Load(object sender, EventArgs e)
        {
            TreeViewAdvColumnHelper.ColumnsToTreeViewAdv(treeViewAdv, GetColumns());

            model.AddRange(ConflictData);
            treeViewAdv.Model = model;
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (!ValidateSelection())
                return;

            if (Solve())
                return;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BeforeCheckedChange(object sender, BeforeCheckedChangeEventArgs e)
        {
            e.Cancel = (e.Node.Parent as ConflictInfoNode == null);
        }

        private List<ColumnData> GetColumns()
        {
            List<ColumnData> columns = new List<ColumnData>()
                {
                    new ColumnData()
                    {
                        Name = "FileName",
                        Header = "FileName", ////Localizer.GlobalInstance["frmConflictSolver_Item_00"], // "FileName",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 150,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "FileName",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3,
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "Destination",
                        Header = "Destination", ////Localizer.GlobalInstance["frmConflictSolver_Item_01"], // "Destination",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 250,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "Destination",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "ConflictingMods",
                        Header = "ConflictingMods", ////Localizer.GlobalInstance["frmConflictSolver_Item_01"], // "Destination",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 250,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeCheckBox,
                                DataPropertyName = "Checked",
                                EditEnabled = true,
                                LeftMargin = 3
                            },
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "ModName",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    },
                    new ColumnData()
                    {
                        Name = "TreePath",
                        Header = "TreePath", ////Localizer.GlobalInstance["frmConflictSolver_Item_01"], // "Destination",
                        SortOrder = SortOrder.None,
                        TooltipText = null,
                        Width = 250,
                        Items = new List<ColumnItemData>()
                        {
                            new ColumnItemData()
                            {
                                Type = ColumnItemType.NodeTextBox,
                                DataPropertyName = "TreePath",
                                IncrementalSearchEnabled = true,
                                LeftMargin = 3
                            }
                        }
                    }
                };
            return columns;
        }

        private bool ValidateSelection()
        {
            // TODO:
            MessageBox.Show(this, "Not implemented yet!", "");
            return false;
        }

        private bool Solve()
        {
            // TODO:
            MessageBox.Show(this, "Not implemented yet!", "");
            return false;
        }
    }
}
