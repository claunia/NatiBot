namespace bot.GUI
{
    partial class frmAvatars
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAvatars));
            this.lstAvatars = new bot.GUI.AvatarList();
            this.lblCurrentSim = new System.Windows.Forms.Label();
            this.lblAvatars = new System.Windows.Forms.Label();
            this.btnClose = new clControls.clImageButton();
            this.SuspendLayout();
            // 
            // lstAvatars
            // 
            this.lstAvatars.BackColor = System.Drawing.Color.Black;
            this.lstAvatars.Client = null;
            this.lstAvatars.ForeColor = System.Drawing.Color.White;
            this.lstAvatars.FullRowSelect = true;
            this.lstAvatars.Location = new System.Drawing.Point(24, 25);
            this.lstAvatars.MultiSelect = false;
            this.lstAvatars.Name = "lstAvatars";
            this.lstAvatars.Size = new System.Drawing.Size(458, 210);
            this.lstAvatars.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstAvatars.TabIndex = 0;
            this.lstAvatars.UseCompatibleStateImageBehavior = false;
            this.lstAvatars.View = System.Windows.Forms.View.Details;
            this.lstAvatars.OnAvatarAdded += new bot.GUI.AvatarList.AvatarCallback(this.lstAvatars_OnAvatarAdded);
            this.lstAvatars.OnAvatarRemoved += new bot.GUI.AvatarList.AvatarCallback(this.lstAvatars_OnAvatarRemoved);
            // 
            // lblCurrentSim
            // 
            this.lblCurrentSim.AutoSize = true;
            this.lblCurrentSim.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentSim.ForeColor = System.Drawing.Color.White;
            this.lblCurrentSim.Location = new System.Drawing.Point(31, 9);
            this.lblCurrentSim.Name = "lblCurrentSim";
            this.lblCurrentSim.Size = new System.Drawing.Size(91, 13);
            this.lblCurrentSim.TabIndex = 1;
            this.lblCurrentSim.Text = "Current simulator: ";
            // 
            // lblAvatars
            // 
            this.lblAvatars.AutoSize = true;
            this.lblAvatars.BackColor = System.Drawing.Color.Transparent;
            this.lblAvatars.ForeColor = System.Drawing.Color.White;
            this.lblAvatars.Location = new System.Drawing.Point(31, 238);
            this.lblAvatars.Name = "lblAvatars";
            this.lblAvatars.Size = new System.Drawing.Size(92, 13);
            this.lblAvatars.TabIndex = 2;
            this.lblAvatars.Text = "{0} avatars found.";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(458, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmAvatars
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAvatars");
            this.ClientSize = new System.Drawing.Size(508, 258);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAvatars);
            this.Controls.Add(this.lblCurrentSim);
            this.Controls.Add(this.lstAvatars);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAvatars";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmAvatars";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmAvatars_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmAvatars_MouseMove);
            this.Load += new System.EventHandler(this.frmAvatars_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private bot.GUI.AvatarList lstAvatars;
        private System.Windows.Forms.Label lblCurrentSim;
        private System.Windows.Forms.Label lblAvatars;
        private clControls.clImageButton btnClose;
    }
}