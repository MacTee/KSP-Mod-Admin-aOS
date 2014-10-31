namespace KSPModAdmin.Core.Views
{
    partial class frmWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmWelcome));
            this.btnFinish = new System.Windows.Forms.Button();
            this.tbKSPPath = new System.Windows.Forms.TextBox();
            this.btnSelectFolder = new System.Windows.Forms.Button();
            this.lblWelcomeAuthor = new System.Windows.Forms.Label();
            this.lblWelcomeStep1 = new System.Windows.Forms.Label();
            this.lblWelcomeTitle = new System.Windows.Forms.Label();
            this.lblWelcomeNote = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.ttWelcome = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnFinish
            // 
            this.btnFinish.Enabled = false;
            this.btnFinish.Location = new System.Drawing.Point(334, 193);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(75, 23);
            this.btnFinish.TabIndex = 1;
            this.btnFinish.Text = "Finish";
            this.ttWelcome.SetToolTip(this.btnFinish, "Finish configuration and go to KSP Mod Admin.");
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // tbKSPPath
            // 
            this.tbKSPPath.Location = new System.Drawing.Point(15, 127);
            this.tbKSPPath.Name = "tbKSPPath";
            this.tbKSPPath.ReadOnly = true;
            this.tbKSPPath.Size = new System.Drawing.Size(475, 20);
            this.tbKSPPath.TabIndex = 8;
            this.tbKSPPath.TabStop = false;
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Image = global::KSPModAdmin.Core.Properties.Resources.folder_add1;
            this.btnSelectFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSelectFolder.Location = new System.Drawing.Point(186, 93);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(114, 23);
            this.btnSelectFolder.TabIndex = 4;
            this.btnSelectFolder.Text = "Select folder";
            this.btnSelectFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ttWelcome.SetToolTip(this.btnSelectFolder, "Opens the folder select dialog, to select a KSP install folder.");
            this.btnSelectFolder.UseVisualStyleBackColor = true;
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // lblWelcomeAuthor
            // 
            this.lblWelcomeAuthor.AutoSize = true;
            this.lblWelcomeAuthor.Location = new System.Drawing.Point(192, 33);
            this.lblWelcomeAuthor.Name = "lblWelcomeAuthor";
            this.lblWelcomeAuthor.Size = new System.Drawing.Size(98, 13);
            this.lblWelcomeAuthor.TabIndex = 6;
            this.lblWelcomeAuthor.Text = "by Bastian Heinrich";
            // 
            // lblWelcomeStep1
            // 
            this.lblWelcomeStep1.AutoSize = true;
            this.lblWelcomeStep1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeStep1.Location = new System.Drawing.Point(124, 65);
            this.lblWelcomeStep1.Name = "lblWelcomeStep1";
            this.lblWelcomeStep1.Size = new System.Drawing.Size(239, 17);
            this.lblWelcomeStep1.TabIndex = 7;
            this.lblWelcomeStep1.Text = "Please select your KSP install folder.";
            // 
            // lblWelcomeTitle
            // 
            this.lblWelcomeTitle.AutoSize = true;
            this.lblWelcomeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeTitle.Location = new System.Drawing.Point(103, 9);
            this.lblWelcomeTitle.Name = "lblWelcomeTitle";
            this.lblWelcomeTitle.Size = new System.Drawing.Size(279, 24);
            this.lblWelcomeTitle.TabIndex = 5;
            this.lblWelcomeTitle.Text = "Welcome to KSP Mod Admin";
            // 
            // lblWelcomeNote
            // 
            this.lblWelcomeNote.AutoSize = true;
            this.lblWelcomeNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcomeNote.Location = new System.Drawing.Point(23, 167);
            this.lblWelcomeNote.Name = "lblWelcomeNote";
            this.lblWelcomeNote.Size = new System.Drawing.Size(345, 13);
            this.lblWelcomeNote.TabIndex = 7;
            this.lblWelcomeNote.Text = "NOTE: You can add more KSP paths later on the Options -> Paths Tab.";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(415, 193);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.ttWelcome.SetToolTip(this.btnExit, "Exit KSP Mod Admin.");
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 228);
            this.Controls.Add(this.tbKSPPath);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.lblWelcomeAuthor);
            this.Controls.Add(this.lblWelcomeNote);
            this.Controls.Add(this.lblWelcomeStep1);
            this.Controls.Add(this.lblWelcomeTitle);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnFinish);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmWelcome";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "KSP Mod Admin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.TextBox tbKSPPath;
        private System.Windows.Forms.Button btnSelectFolder;
        private System.Windows.Forms.Label lblWelcomeAuthor;
        private System.Windows.Forms.Label lblWelcomeStep1;
        private System.Windows.Forms.Label lblWelcomeTitle;
        private System.Windows.Forms.ToolTip ttWelcome;
        private System.Windows.Forms.Label lblWelcomeNote;
        private System.Windows.Forms.Button btnExit;
    }
}