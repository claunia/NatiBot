namespace bot.GUI
{
    partial class frmChat
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
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.lstChatters = new System.Windows.Forms.ListBox();
            this.edtInput = new System.Windows.Forms.TextBox();
            this.cmbBots = new System.Windows.Forms.ComboBox();
            this.lblChat = new System.Windows.Forms.Label();
            this.btnClose = new clControls.clImageButton();
            this.cmbFromLanguage = new System.Windows.Forms.ComboBox();
            this.lblToLanguage = new System.Windows.Forms.Label();
            this.cmbToLanguage = new System.Windows.Forms.ComboBox();
            this.chkAutoTranslate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rtbChat
            // 
            this.rtbChat.BackColor = System.Drawing.Color.Black;
            this.rtbChat.ForeColor = System.Drawing.Color.White;
            this.rtbChat.Location = new System.Drawing.Point(206, 58);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.Size = new System.Drawing.Size(590, 381);
            this.rtbChat.TabIndex = 0;
            this.rtbChat.Text = "";
            // 
            // lstChatters
            // 
            this.lstChatters.BackColor = System.Drawing.Color.Black;
            this.lstChatters.CausesValidation = false;
            this.lstChatters.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lstChatters.ForeColor = System.Drawing.Color.White;
            this.lstChatters.Location = new System.Drawing.Point(12, 58);
            this.lstChatters.Name = "lstChatters";
            this.lstChatters.Size = new System.Drawing.Size(188, 381);
            this.lstChatters.TabIndex = 1;
            this.lstChatters.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstChatters_DrawItem);
            this.lstChatters.SelectedValueChanged += new System.EventHandler(this.lstChatters_SelectedValueChanged);
            // 
            // edtInput
            // 
            this.edtInput.BackColor = System.Drawing.Color.Black;
            this.edtInput.CausesValidation = false;
            this.edtInput.ForeColor = System.Drawing.Color.White;
            this.edtInput.ImeMode = System.Windows.Forms.ImeMode.On;
            this.edtInput.Location = new System.Drawing.Point(50, 449);
            this.edtInput.Name = "edtInput";
            this.edtInput.Size = new System.Drawing.Size(746, 20);
            this.edtInput.TabIndex = 3;
            this.edtInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.edtInput_KeyPress);
            // 
            // cmbBots
            // 
            this.cmbBots.BackColor = System.Drawing.Color.Black;
            this.cmbBots.CausesValidation = false;
            this.cmbBots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBots.ForeColor = System.Drawing.Color.White;
            this.cmbBots.FormattingEnabled = true;
            this.cmbBots.Location = new System.Drawing.Point(50, 31);
            this.cmbBots.Name = "cmbBots";
            this.cmbBots.Size = new System.Drawing.Size(177, 21);
            this.cmbBots.Sorted = true;
            this.cmbBots.TabIndex = 4;
            this.cmbBots.SelectionChangeCommitted += new System.EventHandler(this.cmbBots_SelectionChangeCommitted);
            // 
            // lblChat
            // 
            this.lblChat.AutoSize = true;
            this.lblChat.BackColor = System.Drawing.Color.Transparent;
            this.lblChat.Font = new System.Drawing.Font("Lucida Handwriting", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChat.ForeColor = System.Drawing.Color.White;
            this.lblChat.Location = new System.Drawing.Point(46, 9);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(51, 19);
            this.lblChat.TabIndex = 33;
            this.lblChat.Text = "Chat";
            // 
            // btnClose
            // 
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Location = new System.Drawing.Point(772, 9);
            this.btnClose.Name = "btnClose";
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 5;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cmbFromLanguage
            // 
            this.cmbFromLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFromLanguage.FormattingEnabled = true;
            this.cmbFromLanguage.Location = new System.Drawing.Point(356, 31);
            this.cmbFromLanguage.Name = "cmbFromLanguage";
            this.cmbFromLanguage.Size = new System.Drawing.Size(121, 21);
            this.cmbFromLanguage.Sorted = true;
            this.cmbFromLanguage.TabIndex = 34;
            this.cmbFromLanguage.SelectionChangeCommitted += new System.EventHandler(this.cmbFromLanguage_SelectionChangeCommitted);
            // 
            // lblToLanguage
            // 
            this.lblToLanguage.AutoSize = true;
            this.lblToLanguage.BackColor = System.Drawing.Color.Transparent;
            this.lblToLanguage.ForeColor = System.Drawing.Color.White;
            this.lblToLanguage.Location = new System.Drawing.Point(484, 34);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(19, 13);
            this.lblToLanguage.TabIndex = 36;
            this.lblToLanguage.Text = "to:";
            // 
            // cmbToLanguage
            // 
            this.cmbToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToLanguage.FormattingEnabled = true;
            this.cmbToLanguage.Location = new System.Drawing.Point(509, 31);
            this.cmbToLanguage.Name = "cmbToLanguage";
            this.cmbToLanguage.Size = new System.Drawing.Size(121, 21);
            this.cmbToLanguage.Sorted = true;
            this.cmbToLanguage.TabIndex = 37;
            this.cmbToLanguage.SelectionChangeCommitted += new System.EventHandler(this.cmbToLanguage_SelectionChangeCommitted);
            // 
            // chkAutoTranslate
            // 
            this.chkAutoTranslate.AutoSize = true;
            this.chkAutoTranslate.BackColor = System.Drawing.Color.Transparent;
            this.chkAutoTranslate.ForeColor = System.Drawing.Color.White;
            this.chkAutoTranslate.Location = new System.Drawing.Point(233, 33);
            this.chkAutoTranslate.Name = "chkAutoTranslate";
            this.chkAutoTranslate.Size = new System.Drawing.Size(117, 17);
            this.chkAutoTranslate.TabIndex = 38;
            this.chkAutoTranslate.Text = "Auto-translate from:";
            this.chkAutoTranslate.UseVisualStyleBackColor = false;
            this.chkAutoTranslate.CheckedChanged += new System.EventHandler(this.chkAutoTranslate_CheckedChanged);
            // 
            // frmChat
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmChat");
            this.ClientSize = new System.Drawing.Size(841, 481);
            this.Controls.Add(this.chkAutoTranslate);
            this.Controls.Add(this.cmbToLanguage);
            this.Controls.Add(this.lblToLanguage);
            this.Controls.Add(this.cmbFromLanguage);
            this.Controls.Add(this.lblChat);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cmbBots);
            this.Controls.Add(this.edtInput);
            this.Controls.Add(this.lstChatters);
            this.Controls.Add(this.rtbChat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChat";
            this.Text = "frmChat";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmChat_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmChat_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox edtInput;
        private System.Windows.Forms.ComboBox cmbBots;
        private clControls.clImageButton btnClose;
        private System.Windows.Forms.Label lblChat;
        private System.Windows.Forms.ListBox lstChatters;
        private System.Windows.Forms.ComboBox cmbFromLanguage;
        private System.Windows.Forms.Label lblToLanguage;
        private System.Windows.Forms.ComboBox cmbToLanguage;
        private System.Windows.Forms.CheckBox chkAutoTranslate;
    }
}