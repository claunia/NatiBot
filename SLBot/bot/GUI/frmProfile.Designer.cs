namespace bot.GUI
{
    partial class frmProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfile));
            this.tabProfile = new System.Windows.Forms.TabControl();
            this.tabPageSecondLife = new System.Windows.Forms.TabPage();
            this.txtAccount = new System.Windows.Forms.RichTextBox();
            this.txtAbout = new System.Windows.Forms.RichTextBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lstGroups = new System.Windows.Forms.ListView();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.txtBorn = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPartner = new System.Windows.Forms.TextBox();
            this.txtUUID = new System.Windows.Forms.TextBox();
            this.lblPartner = new System.Windows.Forms.Label();
            this.lblGroups = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.lblBorn = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPhoto = new System.Windows.Forms.Label();
            this.lblUUID = new System.Windows.Forms.Label();
            this.tabPageWeb = new System.Windows.Forms.TabPage();
            this.txtWeb = new System.Windows.Forms.TextBox();
            this.lblWeb = new System.Windows.Forms.Label();
            this.tabPageInterests = new System.Windows.Forms.TabPage();
            this.chkHire = new System.Windows.Forms.CheckBox();
            this.chkBuy = new System.Windows.Forms.CheckBox();
            this.chkSell = new System.Windows.Forms.CheckBox();
            this.chkGroup = new System.Windows.Forms.CheckBox();
            this.chkExplore = new System.Windows.Forms.CheckBox();
            this.chkCustomChars = new System.Windows.Forms.CheckBox();
            this.chkEventPlanning = new System.Windows.Forms.CheckBox();
            this.chkArchitecture = new System.Windows.Forms.CheckBox();
            this.chkScripting = new System.Windows.Forms.CheckBox();
            this.chkModeling = new System.Windows.Forms.CheckBox();
            this.chkBeHired = new System.Windows.Forms.CheckBox();
            this.chkTextures = new System.Windows.Forms.CheckBox();
            this.chkMeet = new System.Windows.Forms.CheckBox();
            this.chkBuild = new System.Windows.Forms.CheckBox();
            this.txtLanguages = new System.Windows.Forms.TextBox();
            this.txtSkills = new System.Windows.Forms.TextBox();
            this.txtWants = new System.Windows.Forms.TextBox();
            this.lblLanguages = new System.Windows.Forms.Label();
            this.lblSkills = new System.Windows.Forms.Label();
            this.lblWants = new System.Windows.Forms.Label();
            this.tabPageFirstLife = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.picPhotoF = new System.Windows.Forms.PictureBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.lblPhotoF = new System.Windows.Forms.Label();
            this.btnClose = new clControls.clImageButton();
            this.lblProfile = new System.Windows.Forms.Label();
            this.tabProfile.SuspendLayout();
            this.tabPageSecondLife.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            this.tabPageWeb.SuspendLayout();
            this.tabPageInterests.SuspendLayout();
            this.tabPageFirstLife.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhotoF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            this.SuspendLayout();
            // 
            // tabProfile
            // 
            this.tabProfile.Controls.Add(this.tabPageSecondLife);
            this.tabProfile.Controls.Add(this.tabPageWeb);
            this.tabProfile.Controls.Add(this.tabPageInterests);
            this.tabProfile.Controls.Add(this.tabPageFirstLife);
            this.tabProfile.Location = new System.Drawing.Point(28, 29);
            this.tabProfile.Name = "tabProfile";
            this.tabProfile.SelectedIndex = 0;
            this.tabProfile.Size = new System.Drawing.Size(446, 554);
            this.tabProfile.TabIndex = 0;
            // 
            // tabPageSecondLife
            // 
            this.tabPageSecondLife.BackColor = System.Drawing.Color.Black;
            this.tabPageSecondLife.Controls.Add(this.txtAccount);
            this.tabPageSecondLife.Controls.Add(this.txtAbout);
            this.tabPageSecondLife.Controls.Add(this.lblAbout);
            this.tabPageSecondLife.Controls.Add(this.lstGroups);
            this.tabPageSecondLife.Controls.Add(this.picPhoto);
            this.tabPageSecondLife.Controls.Add(this.txtBorn);
            this.tabPageSecondLife.Controls.Add(this.txtName);
            this.tabPageSecondLife.Controls.Add(this.txtPartner);
            this.tabPageSecondLife.Controls.Add(this.txtUUID);
            this.tabPageSecondLife.Controls.Add(this.lblPartner);
            this.tabPageSecondLife.Controls.Add(this.lblGroups);
            this.tabPageSecondLife.Controls.Add(this.lblAccount);
            this.tabPageSecondLife.Controls.Add(this.lblBorn);
            this.tabPageSecondLife.Controls.Add(this.lblName);
            this.tabPageSecondLife.Controls.Add(this.lblPhoto);
            this.tabPageSecondLife.Controls.Add(this.lblUUID);
            this.tabPageSecondLife.ForeColor = System.Drawing.Color.White;
            this.tabPageSecondLife.Location = new System.Drawing.Point(4, 22);
            this.tabPageSecondLife.Name = "tabPageSecondLife";
            this.tabPageSecondLife.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSecondLife.Size = new System.Drawing.Size(438, 528);
            this.tabPageSecondLife.TabIndex = 0;
            this.tabPageSecondLife.Text = "2nd Life";
            // 
            // txtAccount
            // 
            this.txtAccount.BackColor = System.Drawing.Color.Black;
            this.txtAccount.ForeColor = System.Drawing.Color.White;
            this.txtAccount.Location = new System.Drawing.Point(270, 77);
            this.txtAccount.Name = "txtAccount";
            this.txtAccount.ReadOnly = true;
            this.txtAccount.Size = new System.Drawing.Size(162, 113);
            this.txtAccount.TabIndex = 19;
            this.txtAccount.Text = "";
            // 
            // txtAbout
            // 
            this.txtAbout.BackColor = System.Drawing.Color.Black;
            this.txtAbout.ForeColor = System.Drawing.Color.White;
            this.txtAbout.Location = new System.Drawing.Point(46, 387);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.ReadOnly = true;
            this.txtAbout.Size = new System.Drawing.Size(386, 135);
            this.txtAbout.TabIndex = 18;
            this.txtAbout.Text = "";
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.BackColor = System.Drawing.Color.Black;
            this.lblAbout.ForeColor = System.Drawing.Color.White;
            this.lblAbout.Location = new System.Drawing.Point(8, 390);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(38, 13);
            this.lblAbout.TabIndex = 16;
            this.lblAbout.Text = "About:";
            // 
            // lstGroups
            // 
            this.lstGroups.AutoArrange = false;
            this.lstGroups.BackColor = System.Drawing.Color.Black;
            this.lstGroups.ForeColor = System.Drawing.Color.White;
            this.lstGroups.FullRowSelect = true;
            this.lstGroups.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstGroups.Location = new System.Drawing.Point(46, 273);
            this.lstGroups.MultiSelect = false;
            this.lstGroups.Name = "lstGroups";
            this.lstGroups.ShowGroups = false;
            this.lstGroups.Size = new System.Drawing.Size(386, 108);
            this.lstGroups.TabIndex = 15;
            this.lstGroups.UseCompatibleStateImageBehavior = false;
            this.lstGroups.View = System.Windows.Forms.View.List;
            // 
            // picPhoto
            // 
            this.picPhoto.Location = new System.Drawing.Point(46, 58);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(209, 209);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPhoto.TabIndex = 14;
            this.picPhoto.TabStop = false;
            // 
            // txtBorn
            // 
            this.txtBorn.BackColor = System.Drawing.Color.Black;
            this.txtBorn.ForeColor = System.Drawing.Color.White;
            this.txtBorn.Location = new System.Drawing.Point(270, 32);
            this.txtBorn.Name = "txtBorn";
            this.txtBorn.ReadOnly = true;
            this.txtBorn.Size = new System.Drawing.Size(162, 20);
            this.txtBorn.TabIndex = 11;
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.Black;
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(46, 32);
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.Size = new System.Drawing.Size(171, 20);
            this.txtName.TabIndex = 10;
            // 
            // txtPartner
            // 
            this.txtPartner.BackColor = System.Drawing.Color.Black;
            this.txtPartner.ForeColor = System.Drawing.Color.White;
            this.txtPartner.Location = new System.Drawing.Point(270, 209);
            this.txtPartner.Name = "txtPartner";
            this.txtPartner.ReadOnly = true;
            this.txtPartner.Size = new System.Drawing.Size(162, 20);
            this.txtPartner.TabIndex = 8;
            // 
            // txtUUID
            // 
            this.txtUUID.BackColor = System.Drawing.Color.Black;
            this.txtUUID.ForeColor = System.Drawing.Color.White;
            this.txtUUID.Location = new System.Drawing.Point(46, 6);
            this.txtUUID.Name = "txtUUID";
            this.txtUUID.ReadOnly = true;
            this.txtUUID.Size = new System.Drawing.Size(386, 20);
            this.txtUUID.TabIndex = 7;
            // 
            // lblPartner
            // 
            this.lblPartner.AutoSize = true;
            this.lblPartner.BackColor = System.Drawing.Color.Black;
            this.lblPartner.ForeColor = System.Drawing.Color.White;
            this.lblPartner.Location = new System.Drawing.Point(267, 193);
            this.lblPartner.Name = "lblPartner";
            this.lblPartner.Size = new System.Drawing.Size(44, 13);
            this.lblPartner.TabIndex = 6;
            this.lblPartner.Text = "Partner:";
            // 
            // lblGroups
            // 
            this.lblGroups.AutoSize = true;
            this.lblGroups.BackColor = System.Drawing.Color.Black;
            this.lblGroups.ForeColor = System.Drawing.Color.White;
            this.lblGroups.Location = new System.Drawing.Point(2, 270);
            this.lblGroups.Name = "lblGroups";
            this.lblGroups.Size = new System.Drawing.Size(44, 13);
            this.lblGroups.TabIndex = 5;
            this.lblGroups.Text = "Groups:";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.BackColor = System.Drawing.Color.Black;
            this.lblAccount.ForeColor = System.Drawing.Color.White;
            this.lblAccount.Location = new System.Drawing.Point(267, 61);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(50, 13);
            this.lblAccount.TabIndex = 4;
            this.lblAccount.Text = "Account:";
            // 
            // lblBorn
            // 
            this.lblBorn.AutoSize = true;
            this.lblBorn.BackColor = System.Drawing.Color.Black;
            this.lblBorn.ForeColor = System.Drawing.Color.White;
            this.lblBorn.Location = new System.Drawing.Point(223, 35);
            this.lblBorn.Name = "lblBorn";
            this.lblBorn.Size = new System.Drawing.Size(32, 13);
            this.lblBorn.TabIndex = 3;
            this.lblBorn.Text = "Born:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Black;
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(2, 35);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name:";
            // 
            // lblPhoto
            // 
            this.lblPhoto.AutoSize = true;
            this.lblPhoto.BackColor = System.Drawing.Color.Black;
            this.lblPhoto.ForeColor = System.Drawing.Color.White;
            this.lblPhoto.Location = new System.Drawing.Point(3, 58);
            this.lblPhoto.Name = "lblPhoto";
            this.lblPhoto.Size = new System.Drawing.Size(38, 13);
            this.lblPhoto.TabIndex = 1;
            this.lblPhoto.Text = "Photo:";
            // 
            // lblUUID
            // 
            this.lblUUID.AutoSize = true;
            this.lblUUID.BackColor = System.Drawing.Color.Black;
            this.lblUUID.ForeColor = System.Drawing.Color.White;
            this.lblUUID.Location = new System.Drawing.Point(3, 9);
            this.lblUUID.Name = "lblUUID";
            this.lblUUID.Size = new System.Drawing.Size(37, 13);
            this.lblUUID.TabIndex = 0;
            this.lblUUID.Text = "UUID:";
            // 
            // tabPageWeb
            // 
            this.tabPageWeb.BackColor = System.Drawing.Color.Black;
            this.tabPageWeb.Controls.Add(this.txtWeb);
            this.tabPageWeb.Controls.Add(this.lblWeb);
            this.tabPageWeb.ForeColor = System.Drawing.Color.White;
            this.tabPageWeb.Location = new System.Drawing.Point(4, 22);
            this.tabPageWeb.Name = "tabPageWeb";
            this.tabPageWeb.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWeb.Size = new System.Drawing.Size(438, 528);
            this.tabPageWeb.TabIndex = 1;
            this.tabPageWeb.Text = "Web";
            // 
            // txtWeb
            // 
            this.txtWeb.BackColor = System.Drawing.Color.Black;
            this.txtWeb.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.txtWeb.Location = new System.Drawing.Point(42, 6);
            this.txtWeb.Name = "txtWeb";
            this.txtWeb.ReadOnly = true;
            this.txtWeb.Size = new System.Drawing.Size(390, 20);
            this.txtWeb.TabIndex = 1;
            // 
            // lblWeb
            // 
            this.lblWeb.AutoSize = true;
            this.lblWeb.ForeColor = System.Drawing.Color.White;
            this.lblWeb.Location = new System.Drawing.Point(3, 9);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(33, 13);
            this.lblWeb.TabIndex = 0;
            this.lblWeb.Text = "Web:";
            // 
            // tabPageInterests
            // 
            this.tabPageInterests.BackColor = System.Drawing.Color.Black;
            this.tabPageInterests.Controls.Add(this.chkHire);
            this.tabPageInterests.Controls.Add(this.chkBuy);
            this.tabPageInterests.Controls.Add(this.chkSell);
            this.tabPageInterests.Controls.Add(this.chkGroup);
            this.tabPageInterests.Controls.Add(this.chkExplore);
            this.tabPageInterests.Controls.Add(this.chkCustomChars);
            this.tabPageInterests.Controls.Add(this.chkEventPlanning);
            this.tabPageInterests.Controls.Add(this.chkArchitecture);
            this.tabPageInterests.Controls.Add(this.chkScripting);
            this.tabPageInterests.Controls.Add(this.chkModeling);
            this.tabPageInterests.Controls.Add(this.chkBeHired);
            this.tabPageInterests.Controls.Add(this.chkTextures);
            this.tabPageInterests.Controls.Add(this.chkMeet);
            this.tabPageInterests.Controls.Add(this.chkBuild);
            this.tabPageInterests.Controls.Add(this.txtLanguages);
            this.tabPageInterests.Controls.Add(this.txtSkills);
            this.tabPageInterests.Controls.Add(this.txtWants);
            this.tabPageInterests.Controls.Add(this.lblLanguages);
            this.tabPageInterests.Controls.Add(this.lblSkills);
            this.tabPageInterests.Controls.Add(this.lblWants);
            this.tabPageInterests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPageInterests.ForeColor = System.Drawing.Color.White;
            this.tabPageInterests.Location = new System.Drawing.Point(4, 22);
            this.tabPageInterests.Name = "tabPageInterests";
            this.tabPageInterests.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInterests.Size = new System.Drawing.Size(438, 528);
            this.tabPageInterests.TabIndex = 2;
            this.tabPageInterests.Text = "Interests";
            // 
            // chkHire
            // 
            this.chkHire.AutoSize = true;
            this.chkHire.BackColor = System.Drawing.Color.Transparent;
            this.chkHire.Enabled = false;
            this.chkHire.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkHire.ForeColor = System.Drawing.Color.White;
            this.chkHire.Location = new System.Drawing.Point(216, 101);
            this.chkHire.Name = "chkHire";
            this.chkHire.Size = new System.Drawing.Size(49, 17);
            this.chkHire.TabIndex = 19;
            this.chkHire.Text = "Hire";
            this.chkHire.UseVisualStyleBackColor = false;
            // 
            // chkBuy
            // 
            this.chkBuy.AutoSize = true;
            this.chkBuy.BackColor = System.Drawing.Color.Transparent;
            this.chkBuy.Enabled = false;
            this.chkBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBuy.ForeColor = System.Drawing.Color.White;
            this.chkBuy.Location = new System.Drawing.Point(216, 78);
            this.chkBuy.Name = "chkBuy";
            this.chkBuy.Size = new System.Drawing.Size(47, 17);
            this.chkBuy.TabIndex = 18;
            this.chkBuy.Text = "Buy";
            this.chkBuy.UseVisualStyleBackColor = false;
            // 
            // chkSell
            // 
            this.chkSell.AutoSize = true;
            this.chkSell.BackColor = System.Drawing.Color.Transparent;
            this.chkSell.Enabled = false;
            this.chkSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSell.ForeColor = System.Drawing.Color.White;
            this.chkSell.Location = new System.Drawing.Point(86, 78);
            this.chkSell.Name = "chkSell";
            this.chkSell.Size = new System.Drawing.Size(47, 17);
            this.chkSell.TabIndex = 17;
            this.chkSell.Text = "Sell";
            this.chkSell.UseVisualStyleBackColor = false;
            // 
            // chkGroup
            // 
            this.chkGroup.AutoSize = true;
            this.chkGroup.BackColor = System.Drawing.Color.Transparent;
            this.chkGroup.Enabled = false;
            this.chkGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGroup.ForeColor = System.Drawing.Color.White;
            this.chkGroup.Location = new System.Drawing.Point(216, 55);
            this.chkGroup.Name = "chkGroup";
            this.chkGroup.Size = new System.Drawing.Size(60, 17);
            this.chkGroup.TabIndex = 16;
            this.chkGroup.Text = "Group";
            this.chkGroup.UseVisualStyleBackColor = false;
            // 
            // chkExplore
            // 
            this.chkExplore.AutoSize = true;
            this.chkExplore.BackColor = System.Drawing.Color.Transparent;
            this.chkExplore.Enabled = false;
            this.chkExplore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkExplore.ForeColor = System.Drawing.Color.White;
            this.chkExplore.Location = new System.Drawing.Point(216, 32);
            this.chkExplore.Name = "chkExplore";
            this.chkExplore.Size = new System.Drawing.Size(68, 17);
            this.chkExplore.TabIndex = 15;
            this.chkExplore.Text = "Explore";
            this.chkExplore.UseVisualStyleBackColor = false;
            // 
            // chkCustomChars
            // 
            this.chkCustomChars.AutoSize = true;
            this.chkCustomChars.BackColor = System.Drawing.Color.Transparent;
            this.chkCustomChars.Enabled = false;
            this.chkCustomChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCustomChars.ForeColor = System.Drawing.Color.White;
            this.chkCustomChars.Location = new System.Drawing.Point(216, 196);
            this.chkCustomChars.Name = "chkCustomChars";
            this.chkCustomChars.Size = new System.Drawing.Size(132, 17);
            this.chkCustomChars.TabIndex = 14;
            this.chkCustomChars.Text = "Custom Characters";
            this.chkCustomChars.UseVisualStyleBackColor = false;
            // 
            // chkEventPlanning
            // 
            this.chkEventPlanning.AutoSize = true;
            this.chkEventPlanning.BackColor = System.Drawing.Color.Transparent;
            this.chkEventPlanning.Enabled = false;
            this.chkEventPlanning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEventPlanning.ForeColor = System.Drawing.Color.White;
            this.chkEventPlanning.Location = new System.Drawing.Point(216, 173);
            this.chkEventPlanning.Name = "chkEventPlanning";
            this.chkEventPlanning.Size = new System.Drawing.Size(112, 17);
            this.chkEventPlanning.TabIndex = 13;
            this.chkEventPlanning.Text = "Event Planning";
            this.chkEventPlanning.UseVisualStyleBackColor = false;
            // 
            // chkArchitecture
            // 
            this.chkArchitecture.AutoSize = true;
            this.chkArchitecture.BackColor = System.Drawing.Color.Transparent;
            this.chkArchitecture.Enabled = false;
            this.chkArchitecture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkArchitecture.ForeColor = System.Drawing.Color.White;
            this.chkArchitecture.Location = new System.Drawing.Point(216, 150);
            this.chkArchitecture.Name = "chkArchitecture";
            this.chkArchitecture.Size = new System.Drawing.Size(95, 17);
            this.chkArchitecture.TabIndex = 12;
            this.chkArchitecture.Text = "Architecture";
            this.chkArchitecture.UseVisualStyleBackColor = false;
            // 
            // chkScripting
            // 
            this.chkScripting.AutoSize = true;
            this.chkScripting.BackColor = System.Drawing.Color.Transparent;
            this.chkScripting.Enabled = false;
            this.chkScripting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkScripting.ForeColor = System.Drawing.Color.White;
            this.chkScripting.Location = new System.Drawing.Point(86, 196);
            this.chkScripting.Name = "chkScripting";
            this.chkScripting.Size = new System.Drawing.Size(76, 17);
            this.chkScripting.TabIndex = 11;
            this.chkScripting.Text = "Scripting";
            this.chkScripting.UseVisualStyleBackColor = false;
            // 
            // chkModeling
            // 
            this.chkModeling.AutoSize = true;
            this.chkModeling.BackColor = System.Drawing.Color.Transparent;
            this.chkModeling.Enabled = false;
            this.chkModeling.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModeling.Location = new System.Drawing.Point(86, 173);
            this.chkModeling.Name = "chkModeling";
            this.chkModeling.Size = new System.Drawing.Size(77, 17);
            this.chkModeling.TabIndex = 10;
            this.chkModeling.Text = "Modeling";
            this.chkModeling.UseVisualStyleBackColor = false;
            // 
            // chkBeHired
            // 
            this.chkBeHired.AutoSize = true;
            this.chkBeHired.BackColor = System.Drawing.Color.Transparent;
            this.chkBeHired.Enabled = false;
            this.chkBeHired.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBeHired.ForeColor = System.Drawing.Color.White;
            this.chkBeHired.Location = new System.Drawing.Point(86, 101);
            this.chkBeHired.Name = "chkBeHired";
            this.chkBeHired.Size = new System.Drawing.Size(73, 17);
            this.chkBeHired.TabIndex = 9;
            this.chkBeHired.Text = "Be hired";
            this.chkBeHired.UseVisualStyleBackColor = false;
            // 
            // chkTextures
            // 
            this.chkTextures.AutoSize = true;
            this.chkTextures.BackColor = System.Drawing.Color.Transparent;
            this.chkTextures.Enabled = false;
            this.chkTextures.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTextures.ForeColor = System.Drawing.Color.White;
            this.chkTextures.Location = new System.Drawing.Point(86, 150);
            this.chkTextures.Name = "chkTextures";
            this.chkTextures.Size = new System.Drawing.Size(75, 17);
            this.chkTextures.TabIndex = 8;
            this.chkTextures.Text = "Textures";
            this.chkTextures.UseVisualStyleBackColor = false;
            // 
            // chkMeet
            // 
            this.chkMeet.AutoSize = true;
            this.chkMeet.BackColor = System.Drawing.Color.Transparent;
            this.chkMeet.Enabled = false;
            this.chkMeet.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMeet.ForeColor = System.Drawing.Color.White;
            this.chkMeet.Location = new System.Drawing.Point(86, 55);
            this.chkMeet.Name = "chkMeet";
            this.chkMeet.Size = new System.Drawing.Size(54, 17);
            this.chkMeet.TabIndex = 7;
            this.chkMeet.Text = "Meet";
            this.chkMeet.UseVisualStyleBackColor = false;
            // 
            // chkBuild
            // 
            this.chkBuild.AutoSize = true;
            this.chkBuild.BackColor = System.Drawing.Color.Transparent;
            this.chkBuild.Enabled = false;
            this.chkBuild.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBuild.ForeColor = System.Drawing.Color.White;
            this.chkBuild.Location = new System.Drawing.Point(86, 32);
            this.chkBuild.Name = "chkBuild";
            this.chkBuild.Size = new System.Drawing.Size(54, 17);
            this.chkBuild.TabIndex = 6;
            this.chkBuild.Text = "Build";
            this.chkBuild.UseVisualStyleBackColor = false;
            // 
            // txtLanguages
            // 
            this.txtLanguages.BackColor = System.Drawing.Color.Black;
            this.txtLanguages.ForeColor = System.Drawing.Color.White;
            this.txtLanguages.Location = new System.Drawing.Point(86, 219);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.ReadOnly = true;
            this.txtLanguages.Size = new System.Drawing.Size(346, 20);
            this.txtLanguages.TabIndex = 5;
            // 
            // txtSkills
            // 
            this.txtSkills.BackColor = System.Drawing.Color.Black;
            this.txtSkills.ForeColor = System.Drawing.Color.White;
            this.txtSkills.Location = new System.Drawing.Point(86, 124);
            this.txtSkills.Name = "txtSkills";
            this.txtSkills.ReadOnly = true;
            this.txtSkills.Size = new System.Drawing.Size(346, 20);
            this.txtSkills.TabIndex = 4;
            // 
            // txtWants
            // 
            this.txtWants.BackColor = System.Drawing.Color.Black;
            this.txtWants.ForeColor = System.Drawing.Color.White;
            this.txtWants.Location = new System.Drawing.Point(86, 6);
            this.txtWants.Name = "txtWants";
            this.txtWants.ReadOnly = true;
            this.txtWants.Size = new System.Drawing.Size(346, 20);
            this.txtWants.TabIndex = 3;
            // 
            // lblLanguages
            // 
            this.lblLanguages.AutoSize = true;
            this.lblLanguages.BackColor = System.Drawing.Color.Black;
            this.lblLanguages.ForeColor = System.Drawing.Color.White;
            this.lblLanguages.Location = new System.Drawing.Point(17, 222);
            this.lblLanguages.Name = "lblLanguages";
            this.lblLanguages.Size = new System.Drawing.Size(73, 13);
            this.lblLanguages.TabIndex = 2;
            this.lblLanguages.Text = "Languages:";
            // 
            // lblSkills
            // 
            this.lblSkills.AutoSize = true;
            this.lblSkills.BackColor = System.Drawing.Color.Black;
            this.lblSkills.ForeColor = System.Drawing.Color.White;
            this.lblSkills.Location = new System.Drawing.Point(46, 127);
            this.lblSkills.Name = "lblSkills";
            this.lblSkills.Size = new System.Drawing.Size(41, 13);
            this.lblSkills.TabIndex = 1;
            this.lblSkills.Text = "Skills:";
            // 
            // lblWants
            // 
            this.lblWants.AutoSize = true;
            this.lblWants.BackColor = System.Drawing.Color.Black;
            this.lblWants.ForeColor = System.Drawing.Color.White;
            this.lblWants.Location = new System.Drawing.Point(3, 9);
            this.lblWants.Name = "lblWants";
            this.lblWants.Size = new System.Drawing.Size(90, 13);
            this.lblWants.TabIndex = 0;
            this.lblWants.Text = "He/she wants:";
            // 
            // tabPageFirstLife
            // 
            this.tabPageFirstLife.BackColor = System.Drawing.Color.Black;
            this.tabPageFirstLife.Controls.Add(this.txtInfo);
            this.tabPageFirstLife.Controls.Add(this.picPhotoF);
            this.tabPageFirstLife.Controls.Add(this.lblInfo);
            this.tabPageFirstLife.Controls.Add(this.lblPhotoF);
            this.tabPageFirstLife.ForeColor = System.Drawing.Color.White;
            this.tabPageFirstLife.Location = new System.Drawing.Point(4, 22);
            this.tabPageFirstLife.Name = "tabPageFirstLife";
            this.tabPageFirstLife.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFirstLife.Size = new System.Drawing.Size(438, 528);
            this.tabPageFirstLife.TabIndex = 3;
            this.tabPageFirstLife.Text = "1st Life";
            // 
            // txtInfo
            // 
            this.txtInfo.BackColor = System.Drawing.Color.Black;
            this.txtInfo.ForeColor = System.Drawing.Color.White;
            this.txtInfo.Location = new System.Drawing.Point(52, 330);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(380, 192);
            this.txtInfo.TabIndex = 4;
            this.txtInfo.Text = "";
            // 
            // picPhotoF
            // 
            this.picPhotoF.Location = new System.Drawing.Point(52, 6);
            this.picPhotoF.Name = "picPhotoF";
            this.picPhotoF.Size = new System.Drawing.Size(318, 318);
            this.picPhotoF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPhotoF.TabIndex = 3;
            this.picPhotoF.TabStop = false;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(18, 333);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(28, 13);
            this.lblInfo.TabIndex = 1;
            this.lblInfo.Text = "Info:";
            // 
            // lblPhotoF
            // 
            this.lblPhotoF.AutoSize = true;
            this.lblPhotoF.Location = new System.Drawing.Point(8, 3);
            this.lblPhotoF.Name = "lblPhotoF";
            this.lblPhotoF.Size = new System.Drawing.Size(38, 13);
            this.lblPhotoF.TabIndex = 0;
            this.lblPhotoF.Text = "Photo:";
            // 
            // btnClose
            // 
            this.btnClose.ButtonBitmap = null;
            this.btnClose.ButtonState = clControls.ButtonState.Normal;
            this.btnClose.DisabledBitmap = null;
            this.btnClose.Location = new System.Drawing.Point(407, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.OnMouseClickBitmap = null;
            this.btnClose.OnMouseOverBitmap = null;
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 6;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblProfile
            // 
            this.lblProfile.AutoSize = true;
            this.lblProfile.BackColor = System.Drawing.Color.Transparent;
            this.lblProfile.Font = new System.Drawing.Font("Lucida Handwriting", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProfile.ForeColor = System.Drawing.Color.White;
            this.lblProfile.Location = new System.Drawing.Point(24, 7);
            this.lblProfile.Name = "lblProfile";
            this.lblProfile.Size = new System.Drawing.Size(68, 19);
            this.lblProfile.TabIndex = 34;
            this.lblProfile.Text = "Profile";
            // 
            // frmProfile
            // 
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmProfile");
            this.ClientSize = new System.Drawing.Size(500, 595);
            this.ControlBox = false;
            this.Controls.Add(this.lblProfile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabProfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmProfile";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmProfile_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmProfile_MouseMove);
            this.tabProfile.ResumeLayout(false);
            this.tabPageSecondLife.ResumeLayout(false);
            this.tabPageSecondLife.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            this.tabPageWeb.ResumeLayout(false);
            this.tabPageWeb.PerformLayout();
            this.tabPageInterests.ResumeLayout(false);
            this.tabPageInterests.PerformLayout();
            this.tabPageFirstLife.ResumeLayout(false);
            this.tabPageFirstLife.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhotoF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabProfile;
        private System.Windows.Forms.TabPage tabPageSecondLife;
        private System.Windows.Forms.TabPage tabPageWeb;
        private System.Windows.Forms.ListView lstGroups;
        private System.Windows.Forms.PictureBox picPhoto;
        private System.Windows.Forms.TextBox txtBorn;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPartner;
        private System.Windows.Forms.TextBox txtUUID;
        private System.Windows.Forms.Label lblPartner;
        private System.Windows.Forms.Label lblGroups;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblBorn;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPhoto;
        private System.Windows.Forms.Label lblUUID;
        private System.Windows.Forms.TextBox txtWeb;
        private System.Windows.Forms.Label lblWeb;
        private System.Windows.Forms.TabPage tabPageInterests;
        private System.Windows.Forms.TabPage tabPageFirstLife;
        private System.Windows.Forms.CheckBox chkHire;
        private System.Windows.Forms.CheckBox chkBuy;
        private System.Windows.Forms.CheckBox chkSell;
        private System.Windows.Forms.CheckBox chkGroup;
        private System.Windows.Forms.CheckBox chkExplore;
        private System.Windows.Forms.CheckBox chkCustomChars;
        private System.Windows.Forms.CheckBox chkEventPlanning;
        private System.Windows.Forms.CheckBox chkArchitecture;
        private System.Windows.Forms.CheckBox chkScripting;
        private System.Windows.Forms.CheckBox chkModeling;
        private System.Windows.Forms.CheckBox chkBeHired;
        private System.Windows.Forms.CheckBox chkTextures;
        private System.Windows.Forms.CheckBox chkMeet;
        private System.Windows.Forms.CheckBox chkBuild;
        private System.Windows.Forms.TextBox txtLanguages;
        private System.Windows.Forms.TextBox txtSkills;
        private System.Windows.Forms.TextBox txtWants;
        private System.Windows.Forms.Label lblLanguages;
        private System.Windows.Forms.Label lblSkills;
        private System.Windows.Forms.Label lblWants;
        private System.Windows.Forms.PictureBox picPhotoF;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Label lblPhotoF;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.RichTextBox txtAbout;
        private System.Windows.Forms.RichTextBox txtAccount;
        private System.Windows.Forms.RichTextBox txtInfo;
        private clControls.clImageButton btnClose;
        private System.Windows.Forms.Label lblProfile;
    }
}