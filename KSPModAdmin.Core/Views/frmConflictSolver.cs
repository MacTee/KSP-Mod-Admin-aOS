using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    /// <summary>
    /// This view handles the solving of conflicts between ModNode files.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class frmConflictSolver : frmBase
    {
        private ConflicDataTreeModel model = new ConflicDataTreeModel();

        /// <summary>
        /// Gets or sets the conflict data this view operates on.
        /// </summary>
        public List<ConflictInfoNode> ConflictData { get; set; }

        /// <summary>
        /// Creates a new instance of the frmConflictSolver class.
        /// This view handles the solving of conflicts between ModNode files.
        /// </summary>
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

            DialogResult = DialogResult.OK;
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

        private void CmsConflictSolver_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var selNode = treeViewAdv.SelectedNode;
            e.Cancel = (selNode == null);
            if (e.Cancel)
                return;

            var row = selNode.Tag as ConflictInfoNode;
            e.Cancel = (row == null);
            if (e.Cancel)
                return;

            e.Cancel = row.Nodes.Count > 0;
        }

        private void tsmiSolveAllConflictsWithThisMod_Click(object sender, EventArgs e)
        {
            var i = Cursor.Position;
            var row = treeViewAdv.SelectedNode.Tag as ConflictInfoNode;

            foreach (var conflictFile in model.Nodes)
            {
                var oneIsAlreadyChecked = conflictFile.Nodes.Cast<ConflictInfoNode>().Any(x => x.Checked);
                if (oneIsAlreadyChecked)
                    continue;

                foreach (ConflictInfoNode mod in conflictFile.Nodes)
                {
                    if (mod.ArchivePath == row.ArchivePath)
                    {
                        mod._Checked = true;
                        break;
                    }
                }
            }

            treeViewAdv.Refresh();
        }

        private List<ColumnData> GetColumns()
        {
            List<ColumnData> columns = new List<ColumnData>()
            {
                new ColumnData()
                {
                    Name = "FileName",
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_00"], // "Filename", ////
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
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_01"], // "Destination", ////
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
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_02"], // "Conflicting Mods", ////
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
                    Name = "Version",
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_04"], // "Version", ////
                    SortOrder = SortOrder.None,
                    TooltipText = null,
                    Width = 60,
                    Items = new List<ColumnItemData>()
                    {
                        new ColumnItemData()
                        {
                            Type = ColumnItemType.NodeTextBox,
                            DataPropertyName = "ModVersion",
                            IncrementalSearchEnabled = true,
                            LeftMargin = 3
                        }
                    }
                },
                new ColumnData()
                {
                    Name = "TreePath",
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_03"], // "TreePath", ////
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
                },
                new ColumnData()
                {
                    Name = "ArchivePath",
                    Header = Localizer.GlobalInstance["frmConflictSolver_Item_05"], // "ArchivePath", ////
                    SortOrder = SortOrder.None,
                    TooltipText = null,
                    Width = 250,
                    Items = new List<ColumnItemData>()
                    {
                        new ColumnItemData()
                        {
                            Type = ColumnItemType.NodeTextBox,
                            DataPropertyName = "ArchivePath",
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

            if (missingSelection.Count > 0 && missingSelection.Count < 16)
                MessageBox.Show(this, GetValidationMsg(missingSelection), Messages.MSG_TITLE_VALIDATION);
            else if (missingSelection.Count < 15)
                MessageBox.Show(this, GetValidationMsg(missingSelection, true), Messages.MSG_TITLE_VALIDATION);

            return missingSelection.Count == 0;
        }

        private bool Solve()
        {
            var conflictingFiles = model.Nodes.Cast<ConflictInfoNode>();
            foreach (var conflictingFile in conflictingFiles)
                SolveConflicts(conflictingFile);

            return !ModRegister.HasConflicts;
        }

        private string GetValidationMsg(List<ConflictInfoNode> missingSelection, bool shortMsg = false)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(Messages.MSG_PLEASE_SELECT_SOLVING_MODS);

            int count = 0;
            foreach (var node in missingSelection)
            {
                if (shortMsg && count > 15)
                {
                    sb.AppendLine("- ...");
                    break;
                }

                sb.AppendLine(string.Format("- {0}", node.FileName));

                count++;
            }

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
                    Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_REMOVE_CONFLICT_FILE_0, fileNode.ConflictingNode.Name));
                    fileNode.ConflictingNode._Checked = false;
                    ModNodeHandler.ProcessMod(fileNode.ConflictingNode, true);

                    UninstallParentIfNecessary(fileNode.ConflictingNode);
                    install = true;
                }

                // reset destination of not selected files.
                Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_RESET_DESTINATION_CONFLICT_FILE_0, fileNode.ConflictingNode.Name));
                fileNode.ConflictingNode._Checked = false;
                ModNodeHandler.SetDestinationPaths(fileNode.ConflictingNode, string.Empty);
                ResetPatentDestinationIfNecessary(fileNode.ConflictingNode);
            }

            // install selected file if one of the not selected was installed.
            if (install && selectedNode != null)
            {
                Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_INSTALL_SELECTED_FILE_0, selectedNode.Name));
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
                Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_UNINSTALL_PARENT_FOLDER_0_1, modNode.Name, modNode.ZipRoot.Name));
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
                Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_INSTALL_PARENT_FOLDER_0_1, modNode.Name, modNode.ZipRoot.Name));
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
                Messenger.AddInfo(string.Format(Messages.MSG_CONFLICT_SOLVER_RESET_DESTINATION_PARENT_FOLDER_0_1, modNode.Name, modNode.ZipRoot.Name));
                parent._Checked = false;
                ModNodeHandler.SetDestinationPaths(parent, string.Empty);
            }
        }
    }
}
