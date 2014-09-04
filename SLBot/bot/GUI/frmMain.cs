/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmMain.cs
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
    using bot.license;
    using bot.GUI;
    using OpenMetaverse;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using System.Net;
    using System.Collections.Generic;
    using clControls;
    using System.Threading;
    using Claunia.clUtils;

    public class frmMain : Form
    {
        private clImageButton btnSetMaster;
        private CheckBox cbGreetMaster;
        private ContextMenuStrip cmsAccount;
        private IContainer components;
        private ToolStripMenuItem editToolStripMenuItem;
        private GroupBox gbAccounts;
        private GroupBox gbMaster;
        private Label lblAccoutnsInfo;
        private Label lblBotsOnline;
        private ToolStripMenuItem loginToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private ToolStripMenuItem objectsToolStripMenuItem;
        private OpenFileDialog openAccountList;
        private ToolStripMenuItem removeToolStripMenuItem;
        private SaveFileDialog saveAccountList;
        private ToolTip ttGreetMaster;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private CheckBox cbRelayConvos;
        private CheckBox cbAgentUpdate;
        private TextBox txtIRCMaster;
        private Label label4;
        private TextBox txtIRCChannel;
        private Label label3;
        private TextBox txtIRCPort;
        private Label label2;
        private TextBox txtIRCServer;
        private Label label1;
        private ToolStrip tsBots;
        private ToolStripButton tsbLoad;
        private ToolStripButton toolStripButton2;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbRemove;
        private ToolStripSeparator sep2;
        private ToolStripButton tsbLogin;
        private ToolStripButton tsbLoginAll;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbLogout;
        private ToolStripButton tsbLogoutAll;
        private TextBox txtSetMaster;
        private Button cmdCrash;
        //private bot.HTTP.HTTPDaemon webserver;
        private clImageButton btnObjects2;
        private clImageButton btnImport;
        private OpenFileDialog dlgImport;
        private ProgressBar prgProgress;
        private clImageButton btnAbout;
        private Label lblUploadStatus;
        private Label lblMain;
        private GroupBox gbBotConfiguration;
        private clImageButton btnConsole;

        private frmConsole console;
        private frmChat chat;

        private Dictionary<Interface, TabPage> Interfaces = new Dictionary<Interface, TabPage>();
        private clImageButton btnExit;
        private GroupBox gbSettings;
        private ComboBox cmbLanguage;
        private Label lblLanguage;
        private ComboBox cmbSkin;
        private Label lblSkin;
        private CheckBox chkLogConsole;
        private clImageButton btnChat;
        private clImageButton btnMap;
        private clImageButton btnFriends;
        private clImageButton btnGroups;
        private clImageButton btnAvatars;
        private CheckBox chkHaveLuck;
        private CheckBox chkTouchMidnightMania;
        private CheckBox chkInformFriends;
        private CheckBox chkInventoryOffers;
        private CheckBox chkSounds;
        private CheckBox chkTextures;
        private CheckBox chkAnimations;
        private CheckBox chkLogChat;
        private clImageButton btnInventory;
        private AccountList accList;
        private Point mouse_offset;

        private delegate void StartImportCallback(int maxvalue);

        private delegate void SetProgressValueCallback(int value,int maxvalue);

        private delegate void StopImportCallback();

        public frmMain()
        {
            this.InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            //Puts language resources
            //this.tabTools.Text = bot.Localization.clResourceManager.getText("frmMain.tabTools");
            this.lblUploadStatus.Text = "";
            this.btnImport.Text = bot.Localization.clResourceManager.getText("frmMain.btnImport");
            this.btnObjects2.Text = bot.Localization.clResourceManager.getText("frmMain.btnObjects");
            this.cmdCrash.Text = bot.Localization.clResourceManager.getText("frmMain.cmdCrash");
            this.btnSetMaster.Text = bot.Localization.clResourceManager.getText("frmMain.btnSetMaster");
            //this.tabConsole.Text = bot.Localization.clResourceManager.getText("frmMain.tabConsole");
            //this.tabBotSettings.Text = bot.Localization.clResourceManager.getText("frmMain.tabBotSettings");
            this.gbBotConfiguration.Text = bot.Localization.clResourceManager.getText("frmMain.tabBotSettings");
            this.groupBox2.Text = bot.Localization.clResourceManager.getText("frmMain.groupBox2");
            this.label4.Text = bot.Localization.clResourceManager.getText("frmMain.label4");
            this.label3.Text = bot.Localization.clResourceManager.getText("frmMain.label3");
            this.label2.Text = bot.Localization.clResourceManager.getText("frmMain.label2");
            this.label1.Text = bot.Localization.clResourceManager.getText("frmMain.label1");
            this.groupBox1.Text = bot.Localization.clResourceManager.getText("frmMain.groupBox1");
            this.cbRelayConvos.Text = bot.Localization.clResourceManager.getText("frmMain.cbRelayConvos");
            this.cbAgentUpdate.Text = bot.Localization.clResourceManager.getText("frmMain.cbAgentUpdate");
            this.cbGreetMaster.Text = bot.Localization.clResourceManager.getText("frmMain.cbGreetMaster");
            this.editToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmMain.editToolStripMenuItem");
            this.loginToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmMain.loginToolStripMenuItem");
            this.logoutToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmMain.logoutToolStripMenuItem");
            this.objectsToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmMain.objectsToolStripMenuItem");
            this.removeToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmMain.removeToolStripMenuItem");
            this.gbAccounts.Text = bot.Localization.clResourceManager.getText("frmMain.gbAccounts");

            this.tsBots.Text = bot.Localization.clResourceManager.getText("frmMain.tsBots");
            this.tsbLoad.Text = bot.Localization.clResourceManager.getText("frmMain.tsbLoad");
            this.toolStripButton2.Text = bot.Localization.clResourceManager.getText("frmMain.toolStripButton2");
            this.tsbAdd.Text = bot.Localization.clResourceManager.getText("frmMain.tsbAdd");
            this.tsbRemove.Text = bot.Localization.clResourceManager.getText("frmMain.tsbRemove");
            this.tsbLogin.Text = bot.Localization.clResourceManager.getText("frmMain.tsbLogin");
            this.tsbLoginAll.Text = bot.Localization.clResourceManager.getText("frmMain.tsbLoginAll");
            this.tsbLogout.Text = bot.Localization.clResourceManager.getText("frmMain.tsbLogout");
            this.tsbLogoutAll.Text = bot.Localization.clResourceManager.getText("frmMain.tsbLogoutAll");
            this.lblAccoutnsInfo.Text = bot.Localization.clResourceManager.getText("frmMain.lblAccoutnsInfo");
            this.openAccountList.Filter = bot.Localization.clResourceManager.getText("frmMain.openAccountList");
            this.saveAccountList.Filter = bot.Localization.clResourceManager.getText("frmMain.openAccountList");
            this.ttGreetMaster.ToolTipTitle = bot.Localization.clResourceManager.getText("frmMain.ttGreetMaster");
            this.dlgImport.Title = bot.Localization.clResourceManager.getText("frmMain.dlgImport");
            this.btnAbout.Text = bot.Localization.clResourceManager.getText("frmMain.btnAbout");
            this.btnConsole.Text = bot.Localization.clResourceManager.getText("frmMain.tabConsole");
            this.gbSettings.Text = bot.Localization.clResourceManager.getText("frmMain.gbSettings");
            this.chkLogConsole.Text = bot.Localization.clResourceManager.getText("frmMain.chkLogConsole");
            this.lblLanguage.Text = bot.Localization.clResourceManager.getText("frmMain.lblLanguage");

            this.cmbLanguage.Items.AddRange(bot.Localization.clResourceManager.getAvailableLanguages());
            this.cmbSkin.Items.AddRange(bot.Localization.clResourceManager.getAvailableSkins().ToArray());

            this.btnAvatars.Text = bot.Localization.clResourceManager.getText("frmMain.btnAvatars");
            this.btnGroups.Text = bot.Localization.clResourceManager.getText("frmMain.btnGroups");
            this.btnFriends.Text = bot.Localization.clResourceManager.getText("frmMain.btnFriends");
            this.btnInventory.Text = bot.Localization.clResourceManager.getText("frmMain.btnInventory");
            this.btnMap.Text = bot.Localization.clResourceManager.getText("frmMain.btnMap");
            this.chkAnimations.Text = bot.Localization.clResourceManager.getText("frmMain.chkAnimations");
            this.chkTextures.Text = bot.Localization.clResourceManager.getText("frmMain.chkTextures");
            this.chkSounds.Text = bot.Localization.clResourceManager.getText("frmMain.chkSounds");
            this.chkInventoryOffers.Text = bot.Localization.clResourceManager.getText("frmMain.chkInventoryOffers");
            this.chkInformFriends.Text = bot.Localization.clResourceManager.getText("frmMain.chkInformFriends");
            this.chkTouchMidnightMania.Text = bot.Localization.clResourceManager.getText("frmMain.chkTouchMidnightMania");
            this.chkHaveLuck.Text = bot.Localization.clResourceManager.getText("frmMain.chkHaveLuck");
            this.chkLogChat.Text = bot.Localization.clResourceManager.getText("frmMain.chkLogChat");

            //Ends putting language resources

            //Starts putting buttons
            this.btnSetMaster.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnSetMaster.idle");
            this.btnSetMaster.Image = bot.Localization.clResourceManager.getButton("frmMain.btnSetMaster.idle");
            this.btnSetMaster.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnSetMaster.onclick");
            this.btnSetMaster.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnSetMaster.onhover");
            this.btnObjects2.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnObjects.idle");
            this.btnObjects2.Image = bot.Localization.clResourceManager.getButton("frmMain.btnObjects.idle");
            this.btnObjects2.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnObjects.onclick");
            this.btnObjects2.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnObjects.onhover");
            this.btnImport.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnImport.idle");
            this.btnImport.Image = bot.Localization.clResourceManager.getButton("frmMain.btnImport.idle");
            this.btnImport.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnImport.onclick");
            this.btnImport.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnImport.onhover");
            this.btnAbout.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAbout.idle");
            this.btnAbout.Image = bot.Localization.clResourceManager.getButton("frmMain.btnAbout.idle");
            this.btnAbout.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAbout.onclick");
            this.btnAbout.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAbout.onhover");
            this.btnConsole.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnConsole.idle");
            this.btnConsole.Image = bot.Localization.clResourceManager.getButton("frmMain.btnConsole.idle");
            this.btnConsole.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnConsole.onclick");
            this.btnConsole.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnConsole.onhover");
            this.btnExit.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnExit.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnExit.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnExit.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.btnChat.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnChat.idle");
            this.btnChat.Image = bot.Localization.clResourceManager.getButton("frmMain.btnChat.idle");
            this.btnChat.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnChat.onclick");
            this.btnChat.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnChat.onhover");
            this.btnAvatars.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAvatars.idle");
            this.btnAvatars.Image = bot.Localization.clResourceManager.getButton("frmMain.btnAvatars.idle");
            this.btnAvatars.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAvatars.onclick");
            this.btnAvatars.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnAvatars.onhover");
            this.btnGroups.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnGroups.idle");
            this.btnGroups.Image = bot.Localization.clResourceManager.getButton("frmMain.btnGroups.idle");
            this.btnGroups.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnGroups.onclick");
            this.btnGroups.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnGroups.onhover");
            this.btnInventory.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnInventory.idle");
            this.btnInventory.Image = bot.Localization.clResourceManager.getButton("frmMain.btnInventory.idle");
            this.btnInventory.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnInventory.onclick");
            this.btnInventory.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnInventory.onhover");
            this.btnFriends.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnFriends.idle");
            this.btnFriends.Image = bot.Localization.clResourceManager.getButton("frmMain.btnFriends.idle");
            this.btnFriends.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnFriends.onclick");
            this.btnFriends.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnFriends.onhover");
            this.btnMap.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnMap.idle");
            this.btnMap.Image = bot.Localization.clResourceManager.getButton("frmMain.btnMap.idle");
            this.btnMap.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnMap.onclick");
            this.btnMap.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmMain.btnMap.onhover");
            //Ends putting buttons

            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmMain");

            if (console == null)
            {
                console = new frmConsole();
                console.OnOutputSend += new frmConsole.OutputSendCallback(this.Console_OutPutSend);
            }

            if (chat == null)
            {
                chat = new frmChat();
            }

            cmbLanguage.SelectedItem = bot.Localization.clResourceManager.getCurrentLanguage();
            cmbSkin.SelectedItem = bot.Localization.clResourceManager.getCurrentSkin();

            this.Paint += new PaintEventHandler(frmMain_Paint);
            this.DisableControls(true);

            Program.NBStats.AddStatData(DateTime.Now.ToString() + " NatiBot main window created."); // STATISTICS
        }

        void frmMain_Paint(object sender, PaintEventArgs e)
        {
            lock (Interfaces)
            {
                foreach (Interface iface in Interfaces.Keys)
                {
                    iface.Paint(sender, e);
                }
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            frmAddAccount account = new frmAddAccount();
            account.OnAddAccount += new frmAddAccount.AddAccountCallback(this.frmAddAcc_OnAddAccount);
            account.Show();
        }

        private void btnLoadAccs_Click(object sender, EventArgs e)
        {
            this.openAccountList.ShowDialog();
            //ACCLIST
            this.DisableLoginControls(this.accList.Items.Count == 0);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                Program.NBStats.AddStatData(String.Format("{0}: {1} logged in on {2}", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName,
                    this.SelectedAccount.LoginDetails.GridCustomLoginUri)); // STATISTICS
                this.SelectedAccount.Connect();
            }
        }

        private void btnLoginAll_Click(object sender, EventArgs e)
        {
            //ACCLIST
            foreach (ListViewItem item in this.accList.Items)
            {
                BotAccount _bAccount;
                _bAccount = (BotAccount)item.Tag;
                _bAccount.Connect();
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                Program.NBStats.AddStatData(String.Format("{0}: {1} logged out on {2}", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName,
                    this.SelectedAccount.LoginDetails.GridCustomLoginUri)); // STATISTICS
                this.SelectedAccount.Disconnect(false);
            }
        }

        private void btnLogoutAll_Click(object sender, EventArgs e)
        {
            //ACCLIST
            foreach (ListViewItem item in this.accList.Items)
            {
                BotAccount _bAccount;
                _bAccount = (BotAccount)item.Tag;
                _bAccount.Disconnect(false);
            }
        }

        private void btnRemoveAccount_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.Delete();
            }
            this.AutoSave();
            //ACCLIST
            this.DisableLoginControls(this.accList.Items.Count == 0);
        }

        private void AutoSave()
        {
            this.SaveXmlAccounts();
        }

        private void btnSaveAccs_Click(object sender, EventArgs e)
        {
            this.saveAccountList.ShowDialog();
        }

        private void btnSetMaster_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                if (this.SelectedAccount.Client != null)
                {
                    this.SelectedAccount.Client.MasterName = this.txtSetMaster.Text;
                }
                else if (this.SelectedAccount.LoginDetails != null)
                {
                    this.SelectedAccount.LoginDetails.MasterName = this.txtSetMaster.Text;
                    this.SelectedAccount.ListItem.SubItems[2].Text = this.txtSetMaster.Text;
                }

                Program.NBStats.AddStatData(String.Format("{0}: {1} changing master to {2}.", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName, this.txtSetMaster.Text)); // STATISTICS
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                if (this.SelectedAccount.LoginDetails == null)
                {
                    MessageBox.Show("SelectedAccount.LoginDetails == null");
                }
                if (this.SelectedAccount.Client != null)
                {
                    MessageBox.Show(bot.Localization.clResourceManager.getText("frmMain.ErrorEditingConnected"));
                }
                else
                {
                    frmAddAccount account = new frmAddAccount(this.SelectedAccount.LoginDetails);
                    account.OnAddAccount += new frmAddAccount.AddAccountCallback(this.frmAddAcc_OnEditAccount);
                    account.Show();
                }
            }
        }

        private void frmAddAcc_OnAddAccount(BotAccount botAccount)
        {
            //ACCLIST

            botAccount.OnDialogScriptReceived += new BotAccount.OnDialogScriptReceivedCallback(botAccount_OnDialogScriptReceived);
            botAccount.OnStatusChange += new BotAccount.StatusChangeCallback(botAccount_OnStatusChange);
            botAccount.OnAccountRemoved += new BotAccount.AccountRemovedCallback(botAccount_OnAccountRemoved);
            botAccount.OnMasterChange += new BotAccount.MasterChangeCallback(botAccount_OnMasterChange);
            botAccount.OnLocationChanged += new BotAccount.LocationChangeCallback(botAccount_OnLocationChanged);

            ListViewItem account = new ListViewItem(botAccount.LoginDetails.FullName);
            account.Tag = botAccount;
            account.SubItems.Add(bot.Localization.clResourceManager.getText("botAccount.Offline"));
            account.SubItems.Add(botAccount.LoginDetails.MasterName);
            account.SubItems.Add(botAccount.LoginDetails.StartLocation);
            botAccount.ListItem = account;
            
            this.accList.Items.Add(account);
            
            this.DisableLoginControls(this.accList.Items.Count == 0);

            this.AutoSave();
            Program.NBStats.AddStatData(String.Format("{0}: Added account {1} on {2}", DateTime.Now.ToString(), botAccount.LoginDetails.FullName, botAccount.LoginDetails.GridCustomLoginUri)); // STATISTICS
        }

        void botAccount_OnLocationChanged(string newLocation, ListViewItem item)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    botAccount_OnLocationChanged(newLocation, item);
                });
            else
                item.SubItems[3].Text = newLocation;
        }

        void botAccount_OnMasterChange(string newMaster, ListViewItem item)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    botAccount_OnMasterChange(newMaster, item);
                });
            else
                item.SubItems[2].Text = newMaster;
        }

        void botAccount_OnAccountRemoved(ListViewItem item)
        {
            if (!this.IsHandleCreated)
                return;
    
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    botAccount_OnAccountRemoved(item);
                });
            else
            {
                item.SubItems[1].Text = bot.Localization.clResourceManager.getText("botAccount.Removing");
                item.ForeColor = Color.Gray;
            }
        }

        private void botAccount_OnDialogScriptReceived(SecondLifeBot bot, ScriptDialogEventArgs args)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    botAccount_OnDialogScriptReceived(bot, args);
                });
            else
                new bot.GUI.frmScriptDialog(bot, args).Show();
        }

        private void botAccount_OnStatusChange(string newStatus, Color color, ListViewItem item)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    botAccount_OnStatusChange(newStatus, color, item);
                });
            else
            {
                item.SubItems[1].Text = newStatus;
                item.ForeColor = color;
            }
        }

        private void frmAddAcc_OnEditAccount(BotAccount botAccount)
        {
            //ACCLIST
            this.tsbRemove.PerformClick();
            botAccount.OnDialogScriptReceived += new BotAccount.OnDialogScriptReceivedCallback(botAccount_OnDialogScriptReceived);
            botAccount.OnStatusChange += new BotAccount.StatusChangeCallback(botAccount_OnStatusChange);
            botAccount.OnAccountRemoved += new BotAccount.AccountRemovedCallback(botAccount_OnAccountRemoved);
            botAccount.OnMasterChange += new BotAccount.MasterChangeCallback(botAccount_OnMasterChange);
            botAccount.OnLocationChanged += new BotAccount.LocationChangeCallback(botAccount_OnLocationChanged);

            ListViewItem account = new ListViewItem(botAccount.LoginDetails.FullName);
            account.Tag = botAccount;
            account.SubItems.Add(bot.Localization.clResourceManager.getText("botAccount.Offline"));
            account.SubItems.Add(botAccount.LoginDetails.MasterName);
            account.SubItems.Add(botAccount.LoginDetails.StartLocation);
            botAccount.ListItem = account;

            this.accList.Items.Add(account);
            this.AutoSave();
            Program.NBStats.AddStatData(String.Format("{0}: Edited account {1} on {2}", DateTime.Now.ToString(), botAccount.LoginDetails.FullName, botAccount.LoginDetails.GridCustomLoginUri)); // STATISTICS
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            bot.license.Version v = new bot.license.Version();

            if (Utilities.GetRunningRuntime() != Utilities.Runtime.Mono)
                this.TransparencyKey = Color.Yellow;

            string str = v.ToString() + " " + v.v_rev;
            this.Text = string.Format("NatiBot - v{0}", str);
            lblMain.Text = string.Format("NatiBot - v{0}", str);
            this.LoadXmlAccounts();
            //ACCLIST
            this.DisableLoginControls(this.accList.Items.Count == 0);
            chkLogConsole.Checked = Program.getWriteConsoleToFileSetting();
            chkLogChat.Checked = Program.getWriteChatToFileSetting();

            if (console != null)
            {
                console.Show();
                console.Hide();
            }
            if (chat != null)
            {
                chat.Show();
                chat.Hide();
            }

            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("bot.Starts"));

//            webserver = new bot.HTTP.HTTPDaemon(9001, "DONGS", this);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnAbout = new clControls.clImageButton();
            this.prgProgress = new System.Windows.Forms.ProgressBar();
            this.btnImport = new clControls.clImageButton();
            this.btnObjects2 = new clControls.clImageButton();
            this.cmdCrash = new System.Windows.Forms.Button();
            this.gbMaster = new System.Windows.Forms.GroupBox();
            this.txtSetMaster = new System.Windows.Forms.TextBox();
            this.btnSetMaster = new clControls.clImageButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtIRCMaster = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIRCChannel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIRCPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIRCServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbRelayConvos = new System.Windows.Forms.CheckBox();
            this.cbAgentUpdate = new System.Windows.Forms.CheckBox();
            this.cbGreetMaster = new System.Windows.Forms.CheckBox();
            this.cmsAccount = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gbAccounts = new System.Windows.Forms.GroupBox();
            this.accList = new bot.GUI.AccountList();
            this.lblBotsOnline = new System.Windows.Forms.Label();
            this.lblAccoutnsInfo = new System.Windows.Forms.Label();
            this.tsBots = new System.Windows.Forms.ToolStrip();
            this.tsbLoad = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbRemove = new System.Windows.Forms.ToolStripButton();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLogin = new System.Windows.Forms.ToolStripButton();
            this.tsbLoginAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbLogout = new System.Windows.Forms.ToolStripButton();
            this.tsbLogoutAll = new System.Windows.Forms.ToolStripButton();
            this.openAccountList = new System.Windows.Forms.OpenFileDialog();
            this.saveAccountList = new System.Windows.Forms.SaveFileDialog();
            this.ttGreetMaster = new System.Windows.Forms.ToolTip(this.components);
            this.dlgImport = new System.Windows.Forms.OpenFileDialog();
            this.lblUploadStatus = new System.Windows.Forms.Label();
            this.lblMain = new System.Windows.Forms.Label();
            this.gbBotConfiguration = new System.Windows.Forms.GroupBox();
            this.chkHaveLuck = new System.Windows.Forms.CheckBox();
            this.chkTouchMidnightMania = new System.Windows.Forms.CheckBox();
            this.chkInformFriends = new System.Windows.Forms.CheckBox();
            this.chkInventoryOffers = new System.Windows.Forms.CheckBox();
            this.chkSounds = new System.Windows.Forms.CheckBox();
            this.chkTextures = new System.Windows.Forms.CheckBox();
            this.chkAnimations = new System.Windows.Forms.CheckBox();
            this.btnConsole = new clControls.clImageButton();
            this.btnExit = new clControls.clImageButton();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.chkLogChat = new System.Windows.Forms.CheckBox();
            this.cmbLanguage = new System.Windows.Forms.ComboBox();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.chkLogConsole = new System.Windows.Forms.CheckBox();
            this.lblSkin = new System.Windows.Forms.Label();
            this.cmbSkin = new System.Windows.Forms.ComboBox();
            this.btnChat = new clControls.clImageButton();
            this.btnMap = new clControls.clImageButton();
            this.btnFriends = new clControls.clImageButton();
            this.btnGroups = new clControls.clImageButton();
            this.btnAvatars = new clControls.clImageButton();
            this.btnInventory = new clControls.clImageButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnAbout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnObjects2)).BeginInit();
            this.gbMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetMaster)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.cmsAccount.SuspendLayout();
            this.gbAccounts.SuspendLayout();
            this.tsBots.SuspendLayout();
            this.gbBotConfiguration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnConsole)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).BeginInit();
            this.gbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnChat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFriends)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAvatars)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInventory)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAbout
            // 
            this.btnAbout.BackColor = System.Drawing.Color.Transparent;
            this.btnAbout.ButtonBitmap = null;
            this.btnAbout.ButtonState = clControls.ButtonState.Normal;
            this.btnAbout.DisabledBitmap = null;
            this.btnAbout.Image = ((System.Drawing.Image)(resources.GetObject("btnAbout.Image")));
            this.btnAbout.Location = new System.Drawing.Point(745, 380);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.OnMouseClickBitmap = null;
            this.btnAbout.OnMouseOverBitmap = null;
            this.btnAbout.Size = new System.Drawing.Size(75, 23);
            this.btnAbout.TabIndex = 30;
            this.btnAbout.TabStop = false;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // prgProgress
            // 
            this.prgProgress.Location = new System.Drawing.Point(51, 452);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(742, 17);
            this.prgProgress.TabIndex = 28;
            this.prgProgress.Visible = false;
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Transparent;
            this.btnImport.ButtonBitmap = null;
            this.btnImport.ButtonState = clControls.ButtonState.Normal;
            this.btnImport.DisabledBitmap = null;
            this.btnImport.Image = ((System.Drawing.Image)(resources.GetObject("btnImport.Image")));
            this.btnImport.Location = new System.Drawing.Point(421, 381);
            this.btnImport.Name = "btnImport";
            this.btnImport.OnMouseClickBitmap = null;
            this.btnImport.OnMouseOverBitmap = null;
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 27;
            this.btnImport.TabStop = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnObjects2
            // 
            this.btnObjects2.BackColor = System.Drawing.Color.Transparent;
            this.btnObjects2.ButtonBitmap = null;
            this.btnObjects2.ButtonState = clControls.ButtonState.Normal;
            this.btnObjects2.DisabledBitmap = null;
            this.btnObjects2.Image = ((System.Drawing.Image)(resources.GetObject("btnObjects2.Image")));
            this.btnObjects2.Location = new System.Drawing.Point(421, 352);
            this.btnObjects2.Name = "btnObjects2";
            this.btnObjects2.OnMouseClickBitmap = null;
            this.btnObjects2.OnMouseOverBitmap = null;
            this.btnObjects2.Size = new System.Drawing.Size(75, 23);
            this.btnObjects2.TabIndex = 26;
            this.btnObjects2.TabStop = false;
            this.btnObjects2.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdCrash
            // 
            this.cmdCrash.ForeColor = System.Drawing.Color.Black;
            this.cmdCrash.Location = new System.Drawing.Point(621, 65);
            this.cmdCrash.Name = "cmdCrash";
            this.cmdCrash.Size = new System.Drawing.Size(105, 23);
            this.cmdCrash.TabIndex = 25;
            this.cmdCrash.Text = "Crash controls";
            this.cmdCrash.UseVisualStyleBackColor = true;
            this.cmdCrash.Visible = false;
            this.cmdCrash.Click += new System.EventHandler(this.cmdCrash_Click);
            // 
            // gbMaster
            // 
            this.gbMaster.BackColor = System.Drawing.Color.Transparent;
            this.gbMaster.Controls.Add(this.txtSetMaster);
            this.gbMaster.Controls.Add(this.btnSetMaster);
            this.gbMaster.ForeColor = System.Drawing.Color.White;
            this.gbMaster.Location = new System.Drawing.Point(250, 98);
            this.gbMaster.Name = "gbMaster";
            this.gbMaster.Size = new System.Drawing.Size(240, 52);
            this.gbMaster.TabIndex = 24;
            this.gbMaster.TabStop = false;
            // 
            // txtSetMaster
            // 
            this.txtSetMaster.BackColor = System.Drawing.Color.Black;
            this.txtSetMaster.ForeColor = System.Drawing.Color.White;
            this.txtSetMaster.Location = new System.Drawing.Point(6, 19);
            this.txtSetMaster.Name = "txtSetMaster";
            this.txtSetMaster.Size = new System.Drawing.Size(144, 20);
            this.txtSetMaster.TabIndex = 4;
            // 
            // btnSetMaster
            // 
            this.btnSetMaster.BackColor = System.Drawing.Color.Transparent;
            this.btnSetMaster.ButtonBitmap = null;
            this.btnSetMaster.ButtonState = clControls.ButtonState.Normal;
            this.btnSetMaster.DisabledBitmap = null;
            this.btnSetMaster.ForeColor = System.Drawing.Color.Black;
            this.btnSetMaster.Image = ((System.Drawing.Image)(resources.GetObject("btnSetMaster.Image")));
            this.btnSetMaster.Location = new System.Drawing.Point(156, 16);
            this.btnSetMaster.Name = "btnSetMaster";
            this.btnSetMaster.OnMouseClickBitmap = null;
            this.btnSetMaster.OnMouseOverBitmap = null;
            this.btnSetMaster.Size = new System.Drawing.Size(75, 23);
            this.btnSetMaster.TabIndex = 3;
            this.btnSetMaster.TabStop = false;
            this.btnSetMaster.Click += new System.EventHandler(this.btnSetMaster_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txtIRCMaster);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtIRCChannel);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtIRCPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtIRCServer);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(232, 127);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "IRC Settings";
            // 
            // txtIRCMaster
            // 
            this.txtIRCMaster.BackColor = System.Drawing.Color.Black;
            this.txtIRCMaster.ForeColor = System.Drawing.Color.White;
            this.txtIRCMaster.Location = new System.Drawing.Point(96, 91);
            this.txtIRCMaster.Name = "txtIRCMaster";
            this.txtIRCMaster.Size = new System.Drawing.Size(123, 20);
            this.txtIRCMaster.TabIndex = 7;
            this.txtIRCMaster.Text = "NiCK";
            this.txtIRCMaster.TextChanged += new System.EventHandler(this.txtIRCMaster_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Master\'s nick";
            // 
            // txtIRCChannel
            // 
            this.txtIRCChannel.BackColor = System.Drawing.Color.Black;
            this.txtIRCChannel.ForeColor = System.Drawing.Color.White;
            this.txtIRCChannel.Location = new System.Drawing.Point(96, 62);
            this.txtIRCChannel.Name = "txtIRCChannel";
            this.txtIRCChannel.Size = new System.Drawing.Size(123, 20);
            this.txtIRCChannel.TabIndex = 5;
            this.txtIRCChannel.Text = "#bots";
            this.txtIRCChannel.TextChanged += new System.EventHandler(this.txtIRCChannel_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Main channel";
            // 
            // txtIRCPort
            // 
            this.txtIRCPort.BackColor = System.Drawing.Color.Black;
            this.txtIRCPort.ForeColor = System.Drawing.Color.White;
            this.txtIRCPort.Location = new System.Drawing.Point(161, 32);
            this.txtIRCPort.Name = "txtIRCPort";
            this.txtIRCPort.Size = new System.Drawing.Size(58, 20);
            this.txtIRCPort.TabIndex = 3;
            this.txtIRCPort.Text = "6667";
            this.txtIRCPort.TextChanged += new System.EventHandler(this.txtIRCPort_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(163, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port";
            // 
            // txtIRCServer
            // 
            this.txtIRCServer.BackColor = System.Drawing.Color.Black;
            this.txtIRCServer.ForeColor = System.Drawing.Color.White;
            this.txtIRCServer.Location = new System.Drawing.Point(13, 32);
            this.txtIRCServer.Name = "txtIRCServer";
            this.txtIRCServer.Size = new System.Drawing.Size(142, 20);
            this.txtIRCServer.TabIndex = 1;
            this.txtIRCServer.Text = "irc.irc-hispano.org";
            this.txtIRCServer.TextChanged += new System.EventHandler(this.txtIRCServer_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "IRC Server";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cbRelayConvos);
            this.groupBox1.Controls.Add(this.cbAgentUpdate);
            this.groupBox1.Controls.Add(this.cbGreetMaster);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(250, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 82);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "General options";
            // 
            // cbRelayConvos
            // 
            this.cbRelayConvos.AutoSize = true;
            this.cbRelayConvos.Location = new System.Drawing.Point(18, 65);
            this.cbRelayConvos.Name = "cbRelayConvos";
            this.cbRelayConvos.Size = new System.Drawing.Size(131, 17);
            this.cbRelayConvos.TabIndex = 2;
            this.cbRelayConvos.Text = "Send IMs/Chat to IRC";
            this.cbRelayConvos.UseVisualStyleBackColor = true;
            this.cbRelayConvos.CheckedChanged += new System.EventHandler(this.cbRelayConvos_CheckedChanged);
            // 
            // cbAgentUpdate
            // 
            this.cbAgentUpdate.AutoSize = true;
            this.cbAgentUpdate.Checked = true;
            this.cbAgentUpdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAgentUpdate.Location = new System.Drawing.Point(18, 42);
            this.cbAgentUpdate.Name = "cbAgentUpdate";
            this.cbAgentUpdate.Size = new System.Drawing.Size(123, 17);
            this.cbAgentUpdate.TabIndex = 1;
            this.cbAgentUpdate.Text = "Send update packet";
            this.cbAgentUpdate.UseVisualStyleBackColor = true;
            this.cbAgentUpdate.CheckedChanged += new System.EventHandler(this.cbAgentUpdate_CheckedChanged);
            // 
            // cbGreetMaster
            // 
            this.cbGreetMaster.AutoSize = true;
            this.cbGreetMaster.Checked = true;
            this.cbGreetMaster.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbGreetMaster.Location = new System.Drawing.Point(18, 19);
            this.cbGreetMaster.Name = "cbGreetMaster";
            this.cbGreetMaster.Size = new System.Drawing.Size(86, 17);
            this.cbGreetMaster.TabIndex = 0;
            this.cbGreetMaster.Text = "Greet master";
            this.ttGreetMaster.SetToolTip(this.cbGreetMaster, "Greet Master");
            this.cbGreetMaster.UseVisualStyleBackColor = true;
            this.cbGreetMaster.CheckedChanged += new System.EventHandler(this.cbGreetMaster_CheckedChanged);
            // 
            // cmsAccount
            // 
            this.cmsAccount.BackColor = System.Drawing.Color.Black;
            this.cmsAccount.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.editToolStripMenuItem,
                this.loginToolStripMenuItem,
                this.logoutToolStripMenuItem,
                this.objectsToolStripMenuItem,
                this.removeToolStripMenuItem
            });
            this.cmsAccount.Name = "cmsAccount";
            this.cmsAccount.ShowImageMargin = false;
            this.cmsAccount.Size = new System.Drawing.Size(141, 114);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.editToolStripMenuItem.Text = "Show/Edit details";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // objectsToolStripMenuItem
            // 
            this.objectsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
            this.objectsToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.objectsToolStripMenuItem.Text = "Objects";
            this.objectsToolStripMenuItem.Click += new System.EventHandler(this.objectsToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // gbAccounts
            // 
            this.gbAccounts.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gbAccounts.BackColor = System.Drawing.Color.Transparent;
            this.gbAccounts.Controls.Add(this.accList);
            this.gbAccounts.Controls.Add(this.lblBotsOnline);
            this.gbAccounts.Controls.Add(this.lblAccoutnsInfo);
            this.gbAccounts.Controls.Add(this.tsBots);
            this.gbAccounts.ForeColor = System.Drawing.Color.White;
            this.gbAccounts.Location = new System.Drawing.Point(51, 31);
            this.gbAccounts.Name = "gbAccounts";
            this.gbAccounts.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.gbAccounts.Size = new System.Drawing.Size(750, 153);
            this.gbAccounts.TabIndex = 9;
            this.gbAccounts.TabStop = false;
            this.gbAccounts.Text = "Accounts";
            // 
            // accList
            // 
            this.accList.BackColor = System.Drawing.Color.Black;
            this.accList.ContextMenuStrip = this.cmsAccount;
            this.accList.ForeColor = System.Drawing.Color.White;
            this.accList.FullRowSelect = true;
            this.accList.Location = new System.Drawing.Point(11, 44);
            this.accList.MultiSelect = false;
            this.accList.Name = "accList";
            this.accList.Size = new System.Drawing.Size(731, 97);
            this.accList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.accList.TabIndex = 26;
            this.accList.UseCompatibleStateImageBehavior = false;
            this.accList.View = System.Windows.Forms.View.Details;
            this.accList.SelectedIndexChanged += new System.EventHandler(this.accList_SelectedIndexChanged);
            // 
            // lblBotsOnline
            // 
            this.lblBotsOnline.AutoSize = true;
            this.lblBotsOnline.Location = new System.Drawing.Point(67, 0);
            this.lblBotsOnline.Name = "lblBotsOnline";
            this.lblBotsOnline.Size = new System.Drawing.Size(0, 13);
            this.lblBotsOnline.TabIndex = 22;
            // 
            // lblAccoutnsInfo
            // 
            this.lblAccoutnsInfo.AutoSize = true;
            this.lblAccoutnsInfo.Location = new System.Drawing.Point(454, 25);
            this.lblAccoutnsInfo.Name = "lblAccoutnsInfo";
            this.lblAccoutnsInfo.Size = new System.Drawing.Size(153, 13);
            this.lblAccoutnsInfo.TabIndex = 21;
            this.lblAccoutnsInfo.Text = "Right click on a row for options";
            // 
            // tsBots
            // 
            this.tsBots.BackColor = System.Drawing.Color.Transparent;
            this.tsBots.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsBots.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.tsbLoad,
                this.toolStripButton2,
                this.toolStripSeparator1,
                this.tsbAdd,
                this.tsbRemove,
                this.sep2,
                this.tsbLogin,
                this.tsbLoginAll,
                this.toolStripSeparator2,
                this.tsbLogout,
                this.tsbLogoutAll
            });
            this.tsBots.Location = new System.Drawing.Point(8, 16);
            this.tsBots.Name = "tsBots";
            this.tsBots.Size = new System.Drawing.Size(734, 25);
            this.tsBots.TabIndex = 25;
            this.tsBots.Text = "Bot handling";
            // 
            // tsbLoad
            // 
            this.tsbLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbLoad.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoad.Image")));
            this.tsbLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoad.Name = "tsbLoad";
            this.tsbLoad.Size = new System.Drawing.Size(23, 22);
            this.tsbLoad.Text = "Load accounts";
            this.tsbLoad.Click += new System.EventHandler(this.tsbLoad_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "Save accounts";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbAdd.Image")));
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "Add account";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbRemove
            // 
            this.tsbRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbRemove.Image = ((System.Drawing.Image)(resources.GetObject("tsbRemove.Image")));
            this.tsbRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRemove.Name = "tsbRemove";
            this.tsbRemove.Size = new System.Drawing.Size(23, 22);
            this.tsbRemove.Text = "Remove account";
            this.tsbRemove.Click += new System.EventHandler(this.tsbRemove_Click);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLogin
            // 
            this.tsbLogin.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLogin.Image = ((System.Drawing.Image)(resources.GetObject("tsbLogin.Image")));
            this.tsbLogin.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogin.Name = "tsbLogin";
            this.tsbLogin.Size = new System.Drawing.Size(41, 22);
            this.tsbLogin.Text = "Login";
            this.tsbLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tsbLoginAll
            // 
            this.tsbLoginAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLoginAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbLoginAll.Image")));
            this.tsbLoginAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLoginAll.Name = "tsbLoginAll";
            this.tsbLoginAll.Size = new System.Drawing.Size(56, 22);
            this.tsbLoginAll.Text = "Login all";
            this.tsbLoginAll.Click += new System.EventHandler(this.tsbLoginAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbLogout
            // 
            this.tsbLogout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLogout.Image = ((System.Drawing.Image)(resources.GetObject("tsbLogout.Image")));
            this.tsbLogout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogout.Name = "tsbLogout";
            this.tsbLogout.Size = new System.Drawing.Size(49, 22);
            this.tsbLogout.Text = "Logout";
            this.tsbLogout.Click += new System.EventHandler(this.tsbLogout_Click);
            // 
            // tsbLogoutAll
            // 
            this.tsbLogoutAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbLogoutAll.Image = ((System.Drawing.Image)(resources.GetObject("tsbLogoutAll.Image")));
            this.tsbLogoutAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLogoutAll.Name = "tsbLogoutAll";
            this.tsbLogoutAll.Size = new System.Drawing.Size(64, 22);
            this.tsbLogoutAll.Text = "Logout all";
            this.tsbLogoutAll.Click += new System.EventHandler(this.tsbLogoutAll_Click);
            // 
            // openAccountList
            // 
            this.openAccountList.DefaultExt = "acc";
            this.openAccountList.FileName = "accounts.acc";
            this.openAccountList.Filter = "Account files *.acc|*.acc|All files|*.*";
            this.openAccountList.FileOk += new System.ComponentModel.CancelEventHandler(this.openAccountList_FileOk);
            // 
            // saveAccountList
            // 
            this.saveAccountList.DefaultExt = "acc";
            this.saveAccountList.FileName = "accounts.acc";
            this.saveAccountList.Filter = "Account files *.acc|*.acc|All files|*.*";
            this.saveAccountList.FileOk += new System.ComponentModel.CancelEventHandler(this.saveAccountList_FileOk);
            // 
            // ttGreetMaster
            // 
            this.ttGreetMaster.ToolTipTitle = "Send a greet to the master at login or changing it";
            // 
            // dlgImport
            // 
            this.dlgImport.DefaultExt = "xml";
            this.dlgImport.Filter = resources.GetString("dlgImport.Filter");
            this.dlgImport.Multiselect = true;
            this.dlgImport.RestoreDirectory = true;
            this.dlgImport.Title = "Element to import";
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.AutoSize = true;
            this.lblUploadStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblUploadStatus.ForeColor = System.Drawing.Color.White;
            this.lblUploadStatus.Location = new System.Drawing.Point(48, 436);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(81, 13);
            this.lblUploadStatus.TabIndex = 31;
            this.lblUploadStatus.Text = "lblUploadStatus";
            // 
            // lblMain
            // 
            this.lblMain.AutoSize = true;
            this.lblMain.BackColor = System.Drawing.Color.Transparent;
            this.lblMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMain.ForeColor = System.Drawing.Color.White;
            this.lblMain.Location = new System.Drawing.Point(47, 9);
            this.lblMain.Name = "lblMain";
            this.lblMain.Size = new System.Drawing.Size(61, 17);
            this.lblMain.TabIndex = 32;
            this.lblMain.Text = "NatiBot";
            // 
            // gbBotConfiguration
            // 
            this.gbBotConfiguration.BackColor = System.Drawing.Color.Transparent;
            this.gbBotConfiguration.Controls.Add(this.chkHaveLuck);
            this.gbBotConfiguration.Controls.Add(this.chkTouchMidnightMania);
            this.gbBotConfiguration.Controls.Add(this.groupBox1);
            this.gbBotConfiguration.Controls.Add(this.chkInformFriends);
            this.gbBotConfiguration.Controls.Add(this.groupBox2);
            this.gbBotConfiguration.Controls.Add(this.chkInventoryOffers);
            this.gbBotConfiguration.Controls.Add(this.gbMaster);
            this.gbBotConfiguration.Controls.Add(this.chkSounds);
            this.gbBotConfiguration.Controls.Add(this.chkTextures);
            this.gbBotConfiguration.Controls.Add(this.chkAnimations);
            this.gbBotConfiguration.Controls.Add(this.cmdCrash);
            this.gbBotConfiguration.ForeColor = System.Drawing.Color.White;
            this.gbBotConfiguration.Location = new System.Drawing.Point(51, 187);
            this.gbBotConfiguration.Name = "gbBotConfiguration";
            this.gbBotConfiguration.Size = new System.Drawing.Size(750, 154);
            this.gbBotConfiguration.TabIndex = 33;
            this.gbBotConfiguration.TabStop = false;
            this.gbBotConfiguration.Text = "Bot configuration";
            // 
            // chkHaveLuck
            // 
            this.chkHaveLuck.AutoSize = true;
            this.chkHaveLuck.Location = new System.Drawing.Point(625, 42);
            this.chkHaveLuck.Name = "chkHaveLuck";
            this.chkHaveLuck.Size = new System.Drawing.Size(99, 17);
            this.chkHaveLuck.TabIndex = 3;
            this.chkHaveLuck.Text = "Have \"Lucky\"s";
            this.chkHaveLuck.UseVisualStyleBackColor = true;
            this.chkHaveLuck.CheckedChanged += new System.EventHandler(this.chkHaveLuck_CheckedChanged);
            // 
            // chkTouchMidnightMania
            // 
            this.chkTouchMidnightMania.AutoSize = true;
            this.chkTouchMidnightMania.Location = new System.Drawing.Point(625, 19);
            this.chkTouchMidnightMania.Name = "chkTouchMidnightMania";
            this.chkTouchMidnightMania.Size = new System.Drawing.Size(105, 17);
            this.chkTouchMidnightMania.TabIndex = 9;
            this.chkTouchMidnightMania.Text = "Touch Midnights";
            this.chkTouchMidnightMania.UseVisualStyleBackColor = true;
            this.chkTouchMidnightMania.CheckedChanged += new System.EventHandler(this.chkTouchMidnightMania_CheckedChanged);
            // 
            // chkInformFriends
            // 
            this.chkInformFriends.AutoSize = true;
            this.chkInformFriends.Location = new System.Drawing.Point(496, 111);
            this.chkInformFriends.Name = "chkInformFriends";
            this.chkInformFriends.Size = new System.Drawing.Size(132, 17);
            this.chkInformFriends.TabIndex = 8;
            this.chkInformFriends.Text = "Inform of friends online";
            this.chkInformFriends.UseVisualStyleBackColor = true;
            this.chkInformFriends.CheckedChanged += new System.EventHandler(this.chkInformFriends_CheckedChanged);
            // 
            // chkInventoryOffers
            // 
            this.chkInventoryOffers.AutoSize = true;
            this.chkInventoryOffers.Location = new System.Drawing.Point(496, 88);
            this.chkInventoryOffers.Name = "chkInventoryOffers";
            this.chkInventoryOffers.Size = new System.Drawing.Size(135, 17);
            this.chkInventoryOffers.TabIndex = 7;
            this.chkInventoryOffers.Text = "Accept inventory offers";
            this.chkInventoryOffers.UseVisualStyleBackColor = true;
            this.chkInventoryOffers.CheckedChanged += new System.EventHandler(this.chkInventoryOffers_CheckedChanged);
            // 
            // chkSounds
            // 
            this.chkSounds.AutoSize = true;
            this.chkSounds.Location = new System.Drawing.Point(496, 65);
            this.chkSounds.Name = "chkSounds";
            this.chkSounds.Size = new System.Drawing.Size(82, 17);
            this.chkSounds.TabIndex = 4;
            this.chkSounds.Text = "Get Sounds";
            this.chkSounds.UseVisualStyleBackColor = true;
            this.chkSounds.CheckedChanged += new System.EventHandler(this.chkSounds_CheckedChanged);
            // 
            // chkTextures
            // 
            this.chkTextures.AutoSize = true;
            this.chkTextures.Location = new System.Drawing.Point(496, 42);
            this.chkTextures.Name = "chkTextures";
            this.chkTextures.Size = new System.Drawing.Size(87, 17);
            this.chkTextures.TabIndex = 5;
            this.chkTextures.Text = "Get Textures";
            this.chkTextures.UseVisualStyleBackColor = true;
            this.chkTextures.CheckedChanged += new System.EventHandler(this.chkTextures_CheckedChanged);
            // 
            // chkAnimations
            // 
            this.chkAnimations.AutoSize = true;
            this.chkAnimations.Location = new System.Drawing.Point(496, 19);
            this.chkAnimations.Name = "chkAnimations";
            this.chkAnimations.Size = new System.Drawing.Size(97, 17);
            this.chkAnimations.TabIndex = 6;
            this.chkAnimations.Text = "Get Animations";
            this.chkAnimations.UseVisualStyleBackColor = true;
            this.chkAnimations.CheckedChanged += new System.EventHandler(this.chkAnimations_CheckedChanged);
            // 
            // btnConsole
            // 
            this.btnConsole.BackColor = System.Drawing.Color.Transparent;
            this.btnConsole.ButtonBitmap = null;
            this.btnConsole.ButtonState = clControls.ButtonState.Normal;
            this.btnConsole.DisabledBitmap = null;
            this.btnConsole.Image = ((System.Drawing.Image)(resources.GetObject("btnConsole.Image")));
            this.btnConsole.Location = new System.Drawing.Point(745, 352);
            this.btnConsole.Name = "btnConsole";
            this.btnConsole.OnMouseClickBitmap = null;
            this.btnConsole.OnMouseOverBitmap = null;
            this.btnConsole.Size = new System.Drawing.Size(75, 23);
            this.btnConsole.TabIndex = 34;
            this.btnConsole.TabStop = false;
            this.btnConsole.Text = "Console";
            this.btnConsole.Click += new System.EventHandler(this.btnConsole_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.ButtonBitmap = null;
            this.btnExit.ButtonState = clControls.ButtonState.Normal;
            this.btnExit.DisabledBitmap = null;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(775, 9);
            this.btnExit.Name = "btnExit";
            this.btnExit.OnMouseClickBitmap = null;
            this.btnExit.OnMouseOverBitmap = null;
            this.btnExit.Size = new System.Drawing.Size(24, 24);
            this.btnExit.TabIndex = 35;
            this.btnExit.TabStop = false;
            this.btnExit.Text = "X";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // gbSettings
            // 
            this.gbSettings.BackColor = System.Drawing.Color.Transparent;
            this.gbSettings.Controls.Add(this.chkLogChat);
            this.gbSettings.Controls.Add(this.cmbLanguage);
            this.gbSettings.Controls.Add(this.lblLanguage);
            this.gbSettings.Controls.Add(this.chkLogConsole);
            this.gbSettings.Controls.Add(this.cmbSkin);
            this.gbSettings.Controls.Add(this.lblSkin);
            this.gbSettings.ForeColor = System.Drawing.Color.White;
            this.gbSettings.Location = new System.Drawing.Point(51, 347);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(364, 66);
            this.gbSettings.TabIndex = 36;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // chkLogChat
            // 
            this.chkLogChat.AutoSize = true;
            this.chkLogChat.Location = new System.Drawing.Point(6, 44);
            this.chkLogChat.Name = "chkLogChat";
            this.chkLogChat.Size = new System.Drawing.Size(103, 17);
            this.chkLogChat.TabIndex = 3;
            this.chkLogChat.Text = "Write chat to file";
            this.chkLogChat.UseVisualStyleBackColor = true;
            this.chkLogChat.CheckedChanged += new System.EventHandler(this.chkLogChat_CheckedChanged);
            // 
            // cmbLanguage
            // 
            this.cmbLanguage.BackColor = System.Drawing.Color.Black;
            this.cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLanguage.ForeColor = System.Drawing.Color.White;
            this.cmbLanguage.FormattingEnabled = true;
            this.cmbLanguage.Location = new System.Drawing.Point(234, 15);
            this.cmbLanguage.Name = "cmbLanguage";
            this.cmbLanguage.Size = new System.Drawing.Size(121, 21);
            this.cmbLanguage.Sorted = true;
            this.cmbLanguage.TabIndex = 20;
            this.cmbLanguage.SelectionChangeCommitted += new System.EventHandler(this.cmbLanguage_SelectionChangeCommitted);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(170, 18);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(58, 13);
            this.lblLanguage.TabIndex = 21;
            this.lblLanguage.Text = "Language:";
            this.lblLanguage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // chkLogConsole
            // 
            this.chkLogConsole.AutoSize = true;
            this.chkLogConsole.Location = new System.Drawing.Point(6, 17);
            this.chkLogConsole.Name = "chkLogConsole";
            this.chkLogConsole.Size = new System.Drawing.Size(119, 17);
            this.chkLogConsole.TabIndex = 0;
            this.chkLogConsole.Text = "Write console to file";
            this.chkLogConsole.UseVisualStyleBackColor = true;
            this.chkLogConsole.CheckedChanged += new System.EventHandler(this.chkLogConsole_CheckedChanged);
            // 
            // lblSkin
            // 
            this.lblSkin.AutoSize = true;
            this.lblSkin.Location = new System.Drawing.Point(197, 45);
            this.lblSkin.Name = "lblSkin";
            this.lblSkin.Size = new System.Drawing.Size(31, 13);
            this.lblSkin.TabIndex = 1;
            this.lblSkin.Text = "Skin:";
            this.lblSkin.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // cmbSkin
            // 
            this.cmbSkin.BackColor = System.Drawing.Color.Black;
            this.cmbSkin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSkin.ForeColor = System.Drawing.Color.White;
            this.cmbSkin.FormattingEnabled = true;
            this.cmbSkin.Location = new System.Drawing.Point(234, 42);
            this.cmbSkin.Name = "cmbSkin";
            this.cmbSkin.Size = new System.Drawing.Size(121, 21);
            this.cmbSkin.Sorted = true;
            this.cmbSkin.TabIndex = 2;
            this.cmbSkin.SelectionChangeCommitted += new System.EventHandler(this.cmbSkin_SelectionChangeCommitted);
            // 
            // btnChat
            // 
            this.btnChat.BackColor = System.Drawing.Color.Transparent;
            this.btnChat.ButtonBitmap = null;
            this.btnChat.ButtonState = clControls.ButtonState.Normal;
            this.btnChat.DisabledBitmap = null;
            this.btnChat.Image = ((System.Drawing.Image)(resources.GetObject("btnChat.Image")));
            this.btnChat.Location = new System.Drawing.Point(502, 380);
            this.btnChat.Name = "btnChat";
            this.btnChat.OnMouseClickBitmap = null;
            this.btnChat.OnMouseOverBitmap = null;
            this.btnChat.Size = new System.Drawing.Size(75, 23);
            this.btnChat.TabIndex = 37;
            this.btnChat.TabStop = false;
            this.btnChat.Text = "Chat";
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnMap
            // 
            this.btnMap.BackColor = System.Drawing.Color.Transparent;
            this.btnMap.ButtonBitmap = null;
            this.btnMap.ButtonState = clControls.ButtonState.Normal;
            this.btnMap.DisabledBitmap = null;
            this.btnMap.Location = new System.Drawing.Point(664, 380);
            this.btnMap.Name = "btnMap";
            this.btnMap.OnMouseClickBitmap = null;
            this.btnMap.OnMouseOverBitmap = null;
            this.btnMap.Size = new System.Drawing.Size(75, 23);
            this.btnMap.TabIndex = 38;
            this.btnMap.TabStop = false;
            this.btnMap.Text = "Map";
            this.btnMap.Click += new System.EventHandler(this.btnMap_Click);
            // 
            // btnFriends
            // 
            this.btnFriends.BackColor = System.Drawing.Color.Transparent;
            this.btnFriends.ButtonBitmap = null;
            this.btnFriends.ButtonState = clControls.ButtonState.Normal;
            this.btnFriends.DisabledBitmap = null;
            this.btnFriends.Location = new System.Drawing.Point(583, 380);
            this.btnFriends.Name = "btnFriends";
            this.btnFriends.OnMouseClickBitmap = null;
            this.btnFriends.OnMouseOverBitmap = null;
            this.btnFriends.Size = new System.Drawing.Size(75, 23);
            this.btnFriends.TabIndex = 39;
            this.btnFriends.TabStop = false;
            this.btnFriends.Text = "Friends";
            this.btnFriends.Click += new System.EventHandler(this.btnFriends_Click);
            // 
            // btnGroups
            // 
            this.btnGroups.BackColor = System.Drawing.Color.Transparent;
            this.btnGroups.ButtonBitmap = null;
            this.btnGroups.ButtonState = clControls.ButtonState.Normal;
            this.btnGroups.DisabledBitmap = null;
            this.btnGroups.Location = new System.Drawing.Point(583, 352);
            this.btnGroups.Name = "btnGroups";
            this.btnGroups.OnMouseClickBitmap = null;
            this.btnGroups.OnMouseOverBitmap = null;
            this.btnGroups.Size = new System.Drawing.Size(75, 23);
            this.btnGroups.TabIndex = 40;
            this.btnGroups.TabStop = false;
            this.btnGroups.Text = "Groups";
            this.btnGroups.Click += new System.EventHandler(this.btnGroups_Click);
            // 
            // btnAvatars
            // 
            this.btnAvatars.BackColor = System.Drawing.Color.Transparent;
            this.btnAvatars.ButtonBitmap = null;
            this.btnAvatars.ButtonState = clControls.ButtonState.Normal;
            this.btnAvatars.DisabledBitmap = null;
            this.btnAvatars.Location = new System.Drawing.Point(502, 352);
            this.btnAvatars.Name = "btnAvatars";
            this.btnAvatars.OnMouseClickBitmap = null;
            this.btnAvatars.OnMouseOverBitmap = null;
            this.btnAvatars.Size = new System.Drawing.Size(75, 23);
            this.btnAvatars.TabIndex = 41;
            this.btnAvatars.TabStop = false;
            this.btnAvatars.Text = "Avatars";
            this.btnAvatars.Click += new System.EventHandler(this.btnAvatars_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.Transparent;
            this.btnInventory.ButtonBitmap = null;
            this.btnInventory.ButtonState = clControls.ButtonState.Normal;
            this.btnInventory.DisabledBitmap = null;
            this.btnInventory.Location = new System.Drawing.Point(664, 352);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.OnMouseClickBitmap = null;
            this.btnInventory.OnMouseOverBitmap = null;
            this.btnInventory.Size = new System.Drawing.Size(75, 23);
            this.btnInventory.TabIndex = 42;
            this.btnInventory.TabStop = false;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.Click += new System.EventHandler(this.btnInventory_Click);
            // 
            // frmMain
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(841, 481);
            this.ControlBox = false;
            this.Controls.Add(this.btnInventory);
            this.Controls.Add(this.gbBotConfiguration);
            this.Controls.Add(this.gbAccounts);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnFriends);
            this.Controls.Add(this.btnAvatars);
            this.Controls.Add(this.btnMap);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnGroups);
            this.Controls.Add(this.lblMain);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.lblUploadStatus);
            this.Controls.Add(this.prgProgress);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnObjects2);
            this.Controls.Add(this.btnConsole);
            this.Controls.Add(this.btnAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "Nati-Bot";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btnAbout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnImport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnObjects2)).EndInit();
            this.gbMaster.ResumeLayout(false);
            this.gbMaster.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSetMaster)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.cmsAccount.ResumeLayout(false);
            this.gbAccounts.ResumeLayout(false);
            this.gbAccounts.PerformLayout();
            this.tsBots.ResumeLayout(false);
            this.tsBots.PerformLayout();
            this.gbBotConfiguration.ResumeLayout(false);
            this.gbBotConfiguration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnConsole)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExit)).EndInit();
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnChat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnFriends)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAvatars)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInventory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void DisableControls(bool what)
        {
            this.cbAgentUpdate.Enabled = !what;
            this.cbGreetMaster.Enabled = !what;
            this.cbRelayConvos.Enabled = !what;
            this.txtIRCChannel.Enabled = !what;
            this.txtIRCMaster.Enabled = !what;
            this.txtIRCPort.Enabled = !what;
            this.txtIRCServer.Enabled = !what;
            this.txtSetMaster.Enabled = !what;

            this.tsbRemove.Enabled = !what;
            this.btnSetMaster.Enabled = !what;
            //if(!what) RegisterAllPlugins(Assembly.GetExecutingAssembly());
            //EnableInterfaces(!what);
        }

        private void DisableLoginControls(bool what)
        {
            this.tsbLogin.Enabled = !what;
            this.tsbLoginAll.Enabled = !what;
            this.tsbLogout.Enabled = !what;
            this.tsbLogoutAll.Enabled = !what;
        }

        private void LoadXmlAccounts()
        {
            this.LoadXmlAccounts("./config/accounts.xml");
        }

        private void LoadXmlAccounts(string file)
        {
            LoginDetailList list = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LoginDetailList));
                TextReader textReader = new StreamReader(file);
                LoginDetailList list2 = (LoginDetailList)serializer.Deserialize(textReader);
                if (list2 != null)
                {
                    list = list2;
                }
                textReader.Close();
            }
            catch (Exception exception)
            {
                //MessageBox.Show(exception.Message);
            }
            if (list != null)
            {
                foreach (LoginDetails details in list.Items)
                {
                    this.frmAddAcc_OnAddAccount(new BotAccount(details));
                }
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tsbLogin.PerformClick();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tsbLogout.PerformClick();
        }

        private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.button1_Click(sender, e);
        }

        private void openAccountList_FileOk(object sender, CancelEventArgs e)
        {
            //ACCLIST
            while (this.accList.Items.Count > 0)
            {
                ListViewItem item = this.accList.Items[0];
                this.accList.Items.Remove(item);
            }
            this.LoadXmlAccounts(this.openAccountList.FileName);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.tsbRemove.PerformClick();
        }

        private void saveAccountList_FileOk(object sender, CancelEventArgs e)
        {
            this.SaveXmlAccounts(this.saveAccountList.FileName);
        }

        private void SaveXmlAccounts()
        {
            this.SaveXmlAccounts("./config/accounts.xml");
        }

        private void SaveXmlAccounts(string file)
        {
            bool flag = false;
            if (!Directory.Exists("./config/"))
                Directory.CreateDirectory("./config/");

            LoginDetailList o = new LoginDetailList();
            //ACCLIST
            
            foreach (ListViewItem item in this.accList.Items)
            {
                if (item.Tag != null)
                {
                    flag = true;
                    BotAccount _bAcc = (BotAccount)item.Tag;
                    o.Add(_bAcc.LoginDetails);
                }
            }
            if (flag)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(LoginDetailList));
                    TextWriter textWriter = new StreamWriter(file);
                    serializer.Serialize(textWriter, o);
                    textWriter.Close();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void updateGUI(BotAccount acc)
        {
            /*this.cbAgentUpdate.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.cbGreetMaster.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.cbRelayConvos.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));

            this.txtIRCServer.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.txtIRCPort.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.txtIRCChannel.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.txtIRCMaster.Enabled = acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));

            //this.btnObjects2.Enabled = !acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));
            this.cmdCrash.Enabled = !acc.GridViewRow.Status.Equals(bot.Localization.clResourceManager.getText("botAccount.Online"));*/

            this.chkAnimations.Checked = acc.LoginDetails.BotConfig.GetAnimations;
            this.chkHaveLuck.Checked = acc.LoginDetails.BotConfig.HaveLuck;
            this.chkInformFriends.Checked = acc.LoginDetails.BotConfig.InformFriends;
            this.chkInventoryOffers.Checked = acc.LoginDetails.BotConfig.AcceptInventoryOffers;
            this.chkSounds.Checked = acc.LoginDetails.BotConfig.GetSounds;
            this.chkTextures.Checked = acc.LoginDetails.BotConfig.GetTextures;
            this.chkTouchMidnightMania.Checked = acc.LoginDetails.BotConfig.TouchMidnightMania;

            this.cbAgentUpdate.Checked = acc.LoginDetails.SendAgentUpdatePacket;
            this.cbGreetMaster.Checked = acc.LoginDetails.GreetMaster;
            this.cbRelayConvos.Checked = acc.LoginDetails.RelayChatToIRC;
            this.txtIRCChannel.Text = acc.LoginDetails.IRC_Settings.MainChannel;
            this.txtIRCPort.Text = string.Concat(acc.LoginDetails.IRC_Settings.ServerPort);
            this.txtIRCServer.Text = acc.LoginDetails.IRC_Settings.ServerHost;
            this.txtIRCMaster.Text = acc.LoginDetails.IRC_Settings.Master;
        }

        public BotAccount SelectedAccount
        {
            get
            {
                //ACCLIST
                if (this.accList.SelectedItems.Count == 1)
                {
                    return (BotAccount)this.accList.SelectedItems[0].Tag;
                }
                return null;
            }
        }

        private void cbGreetMaster_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.GreetMaster = cbGreetMaster.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void cbAgentUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.SendAgentUpdatePacket = cbAgentUpdate.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void cbRelayConvos_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.RelayChatToIRC = cbRelayConvos.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void txtIRCMaster_TextChanged(object sender, EventArgs e)
        {
            this.SaveXmlAccounts();
        }

        private void txtIRCChannel_TextChanged(object sender, EventArgs e)
        {
            this.SaveXmlAccounts();
        }

        private void txtIRCPort_TextChanged(object sender, EventArgs e)
        {
            this.SaveXmlAccounts();
        }

        private void txtIRCServer_TextChanged(object sender, EventArgs e)
        {
            this.SaveXmlAccounts();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.saveAccountList.ShowDialog();
        }

        private void tsbLoad_Click(object sender, EventArgs e)
        {
            this.openAccountList.ShowDialog();
            //ACCLIST
            this.DisableLoginControls(this.accList.Items.Count == 0);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            frmAddAccount account = new frmAddAccount();
            account.OnAddAccount += new frmAddAccount.AddAccountCallback(this.frmAddAcc_OnAddAccount);
            account.Show();
        }

        private void tsbRemove_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.Delete();
            }
            this.AutoSave();
            //ACCLIST
            this.DisableLoginControls(this.accList.Items.Count == 0);
        }

        private void tsbLogin_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.OnDialogScriptReceived -= botAccount_OnDialogScriptReceived;
                this.SelectedAccount.Connect();
            }
        }

        private void tsbLoginAll_Click(object sender, EventArgs e)
        {
            //ACCLIST
            foreach (ListViewItem item in this.accList.Items)
            {
                BotAccount _bAcc = (BotAccount)item.Tag;
                _bAcc.Connect();
            }
        }

        private void tsbLogout_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.Disconnect(false);
            }
        }

        private void tsbLogoutAll_Click(object sender, EventArgs e)
        {
            //ACCLIST
            
            foreach (ListViewItem item in this.accList.Items)
            {
                BotAccount _bAcc = (BotAccount)item.Tag;
                _bAcc.Disconnect(false);
            }
        }

        // Tabs disabled in new interface
        /*
        private void EnableInterfaces(bool enable)
        {
            tabControl.TabPages.Clear();
            tabControl.TabPages.Add(tabTools);
            tabControl.TabPages.Add(tabConsole);
            tabControl.TabPages.Add(tabBotSettings);

            if (enable)
            {
                lock (Interfaces)
                {
                    foreach (TabPage page in Interfaces.Values)
                    {
                        tabControl.TabPages.Add(page);
                    }
                }
            }
        }

        private void RegisterAllPlugins(Assembly assembly)
        {
            Interfaces.Clear();
            foreach (Type t in assembly.GetTypes())
            {
                try
                {
                    if (t.IsSubclassOf(typeof(Interface)))
                    {
                        ConstructorInfo[] infos = t.GetConstructors();
                        Interface iface = (Interface)infos[0].Invoke(new object[] { this });
                        if(iface==null)
                            MessageBox.Show("iface == null.  Yell at N3X15 to fix his shit.");
                        RegisterPlugin(iface);
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString()+"\nFUCK SALT");
                }
            }
        }

        private void RegisterPlugin(Interface iface)
        {
            if (this.SelectedAccount==null)
                return;
            TabPage page = new TabPage();
            tabControl.TabPages.Add(page);

            try
            {
            iface.Client = this.SelectedAccount.Client;
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
            iface.TabPage = page;

            if (!Interfaces.ContainsKey(iface))
            {
                lock (Interfaces) Interfaces.Add(iface, page);
            }

            iface.Initialize();

            page.Text = iface.Name;
            page.ToolTipText = iface.Description;
        }*/

        private void cmdCrash_Click(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            if (this.SelectedAccount.ListItem.SubItems[1].Text.Equals(bot.Localization.clResourceManager.getText("botAccount.Online")))
                new frmCrasher(this.SelectedAccount.Client).Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs ev)
        {
            this.tsbLogoutAll_Click(sender, ev);
            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("bot.FormClosing"));
            Environment.Exit(0);
            //Application.Exit();
            /*try
            {
                webserver.Die();
            }
            catch (Exception e) { }*/
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmObjects(this.SelectedAccount.Client).Show();
            }
        }

        private void StartImport(int maxValue)
        {
            prgProgress.Visible = true;
            prgProgress.Minimum = 0;
            prgProgress.Value = 0;
            prgProgress.Maximum = maxValue;
            lblUploadStatus.Visible = true;
            lblUploadStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmMain.btnImport.Proceeding"), dlgImport.FileNames.Length.ToString());
            btnImport.Enabled = false;
        }

        private void SetProgressValue(int value, int maxvalue)
        {
            prgProgress.Value = value;
            lblUploadStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmMain.btnImport.Uploaded"), value.ToString(), maxvalue.ToString());
        }

        private void StopImport()
        {
            prgProgress.Visible = false;
            lblUploadStatus.Visible = false;
            btnImport.Enabled = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                DialogResult result;

                result = dlgImport.ShowDialog();

                if (result == DialogResult.OK || result == DialogResult.Yes)
                {
                    MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmMain.btnImport.Proceeding"), dlgImport.FileNames.Length.ToString()));

                    SecondLifeBot _client = this.SelectedAccount.Client;

                    Program.NBStats.AddStatData(String.Format("{0}: {1} importing {2} elements.", DateTime.Now.ToString(), _client.ToString(), dlgImport.FileNames.Length)); // STATISTICS

                    Thread thImport = new Thread(delegate()
                    {
                        StartImportCallback s = new StartImportCallback(StartImport);
                        SetProgressValueCallback p = new SetProgressValueCallback(SetProgressValue);
                        StopImportCallback f = new StopImportCallback(StopImport);
                        int counter;

                        counter = 0;

                        this.Invoke(s, dlgImport.FileNames.Length);

                        foreach (string filename in dlgImport.FileNames)
                        {
                            counter++;
                            
                            bot.Console.WriteLine(String.Format(bot.Localization.clResourceManager.getText("frmMain.btnImport.Importing"), filename));

                            string cmd = "";

                            if (filename.EndsWith(".xml"))
                            {
                                cmd = "import " +
                                " \"" + filename.Replace("\\", "/") + "\"";
                            }
                            else
                            {
                                cmd = "upload " +
                                " \"" + filename.Replace("\\", "/") + "\"" +
                                " " +
                                " \"" + filename.Replace("\\", "/") + "\"";
                            }

                            _client.DoCommand(cmd, UUID.Zero, true);

                            this.Invoke(p, counter, dlgImport.FileNames.Length);
                            System.Threading.Thread.Sleep(500);
                        }

                        this.Invoke(f);
                    });
                    thImport.IsBackground = true;
                    thImport.Name = "Import all";
                    thImport.Start();
                }
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            new bot.GUI.frmAbout().Show();
        }

        private void btnConsole_Click(object sender, EventArgs e)
        {
            if (console == null)
            {
                console = new frmConsole();
                console.OnOutputSend += new frmConsole.OutputSendCallback(this.Console_OutPutSend);
            }
            if (console != null)
            {
                console.Show();
                console.Focus();
            }
        }

        private void Console_OutPutSend(string message)
        {
            if (this.SelectedAccount.Client != null)
            {
                this.SelectedAccount.Client.DoCommand(message, UUID.Zero, true);
            }
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            this.tsbLogoutAll_Click(sender, e);
            Program.NBStats.AddStatData(String.Format("{0}: Natibot exiting.", DateTime.Now.ToString())); // STATISTICS
            Program.NBStats.SendStatistics();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.tsbLogoutAll_Click(sender, e);
            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("bot.ButtonClosing"));
            Program.NBStats.AddStatData(String.Format("{0}: Natibot exiting.", DateTime.Now.ToString())); // STATISTICS
            Program.NBStats.SendStatistics();
            //Application.Exit();
            Environment.Exit(0);
        }

        private void cmbLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.Localization.clResourceManager.setCurrentLanguage(cmbLanguage.SelectedItem.ToString());
            Program.NBStats.AddStatData(String.Format("{0}: Changed language to {1}.", DateTime.Now.ToString(), cmbLanguage.SelectedItem.ToString())); // STATISTICS
        }

        private void cmbSkin_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bot.Localization.clResourceManager.setSkin(cmbSkin.SelectedItem.ToString());
            Program.NBStats.AddStatData(String.Format("{0}: Skin changed to {1}.", DateTime.Now.ToString(), cmbSkin.SelectedItem.ToString())); // STATISTICS
        }

        private void chkLogConsole_CheckedChanged(object sender, EventArgs e)
        {
            Program.setWriteConsoleToFileSetting(chkLogConsole.Checked);
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            if (chat == null)
            {
                chat = new frmChat();
            }
            if (chat != null)
            {
                chat.Show();
                chat.Focus();
            }
        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmMap(this.SelectedAccount.Client).Show();
            }
        }

        private void btnFriends_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmFriends(this.SelectedAccount.Client).Show();
            }
        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmGroups(this.SelectedAccount.Client).Show();
            }
        }

        private void btnAvatars_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmAvatars(this.SelectedAccount.Client).Show();
            }
        }

        private void chkLogChat_CheckedChanged(object sender, EventArgs e)
        {
            Program.setWriteChatToFileSetting(chkLogChat.Checked);
        }

        private void chkAnimations_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.GetAnimations = this.chkAnimations.Checked;
                this.SaveXmlAccounts();
                Program.NBStats.AddStatData(String.Format("{0}: {1} animations {2}", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName, this.chkAnimations.Checked)); // STATISTICS
            }
        }

        private void chkTextures_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.GetTextures = this.chkTextures.Checked;
                this.SaveXmlAccounts();
                Program.NBStats.AddStatData(String.Format("{0}: {1} textures {2}", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName, this.chkTextures.Checked)); // STATISTICS
            }
        }

        private void chkSounds_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.GetSounds = this.chkSounds.Checked;
                this.SaveXmlAccounts();
                Program.NBStats.AddStatData(String.Format("{0}: {1} sounds {2}", DateTime.Now.ToString(), this.SelectedAccount.LoginDetails.FullName, this.chkSounds.Checked)); // STATISTICS
            }
        }

        private void chkInventoryOffers_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.AcceptInventoryOffers = this.chkInventoryOffers.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void chkInformFriends_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.InformFriends = this.chkInformFriends.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void chkTouchMidnightMania_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.TouchMidnightMania = this.chkTouchMidnightMania.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void chkHaveLuck_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.SelectedAccount.LoginDetails.BotConfig.HaveLuck = this.chkHaveLuck.Checked;
                this.SaveXmlAccounts();
            }
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if ((this.SelectedAccount != null) && (this.SelectedAccount.Client != null))
            {
                new bot.GUI.frmInventory(this.SelectedAccount.Client).Show();
            }
        }

        private void accList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedAccount != null)
            {
                this.updateGUI(this.SelectedAccount);
            }

            this.DisableControls(this.SelectedAccount == null);
        }
    }
}

