namespace KSPModAdmin.Plugin.PartsTab.Views
{
    partial class ucPartsTabView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPartsTabView));
            this.ttPlugin = new System.Windows.Forms.ToolTip(this.components);
            this.lblPartsCount = new System.Windows.Forms.Label();
            this.lblPartsFilterMod = new System.Windows.Forms.Label();
            this.lblPartsFilterCategory = new System.Windows.Forms.Label();
            this.cbModFilter = new System.Windows.Forms.ComboBox();
            this.cbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.tsParts = new System.Windows.Forms.ToolStrip();
            this.tsbPartsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPartsRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPartsEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbPartsChangeCategory = new System.Windows.Forms.ToolStripButton();
            this.tslPartsProcessing = new System.Windows.Forms.ToolStripLabel();
            this.tvParts = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.cmsParts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPartsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPartsRemovePart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPartsEditPart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPartsChangeCategory = new System.Windows.Forms.ToolStripMenuItem();
            this.gbPartsFilter = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tsParts.SuspendLayout();
            this.cmsParts.SuspendLayout();
            this.gbPartsFilter.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPartsCount
            // 
            this.lblPartsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPartsCount.AutoSize = true;
            this.lblPartsCount.Location = new System.Drawing.Point(3, 429);
            this.lblPartsCount.Name = "lblPartsCount";
            this.lblPartsCount.Size = new System.Drawing.Size(47, 13);
            this.lblPartsCount.TabIndex = 26;
            this.lblPartsCount.Text = "Count: 0";
            // 
            // lblPartsFilterMod
            // 
            this.lblPartsFilterMod.AutoSize = true;
            this.lblPartsFilterMod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPartsFilterMod.Location = new System.Drawing.Point(266, 0);
            this.lblPartsFilterMod.Name = "lblPartsFilterMod";
            this.lblPartsFilterMod.Size = new System.Drawing.Size(31, 27);
            this.lblPartsFilterMod.TabIndex = 23;
            this.lblPartsFilterMod.Text = "Mod:";
            this.lblPartsFilterMod.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPartsFilterCategory
            // 
            this.lblPartsFilterCategory.AutoSize = true;
            this.lblPartsFilterCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPartsFilterCategory.Location = new System.Drawing.Point(3, 0);
            this.lblPartsFilterCategory.Name = "lblPartsFilterCategory";
            this.lblPartsFilterCategory.Size = new System.Drawing.Size(52, 27);
            this.lblPartsFilterCategory.TabIndex = 24;
            this.lblPartsFilterCategory.Text = "Category:";
            this.lblPartsFilterCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbModFilter
            // 
            this.cbModFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModFilter.FormattingEnabled = true;
            this.cbModFilter.Items.AddRange(new object[] {
            "All",
            "Squad"});
            this.cbModFilter.Location = new System.Drawing.Point(303, 3);
            this.cbModFilter.Name = "cbModFilter";
            this.cbModFilter.Size = new System.Drawing.Size(294, 21);
            this.cbModFilter.TabIndex = 21;
            this.cbModFilter.DropDown += new System.EventHandler(this.Filter_DropDown);
            this.cbModFilter.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // cbCategoryFilter
            // 
            this.cbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoryFilter.FormattingEnabled = true;
            this.cbCategoryFilter.Items.AddRange(new object[] {
            "All",
            "Pods",
            "Propulsion",
            "Control",
            "Structural",
            "Aero",
            "Utility",
            "Science"});
            this.cbCategoryFilter.Location = new System.Drawing.Point(61, 3);
            this.cbCategoryFilter.Name = "cbCategoryFilter";
            this.cbCategoryFilter.Size = new System.Drawing.Size(194, 21);
            this.cbCategoryFilter.TabIndex = 22;
            this.cbCategoryFilter.DropDown += new System.EventHandler(this.Filter_DropDown);
            this.cbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // tsParts
            // 
            this.tsParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPartsRefresh,
            this.toolStripSeparator1,
            this.tsbPartsRemove,
            this.toolStripSeparator2,
            this.tsbPartsEdit,
            this.tsbPartsChangeCategory,
            this.tslPartsProcessing});
            this.tsParts.Location = new System.Drawing.Point(0, 0);
            this.tsParts.Name = "tsParts";
            this.tsParts.Size = new System.Drawing.Size(675, 31);
            this.tsParts.TabIndex = 27;
            this.tsParts.Text = "toolStrip1";
            // 
            // tsbPartsRefresh
            // 
            this.tsbPartsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsRefresh.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.refresh_24x24;
            this.tsbPartsRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsRefresh.Name = "tsbPartsRefresh";
            this.tsbPartsRefresh.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsRefresh.Text = "toolStripButton1";
            this.tsbPartsRefresh.Click += new System.EventHandler(this.tsbPartsRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbPartsRemove
            // 
            this.tsbPartsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsRemove.Enabled = false;
            this.tsbPartsRemove.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_delete_24x24;
            this.tsbPartsRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsRemove.Name = "tsbPartsRemove";
            this.tsbPartsRemove.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsRemove.Text = "toolStripButton2";
            this.tsbPartsRemove.Click += new System.EventHandler(this.tsbPartsRemove_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbPartsEdit
            // 
            this.tsbPartsEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsEdit.Enabled = false;
            this.tsbPartsEdit.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_edit_24x24;
            this.tsbPartsEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsEdit.Name = "tsbPartsEdit";
            this.tsbPartsEdit.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsEdit.Text = "toolStripButton3";
            this.tsbPartsEdit.Click += new System.EventHandler(this.tsbPartsEdit_Click);
            // 
            // tsbPartsChangeCategory
            // 
            this.tsbPartsChangeCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsChangeCategory.Enabled = false;
            this.tsbPartsChangeCategory.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_replace_24x24;
            this.tsbPartsChangeCategory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsChangeCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsChangeCategory.Name = "tsbPartsChangeCategory";
            this.tsbPartsChangeCategory.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsChangeCategory.Text = "toolStripButton4";
            this.tsbPartsChangeCategory.Click += new System.EventHandler(this.tsbPartsChangeCategory_Click);
            // 
            // tslPartsProcessing
            // 
            this.tslPartsProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslPartsProcessing.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.loader;
            this.tslPartsProcessing.Name = "tslPartsProcessing";
            this.tslPartsProcessing.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tslPartsProcessing.Size = new System.Drawing.Size(16, 28);
            this.tslPartsProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tslPartsProcessing.Visible = false;
            // 
            // tvParts
            // 
            this.tvParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvParts.BackColor = System.Drawing.SystemColors.Window;
            this.tvParts.ContextMenuStrip = this.cmsParts;
            this.tvParts.DefaultToolTipProvider = null;
            this.tvParts.DragDropMarkColor = System.Drawing.Color.Black;
            this.tvParts.LineColor = System.Drawing.SystemColors.ControlDark;
            this.tvParts.Location = new System.Drawing.Point(0, 87);
            this.tvParts.Model = null;
            this.tvParts.Name = "tvParts";
            this.tvParts.SelectedNode = null;
            this.tvParts.Size = new System.Drawing.Size(675, 339);
            this.tvParts.TabIndex = 28;
            this.tvParts.Text = "treeViewAdv1";
            this.tvParts.UseColumns = true;
            this.tvParts.SelectionChanged += new System.EventHandler(this.tvParts_SelectionChanged);
            // 
            // cmsParts
            // 
            this.cmsParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPartsRefresh,
            this.toolStripSeparator3,
            this.tsmiPartsRemovePart,
            this.toolStripSeparator4,
            this.tsmiPartsEditPart,
            this.tsmiPartsChangeCategory});
            this.cmsParts.Name = "cmsParts";
            this.cmsParts.Size = new System.Drawing.Size(165, 126);
            this.cmsParts.Opening += new System.ComponentModel.CancelEventHandler(this.cmsParts_Opening);
            // 
            // tsmiPartsRefresh
            // 
            this.tsmiPartsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPartsRefresh.Image")));
            this.tsmiPartsRefresh.Name = "tsmiPartsRefresh";
            this.tsmiPartsRefresh.Size = new System.Drawing.Size(164, 22);
            this.tsmiPartsRefresh.Text = "Refresh";
            this.tsmiPartsRefresh.Click += new System.EventHandler(this.tsbPartsRefresh_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmiPartsRemovePart
            // 
            this.tsmiPartsRemovePart.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_delete;
            this.tsmiPartsRemovePart.Name = "tsmiPartsRemovePart";
            this.tsmiPartsRemovePart.Size = new System.Drawing.Size(164, 22);
            this.tsmiPartsRemovePart.Text = "Remove part";
            this.tsmiPartsRemovePart.Click += new System.EventHandler(this.tsbPartsRemove_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(161, 6);
            // 
            // tsmiPartsEditPart
            // 
            this.tsmiPartsEditPart.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_edit;
            this.tsmiPartsEditPart.Name = "tsmiPartsEditPart";
            this.tsmiPartsEditPart.Size = new System.Drawing.Size(164, 22);
            this.tsmiPartsEditPart.Text = "Edit Part";
            this.tsmiPartsEditPart.Click += new System.EventHandler(this.tsbPartsEdit_Click);
            // 
            // tsmiPartsChangeCategory
            // 
            this.tsmiPartsChangeCategory.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_replace_24x24;
            this.tsmiPartsChangeCategory.Name = "tsmiPartsChangeCategory";
            this.tsmiPartsChangeCategory.Size = new System.Drawing.Size(164, 22);
            this.tsmiPartsChangeCategory.Text = "Change category";
            this.tsmiPartsChangeCategory.Click += new System.EventHandler(this.tsbPartsChangeCategory_Click);
            // 
            // gbPartsFilter
            // 
            this.gbPartsFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPartsFilter.Controls.Add(this.tableLayoutPanel1);
            this.gbPartsFilter.Location = new System.Drawing.Point(3, 29);
            this.gbPartsFilter.Name = "gbPartsFilter";
            this.gbPartsFilter.Size = new System.Drawing.Size(669, 54);
            this.gbPartsFilter.TabIndex = 29;
            this.gbPartsFilter.TabStop = false;
            this.gbPartsFilter.Text = "Filter:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblPartsFilterCategory, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbCategoryFilter, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblPartsFilterMod, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.cbModFilter, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(657, 27);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ucPartsTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvParts);
            this.Controls.Add(this.tsParts);
            this.Controls.Add(this.lblPartsCount);
            this.Controls.Add(this.gbPartsFilter);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucPartsTabView";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.tsParts.ResumeLayout(false);
            this.tsParts.PerformLayout();
            this.cmsParts.ResumeLayout(false);
            this.gbPartsFilter.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPlugin;
        private System.Windows.Forms.Label lblPartsCount;
        private System.Windows.Forms.Label lblPartsFilterMod;
        private System.Windows.Forms.Label lblPartsFilterCategory;
        private System.Windows.Forms.ComboBox cbModFilter;
        private System.Windows.Forms.ComboBox cbCategoryFilter;
        private System.Windows.Forms.ToolStrip tsParts;
        private System.Windows.Forms.ToolStripButton tsbPartsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPartsRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPartsEdit;
        private System.Windows.Forms.ToolStripButton tsbPartsChangeCategory;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvParts;
        private System.Windows.Forms.GroupBox gbPartsFilter;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripLabel tslPartsProcessing;
        private System.Windows.Forms.ContextMenuStrip cmsParts;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsRemovePart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsEditPart;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsChangeCategory;
    }
}
