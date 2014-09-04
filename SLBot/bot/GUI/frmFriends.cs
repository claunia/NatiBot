/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmFriends.cs
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
using System.Collections;

namespace bot.GUI
{
    public partial class frmFriends : Form
    {
        private SecondLifeBot client;

        private ListColumnSorterNormal _ColumnSorter = new ListColumnSorterNormal();

        private frmProfile _frmProfile;
        private Point mouse_offset;

        public frmFriends(SecondLifeBot client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.InitializeComponent();
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmFriends");
            this.client = client;
            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmFriends.Text"), client.ToString());
            this.btnMessage.Text = bot.Localization.clResourceManager.getText("frmFriends.btnMessage");
            this.btnProfile.Text = bot.Localization.clResourceManager.getText("frmFriends.btnProfile");
            this.btnRemove.Text = bot.Localization.clResourceManager.getText("frmFriends.btnRemove");
            this.btnTeleport.Text = bot.Localization.clResourceManager.getText("frmFriends.btnTeleport");
            this.btnProfile.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnProfile.idle");
            this.btnProfile.Image = bot.Localization.clResourceManager.getButton("frmFriends.btnProfile.idle");
            this.btnProfile.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnProfile.onclick");
            this.btnProfile.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnProfile.onhover");
            this.btnMessage.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnMessage.idle");
            this.btnMessage.Image = bot.Localization.clResourceManager.getButton("frmFriends.btnMessage.idle");
            this.btnMessage.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnMessage.onclick");
            this.btnMessage.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnMessage.onhover");
            this.btnTeleport.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.idle");
            this.btnTeleport.Image = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.idle");
            this.btnTeleport.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.onclick");
            this.btnTeleport.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.onhover");
            this.btnRemove.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnRemove.idle");
            this.btnRemove.Image = bot.Localization.clResourceManager.getButton("frmFriends.btnRemove.idle");
            this.btnRemove.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnRemove.onclick");
            this.btnRemove.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnRemove.onhover");

            ColumnHeader header1 = lstFriends.Columns.Add(bot.Localization.clResourceManager.getText("frmFriends.NameColumn"));
            header1.Width = lstFriends.Width - 20;

            ColumnHeader header2 = lstFriends.Columns.Add(" ");
            header2.Width = 40;

            _ColumnSorter.SortColumn = 1;
            _ColumnSorter.Ascending = false;

            this.DoubleBuffered = true;
            lstFriends.ListViewItemSorter = _ColumnSorter;
            lstFriends.View = View.Details;

            lstFriends.ColumnClick += new ColumnClickEventHandler(FriendList_ColumnClick);
            lstFriends.DoubleClick += new System.EventHandler(FriendList_DoubleClick);
            client.Network.Disconnected += new EventHandler<DisconnectedEventArgs>(Network_Disconnected);
        }

        void Network_Disconnected(object sender, DisconnectedEventArgs e)
        {
            this.Hide();
        }

        private void frmFriends_Load(object sender, EventArgs e)
        {
            client.Friends.FriendNames += Friends_FriendNames;
            client.Friends.FriendOffline += Friends_FriendUpdate;
            client.Friends.FriendOnline += Friends_FriendUpdate;
            RefreshFriends();
        }

        void Friends_FriendNames(object sender, FriendNamesEventArgs e)
        {
            RefreshFriends();
        }

        void Friends_FriendUpdate(object sender, FriendInfoEventArgs e)
        {
            RefreshFriends();
        }

        private void RefreshFriends()
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    RefreshFriends();
                });
            else
            {
                client.Friends.FriendList.ForEach(delegate(FriendInfo friend)
                {
                    string key = friend.UUID.ToString();
                    string onlineText;
                    string name = friend.Name == null ? bot.Localization.clResourceManager.getText("Controls.Loading") : friend.Name;
                    int image;
                    Color color;

                    if (friend.IsOnline)
                    {
                        image = 1;
                        onlineText = "*";
                        color = Color.FromKnownColor(KnownColor.White);
                    }
                    else
                    {
                        image = 0;
                        onlineText = " ";
                        color = Color.FromKnownColor(KnownColor.GrayText);
                    }

                    if (!lstFriends.Items.ContainsKey(key))
                    {
                        lstFriends.Items.Add(key, name, image);
                        lstFriends.Items[key].SubItems.Add(onlineText);
                    }
                    else
                    {
                        if (lstFriends.Items[key].Text == string.Empty || friend.Name != null)
                            lstFriends.Items[key].Text = name;

                        lstFriends.Items[key].SubItems[1].Text = onlineText;
                    }

                    lstFriends.Items[key].ForeColor = color;
                    lstFriends.Items[key].ImageIndex = image;
                    lstFriends.Items[key].Tag = friend;
                });
            }
        }

        private void frmFriends_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Friends.FriendNames -= Friends_FriendNames;
            client.Friends.FriendOffline -= Friends_FriendUpdate;
            client.Friends.FriendOnline -= Friends_FriendUpdate;
        }

        private void FriendList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _ColumnSorter.SortColumn = e.Column;
            if ((_ColumnSorter.Ascending = (lstFriends.Sorting == SortOrder.Ascending)))
                lstFriends.Sorting = SortOrder.Descending;
            else
                lstFriends.Sorting = SortOrder.Ascending;
            lstFriends.ListViewItemSorter = _ColumnSorter;
        }

        private void FriendList_DoubleClick(object sender, System.EventArgs e)
        {
            ListView list = (ListView)sender;
            if (list.SelectedItems.Count > 0 && list.SelectedItems[0].Tag is FriendInfo)
            {
                FriendInfo friend = (FriendInfo)list.SelectedItems[0].Tag;
                btnProfile_Click(sender, e);
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (lstFriends.SelectedItems.Count > 0 && lstFriends.SelectedItems[0].Tag is FriendInfo)
            {
                FriendInfo friend = (FriendInfo)lstFriends.SelectedItems[0].Tag;
                _frmProfile = new frmProfile(client, friend.UUID);
                _frmProfile.RefreshInfo();
                _frmProfile.Show();
            }
        }

        private void btnMessage_Click(object sender, EventArgs e)
        {
            if (lstFriends.SelectedItems.Count > 0 && lstFriends.SelectedItems[0].Tag is FriendInfo)
            {
                FriendInfo friend = (FriendInfo)lstFriends.SelectedItems[0].Tag;
                frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("frmFriends.btnMessage.DialogTitle"),
                                           String.Format(bot.Localization.clResourceManager.getText("frmFriends.btnMessage.DialogLabel"), friend.Name),
                                           "");

                DialogResult _result = _frmDialog.ShowDialog();
                if (_result == DialogResult.OK)
                {
                    this.client.Self.InstantMessage(friend.UUID, _frmDialog._output);

                    bot.Chat.structInstantMessage sim;
                    InstantMessage im = new InstantMessage();

                    im.Message = _frmDialog._output;
                    im.FromAgentID = friend.UUID;
                    im.FromAgentName = friend.Name;
                    im.Dialog = InstantMessageDialog.MessageFromAgent;

                    sim.client = this.client;
                    sim.isReceived = false;
                    sim.message = im;
                    sim.simulator = this.client.Network.CurrentSim;
                    sim.timestamp = DateTime.Now;

                    bot.Chat.receivedIM(sim);
                }
            }
        }

        private void btnTeleport_Click(object sender, EventArgs e)
        {
            if (lstFriends.SelectedItems.Count > 0 && lstFriends.SelectedItems[0].Tag is FriendInfo)
            {
                FriendInfo friend = (FriendInfo)lstFriends.SelectedItems[0].Tag;
                frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("frmFriends.btnTeleport.DialogTitle"),
                                           String.Format(bot.Localization.clResourceManager.getText("frmFriends.btnTeleport.DialogLabel"), friend.Name),
                                           bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Message"));

                DialogResult _result = _frmDialog.ShowDialog();
                if (_result == DialogResult.OK)
                {
                    this.client.Self.SendTeleportLure(friend.UUID, _frmDialog._output);
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstFriends.SelectedItems.Count > 0 && lstFriends.SelectedItems[0].Tag is FriendInfo)
            {
                FriendInfo friend = (FriendInfo)lstFriends.SelectedItems[0].Tag;
                DialogResult _result = MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("frmFriends.btnRemove.DialogLabel"), friend.Name),
                                           bot.Localization.clResourceManager.getText("frmFriends.btnRemove.DialogTitle"), MessageBoxButtons.YesNo);
                
                if (_result == DialogResult.Yes)
                {
                    this.client.Friends.TerminateFriendship(friend.UUID);
                    RefreshFriends();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmFriends_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmFriends_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
