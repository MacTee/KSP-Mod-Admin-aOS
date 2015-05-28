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
            this.ttPlugin = new System.Windows.Forms.ToolTip(this.components);
            this.lblCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblBuildingFilter = new System.Windows.Forms.Label();
            this.cbModFilter = new System.Windows.Forms.ComboBox();
            this.cbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbPartsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbPartsRemove = new System.Windows.Forms.ToolStripButton();
            this.tsbPartsEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbPartsChangeCategory = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbProcessing = new System.Windows.Forms.ToolStripButton();
            this.treeViewAdv1 = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(3, 387);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(47, 13);
            this.lblCount.TabIndex = 26;
            this.lblCount.Text = "Count: 0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 23;
            this.label1.Text = "Mod filter:";
            // 
            // lblBuildingFilter
            // 
            this.lblBuildingFilter.AutoSize = true;
            this.lblBuildingFilter.Location = new System.Drawing.Point(5, 37);
            this.lblBuildingFilter.Name = "lblBuildingFilter";
            this.lblBuildingFilter.Size = new System.Drawing.Size(74, 13);
            this.lblBuildingFilter.TabIndex = 24;
            this.lblBuildingFilter.Text = "Category filter:";
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
            this.cbModFilter.Location = new System.Drawing.Point(85, 61);
            this.cbModFilter.Name = "cbModFilter";
            this.cbModFilter.Size = new System.Drawing.Size(348, 21);
            this.cbModFilter.TabIndex = 21;
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
            this.cbCategoryFilter.Location = new System.Drawing.Point(85, 34);
            this.cbCategoryFilter.Name = "cbCategoryFilter";
            this.cbCategoryFilter.Size = new System.Drawing.Size(348, 21);
            this.cbCategoryFilter.TabIndex = 22;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbPartsRefresh,
            this.toolStripSeparator1,
            this.tsbPartsRemove,
            this.toolStripSeparator2,
            this.tsbPartsEdit,
            this.tsbPartsChangeCategory,
            this.tsbProcessing});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(450, 31);
            this.toolStrip1.TabIndex = 27;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbPartsRefresh
            // 
            this.tsbPartsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsRefresh.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.refresh;
            this.tsbPartsRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsRefresh.Name = "tsbPartsRefresh";
            this.tsbPartsRefresh.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsRefresh.Text = "toolStripButton1";
            // 
            // tsbPartsRemove
            // 
            this.tsbPartsRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsRemove.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_delete_24x24;
            this.tsbPartsRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsRemove.Name = "tsbPartsRemove";
            this.tsbPartsRemove.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsRemove.Text = "toolStripButton2";
            // 
            // tsbPartsEdit
            // 
            this.tsbPartsEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsEdit.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_edit_24x24;
            this.tsbPartsEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsEdit.Name = "tsbPartsEdit";
            this.tsbPartsEdit.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsEdit.Text = "toolStripButton3";
            // 
            // tsbPartsChangeCategory
            // 
            this.tsbPartsChangeCategory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPartsChangeCategory.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.brick_folder_24x24;
            this.tsbPartsChangeCategory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbPartsChangeCategory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPartsChangeCategory.Name = "tsbPartsChangeCategory";
            this.tsbPartsChangeCategory.Size = new System.Drawing.Size(28, 28);
            this.tsbPartsChangeCategory.Text = "toolStripButton4";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbProcessing
            // 
            this.tsbProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbProcessing.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbProcessing.Image = global::KSPModAdmin.Plugin.PartsTab.Properties.Resources.loader;
            this.tsbProcessing.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbProcessing.Name = "tsbProcessing";
            this.tsbProcessing.Size = new System.Drawing.Size(23, 28);
            this.tsbProcessing.Text = "toolStripButton5";
            // 
            // treeViewAdv1
            // 
            this.treeViewAdv1.BackColor = System.Drawing.SystemColors.Window;
            this.treeViewAdv1.DefaultToolTipProvider = null;
            this.treeViewAdv1.DragDropMarkColor = System.Drawing.Color.Black;
            this.treeViewAdv1.LineColor = System.Drawing.SystemColors.ControlDark;
            this.treeViewAdv1.Location = new System.Drawing.Point(0, 88);
            this.treeViewAdv1.Model = null;
            this.treeViewAdv1.Name = "treeViewAdv1";
            this.treeViewAdv1.SelectedNode = null;
            this.treeViewAdv1.Size = new System.Drawing.Size(450, 296);
            this.treeViewAdv1.TabIndex = 28;
            this.treeViewAdv1.Text = "treeViewAdv1";
            // 
            // ucPartsTabView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewAdv1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblBuildingFilter);
            this.Controls.Add(this.cbModFilter);
            this.Controls.Add(this.cbCategoryFilter);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucPartsTabView";
            this.Size = new System.Drawing.Size(450, 400);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttPlugin;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblBuildingFilter;
        private System.Windows.Forms.ComboBox cbModFilter;
        private System.Windows.Forms.ComboBox cbCategoryFilter;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbPartsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbPartsRemove;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbPartsEdit;
        private System.Windows.Forms.ToolStripButton tsbPartsChangeCategory;
        private System.Windows.Forms.ToolStripButton tsbProcessing;
        private Core.Utils.Controls.Aga.Controls.Tree.TreeViewAdv treeViewAdv1;
    }
}
