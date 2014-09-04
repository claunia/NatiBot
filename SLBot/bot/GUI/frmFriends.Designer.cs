namespace bot.GUI
{
    partial class frmFriends
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFriends));
            this.lstFriends = new System.Windows.Forms.ListView();
            this.btnMessage = new clControls.clImageButton();
            this.btnProfile = new clControls.clImageButton();
            this.btnTeleport = new clControls.clImageButton();
            this.btnRemove = new clControls.clImageButton();
            this.btnClose = new clControls.clImageButton();
            this.SuspendLayout();
            // 
            // lstFriends
            // 
            this.lstFriends.BackColor = System.Drawing.Color.Black;
            this.lstFriends.ForeColor = System.Drawing.Color.White;
            this.lstFriends.FullRowSelect = true;
            this.lstFriends.Location = new System.Drawing.Point(22, 28);
            this.lstFriends.MultiSelect = false;
            this.lstFriends.Name = "lstFriends";
            this.lstFriends.Size = new System.Drawing.Size(285, 268);
            this.lstFriends.TabIndex = 0;
            this.lstFriends.UseCompatibleStateImageBehavior = false;
            // 
            // btnMessage
            // 
            this.btnMessage.Location = new System.Drawing.Point(313, 58);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(75, 23);
            this.btnMessage.TabIndex = 1;
            this.btnMessage.Text = "Message";
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            this.btnMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnProfile
            // 
            this.btnProfile.Location = new System.Drawing.Point(313, 87);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(75, 23);
            this.btnProfile.TabIndex = 2;
            this.btnProfile.Text = "Profile";
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            this.btnProfile.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnTeleport
            // 
            this.btnTeleport.Location = new System.Drawing.Point(313, 116);
            this.btnTeleport.Name = "btnTeleport";
            this.btnTeleport.Size = new System.Drawing.Size(75, 23);
            this.btnTeleport.TabIndex = 3;
            this.btnTeleport.Text = "Teleport";
            this.btnTeleport.Click += new System.EventHandler(this.btnTeleport_Click);
            this.btnTeleport.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(313, 145);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(364, 28);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmFriends
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmFriends");
            this.ClientSize = new System.Drawing.Size(400, 325);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnTeleport);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.lstFriends);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFriends";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmFriends";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmFriends_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmFriends_MouseMove);
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmFriends_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFriends_FormClosing);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ListView lstFriends;
        private clControls.clImageButton btnMessage;
        private clControls.clImageButton btnProfile;
        private clControls.clImageButton btnTeleport;
        private clControls.clImageButton btnRemove;
        private clControls.clImageButton btnClose;

    }
}