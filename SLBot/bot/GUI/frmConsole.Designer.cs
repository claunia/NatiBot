namespace bot.GUI
{
    partial class frmConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsole));
            this.edtInput = new System.Windows.Forms.TextBox();
            this.rtbConsole = new System.Windows.Forms.RichTextBox();
            this.btnClose = new clControls.clImageButton();
            this.lblConsole = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // edtInput
            // 
            this.edtInput.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.edtInput.BackColor = System.Drawing.Color.Black;
            this.edtInput.ForeColor = System.Drawing.Color.Lime;
            this.edtInput.Location = new System.Drawing.Point(26, 327);
            this.edtInput.Name = "edtInput";
            this.edtInput.Size = new System.Drawing.Size(376, 20);
            this.edtInput.TabIndex = 0;
            this.edtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtInput_KeyPress);
            // 
            // rtbConsole
            // 
            this.rtbConsole.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.rtbConsole.BackColor = System.Drawing.Color.Black;
            this.rtbConsole.ForeColor = System.Drawing.Color.Lime;
            this.rtbConsole.Location = new System.Drawing.Point(26, 31);
            this.rtbConsole.Name = "rtbConsole";
            this.rtbConsole.ReadOnly = true;
            this.rtbConsole.Size = new System.Drawing.Size(376, 290);
            this.rtbConsole.TabIndex = 1;
            this.rtbConsole.Text = "";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Location = new System.Drawing.Point(378, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 2;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "X";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblConsole
            // 
            this.lblConsole.AutoSize = true;
            this.lblConsole.BackColor = System.Drawing.Color.Transparent;
            this.lblConsole.Font = new System.Drawing.Font("Lucida Handwriting", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConsole.ForeColor = System.Drawing.Color.White;
            this.lblConsole.Location = new System.Drawing.Point(22, 9);
            this.lblConsole.Name = "lblConsole";
            this.lblConsole.Size = new System.Drawing.Size(75, 19);
            this.lblConsole.TabIndex = 3;
            this.lblConsole.Text = "Console";
            // 
            // frmConsole
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmConsole");
            this.ClientSize = new System.Drawing.Size(426, 349);
            this.Controls.Add(this.lblConsole);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.rtbConsole);
            this.Controls.Add(this.edtInput);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmConsole";
            this.Text = "frmConsole";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmConsole_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmConsole_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edtInput;
        private System.Windows.Forms.RichTextBox rtbConsole;
        private clControls.clImageButton btnClose;
        private System.Windows.Forms.Label lblConsole;
    }
}