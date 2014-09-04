/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmGroups.cs
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenMetaverse;
using System.Threading;

namespace bot.GUI
{
    public partial class frmGroups : Form
    {
        private SecondLifeBot _client;
        ManualResetEvent JoinGroupChatEvent = new ManualResetEvent(false);
        private Point mouse_offset;

        public frmGroups(SecondLifeBot Client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();
            _client = Client;
            lstGroups.Client = _client;
            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmGroups.Text"), _client.ToString());
            btnLeave.Text = bot.Localization.clResourceManager.getText("frmGroups.btnLeave");
            btnActivate.Text = bot.Localization.clResourceManager.getText("frmGroups.btnActivate");
            btnMessage.Text = bot.Localization.clResourceManager.getText("frmGroups.btnMessage");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmGroups");
            this.btnLeave.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnLeave.idle");
            this.btnLeave.Image = bot.Localization.clResourceManager.getButton("frmGroup.btnLeave.idle");
            this.btnLeave.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnLeave.onclick");
            this.btnLeave.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnLeave.onhover");
            this.btnActivate.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnActivate.idle");
            this.btnActivate.Image = bot.Localization.clResourceManager.getButton("frmGroup.btnActivate.idle");
            this.btnActivate.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnActivate.onclick");
            this.btnActivate.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnActivate.onhover");
            this.btnMessage.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnMessage.idle");
            this.btnMessage.Image = bot.Localization.clResourceManager.getButton("frmGroup.btnMessage.idle");
            this.btnMessage.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnMessage.onclick");
            this.btnMessage.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmGroup.btnMessage.onhover");
        }

        private void btnLeave_Click(object sender, EventArgs e)
        {
            if (lstGroups.SelectedItems.Count > 0 && lstGroups.SelectedItems[0].Tag is Group)
            {
                Group group = (Group)lstGroups.SelectedItems[0].Tag;
                DialogResult _result = MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmGroups.btnLeave.DialogLabel"), group.Name),
                                           bot.Localization.clResourceManager.getText("frmGroups.btnLeave.DialogTitle"), MessageBoxButtons.YesNo);

                if (_result == DialogResult.Yes)
                {
                    this._client.Groups.LeaveGroup(group.ID);
                }
            }
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (lstGroups.SelectedItems.Count > 0 && lstGroups.SelectedItems[0].Tag is Group)
            {
                Group group = (Group)lstGroups.SelectedItems[0].Tag;
                MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmGroups.btnActivate.DialogLabel"), group.Name),
                    bot.Localization.clResourceManager.getText("frmGroups.btnActivate.DialogTitle"), MessageBoxButtons.OK);

                this._client.Groups.ActivateGroup(group.ID);
            }
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (lstGroups.SelectedItems.Count > 0 && lstGroups.SelectedItems[0].Tag is Group)
            {
                Group group = (Group)lstGroups.SelectedItems[0].Tag;
                frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("frmGroups.btnMessage.DialogTitle"),
                                           String.Format(bot.Localization.clResourceManager.getText("frmGroups.btnMessage.DialogLabel"), group.Name),
                                           "");

                DialogResult _result = _frmDialog.ShowDialog();
                if (_result == DialogResult.OK)
                {
                    if (this._client.Self.GroupChatSessions.ContainsKey(group.ID))
                    {
                        this._client.Self.InstantMessageGroup(group.ID, _frmDialog._output);
                    }
                    else
                    {
                        JoinGroupChatEvent = new ManualResetEvent(false);
                        this._client.Self.GroupChatJoined += this.OnGroupChatJoin;
                        this._client.Self.RequestJoinGroupChat(group.ID);
                        if (JoinGroupChatEvent.WaitOne(15000, false))
                        {
                            this._client.Self.InstantMessageGroup(group.ID, _frmDialog._output);
                            MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmGroups.btnMessage.SentOkLabel"), group.Name),
                                bot.Localization.clResourceManager.getText("frmGroups.btnMessage.SentOkTitle"), MessageBoxButtons.OK);
                        }
                        else
                        {
                            MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmGroups.btnMessage.NotSentLabel"), group.Name),
                                bot.Localization.clResourceManager.getText("frmGroups.btnMessage.NotSentTitle"), MessageBoxButtons.OK);
                        }
                                
                        this._client.Self.GroupChatJoined -= this.OnGroupChatJoin;
                    }
                }
            }
        }

        // I do not see whay this should do nothing here.
        void OnGroupChatJoin(object sender, GroupChatJoinedEventArgs e)
        {
            JoinGroupChatEvent.Set();
            return;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmGroups_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmGroups_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
