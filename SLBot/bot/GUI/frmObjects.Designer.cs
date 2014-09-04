namespace bot.GUI
{
    partial class frmObjects
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnRefresh = new clControls.clImageButton();
            this.lblObjects = new System.Windows.Forms.Label();
            this.btnExport = new clControls.clImageButton();
            this.btnExportAll = new clControls.clImageButton();
            this.chkRefresh = new System.Windows.Forms.CheckBox();
            this.cmsObjects = new System.Windows.Forms.ContextMenuStrip();
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.namesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uUIDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.sitOnItToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firstSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.touchThemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyThemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.requestTakeToInventoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.particlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectAndParticlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrRefreshTimer = new System.Windows.Forms.Timer();
            this.prgExporting = new System.Windows.Forms.ProgressBar();
            this.txtMeters = new System.Windows.Forms.TextBox();
            this.lblDistance = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new clControls.clImageButton();
            this.lblSearch = new System.Windows.Forms.Label();
            this.lblfrmObjects = new System.Windows.Forms.Label();
            this.btnClose = new clControls.clImageButton();
            this.lstObjects = new bot.GUI.ObjectList();
            this.cmsObjects.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(30, 411);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.ButtonBitmap = null;
            this.btnRefresh.ButtonState = clControls.ButtonState.Normal;
            this.btnRefresh.DisabledBitmap = null;
            this.btnRefresh.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnRefresh.idle");
            this.btnRefresh.Location = new System.Drawing.Point(115, 370);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.OnMouseClickBitmap = null;
            this.btnRefresh.OnMouseOverBitmap = null;
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // lblObjects
            // 
            this.lblObjects.AutoSize = true;
            this.lblObjects.BackColor = System.Drawing.Color.Transparent;
            this.lblObjects.ForeColor = System.Drawing.Color.White;
            this.lblObjects.Location = new System.Drawing.Point(9, 398);
            this.lblObjects.Name = "lblObjects";
            this.lblObjects.Size = new System.Drawing.Size(0, 13);
            this.lblObjects.TabIndex = 3;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.ButtonBitmap = null;
            this.btnExport.ButtonState = clControls.ButtonState.Normal;
            this.btnExport.DisabledBitmap = null;
            this.btnExport.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnExport.idle");
            this.btnExport.Location = new System.Drawing.Point(196, 370);
            this.btnExport.Name = "btnExport";
            this.btnExport.OnMouseClickBitmap = null;
            this.btnExport.OnMouseOverBitmap = null;
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 4;
            this.btnExport.TabStop = false;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportAll
            // 
            this.btnExportAll.BackColor = System.Drawing.Color.Transparent;
            this.btnExportAll.ButtonBitmap = null;
            this.btnExportAll.ButtonState = clControls.ButtonState.Normal;
            this.btnExportAll.DisabledBitmap = null;
            this.btnExportAll.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnExportAll.idle");
            this.btnExportAll.Location = new System.Drawing.Point(277, 370);
            this.btnExportAll.Name = "btnExportAll";
            this.btnExportAll.OnMouseClickBitmap = null;
            this.btnExportAll.OnMouseOverBitmap = null;
            this.btnExportAll.Size = new System.Drawing.Size(75, 23);
            this.btnExportAll.TabIndex = 5;
            this.btnExportAll.TabStop = false;
            this.btnExportAll.Text = "Export All";
            this.btnExportAll.Click += new System.EventHandler(this.btnExportAll_Click);
            // 
            // chkRefresh
            // 
            this.chkRefresh.AutoSize = true;
            this.chkRefresh.BackColor = System.Drawing.Color.Transparent;
            this.chkRefresh.Checked = true;
            this.chkRefresh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRefresh.ForeColor = System.Drawing.Color.White;
            this.chkRefresh.Location = new System.Drawing.Point(358, 374);
            this.chkRefresh.Name = "chkRefresh";
            this.chkRefresh.Size = new System.Drawing.Size(150, 17);
            this.chkRefresh.TabIndex = 6;
            this.chkRefresh.Text = "Refresh every 10 seconds";
            this.chkRefresh.UseVisualStyleBackColor = false;
            this.chkRefresh.CheckedChanged += new System.EventHandler(this.chkRefresh_CheckedChanged);
            // 
            // cmsObjects
            // 
            this.cmsObjects.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.toolStripSeparator1,
            this.sitOnItToolStripMenuItem,
            this.touchThemToolStripMenuItem,
            this.buyThemToolStripMenuItem,
            this.requestTakeToInventoryToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportToolStripMenuItem});
            this.cmsObjects.Name = "cmsObjects";
            this.cmsObjects.Size = new System.Drawing.Size(209, 148);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.namesToolStripMenuItem,
            this.uUIDsToolStripMenuItem,
            this.locationsToolStripMenuItem});
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to clipboard";
            // 
            // namesToolStripMenuItem
            // 
            this.namesToolStripMenuItem.Name = "namesToolStripMenuItem";
            this.namesToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.namesToolStripMenuItem.Text = "Names";
            this.namesToolStripMenuItem.Click += new System.EventHandler(this.namesToolStripMenuItem_Click);
            // 
            // uUIDsToolStripMenuItem
            // 
            this.uUIDsToolStripMenuItem.Name = "uUIDsToolStripMenuItem";
            this.uUIDsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.uUIDsToolStripMenuItem.Text = "UUIDs";
            this.uUIDsToolStripMenuItem.Click += new System.EventHandler(this.UUIDsToolStripMenuItem_Click);
            // 
            // locationsToolStripMenuItem
            // 
            this.locationsToolStripMenuItem.Name = "locationsToolStripMenuItem";
            this.locationsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.locationsToolStripMenuItem.Text = "Locations";
            this.locationsToolStripMenuItem.Click += new System.EventHandler(this.locationsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(205, 6);
            // 
            // sitOnItToolStripMenuItem
            // 
            this.sitOnItToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstSelectedToolStripMenuItem,
            this.allSelectedToolStripMenuItem});
            this.sitOnItToolStripMenuItem.Name = "sitOnItToolStripMenuItem";
            this.sitOnItToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sitOnItToolStripMenuItem.Text = "Sit on";
            // 
            // firstSelectedToolStripMenuItem
            // 
            this.firstSelectedToolStripMenuItem.Name = "firstSelectedToolStripMenuItem";
            this.firstSelectedToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.firstSelectedToolStripMenuItem.Text = "First selected";
            this.firstSelectedToolStripMenuItem.Click += new System.EventHandler(this.firstSelectedToolStripMenuItem_Click);
            // 
            // allSelectedToolStripMenuItem
            // 
            this.allSelectedToolStripMenuItem.Name = "allSelectedToolStripMenuItem";
            this.allSelectedToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.allSelectedToolStripMenuItem.Text = "All selected";
            this.allSelectedToolStripMenuItem.Click += new System.EventHandler(this.allSelectedToolStripMenuItem_Click);
            // 
            // touchThemToolStripMenuItem
            // 
            this.touchThemToolStripMenuItem.Name = "touchThemToolStripMenuItem";
            this.touchThemToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.touchThemToolStripMenuItem.Text = "Touch them";
            this.touchThemToolStripMenuItem.Click += new System.EventHandler(this.touchThemToolStripMenuItem_Click);
            // 
            // buyThemToolStripMenuItem
            // 
            this.buyThemToolStripMenuItem.Name = "buyThemToolStripMenuItem";
            this.buyThemToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.buyThemToolStripMenuItem.Text = "Buy them";
            this.buyThemToolStripMenuItem.Click += new System.EventHandler(this.buyThemToolStripMenuItem_Click);
            // 
            // requestTakeToInventoryToolStripMenuItem
            // 
            this.requestTakeToInventoryToolStripMenuItem.Name = "requestTakeToInventoryToolStripMenuItem";
            this.requestTakeToInventoryToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.requestTakeToInventoryToolStripMenuItem.Text = "Request take to inventory";
            this.requestTakeToInventoryToolStripMenuItem.Click += new System.EventHandler(this.requestTakeToInventoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectToolStripMenuItem,
            this.particlesToolStripMenuItem,
            this.objectAndParticlesToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.exportToolStripMenuItem.Text = "Export";
            // 
            // objectToolStripMenuItem
            // 
            this.objectToolStripMenuItem.Name = "objectToolStripMenuItem";
            this.objectToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.objectToolStripMenuItem.Text = "Objects";
            this.objectToolStripMenuItem.Click += new System.EventHandler(this.objectToolStripMenuItem_Click);
            // 
            // particlesToolStripMenuItem
            // 
            this.particlesToolStripMenuItem.Name = "particlesToolStripMenuItem";
            this.particlesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.particlesToolStripMenuItem.Text = "Particles";
            this.particlesToolStripMenuItem.Click += new System.EventHandler(this.particlesToolStripMenuItem_Click);
            // 
            // objectAndParticlesToolStripMenuItem
            // 
            this.objectAndParticlesToolStripMenuItem.Name = "objectAndParticlesToolStripMenuItem";
            this.objectAndParticlesToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.objectAndParticlesToolStripMenuItem.Text = "Object and particles";
            this.objectAndParticlesToolStripMenuItem.Click += new System.EventHandler(this.objectAndParticlesToolStripMenuItem_Click);
            // 
            // tmrRefreshTimer
            // 
            this.tmrRefreshTimer.Enabled = true;
            this.tmrRefreshTimer.Interval = 120000;
            this.tmrRefreshTimer.Tick += new System.EventHandler(this.tmrRefreshTimer_Tick);
            // 
            // prgExporting
            // 
            this.prgExporting.Location = new System.Drawing.Point(33, 427);
            this.prgExporting.Name = "prgExporting";
            this.prgExporting.Size = new System.Drawing.Size(461, 16);
            this.prgExporting.TabIndex = 7;
            this.prgExporting.Visible = false;
            // 
            // txtMeters
            // 
            this.txtMeters.BackColor = System.Drawing.Color.Black;
            this.txtMeters.ForeColor = System.Drawing.Color.White;
            this.txtMeters.Location = new System.Drawing.Point(62, 370);
            this.txtMeters.Name = "txtMeters";
            this.txtMeters.Size = new System.Drawing.Size(47, 20);
            this.txtMeters.TabIndex = 8;
            this.txtMeters.TextChanged += new System.EventHandler(this.txtMeters_TextChanged);
            // 
            // lblDistance
            // 
            this.lblDistance.AutoSize = true;
            this.lblDistance.BackColor = System.Drawing.Color.Transparent;
            this.lblDistance.ForeColor = System.Drawing.Color.White;
            this.lblDistance.Location = new System.Drawing.Point(12, 373);
            this.lblDistance.Name = "lblDistance";
            this.lblDistance.Size = new System.Drawing.Size(49, 13);
            this.lblDistance.TabIndex = 9;
            this.lblDistance.Text = "Distance";
            // 
            // txtSearch
            // 
            this.txtSearch.BackColor = System.Drawing.Color.Black;
            this.txtSearch.ForeColor = System.Drawing.Color.White;
            this.txtSearch.Location = new System.Drawing.Point(62, 344);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(370, 20);
            this.txtSearch.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.ButtonBitmap = null;
            this.btnSearch.ButtonState = clControls.ButtonState.Normal;
            this.btnSearch.DisabledBitmap = null;
            this.btnSearch.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnFindNext.idle");
            this.btnSearch.Location = new System.Drawing.Point(438, 342);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.OnMouseClickBitmap = null;
            this.btnSearch.OnMouseOverBitmap = null;
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 11;
            this.btnSearch.TabStop = false;
            this.btnSearch.Text = "Find next";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.BackColor = System.Drawing.Color.Transparent;
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(12, 347);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(44, 13);
            this.lblSearch.TabIndex = 12;
            this.lblSearch.Text = "Search:";
            // 
            // lblfrmObjects
            // 
            this.lblfrmObjects.AutoSize = true;
            this.lblfrmObjects.BackColor = System.Drawing.Color.Transparent;
            this.lblfrmObjects.Font = new System.Drawing.Font("Lucida Handwriting", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfrmObjects.ForeColor = System.Drawing.Color.White;
            this.lblfrmObjects.Location = new System.Drawing.Point(29, 9);
            this.lblfrmObjects.Name = "lblfrmObjects";
            this.lblfrmObjects.Size = new System.Drawing.Size(69, 19);
            this.lblfrmObjects.TabIndex = 13;
            this.lblfrmObjects.Text = "Objects";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Location = new System.Drawing.Point(470, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 14;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstObjects
            // 
            this.lstObjects.BackColor = System.Drawing.Color.Black;
            this.lstObjects.Client = null;
            this.lstObjects.ContextMenuStrip = this.cmsObjects;
            this.lstObjects.ForeColor = System.Drawing.Color.White;
            this.lstObjects.FullRowSelect = true;
            this.lstObjects.Location = new System.Drawing.Point(12, 47);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(501, 291);
            this.lstObjects.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstObjects.TabIndex = 0;
            this.lstObjects.UseCompatibleStateImageBehavior = false;
            this.lstObjects.View = System.Windows.Forms.View.Details;
            // 
            // frmObjects
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmObjects");
            this.ClientSize = new System.Drawing.Size(525, 455);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblfrmObjects);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.lblDistance);
            this.Controls.Add(this.txtMeters);
            this.Controls.Add(this.prgExporting);
            this.Controls.Add(this.chkRefresh);
            this.Controls.Add(this.btnExportAll);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.lblObjects);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lstObjects);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmObjects";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Objects";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmObjects_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmObjects_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmObjects_MouseMove);
            this.cmsObjects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private clControls.clImageButton btnRefresh;
        private System.Windows.Forms.Label lblObjects;
        private clControls.clImageButton btnExport;
        private clControls.clImageButton btnExportAll;
        private System.Windows.Forms.CheckBox chkRefresh;
        private System.Windows.Forms.Timer tmrRefreshTimer;
        private System.Windows.Forms.ProgressBar prgExporting;
        private System.Windows.Forms.TextBox txtMeters;
        private System.Windows.Forms.Label lblDistance;
        private System.Windows.Forms.TextBox txtSearch;
        private clControls.clImageButton btnSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Label lblfrmObjects;
        private clControls.clImageButton btnClose;
        private System.Windows.Forms.ContextMenuStrip cmsObjects;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem namesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uUIDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem sitOnItToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem firstSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem touchThemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyThemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestTakeToInventoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem particlesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem objectAndParticlesToolStripMenuItem;
        private bot.GUI.ObjectList lstObjects;
    }
}