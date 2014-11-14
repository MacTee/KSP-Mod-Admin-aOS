using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Properties;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    public partial class ucModSelection : ucBase
    {
        #region Constructors

        public ucModSelection()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            #region Init TreeView NodeControls here to avoid init during DesignTime

            // Column 1 (Checked/Icon/Name)
            NodeCheckBox nodeCheckBox1 = new NodeCheckBox();
            nodeCheckBox1.DataPropertyName = "Checked";
            nodeCheckBox1.EditEnabled = true;
            nodeCheckBox1.LeftMargin = 0;
            nodeCheckBox1.ParentColumn = this.treeColumn1;
            tvModSelection.NodeControls.Add(nodeCheckBox1);

            NodeIcon nodeIcon1 = new NodeIcon();
            nodeIcon1.DataPropertyName = "Icon";
            nodeIcon1.LeftMargin = 1;
            nodeIcon1.ParentColumn = this.treeColumn1;
            nodeIcon1.ScaleMode = Utils.Controls.Aga.Controls.Tree.ImageScaleMode.Clip;
            tvModSelection.NodeControls.Add(nodeIcon1);

            NodeTextBox nodeTextBox1 = new NodeTextBox();
            nodeTextBox1.DataPropertyName = "Name";
            nodeTextBox1.IncrementalSearchEnabled = true;
            nodeTextBox1.LeftMargin = 3;
            nodeTextBox1.ParentColumn = this.treeColumn1;
            tvModSelection.NodeControls.Add(nodeTextBox1);

            // Column 2 (VersionControl)
            NodeTextBox nodeTextBox2 = new NodeTextBox();
            nodeTextBox2.DataPropertyName = "VersionControl";
            nodeTextBox2.IncrementalSearchEnabled = true;
            nodeTextBox2.LeftMargin = 3;
            nodeTextBox2.ParentColumn = this.treeColumn2;
            tvModSelection.NodeControls.Add(nodeTextBox2);

            #endregion

            ModSelectionController.Initialize(this);
            tvModSelection.Model = ModSelectionController.Model;

            // TODO: Fix display error of icons & CheckBoxes when Columns are used!
            if (Environment.OSVersion.Platform == PlatformID.Unix)
                tvModSelection.UseColumns = false;
        }

        #endregion

        #region Properties

        #region Direct access to ListViewItems

        private string ModName { set { lvModSelection.Items[0].SubItems[1].Text = value; } }

        private string ArchivePath { set { lvModSelection.Items[1].SubItems[1].Text = value; } }

        private string ModVersionControl { set { lvModSelection.Items[2].SubItems[1].Text = value; } }

        private string ModID { set { lvModSelection.Items[3].SubItems[1].Text = value; } }

        private string ModVersion { set { lvModSelection.Items[4].SubItems[1].Text = value; } }

        private string ModAuthor { set { lvModSelection.Items[5].SubItems[1].Text = value; } }

        private string ModCreationDate { set { lvModSelection.Items[6].SubItems[1].Text = value; } }

        private string ModChangeDate { set { lvModSelection.Items[7].SubItems[1].Text = value; } }

        private bool ModOutdated { set { lvModSelection.Items[8].SubItems[1].Text = (value) ? Messages.YES : Messages.NO; } }

        private string ModRating { set { lvModSelection.Items[9].SubItems[1].Text = value; } }

        private string ModDownloads { set { lvModSelection.Items[10].SubItems[1].Text = value; } }

        private string ModNote { set { lvModSelection.Items[11].SubItems[1].Text = value; } }

        private string FileName { set { lvModSelection.Items[12].SubItems[1].Text = value; } }

        private string FileDestination { set { lvModSelection.Items[13].SubItems[1].Text = (string.IsNullOrEmpty(value)) ? Messages.NONE : value; } }

        private bool FileConflict { set { lvModSelection.Items[14].SubItems[1].Text = (value) ? Messages.YES : Messages.NONE; } }

        private bool FileInstalled { set { lvModSelection.Items[15].SubItems[1].Text = (value) ? Messages.YES : Messages.NO; } }

        #endregion

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ModNode SelectedNode
        {
            get { return (tvModSelection.SelectedNode != null) ? tvModSelection.SelectedNode.Tag as ModNode : null; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public List<ModNode> SelectedNodes
        {
            get 
            { 
                List<ModNode> mods = new List<ModNode>(); 
                if (tvModSelection.SelectedNodes.Count > 0)
                {
                    foreach (var node in tvModSelection.SelectedNodes)
                        mods.Add((ModNode)node.Tag);
                }
                
                return mods;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool OverrideModFiles
        {
            get { return tsbOverride.Checked; }
            set { tsbOverride.Checked = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool ShowBusy
        {
            get
            {
                bool visible = false;
                InvokeIfRequired(() => { visible = tslBusy.Visible; });
                return visible;
            }
            set
            {
                InvokeIfRequired(() => { tslBusy.Visible = value; });
            }
        }

        #endregion

        /// <summary>
        /// Forces the view to redraw.
        /// </summary>
        public override void InvalidateView()
        {
            InvokeIfRequired(() =>
                {
                    Invalidate();
                    tvModSelection.Invalidate();
                    tvModSelection.Update();
                    tvModSelection.Refresh();
                });
        }

        #region Event handling

        private void ucModSelection_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            tvModSelection_SelectionChanged(null, null);
        }

        private void btnProceedHighlighted_Click(object sender, EventArgs e)
        {
            ModSelectionController.ProcessModsAsync(new ModNode[] { SelectedNode as ModNode });
        }

        private void btnProceedAll_Click(object sender, EventArgs e)
        {
            ModSelectionController.ProcessAllModsAsync();

            tvModSelection.Select();
            tvModSelection.Focus();
            tvModSelection.Invalidate();
        }

        private void tsbOverride_CheckedChanged(object sender, EventArgs e)
        {
            tsbOverride.Image = tsbOverride.Checked ? Resources.component_data_next : Resources.component_data_delete;
        }

        private void tsbAddMod_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenAddModDialog();
        }

        private void tsmiAddModArchives_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenAddModFileDialog();
        }

        private void tsbRemoveMod_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.RemoveMod(SelectedNodes.ToArray());
        }

        private void tsbRemoveAll_Click(object sender, EventArgs e)
        {
            ModSelectionController.RemoveAllMods();
        }

        private void tsbEditModInfos_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.EditModInfos(SelectedNode);
        }

        private void tsbCopyModInfos_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.CopyModInfos(SelectedNode);
        }

        private void tsbRefreshCheckedState_Click(object sender, EventArgs e)
        {
            ModSelectionController.RefreshCheckedStateOfModAsync(SelectedNode);
        }

        private void tsbChangeDestination_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.ChangeDestination(SelectedNode);
        }

        private void tsmiResetDestination_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.ResetDestination(SelectedNode);
        }

        private void tsbCreateZip_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.CreateZip(new ModNode[] { SelectedNode }.ToList());
        }

        private void tsbExImport_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenExportImportDialog();
        }

        private void tsbScan_Click(object sender, EventArgs e)
        {
            ModSelectionController.ScanGameData();
        }

        private void tsbUpdateCheckAllMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.CheckForUpdatesAllModsAsync();
        }

        private void tsbModUpdateCheck_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.CheckForModUpdatesAsync(new [] { SelectedNode.ZipRoot });
        }

        private void tsbUpdateAllOutdatedMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.UpdateAllOutdatedModsAsync();
        }

        private void tsbUpdateMod_Click(object sender, EventArgs e)
        {
            if (SelectedNode != null)
                ModSelectionController.UpdateOutdatedModsAsync(new[] { SelectedNode.ZipRoot });
        }

        private void tssbVisitVersionControlSite_ButtonClick(object sender, EventArgs e)
        {
            MessageBox.Show(ParentForm, "Not implemented yet!", Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void tsmiVisitAdditionalLink_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ParentForm, "Not implemented yet!", Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void tsbSolveConflicts_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenConflictSolver();
        }

        private void tsmiRedetectDestination_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ParentForm, "Not implemented yet!", Messages.MSG_TITLE_ATTENTION, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void tsbRefreshCheckedstateForAllMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.RefreshCheckedStateAllModsAsync();
        }

        private void tsbUncheckAllMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.UncheckAllMods();
        }

        private void tsbCheckAllMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.CheckAllMods();
        }

        private void tvModSelection_SelectionChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            ModNode selNode = SelectedNode;
            tvModSelection.ContextMenuStrip = (selNode == null) ? cmsModSelectionAllMods : cmsModSelectionOneMod;
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, tvModSelection.ContextMenuStrip, OptionsController.SelectedLanguage);

            if (selNode != null)
            {
                ModNode zipRoot = selNode.ZipRoot;
                ArchivePath = string.IsNullOrEmpty(zipRoot.Key) ? Messages.NONE : zipRoot.Key;
                ModName = zipRoot.Name;
                ModVersionControl = zipRoot.SiteHandlerName;
                ModID = zipRoot.ProductID;
                ModVersion = zipRoot.Version;
                ModAuthor = zipRoot.Author;
                ModCreationDate = zipRoot.CreationDate;
                ModChangeDate = zipRoot.ChangeDate;
                ModOutdated = zipRoot.IsOutdated;
                ModRating = zipRoot.Rating;
                ModDownloads = zipRoot.Downloads;
                ModNote = zipRoot.Note;
                FileName = selNode.Name;
                FileDestination = selNode.Destination;
                FileConflict = selNode.HasCollision;
                FileInstalled = selNode.IsInstalled;

                btnProceedHighlighted.Enabled = true;
                tsbProceedMod.Enabled = true;

                tssbAddMod.Enabled = true;
                tsmiAddMod.Enabled = true;
                tsmiAddModArchives.Enabled = true;

                tsbRemoveMod.Enabled = true;
                tsbRemoveAll.Enabled = true;

                //tsbExImport.Enabled = true;
                tsbScan.Enabled = true;
                tsbUpdateCheckAllMods.Enabled = true;
                tsbUpdateMod.Enabled = true;

                tsbProceedMod.Enabled = true;

                tsbModUpdateCheck.Enabled = true;

                //tssbVisitVersionControlSite.Enabled = true;
                //tsmiVisitCurseForge.Enabled = true;
                //tsmiVisitKSPForum.Enabled = true;

                tsbEditModInfos.Enabled = true;
                tsbCopyModInfos.Enabled = true;

                //tsbSolveConflicts.Enabled = true;
                tsbRefreshCheckedState.Enabled = true;

                tssbChangeDestination.Enabled = true;
                tsmiChangeDestination.Enabled = true;
                tsmiResetDestination.Enabled = true;

                tsbCreateZip.Enabled = !selNode.ZipExists;
            }
            else
            {
                ModName = "KSP Mod Admin AnyOS";
                ArchivePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                ModVersionControl = (OptionsController.VersionCheck) ? Messages.ON : Messages.OFF;
                ModID = string.Empty;
                ModVersion = VersionHelper.GetAssemblyVersion(false);
                ModAuthor = "BHeinrich";
                ModCreationDate = "27.05.2014";
                ModChangeDate = "1.11.2014";
                ModOutdated = false;
                ModRating = string.Empty;
                ModDownloads = "75k+";
                ModNote = "KSP MA aOS is the mod managing tool for KSP on any OS. ;)";
                FileName = "KSPModAdmin.exe";
                FileDestination = string.Empty;
                FileConflict = false;
                FileInstalled = true;

                btnProceedHighlighted.Enabled = false;
                tsbProceedMod.Enabled = false;

                //tssbAddMod.Enabled = false;
                //tsmiAddMod.Enabled = false;
                //tsmiAddModArchives.Enabled = false;

                tsbRemoveMod.Enabled = false;
                //tsbRemoveAll.Enabled = false;

                //tsbExImport.Enabled = false;
                //tsbScan.Enabled = false;
                //tsbUpdateCheckAllMods.Enabled = false;
                tsbUpdateMod.Enabled = false;

                tsbProceedMod.Enabled = false;

                tsbModUpdateCheck.Enabled = false;

                tssbVisitVersionControlSite.Enabled = false;
                tsmiVisitVersionControlSite.Enabled = false;
                tsmiVisitAdditionalLink.Enabled = false;

                tsbEditModInfos.Enabled = false;
                tsbCopyModInfos.Enabled = false;

                tsbSolveConflicts.Enabled = false;
                tsbRefreshCheckedState.Enabled = false;

                tssbChangeDestination.Enabled = false;
                tsmiChangeDestination.Enabled = false;
                tsmiResetDestination.Enabled = false;

                tsbCreateZip.Enabled = false;
            }

            lvModSelection.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            lvModSelection.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void tvModSelection_DrawControl(object sender, DrawEventArgs e)
        {
            var node = (ModNode)e.Node.Tag;
            if (!node.ZipExists)
                e.TextColor = OptionsController.ColorModArchiveMissing;

            if (node.IsInstalled || node.HasInstalledChilds)
            {
                if (node.Text.ToLower() == Constants.GAMEDATA)
                {
                    if (node.HasInstalledChilds)
                        e.TextColor = OptionsController.ColorModInstalled;
                }
                else
                    e.TextColor = OptionsController.ColorModInstalled;
            }
            else if (!node.HasDestination && !node.HasDestinationForChilds)
                e.TextColor = OptionsController.ColorDestinationMissing;
            if (node.IsOutdated)
                e.TextColor = OptionsController.ColorModOutdated;
            if (!node.ZipExists)
                e.TextColor = OptionsController.ColorModArchiveMissing;
            if (node.HasChildCollision)
                e.TextColor = OptionsController.ColorDestinationConflict;
        }

        private void tvModSelection_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];

                foreach (string filename in filenames)
                {
                    string ext = System.IO.Path.GetExtension(filename).ToLower();
                    if (ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR || ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT)
                    {
                        e.Effect = DragDropEffects.Copy; // Okay
                        break;
                    }
                }
            }
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void tvModSelection_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filenames = e.Data.GetData(DataFormats.FileDrop, true) as string[];
                List<string> accepted = new List<string>();
                foreach (string filename in filenames)
                {
                    string ext = System.IO.Path.GetExtension(filename).ToLower();
                    if (ext == Constants.EXT_ZIP || ext == Constants.EXT_RAR || ext == Constants.EXT_7ZIP || ext == Constants.EXT_CRAFT)
                        accepted.Add(filename);
                }

                if (accepted.Count > 0)
                    ModSelectionController.AddModsAsync(accepted.ToArray());
            }
        }

        private void cmsModSelectionOneMod_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModNode selectedNode = SelectedNode;
            if (selectedNode == null)
            {
                tsmiCmsDestination.Visible = false;
                tsmiCmsRefreschCheckedState.Visible = false;
                toolStripSeparator18.Visible = false;
                tsmiCmsEditModInfos.Visible = false;
                tsmiCmsCopyModInfos.Visible = false;
                toolStripSeparator17.Visible = false;
                tsmiCmsUpdatecheckMod.Visible = false;
                tsmiCmsVisitVersionControlSite.Visible = false;
                tsmiCmsVisitAdditionalLink.Visible = false;
                toolStripSeparator10.Visible = false;
                tsmiCmsSolveConflicts.Visible = false;
                toolStripSeparator19.Visible = false;
                tsmiCmsProceedMod.Visible = false;
                toolStripSeparator13.Visible = false;
                tsmiCmsRemoveMod.Visible = false;
                toolStripSeparator20.Visible = false;
                tsmiCmsCreateZip.Visible = false;
            }
            else
            {
                tsmiCmsDestination.Visible = true;
                tsmiCmsRefreschCheckedState.Visible = true;
                toolStripSeparator18.Visible = true;
                tsmiCmsEditModInfos.Visible = true;
                tsmiCmsCopyModInfos.Visible = true;
                toolStripSeparator17.Visible = true;
                tsmiCmsUpdatecheckMod.Visible = true;
                //tsmiCmsVisitVersionControlSite.Visible = true;
                //tsmiCmsVisitAdditionalLink.Visible = true;
                toolStripSeparator10.Visible = true;
                tsmiCmsSolveConflicts.Visible = true;
                toolStripSeparator19.Visible = true;
                tsmiCmsProceedMod.Visible = true;
                toolStripSeparator13.Visible = true;
                tsmiCmsRemoveMod.Visible = true;
                toolStripSeparator20.Visible = true;
                tsmiCmsCreateZip.Visible = true;
                tsmiCmsCreateZip.Enabled = !selectedNode.ZipExists;
            }

        }

        #endregion

        internal void SetEnabledOfAllControls(bool enable)
        {
            InvokeIfRequired(() =>
            {
                foreach (ToolStripItem c in tsModSelection.Items)
                    if (c != tslBusy) c.Enabled = enable;

                foreach (ToolStripItem c in tsMod.Items)
                    c.Enabled = enable;

                //tvModSelection.ReadOnly = !enable;
                btnProceedHighlighted.Enabled = enable;
                btnProceedAll.Enabled = enable;
            });
        }

        internal void SetProgressBarStates(bool visibile, int max = 100, int value = 0)
        {
            InvokeIfRequired(() =>
            {
                tsProgressBar.Maximum = max;
                tsProgressBar.Value = (value <= max) ? value : max;
                tsProgressBar.Visible = visibile;
            });
        }

        internal ModSelectionViewInfo GetModSelectionViewInfo()
        {
            ModSelectionViewInfo vInfo = new ModSelectionViewInfo();

            if (splitContainer1.SplitterDistance > 0)
            {
                double d = splitContainer1.SplitterDistance / (double)splitContainer1.Width;
                vInfo.ModInfosSplitterPos = d;
            }

            foreach (TreeColumn column in tvModSelection.Columns)
                vInfo.ModSelectionColumnWidths.Add(column.Width);

            foreach (ColumnHeader column in lvModSelection.Columns)
                vInfo.ModInfosColumnWidths.Add(column.Width);

            return vInfo;
        }

        internal void SetModSelectionViewInfo(ModSelectionViewInfo vInfo)
        {
            if (vInfo.ModInfosSplitterPos > 0)
            {
                double d = (double)splitContainer1.Width * vInfo.ModInfosSplitterPos;
                splitContainer1.SplitterDistance = (int)d + 1;
            }

            for (int i = 0; i < vInfo.ModSelectionColumnWidths.Count; ++i)
            {
                var width = vInfo.ModSelectionColumnWidths[i];
                if (i < tvModSelection.Columns.Count && width > 0)
                    tvModSelection.Columns[i].Width = vInfo.ModSelectionColumnWidths[i];
            }

            for (int i = 0; i < vInfo.ModInfosColumnWidths.Count; ++i)
            {
                var width = vInfo.ModInfosColumnWidths[i];
                if (i < lvModSelection.Columns.Count && width > 0)
                    lvModSelection.Columns[i].Width = width;
            }
        }
    }

    internal class ModSelectionViewInfo
    {
        public double ModInfosSplitterPos = 0.0d;

        public List<int> ModSelectionColumnWidths = new List<int>();
        public List<int> ModInfosColumnWidths = new List<int>();

        public bool IsEmpty  { get { return (ModInfosSplitterPos == 0.0d && ModSelectionColumnWidths.Count == 0 && ModInfosColumnWidths.Count == 0); } }
    }
}
