namespace KSPModAdmin.Plugin.PartsAndCraftsTab.Views
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
            this.tsParts = new System.Windows.Forms.ToolStrip();
            this.tsbPartsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPartsRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPartsEdit = new System.Windows.Forms.ToolStripButton();
            this.tslPartsProcessing = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.lblPartsFilterCategory = new System.Windows.Forms.ToolStripLabel();
            this.cbCategoryFilter = new System.Windows.Forms.ToolStripComboBox();
            this.lblPartsFilterMod = new System.Windows.Forms.ToolStripLabel();
            this.cbModFilter = new System.Windows.Forms.ToolStripComboBox();
            this.tvParts = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.cmsParts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPartsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPartsRemovePart = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiPartsEditPart = new System.Windows.Forms.ToolStripMenuItem();
            this.tsParts.SuspendLayout();
            this.cmsParts.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPartsCount
            // 
            this.lblPartsCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblPartsCount.AutoSize = true;
            this.lblPartsCount.Location = new System.Drawing.Point(3, 426);
            this.lblPartsCount.Name = "lblPartsCount";
            this.lblPartsCount.Size = new System.Drawing.Size(47, 13);
            this.lblPartsCount.TabIndex = 26;
            this.lblPartsCount.Text = "Count: 0";
            // 
            // tsParts
            // 
            this.tsParts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPartsRefresh,
            this.toolStripSeparator1,
            this.tsbPartsRemove,
            this.toolStripSeparator2,
            this.tsbPartsEdit,
            this.tslPartsProcessing,
            this.toolStripSeparator5,
            this.lblPartsFilterCategory,
            this.cbCategoryFilter,
            this.lblPartsFilterMod,
            this.cbModFilter});
            this.tsParts.Location = new System.Drawing.Point(0, 0);
            this.tsParts.MinimumSize = new System.Drawing.Size(575, 39);
            this.tsParts.Name = "tsParts";
            this.tsParts.Size = new System.Drawing.Size(675, 39);
            this.tsParts.TabIndex = 27;
            this.tsParts.Text = "toolStrip1";
            // 
            // tsbPartsRefresh
            // 
            this.tsbPartsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsRefresh.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.refresh_24x24;
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
            this.tsbPartsRemove.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.brick_delete_24x24;
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
            this.tsbPartsEdit.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.brick_edit_24x24;
            this.tsbPartsEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsEdit.Name = "tsbPartsEdit";
            this.tsbPartsEdit.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsEdit.Text = "toolStripButton3";
            this.tsbPartsEdit.Click += new System.EventHandler(this.tsbPartsEdit_Click);
            // 
            // tslPartsProcessing
            // 
            this.tslPartsProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslPartsProcessing.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.loader;
            this.tslPartsProcessing.Name = "tslPartsProcessing";
            this.tslPartsProcessing.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tslPartsProcessing.Size = new System.Drawing.Size(16, 28);
            this.tslPartsProcessing.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tslPartsProcessing.Visible = false;
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 31);
            // 
            // lblPartsFilterCategory
            // 
            this.lblPartsFilterCategory.Name = "lblPartsFilterCategory";
            this.lblPartsFilterCategory.Size = new System.Drawing.Size(58, 28);
            this.lblPartsFilterCategory.Text = "Category:";
            // 
            // cbCategoryFilter
            // 
            this.cbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategoryFilter.Items.AddRange(new object[] {
            "All",
            "Pods",
            "Propulsion",
            "Control",
            "Structural",
            "Aero",
            "Utility",
            "Science"});
            this.cbCategoryFilter.Name = "cbCategoryFilter";
            this.cbCategoryFilter.Size = new System.Drawing.Size(121, 39);
            this.cbCategoryFilter.DropDown += new System.EventHandler(this.Filter_DropDown);
            this.cbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
            // 
            // lblPartsFilterMod
            // 
            this.lblPartsFilterMod.Name = "lblPartsFilterMod";
            this.lblPartsFilterMod.Size = new System.Drawing.Size(35, 28);
            this.lblPartsFilterMod.Text = "Mod:";
            // 
            // cbModFilter
            // 
            this.cbModFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModFilter.Items.AddRange(new object[] {
            "All",
            "Squad"});
            this.cbModFilter.Name = "cbModFilter";
            this.cbModFilter.Size = new System.Drawing.Size(300, 39);
            this.cbModFilter.DropDown += new System.EventHandler(this.Filter_DropDown);
            this.cbModFilter.SelectedIndexChanged += new System.EventHandler(this.Filter_SelectedIndexChanged);
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
            this.tvParts.Location = new System.Drawing.Point(0, 42);
            this.tvParts.Model = null;
            this.tvParts.Name = "tvParts";
            this.tvParts.SelectedNode = null;
            this.tvParts.Size = new System.Drawing.Size(675, 381);
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
            this.tsmiPartsEditPart});
            this.cmsParts.Name = "cmsParts";
            this.cmsParts.Size = new System.Drawing.Size(142, 82);
            this.cmsParts.Opening += new System.ComponentModel.CancelEventHandler(this.cmsParts_Opening);
            // 
            // tsmiPartsRefresh
            // 
            this.tsmiPartsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsmiPartsRefresh.Image")));
            this.tsmiPartsRefresh.Name = "tsmiPartsRefresh";
            this.tsmiPartsRefresh.Size = new System.Drawing.Size(141, 22);
            this.tsmiPartsRefresh.Text = "Refresh";
            this.tsmiPartsRefresh.Click += new System.EventHandler(this.tsbPartsRefresh_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(138, 6);
            // 
            // tsmiPartsRemovePart
            // 
            this.tsmiPartsRemovePart.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.brick_delete;
            this.tsmiPartsRemovePart.Name = "tsmiPartsRemovePart";
            this.tsmiPartsRemovePart.Size = new System.Drawing.Size(141, 22);
            this.tsmiPartsRemovePart.Text = "Remove part";
            this.tsmiPartsRemovePart.Click += new System.EventHandler(this.tsbPartsRemove_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(138, 6);
            // 
            // tsmiPartsEditPart
            // 
            this.tsmiPartsEditPart.Image = global::KSPModAdmin.Plugin.PartsAndCraftsTab.Properties.Resources.brick_edit;
            this.tsmiPartsEditPart.Name = "tsmiPartsEditPart";
            this.tsmiPartsEditPart.Size = new System.Drawing.Size(141, 22);
            this.tsmiPartsEditPart.Text = "Edit Part";
            this.tsmiPartsEditPart.Click += new System.EventHandler(this.tsbPartsEdit_Click);
            // 
            // ucPartsTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvParts);
            this.Controls.Add(this.tsParts);
            this.Controls.Add(this.lblPartsCount);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucPartsTabView";
            this.Size = new System.Drawing.Size(675, 442);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.tsParts.ResumeLayout(false);
            this.tsParts.PerformLayout();
            this.cmsParts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPlugin;
        private System.Windows.Forms.Label lblPartsCount;
        private System.Windows.Forms.ToolStrip tsParts;
        private System.Windows.Forms.ToolStripButton tsbPartsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPartsRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPartsEdit;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv tvParts;
        private System.Windows.Forms.ToolStripLabel tslPartsProcessing;
        private System.Windows.Forms.ContextMenuStrip cmsParts;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsRemovePart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmiPartsEditPart;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripLabel lblPartsFilterCategory;
        private System.Windows.Forms.ToolStripComboBox cbCategoryFilter;
        private System.Windows.Forms.ToolStripLabel lblPartsFilterMod;
        private System.Windows.Forms.ToolStripComboBox cbModFilter;
    }
}
