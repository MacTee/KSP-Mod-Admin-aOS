using System.Collections.Generic;
using KSPModAdmin.Core.Utils.Localization;

namespace KSPModAdmin.Core.Views
{
    partial class frmMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageModSelection = new System.Windows.Forms.TabPage();
            this.ucModSelection1 = new KSPModAdmin.Core.Views.ucModSelection();
            this.ucKSPStartup1 = new KSPModAdmin.Core.Views.ucKSPStartup();
            this.tabPageOptions = new System.Windows.Forms.TabPage();
            this.ucOptions1 = new KSPModAdmin.Core.Views.ucOptions();
            this.ilMain = new System.Windows.Forms.ImageList(this.components);
            this.lblKSPPath = new System.Windows.Forms.Label();
            this.cbKSPPath = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbMessages = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPageModSelection.SuspendLayout();
            this.tabPageOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageModSelection);
            this.tabControl1.Controls.Add(this.tabPageOptions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ImageList = this.ilMain;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(689, 540);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageModSelection
            // 
            this.tabPageModSelection.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageModSelection.Controls.Add(this.ucModSelection1);
            this.tabPageModSelection.Controls.Add(this.ucKSPStartup1);
            this.tabPageModSelection.ImageIndex = 0;
            this.tabPageModSelection.Location = new System.Drawing.Point(4, 23);
            this.tabPageModSelection.Name = "tabPageModSelection";
            this.tabPageModSelection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageModSelection.Size = new System.Drawing.Size(681, 513);
            this.tabPageModSelection.TabIndex = 0;
            this.tabPageModSelection.Text = "Mods";
            // 
            // ucModSelection1
            // 
            this.ucModSelection1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucModSelection1.Location = new System.Drawing.Point(3, 0);
            this.ucModSelection1.Name = "ucModSelection1";
            this.ucModSelection1.Size = new System.Drawing.Size(675, 442);
            this.ucModSelection1.TabIndex = 1;
            // 
            // ucKSPStartup1
            // 
            this.ucKSPStartup1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucKSPStartup1.Location = new System.Drawing.Point(3, 443);
            this.ucKSPStartup1.Name = "ucKSPStartup1";
            this.ucKSPStartup1.Size = new System.Drawing.Size(675, 67);
            this.ucKSPStartup1.TabIndex = 1;
            // 
            // tabPageOptions
            // 
            this.tabPageOptions.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageOptions.Controls.Add(this.ucOptions1);
            this.tabPageOptions.ImageIndex = 1;
            this.tabPageOptions.Location = new System.Drawing.Point(4, 23);
            this.tabPageOptions.Name = "tabPageOptions";
            this.tabPageOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOptions.Size = new System.Drawing.Size(681, 513);
            this.tabPageOptions.TabIndex = 1;
            this.tabPageOptions.Text = "Options";
            // 
            // ucOptions1
            // 
            this.ucOptions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOptions1.Location = new System.Drawing.Point(3, 3);
            this.ucOptions1.Name = "ucOptions1";
            this.ucOptions1.Size = new System.Drawing.Size(675, 507);
            this.ucOptions1.TabIndex = 0;
            // 
            // ilMain
            // 
            this.ilMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilMain.ImageStream")));
            this.ilMain.TransparentColor = System.Drawing.Color.Transparent;
            this.ilMain.Images.SetKeyName(0, "components.png");
            this.ilMain.Images.SetKeyName(1, "gears.png");
            // 
            // lblKSPPath
            // 
            this.lblKSPPath.AutoSize = true;
            this.lblKSPPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKSPPath.Location = new System.Drawing.Point(3, 0);
            this.lblKSPPath.Name = "lblKSPPath";
            this.lblKSPPath.Size = new System.Drawing.Size(84, 26);
            this.lblKSPPath.TabIndex = 0;
            this.lblKSPPath.Text = "KSP install path:";
            this.lblKSPPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbKSPPath
            // 
            this.cbKSPPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbKSPPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKSPPath.FormattingEnabled = true;
            this.cbKSPPath.Location = new System.Drawing.Point(93, 3);
            this.cbKSPPath.Name = "cbKSPPath";
            this.cbKSPPath.Size = new System.Drawing.Size(579, 21);
            this.cbKSPPath.TabIndex = 1;
            this.ttMain.SetToolTip(this.cbKSPPath, "Chose the KSP install path to perform actions on.");
            this.cbKSPPath.SelectedIndexChanged += new System.EventHandler(this.cbKSPPath_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Location = new System.Drawing.Point(0, 33);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tbMessages);
            this.splitContainer2.Panel2MinSize = 32;
            this.splitContainer2.Size = new System.Drawing.Size(689, 579);
            this.splitContainer2.SplitterDistance = 540;
            this.splitContainer2.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 540);
            this.panel1.TabIndex = 0;
            // 
            // tbMessages
            // 
            this.tbMessages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMessages.Location = new System.Drawing.Point(0, 0);
            this.tbMessages.Multiline = true;
            this.tbMessages.Name = "tbMessages";
            this.tbMessages.ReadOnly = true;
            this.tbMessages.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbMessages.Size = new System.Drawing.Size(689, 35);
            this.tbMessages.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.cbKSPPath, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblKSPPath, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(675, 26);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 612);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.splitContainer2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(705, 650);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "KSP Mod Admin AnyOS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageModSelection.ResumeLayout(false);
            this.tabPageOptions.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageModSelection;
        private System.Windows.Forms.Label lblKSPPath;
        internal System.Windows.Forms.ComboBox cbKSPPath;
        private System.Windows.Forms.TabPage tabPageOptions;
        private System.Windows.Forms.ImageList ilMain;
        private ucModSelection ucModSelection1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private ucOptions ucOptions1;
        public System.Windows.Forms.TextBox tbMessages;
        internal ucKSPStartup ucKSPStartup1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolTip ttMain;
    }
}

