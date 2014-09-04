namespace bot.GUI
{
    partial class frmDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDialog));
            this.btnOK = new clControls.clImageButton();
            this.btnCancel = new clControls.clImageButton();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(29, 81);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.MouseClick += new System.Windows.Forms.MouseEventHandler(btnOK_MouseClick);
            this.btnOK.BackColor = System.Drawing.Color.Transparent;

            // 
            // btnCancel
            // 
            this.btnCancel.MouseClick += new System.Windows.Forms.MouseEventHandler(btnCancel_MouseClick);
            this.btnCancel.Location = new System.Drawing.Point(240, 81);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.Color.Transparent;
            this.lblDescription.ForeColor = System.Drawing.Color.White;
            this.lblDescription.Location = new System.Drawing.Point(26, 9);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(35, 13);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "label1";
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(29, 42);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(286, 20);
            this.txtOutput.TabIndex = 3;
            this.txtOutput.TextChanged += new System.EventHandler(this.txtOutput_TextChanged);
            // 
            // frmDialog
            // 
            //this.AcceptButton = this.btnOK;
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmDialog");
            //this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(338, 135);
            this.ControlBox = false;
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmDialog";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(frmDialog_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(frmDialog_MouseMove);
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        private clControls.clImageButton btnOK;
        private clControls.clImageButton btnCancel;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtOutput;
    }
}