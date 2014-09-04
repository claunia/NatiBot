/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmAddAccount.cs
Version        : 1.0.326
Author(s)      : Natalia Portillo
 
Component      : NatiBot

Revision       : r326
Last change by : Natalia Portillo
Date           : 2010/01/01

--[ License ] --------------------------------------------------------------
 
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.

----------------------------------------------------------------------------
Copyright (C) 2008-2014 Claunia.com
****************************************************************************/
namespace bot.GUI
{
    using bot;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using clControls;
    using System.Collections.Generic;

    public class frmAddAccount : Form
    {
        private clImageButton btnAddAccount;
        private clImageButton btnCancel;
        private IContainer components;
        private GroupBox gbEditAccounts;
        private GroupBox gbMasterSetup;
        private GroupBox groupBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private string m_Sim;
        private RadioButton radHome;
        private RadioButton radLast;
        private RadioButton radSet;
        private TextBox txtFirstName;
        private TextBox txtLastName;
        private TextBox txtPassword;
        private Label label5;
        private TextBox txtMasterIRC;
        private Label lblMasterIRC;
        private TextBox txtMaster;
        private GroupBox gbIRC;
        private TextBox txtIRCChannel;
        private Label label7;
        private Label label6;
        private TextBox txtServerPort;
        private TextBox txtServerHost;
        private Label lblServerHost;
        private ComboBox cbGrid;
        private Label label8;
        private CheckBox chkUseIRC;
        private Label lblAddAccount;
        private TextBox txtStartSim;

        public event AddAccountCallback OnAddAccount;

        private Point mouse_offset;

        public frmAddAccount()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.m_Sim = "Sim X Y Z";
            this.InitializeComponent();

            // LOGIN GRIDS
            Dictionary<string, string> dict = LoginGrids.ToDictionary();

            foreach (string key in dict.Keys)
            {
                this.cbGrid.Items.Add(key);
            }

            //Puts language resources
            this.gbMasterSetup.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbMasterSetup");
            this.lblMasterIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.lblMasterIRC");
            this.label5.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label5");
            this.groupBox1.Text = bot.Localization.clResourceManager.getText("frmAddAccount.groupBox1");
            this.label4.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label4");
            this.radSet.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radSet");
            this.radLast.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radLast");
            this.radHome.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radHome");
            this.gbEditAccounts.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbEditAccounts");
            this.label8.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label8");
            this.label1.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label1");
            this.label3.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label3");
            this.label2.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label2");
            this.btnAddAccount.Text = bot.Localization.clResourceManager.getText("frmAddAccount.btnAddAccount");
            this.btnCancel.Text = bot.Localization.clResourceManager.getText("frmAddAccount.btnCancel");
            this.gbIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbIRC");
            this.chkUseIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.chkUseIRC");
            this.label7.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label7");
            this.label6.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label6");
            this.lblServerHost.Text = bot.Localization.clResourceManager.getText("frmAddAccount.lblServerHost");
            this.Text = bot.Localization.clResourceManager.getText("frmAddAccount");
            this.lblAddAccount.Text = bot.Localization.clResourceManager.getText("frmAddAccount");
            //Ends putting language resources

            //Starts putting buttons
            this.btnAddAccount.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnAdd.idle");
            this.btnAddAccount.Image = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnAdd.idle");
            this.btnAddAccount.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnAdd.onclick");
            this.btnAddAccount.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnAdd.onhover");
            this.btnCancel.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.Image = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onclick");
            this.btnCancel.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onhover");
            //Ends putting buttons

            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAddAccount");
        }

        public frmAddAccount(LoginDetails loginDetails)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.m_Sim = "Sim X Y Z";
            this.InitializeComponent();
            this.txtFirstName.Text = loginDetails.FirstName;
            this.txtLastName.Text = loginDetails.LastName;
            this.txtPassword.Text = loginDetails.Password;
            this.txtMaster.Text = loginDetails.MasterName;
            this.txtStartSim.Text = loginDetails.StartLocation;

            // LOGIN GRIDS
            Dictionary<string, string> dict = LoginGrids.ToDictionary();

            foreach (string key in dict.Keys)
            {
                this.cbGrid.Items.Add(key);
            }

            SetSelectedGrid(loginDetails);

            this.txtMasterIRC.Text = loginDetails.IRC_Settings.Master;
            this.txtServerPort.Text = string.Concat(loginDetails.IRC_Settings.ServerPort);
            this.txtServerHost.Text = loginDetails.IRC_Settings.ServerHost;
            this.txtIRCChannel.Text = loginDetails.IRC_Settings.MainChannel;

            //Puts language resources
            this.Text = bot.Localization.clResourceManager.getText("frmAddAccount.Editing");
            this.lblAddAccount.Text = bot.Localization.clResourceManager.getText("frmAddAccount.Editing");
            this.btnAddAccount.Text = bot.Localization.clResourceManager.getText("frmAddAccount.btnEditAccount");
            this.gbMasterSetup.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbMasterSetup");
            this.lblMasterIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.lblMasterIRC");
            this.label5.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label5");
            this.groupBox1.Text = bot.Localization.clResourceManager.getText("frmAddAccount.groupBox1");
            this.label4.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label4");
            this.radSet.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radSet");
            this.radLast.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radLast");
            this.radHome.Text = bot.Localization.clResourceManager.getText("frmAddAccount.radHome");
            this.gbEditAccounts.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbEditAccounts");
            this.label8.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label8");
            this.label1.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label1");
            this.label3.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label3");
            this.label2.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label2");
            this.btnCancel.Text = bot.Localization.clResourceManager.getText("frmAddAccount.btnCancel");
            this.gbIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.gbIRC");
            this.chkUseIRC.Text = bot.Localization.clResourceManager.getText("frmAddAccount.chkUseIRC");
            this.label7.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label7");
            this.label6.Text = bot.Localization.clResourceManager.getText("frmAddAccount.label6");
            this.lblServerHost.Text = bot.Localization.clResourceManager.getText("frmAddAccount.lblServerHost");
            //Ends putting language resources

            //Starts putting buttons
            this.btnAddAccount.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnEdit.idle");
            this.btnAddAccount.Image = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnEdit.idle");
            this.btnAddAccount.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnEdit.onclick");
            this.btnAddAccount.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnEdit.onhover");
            this.btnCancel.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.Image = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onclick");
            this.btnCancel.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onhover");
            //Ends putting buttons

            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAddAccount");
        }

        private void SetSelectedGrid(LoginDetails loginDetails)
        {
            Dictionary<string, string> dict = LoginGrids.ToDictionary();

            if (dict.ContainsValue(loginDetails.Grid))
            {
                for (int i = 0; i < this.cbGrid.Items.Count; i++)
                {
                    foreach (string value in dict.Values)
                    {
                        if (this.cbGrid.Items[i].ToString() == value)
                        {
                            this.cbGrid.SelectedIndex = i;
                            break;
                        }
                    }
                    if (this.cbGrid.SelectedIndex == i)
                        break;
                }
            }
            else
            {
                this.cbGrid.SelectedIndex = 0;
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            LoginDetails loginDetails = new LoginDetails();
            loginDetails.FirstName = this.txtFirstName.Text;
            loginDetails.LastName = this.txtLastName.Text;
            loginDetails.Password = this.txtPassword.Text;
            loginDetails.MasterName = this.txtMaster.Text;
            loginDetails.StartLocation = this.txtStartSim.Text;

            Dictionary<string, string> dict = LoginGrids.ToDictionary();
            string customURI;

            if (dict.ContainsKey(this.cbGrid.Text))
            {
                if (dict.TryGetValue(this.cbGrid.Text, out customURI))
                {
                    loginDetails.GridCustomLoginUri = customURI;
                    loginDetails.Grid = loginDetails.GridCustomLoginUri;
                }
                else
                {
                    loginDetails.GridCustomLoginUri = this.cbGrid.Text;
                    loginDetails.Grid = loginDetails.GridCustomLoginUri;
                }
            }
            else
            {
                loginDetails.GridCustomLoginUri = this.cbGrid.Text;
                loginDetails.Grid = loginDetails.GridCustomLoginUri;
            }

            loginDetails.IRC_Settings = new IRCSettings();
            loginDetails.IRC_Settings.isUsingIRC = this.chkUseIRC.Checked;
            loginDetails.IRC_Settings.Master = this.txtMasterIRC.Text;
            loginDetails.IRC_Settings.ServerPort = int.Parse(this.txtServerPort.Text);
            loginDetails.IRC_Settings.ServerHost = this.txtServerHost.Text;
            loginDetails.IRC_Settings.MainChannel = this.txtIRCChannel.Text;

            
            BotAccount account = new BotAccount(loginDetails);
            if (this.OnAddAccount != null)
            {
                this.OnAddAccount(account);
            }
            base.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void frmAddAccount_Load(object sender, EventArgs e)
        {
            if (btnAddAccount.Text != bot.Localization.clResourceManager.getText("frmAddAccount.Editing"))
                this.txtMaster.Text = "Phillip Linden";
            if (btnAddAccount.Text != bot.Localization.clResourceManager.getText("frmAddAccount.Editing"))
                this.cbGrid.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddAccount));
            this.gbMasterSetup = new System.Windows.Forms.GroupBox();
            this.txtMasterIRC = new System.Windows.Forms.TextBox();
            this.lblMasterIRC = new System.Windows.Forms.Label();
            this.txtMaster = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radSet = new System.Windows.Forms.RadioButton();
            this.radLast = new System.Windows.Forms.RadioButton();
            this.radHome = new System.Windows.Forms.RadioButton();
            this.txtStartSim = new System.Windows.Forms.TextBox();
            this.gbEditAccounts = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbGrid = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnAddAccount = new clControls.clImageButton();
            this.btnCancel = new clControls.clImageButton();
            this.gbIRC = new System.Windows.Forms.GroupBox();
            this.chkUseIRC = new System.Windows.Forms.CheckBox();
            this.txtIRCChannel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.txtServerHost = new System.Windows.Forms.TextBox();
            this.lblServerHost = new System.Windows.Forms.Label();
            this.lblAddAccount = new System.Windows.Forms.Label();
            this.gbMasterSetup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbEditAccounts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.gbIRC.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMasterSetup
            // 
            this.gbMasterSetup.BackColor = System.Drawing.Color.Transparent;
            this.gbMasterSetup.Controls.Add(this.txtMasterIRC);
            this.gbMasterSetup.Controls.Add(this.lblMasterIRC);
            this.gbMasterSetup.Controls.Add(this.txtMaster);
            this.gbMasterSetup.Controls.Add(this.label5);
            this.gbMasterSetup.ForeColor = System.Drawing.Color.White;
            this.gbMasterSetup.Location = new System.Drawing.Point(255, 40);
            this.gbMasterSetup.Name = "gbMasterSetup";
            this.gbMasterSetup.Size = new System.Drawing.Size(272, 103);
            this.gbMasterSetup.TabIndex = 8;
            this.gbMasterSetup.TabStop = false;
            this.gbMasterSetup.Text = "Master";
            // 
            // txtMasterIRC
            // 
            this.txtMasterIRC.BackColor = System.Drawing.Color.Black;
            this.txtMasterIRC.ForeColor = System.Drawing.Color.White;
            this.txtMasterIRC.Location = new System.Drawing.Point(14, 71);
            this.txtMasterIRC.Name = "txtMasterIRC";
            this.txtMasterIRC.Size = new System.Drawing.Size(169, 20);
            this.txtMasterIRC.TabIndex = 3;
            this.txtMasterIRC.Text = "NiCK";
            // 
            // lblMasterIRC
            // 
            this.lblMasterIRC.AutoSize = true;
            this.lblMasterIRC.Location = new System.Drawing.Point(11, 55);
            this.lblMasterIRC.Name = "lblMasterIRC";
            this.lblMasterIRC.Size = new System.Drawing.Size(93, 13);
            this.lblMasterIRC.TabIndex = 2;
            this.lblMasterIRC.Text = "Master\'s IRC nick:";
            // 
            // txtMaster
            // 
            this.txtMaster.BackColor = System.Drawing.Color.Black;
            this.txtMaster.ForeColor = System.Drawing.Color.White;
            this.txtMaster.Location = new System.Drawing.Point(14, 32);
            this.txtMaster.Name = "txtMaster";
            this.txtMaster.Size = new System.Drawing.Size(170, 20);
            this.txtMaster.TabIndex = 1;
            this.txtMaster.Text = "Phillip Linden";
            //this.txtMaster.Leave += new System.EventHandler(this.txtMaster_Leave);
            //this.txtMaster.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtMaster_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Master\'s name:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.radSet);
            this.groupBox1.Controls.Add(this.radLast);
            this.groupBox1.Controls.Add(this.radHome);
            this.groupBox1.Controls.Add(this.txtStartSim);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(227, 84);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Starting location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 24;
            this.label4.Text = "Location:";
            // 
            // radSet
            // 
            this.radSet.AutoSize = true;
            this.radSet.Location = new System.Drawing.Point(124, 19);
            this.radSet.Name = "radSet";
            this.radSet.Size = new System.Drawing.Size(44, 17);
            this.radSet.TabIndex = 20;
            this.radSet.Text = "Set:";
            this.radSet.UseVisualStyleBackColor = true;
            this.radSet.CheckedChanged += new System.EventHandler(this.radSet_CheckedChanged);
            // 
            // radLast
            // 
            this.radLast.AutoSize = true;
            this.radLast.Checked = true;
            this.radLast.Location = new System.Drawing.Point(6, 19);
            this.radLast.Name = "radLast";
            this.radLast.Size = new System.Drawing.Size(45, 17);
            this.radLast.TabIndex = 19;
            this.radLast.TabStop = true;
            this.radLast.Text = "Last";
            this.radLast.UseVisualStyleBackColor = true;
            this.radLast.CheckedChanged += new System.EventHandler(this.radLast_CheckedChanged);
            // 
            // radHome
            // 
            this.radHome.AutoSize = true;
            this.radHome.Location = new System.Drawing.Point(65, 19);
            this.radHome.Name = "radHome";
            this.radHome.Size = new System.Drawing.Size(53, 17);
            this.radHome.TabIndex = 18;
            this.radHome.Text = "Home";
            this.radHome.UseVisualStyleBackColor = true;
            this.radHome.CheckedChanged += new System.EventHandler(this.radHome_CheckedChanged);
            // 
            // txtStartSim
            // 
            this.txtStartSim.BackColor = System.Drawing.Color.Black;
            this.txtStartSim.Enabled = false;
            this.txtStartSim.ForeColor = System.Drawing.Color.White;
            this.txtStartSim.Location = new System.Drawing.Point(6, 55);
            this.txtStartSim.Name = "txtStartSim";
            this.txtStartSim.Size = new System.Drawing.Size(209, 20);
            this.txtStartSim.TabIndex = 4;
            this.txtStartSim.Text = "last";
            this.txtStartSim.TextChanged += new System.EventHandler(this.txtStartSim_TextChanged);
            // 
            // gbEditAccounts
            // 
            this.gbEditAccounts.BackColor = System.Drawing.Color.Transparent;
            this.gbEditAccounts.Controls.Add(this.label8);
            this.gbEditAccounts.Controls.Add(this.cbGrid);
            this.gbEditAccounts.Controls.Add(this.label1);
            this.gbEditAccounts.Controls.Add(this.label3);
            this.gbEditAccounts.Controls.Add(this.txtLastName);
            this.gbEditAccounts.Controls.Add(this.label2);
            this.gbEditAccounts.Controls.Add(this.txtFirstName);
            this.gbEditAccounts.Controls.Add(this.txtPassword);
            this.gbEditAccounts.ForeColor = System.Drawing.Color.White;
            this.gbEditAccounts.Location = new System.Drawing.Point(12, 40);
            this.gbEditAccounts.Name = "gbEditAccounts";
            this.gbEditAccounts.Size = new System.Drawing.Size(227, 139);
            this.gbEditAccounts.TabIndex = 21;
            this.gbEditAccounts.TabStop = false;
            this.gbEditAccounts.Text = "Login details";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 94);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Login Grid / URI:";
            // 
            // cbGrid
            // 
            this.cbGrid.BackColor = System.Drawing.Color.Black;
            this.cbGrid.ForeColor = System.Drawing.Color.White;
            this.cbGrid.FormattingEnabled = true;
            this.cbGrid.Location = new System.Drawing.Point(9, 110);
            this.cbGrid.Name = "cbGrid";
            this.cbGrid.Size = new System.Drawing.Size(206, 21);
            this.cbGrid.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Name:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Password:";
            // 
            // txtLastName
            // 
            this.txtLastName.BackColor = System.Drawing.Color.Black;
            this.txtLastName.ForeColor = System.Drawing.Color.White;
            this.txtLastName.Location = new System.Drawing.Point(115, 32);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(100, 20);
            this.txtLastName.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Lastname:";
            // 
            // txtFirstName
            // 
            this.txtFirstName.BackColor = System.Drawing.Color.Black;
            this.txtFirstName.ForeColor = System.Drawing.Color.White;
            this.txtFirstName.Location = new System.Drawing.Point(9, 32);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(100, 20);
            this.txtFirstName.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.Black;
            this.txtPassword.ForeColor = System.Drawing.Color.White;
            this.txtPassword.Location = new System.Drawing.Point(9, 71);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(206, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAccount.ButtonBitmap = null;
            this.btnAddAccount.ButtonState = clControls.ButtonState.Normal;
            this.btnAddAccount.DisabledBitmap = null;
            this.btnAddAccount.Image = bot.Localization.clResourceManager.getButton("frmAddAccounts.btnEdit.idle");
            this.btnAddAccount.Location = new System.Drawing.Point(38, 305);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.OnMouseClickBitmap = null;
            this.btnAddAccount.OnMouseOverBitmap = null;
            this.btnAddAccount.Size = new System.Drawing.Size(76, 23);
            this.btnAddAccount.TabIndex = 6;
            this.btnAddAccount.TabStop = false;
            this.btnAddAccount.Text = "Add";
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.ButtonBitmap = null;
            this.btnCancel.ButtonState = clControls.ButtonState.Normal;
            this.btnCancel.DisabledBitmap = null;
            this.btnCancel.Image = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.Location = new System.Drawing.Point(441, 305);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OnMouseClickBitmap = null;
            this.btnCancel.OnMouseOverBitmap = null;
            this.btnCancel.Size = new System.Drawing.Size(76, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gbIRC
            // 
            this.gbIRC.BackColor = System.Drawing.Color.Transparent;
            this.gbIRC.Controls.Add(this.chkUseIRC);
            this.gbIRC.Controls.Add(this.txtIRCChannel);
            this.gbIRC.Controls.Add(this.label7);
            this.gbIRC.Controls.Add(this.label6);
            this.gbIRC.Controls.Add(this.txtServerPort);
            this.gbIRC.Controls.Add(this.txtServerHost);
            this.gbIRC.Controls.Add(this.lblServerHost);
            this.gbIRC.ForeColor = System.Drawing.Color.White;
            this.gbIRC.Location = new System.Drawing.Point(255, 149);
            this.gbIRC.Name = "gbIRC";
            this.gbIRC.Size = new System.Drawing.Size(272, 150);
            this.gbIRC.TabIndex = 23;
            this.gbIRC.TabStop = false;
            this.gbIRC.Text = "IRC server configuration";
            this.gbIRC.Enter += new System.EventHandler(this.gbIRC_Enter);
            // 
            // chkUseIRC
            // 
            this.chkUseIRC.AutoSize = true;
            this.chkUseIRC.Location = new System.Drawing.Point(9, 19);
            this.chkUseIRC.Name = "chkUseIRC";
            this.chkUseIRC.Size = new System.Drawing.Size(104, 17);
            this.chkUseIRC.TabIndex = 7;
            this.chkUseIRC.Text = "Use IRC server?";
            this.chkUseIRC.UseVisualStyleBackColor = true;
            this.chkUseIRC.CheckedChanged += new System.EventHandler(this.chkUseIRC_CheckedChanged);
            // 
            // txtIRCChannel
            // 
            this.txtIRCChannel.BackColor = System.Drawing.Color.Black;
            this.txtIRCChannel.ForeColor = System.Drawing.Color.White;
            this.txtIRCChannel.Location = new System.Drawing.Point(9, 109);
            this.txtIRCChannel.Name = "txtIRCChannel";
            this.txtIRCChannel.Size = new System.Drawing.Size(175, 20);
            this.txtIRCChannel.TabIndex = 5;
            this.txtIRCChannel.Text = "#BOTS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Channel:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(187, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Port (No SSL):";
            // 
            // txtServerPort
            // 
            this.txtServerPort.BackColor = System.Drawing.Color.Black;
            this.txtServerPort.ForeColor = System.Drawing.Color.White;
            this.txtServerPort.Location = new System.Drawing.Point(187, 68);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(61, 20);
            this.txtServerPort.TabIndex = 2;
            this.txtServerPort.Text = "6667";
            // 
            // txtServerHost
            // 
            this.txtServerHost.BackColor = System.Drawing.Color.Black;
            this.txtServerHost.ForeColor = System.Drawing.Color.White;
            this.txtServerHost.Location = new System.Drawing.Point(9, 68);
            this.txtServerHost.Name = "txtServerHost";
            this.txtServerHost.Size = new System.Drawing.Size(174, 20);
            this.txtServerHost.TabIndex = 1;
            this.txtServerHost.Text = "IRC.IRC-HISPANO.ORG";
            this.txtServerHost.TextChanged += new System.EventHandler(this.txtServerHost_TextChanged);
            // 
            // lblServerHost
            // 
            this.lblServerHost.AutoSize = true;
            this.lblServerHost.Location = new System.Drawing.Point(9, 51);
            this.lblServerHost.Name = "lblServerHost";
            this.lblServerHost.Size = new System.Drawing.Size(81, 13);
            this.lblServerHost.TabIndex = 0;
            this.lblServerHost.Text = "Server address:";
            // 
            // lblAddAccount
            // 
            this.lblAddAccount.AutoSize = true;
            this.lblAddAccount.BackColor = System.Drawing.Color.Transparent;
            this.lblAddAccount.Font = new System.Drawing.Font("Lucida Handwriting", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddAccount.ForeColor = System.Drawing.Color.White;
            this.lblAddAccount.Location = new System.Drawing.Point(34, 9);
            this.lblAddAccount.Name = "lblAddAccount";
            this.lblAddAccount.Size = new System.Drawing.Size(122, 19);
            this.lblAddAccount.TabIndex = 24;
            this.lblAddAccount.Text = "Add account";
            // 
            // frmAddAccount
            // 
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAddAccount");
            this.ClientSize = new System.Drawing.Size(550, 340);
            this.Controls.Add(this.lblAddAccount);
            this.Controls.Add(this.gbIRC);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.gbMasterSetup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnAddAccount);
            this.Controls.Add(this.gbEditAccounts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAddAccount";
            this.Text = "Add account";
            this.TransparencyKey = System.Drawing.Color.Yellow;
            this.Load += new System.EventHandler(this.frmAddAccount_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmAddAccount_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmAddAccount_MouseMove);
            this.gbMasterSetup.ResumeLayout(false);
            this.gbMasterSetup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbEditAccounts.ResumeLayout(false);
            this.gbEditAccounts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.gbIRC.ResumeLayout(false);
            this.gbIRC.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void radHome_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStartSim.Text = "home";
            this.txtStartSim.Enabled = false;
        }

        private void radLast_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStartSim.Text = "last";
            this.txtStartSim.Enabled = false;
        }

        private void radSet_CheckedChanged(object sender, EventArgs e)
        {
            this.txtStartSim.Text = this.m_Sim;
            this.txtStartSim.Enabled = true;
            if (this.txtStartSim.Focused)
            {
                this.m_Sim = this.txtStartSim.Text;
            }
        }

        private void txtStartSim_TextChanged(object sender, EventArgs e)
        {
            if (this.txtStartSim.Focused)
            {
                this.m_Sim = this.txtStartSim.Text;
            }
        }

        public delegate void AddAccountCallback(BotAccount loginDetails);

        /*private void txtMaster_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtMaster.Text.Equals("Phillip Linden"))
                txtMaster.Text = "";
        }

        private void txtMaster_Leave(object sender, EventArgs e)
        {
            if (txtMaster.Text.Equals(""))
                txtMaster.Text = "Phillip Linden";
        }*/

        private void chkUseIRC_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = chkUseIRC.Checked;

            txtMasterIRC.Enabled = isChecked;
            txtServerHost.Enabled = isChecked;
            txtServerPort.Enabled = isChecked;
            txtIRCChannel.Enabled = isChecked;
        }

        private void txtServerHost_TextChanged(object sender, EventArgs e)
        {

        }

        private void gbIRC_Enter(object sender, EventArgs e)
        {

        }

        private void frmAddAccount_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmAddAccount_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }
    }
}

