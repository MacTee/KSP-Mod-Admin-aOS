namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Views
{
    partial class ucCraftsTabView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttPlugin = new System.Windows.Forms.ToolTip(this.components);
            this.tsCraftsTab = new System.Windows.Forms.ToolStrip();
            this.tsbCraftsTabRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCraftsTabValidate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCraftsTabRename = new System.Windows.Forms.ToolStripButton();
            this.tsbCraftsTabSwap = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCraftsTabRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslBuildingFilter = new System.Windows.Forms.ToolStripLabel();
            this.cbCraftsTabBuildingFilter = new KSPModAdmin.Core.Utils.Controls.ToolStripSpringComboBox();
            this.tslCraftsTabProcessing = new System.Windows.Forms.ToolStripLabel();
            this.tvCrafts = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.cmsCraftsTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiCraftsTabRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCraftsTabValidateCrafts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCraftsTabRenameCraft = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCraftsTabSwapBuildings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCraftsTabRemoveCraft = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCraftsTabNote = new System.Windows.Forms.Label();
            this.lblCraftsTabCount = new System.Windows.Forms.Label();
            this.tsCraftsTab.SuspendLayout();
            this.cmsCraftsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsCraftsTab
            // 
            this.tsCraftsTab.AutoSize = false;
            this.tsCraftsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCraftsTabRefresh,
            this.toolStripSeparator1,
            this.tsbCraftsTabValidate,
            this.toolStripSeparator4,
            this.tsbCraftsTabRename,
            this.tsbCraftsTabSwap,
            this.toolStripSeparator2,
            this.tsbCraftsTabRemove,
            this.toolStripSeparator3,
            this.tslBuildingFilter,
            this.cbCraftsTabBuildingFilter,
            this.tslCraftsTabProcessing});
            this.tsCraftsTab.Location = new System.Drawing.Point(0, 0);
            this.tsCraftsTab.Name = "tsCraftsTab";
            this.tsCraftsTab.Size = new System.Drawing.Size(675, 39);
            this.tsCraftsTab.TabIndex = 0;
            this.tsCraftsTab.Text = "toolStrip1";
            // 
            // tsbCraftsTabRefresh
            // 
            this.tsbCraftsTabRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCraftsTabRefresh.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.refresh_24x24;
            this.tsbCraftsTabRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCraftsTabRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCraftsTabRefresh.Name = "tsbCraftsTabRefresh";
            this.tsbCraftsTabRefresh.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbCraftsTabRefresh.Size = new System.Drawing.Size(28, 36);
            this.tsbCraftsTabRefresh.Text = "toolStripButton1";
            this.tsbCraftsTabRefresh.Click += new System.EventHandler(this.tsbCraftsTabRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbCraftsTabValidate
            // 
            this.tsbCraftsTabValidate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCraftsTabValidate.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_24x24_checkbox_checked;
            this.tsbCraftsTabValidate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCraftsTabValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCraftsTabValidate.Name = "tsbCraftsTabValidate";
            this.tsbCraftsTabValidate.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbCraftsTabValidate.Size = new System.Drawing.Size(36, 36);
            this.tsbCraftsTabValidate.Text = "toolStripButton3";
            this.tsbCraftsTabValidate.Click += new System.EventHandler(this.tsbCraftsTabValidate_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbCraftsTabRename
            // 
            this.tsbCraftsTabRename.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCraftsTabRename.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_24x24_scroll;
            this.tsbCraftsTabRename.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCraftsTabRename.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCraftsTabRename.Name = "tsbCraftsTabRename";
            this.tsbCraftsTabRename.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbCraftsTabRename.Size = new System.Drawing.Size(36, 36);
            this.tsbCraftsTabRename.Text = "toolStripButton1";
            this.tsbCraftsTabRename.Click += new System.EventHandler(this.tsbCraftsTabRename_Click);
            // 
            // tsbCraftsTabSwap
            // 
            this.tsbCraftsTabSwap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCraftsTabSwap.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_24x24_replace;
            this.tsbCraftsTabSwap.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCraftsTabSwap.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCraftsTabSwap.Name = "tsbCraftsTabSwap";
            this.tsbCraftsTabSwap.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbCraftsTabSwap.Size = new System.Drawing.Size(36, 36);
            this.tsbCraftsTabSwap.Text = "toolStripButton2";
            this.tsbCraftsTabSwap.Click += new System.EventHandler(this.tsbCraftsTabSwap_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbCraftsTabRemove
            // 
            this.tsbCraftsTabRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCraftsTabRemove.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_24x24_delete;
            this.tsbCraftsTabRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbCraftsTabRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCraftsTabRemove.Name = "tsbCraftsTabRemove";
            this.tsbCraftsTabRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsbCraftsTabRemove.Size = new System.Drawing.Size(36, 36);
            this.tsbCraftsTabRemove.Text = "toolStripButton4";
            this.tsbCraftsTabRemove.Click += new System.EventHandler(this.tsbCraftsTabRemove_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // tslBuildingFilter
            // 
            this.tslBuildingFilter.Name = "tslBuildingFilter";
            this.tslBuildingFilter.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tslBuildingFilter.Size = new System.Drawing.Size(83, 36);
            this.tslBuildingFilter.Text = "Building Filter:";
            // 
            // cbCraftsTabBuildingFilter
            // 
            this.cbCraftsTabBuildingFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCraftsTabBuildingFilter.Items.AddRange(new object[] {
            "All",
            "VAB",
            "SPH"});
            this.cbCraftsTabBuildingFilter.Name = "cbCraftsTabBuildingFilter";
            this.cbCraftsTabBuildingFilter.RightMargin = 10;
            this.cbCraftsTabBuildingFilter.Size = new System.Drawing.Size(329, 39);
            this.cbCraftsTabBuildingFilter.SelectedIndexChanged += new System.EventHandler(this.cbCraftsTabBuildingFilter_SelectedIndexChanged);
            // 
            // tslCraftsTabProcessing
            // 
            this.tslCraftsTabProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslCraftsTabProcessing.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.loader;
            this.tslCraftsTabProcessing.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tslCraftsTabProcessing.Name = "tslCraftsTabProcessing";
            this.tslCraftsTabProcessing.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tslCraftsTabProcessing.Size = new System.Drawing.Size(16, 36);
            this.tslCraftsTabProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tslCraftsTabProcessing.Visible = false;
            // 
            // tvCrafts
            // 
            this.tvCrafts.AllowColumnSort = true;
            this.tvCrafts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvCrafts.BackColor = System.Drawing.SystemColors.Window;
            this.tvCrafts.ContextMenuStrip = this.cmsCraftsTab;
            this.tvCrafts.DefaultToolTipProvider = null;
            this.tvCrafts.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvCrafts.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvCrafts.Location = new System.Drawing.Point(0, 39);
            this.tvCrafts.Model = null;
            this.tvCrafts.Name = "tvCrafts";
            this.tvCrafts.SelectedNode = null;
            this.tvCrafts.Size = new System.Drawing.Size(675, 381);
            this.tvCrafts.TabIndex = 1;
            this.tvCrafts.Text = "treeViewAdv1";
            this.tvCrafts.UseColumns = true;
            this.tvCrafts.SelectionChanged += new System.EventHandler(this.tvCrafts_SelectionChanged);
            this.tvCrafts.DrawControl += new System.EventHandler<KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.NodeControls.DrawEventArgs>(this.tvCrafts_DrawControl);
            // 
            // cmsCraftsTab
            // 
            this.cmsCraftsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCraftsTabRefresh,
            this.toolStripSeparator5,
            this.tsmiCraftsTabValidateCrafts,
            this.toolStripSeparator6,
            this.tsmiCraftsTabRenameCraft,
            this.tsmiCraftsTabSwapBuildings,
            this.toolStripSeparator7,
            this.tsmiCraftsTabRemoveCraft});
            this.cmsCraftsTab.Name = "cmsCraftsTab";
            this.cmsCraftsTab.Size = new System.Drawing.Size(155, 132);
            this.cmsCraftsTab.Opening += new System.ComponentModel.CancelEventHandler(this.cmsCraftsTab_Opening);
            // 
            // tsmiCraftsTabRefresh
            // 
            this.tsmiCraftsTabRefresh.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.refresh;
            this.tsmiCraftsTabRefresh.Name = "tsmiCraftsTabRefresh";
            this.tsmiCraftsTabRefresh.Size = new System.Drawing.Size(154, 22);
            this.tsmiCraftsTabRefresh.Text = "Refresh";
            this.tsmiCraftsTabRefresh.Click += new System.EventHandler(this.tsbCraftsTabRefresh_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // tsmiCraftsTabValidateCrafts
            // 
            this.tsmiCraftsTabValidateCrafts.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_checkbox_checked;
            this.tsmiCraftsTabValidateCrafts.Name = "tsmiCraftsTabValidateCrafts";
            this.tsmiCraftsTabValidateCrafts.Size = new System.Drawing.Size(154, 22);
            this.tsmiCraftsTabValidateCrafts.Text = "Validate crafts";
            this.tsmiCraftsTabValidateCrafts.Click += new System.EventHandler(this.tsbCraftsTabValidate_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(151, 6);
            // 
            // tsmiCraftsTabRenameCraft
            // 
            this.tsmiCraftsTabRenameCraft.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_scroll;
            this.tsmiCraftsTabRenameCraft.Name = "tsmiCraftsTabRenameCraft";
            this.tsmiCraftsTabRenameCraft.Size = new System.Drawing.Size(154, 22);
            this.tsmiCraftsTabRenameCraft.Text = "Rename craft";
            this.tsmiCraftsTabRenameCraft.Click += new System.EventHandler(this.tsbCraftsTabRename_Click);
            // 
            // tsmiCraftsTabSwapBuildings
            // 
            this.tsmiCraftsTabSwapBuildings.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_replace;
            this.tsmiCraftsTabSwapBuildings.Name = "tsmiCraftsTabSwapBuildings";
            this.tsmiCraftsTabSwapBuildings.Size = new System.Drawing.Size(154, 22);
            this.tsmiCraftsTabSwapBuildings.Text = "Swap buildings";
            this.tsmiCraftsTabSwapBuildings.Click += new System.EventHandler(this.tsbCraftsTabSwap_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(151, 6);
            // 
            // tsmiCraftsTabRemoveCraft
            // 
            this.tsmiCraftsTabRemoveCraft.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.airplane_delete;
            this.tsmiCraftsTabRemoveCraft.Name = "tsmiCraftsTabRemoveCraft";
            this.tsmiCraftsTabRemoveCraft.Size = new System.Drawing.Size(154, 22);
            this.tsmiCraftsTabRemoveCraft.Text = "Remove craft";
            this.tsmiCraftsTabRemoveCraft.Click += new System.EventHandler(this.tsbCraftsTabRemove_Click);
            // 
            // lblCraftsTabNote
            // 
            this.lblCraftsTabNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCraftsTabNote.Location = new System.Drawing.Point(179, 426);
            this.lblCraftsTabNote.Name = "lblCraftsTabNote";
            this.lblCraftsTabNote.Size = new System.Drawing.Size(493, 13);
            this.lblCraftsTabNote.TabIndex = 4;
            this.lblCraftsTabNote.Text = "NOTE: Crafts with invalid parts are red.";
            this.lblCraftsTabNote.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCraftsTabCount
            // 
            this.lblCraftsTabCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCraftsTabCount.AutoSize = true;
            this.lblCraftsTabCount.Location = new System.Drawing.Point(3, 426);
            this.lblCraftsTabCount.Name = "lblCraftsTabCount";
            this.lblCraftsTabCount.Size = new System.Drawing.Size(47, 13);
            this.lblCraftsTabCount.TabIndex = 5;
            this.lblCraftsTabCount.Text = "Count: 0";
            // 
            // ucCraftsTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCraftsTabNote);
            this.Controls.Add(this.lblCraftsTabCount);
            this.Controls.Add(this.tvCrafts);
            this.Controls.Add(this.tsCraftsTab);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucCraftsTabView";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.tsCraftsTab.ResumeLayout(false);
            this.tsCraftsTab.PerformLayout();
            this.cmsCraftsTab.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPlugin;
        private System.Windows.Forms.ToolStrip tsCraftsTab;
        private System.Windows.Forms.ToolStripButton tsbCraftsTabRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvCrafts;
        private System.Windows.Forms.ToolStripButton tsbCraftsTabRename;
        private System.Windows.Forms.ToolStripButton tsbCraftsTabSwap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbCraftsTabValidate;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbCraftsTabRemove;
        private System.Windows.Forms.Label lblCraftsTabNote;
        private System.Windows.Forms.Label lblCraftsTabCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel tslBuildingFilter;
        private Core.Utils.Controls.ToolStripSpringComboBox cbCraftsTabBuildingFilter;
        private System.Windows.Forms.ToolStripLabel tslCraftsTabProcessing;
        private System.Windows.Forms.ContextMenuStrip cmsCraftsTab;
        private System.Windows.Forms.ToolStripMenuItem tsmiCraftsTabRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmiCraftsTabValidateCrafts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmiCraftsTabRenameCraft;
        private System.Windows.Forms.ToolStripMenuItem tsmiCraftsTabSwapBuildings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmiCraftsTabRemoveCraft;
    }
}
