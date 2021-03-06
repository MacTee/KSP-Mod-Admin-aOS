﻿namespace KSPModAdmin.Plugin.FlagsTab.Views
{
    partial class ucFlagsView
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
            this.ttFlags = new System.Windows.Forms.ToolTip(this.components);
            this.lvFlags = new KSPModAdmin.Core.Utils.Controls.ListViewAdv();
            this.cmsFlagsTab = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiFlagsTabRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiFlagsTabAddNewFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFlagsTabRemoveFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.ilFlags = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbFlagsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAddFlag = new System.Windows.Forms.ToolStripButton();
            this.tsbRemoveFlag = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tslModFilter = new System.Windows.Forms.ToolStripLabel();
            this.tsscbModFilter = new KSPModAdmin.Core.Utils.Controls.ToolStripSpringComboBox();
            this.tslProcessing = new System.Windows.Forms.ToolStripLabel();
            this.cmsFlagsTab.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvFlags
            // 
            this.lvFlags.ContextMenuStrip = this.cmsFlagsTab;
            this.lvFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFlags.LargeImageList = this.ilFlags;
            this.lvFlags.Location = new System.Drawing.Point(0, 39);
            this.lvFlags.Name = "lvFlags";
            this.lvFlags.Size = new System.Drawing.Size(540, 361);
            this.lvFlags.SmallImageList = this.ilFlags;
            this.lvFlags.TabIndex = 1;
            this.lvFlags.UseCompatibleStateImageBehavior = false;
            this.lvFlags.SelectedIndexChanged += new System.EventHandler(this.lvFlags_SelectedIndexChanged);
            // 
            // cmsFlagsTab
            // 
            this.cmsFlagsTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFlagsTabRefresh,
            this.toolStripSeparator3,
            this.tsmiFlagsTabAddNewFlag,
            this.tsmiFlagsTabRemoveFlag});
            this.cmsFlagsTab.Name = "cmsFlagsTab";
            this.cmsFlagsTab.Size = new System.Drawing.Size(147, 76);
            this.cmsFlagsTab.Opening += new System.ComponentModel.CancelEventHandler(this.cmsFlagsTab_Opening);
            // 
            // tsmiFlagsTabRefresh
            // 
            this.tsmiFlagsTabRefresh.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.refresh;
            this.tsmiFlagsTabRefresh.Name = "tsmiFlagsTabRefresh";
            this.tsmiFlagsTabRefresh.Size = new System.Drawing.Size(146, 22);
            this.tsmiFlagsTabRefresh.Text = "Refresh";
            this.tsmiFlagsTabRefresh.Click += new System.EventHandler(this.tsbFlagsRefresh_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // tsmiFlagsTabAddNewFlag
            // 
            this.tsmiFlagsTabAddNewFlag.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.flag_scotland_add_24x24;
            this.tsmiFlagsTabAddNewFlag.Name = "tsmiFlagsTabAddNewFlag";
            this.tsmiFlagsTabAddNewFlag.Size = new System.Drawing.Size(146, 22);
            this.tsmiFlagsTabAddNewFlag.Text = "Add new Flag";
            this.tsmiFlagsTabAddNewFlag.Click += new System.EventHandler(this.tsbAddFlag_Click);
            // 
            // tsmiFlagsTabRemoveFlag
            // 
            this.tsmiFlagsTabRemoveFlag.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.flag_scotland_delete_24x24;
            this.tsmiFlagsTabRemoveFlag.Name = "tsmiFlagsTabRemoveFlag";
            this.tsmiFlagsTabRemoveFlag.Size = new System.Drawing.Size(146, 22);
            this.tsmiFlagsTabRemoveFlag.Text = "Remove flag";
            this.tsmiFlagsTabRemoveFlag.Click += new System.EventHandler(this.tsbRemoveFlag_Click);
            // 
            // ilFlags
            // 
            this.ilFlags.ColorDepth = System.Windows.Forms.ColorDepth.Depth16Bit;
            this.ilFlags.ImageSize = new System.Drawing.Size(128, 80);
            this.ilFlags.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFlagsRefresh,
            this.toolStripSeparator1,
            this.tsbAddFlag,
            this.tsbRemoveFlag,
            this.toolStripSeparator2,
            this.tslModFilter,
            this.tsscbModFilter,
            this.tslProcessing});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(540, 39);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(540, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbFlagsRefresh
            // 
            this.tsbFlagsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFlagsRefresh.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.refresh;
            this.tsbFlagsRefresh.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbFlagsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFlagsRefresh.Name = "tsbFlagsRefresh";
            this.tsbFlagsRefresh.Size = new System.Drawing.Size(28, 36);
            this.tsbFlagsRefresh.Text = "toolStripButton1";
            this.tsbFlagsRefresh.Click += new System.EventHandler(this.tsbFlagsRefresh_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbAddFlag
            // 
            this.tsbAddFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddFlag.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.flag_scotland_add_24x24;
            this.tsbAddFlag.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbAddFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddFlag.Name = "tsbAddFlag";
            this.tsbAddFlag.Size = new System.Drawing.Size(28, 36);
            this.tsbAddFlag.Text = "toolStripButton2";
            this.tsbAddFlag.ToolTipText = "Add new Flag\r\nOpens a FileSelect dialog to select a png which will be added to th" +
    "e KSP flags.\r\nThe png will be resized to 256x160.";
            this.tsbAddFlag.Click += new System.EventHandler(this.tsbAddFlag_Click);
            // 
            // tsbRemoveFlag
            // 
            this.tsbRemoveFlag.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemoveFlag.Enabled = false;
            this.tsbRemoveFlag.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.flag_scotland_delete_24x24;
            this.tsbRemoveFlag.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbRemoveFlag.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemoveFlag.Name = "tsbRemoveFlag";
            this.tsbRemoveFlag.Size = new System.Drawing.Size(28, 36);
            this.tsbRemoveFlag.Text = "toolStripButton3";
            this.tsbRemoveFlag.ToolTipText = "Remove flag\r\nDeletes and removes the selected flag.";
            this.tsbRemoveFlag.Click += new System.EventHandler(this.tsbRemoveFlag_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // tslModFilter
            // 
            this.tslModFilter.Name = "tslModFilter";
            this.tslModFilter.Size = new System.Drawing.Size(100, 36);
            this.tslModFilter.Text = "Folder/Mod filter:";
            // 
            // tsscbModFilter
            // 
            this.tsscbModFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsscbModFilter.Name = "tsscbModFilter";
            this.tsscbModFilter.RightMargin = 10;
            this.tsscbModFilter.Size = new System.Drawing.Size(277, 39);
            this.tsscbModFilter.ToolTipText = "Flag Filter\r\nSelect a filter to adjust the displayed falg groups.";
            this.tsscbModFilter.SelectedIndexChanged += new System.EventHandler(this.tsscbModFilter_SelectedIndexChanged);
            // 
            // tslProcessing
            // 
            this.tslProcessing.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslProcessing.Image = global::KSPModAdmin.Plugin.FlagsTab.Properties.Resources.loader;
            this.tslProcessing.Name = "tslProcessing";
            this.tslProcessing.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tslProcessing.Size = new System.Drawing.Size(16, 36);
            this.tslProcessing.Visible = false;
            // 
            // ucFlagsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvFlags);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(450, 400);
            this.Name = "ucFlagsView";
            this.Size = new System.Drawing.Size(540, 400);
            this.Load += new System.EventHandler(this.ucPluginView_Load);
            this.cmsFlagsTab.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip ttFlags;
        private KSPModAdmin.Core.Utils.Controls.ListViewAdv lvFlags;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbFlagsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbAddFlag;
        private System.Windows.Forms.ToolStripButton tsbRemoveFlag;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel tslModFilter;
        private Core.Utils.Controls.ToolStripSpringComboBox tsscbModFilter;
        private System.Windows.Forms.ToolStripLabel tslProcessing;
        private System.Windows.Forms.ImageList ilFlags;
        private System.Windows.Forms.ContextMenuStrip cmsFlagsTab;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlagsTabRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlagsTabAddNewFlag;
        private System.Windows.Forms.ToolStripMenuItem tsmiFlagsTabRemoveFlag;
    }
}
