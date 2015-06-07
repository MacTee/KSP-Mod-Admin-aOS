using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KSPModAdmin.Core.Model;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.Helper;
using KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls;
using KSPModAdmin.Core.Controller;
using KSPModAdmin.Core.Properties;
using KSPModAdmin.Core.Utils;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed. Suppression is OK here.")]
    public partial class ucModSelection : ucBase
    {
        #region Constructors

        /// <summary>
        /// Creates a new instance of the ucModSelection class.
        /// </summary>
        public ucModSelection()
        {
            InitializeComponent();

            // Create TreeViewAdv columns
            new ModSelectionColumnsInfo().ToTreeViewAdv(tvModSelection);

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            ModSelectionController.Initialize(this);
            tvModSelection.Model = ModSelectionController.Model;

            // TODO: Fix display error of icons & CheckBoxes when Columns are used!
            if (PlatformHelper.GetPlatform() == Platform.Linux)
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

        private string KSPVersion { set { lvModSelection.Items[5].SubItems[1].Text = value; } }

        private string ModAuthor { set { lvModSelection.Items[6].SubItems[1].Text = value; } }

        private string ModCreationDate { set { lvModSelection.Items[7].SubItems[1].Text = value; } }

        private string ModChangeDate { set { lvModSelection.Items[8].SubItems[1].Text = value; } }

        private bool ModOutdated { set { lvModSelection.Items[9].SubItems[1].Text = (value) ? Messages.YES : Messages.NO; } }

        private string ModRating { set { lvModSelection.Items[10].SubItems[1].Text = value; } }

        private string ModDownloads { set { lvModSelection.Items[11].SubItems[1].Text = value; } }

        private string ModNote { set { lvModSelection.Items[12].SubItems[1].Text = value; } }

        private string FileName { set { lvModSelection.Items[13].SubItems[1].Text = value; } }

        private string FileDestination { set { lvModSelection.Items[14].SubItems[1].Text = (string.IsNullOrEmpty(value)) ? Messages.NONE : value; } }

        private bool FileConflict { set { lvModSelection.Items[15].SubItems[1].Text = (value) ? Messages.YES : Messages.NONE; } }

        private bool FileInstalled { set { lvModSelection.Items[16].SubItems[1].Text = (value) ? Messages.YES : Messages.NO; } }

        #endregion

        /// <summary>
        /// Flag to determine if a ModNode is selected.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool HasSelectedNode
        {
            get { return (tvModSelection.SelectedNode != null); }
        }

        /// <summary>
        /// Gets the selected ModNode.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ModNode SelectedNode
        {
            get { return (tvModSelection.SelectedNode != null) ? tvModSelection.SelectedNode.Tag as ModNode : null; }
        }

        /// <summary>
        /// Gets the root ModNode of the selected ModNode.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public ModNode SelectedMod
        {
            get { return (tvModSelection.SelectedNode != null) ? (tvModSelection.SelectedNode.Tag as ModNode).ZipRoot : null; }
        }

        /// <summary>
        /// Gets a list of all selected Mods (root ModNodes).
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public List<ModNode> SelectedMods
        {
            get 
            { 
                List<ModNode> mods = new List<ModNode>(); 
                if (tvModSelection.SelectedNodes.Count > 0)
                {
                    foreach (var node in tvModSelection.SelectedNodes)
                    {
                        ModNode root = (node.Tag as ModNode).ZipRoot;
                        if (!mods.Contains(root))
                            mods.Add(root);
                    }
                }
                
                return mods;
            }
        }

        /// <summary>
        /// Flag to determine if files of a mod should be overridden during the installation of a mod.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden)]
        public bool OverrideModFiles
        {
            get { return tsbOverride.Checked; }
            set { tsbOverride.Checked = value; }
        }

        /// <summary>
        /// Gets or sets the visibility of the loading indicator.
        /// </summary>
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
                    tvModSelection_SelectionChanged(null, null);
                    Invalidate();
                    tvModSelection.Invalidate();
                    tvModSelection.Update();
                    tvModSelection.Refresh();
                });
        }

        /// <summary>
        /// Sets the Selected Node of the TreeViewAdv Control to null.
        /// </summary>
        public void ResetSelectedNode()
        {
            tvModSelection.SelectedNode = null;
        }

        /// <summary>
        /// Add a ActionKey CallbackFunction binding to the flag ListView.
        /// </summary>
        /// <param name="key">The action key that raises the callback.</param>
        /// <param name="callback">The callback function with the action that should be called.</param>
        /// <param name="modifierKeys">Required state of the modifier keys to get the callback function called.</param>
        /// <param name="once">Flag to determine if the callback function should only be called once.</param>
        public void AddActionKey(VirtualKey key, ActionKeyHandler callback, ModifierKey[] modifierKeys = null, bool once = false)
        {
            tvModSelection.AddActionKey(key, callback, modifierKeys, once);
        }

        #region Event handling

        private void ucModSelection_Load(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            tvModSelection_SelectionChanged(null, null);
        }

        #region Button event handling

        private void btnProceedMod_Click(object sender, EventArgs e)
        {
            ModSelectionController.ProcessModsAsync(new[] { SelectedMod });
        }

        private void btnProceedHighlighted_Click(object sender, EventArgs e)
        {
            ModSelectionController.ProcessModsAsync(SelectedMods.ToArray());
        }

        private void btnProceedAll_Click(object sender, EventArgs e)
        {
            ModSelectionController.ProcessAllModsAsync();

            tvModSelection.Select();
            tvModSelection.Focus();
            tvModSelection.Invalidate();
        }

        #region ToolStripMenu & ContextMenu buttons event handling

        private void tsbOverride_CheckedChanged(object sender, EventArgs e)
        {
            tsbOverride.Image = tsbOverride.Checked ? Resources.component_next_data_24x24 : Resources.component_delete_data_24x24;
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
            if (HasSelectedNode)
                ModSelectionController.RemoveMod(new[] { SelectedMod });
        }

        private void tsmiCmsRemoveHighlightedMods_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.RemoveMod(SelectedMods.ToArray());
        }

        private void tsbRemoveAll_Click(object sender, EventArgs e)
        {
            ModSelectionController.RemoveAllMods();
        }

        private void tsbEditModInfos_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.EditModInfos(SelectedMod);
        }

        private void tsbCopyModInfos_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.CopyModInfos(SelectedMod);
        }

        private void tsbRefreshCheckedState_Click(object sender, EventArgs e)
        {
            ModSelectionController.RefreshCheckedStateOfModsAsync(new[] { SelectedMod });
        }

        private void tsmiCmsRefreshCheckedStateForHighlightedMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.RefreshCheckedStateOfModsAsync(SelectedMods.ToArray());
        }

        private void tsbChangeDestination_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.ChangeDestination(SelectedNode);
        }

        private void tsmiResetDestination_Click(object sender, EventArgs e)
        {
            ////if (HasSelectedNode)
            ////    ModSelectionController.ResetDestination(SelectedNode);

            if (tvModSelection.SelectedNodes.Count > 0)
            {
                foreach (var node in tvModSelection.SelectedNodes)
                {
                    ModSelectionController.ResetDestination(node.Tag as ModNode);
                }
            }
        }

        private void tsbCreateZip_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.CreateZip(new ModNode[] { SelectedMod }.ToList());
        }

        private void tsbExImport_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenExportImportDialog();
        }

        private void tsmiExporText_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
            {
                InitialDirectory = OptionsController.DownloadPath, 
                FileName = "ModList.txt"
            };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var sw = new StreamWriter(dlg.OpenFile());
                sw.Write(ModSelectionController.GetModListAsText());
                sw.Close();
            }
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
            if (HasSelectedNode)
                ModSelectionController.CheckForModUpdatesAsync(new[] { SelectedMod });
        }

        private void tsmiCmsCheckHighlightedModsForUpdates_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.CheckForModUpdatesAsync(SelectedMods.ToArray());
        }

        private void tsbUpdateAllOutdatedMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.UpdateAllOutdatedModsAsync();
        }

        private void tsbUpdateMod_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.UpdateOutdatedModsAsync(new[] { SelectedMod });
        }

        private void tsmiUpdateHiglightedMods_Click(object sender, EventArgs e)
        {
            if (HasSelectedNode)
                ModSelectionController.UpdateOutdatedModsAsync(SelectedMods.ToArray());
        }

        private void tssbVisitVersionControlSite_ButtonClick(object sender, EventArgs e)
        {
            if (SelectedMod != null &&  !string.IsNullOrEmpty(SelectedMod.ModURL)) 
                Process.Start(SelectedMod.ModURL);
        }

        private void tsmiVisitAdditionalLink_Click(object sender, EventArgs e)
        {
            if (SelectedMod != null &&  !string.IsNullOrEmpty(SelectedMod.AdditionalURL)) 
                Process.Start(SelectedMod.AdditionalURL);
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

        private void tsbHelp_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenWiki();
        }

        private void tsbRelocateArchivePath_Click(object sender, EventArgs e)
        {
            ModSelectionController.RelocateArchivePath(SelectedMod);
        }

        private void tsmiRelocateArchivePathAllMods_Click(object sender, EventArgs e)
        {
            ModSelectionController.RelocateArchivePathAllMods();
        }

        private void tsmiCmsTreeViewOptions_Click(object sender, EventArgs e)
        {
            ModSelectionController.OpenTreeViewOptions();
        }

        private void tsmiCmsOneModOpenFolder_Click(object sender, EventArgs e)
        {
            OpenFolder(SelectedNode);
        }

        private void tsmiCmsOneModOpenFile_Click(object sender, EventArgs e)
        {
            OpenFile(SelectedNode);
        }

        #endregion

        #endregion

        #region TreeViewAdv event handling

        private void tvModSelection_SelectionChanged(object sender, EventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime || DesignMode)
                return;

            ModNode selectedNode = SelectedNode;
            tvModSelection.ContextMenuStrip = (selectedNode == null) ? cmsModSelectionAllMods : cmsModSelectionOneMod;
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, tvModSelection.ContextMenuStrip, OptionsController.SelectedLanguage);

            if (selectedNode != null)
            {
                ModNode zipRoot = selectedNode.ZipRoot;
                ArchivePath = string.IsNullOrEmpty(zipRoot.Key) ? Messages.NONE : zipRoot.Key;
                ModName = zipRoot.Name;
                ModVersionControl = zipRoot.SiteHandlerName;
                ModID = zipRoot.ProductID;
                ModVersion = zipRoot.Version;
                KSPVersion = zipRoot.KSPVersion;
                ModAuthor = zipRoot.Author;
                ModCreationDate = zipRoot.CreationDate;
                ModChangeDate = zipRoot.ChangeDate;
                ModOutdated = zipRoot.IsOutdated;
                ModRating = zipRoot.Rating;
                ModDownloads = zipRoot.Downloads;
                ModNote = zipRoot.Note;
                FileName = selectedNode.Name;
                FileDestination = selectedNode.Destination;
                FileConflict = selectedNode.HasCollision;
                FileInstalled = selectedNode.IsInstalled;

                btnProceedHighlighted.Enabled = true;
                tsbProceedMod.Enabled = true;

                tssbAddMod.Enabled = true;
                tsmiAddMod.Enabled = true;
                tsmiAddModArchives.Enabled = true;

                tsbRemoveMod.Enabled = true;
                tsbRemoveAll.Enabled = true;

                ////tsbExImport.Enabled = true;
                tsbScan.Enabled = true;
                tsbUpdateCheckAllMods.Enabled = true;
                tsbUpdateMod.Enabled = true;

                tsbProceedMod.Enabled = true;

                tsbModUpdateCheck.Enabled = true;

                tssbVisitVersionControlSite.Enabled = true;
                tsmiVisitVersionControlSite.Enabled = !string.IsNullOrEmpty(selectedNode.ZipRoot.ModURL);
                tsmiVisitAdditionalLink.Enabled = !string.IsNullOrEmpty(selectedNode.ZipRoot.AdditionalURL);


                tsbEditModInfos.Enabled = true;
                tsbCopyModInfos.Enabled = true;

                tsbSolveConflicts.Enabled = ModRegister.HasConflicts;
                tsbRefreshCheckedState.Enabled = true;

                tssbChangeDestination.Enabled = true;
                tsmiChangeDestination.Enabled = true;
                tsmiResetDestination.Enabled = true;

                tsbCreateZip.Enabled = !selectedNode.ZipExists;
                tsbRelocateArchivePath.Enabled = true;
            }
            else
            {
                ModName = "KSP Mod Admin AnyOS";
                ArchivePath = System.Reflection.Assembly.GetEntryAssembly().Location;
                ModVersionControl = (OptionsController.VersionCheck) ? Messages.ON : Messages.OFF;
                ModID = string.Empty;
                ModVersion = VersionHelper.GetAssemblyVersion(false);
                KSPVersion = "0.21 or higher";
                ModAuthor = "BHeinrich (MacKerbal@mactee.de)";
                ModCreationDate = "25.04.2013";
                ModChangeDate = VersionHelper.GetChangeDate(false).ToString();
                ModOutdated = false;
                ModRating = string.Empty;
                ModDownloads = "86k+";
                ModNote = "KSP MA aOS is the mod managing tool for KSP on any OS. ;)";
                FileName = "KSPModAdmin.exe";
                FileDestination = string.Empty;
                FileConflict = false;
                FileInstalled = true;

                btnProceedHighlighted.Enabled = false;
                tsbProceedMod.Enabled = false;

                ////tssbAddMod.Enabled = false;
                ////tsmiAddMod.Enabled = false;
                ////tsmiAddModArchives.Enabled = false;

                tsbRemoveMod.Enabled = false;
                ////tsbRemoveAll.Enabled = false;

                ////tsbExImport.Enabled = false;
                ////tsbScan.Enabled = false;
                ////tsbUpdateCheckAllMods.Enabled = false;
                tsbUpdateMod.Enabled = false;

                tsbProceedMod.Enabled = false;

                tsbModUpdateCheck.Enabled = false;

                tssbVisitVersionControlSite.Enabled = false;
                tsmiVisitVersionControlSite.Enabled = false;
                tsmiVisitAdditionalLink.Enabled = false;

                tsbEditModInfos.Enabled = false;
                tsbCopyModInfos.Enabled = false;

                tsbSolveConflicts.Enabled = ModRegister.HasConflicts;
                tsbRefreshCheckedState.Enabled = false;

                tssbChangeDestination.Enabled = false;
                tsmiChangeDestination.Enabled = false;
                tsmiResetDestination.Enabled = false;

                tsbCreateZip.Enabled = false;
                tsbRelocateArchivePath.Enabled = false;
            }

            lvModSelection.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
            lvModSelection.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.ColumnContent);
        }

        private void tvModSelection_DrawControl(object sender, DrawEventArgs e)
        {
            var node = (ModNode)e.Node.Tag;
            e.TextColor = Color.Black;

            if (!node.ZipExists)
                e.TextColor = OptionsController.ColorModArchiveMissing;

            if (node.IsInstalled || node.HasInstalledChilds)
            {
                if (node.Text.Equals(Constants.GAMEDATA, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (node.HasInstalledChilds)
                        e.TextColor = OptionsController.ColorModInstalled;
                }
                else
                    e.TextColor = OptionsController.ColorModInstalled;
            }
            else if (!node.HasDestination && !node.HasDestinationForChilds)
                e.TextColor = OptionsController.ColorDestinationMissing;
            if (OptionsController.Color4OutdatedMods && node.IsOutdated)
                e.TextColor = OptionsController.ColorModOutdated;
            if (!node.ZipExists)
                e.TextColor = OptionsController.ColorModArchiveMissing;
            if (OptionsController.ConflictDetectionOnOff && (node.HasChildCollision || (node.IsFile && node.HasCollision)))
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

        private void tvModSelection_DoubleClick(object sender, EventArgs e)
        {
            OpenFile(SelectedNode);
        }

        private void tvModSelection_ColumnClicked(object sender, TreeColumnEventArgs e)
        {
            if (e.Column == null)
                return;

            if (e.Column.SortOrder == SortOrder.None)
                e.Column.SortOrder = SortOrder.Ascending;

            else if (e.Column.SortOrder == SortOrder.Descending)
                e.Column.SortOrder = SortOrder.Ascending;

            else if (e.Column.SortOrder == SortOrder.Ascending)
                e.Column.SortOrder = SortOrder.Descending;

            SortColumn(e.Column as NamedTreeColumn);
        }

        #endregion

        private void cmsModSelectionOneMod_Opened(object sender, EventArgs e)
        {
            int selectedModCount = SelectedMods.Count;
            ModNode selectedNode = SelectedNode;
            if (HasSelectedNode)
            {
                if (selectedModCount != 1)
                    tsmiCmsDestinationPath.Text = "<" + Messages.MSG_NOT_AVAILABLE + ">";
                else
                    tsmiCmsDestinationPath.Text = string.IsNullOrEmpty(selectedNode.Destination) ? "<" + Messages.MSG_NO_DESTINATION + ">" : selectedNode.Destination;

                tsmiCmsSelectNewDestination.Enabled = (selectedModCount == 1);
                tsmiCmsResetDestination.Enabled = (selectedModCount == 1);
                tsmiCmsRedetectDestination.Enabled = (selectedModCount == 1);
                tsmiCmsRefreschCheckedState.Visible = (selectedModCount == 1);
                tsmiCmsRefreshCheckedStateForHighlightedMods.Visible = !tsmiCmsRefreschCheckedState.Visible;
                tsmiCmsEditModInfos.Enabled = (selectedModCount == 1);
                tsmiCmsCopyModInfos.Enabled = (selectedModCount == 1);
                tsmiCmsUpdatecheckMod.Visible = (selectedModCount == 1);
                tsmiCmsCheckHighlightedModsForUpdates.Visible = !tsmiCmsUpdatecheckMod.Visible;
                tsmiUpdateMod.Visible = (selectedModCount == 1);
                tsmiUpdateHiglightedMods.Visible = !tsmiUpdateMod.Visible;
                tsmiCmsVisitVersionControlSite.Enabled = !string.IsNullOrEmpty(selectedNode.ZipRoot.ModURL);
                tsmiCmsVisitAdditionalLink.Enabled = !string.IsNullOrEmpty(selectedNode.ZipRoot.AdditionalURL);
                tsmiCmsRemoveMod.Visible = (selectedModCount == 1);
                tsmiCmsRemoveHighlightedMods.Visible = !tsmiCmsRemoveMod.Visible;
                tsmiCmsProceedMod.Visible = (selectedModCount == 1);
                tsmiCmsProceedHighlightedMods.Visible = !tsmiCmsProceedMod.Visible;
                tsmiCmsCreateZip.Enabled = !selectedNode.ZipExists;
                tsmiCmsOneModOpenFile.Visible = selectedNode.IsFile;
                tsmiCmsOneModOpenFolder.Visible = !selectedNode.IsFile && selectedNode.IsInstalled;
                tsmiCmsSolveConflicts.Enabled = ModRegister.HasConflicts;
            }

            if (tvModSelection.SelectedNodes.Count > 1)
            {
                tsmiCmsDestinationPath.Text = "<" + Messages.MSG_NOT_AVAILABLE + ">";
                tsmiCmsSelectNewDestination.Enabled = false;
                tsmiCmsRedetectDestination.Enabled = false;
                tsmiCmsResetDestination.Visible = false;
                tsmiCmsResetDestinations.Visible = true;
                tsmiCmsOneModOpenFile.Visible = false;
                tsmiCmsOneModOpenFolder.Visible = false;
            }
            else
            {
                tsmiCmsResetDestination.Visible = true;
                tsmiCmsResetDestinations.Visible = false;
            }
        }

        private void cmsModSelectionAllMods_Opened(object sender, EventArgs e)
        {
            tsmiOpenConflictSolver.Enabled = ModRegister.HasConflicts;
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

            vInfo.ModSelectionColumnsInfo = new ModSelectionColumnsInfo(tvModSelection);

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

            if (vInfo.ModSelectionColumnsInfo != null && vInfo.ModSelectionColumnsInfo.Columns.Count > 0)
                vInfo.ModSelectionColumnsInfo.ToTreeViewAdv(tvModSelection);

            for (int i = 0; i < vInfo.ModInfosColumnWidths.Count; ++i)
            {
                var width = vInfo.ModInfosColumnWidths[i];
                if (i < lvModSelection.Columns.Count && width > 0)
                    lvModSelection.Columns[i].Width = width;
            }
        }

        internal void LanguageChanged()
        {
            // translates the controls of the view.
            ControlTranslator.TranslateControls(Localizer.GlobalInstance, this as Control, OptionsController.SelectedLanguage);

            // translate columns of ModSelection TreeView
            List<NamedTreeColumn> columns = new List<NamedTreeColumn>();
            foreach (NamedTreeColumn column in tvModSelection.Columns)
            {
                var newColData = ModSelectionColumnsInfo.GetColumn(column.Name);
                if (newColData != null)
                    column.Header = newColData.Header;
            }
        }

        internal void SortColumn(NamedTreeColumn column)
        {
            if (column == null)
                return;

            List<ModNode> nodes = null;
            switch (column.Name)
            {
                case ModSelectionColumnsInfo.COLUMNMOD:
                    nodes = ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Cast<ModNode>().ToList();
                    nodes.Sort(delegate(ModNode node1, ModNode node2)
                        {
                            if (column.SortOrder == SortOrder.Ascending)
                                return node1.Text.CompareTo(node2.Text);
                            else
                                return node2.Text.CompareTo(node1.Text);
                        });

                    ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Clear();
                    foreach (ModNode modNode in nodes)
                        ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Add(modNode);

                    break;

                default:
                    nodes = ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Cast<ModNode>().ToList();
                    nodes.Sort(delegate(ModNode node1, ModNode node2)
                        {
                            NodeControl nodeControl = null;
                            foreach (NodeControl control in tvModSelection.NodeControls)
                            {
                                if (control.ParentColumn == column)
                                {
                                    nodeControl = control;
                                    break;
                                }
                            }

                            string propName = nodeControl.GetType().GetProperty("DataPropertyName").GetValue(nodeControl, null).ToString();

                            object obj1 = node1.GetType().GetProperty(propName).GetValue(node1, null);
                            object obj2 = node2.GetType().GetProperty(propName).GetValue(node2, null);
                            string value1 = (obj1 == null) ? string.Empty : obj1.ToString();
                            string value2 = (obj2 == null) ? string.Empty : obj2.ToString();

                            if (column.SortOrder == SortOrder.Ascending)
                                return value1.CompareTo(value2);
                            else
                                return value2.CompareTo(value1);
                        });

                    ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Clear();
                    foreach (ModNode modNode in nodes)
                        ((ModSelectionTreeModel)tvModSelection.Model).Nodes.Add(modNode);

                    break;
            }

            InvalidateView();
        }

        private void OpenFile(ModNode node)
        {
            if (node != null && node.IsFile)
                ModSelectionController.OpenTextDisplayer(node);
        }

        private void OpenFolder(ModNode node)
        {
            if (node != null && !node.IsFile && node.IsInstalled)
                OptionsController.OpenFolder(KSPPathHelper.GetAbsolutePath(SelectedNode.Destination));
        }
    }
}
