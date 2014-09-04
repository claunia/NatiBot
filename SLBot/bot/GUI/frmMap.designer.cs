namespace bot.GUI
{
    partial class frmMap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMap));
            this.txtSLUrl = new System.Windows.Forms.TextBox();
            this.lblMapDownloading = new System.Windows.Forms.Label();
            this.lblZ = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.btnTeleport = new clControls.clImageButton();
            this.nudZ = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedPosition = new System.Windows.Forms.Label();
            this.picMap = new System.Windows.Forms.PictureBox();
            this.lblAvatars = new System.Windows.Forms.Label();
            this.lblSim = new System.Windows.Forms.Label();
            this.btnClose = new clControls.clImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSLUrl
            // 
            this.txtSLUrl.BackColor = System.Drawing.Color.Black;
            this.txtSLUrl.ForeColor = System.Drawing.Color.White;
            this.txtSLUrl.Location = new System.Drawing.Point(19, 335);
            this.txtSLUrl.Name = "txtSLUrl";
            this.txtSLUrl.ReadOnly = true;
            this.txtSLUrl.Size = new System.Drawing.Size(279, 20);
            this.txtSLUrl.TabIndex = 12;
            // 
            // lblMapDownloading
            // 
            this.lblMapDownloading.BackColor = System.Drawing.Color.Transparent;
            this.lblMapDownloading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMapDownloading.ForeColor = System.Drawing.Color.White;
            this.lblMapDownloading.Location = new System.Drawing.Point(99, 150);
            this.lblMapDownloading.Name = "lblMapDownloading";
            this.lblMapDownloading.Size = new System.Drawing.Size(111, 37);
            this.lblMapDownloading.TabIndex = 11;
            this.lblMapDownloading.Text = "MAP downloading...";
            this.lblMapDownloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblZ
            // 
            this.lblZ.AutoSize = true;
            this.lblZ.BackColor = System.Drawing.Color.Transparent;
            this.lblZ.ForeColor = System.Drawing.Color.White;
            this.lblZ.Location = new System.Drawing.Point(142, 382);
            this.lblZ.Name = "lblZ";
            this.lblZ.Size = new System.Drawing.Size(14, 13);
            this.lblZ.TabIndex = 10;
            this.lblZ.Text = "Z";
            // 
            // lblY
            // 
            this.lblY.AutoSize = true;
            this.lblY.BackColor = System.Drawing.Color.Transparent;
            this.lblY.ForeColor = System.Drawing.Color.White;
            this.lblY.Location = new System.Drawing.Point(82, 382);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(14, 13);
            this.lblY.TabIndex = 9;
            this.lblY.Text = "Y";
            // 
            // lblX
            // 
            this.lblX.AutoSize = true;
            this.lblX.BackColor = System.Drawing.Color.Transparent;
            this.lblX.ForeColor = System.Drawing.Color.White;
            this.lblX.Location = new System.Drawing.Point(21, 382);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(14, 13);
            this.lblX.TabIndex = 8;
            this.lblX.Text = "X";
            // 
            // btnTeleport
            // 
            this.btnTeleport.Location = new System.Drawing.Point(216, 375);
            this.btnTeleport.Name = "btnTeleport";
            this.btnTeleport.Size = new System.Drawing.Size(75, 23);
            this.btnTeleport.TabIndex = 6;
            this.btnTeleport.Text = "Teleport";
            this.btnTeleport.Click += new System.EventHandler(this.btnTeleport_Click);
            this.btnTeleport.BackColor = System.Drawing.Color.Transparent;
            // 
            // nudZ
            // 
            this.nudZ.BackColor = System.Drawing.Color.Black;
            this.nudZ.ForeColor = System.Drawing.Color.White;
            this.nudZ.Location = new System.Drawing.Point(159, 378);
            this.nudZ.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudZ.Name = "nudZ";
            this.nudZ.Size = new System.Drawing.Size(51, 20);
            this.nudZ.TabIndex = 5;
            // 
            // nudY
            // 
            this.nudY.BackColor = System.Drawing.Color.Black;
            this.nudY.ForeColor = System.Drawing.Color.White;
            this.nudY.Location = new System.Drawing.Point(99, 378);
            this.nudY.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(39, 20);
            this.nudY.TabIndex = 4;
            // 
            // nudX
            // 
            this.nudX.BackColor = System.Drawing.Color.Black;
            this.nudX.ForeColor = System.Drawing.Color.White;
            this.nudX.Location = new System.Drawing.Point(39, 378);
            this.nudX.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(39, 20);
            this.nudX.TabIndex = 3;
            // 
            // lblSelectedPosition
            // 
            this.lblSelectedPosition.AutoSize = true;
            this.lblSelectedPosition.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedPosition.ForeColor = System.Drawing.Color.White;
            this.lblSelectedPosition.Location = new System.Drawing.Point(19, 362);
            this.lblSelectedPosition.Name = "lblSelectedPosition";
            this.lblSelectedPosition.Size = new System.Drawing.Size(91, 13);
            this.lblSelectedPosition.TabIndex = 2;
            this.lblSelectedPosition.Text = "Selected position:";
            // 
            // picMap
            // 
            this.picMap.BackColor = System.Drawing.Color.Transparent;
            this.picMap.Location = new System.Drawing.Point(19, 49);
            this.picMap.Name = "picMap";
            this.picMap.Size = new System.Drawing.Size(279, 256);
            this.picMap.TabIndex = 0;
            this.picMap.TabStop = false;
            this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.world_MouseUp);
            // 
            // lblAvatars
            // 
            this.lblAvatars.AutoSize = true;
            this.lblAvatars.BackColor = System.Drawing.Color.Transparent;
            this.lblAvatars.ForeColor = System.Drawing.Color.White;
            this.lblAvatars.Location = new System.Drawing.Point(19, 319);
            this.lblAvatars.Name = "lblAvatars";
            this.lblAvatars.Size = new System.Drawing.Size(43, 13);
            this.lblAvatars.TabIndex = 13;
            this.lblAvatars.Text = "Avatars";
            // 
            // lblSim
            // 
            this.lblSim.AutoSize = true;
            this.lblSim.BackColor = System.Drawing.Color.Transparent;
            this.lblSim.ForeColor = System.Drawing.Color.White;
            this.lblSim.Location = new System.Drawing.Point(19, 33);
            this.lblSim.Name = "lblSim";
            this.lblSim.Size = new System.Drawing.Size(70, 13);
            this.lblSim.TabIndex = 14;
            this.lblSim.Text = "Simulator info";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(274, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "button1";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmMap
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmMap");
            this.ClientSize = new System.Drawing.Size(320, 435);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblSim);
            this.Controls.Add(this.lblAvatars);
            this.Controls.Add(this.txtSLUrl);
            this.Controls.Add(this.lblMapDownloading);
            this.Controls.Add(this.picMap);
            this.Controls.Add(this.lblZ);
            this.Controls.Add(this.lblSelectedPosition);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.btnTeleport);
            this.Controls.Add(this.nudZ);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmMap_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmMap_MouseMove);
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmMapClient_Load);
            this.Enter += new System.EventHandler(this.frmMapClient_Enter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMapClient_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.PictureBox picMap;
        private System.Windows.Forms.Label lblSelectedPosition;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudZ;
        private clControls.clImageButton btnTeleport;
        private System.Windows.Forms.Label lblZ;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblMapDownloading;
        private System.Windows.Forms.TextBox txtSLUrl;
        private System.Windows.Forms.Label lblAvatars;
        private System.Windows.Forms.Label lblSim;
        private clControls.clImageButton btnClose;
    }
}

