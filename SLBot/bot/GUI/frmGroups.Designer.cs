namespace bot.GUI
{
    partial class frmGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGroups));
            this.btnMessage = new clControls.clImageButton();
            this.btnActivate = new clControls.clImageButton();
            this.btnLeave = new clControls.clImageButton();
            this.lstGroups = new bot.GUI.GroupList();
            this.btnClose = new clControls.clImageButton();
            this.SuspendLayout();
            // 
            // btnMessage
            // 
            this.btnMessage.Location = new System.Drawing.Point(278, 54);
            this.btnMessage.Name = "btnMessage";
            this.btnMessage.Size = new System.Drawing.Size(75, 23);
            this.btnMessage.TabIndex = 1;
            this.btnMessage.Text = "Message";
            this.btnMessage.Click += new System.EventHandler(this.btnMessage_Click);
            this.btnMessage.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnActivate
            // 
            this.btnActivate.Location = new System.Drawing.Point(278, 83);
            this.btnActivate.Name = "btnActivate";
            this.btnActivate.Size = new System.Drawing.Size(75, 23);
            this.btnActivate.TabIndex = 2;
            this.btnActivate.Text = "Activate";
            this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
            this.btnActivate.BackColor = System.Drawing.Color.Transparent;
            // 
            // btnLeave
            // 
            this.btnLeave.Location = new System.Drawing.Point(278, 112);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(75, 23);
            this.btnLeave.TabIndex = 3;
            this.btnLeave.Text = "Leave";
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            this.btnLeave.BackColor = System.Drawing.Color.Transparent;
            // 
            // lstGroups
            // 
            this.lstGroups.BackColor = System.Drawing.Color.Black;
            this.lstGroups.Client = null;
            this.lstGroups.ForeColor = System.Drawing.Color.White;
            this.lstGroups.Location = new System.Drawing.Point(21, 24);
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.Size = new System.Drawing.Size(251, 223);
            this.lstGroups.TabIndex = 0;
            this.lstGroups.UseCompatibleStateImageBehavior = false;
            this.lstGroups.View = System.Windows.Forms.View.Details;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(329, 24);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmGroups
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmGroups");
            this.ClientSize = new System.Drawing.Size(365, 270);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.btnActivate);
            this.Controls.Add(this.btnMessage);
            this.Controls.Add(this.lstGroups);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGroups";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmGroups_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmGroups_MouseMove);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Groups";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.ResumeLayout(false);

        }
        #endregion

        private bot.GUI.GroupList lstGroups;
        private clControls.clImageButton btnMessage;
        private clControls.clImageButton btnActivate;
        private clControls.clImageButton btnLeave;
        private clControls.clImageButton btnClose;
    }
}