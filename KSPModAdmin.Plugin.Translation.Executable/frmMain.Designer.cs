namespace KSPModAdmin.Plugin.Translation.Executable
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
            this.ucTranslationView1 = new KSPModAdmin.Plugin.Translation.ucTranslationView();
            this.SuspendLayout();
            // 
            // ucTranslationView1
            // 
            this.ucTranslationView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTranslationView1.Location = new System.Drawing.Point(0, 0);
            this.ucTranslationView1.MinimumSize = new System.Drawing.Size(450, 400);
            this.ucTranslationView1.Name = "ucTranslationView1";
            this.ucTranslationView1.Size = new System.Drawing.Size(687, 471);
            this.ucTranslationView1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 471);
            this.Controls.Add(this.ucTranslationView1);
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KSP Mod Admin Translation Tool";
            this.ResumeLayout(false);

        }

        #endregion

        private Plugin.Translation.ucTranslationView ucTranslationView1;
    }
}

