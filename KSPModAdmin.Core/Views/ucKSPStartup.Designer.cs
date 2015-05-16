namespace KSPModAdmin.Core.Views
{
    partial class ucKSPStartup
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.rbFullscreen = new System.Windows.Forms.RadioButton();
            this.rbWindowed = new System.Windows.Forms.RadioButton();
            this.cbForceOpenGL = new System.Windows.Forms.CheckBox();
            this.cbBorderlessWin = new System.Windows.Forms.CheckBox();
            this.cbResolutions = new System.Windows.Forms.ComboBox();
            this.cbUse64Bit = new System.Windows.Forms.CheckBox();
            this.ttKSPStartup = new System.Windows.Forms.ToolTip(this.components);
            this.btnLaunchKSP = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.rbFullscreen, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.rbWindowed, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbForceOpenGL, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbBorderlessWin, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbResolutions, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbUse64Bit, 5, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 40);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(671, 26);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // rbFullscreen
            // 
            this.rbFullscreen.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbFullscreen.AutoSize = true;
            this.rbFullscreen.Location = new System.Drawing.Point(50, 4);
            this.rbFullscreen.Name = "rbFullscreen";
            this.rbFullscreen.Size = new System.Drawing.Size(73, 17);
            this.rbFullscreen.TabIndex = 2;
            this.rbFullscreen.TabStop = true;
            this.rbFullscreen.Text = "Fullscreen";
            this.ttKSPStartup.SetToolTip(this.rbFullscreen, "Starts KSP in Fullscreen mode.");
            this.rbFullscreen.UseVisualStyleBackColor = true;
            this.rbFullscreen.CheckedChanged += new System.EventHandler(this.rbFullscreen_CheckedChanged);
            // 
            // rbWindowed
            // 
            this.rbWindowed.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.rbWindowed.AutoSize = true;
            this.rbWindowed.Location = new System.Drawing.Point(129, 4);
            this.rbWindowed.Name = "rbWindowed";
            this.rbWindowed.Size = new System.Drawing.Size(76, 17);
            this.rbWindowed.TabIndex = 3;
            this.rbWindowed.TabStop = true;
            this.rbWindowed.Text = "Windowed";
            this.ttKSPStartup.SetToolTip(this.rbWindowed, "Starts KSP in windowed mode.");
            this.rbWindowed.UseVisualStyleBackColor = true;
            this.rbWindowed.CheckedChanged += new System.EventHandler(this.rbWindowed_CheckedChanged);
            // 
            // cbForceOpenGL
            // 
            this.cbForceOpenGL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbForceOpenGL.AutoSize = true;
            this.cbForceOpenGL.Location = new System.Drawing.Point(211, 6);
            this.cbForceOpenGL.Name = "cbForceOpenGL";
            this.cbForceOpenGL.Size = new System.Drawing.Size(96, 17);
            this.cbForceOpenGL.TabIndex = 14;
            this.cbForceOpenGL.Text = "Force OpenGL";
            this.ttKSPStartup.SetToolTip(this.cbForceOpenGL, "Starts KSP with OpenGL redering mode.");
            this.cbForceOpenGL.UseVisualStyleBackColor = true;
            // 
            // cbBorderlessWin
            // 
            this.cbBorderlessWin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbBorderlessWin.AutoSize = true;
            this.cbBorderlessWin.Enabled = false;
            this.cbBorderlessWin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBorderlessWin.Location = new System.Drawing.Point(313, 6);
            this.cbBorderlessWin.Name = "cbBorderlessWin";
            this.cbBorderlessWin.Size = new System.Drawing.Size(114, 17);
            this.cbBorderlessWin.TabIndex = 4;
            this.cbBorderlessWin.Text = "Borderless window";
            this.ttKSPStartup.SetToolTip(this.cbBorderlessWin, "Starts KSP with a border less window.");
            this.cbBorderlessWin.UseVisualStyleBackColor = true;
            // 
            // cbResolutions
            // 
            this.cbResolutions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbResolutions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolutions.FormattingEnabled = true;
            this.cbResolutions.Location = new System.Drawing.Point(491, 3);
            this.cbResolutions.Name = "cbResolutions";
            this.cbResolutions.Size = new System.Drawing.Size(129, 21);
            this.cbResolutions.TabIndex = 13;
            this.ttKSPStartup.SetToolTip(this.cbResolutions, "Sets the KSP window/screen resolution.");
            this.cbResolutions.SelectedIndexChanged += new System.EventHandler(this.cbResolutions_SelectedIndexChanged);
            // 
            // cbUse64Bit
            // 
            this.cbUse64Bit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbUse64Bit.AutoSize = true;
            this.cbUse64Bit.Location = new System.Drawing.Point(433, 6);
            this.cbUse64Bit.Name = "cbUse64Bit";
            this.cbUse64Bit.Size = new System.Drawing.Size(52, 17);
            this.cbUse64Bit.TabIndex = 15;
            this.cbUse64Bit.Text = "64-bit";
            this.cbUse64Bit.UseVisualStyleBackColor = true;
            // 
            // btnLaunchKSP
            // 
            this.btnLaunchKSP.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLaunchKSP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaunchKSP.Image = global::KSPModAdmin.Core.Properties.Resources.kerbal_24x24;
            this.btnLaunchKSP.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLaunchKSP.Location = new System.Drawing.Point(3, -1);
            this.btnLaunchKSP.Name = "btnLaunchKSP";
            this.btnLaunchKSP.Size = new System.Drawing.Size(665, 40);
            this.btnLaunchKSP.TabIndex = 1;
            this.btnLaunchKSP.Text = "Launch Kerbal Space Program";
            this.btnLaunchKSP.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttKSPStartup.SetToolTip(this.btnLaunchKSP, "Runs the KSP.exe of the selected KSP install path.");
            this.btnLaunchKSP.UseVisualStyleBackColor = true;
            this.btnLaunchKSP.Click += new System.EventHandler(this.btnLunchKSP_Click);
            // 
            // ucKSPStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLaunchKSP);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "ucKSPStartup";
            this.Size = new System.Drawing.Size(671, 66);
            this.Load += new System.EventHandler(this.ucKSPStartup_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton rbFullscreen;
        private System.Windows.Forms.CheckBox cbBorderlessWin;
        private System.Windows.Forms.ComboBox cbResolutions;
        private System.Windows.Forms.RadioButton rbWindowed;
        public System.Windows.Forms.Button btnLaunchKSP;
        private System.Windows.Forms.ToolTip ttKSPStartup;
        private System.Windows.Forms.CheckBox cbForceOpenGL;
		private System.Windows.Forms.CheckBox cbUse64Bit;
    }
}
