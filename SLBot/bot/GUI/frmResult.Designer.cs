namespace bot.GUI
{
    partial class frmResult
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmResult));
            this.btnOK = new clControls.clImageButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(121, 230);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.MouseClick += new System.Windows.Forms.MouseEventHandler(btnOK_MouseClick);
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(22, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "label1";
            // 
            // txtResult
            // 
            this.txtResult.BackColor = System.Drawing.Color.Black;
            this.txtResult.ForeColor = System.Drawing.Color.White;
            this.txtResult.Location = new System.Drawing.Point(15, 25);
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.Size = new System.Drawing.Size(293, 199);
            this.txtResult.TabIndex = 4;
            this.txtResult.Text = "";
            // 
            // frmResult
            // 
            //this.AcceptButton = this.btnOK;
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmResult");
            this.ClientSize = new System.Drawing.Size(320, 265);
            this.ControlBox = false;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmResult";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmResult";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmResult_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmResult_MouseDown);
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private clControls.clImageButton btnOK;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox txtResult;
    }
}