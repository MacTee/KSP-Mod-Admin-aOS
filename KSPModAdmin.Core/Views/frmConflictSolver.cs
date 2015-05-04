﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
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

            if (!Solve())
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
            List<ConflictInfoNode> missingSelection = new List<ConflictInfoNode>();
            foreach (var node in model.Nodes.Cast<ConflictInfoNode>())
            {
                bool noSelection = true;
                foreach (var child in node.Nodes.Cast<ConflictInfoNode>())
                {
                    if (child.Checked) 
                        noSelection = false;
                }

                if (noSelection)
                    missingSelection.Add(node);
            }

            if (missingSelection.Count > 0)
                MessageBox.Show(this, GetValidationMsg(missingSelection), "Validation");

            return missingSelection.Count == 0;
        }

        private bool Solve()
        {
            var conflictingFiles = model.Nodes.Cast<ConflictInfoNode>();
            foreach (var conflictingFile in conflictingFiles)
                SolveConflicts(conflictingFile);

            return true;
        }

        private string GetValidationMsg(List<ConflictInfoNode> missingSelection)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Please select a solving mod for this conflict files:");
            foreach (var node in missingSelection)
                sb.AppendLine(string.Format("- {0}", node.FileName));

            return sb.ToString();
        }

        private void SolveConflicts(ConflictInfoNode conflictingFile)
        {
            bool install = false;
            ModNode selectedNode = null;
            foreach (var fileNode in conflictingFile.Nodes.Cast<ConflictInfoNode>())
            {
                // remember selected node for later use.
                if (fileNode.Checked)
                {
                    selectedNode = fileNode.ConflictingNode;
                    continue;
                }

                // uninstall not selected files if installed.
                if (fileNode.ConflictingNode.IsInstalled)
                {
                    fileNode.ConflictingNode._Checked = false;
                    ModNodeHandler.ProcessMod(fileNode.ConflictingNode, true);
                    UninstallParentIfNecessary(fileNode.ConflictingNode);
                    install = true;
                }

                // reset destination of not selected files.
                fileNode.ConflictingNode._Checked = false;
                ModNodeHandler.SetDestinationPaths(fileNode.ConflictingNode, string.Empty);
                ResetPatentDestinationIfNecessary(fileNode.ConflictingNode);
            }

            // install selected file if one of the not selected was installed.
            if (install && selectedNode != null)
            {
                selectedNode._Checked = true;
                ModNodeHandler.ProcessMod(selectedNode, true);
                InstallParentIfNecessary(selectedNode);
            }
        }

        private void UninstallParentIfNecessary(ModNode modNode)
        {
            var parent = modNode.Parent as ModNode;
            if (parent == null)
                return;

            if (!parent.Checked && parent.IsInstalled)
            {
                parent._Checked = false;
                ModNodeHandler.ProcessMod(parent, true);
                UninstallParentIfNecessary(parent);
            }
        }

        private void InstallParentIfNecessary(ModNode modNode)
        {
            var parent = modNode.Parent as ModNode;
            if (parent == null)
                return;

            if (parent.Checked && !parent.IsInstalled)
            {
                parent._Checked = true;
                ModNodeHandler.ProcessMod(parent, true);
                InstallParentIfNecessary(parent);
            }
        }

        private void ResetPatentDestinationIfNecessary(ModNode modNode)
        {
            var parent = modNode.Parent as ModNode;
            if (parent == null)
                return;

            if (!parent.HasDestinationForChilds)
            {
                parent._Checked = false;
                ModNodeHandler.SetDestinationPaths(parent, string.Empty);
            }
        }
    }
}
