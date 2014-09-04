namespace bot.GUI
{
	partial class frmInventory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInventory));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyObjectUUIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteObjectFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.wearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.emptyTrashToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emptyLostFoundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tvInventory = new bot.GUI.InventoryTree();
            this.btnClose = new clControls.clImageButton();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(275, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyObjectUUIDToolStripMenuItem,
            this.deleteObjectFolderToolStripMenuItem,
            this.toolStripMenuItem1,
            this.wearToolStripMenuItem,
            this.detachToolStripMenuItem,
            this.toolStripMenuItem2,
            this.emptyTrashToolStripMenuItem,
            this.emptyLostFoundToolStripMenuItem});
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // copyObjectUUIDToolStripMenuItem
            // 
            this.copyObjectUUIDToolStripMenuItem.Name = "copyObjectUUIDToolStripMenuItem";
            this.copyObjectUUIDToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.copyObjectUUIDToolStripMenuItem.Text = "Copy Asset UUID";
            this.copyObjectUUIDToolStripMenuItem.Click += new System.EventHandler(this.copyObjectUUIDToolStripMenuItem_Click);
            // 
            // deleteObjectFolderToolStripMenuItem
            // 
            this.deleteObjectFolderToolStripMenuItem.Name = "deleteObjectFolderToolStripMenuItem";
            this.deleteObjectFolderToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.deleteObjectFolderToolStripMenuItem.Text = "Delete Object/Folder";
            this.deleteObjectFolderToolStripMenuItem.Click += new System.EventHandler(this.deleteObjectFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(180, 6);
            // 
            // wearToolStripMenuItem
            // 
            this.wearToolStripMenuItem.Name = "wearToolStripMenuItem";
            this.wearToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.wearToolStripMenuItem.Text = "Attach";
            this.wearToolStripMenuItem.Click += new System.EventHandler(this.wearToolStripMenuItem_Click);
            // 
            // detachToolStripMenuItem
            // 
            this.detachToolStripMenuItem.Name = "detachToolStripMenuItem";
            this.detachToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.detachToolStripMenuItem.Text = "Detach";
            this.detachToolStripMenuItem.Click += new System.EventHandler(this.detachToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(180, 6);
            // 
            // emptyTrashToolStripMenuItem
            // 
            this.emptyTrashToolStripMenuItem.Name = "emptyTrashToolStripMenuItem";
            this.emptyTrashToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.emptyTrashToolStripMenuItem.Text = "Empty trash";
            this.emptyTrashToolStripMenuItem.Click += new System.EventHandler(this.emptyTrashToolStripMenuItem_Click);
            // 
            // emptyLostFoundToolStripMenuItem
            // 
            this.emptyLostFoundToolStripMenuItem.Name = "emptyLostFoundToolStripMenuItem";
            this.emptyLostFoundToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.emptyLostFoundToolStripMenuItem.Text = "Empty lost && found";
            this.emptyLostFoundToolStripMenuItem.Click += new System.EventHandler(this.emptyLostFoundToolStripMenuItem_Click);
            // 
            // tvInventory
            // 
            this.tvInventory.BackColor = System.Drawing.Color.Black;
            this.tvInventory.Client = null;
            this.tvInventory.ForeColor = System.Drawing.Color.White;
            this.tvInventory.Location = new System.Drawing.Point(23, 27);
            this.tvInventory.Name = "tvInventory";
            this.tvInventory.Size = new System.Drawing.Size(231, 511);
            this.tvInventory.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(230, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmInventory
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmInventory");
            this.ClientSize = new System.Drawing.Size(275, 550);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tvInventory);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInventory";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventory";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmInventory_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmInventory_MouseDown);
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmInventory_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyObjectUUIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteObjectFolderToolStripMenuItem;
        private InventoryTree tvInventory;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem emptyTrashToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emptyLostFoundToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem wearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detachToolStripMenuItem;
        private clControls.clImageButton btnClose;
	}
}