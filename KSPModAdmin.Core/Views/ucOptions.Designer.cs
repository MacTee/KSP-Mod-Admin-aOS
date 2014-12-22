using KSPModAdmin.Core.Utils.Controls;

namespace KSPModAdmin.Core.Views
{
    partial class ucOptions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucOptions));
            this.ilOptions = new System.Windows.Forms.ImageList(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageUpdate = new System.Windows.Forms.TabPage();
            this.gbModUpdate = new System.Windows.Forms.GroupBox();
            this.cbDeleteOldArchive = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.lblModUpdateBehavior = new System.Windows.Forms.Label();
            this.cbModUpdateBehavior = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblUpdateCheckInterval = new System.Windows.Forms.Label();
            this.cbModUpdateInterval = new System.Windows.Forms.ComboBox();
            this.pbCheckModUpdate = new System.Windows.Forms.PictureBox();
            this.btnCheckModUpdates = new System.Windows.Forms.Button();
            this.gbAdminVersion = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblDownloadKspMA = new System.Windows.Forms.Label();
            this.llblAdminDownload = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblPostDownloadAction = new System.Windows.Forms.Label();
            this.cbPostDownloadAction = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblKSPModAdminVersion = new System.Windows.Forms.Label();
            this.pbUp2Date = new System.Windows.Forms.PictureBox();
            this.pbUpdateLoad = new System.Windows.Forms.PictureBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cbVersionCheck = new System.Windows.Forms.CheckBox();
            this.prgBarAdminDownload = new System.Windows.Forms.ProgressBar();
            this.btnOpenDownloads = new System.Windows.Forms.Button();
            this.tabPagePath = new System.Windows.Forms.TabPage();
            this.gbPaths = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel12 = new System.Windows.Forms.TableLayoutPanel();
            this.lblKnownKSPPaths = new System.Windows.Forms.Label();
            this.tbDepth = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.btnAddPath = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSteamSearch = new System.Windows.Forms.Button();
            this.btnKSPFolderSearch = new System.Windows.Forms.Button();
            this.tlpSearchBG = new System.Windows.Forms.TableLayoutPanel();
            this.tlpSearch = new System.Windows.Forms.TableLayoutPanel();
            this.lblLoading = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.cbKSPPath = new System.Windows.Forms.ComboBox();
            this.lblModDownloadPath = new System.Windows.Forms.Label();
            this.tbDownloadPath = new System.Windows.Forms.TextBox();
            this.btnDownloadPath = new System.Windows.Forms.Button();
            this.btnOpenDownloadFolder = new System.Windows.Forms.Button();
            this.btnOpenKSPRoot = new System.Windows.Forms.Button();
            this.lblSelectedKSPPath = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvKnownPaths = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblKSPPathNote = new System.Windows.Forms.Label();
            this.tbNote = new System.Windows.Forms.TextBox();
            this.tabPageMisc = new System.Windows.Forms.TabPage();
            this.gbDestinationDetection = new System.Windows.Forms.GroupBox();
            this.rbDDJustDump = new System.Windows.Forms.RadioButton();
            this.rbDDSmartDestDetection = new System.Windows.Forms.RadioButton();
            this.cbDDCopyToGameData = new System.Windows.Forms.CheckBox();
            this.gbToolTip = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel13 = new System.Windows.Forms.TableLayoutPanel();
            this.lblSecond2 = new System.Windows.Forms.Label();
            this.cbToolTipOnOff = new System.Windows.Forms.CheckBox();
            this.tbToolTipDisplayTime = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.NumericTextBox();
            this.lblToolTipDisplayTime = new System.Windows.Forms.Label();
            this.lblToolTipDelay = new System.Windows.Forms.Label();
            this.lblSecond1 = new System.Windows.Forms.Label();
            this.tbToolTipDelay = new KSPModAdmin.Core.Utils.Controls.Aga.Controls.NumericTextBox();
            this.gbLaguage = new System.Windows.Forms.GroupBox();
            this.cbLanguages = new System.Windows.Forms.ComboBox();
            this.gbNodeColors = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorDestinationDetection = new System.Windows.Forms.Label();
            this.pDestinationDetected = new System.Windows.Forms.Panel();
            this.lblColorRDestinationDetection = new System.Windows.Forms.Label();
            this.tbDestinationDetectedRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorGDestinationDetection = new System.Windows.Forms.Label();
            this.tbDestinationDetectedGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.btnDestinationDetected = new System.Windows.Forms.Button();
            this.lblColorBDestinationDetection = new System.Windows.Forms.Label();
            this.tbDestinationDetectedBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorDestinationMissing = new System.Windows.Forms.Label();
            this.pDestinationMissing = new System.Windows.Forms.Panel();
            this.lblColorRDestinationMissing = new System.Windows.Forms.Label();
            this.tbDestinationMissingRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorGDestinationMissing = new System.Windows.Forms.Label();
            this.tbDestinationMissingGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.btnDestinationMissing = new System.Windows.Forms.Button();
            this.lblColorBDestinationMissing = new System.Windows.Forms.Label();
            this.tbDestinationMissingBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorDestinationConflicts = new System.Windows.Forms.Label();
            this.pDestinationConflict = new System.Windows.Forms.Panel();
            this.lblColorRDestinationConflict = new System.Windows.Forms.Label();
            this.tbDestinationConflictRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorGDestinationConflict = new System.Windows.Forms.Label();
            this.tbDestinationConflictGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.btnDestinationConflict = new System.Windows.Forms.Button();
            this.lblColorBDestinationConflict = new System.Windows.Forms.Label();
            this.tbDestinationConflictBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorModIsInstalled = new System.Windows.Forms.Label();
            this.pModInstalled = new System.Windows.Forms.Panel();
            this.lblColorRModIsInstalled = new System.Windows.Forms.Label();
            this.tbModInstalledRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.btnModInstalled = new System.Windows.Forms.Button();
            this.lblColorGModIsInstalled = new System.Windows.Forms.Label();
            this.tbModInstalledGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorBModIsInstalled = new System.Windows.Forms.Label();
            this.tbModInstalledBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorModArchiveMissing = new System.Windows.Forms.Label();
            this.pModArchiveMissing = new System.Windows.Forms.Panel();
            this.lblColorRModArchiveMissing = new System.Windows.Forms.Label();
            this.btnModArchiveMissing = new System.Windows.Forms.Button();
            this.tbModArchiveMissingRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorGModArchiveMissing = new System.Windows.Forms.Label();
            this.tbModArchiveMissingGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorBModArchiveMissing = new System.Windows.Forms.Label();
            this.tbModArchiveMissingBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.lblColorModOutdated = new System.Windows.Forms.Label();
            this.pModOutdated = new System.Windows.Forms.Panel();
            this.btnModOutdated = new System.Windows.Forms.Button();
            this.lblColorRModOutdated = new System.Windows.Forms.Label();
            this.tbModOutdatedRed = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorGModOutdated = new System.Windows.Forms.Label();
            this.tbModOutdatedGreen = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.lblColorBModOutdated = new System.Windows.Forms.Label();
            this.tbModOutdatedBlue = new KSPModAdmin.Core.Utils.Controls.TextBoxNumeric();
            this.gpModConflictHandling = new System.Windows.Forms.GroupBox();
            this.cbConflictDetectionOnOff = new System.Windows.Forms.CheckBox();
            this.cbShowConflictSolver = new System.Windows.Forms.CheckBox();
            this.ttOptions = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.tabControl2.SuspendLayout();
            this.tabPageUpdate.SuspendLayout();
            this.gbModUpdate.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckModUpdate)).BeginInit();
            this.gbAdminVersion.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp2Date)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdateLoad)).BeginInit();
            this.tabPagePath.SuspendLayout();
            this.gbPaths.SuspendLayout();
            this.tableLayoutPanel12.SuspendLayout();
            this.tlpSearchBG.SuspendLayout();
            this.tlpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageMisc.SuspendLayout();
            this.gbDestinationDetection.SuspendLayout();
            this.gbToolTip.SuspendLayout();
            this.tableLayoutPanel13.SuspendLayout();
            this.gbLaguage.SuspendLayout();
            this.gbNodeColors.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.gpModConflictHandling.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.SuspendLayout();
            // 
            // ilOptions
            // 
            this.ilOptions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilOptions.ImageStream")));
            this.ilOptions.TransparentColor = System.Drawing.Color.Transparent;
            this.ilOptions.Images.SetKeyName(0, "earth_replace.png");
            this.ilOptions.Images.SetKeyName(1, "harddisk.png");
            this.ilOptions.Images.SetKeyName(2, "gears_preferences.png");
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageUpdate);
            this.tabControl2.Controls.Add(this.tabPagePath);
            this.tabControl2.Controls.Add(this.tabPageMisc);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.ImageList = this.ilOptions;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(675, 507);
            this.tabControl2.TabIndex = 1;
            // 
            // tabPageUpdate
            // 
            this.tabPageUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageUpdate.Controls.Add(this.gbModUpdate);
            this.tabPageUpdate.Controls.Add(this.gbAdminVersion);
            this.tabPageUpdate.ImageIndex = 0;
            this.tabPageUpdate.Location = new System.Drawing.Point(4, 23);
            this.tabPageUpdate.Name = "tabPageUpdate";
            this.tabPageUpdate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageUpdate.Size = new System.Drawing.Size(667, 480);
            this.tabPageUpdate.TabIndex = 0;
            this.tabPageUpdate.Text = "Update";
            // 
            // gbModUpdate
            // 
            this.gbModUpdate.Controls.Add(this.cbDeleteOldArchive);
            this.gbModUpdate.Controls.Add(this.tableLayoutPanel5);
            this.gbModUpdate.Controls.Add(this.tableLayoutPanel4);
            this.gbModUpdate.Controls.Add(this.pbCheckModUpdate);
            this.gbModUpdate.Controls.Add(this.btnCheckModUpdates);
            this.gbModUpdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbModUpdate.Location = new System.Drawing.Point(3, 183);
            this.gbModUpdate.Name = "gbModUpdate";
            this.gbModUpdate.Size = new System.Drawing.Size(661, 294);
            this.gbModUpdate.TabIndex = 7;
            this.gbModUpdate.TabStop = false;
            this.gbModUpdate.Text = "Mod updating:";
            // 
            // cbDeleteOldArchive
            // 
            this.cbDeleteOldArchive.AutoSize = true;
            this.cbDeleteOldArchive.Location = new System.Drawing.Point(11, 125);
            this.cbDeleteOldArchive.Name = "cbDeleteOldArchive";
            this.cbDeleteOldArchive.Size = new System.Drawing.Size(172, 17);
            this.cbDeleteOldArchive.TabIndex = 19;
            this.cbDeleteOldArchive.Text = "Delete old archive after update";
            this.ttOptions.SetToolTip(this.cbDeleteOldArchive, "Check this option if the old archive of a mod should be deleted after an update.");
            this.cbDeleteOldArchive.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.lblModUpdateBehavior, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.cbModUpdateBehavior, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(21, 92);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(394, 27);
            this.tableLayoutPanel5.TabIndex = 18;
            // 
            // lblModUpdateBehavior
            // 
            this.lblModUpdateBehavior.AutoSize = true;
            this.lblModUpdateBehavior.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblModUpdateBehavior.Location = new System.Drawing.Point(3, 0);
            this.lblModUpdateBehavior.Name = "lblModUpdateBehavior";
            this.lblModUpdateBehavior.Size = new System.Drawing.Size(144, 27);
            this.lblModUpdateBehavior.TabIndex = 14;
            this.lblModUpdateBehavior.Text = "Mod update behavior:";
            this.lblModUpdateBehavior.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbModUpdateBehavior
            // 
            this.cbModUpdateBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModUpdateBehavior.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModUpdateBehavior.FormattingEnabled = true;
            this.cbModUpdateBehavior.Items.AddRange(new object[] {
            "Remove and Add",
            "Copy destination",
            "Copy checked state",
            "Manually"});
            this.cbModUpdateBehavior.Location = new System.Drawing.Point(153, 3);
            this.cbModUpdateBehavior.Name = "cbModUpdateBehavior";
            this.cbModUpdateBehavior.Size = new System.Drawing.Size(238, 21);
            this.cbModUpdateBehavior.TabIndex = 7;
            this.ttOptions.SetToolTip(this.cbModUpdateBehavior, "Chose the action which should be executed on a mod update.");
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.lblUpdateCheckInterval, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.cbModUpdateInterval, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(21, 59);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(394, 27);
            this.tableLayoutPanel4.TabIndex = 17;
            // 
            // lblUpdateCheckInterval
            // 
            this.lblUpdateCheckInterval.AutoSize = true;
            this.lblUpdateCheckInterval.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblUpdateCheckInterval.Location = new System.Drawing.Point(3, 0);
            this.lblUpdateCheckInterval.Name = "lblUpdateCheckInterval";
            this.lblUpdateCheckInterval.Size = new System.Drawing.Size(144, 27);
            this.lblUpdateCheckInterval.TabIndex = 14;
            this.lblUpdateCheckInterval.Text = "Update check interval:";
            this.lblUpdateCheckInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbModUpdateInterval
            // 
            this.cbModUpdateInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbModUpdateInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbModUpdateInterval.FormattingEnabled = true;
            this.cbModUpdateInterval.Items.AddRange(new object[] {
            "Manualy",
            "On startup",
            "Once a day",
            "Once a week"});
            this.cbModUpdateInterval.Location = new System.Drawing.Point(153, 3);
            this.cbModUpdateInterval.Name = "cbModUpdateInterval";
            this.cbModUpdateInterval.Size = new System.Drawing.Size(238, 21);
            this.cbModUpdateInterval.TabIndex = 6;
            this.ttOptions.SetToolTip(this.cbModUpdateInterval, "Chose the interval on which the mod update check should be started.");
            // 
            // pbCheckModUpdate
            // 
            this.pbCheckModUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbCheckModUpdate.Image = ((System.Drawing.Image)(resources.GetObject("pbCheckModUpdate.Image")));
            this.pbCheckModUpdate.Location = new System.Drawing.Point(630, 27);
            this.pbCheckModUpdate.Name = "pbCheckModUpdate";
            this.pbCheckModUpdate.Size = new System.Drawing.Size(20, 20);
            this.pbCheckModUpdate.TabIndex = 16;
            this.pbCheckModUpdate.TabStop = false;
            this.ttOptions.SetToolTip(this.pbCheckModUpdate, "working...");
            this.pbCheckModUpdate.Visible = false;
            // 
            // btnCheckModUpdates
            // 
            this.btnCheckModUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckModUpdates.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckModUpdates.Location = new System.Drawing.Point(6, 19);
            this.btnCheckModUpdates.Name = "btnCheckModUpdates";
            this.btnCheckModUpdates.Size = new System.Drawing.Size(613, 35);
            this.btnCheckModUpdates.TabIndex = 5;
            this.btnCheckModUpdates.Text = "Check for mod updates";
            this.ttOptions.SetToolTip(this.btnCheckModUpdates, "Check for mod updates\r\nStarts the update check for all mods from the ModSelection" +
        ".");
            this.btnCheckModUpdates.UseVisualStyleBackColor = true;
            this.btnCheckModUpdates.Click += new System.EventHandler(this.btnCheckModUpdates_Click);
            // 
            // gbAdminVersion
            // 
            this.gbAdminVersion.Controls.Add(this.tableLayoutPanel3);
            this.gbAdminVersion.Controls.Add(this.tableLayoutPanel2);
            this.gbAdminVersion.Controls.Add(this.tableLayoutPanel1);
            this.gbAdminVersion.Controls.Add(this.pbUpdateLoad);
            this.gbAdminVersion.Controls.Add(this.btnUpdate);
            this.gbAdminVersion.Controls.Add(this.cbVersionCheck);
            this.gbAdminVersion.Controls.Add(this.prgBarAdminDownload);
            this.gbAdminVersion.Controls.Add(this.btnOpenDownloads);
            this.gbAdminVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbAdminVersion.Location = new System.Drawing.Point(3, 3);
            this.gbAdminVersion.Name = "gbAdminVersion";
            this.gbAdminVersion.Size = new System.Drawing.Size(661, 180);
            this.gbAdminVersion.TabIndex = 6;
            this.gbAdminVersion.TabStop = false;
            this.gbAdminVersion.Text = "Update KSP Mod Admin:";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.lblDownloadKspMA, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.llblAdminDownload, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(21, 152);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(394, 16);
            this.tableLayoutPanel3.TabIndex = 17;
            // 
            // lblDownloadKspMA
            // 
            this.lblDownloadKspMA.AutoSize = true;
            this.lblDownloadKspMA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDownloadKspMA.Location = new System.Drawing.Point(3, 0);
            this.lblDownloadKspMA.Name = "lblDownloadKspMA";
            this.lblDownloadKspMA.Size = new System.Drawing.Size(144, 16);
            this.lblDownloadKspMA.TabIndex = 0;
            this.lblDownloadKspMA.Text = "Download:";
            this.lblDownloadKspMA.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // llblAdminDownload
            // 
            this.llblAdminDownload.AutoSize = true;
            this.llblAdminDownload.Location = new System.Drawing.Point(153, 0);
            this.llblAdminDownload.Name = "llblAdminDownload";
            this.llblAdminDownload.Size = new System.Drawing.Size(133, 13);
            this.llblAdminDownload.TabIndex = 4;
            this.llblAdminDownload.TabStop = true;
            this.llblAdminDownload.Text = "KSP Mod Admin-v1.0.0.zip";
            this.ttOptions.SetToolTip(this.llblAdminDownload, "Click to start download of the new KSP Mod Admin manually.");
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.lblPostDownloadAction, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.cbPostDownloadAction, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(21, 120);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(394, 26);
            this.tableLayoutPanel2.TabIndex = 16;
            // 
            // lblPostDownloadAction
            // 
            this.lblPostDownloadAction.AutoSize = true;
            this.lblPostDownloadAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPostDownloadAction.Location = new System.Drawing.Point(3, 0);
            this.lblPostDownloadAction.Name = "lblPostDownloadAction";
            this.lblPostDownloadAction.Size = new System.Drawing.Size(144, 26);
            this.lblPostDownloadAction.TabIndex = 14;
            this.lblPostDownloadAction.Text = "Post download action:";
            this.lblPostDownloadAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbPostDownloadAction
            // 
            this.cbPostDownloadAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbPostDownloadAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPostDownloadAction.FormattingEnabled = true;
            this.cbPostDownloadAction.Items.AddRange(new object[] {
            "Ignore finished download",
            "Ask for auto install",
            "Auto install after download"});
            this.cbPostDownloadAction.Location = new System.Drawing.Point(153, 3);
            this.cbPostDownloadAction.Name = "cbPostDownloadAction";
            this.cbPostDownloadAction.Size = new System.Drawing.Size(238, 21);
            this.cbPostDownloadAction.TabIndex = 3;
            this.ttOptions.SetToolTip(this.cbPostDownloadAction, "Chose the action that should be executed after a update.");
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblKSPModAdminVersion, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbUp2Date, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(21, 21);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(360, 21);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // lblKSPModAdminVersion
            // 
            this.lblKSPModAdminVersion.AutoSize = true;
            this.lblKSPModAdminVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKSPModAdminVersion.Location = new System.Drawing.Point(3, 0);
            this.lblKSPModAdminVersion.Name = "lblKSPModAdminVersion";
            this.lblKSPModAdminVersion.Size = new System.Drawing.Size(144, 21);
            this.lblKSPModAdminVersion.TabIndex = 0;
            this.lblKSPModAdminVersion.Text = "Current version: 2.0.0";
            this.lblKSPModAdminVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pbUp2Date
            // 
            this.pbUp2Date.Image = ((System.Drawing.Image)(resources.GetObject("pbUp2Date.Image")));
            this.pbUp2Date.Location = new System.Drawing.Point(153, 3);
            this.pbUp2Date.Name = "pbUp2Date";
            this.pbUp2Date.Size = new System.Drawing.Size(20, 15);
            this.pbUp2Date.TabIndex = 13;
            this.pbUp2Date.TabStop = false;
            this.pbUp2Date.Visible = false;
            // 
            // pbUpdateLoad
            // 
            this.pbUpdateLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbUpdateLoad.Image = ((System.Drawing.Image)(resources.GetObject("pbUpdateLoad.Image")));
            this.pbUpdateLoad.Location = new System.Drawing.Point(630, 56);
            this.pbUpdateLoad.Name = "pbUpdateLoad";
            this.pbUpdateLoad.Size = new System.Drawing.Size(20, 20);
            this.pbUpdateLoad.TabIndex = 12;
            this.pbUpdateLoad.TabStop = false;
            this.ttOptions.SetToolTip(this.pbUpdateLoad, "working...");
            this.pbUpdateLoad.Visible = false;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(7, 48);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(612, 35);
            this.btnUpdate.TabIndex = 0;
            this.btnUpdate.Text = "Check for KSP MA updates";
            this.ttOptions.SetToolTip(this.btnUpdate, "Check for KSP MA updates\r\nStarts the check for KSP Mod Admin updates.");
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // cbVersionCheck
            // 
            this.cbVersionCheck.AutoSize = true;
            this.cbVersionCheck.Location = new System.Drawing.Point(11, 93);
            this.cbVersionCheck.Name = "cbVersionCheck";
            this.cbVersionCheck.Size = new System.Drawing.Size(182, 17);
            this.cbVersionCheck.TabIndex = 1;
            this.cbVersionCheck.Text = "Check for new version on startup";
            this.ttOptions.SetToolTip(this.cbVersionCheck, "Check for new version on Startup\r\nIf checked KSP MA will check for updates during" +
        " the start of KSP MA.");
            this.cbVersionCheck.UseVisualStyleBackColor = true;
            // 
            // prgBarAdminDownload
            // 
            this.prgBarAdminDownload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prgBarAdminDownload.Location = new System.Drawing.Point(274, 93);
            this.prgBarAdminDownload.Name = "prgBarAdminDownload";
            this.prgBarAdminDownload.Size = new System.Drawing.Size(345, 17);
            this.prgBarAdminDownload.TabIndex = 4;
            this.prgBarAdminDownload.Visible = false;
            // 
            // btnOpenDownloads
            // 
            this.btnOpenDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDownloads.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDownloads.Image")));
            this.btnOpenDownloads.Location = new System.Drawing.Point(629, 90);
            this.btnOpenDownloads.Name = "btnOpenDownloads";
            this.btnOpenDownloads.Size = new System.Drawing.Size(25, 24);
            this.btnOpenDownloads.TabIndex = 2;
            this.ttOptions.SetToolTip(this.btnOpenDownloads, "Open Download Folder\r\nOpens the download folder.");
            this.btnOpenDownloads.UseVisualStyleBackColor = true;
            this.btnOpenDownloads.Click += new System.EventHandler(this.btnOpenDownloads_Click);
            // 
            // tabPagePath
            // 
            this.tabPagePath.BackColor = System.Drawing.SystemColors.Control;
            this.tabPagePath.Controls.Add(this.gbPaths);
            this.tabPagePath.ImageIndex = 1;
            this.tabPagePath.Location = new System.Drawing.Point(4, 23);
            this.tabPagePath.Name = "tabPagePath";
            this.tabPagePath.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePath.Size = new System.Drawing.Size(667, 480);
            this.tabPagePath.TabIndex = 1;
            this.tabPagePath.Text = "Paths";
            // 
            // gbPaths
            // 
            this.gbPaths.Controls.Add(this.tableLayoutPanel12);
            this.gbPaths.Controls.Add(this.tlpSearchBG);
            this.gbPaths.Controls.Add(this.cbKSPPath);
            this.gbPaths.Controls.Add(this.lblModDownloadPath);
            this.gbPaths.Controls.Add(this.tbDownloadPath);
            this.gbPaths.Controls.Add(this.btnDownloadPath);
            this.gbPaths.Controls.Add(this.btnOpenDownloadFolder);
            this.gbPaths.Controls.Add(this.btnOpenKSPRoot);
            this.gbPaths.Controls.Add(this.lblSelectedKSPPath);
            this.gbPaths.Controls.Add(this.splitContainer1);
            this.gbPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPaths.Location = new System.Drawing.Point(3, 3);
            this.gbPaths.Name = "gbPaths";
            this.gbPaths.Size = new System.Drawing.Size(661, 474);
            this.gbPaths.TabIndex = 6;
            this.gbPaths.TabStop = false;
            this.gbPaths.Text = "Paths:";
            // 
            // tableLayoutPanel12
            // 
            this.tableLayoutPanel12.ColumnCount = 7;
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel12.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Controls.Add(this.lblKnownKSPPaths, 0, 0);
            this.tableLayoutPanel12.Controls.Add(this.tbDepth, 6, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnAddPath, 2, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnRemove, 3, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnSteamSearch, 4, 0);
            this.tableLayoutPanel12.Controls.Add(this.btnKSPFolderSearch, 5, 0);
            this.tableLayoutPanel12.Location = new System.Drawing.Point(9, 63);
            this.tableLayoutPanel12.Name = "tableLayoutPanel12";
            this.tableLayoutPanel12.RowCount = 1;
            this.tableLayoutPanel12.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel12.Size = new System.Drawing.Size(594, 30);
            this.tableLayoutPanel12.TabIndex = 24;
            // 
            // lblKnownKSPPaths
            // 
            this.lblKnownKSPPaths.AutoSize = true;
            this.lblKnownKSPPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblKnownKSPPaths.Location = new System.Drawing.Point(3, 0);
            this.lblKnownKSPPaths.Name = "lblKnownKSPPaths";
            this.lblKnownKSPPaths.Size = new System.Drawing.Size(120, 30);
            this.lblKnownKSPPaths.TabIndex = 2;
            this.lblKnownKSPPaths.Text = "Known KSP install path:";
            this.lblKnownKSPPaths.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbDepth
            // 
            this.tbDepth.DecimalSeparator = ",";
            this.tbDepth.Location = new System.Drawing.Point(271, 3);
            this.tbDepth.Maximum = 9D;
            this.tbDepth.Minimum = 1D;
            this.tbDepth.Minus = "-";
            this.tbDepth.Name = "tbDepth";
            this.tbDepth.Plus = "+";
            this.tbDepth.Size = new System.Drawing.Size(25, 20);
            this.tbDepth.TabIndex = 6;
            this.tbDepth.Text = "3";
            this.tbDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbDepth.ValidCharacters = "1234567890";
            // 
            // btnAddPath
            // 
            this.btnAddPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPath.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPath.Image")));
            this.btnAddPath.Location = new System.Drawing.Point(137, 3);
            this.btnAddPath.Name = "btnAddPath";
            this.btnAddPath.Size = new System.Drawing.Size(25, 24);
            this.btnAddPath.TabIndex = 2;
            this.ttOptions.SetToolTip(this.btnAddPath, "Add KSP install path\r\nOpens a FolderSelectDialog to add a new KSP install folder." +
        "");
            this.btnAddPath.UseVisualStyleBackColor = true;
            this.btnAddPath.Click += new System.EventHandler(this.btnAddPath_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(168, 3);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(25, 24);
            this.btnRemove.TabIndex = 3;
            this.ttOptions.SetToolTip(this.btnRemove, "Remove KSP install folder\r\nRemoves the selected KSP install folder from the known" +
        " paths.");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSteamSearch
            // 
            this.btnSteamSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSteamSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSteamSearch.Image")));
            this.btnSteamSearch.Location = new System.Drawing.Point(204, 3);
            this.btnSteamSearch.Name = "btnSteamSearch";
            this.btnSteamSearch.Size = new System.Drawing.Size(25, 24);
            this.btnSteamSearch.TabIndex = 4;
            this.btnSteamSearch.Tag = "Start";
            this.ttOptions.SetToolTip(this.btnSteamSearch, "Steam search\r\nSearches the default steam path for KSP install folders.");
            this.btnSteamSearch.UseVisualStyleBackColor = true;
            this.btnSteamSearch.Click += new System.EventHandler(this.btnSteamSearch_Click);
            // 
            // btnKSPFolderSearch
            // 
            this.btnKSPFolderSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKSPFolderSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnKSPFolderSearch.Image")));
            this.btnKSPFolderSearch.Location = new System.Drawing.Point(240, 3);
            this.btnKSPFolderSearch.Name = "btnKSPFolderSearch";
            this.btnKSPFolderSearch.Size = new System.Drawing.Size(25, 24);
            this.btnKSPFolderSearch.TabIndex = 5;
            this.btnKSPFolderSearch.Tag = "Start";
            this.ttOptions.SetToolTip(this.btnKSPFolderSearch, "KSP folder search\r\nStarts/Stops a KSP folder search. \r\nKSP MA will search all fol" +
        "ders next (up & down) to its install paths.\r\nThe depth is depanding on the numbe" +
        "r on the right of this button.");
            this.btnKSPFolderSearch.UseVisualStyleBackColor = true;
            this.btnKSPFolderSearch.Click += new System.EventHandler(this.btnFolderSearch_Click);
            // 
            // tlpSearchBG
            // 
            this.tlpSearchBG.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpSearchBG.BackColor = System.Drawing.Color.Transparent;
            this.tlpSearchBG.ColumnCount = 3;
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearchBG.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.Controls.Add(this.tlpSearch, 1, 1);
            this.tlpSearchBG.Location = new System.Drawing.Point(21, 95);
            this.tlpSearchBG.Name = "tlpSearchBG";
            this.tlpSearchBG.RowCount = 3;
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpSearchBG.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpSearchBG.Size = new System.Drawing.Size(622, 314);
            this.tlpSearchBG.TabIndex = 17;
            this.tlpSearchBG.Visible = false;
            // 
            // tlpSearch
            // 
            this.tlpSearch.AutoSize = true;
            this.tlpSearch.ColumnCount = 2;
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearch.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpSearch.Controls.Add(this.lblLoading, 1, 0);
            this.tlpSearch.Controls.Add(this.picLoading, 0, 0);
            this.tlpSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpSearch.Location = new System.Drawing.Point(260, 141);
            this.tlpSearch.Name = "tlpSearch";
            this.tlpSearch.RowCount = 1;
            this.tlpSearch.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpSearch.Size = new System.Drawing.Size(101, 31);
            this.tlpSearch.TabIndex = 16;
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Location = new System.Drawing.Point(33, 9);
            this.lblLoading.Margin = new System.Windows.Forms.Padding(3, 9, 3, 0);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(65, 13);
            this.lblLoading.TabIndex = 0;
            this.lblLoading.Text = "searching ...";
            // 
            // picLoading
            // 
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(3, 3);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(24, 25);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            // 
            // cbKSPPath
            // 
            this.cbKSPPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbKSPPath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbKSPPath.FormattingEnabled = true;
            this.cbKSPPath.Location = new System.Drawing.Point(21, 39);
            this.cbKSPPath.Name = "cbKSPPath";
            this.cbKSPPath.Size = new System.Drawing.Size(605, 21);
            this.cbKSPPath.TabIndex = 0;
            this.ttOptions.SetToolTip(this.cbKSPPath, "Chose the KSP install path to perform actions on.");
            this.cbKSPPath.SelectedIndexChanged += new System.EventHandler(this.cbKSPPath_SelectedIndexChanged);
            // 
            // lblModDownloadPath
            // 
            this.lblModDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblModDownloadPath.AutoSize = true;
            this.lblModDownloadPath.Location = new System.Drawing.Point(12, 422);
            this.lblModDownloadPath.Name = "lblModDownloadPath";
            this.lblModDownloadPath.Size = new System.Drawing.Size(108, 13);
            this.lblModDownloadPath.TabIndex = 2;
            this.lblModDownloadPath.Text = "Download/Mod path:";
            // 
            // tbDownloadPath
            // 
            this.tbDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDownloadPath.Location = new System.Drawing.Point(21, 438);
            this.tbDownloadPath.Name = "tbDownloadPath";
            this.tbDownloadPath.ReadOnly = true;
            this.tbDownloadPath.Size = new System.Drawing.Size(577, 20);
            this.tbDownloadPath.TabIndex = 1;
            this.tbDownloadPath.TabStop = false;
            // 
            // btnDownloadPath
            // 
            this.btnDownloadPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDownloadPath.Enabled = false;
            this.btnDownloadPath.Image = ((System.Drawing.Image)(resources.GetObject("btnDownloadPath.Image")));
            this.btnDownloadPath.Location = new System.Drawing.Point(604, 437);
            this.btnDownloadPath.Name = "btnDownloadPath";
            this.btnDownloadPath.Size = new System.Drawing.Size(25, 23);
            this.btnDownloadPath.TabIndex = 9;
            this.ttOptions.SetToolTip(this.btnDownloadPath, "Select download path\r\nOpens a FolderSelectDialog to select a new download folder." +
        "");
            this.btnDownloadPath.UseVisualStyleBackColor = true;
            this.btnDownloadPath.Click += new System.EventHandler(this.btnDownloadPath_Click);
            // 
            // btnOpenDownloadFolder
            // 
            this.btnOpenDownloadFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenDownloadFolder.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDownloadFolder.Image")));
            this.btnOpenDownloadFolder.Location = new System.Drawing.Point(632, 437);
            this.btnOpenDownloadFolder.Name = "btnOpenDownloadFolder";
            this.btnOpenDownloadFolder.Size = new System.Drawing.Size(25, 24);
            this.btnOpenDownloadFolder.TabIndex = 10;
            this.ttOptions.SetToolTip(this.btnOpenDownloadFolder, "Open download folder\r\nOpens the download folder.");
            this.btnOpenDownloadFolder.UseVisualStyleBackColor = true;
            this.btnOpenDownloadFolder.Click += new System.EventHandler(this.btnOpenDownloadFolder_Click);
            // 
            // btnOpenKSPRoot
            // 
            this.btnOpenKSPRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenKSPRoot.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenKSPRoot.Image")));
            this.btnOpenKSPRoot.Location = new System.Drawing.Point(632, 38);
            this.btnOpenKSPRoot.Name = "btnOpenKSPRoot";
            this.btnOpenKSPRoot.Size = new System.Drawing.Size(25, 24);
            this.btnOpenKSPRoot.TabIndex = 1;
            this.ttOptions.SetToolTip(this.btnOpenKSPRoot, "Open KSP install folder.\r\nOpens the selected KSP install folder.");
            this.btnOpenKSPRoot.UseVisualStyleBackColor = true;
            this.btnOpenKSPRoot.Click += new System.EventHandler(this.btnOpenKSPRoot_Click);
            // 
            // lblSelectedKSPPath
            // 
            this.lblSelectedKSPPath.AutoSize = true;
            this.lblSelectedKSPPath.Location = new System.Drawing.Point(12, 21);
            this.lblSelectedKSPPath.Name = "lblSelectedKSPPath";
            this.lblSelectedKSPPath.Size = new System.Drawing.Size(129, 13);
            this.lblSelectedKSPPath.TabIndex = 2;
            this.lblSelectedKSPPath.Text = "Selected KSP install path:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer1.Location = new System.Drawing.Point(21, 96);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvKnownPaths);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(622, 313);
            this.splitContainer1.SplitterDistance = 441;
            this.splitContainer1.TabIndex = 23;
            // 
            // tvKnownPaths
            // 
            this.tvKnownPaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvKnownPaths.Location = new System.Drawing.Point(0, 0);
            this.tvKnownPaths.Name = "tvKnownPaths";
            this.tvKnownPaths.Size = new System.Drawing.Size(441, 313);
            this.tvKnownPaths.TabIndex = 7;
            this.tvKnownPaths.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvKnownPaths_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.lblKSPPathNote);
            this.panel1.Controls.Add(this.tbNote);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 313);
            this.panel1.TabIndex = 23;
            // 
            // lblKSPPathNote
            // 
            this.lblKSPPathNote.AutoSize = true;
            this.lblKSPPathNote.Location = new System.Drawing.Point(4, 4);
            this.lblKSPPathNote.Name = "lblKSPPathNote";
            this.lblKSPPathNote.Size = new System.Drawing.Size(33, 13);
            this.lblKSPPathNote.TabIndex = 22;
            this.lblKSPPathNote.Text = "Note:";
            // 
            // tbNote
            // 
            this.tbNote.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNote.Location = new System.Drawing.Point(0, 20);
            this.tbNote.Multiline = true;
            this.tbNote.Name = "tbNote";
            this.tbNote.Size = new System.Drawing.Size(177, 293);
            this.tbNote.TabIndex = 8;
            this.tbNote.TextChanged += new System.EventHandler(this.tbNote_TextChanged);
            // 
            // tabPageMisc
            // 
            this.tabPageMisc.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageMisc.Controls.Add(this.gbDestinationDetection);
            this.tabPageMisc.Controls.Add(this.gbToolTip);
            this.tabPageMisc.Controls.Add(this.gbLaguage);
            this.tabPageMisc.Controls.Add(this.gbNodeColors);
            this.tabPageMisc.Controls.Add(this.gpModConflictHandling);
            this.tabPageMisc.ImageIndex = 2;
            this.tabPageMisc.Location = new System.Drawing.Point(4, 23);
            this.tabPageMisc.Name = "tabPageMisc";
            this.tabPageMisc.Size = new System.Drawing.Size(667, 480);
            this.tabPageMisc.TabIndex = 2;
            this.tabPageMisc.Text = "Misc";
            // 
            // gbDestinationDetection
            // 
            this.gbDestinationDetection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDestinationDetection.Controls.Add(this.tableLayoutPanel14);
            this.gbDestinationDetection.Controls.Add(this.cbDDCopyToGameData);
            this.gbDestinationDetection.Location = new System.Drawing.Point(0, 54);
            this.gbDestinationDetection.Name = "gbDestinationDetection";
            this.gbDestinationDetection.Size = new System.Drawing.Size(667, 69);
            this.gbDestinationDetection.TabIndex = 32;
            this.gbDestinationDetection.TabStop = false;
            this.gbDestinationDetection.Text = "Destination detection:";
            // 
            // rbDDJustDump
            // 
            this.rbDDJustDump.AutoSize = true;
            this.rbDDJustDump.Location = new System.Drawing.Point(258, 3);
            this.rbDDJustDump.Name = "rbDDJustDump";
            this.rbDDJustDump.Size = new System.Drawing.Size(197, 17);
            this.rbDDJustDump.TabIndex = 1;
            this.rbDDJustDump.Text = "Just copy mod archive to GameData";
            this.ttOptions.SetToolTip(this.rbDDJustDump, "The destination of the mod archive node will be set to GameData.\r\nNOTE: May cause" +
        " problems with conflict detection!");
            this.rbDDJustDump.UseVisualStyleBackColor = true;
            // 
            // rbDDSmartDestDetection
            // 
            this.rbDDSmartDestDetection.AutoSize = true;
            this.rbDDSmartDestDetection.Checked = true;
            this.rbDDSmartDestDetection.Location = new System.Drawing.Point(3, 3);
            this.rbDDSmartDestDetection.Name = "rbDDSmartDestDetection";
            this.rbDDSmartDestDetection.Size = new System.Drawing.Size(229, 17);
            this.rbDDSmartDestDetection.TabIndex = 1;
            this.rbDDSmartDestDetection.TabStop = true;
            this.rbDDSmartDestDetection.Text = "Smart destination detection (recommended)";
            this.ttOptions.SetToolTip(this.rbDDSmartDestDetection, "KSP MA searches for a similar KSP folder structure in the mod archive\r\nand sets t" +
        "he destination of the mod files accordingly.");
            this.rbDDSmartDestDetection.UseVisualStyleBackColor = true;
            this.rbDDSmartDestDetection.CheckedChanged += new System.EventHandler(this.rbDDSmartDestDetection_CheckedChanged);
            // 
            // cbDDCopyToGameData
            // 
            this.cbDDCopyToGameData.AutoSize = true;
            this.cbDDCopyToGameData.Location = new System.Drawing.Point(24, 42);
            this.cbDDCopyToGameData.Name = "cbDDCopyToGameData";
            this.cbDDCopyToGameData.Size = new System.Drawing.Size(161, 17);
            this.cbDDCopyToGameData.TabIndex = 0;
            this.cbDDCopyToGameData.Text = "Fallback to GameData folder";
            this.ttOptions.SetToolTip(this.cbDDCopyToGameData, "If destination detection fails the destination of the mod archive node\r\nwill be s" +
        "et to the GameData folder.\r\nNOTE: May cause problems with conflict detection!");
            this.cbDDCopyToGameData.UseVisualStyleBackColor = true;
            // 
            // gbToolTip
            // 
            this.gbToolTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbToolTip.Controls.Add(this.tableLayoutPanel13);
            this.gbToolTip.Location = new System.Drawing.Point(0, 190);
            this.gbToolTip.Name = "gbToolTip";
            this.gbToolTip.Size = new System.Drawing.Size(667, 45);
            this.gbToolTip.TabIndex = 5;
            this.gbToolTip.TabStop = false;
            this.gbToolTip.Text = "ToolTip:";
            // 
            // tableLayoutPanel13
            // 
            this.tableLayoutPanel13.ColumnCount = 10;
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel13.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 315F));
            this.tableLayoutPanel13.Controls.Add(this.lblSecond2, 8, 0);
            this.tableLayoutPanel13.Controls.Add(this.cbToolTipOnOff, 0, 0);
            this.tableLayoutPanel13.Controls.Add(this.tbToolTipDisplayTime, 7, 0);
            this.tableLayoutPanel13.Controls.Add(this.lblToolTipDisplayTime, 6, 0);
            this.tableLayoutPanel13.Controls.Add(this.lblToolTipDelay, 2, 0);
            this.tableLayoutPanel13.Controls.Add(this.lblSecond1, 4, 0);
            this.tableLayoutPanel13.Controls.Add(this.tbToolTipDelay, 3, 0);
            this.tableLayoutPanel13.Location = new System.Drawing.Point(6, 14);
            this.tableLayoutPanel13.Name = "tableLayoutPanel13";
            this.tableLayoutPanel13.RowCount = 1;
            this.tableLayoutPanel13.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel13.Size = new System.Drawing.Size(655, 26);
            this.tableLayoutPanel13.TabIndex = 2;
            // 
            // lblSecond2
            // 
            this.lblSecond2.AutoSize = true;
            this.lblSecond2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSecond2.Location = new System.Drawing.Point(330, 0);
            this.lblSecond2.Name = "lblSecond2";
            this.lblSecond2.Size = new System.Drawing.Size(27, 26);
            this.lblSecond2.TabIndex = 2;
            this.lblSecond2.Text = "sec.";
            this.lblSecond2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbToolTipOnOff
            // 
            this.cbToolTipOnOff.AutoSize = true;
            this.cbToolTipOnOff.Checked = true;
            this.cbToolTipOnOff.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbToolTipOnOff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbToolTipOnOff.Location = new System.Drawing.Point(3, 3);
            this.cbToolTipOnOff.Name = "cbToolTipOnOff";
            this.cbToolTipOnOff.Size = new System.Drawing.Size(65, 20);
            this.cbToolTipOnOff.TabIndex = 0;
            this.cbToolTipOnOff.Text = "On / Off";
            this.cbToolTipOnOff.UseVisualStyleBackColor = true;
            this.cbToolTipOnOff.CheckedChanged += new System.EventHandler(this.cbToolTipOnOff_CheckedChanged);
            // 
            // tbToolTipDisplayTime
            // 
            this.tbToolTipDisplayTime.AllowDecimalSeparator = false;
            this.tbToolTipDisplayTime.AllowNegativeSign = false;
            this.tbToolTipDisplayTime.Location = new System.Drawing.Point(284, 3);
            this.tbToolTipDisplayTime.MaxLength = 5;
            this.tbToolTipDisplayTime.Name = "tbToolTipDisplayTime";
            this.tbToolTipDisplayTime.Size = new System.Drawing.Size(40, 20);
            this.tbToolTipDisplayTime.TabIndex = 1;
            this.tbToolTipDisplayTime.Text = "10.00";
            this.tbToolTipDisplayTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbToolTipDisplayTime.TextChanged += new System.EventHandler(this.tbToolTipDisplayTime_TextChanged);
            // 
            // lblToolTipDisplayTime
            // 
            this.lblToolTipDisplayTime.AutoSize = true;
            this.lblToolTipDisplayTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolTipDisplayTime.Location = new System.Drawing.Point(212, 0);
            this.lblToolTipDisplayTime.Name = "lblToolTipDisplayTime";
            this.lblToolTipDisplayTime.Size = new System.Drawing.Size(66, 26);
            this.lblToolTipDisplayTime.TabIndex = 2;
            this.lblToolTipDisplayTime.Text = "Display time:";
            this.lblToolTipDisplayTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblToolTipDelay
            // 
            this.lblToolTipDelay.AutoSize = true;
            this.lblToolTipDelay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolTipDelay.Location = new System.Drawing.Point(82, 0);
            this.lblToolTipDelay.Name = "lblToolTipDelay";
            this.lblToolTipDelay.Size = new System.Drawing.Size(37, 26);
            this.lblToolTipDelay.TabIndex = 2;
            this.lblToolTipDelay.Text = "Delay:";
            this.lblToolTipDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSecond1
            // 
            this.lblSecond1.AutoSize = true;
            this.lblSecond1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSecond1.Location = new System.Drawing.Point(171, 0);
            this.lblSecond1.Name = "lblSecond1";
            this.lblSecond1.Size = new System.Drawing.Size(27, 26);
            this.lblSecond1.TabIndex = 2;
            this.lblSecond1.Text = "sec.";
            this.lblSecond1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbToolTipDelay
            // 
            this.tbToolTipDelay.AllowDecimalSeparator = false;
            this.tbToolTipDelay.AllowNegativeSign = false;
            this.tbToolTipDelay.Location = new System.Drawing.Point(125, 3);
            this.tbToolTipDelay.MaxLength = 5;
            this.tbToolTipDelay.Name = "tbToolTipDelay";
            this.tbToolTipDelay.Size = new System.Drawing.Size(40, 20);
            this.tbToolTipDelay.TabIndex = 1;
            this.tbToolTipDelay.Text = "0.50";
            this.tbToolTipDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbToolTipDelay.TextChanged += new System.EventHandler(this.tbToolTipDelay_TextChanged);
            // 
            // gbLaguage
            // 
            this.gbLaguage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLaguage.Controls.Add(this.cbLanguages);
            this.gbLaguage.Location = new System.Drawing.Point(0, 3);
            this.gbLaguage.Name = "gbLaguage";
            this.gbLaguage.Size = new System.Drawing.Size(667, 50);
            this.gbLaguage.TabIndex = 4;
            this.gbLaguage.TabStop = false;
            this.gbLaguage.Text = "Language:";
            // 
            // cbLanguages
            // 
            this.cbLanguages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguages.FormattingEnabled = true;
            this.cbLanguages.Location = new System.Drawing.Point(9, 19);
            this.cbLanguages.Name = "cbLanguages";
            this.cbLanguages.Size = new System.Drawing.Size(176, 21);
            this.cbLanguages.TabIndex = 0;
            this.ttOptions.SetToolTip(this.cbLanguages, "Chose your favorit language.");
            this.cbLanguages.SelectedIndexChanged += new System.EventHandler(this.cbLanguages_SelectedIndexChanged);
            // 
            // gbNodeColors
            // 
            this.gbNodeColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel11);
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel10);
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel9);
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel8);
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel7);
            this.gbNodeColors.Controls.Add(this.tableLayoutPanel6);
            this.gbNodeColors.Location = new System.Drawing.Point(0, 236);
            this.gbNodeColors.Name = "gbNodeColors";
            this.gbNodeColors.Size = new System.Drawing.Size(667, 244);
            this.gbNodeColors.TabIndex = 3;
            this.gbNodeColors.TabStop = false;
            this.gbNodeColors.Text = "ModSelection node colors:";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel11.ColumnCount = 9;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel11.Controls.Add(this.lblColorDestinationDetection, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.pDestinationDetected, 1, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblColorRDestinationDetection, 2, 0);
            this.tableLayoutPanel11.Controls.Add(this.tbDestinationDetectedRed, 3, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblColorGDestinationDetection, 4, 0);
            this.tableLayoutPanel11.Controls.Add(this.tbDestinationDetectedGreen, 5, 0);
            this.tableLayoutPanel11.Controls.Add(this.btnDestinationDetected, 8, 0);
            this.tableLayoutPanel11.Controls.Add(this.lblColorBDestinationDetection, 6, 0);
            this.tableLayoutPanel11.Controls.Add(this.tbDestinationDetectedBlue, 7, 0);
            this.tableLayoutPanel11.Location = new System.Drawing.Point(9, 19);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel11.TabIndex = 31;
            // 
            // lblColorDestinationDetection
            // 
            this.lblColorDestinationDetection.AutoSize = true;
            this.lblColorDestinationDetection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorDestinationDetection.Location = new System.Drawing.Point(3, 0);
            this.lblColorDestinationDetection.Name = "lblColorDestinationDetection";
            this.lblColorDestinationDetection.Size = new System.Drawing.Size(144, 29);
            this.lblColorDestinationDetection.TabIndex = 0;
            this.lblColorDestinationDetection.Text = "Destination detected:";
            this.lblColorDestinationDetection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pDestinationDetected
            // 
            this.pDestinationDetected.BackColor = System.Drawing.Color.Black;
            this.pDestinationDetected.Location = new System.Drawing.Point(153, 5);
            this.pDestinationDetected.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pDestinationDetected.Name = "pDestinationDetected";
            this.pDestinationDetected.Size = new System.Drawing.Size(20, 20);
            this.pDestinationDetected.TabIndex = 1;
            // 
            // lblColorRDestinationDetection
            // 
            this.lblColorRDestinationDetection.AutoSize = true;
            this.lblColorRDestinationDetection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRDestinationDetection.Location = new System.Drawing.Point(179, 0);
            this.lblColorRDestinationDetection.Name = "lblColorRDestinationDetection";
            this.lblColorRDestinationDetection.Size = new System.Drawing.Size(15, 29);
            this.lblColorRDestinationDetection.TabIndex = 2;
            this.lblColorRDestinationDetection.Text = "R";
            this.lblColorRDestinationDetection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationDetectedRed
            // 
            this.tbDestinationDetectedRed.DecimalSeparator = ",";
            this.tbDestinationDetectedRed.Location = new System.Drawing.Point(200, 5);
            this.tbDestinationDetectedRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationDetectedRed.Maximum = 255D;
            this.tbDestinationDetectedRed.MaxLength = 3;
            this.tbDestinationDetectedRed.Minimum = 0D;
            this.tbDestinationDetectedRed.Minus = "-";
            this.tbDestinationDetectedRed.Name = "tbDestinationDetectedRed";
            this.tbDestinationDetectedRed.Plus = "+";
            this.tbDestinationDetectedRed.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationDetectedRed.TabIndex = 2;
            this.tbDestinationDetectedRed.Text = "0";
            this.tbDestinationDetectedRed.ValidCharacters = "1234567890";
            this.tbDestinationDetectedRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorGDestinationDetection
            // 
            this.lblColorGDestinationDetection.AutoSize = true;
            this.lblColorGDestinationDetection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGDestinationDetection.Location = new System.Drawing.Point(233, 0);
            this.lblColorGDestinationDetection.Name = "lblColorGDestinationDetection";
            this.lblColorGDestinationDetection.Size = new System.Drawing.Size(15, 29);
            this.lblColorGDestinationDetection.TabIndex = 2;
            this.lblColorGDestinationDetection.Text = "G";
            this.lblColorGDestinationDetection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationDetectedGreen
            // 
            this.tbDestinationDetectedGreen.DecimalSeparator = ",";
            this.tbDestinationDetectedGreen.Location = new System.Drawing.Point(254, 5);
            this.tbDestinationDetectedGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationDetectedGreen.Maximum = 255D;
            this.tbDestinationDetectedGreen.MaxLength = 3;
            this.tbDestinationDetectedGreen.Minimum = 0D;
            this.tbDestinationDetectedGreen.Minus = "-";
            this.tbDestinationDetectedGreen.Name = "tbDestinationDetectedGreen";
            this.tbDestinationDetectedGreen.Plus = "+";
            this.tbDestinationDetectedGreen.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationDetectedGreen.TabIndex = 3;
            this.tbDestinationDetectedGreen.Text = "0";
            this.tbDestinationDetectedGreen.ValidCharacters = "1234567890";
            this.tbDestinationDetectedGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // btnDestinationDetected
            // 
            this.btnDestinationDetected.Image = ((System.Drawing.Image)(resources.GetObject("btnDestinationDetected.Image")));
            this.btnDestinationDetected.Location = new System.Drawing.Point(340, 3);
            this.btnDestinationDetected.Name = "btnDestinationDetected";
            this.btnDestinationDetected.Size = new System.Drawing.Size(24, 23);
            this.btnDestinationDetected.TabIndex = 5;
            this.ttOptions.SetToolTip(this.btnDestinationDetected, "Chose node color for nodes which have a destination.");
            this.btnDestinationDetected.UseVisualStyleBackColor = true;
            this.btnDestinationDetected.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // lblColorBDestinationDetection
            // 
            this.lblColorBDestinationDetection.AutoSize = true;
            this.lblColorBDestinationDetection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBDestinationDetection.Location = new System.Drawing.Point(287, 0);
            this.lblColorBDestinationDetection.Name = "lblColorBDestinationDetection";
            this.lblColorBDestinationDetection.Size = new System.Drawing.Size(14, 29);
            this.lblColorBDestinationDetection.TabIndex = 2;
            this.lblColorBDestinationDetection.Text = "B";
            this.lblColorBDestinationDetection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationDetectedBlue
            // 
            this.tbDestinationDetectedBlue.DecimalSeparator = ",";
            this.tbDestinationDetectedBlue.Location = new System.Drawing.Point(307, 5);
            this.tbDestinationDetectedBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationDetectedBlue.Maximum = 255D;
            this.tbDestinationDetectedBlue.MaxLength = 3;
            this.tbDestinationDetectedBlue.Minimum = 0D;
            this.tbDestinationDetectedBlue.Minus = "-";
            this.tbDestinationDetectedBlue.Name = "tbDestinationDetectedBlue";
            this.tbDestinationDetectedBlue.Plus = "+";
            this.tbDestinationDetectedBlue.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationDetectedBlue.TabIndex = 4;
            this.tbDestinationDetectedBlue.Text = "0";
            this.tbDestinationDetectedBlue.ValidCharacters = "1234567890";
            this.tbDestinationDetectedBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel10.ColumnCount = 9;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel10.Controls.Add(this.lblColorDestinationMissing, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.pDestinationMissing, 1, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblColorRDestinationMissing, 2, 0);
            this.tableLayoutPanel10.Controls.Add(this.tbDestinationMissingRed, 3, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblColorGDestinationMissing, 4, 0);
            this.tableLayoutPanel10.Controls.Add(this.tbDestinationMissingGreen, 5, 0);
            this.tableLayoutPanel10.Controls.Add(this.btnDestinationMissing, 8, 0);
            this.tableLayoutPanel10.Controls.Add(this.lblColorBDestinationMissing, 6, 0);
            this.tableLayoutPanel10.Controls.Add(this.tbDestinationMissingBlue, 7, 0);
            this.tableLayoutPanel10.Location = new System.Drawing.Point(9, 50);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel10.TabIndex = 30;
            // 
            // lblColorDestinationMissing
            // 
            this.lblColorDestinationMissing.AutoSize = true;
            this.lblColorDestinationMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorDestinationMissing.Location = new System.Drawing.Point(3, 0);
            this.lblColorDestinationMissing.Name = "lblColorDestinationMissing";
            this.lblColorDestinationMissing.Size = new System.Drawing.Size(144, 29);
            this.lblColorDestinationMissing.TabIndex = 0;
            this.lblColorDestinationMissing.Text = "Destination missing:";
            this.lblColorDestinationMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pDestinationMissing
            // 
            this.pDestinationMissing.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130)))));
            this.pDestinationMissing.Location = new System.Drawing.Point(153, 5);
            this.pDestinationMissing.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pDestinationMissing.Name = "pDestinationMissing";
            this.pDestinationMissing.Size = new System.Drawing.Size(20, 20);
            this.pDestinationMissing.TabIndex = 1;
            // 
            // lblColorRDestinationMissing
            // 
            this.lblColorRDestinationMissing.AutoSize = true;
            this.lblColorRDestinationMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRDestinationMissing.Location = new System.Drawing.Point(179, 0);
            this.lblColorRDestinationMissing.Name = "lblColorRDestinationMissing";
            this.lblColorRDestinationMissing.Size = new System.Drawing.Size(15, 29);
            this.lblColorRDestinationMissing.TabIndex = 2;
            this.lblColorRDestinationMissing.Text = "R";
            this.lblColorRDestinationMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationMissingRed
            // 
            this.tbDestinationMissingRed.DecimalSeparator = ",";
            this.tbDestinationMissingRed.Location = new System.Drawing.Point(200, 5);
            this.tbDestinationMissingRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationMissingRed.Maximum = 255D;
            this.tbDestinationMissingRed.MaxLength = 3;
            this.tbDestinationMissingRed.Minimum = 0D;
            this.tbDestinationMissingRed.Minus = "-";
            this.tbDestinationMissingRed.Name = "tbDestinationMissingRed";
            this.tbDestinationMissingRed.Plus = "+";
            this.tbDestinationMissingRed.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationMissingRed.TabIndex = 6;
            this.tbDestinationMissingRed.Text = "130";
            this.tbDestinationMissingRed.ValidCharacters = "1234567890";
            this.tbDestinationMissingRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorGDestinationMissing
            // 
            this.lblColorGDestinationMissing.AutoSize = true;
            this.lblColorGDestinationMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGDestinationMissing.Location = new System.Drawing.Point(233, 0);
            this.lblColorGDestinationMissing.Name = "lblColorGDestinationMissing";
            this.lblColorGDestinationMissing.Size = new System.Drawing.Size(15, 29);
            this.lblColorGDestinationMissing.TabIndex = 2;
            this.lblColorGDestinationMissing.Text = "G";
            this.lblColorGDestinationMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationMissingGreen
            // 
            this.tbDestinationMissingGreen.DecimalSeparator = ",";
            this.tbDestinationMissingGreen.Location = new System.Drawing.Point(254, 5);
            this.tbDestinationMissingGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationMissingGreen.Maximum = 255D;
            this.tbDestinationMissingGreen.MaxLength = 3;
            this.tbDestinationMissingGreen.Minimum = 0D;
            this.tbDestinationMissingGreen.Minus = "-";
            this.tbDestinationMissingGreen.Name = "tbDestinationMissingGreen";
            this.tbDestinationMissingGreen.Plus = "+";
            this.tbDestinationMissingGreen.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationMissingGreen.TabIndex = 7;
            this.tbDestinationMissingGreen.Text = "130";
            this.tbDestinationMissingGreen.ValidCharacters = "1234567890";
            this.tbDestinationMissingGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // btnDestinationMissing
            // 
            this.btnDestinationMissing.Image = ((System.Drawing.Image)(resources.GetObject("btnDestinationMissing.Image")));
            this.btnDestinationMissing.Location = new System.Drawing.Point(340, 3);
            this.btnDestinationMissing.Name = "btnDestinationMissing";
            this.btnDestinationMissing.Size = new System.Drawing.Size(24, 23);
            this.btnDestinationMissing.TabIndex = 9;
            this.ttOptions.SetToolTip(this.btnDestinationMissing, "Chose node color for nodes where the destination is missing.");
            this.btnDestinationMissing.UseVisualStyleBackColor = true;
            this.btnDestinationMissing.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // lblColorBDestinationMissing
            // 
            this.lblColorBDestinationMissing.AutoSize = true;
            this.lblColorBDestinationMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBDestinationMissing.Location = new System.Drawing.Point(287, 0);
            this.lblColorBDestinationMissing.Name = "lblColorBDestinationMissing";
            this.lblColorBDestinationMissing.Size = new System.Drawing.Size(14, 29);
            this.lblColorBDestinationMissing.TabIndex = 2;
            this.lblColorBDestinationMissing.Text = "B";
            this.lblColorBDestinationMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationMissingBlue
            // 
            this.tbDestinationMissingBlue.DecimalSeparator = ",";
            this.tbDestinationMissingBlue.Location = new System.Drawing.Point(307, 5);
            this.tbDestinationMissingBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationMissingBlue.Maximum = 255D;
            this.tbDestinationMissingBlue.MaxLength = 3;
            this.tbDestinationMissingBlue.Minimum = 0D;
            this.tbDestinationMissingBlue.Minus = "-";
            this.tbDestinationMissingBlue.Name = "tbDestinationMissingBlue";
            this.tbDestinationMissingBlue.Plus = "+";
            this.tbDestinationMissingBlue.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationMissingBlue.TabIndex = 8;
            this.tbDestinationMissingBlue.Text = "130";
            this.tbDestinationMissingBlue.ValidCharacters = "1234567890";
            this.tbDestinationMissingBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel9.ColumnCount = 9;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel9.Controls.Add(this.lblColorDestinationConflicts, 0, 0);
            this.tableLayoutPanel9.Controls.Add(this.pDestinationConflict, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblColorRDestinationConflict, 2, 0);
            this.tableLayoutPanel9.Controls.Add(this.tbDestinationConflictRed, 3, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblColorGDestinationConflict, 4, 0);
            this.tableLayoutPanel9.Controls.Add(this.tbDestinationConflictGreen, 5, 0);
            this.tableLayoutPanel9.Controls.Add(this.btnDestinationConflict, 8, 0);
            this.tableLayoutPanel9.Controls.Add(this.lblColorBDestinationConflict, 6, 0);
            this.tableLayoutPanel9.Controls.Add(this.tbDestinationConflictBlue, 7, 0);
            this.tableLayoutPanel9.Location = new System.Drawing.Point(9, 81);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel9.TabIndex = 29;
            // 
            // lblColorDestinationConflicts
            // 
            this.lblColorDestinationConflicts.AutoSize = true;
            this.lblColorDestinationConflicts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorDestinationConflicts.Location = new System.Drawing.Point(3, 0);
            this.lblColorDestinationConflicts.Name = "lblColorDestinationConflicts";
            this.lblColorDestinationConflicts.Size = new System.Drawing.Size(144, 29);
            this.lblColorDestinationConflicts.TabIndex = 0;
            this.lblColorDestinationConflicts.Text = "Destination conflict:";
            this.lblColorDestinationConflicts.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pDestinationConflict
            // 
            this.pDestinationConflict.BackColor = System.Drawing.Color.Orange;
            this.pDestinationConflict.Location = new System.Drawing.Point(153, 5);
            this.pDestinationConflict.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pDestinationConflict.Name = "pDestinationConflict";
            this.pDestinationConflict.Size = new System.Drawing.Size(20, 20);
            this.pDestinationConflict.TabIndex = 1;
            // 
            // lblColorRDestinationConflict
            // 
            this.lblColorRDestinationConflict.AutoSize = true;
            this.lblColorRDestinationConflict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRDestinationConflict.Location = new System.Drawing.Point(179, 0);
            this.lblColorRDestinationConflict.Name = "lblColorRDestinationConflict";
            this.lblColorRDestinationConflict.Size = new System.Drawing.Size(15, 29);
            this.lblColorRDestinationConflict.TabIndex = 2;
            this.lblColorRDestinationConflict.Text = "R";
            this.lblColorRDestinationConflict.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationConflictRed
            // 
            this.tbDestinationConflictRed.DecimalSeparator = ",";
            this.tbDestinationConflictRed.Location = new System.Drawing.Point(200, 5);
            this.tbDestinationConflictRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationConflictRed.Maximum = 255D;
            this.tbDestinationConflictRed.MaxLength = 3;
            this.tbDestinationConflictRed.Minimum = 0D;
            this.tbDestinationConflictRed.Minus = "-";
            this.tbDestinationConflictRed.Name = "tbDestinationConflictRed";
            this.tbDestinationConflictRed.Plus = "+";
            this.tbDestinationConflictRed.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationConflictRed.TabIndex = 10;
            this.tbDestinationConflictRed.Text = "255";
            this.tbDestinationConflictRed.ValidCharacters = "1234567890";
            this.tbDestinationConflictRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorGDestinationConflict
            // 
            this.lblColorGDestinationConflict.AutoSize = true;
            this.lblColorGDestinationConflict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGDestinationConflict.Location = new System.Drawing.Point(233, 0);
            this.lblColorGDestinationConflict.Name = "lblColorGDestinationConflict";
            this.lblColorGDestinationConflict.Size = new System.Drawing.Size(15, 29);
            this.lblColorGDestinationConflict.TabIndex = 2;
            this.lblColorGDestinationConflict.Text = "G";
            this.lblColorGDestinationConflict.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationConflictGreen
            // 
            this.tbDestinationConflictGreen.DecimalSeparator = ",";
            this.tbDestinationConflictGreen.Location = new System.Drawing.Point(254, 5);
            this.tbDestinationConflictGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationConflictGreen.Maximum = 255D;
            this.tbDestinationConflictGreen.MaxLength = 3;
            this.tbDestinationConflictGreen.Minimum = 0D;
            this.tbDestinationConflictGreen.Minus = "-";
            this.tbDestinationConflictGreen.Name = "tbDestinationConflictGreen";
            this.tbDestinationConflictGreen.Plus = "+";
            this.tbDestinationConflictGreen.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationConflictGreen.TabIndex = 11;
            this.tbDestinationConflictGreen.Text = "165";
            this.tbDestinationConflictGreen.ValidCharacters = "1234567890";
            this.tbDestinationConflictGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // btnDestinationConflict
            // 
            this.btnDestinationConflict.Image = ((System.Drawing.Image)(resources.GetObject("btnDestinationConflict.Image")));
            this.btnDestinationConflict.Location = new System.Drawing.Point(340, 3);
            this.btnDestinationConflict.Name = "btnDestinationConflict";
            this.btnDestinationConflict.Size = new System.Drawing.Size(24, 23);
            this.btnDestinationConflict.TabIndex = 13;
            this.ttOptions.SetToolTip(this.btnDestinationConflict, "Chose node color for nodes that have conflicts.");
            this.btnDestinationConflict.UseVisualStyleBackColor = true;
            this.btnDestinationConflict.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // lblColorBDestinationConflict
            // 
            this.lblColorBDestinationConflict.AutoSize = true;
            this.lblColorBDestinationConflict.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBDestinationConflict.Location = new System.Drawing.Point(287, 0);
            this.lblColorBDestinationConflict.Name = "lblColorBDestinationConflict";
            this.lblColorBDestinationConflict.Size = new System.Drawing.Size(14, 29);
            this.lblColorBDestinationConflict.TabIndex = 2;
            this.lblColorBDestinationConflict.Text = "B";
            this.lblColorBDestinationConflict.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbDestinationConflictBlue
            // 
            this.tbDestinationConflictBlue.DecimalSeparator = ",";
            this.tbDestinationConflictBlue.Location = new System.Drawing.Point(307, 5);
            this.tbDestinationConflictBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbDestinationConflictBlue.Maximum = 255D;
            this.tbDestinationConflictBlue.MaxLength = 3;
            this.tbDestinationConflictBlue.Minimum = 0D;
            this.tbDestinationConflictBlue.Minus = "-";
            this.tbDestinationConflictBlue.Name = "tbDestinationConflictBlue";
            this.tbDestinationConflictBlue.Plus = "+";
            this.tbDestinationConflictBlue.Size = new System.Drawing.Size(27, 20);
            this.tbDestinationConflictBlue.TabIndex = 12;
            this.tbDestinationConflictBlue.Text = "0";
            this.tbDestinationConflictBlue.ValidCharacters = "1234567890";
            this.tbDestinationConflictBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel8.ColumnCount = 9;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel8.Controls.Add(this.lblColorModIsInstalled, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.pModInstalled, 1, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblColorRModIsInstalled, 2, 0);
            this.tableLayoutPanel8.Controls.Add(this.tbModInstalledRed, 3, 0);
            this.tableLayoutPanel8.Controls.Add(this.btnModInstalled, 8, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblColorGModIsInstalled, 4, 0);
            this.tableLayoutPanel8.Controls.Add(this.tbModInstalledGreen, 5, 0);
            this.tableLayoutPanel8.Controls.Add(this.lblColorBModIsInstalled, 6, 0);
            this.tableLayoutPanel8.Controls.Add(this.tbModInstalledBlue, 7, 0);
            this.tableLayoutPanel8.Location = new System.Drawing.Point(9, 112);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 1;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel8.TabIndex = 28;
            // 
            // lblColorModIsInstalled
            // 
            this.lblColorModIsInstalled.AutoSize = true;
            this.lblColorModIsInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorModIsInstalled.Location = new System.Drawing.Point(3, 0);
            this.lblColorModIsInstalled.Name = "lblColorModIsInstalled";
            this.lblColorModIsInstalled.Size = new System.Drawing.Size(144, 29);
            this.lblColorModIsInstalled.TabIndex = 0;
            this.lblColorModIsInstalled.Text = "Mod is installed:";
            this.lblColorModIsInstalled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pModInstalled
            // 
            this.pModInstalled.BackColor = System.Drawing.Color.Green;
            this.pModInstalled.Location = new System.Drawing.Point(153, 5);
            this.pModInstalled.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pModInstalled.Name = "pModInstalled";
            this.pModInstalled.Size = new System.Drawing.Size(20, 20);
            this.pModInstalled.TabIndex = 1;
            // 
            // lblColorRModIsInstalled
            // 
            this.lblColorRModIsInstalled.AutoSize = true;
            this.lblColorRModIsInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRModIsInstalled.Location = new System.Drawing.Point(179, 0);
            this.lblColorRModIsInstalled.Name = "lblColorRModIsInstalled";
            this.lblColorRModIsInstalled.Size = new System.Drawing.Size(15, 29);
            this.lblColorRModIsInstalled.TabIndex = 2;
            this.lblColorRModIsInstalled.Text = "R";
            this.lblColorRModIsInstalled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModInstalledRed
            // 
            this.tbModInstalledRed.DecimalSeparator = ",";
            this.tbModInstalledRed.Location = new System.Drawing.Point(200, 5);
            this.tbModInstalledRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModInstalledRed.Maximum = 255D;
            this.tbModInstalledRed.MaxLength = 3;
            this.tbModInstalledRed.Minimum = 0D;
            this.tbModInstalledRed.Minus = "-";
            this.tbModInstalledRed.Name = "tbModInstalledRed";
            this.tbModInstalledRed.Plus = "+";
            this.tbModInstalledRed.Size = new System.Drawing.Size(27, 20);
            this.tbModInstalledRed.TabIndex = 14;
            this.tbModInstalledRed.Text = "0";
            this.tbModInstalledRed.ValidCharacters = "1234567890";
            this.tbModInstalledRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // btnModInstalled
            // 
            this.btnModInstalled.Image = ((System.Drawing.Image)(resources.GetObject("btnModInstalled.Image")));
            this.btnModInstalled.Location = new System.Drawing.Point(340, 3);
            this.btnModInstalled.Name = "btnModInstalled";
            this.btnModInstalled.Size = new System.Drawing.Size(24, 23);
            this.btnModInstalled.TabIndex = 17;
            this.ttOptions.SetToolTip(this.btnModInstalled, "Chose node color for nodes that are installed.");
            this.btnModInstalled.UseVisualStyleBackColor = true;
            this.btnModInstalled.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // lblColorGModIsInstalled
            // 
            this.lblColorGModIsInstalled.AutoSize = true;
            this.lblColorGModIsInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGModIsInstalled.Location = new System.Drawing.Point(233, 0);
            this.lblColorGModIsInstalled.Name = "lblColorGModIsInstalled";
            this.lblColorGModIsInstalled.Size = new System.Drawing.Size(15, 29);
            this.lblColorGModIsInstalled.TabIndex = 2;
            this.lblColorGModIsInstalled.Text = "G";
            this.lblColorGModIsInstalled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModInstalledGreen
            // 
            this.tbModInstalledGreen.DecimalSeparator = ",";
            this.tbModInstalledGreen.Location = new System.Drawing.Point(254, 5);
            this.tbModInstalledGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModInstalledGreen.Maximum = 255D;
            this.tbModInstalledGreen.MaxLength = 3;
            this.tbModInstalledGreen.Minimum = 0D;
            this.tbModInstalledGreen.Minus = "-";
            this.tbModInstalledGreen.Name = "tbModInstalledGreen";
            this.tbModInstalledGreen.Plus = "+";
            this.tbModInstalledGreen.Size = new System.Drawing.Size(27, 20);
            this.tbModInstalledGreen.TabIndex = 15;
            this.tbModInstalledGreen.Text = "128";
            this.tbModInstalledGreen.ValidCharacters = "1234567890";
            this.tbModInstalledGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorBModIsInstalled
            // 
            this.lblColorBModIsInstalled.AutoSize = true;
            this.lblColorBModIsInstalled.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBModIsInstalled.Location = new System.Drawing.Point(287, 0);
            this.lblColorBModIsInstalled.Name = "lblColorBModIsInstalled";
            this.lblColorBModIsInstalled.Size = new System.Drawing.Size(14, 29);
            this.lblColorBModIsInstalled.TabIndex = 2;
            this.lblColorBModIsInstalled.Text = "B";
            this.lblColorBModIsInstalled.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModInstalledBlue
            // 
            this.tbModInstalledBlue.DecimalSeparator = ",";
            this.tbModInstalledBlue.Location = new System.Drawing.Point(307, 5);
            this.tbModInstalledBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModInstalledBlue.Maximum = 255D;
            this.tbModInstalledBlue.MaxLength = 3;
            this.tbModInstalledBlue.Minimum = 0D;
            this.tbModInstalledBlue.Minus = "-";
            this.tbModInstalledBlue.Name = "tbModInstalledBlue";
            this.tbModInstalledBlue.Plus = "+";
            this.tbModInstalledBlue.Size = new System.Drawing.Size(27, 20);
            this.tbModInstalledBlue.TabIndex = 16;
            this.tbModInstalledBlue.Text = "0";
            this.tbModInstalledBlue.ValidCharacters = "1234567890";
            this.tbModInstalledBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel7.ColumnCount = 9;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.lblColorModArchiveMissing, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.pModArchiveMissing, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblColorRModArchiveMissing, 2, 0);
            this.tableLayoutPanel7.Controls.Add(this.btnModArchiveMissing, 8, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbModArchiveMissingRed, 3, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblColorGModArchiveMissing, 4, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbModArchiveMissingGreen, 5, 0);
            this.tableLayoutPanel7.Controls.Add(this.lblColorBModArchiveMissing, 6, 0);
            this.tableLayoutPanel7.Controls.Add(this.tbModArchiveMissingBlue, 7, 0);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(9, 143);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel7.TabIndex = 27;
            // 
            // lblColorModArchiveMissing
            // 
            this.lblColorModArchiveMissing.AutoSize = true;
            this.lblColorModArchiveMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorModArchiveMissing.Location = new System.Drawing.Point(3, 0);
            this.lblColorModArchiveMissing.Name = "lblColorModArchiveMissing";
            this.lblColorModArchiveMissing.Size = new System.Drawing.Size(144, 29);
            this.lblColorModArchiveMissing.TabIndex = 0;
            this.lblColorModArchiveMissing.Text = "Mod archive is missing:";
            this.lblColorModArchiveMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pModArchiveMissing
            // 
            this.pModArchiveMissing.BackColor = System.Drawing.Color.Red;
            this.pModArchiveMissing.Location = new System.Drawing.Point(153, 5);
            this.pModArchiveMissing.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pModArchiveMissing.Name = "pModArchiveMissing";
            this.pModArchiveMissing.Size = new System.Drawing.Size(20, 20);
            this.pModArchiveMissing.TabIndex = 1;
            // 
            // lblColorRModArchiveMissing
            // 
            this.lblColorRModArchiveMissing.AutoSize = true;
            this.lblColorRModArchiveMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRModArchiveMissing.Location = new System.Drawing.Point(179, 0);
            this.lblColorRModArchiveMissing.Name = "lblColorRModArchiveMissing";
            this.lblColorRModArchiveMissing.Size = new System.Drawing.Size(15, 29);
            this.lblColorRModArchiveMissing.TabIndex = 2;
            this.lblColorRModArchiveMissing.Text = "R";
            this.lblColorRModArchiveMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnModArchiveMissing
            // 
            this.btnModArchiveMissing.Image = ((System.Drawing.Image)(resources.GetObject("btnModArchiveMissing.Image")));
            this.btnModArchiveMissing.Location = new System.Drawing.Point(340, 3);
            this.btnModArchiveMissing.Name = "btnModArchiveMissing";
            this.btnModArchiveMissing.Size = new System.Drawing.Size(24, 23);
            this.btnModArchiveMissing.TabIndex = 21;
            this.ttOptions.SetToolTip(this.btnModArchiveMissing, "Chose node color for nodes where the mod archive is missing. ");
            this.btnModArchiveMissing.UseVisualStyleBackColor = true;
            this.btnModArchiveMissing.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // tbModArchiveMissingRed
            // 
            this.tbModArchiveMissingRed.DecimalSeparator = ",";
            this.tbModArchiveMissingRed.Location = new System.Drawing.Point(200, 5);
            this.tbModArchiveMissingRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModArchiveMissingRed.Maximum = 255D;
            this.tbModArchiveMissingRed.MaxLength = 3;
            this.tbModArchiveMissingRed.Minimum = 0D;
            this.tbModArchiveMissingRed.Minus = "-";
            this.tbModArchiveMissingRed.Name = "tbModArchiveMissingRed";
            this.tbModArchiveMissingRed.Plus = "+";
            this.tbModArchiveMissingRed.Size = new System.Drawing.Size(27, 20);
            this.tbModArchiveMissingRed.TabIndex = 18;
            this.tbModArchiveMissingRed.Text = "255";
            this.tbModArchiveMissingRed.ValidCharacters = "1234567890";
            this.tbModArchiveMissingRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorGModArchiveMissing
            // 
            this.lblColorGModArchiveMissing.AutoSize = true;
            this.lblColorGModArchiveMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGModArchiveMissing.Location = new System.Drawing.Point(233, 0);
            this.lblColorGModArchiveMissing.Name = "lblColorGModArchiveMissing";
            this.lblColorGModArchiveMissing.Size = new System.Drawing.Size(15, 29);
            this.lblColorGModArchiveMissing.TabIndex = 2;
            this.lblColorGModArchiveMissing.Text = "G";
            this.lblColorGModArchiveMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModArchiveMissingGreen
            // 
            this.tbModArchiveMissingGreen.DecimalSeparator = ",";
            this.tbModArchiveMissingGreen.Location = new System.Drawing.Point(254, 5);
            this.tbModArchiveMissingGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModArchiveMissingGreen.Maximum = 255D;
            this.tbModArchiveMissingGreen.MaxLength = 3;
            this.tbModArchiveMissingGreen.Minimum = 0D;
            this.tbModArchiveMissingGreen.Minus = "-";
            this.tbModArchiveMissingGreen.Name = "tbModArchiveMissingGreen";
            this.tbModArchiveMissingGreen.Plus = "+";
            this.tbModArchiveMissingGreen.Size = new System.Drawing.Size(27, 20);
            this.tbModArchiveMissingGreen.TabIndex = 19;
            this.tbModArchiveMissingGreen.Text = "0";
            this.tbModArchiveMissingGreen.ValidCharacters = "1234567890";
            this.tbModArchiveMissingGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorBModArchiveMissing
            // 
            this.lblColorBModArchiveMissing.AutoSize = true;
            this.lblColorBModArchiveMissing.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBModArchiveMissing.Location = new System.Drawing.Point(287, 0);
            this.lblColorBModArchiveMissing.Name = "lblColorBModArchiveMissing";
            this.lblColorBModArchiveMissing.Size = new System.Drawing.Size(14, 29);
            this.lblColorBModArchiveMissing.TabIndex = 2;
            this.lblColorBModArchiveMissing.Text = "B";
            this.lblColorBModArchiveMissing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModArchiveMissingBlue
            // 
            this.tbModArchiveMissingBlue.DecimalSeparator = ",";
            this.tbModArchiveMissingBlue.Location = new System.Drawing.Point(307, 5);
            this.tbModArchiveMissingBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModArchiveMissingBlue.Maximum = 255D;
            this.tbModArchiveMissingBlue.MaxLength = 3;
            this.tbModArchiveMissingBlue.Minimum = 0D;
            this.tbModArchiveMissingBlue.Minus = "-";
            this.tbModArchiveMissingBlue.Name = "tbModArchiveMissingBlue";
            this.tbModArchiveMissingBlue.Plus = "+";
            this.tbModArchiveMissingBlue.Size = new System.Drawing.Size(27, 20);
            this.tbModArchiveMissingBlue.TabIndex = 20;
            this.tbModArchiveMissingBlue.Text = "0";
            this.tbModArchiveMissingBlue.ValidCharacters = "1234567890";
            this.tbModArchiveMissingBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel6.ColumnCount = 9;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel6.Controls.Add(this.lblColorModOutdated, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.pModOutdated, 1, 0);
            this.tableLayoutPanel6.Controls.Add(this.btnModOutdated, 8, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblColorRModOutdated, 2, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbModOutdatedRed, 3, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblColorGModOutdated, 4, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbModOutdatedGreen, 5, 0);
            this.tableLayoutPanel6.Controls.Add(this.lblColorBModOutdated, 6, 0);
            this.tableLayoutPanel6.Controls.Add(this.tbModOutdatedBlue, 7, 0);
            this.tableLayoutPanel6.Location = new System.Drawing.Point(9, 174);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(652, 29);
            this.tableLayoutPanel6.TabIndex = 26;
            // 
            // lblColorModOutdated
            // 
            this.lblColorModOutdated.AutoSize = true;
            this.lblColorModOutdated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorModOutdated.Location = new System.Drawing.Point(3, 0);
            this.lblColorModOutdated.Name = "lblColorModOutdated";
            this.lblColorModOutdated.Size = new System.Drawing.Size(144, 29);
            this.lblColorModOutdated.TabIndex = 0;
            this.lblColorModOutdated.Text = "Mod outdated:";
            this.lblColorModOutdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pModOutdated
            // 
            this.pModOutdated.BackColor = System.Drawing.Color.Blue;
            this.pModOutdated.Location = new System.Drawing.Point(153, 5);
            this.pModOutdated.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.pModOutdated.Name = "pModOutdated";
            this.pModOutdated.Size = new System.Drawing.Size(20, 20);
            this.pModOutdated.TabIndex = 1;
            // 
            // btnModOutdated
            // 
            this.btnModOutdated.Image = ((System.Drawing.Image)(resources.GetObject("btnModOutdated.Image")));
            this.btnModOutdated.Location = new System.Drawing.Point(340, 3);
            this.btnModOutdated.Name = "btnModOutdated";
            this.btnModOutdated.Size = new System.Drawing.Size(24, 23);
            this.btnModOutdated.TabIndex = 25;
            this.ttOptions.SetToolTip(this.btnModOutdated, "Chose node color for nodes where the mod is outdated.");
            this.btnModOutdated.UseVisualStyleBackColor = true;
            this.btnModOutdated.Click += new System.EventHandler(this.ColorChangeButton_Click);
            // 
            // lblColorRModOutdated
            // 
            this.lblColorRModOutdated.AutoSize = true;
            this.lblColorRModOutdated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorRModOutdated.Location = new System.Drawing.Point(179, 0);
            this.lblColorRModOutdated.Name = "lblColorRModOutdated";
            this.lblColorRModOutdated.Size = new System.Drawing.Size(15, 29);
            this.lblColorRModOutdated.TabIndex = 2;
            this.lblColorRModOutdated.Text = "R";
            this.lblColorRModOutdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModOutdatedRed
            // 
            this.tbModOutdatedRed.DecimalSeparator = ",";
            this.tbModOutdatedRed.Location = new System.Drawing.Point(200, 5);
            this.tbModOutdatedRed.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModOutdatedRed.Maximum = 255D;
            this.tbModOutdatedRed.MaxLength = 3;
            this.tbModOutdatedRed.Minimum = 0D;
            this.tbModOutdatedRed.Minus = "-";
            this.tbModOutdatedRed.Name = "tbModOutdatedRed";
            this.tbModOutdatedRed.Plus = "+";
            this.tbModOutdatedRed.Size = new System.Drawing.Size(27, 20);
            this.tbModOutdatedRed.TabIndex = 22;
            this.tbModOutdatedRed.Text = "0";
            this.tbModOutdatedRed.ValidCharacters = "1234567890";
            this.tbModOutdatedRed.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorGModOutdated
            // 
            this.lblColorGModOutdated.AutoSize = true;
            this.lblColorGModOutdated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorGModOutdated.Location = new System.Drawing.Point(233, 0);
            this.lblColorGModOutdated.Name = "lblColorGModOutdated";
            this.lblColorGModOutdated.Size = new System.Drawing.Size(15, 29);
            this.lblColorGModOutdated.TabIndex = 2;
            this.lblColorGModOutdated.Text = "G";
            this.lblColorGModOutdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModOutdatedGreen
            // 
            this.tbModOutdatedGreen.DecimalSeparator = ",";
            this.tbModOutdatedGreen.Location = new System.Drawing.Point(254, 5);
            this.tbModOutdatedGreen.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModOutdatedGreen.Maximum = 255D;
            this.tbModOutdatedGreen.MaxLength = 3;
            this.tbModOutdatedGreen.Minimum = 0D;
            this.tbModOutdatedGreen.Minus = "-";
            this.tbModOutdatedGreen.Name = "tbModOutdatedGreen";
            this.tbModOutdatedGreen.Plus = "+";
            this.tbModOutdatedGreen.Size = new System.Drawing.Size(27, 20);
            this.tbModOutdatedGreen.TabIndex = 23;
            this.tbModOutdatedGreen.Text = "0";
            this.tbModOutdatedGreen.ValidCharacters = "1234567890";
            this.tbModOutdatedGreen.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // lblColorBModOutdated
            // 
            this.lblColorBModOutdated.AutoSize = true;
            this.lblColorBModOutdated.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblColorBModOutdated.Location = new System.Drawing.Point(287, 0);
            this.lblColorBModOutdated.Name = "lblColorBModOutdated";
            this.lblColorBModOutdated.Size = new System.Drawing.Size(14, 29);
            this.lblColorBModOutdated.TabIndex = 2;
            this.lblColorBModOutdated.Text = "B";
            this.lblColorBModOutdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbModOutdatedBlue
            // 
            this.tbModOutdatedBlue.DecimalSeparator = ",";
            this.tbModOutdatedBlue.Location = new System.Drawing.Point(307, 5);
            this.tbModOutdatedBlue.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.tbModOutdatedBlue.Maximum = 255D;
            this.tbModOutdatedBlue.MaxLength = 3;
            this.tbModOutdatedBlue.Minimum = 0D;
            this.tbModOutdatedBlue.Minus = "-";
            this.tbModOutdatedBlue.Name = "tbModOutdatedBlue";
            this.tbModOutdatedBlue.Plus = "+";
            this.tbModOutdatedBlue.Size = new System.Drawing.Size(27, 20);
            this.tbModOutdatedBlue.TabIndex = 24;
            this.tbModOutdatedBlue.Text = "200";
            this.tbModOutdatedBlue.ValidCharacters = "1234567890";
            this.tbModOutdatedBlue.TextChanged += new System.EventHandler(this.ColorTextBoxes_TextChanged);
            // 
            // gpModConflictHandling
            // 
            this.gpModConflictHandling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpModConflictHandling.Controls.Add(this.cbConflictDetectionOnOff);
            this.gpModConflictHandling.Controls.Add(this.cbShowConflictSolver);
            this.gpModConflictHandling.Location = new System.Drawing.Point(0, 124);
            this.gpModConflictHandling.Name = "gpModConflictHandling";
            this.gpModConflictHandling.Size = new System.Drawing.Size(667, 65);
            this.gpModConflictHandling.TabIndex = 2;
            this.gpModConflictHandling.TabStop = false;
            this.gpModConflictHandling.Text = "Mod file conflict handling (Experimantel):";
            // 
            // cbConflictDetectionOnOff
            // 
            this.cbConflictDetectionOnOff.AutoSize = true;
            this.cbConflictDetectionOnOff.Location = new System.Drawing.Point(9, 19);
            this.cbConflictDetectionOnOff.Name = "cbConflictDetectionOnOff";
            this.cbConflictDetectionOnOff.Size = new System.Drawing.Size(150, 17);
            this.cbConflictDetectionOnOff.TabIndex = 0;
            this.cbConflictDetectionOnOff.Text = "Conflict detection On / Off";
            this.ttOptions.SetToolTip(this.cbConflictDetectionOnOff, "Conflict detection On / Off\r\nTurns the conflict detection on or off.");
            this.cbConflictDetectionOnOff.UseVisualStyleBackColor = true;
            this.cbConflictDetectionOnOff.CheckedChanged += new System.EventHandler(this.cbConflictDetectionOnOff_CheckedChanged);
            // 
            // cbShowConflictSolver
            // 
            this.cbShowConflictSolver.AutoSize = true;
            this.cbShowConflictSolver.Location = new System.Drawing.Point(9, 42);
            this.cbShowConflictSolver.Name = "cbShowConflictSolver";
            this.cbShowConflictSolver.Size = new System.Drawing.Size(157, 17);
            this.cbShowConflictSolver.TabIndex = 1;
            this.cbShowConflictSolver.Text = "Show conflict sovling dialog";
            this.ttOptions.SetToolTip(this.cbShowConflictSolver, "Show conflict detection dialog\r\nShow/hides the conflict detection doalog after co" +
        "nflict detection.");
            this.cbShowConflictSolver.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel14.ColumnCount = 4;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.rbDDSmartDestDetection, 0, 0);
            this.tableLayoutPanel14.Controls.Add(this.rbDDJustDump, 2, 0);
            this.tableLayoutPanel14.Location = new System.Drawing.Point(9, 15);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 1;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(652, 24);
            this.tableLayoutPanel14.TabIndex = 2;
            // 
            // ucOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl2);
            this.Name = "ucOptions";
            this.Size = new System.Drawing.Size(675, 507);
            this.tabControl2.ResumeLayout(false);
            this.tabPageUpdate.ResumeLayout(false);
            this.gbModUpdate.ResumeLayout(false);
            this.gbModUpdate.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCheckModUpdate)).EndInit();
            this.gbAdminVersion.ResumeLayout(false);
            this.gbAdminVersion.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbUp2Date)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpdateLoad)).EndInit();
            this.tabPagePath.ResumeLayout(false);
            this.gbPaths.ResumeLayout(false);
            this.gbPaths.PerformLayout();
            this.tableLayoutPanel12.ResumeLayout(false);
            this.tableLayoutPanel12.PerformLayout();
            this.tlpSearchBG.ResumeLayout(false);
            this.tlpSearchBG.PerformLayout();
            this.tlpSearch.ResumeLayout(false);
            this.tlpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabPageMisc.ResumeLayout(false);
            this.gbDestinationDetection.ResumeLayout(false);
            this.gbDestinationDetection.PerformLayout();
            this.gbToolTip.ResumeLayout(false);
            this.tableLayoutPanel13.ResumeLayout(false);
            this.tableLayoutPanel13.PerformLayout();
            this.gbLaguage.ResumeLayout(false);
            this.gbNodeColors.ResumeLayout(false);
            this.tableLayoutPanel11.ResumeLayout(false);
            this.tableLayoutPanel11.PerformLayout();
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel8.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.gpModConflictHandling.ResumeLayout(false);
            this.gpModConflictHandling.PerformLayout();
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel14.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TabControl tabControl2;
        internal System.Windows.Forms.TabPage tabPageUpdate;
        internal System.Windows.Forms.TabPage tabPagePath;
        internal System.Windows.Forms.TabPage tabPageMisc;
        internal System.Windows.Forms.ImageList ilOptions;
        internal System.Windows.Forms.GroupBox gbModUpdate;
        internal System.Windows.Forms.PictureBox pbCheckModUpdate;
        internal System.Windows.Forms.ComboBox cbModUpdateBehavior;
        internal System.Windows.Forms.Label lblModUpdateBehavior;
        internal System.Windows.Forms.ComboBox cbModUpdateInterval;
        internal System.Windows.Forms.Label lblUpdateCheckInterval;
        internal System.Windows.Forms.Button btnCheckModUpdates;
        internal System.Windows.Forms.GroupBox gbAdminVersion;
        internal System.Windows.Forms.ComboBox cbPostDownloadAction;
        internal System.Windows.Forms.Label lblPostDownloadAction;
        internal System.Windows.Forms.PictureBox pbUp2Date;
        internal System.Windows.Forms.PictureBox pbUpdateLoad;
        internal System.Windows.Forms.Button btnUpdate;
        internal System.Windows.Forms.LinkLabel llblAdminDownload;
        internal System.Windows.Forms.Label lblDownloadKspMA;
        internal System.Windows.Forms.Label lblKSPModAdminVersion;
        internal System.Windows.Forms.CheckBox cbVersionCheck;
        internal System.Windows.Forms.ProgressBar prgBarAdminDownload;
        internal System.Windows.Forms.Button btnOpenDownloads;
        internal System.Windows.Forms.GroupBox gbPaths;
        internal System.Windows.Forms.ComboBox cbKSPPath;
        internal System.Windows.Forms.Label lblModDownloadPath;
        internal System.Windows.Forms.TextBox tbDownloadPath;
        internal System.Windows.Forms.Button btnDownloadPath;
        internal System.Windows.Forms.Button btnOpenDownloadFolder;
        internal System.Windows.Forms.Button btnOpenKSPRoot;
        internal System.Windows.Forms.Label lblKnownKSPPaths;
        internal System.Windows.Forms.Label lblSelectedKSPPath;
        internal System.Windows.Forms.Button btnRemove;
        internal System.Windows.Forms.Button btnSteamSearch;
        internal System.Windows.Forms.Button btnKSPFolderSearch;
        internal System.Windows.Forms.Button btnAddPath;
        internal System.Windows.Forms.TableLayoutPanel tlpSearchBG;
        internal System.Windows.Forms.TableLayoutPanel tlpSearch;
        internal System.Windows.Forms.Label lblLoading;
        internal System.Windows.Forms.PictureBox picLoading;
        internal System.Windows.Forms.GroupBox gbNodeColors;
        internal System.Windows.Forms.Button btnDestinationConflict;
        internal System.Windows.Forms.Button btnModOutdated;
        internal System.Windows.Forms.Button btnModArchiveMissing;
        internal System.Windows.Forms.Button btnModInstalled;
        internal System.Windows.Forms.Button btnDestinationMissing;
        internal TextBoxNumeric tbDestinationConflictBlue;
        internal TextBoxNumeric tbModOutdatedBlue;
        internal TextBoxNumeric tbModArchiveMissingBlue;
        internal TextBoxNumeric tbModInstalledBlue;
        internal System.Windows.Forms.Button btnDestinationDetected;
        internal TextBoxNumeric tbDestinationMissingBlue;
        internal System.Windows.Forms.Label lblColorBDestinationConflict;
        internal System.Windows.Forms.Label lblColorBModOutdated;
        internal System.Windows.Forms.Label lblColorBModArchiveMissing;
        internal System.Windows.Forms.Label lblColorBModIsInstalled;
        internal TextBoxNumeric tbDestinationDetectedBlue;
        internal System.Windows.Forms.Label lblColorBDestinationMissing;
        internal TextBoxNumeric tbDestinationConflictGreen;
        internal TextBoxNumeric tbModOutdatedGreen;
        internal TextBoxNumeric tbModArchiveMissingGreen;
        internal TextBoxNumeric tbModInstalledGreen;
        internal System.Windows.Forms.Label lblColorBDestinationDetection;
        internal TextBoxNumeric tbDestinationMissingGreen;
        internal System.Windows.Forms.Label lblColorGDestinationConflict;
        internal System.Windows.Forms.Label lblColorGModOutdated;
        internal System.Windows.Forms.Label lblColorGModArchiveMissing;
        internal System.Windows.Forms.Label lblColorGModIsInstalled;
        internal TextBoxNumeric tbDestinationDetectedGreen;
        internal System.Windows.Forms.Label lblColorGDestinationMissing;
        internal TextBoxNumeric tbDestinationConflictRed;
        internal TextBoxNumeric tbModOutdatedRed;
        internal System.Windows.Forms.Label lblColorRDestinationConflict;
        internal TextBoxNumeric tbModArchiveMissingRed;
        internal System.Windows.Forms.Label lblColorRModOutdated;
        internal TextBoxNumeric tbModInstalledRed;
        internal System.Windows.Forms.Label lblColorRModArchiveMissing;
        internal System.Windows.Forms.Label lblColorGDestinationDetection;
        internal System.Windows.Forms.Panel pDestinationConflict;
        internal System.Windows.Forms.Label lblColorRModIsInstalled;
        internal System.Windows.Forms.Panel pModOutdated;
        internal TextBoxNumeric tbDestinationMissingRed;
        internal System.Windows.Forms.Label lblColorRDestinationMissing;
        internal System.Windows.Forms.Panel pModInstalled;
        internal TextBoxNumeric tbDestinationDetectedRed;
        internal System.Windows.Forms.Panel pDestinationMissing;
        internal System.Windows.Forms.Label lblColorRDestinationDetection;
        internal System.Windows.Forms.Panel pDestinationDetected;
        internal System.Windows.Forms.Label lblColorModOutdated;
        internal System.Windows.Forms.Label lblColorModIsInstalled;
        internal System.Windows.Forms.Label lblColorModArchiveMissing;
        internal System.Windows.Forms.Label lblColorDestinationConflicts;
        internal System.Windows.Forms.Label lblColorDestinationMissing;
        internal System.Windows.Forms.Label lblColorDestinationDetection;
        internal System.Windows.Forms.GroupBox gpModConflictHandling;
        internal System.Windows.Forms.CheckBox cbConflictDetectionOnOff;
        internal System.Windows.Forms.CheckBox cbShowConflictSolver;
        internal System.Windows.Forms.Label lblKSPPathNote;
        internal System.Windows.Forms.TextBox tbNote;
        internal System.Windows.Forms.TreeView tvKnownPaths;
        internal TextBoxNumeric tbDepth;
        internal System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbLaguage;
        private System.Windows.Forms.ComboBox cbLanguages;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        internal System.Windows.Forms.Panel pModArchiveMissing;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.ToolTip ttOptions;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel12;
        private System.Windows.Forms.GroupBox gbToolTip;
        private System.Windows.Forms.Label lblSecond2;
        private System.Windows.Forms.Label lblToolTipDisplayTime;
        private System.Windows.Forms.Label lblSecond1;
        private Utils.Controls.Aga.Controls.NumericTextBox tbToolTipDisplayTime;
        private System.Windows.Forms.Label lblToolTipDelay;
        private Utils.Controls.Aga.Controls.NumericTextBox tbToolTipDelay;
        private System.Windows.Forms.CheckBox cbToolTipOnOff;
        private System.Windows.Forms.CheckBox cbDeleteOldArchive;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel13;
        private System.Windows.Forms.GroupBox gbDestinationDetection;
        private System.Windows.Forms.RadioButton rbDDJustDump;
        private System.Windows.Forms.RadioButton rbDDSmartDestDetection;
        private System.Windows.Forms.CheckBox cbDDCopyToGameData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
    }
}
