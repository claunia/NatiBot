/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AvatarList.cs
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
Copyright (C) 2007-2009 openmetaverse.org
****************************************************************************/


// Copied from OpenMetaverse.GUI
/*
 * Copyright (c) 2007-2009, openmetaverse.org
 * All rights reserved.
 *
 * - Redistribution and use in source and binary forms, with or without 
 *   modification, are permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this
 *   list of conditions and the following disclaimer.
 * - Neither the name of the openmetaverse.org nor the names 
 *   of its contributors may be used to endorse or promote products derived from
 *   this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */

using OpenMetaverse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bot.GUI
{

    /// <summary>
    /// ListView GUI component for viewing a client's nearby avatars list
    /// </summary>
    public class AvatarList : ListView
    {
        private SecondLifeBot _Client;
        private ListColumnSorter _ColumnSorter = new ListColumnSorter();
        private TrackedAvatar _SelectedAvatar;

        private frmProfile _frmProfile;

        private DoubleDictionary<uint, UUID, TrackedAvatar> _TrackedAvatars = new DoubleDictionary<uint, UUID, TrackedAvatar>();
        private Dictionary<UUID, TrackedAvatar> _UntrackedAvatars = new Dictionary<UUID, TrackedAvatar>();

        public delegate void AvatarCallback(TrackedAvatar trackedAvatar);

        /// <summary>
        /// Triggered when the user double clicks on an avatar in the list
        /// </summary>
        public event AvatarCallback OnAvatarDoubleClick;

        /// <summary>
        /// Triggered when a new avatar is added to the list
        /// </summary>
        public event AvatarCallback OnAvatarAdded;

        /// <summary>
        /// Triggered when an avatar is removed from the list
        /// </summary>
        public event AvatarCallback OnAvatarRemoved;

        /// <summary>
        /// Gets or sets the GridClient associated with this control
        /// </summary>
        public SecondLifeBot Client
        {
            get { return _Client; }
            set
            {
                if (value != null)
                    InitializeClient(value);
            }
        }

        /// <summary>
        /// Returns the current selected avatar in the tracked avatars list
        /// </summary>
        public TrackedAvatar SelectedAvatar
        {
            get { return _SelectedAvatar; }
        }

        /// <summary>
        /// TreeView control for an unspecified client's nearby avatar list
        /// </summary>
        public AvatarList()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            ColumnHeader header1 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Name"));
            header1.Width = 192;

            ColumnHeader header2 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Distance"));
            header2.Width = 58;

            ColumnHeader header3 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Viewer"));
            header3.Width = 120;

            ColumnHeader header4 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Position"));
            header4.Width = 116;

            this.MultiSelect = false;
            this.SelectedIndexChanged += new EventHandler(AvatarList_SelectedIndexChanged);

            _ColumnSorter.SortColumn = 1;
            this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;

            EventHandler clickHandler = new EventHandler(defaultMenuItem_Click);
            this.ContextMenu = new ContextMenu();
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.CopyID"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ShowProfile"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferTP"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.TPTo"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WalkTo"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.SendMessage"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ListAttachs"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpAttachs"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpOutfit"), clickHandler);
            this.ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriendship"), clickHandler);

            this.DoubleBuffered = true;
            this.ListViewItemSorter = _ColumnSorter;
            this.View = View.Details;
            this.ColumnClick += new ColumnClickEventHandler(AvatarList_ColumnClick);
            this.DoubleClick += new EventHandler(AvatarList_DoubleClick);
        }

        void AvatarList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (_TrackedAvatars)
            {
                lock (_UntrackedAvatars)
                {
                    if (this.SelectedItems.Count > 0)
                    {
                        UUID selectedID = new UUID(this.SelectedItems[0].Name);
                        TrackedAvatar selectedAV;
                        if (!_TrackedAvatars.TryGetValue(selectedID, out selectedAV) && !_UntrackedAvatars.TryGetValue(selectedID, out selectedAV))
                            selectedAV = null;

                        _SelectedAvatar = selectedAV;
                    }
                }
            }
        }

        /// <summary>
        /// Thread-safe method for clearing the TreeView control
        /// </summary>
        public void ClearItems()
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ClearItems();
                });
            else
            {
                if (this.Handle != IntPtr.Zero)
                    this.Items.Clear();
            }
        }

        public TrackedAvatar GetAvatar(UUID avatarID)
        {
            TrackedAvatar av;
            _TrackedAvatars.TryGetValue(avatarID, out av);
            return av;
        }

        private void InitializeClient(SecondLifeBot client)
        {
            _Client = client;
            _Client.Avatars.AvatarAppearance += Avatars_OnAvatarAppearance;
            _Client.Avatars.UUIDNameReply += new EventHandler<UUIDNameReplyEventArgs>(Avatars_UUIDNameReply);
            _Client.Grid.CoarseLocationUpdate += Grid_CoarseLocationUpdate;
            _Client.Network.SimChanged += Network_OnCurrentSimChanged;
            _Client.Objects.AvatarUpdate += Objects_OnNewAvatar;
            _Client.Objects.TerseObjectUpdate += Objects_OnObjectUpdated;
        }

        void Avatars_UUIDNameReply(object sender, UUIDNameReplyEventArgs e)
        {
            lock (_UntrackedAvatars)
            {
                foreach (KeyValuePair<UUID, string> name in e.Names)
                {
                    TrackedAvatar trackedAvatar;
                    if (_UntrackedAvatars.TryGetValue(name.Key, out trackedAvatar))
                    {
                        trackedAvatar.Name = name.Value;

                        if (OnAvatarAdded != null && trackedAvatar.ListViewItem.Text == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        {
                            try
                            {
                                OnAvatarAdded(trackedAvatar);
                            }
                            catch (Exception ex)
                            {
                                Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex);
                            }
                        }

                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            trackedAvatar.ListViewItem.Text = name.Value;
                        });
                    }
                }
            }
        }

        void Grid_CoarseLocationUpdate(object sender, CoarseLocationUpdateEventArgs e)
        {
            UpdateCoarseInfo(e.Simulator, e.NewEntries, e.RemovedEntries);
        }

        public void InitializateAvatars()
        {
            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Client.Network.Simulators[i].ObjectsAvatars.ForEach(
                        delegate(Avatar av)
                        {
                            Vector3 coarsePos;
                            if (!Client.Network.Simulators[i].AvatarPositions.TryGetValue(av.ID, out coarsePos))
                                coarsePos = Vector3.Zero;
                            coarsePos.Z = av.Position.Z;
                            this.BeginInvoke((MethodInvoker)delegate
                            {
                                AddAvatar(av.ID, av, coarsePos);
                            });
                        });
                }
            }
        }

        private void AddAvatar(UUID avatarID, Avatar avatar, Vector3 coarsePosition)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AddAvatar(avatar.ID, avatar, coarsePosition);
                });
            else
            {
                TrackedAvatar trackedAvatar = new TrackedAvatar();
                trackedAvatar.CoarseLocation = coarsePosition;
                trackedAvatar.ID = avatarID;
                trackedAvatar.ListViewItem = this.Items.Add(avatarID.ToString(), trackedAvatar.Name, null);
                trackedAvatar.ListViewItem.Name = avatarID.ToString();

                string strDist = avatarID == _Client.Self.AgentID ? "--" : (int)Vector3.Distance(_Client.Self.SimPosition, coarsePosition) + "m";
                trackedAvatar.ListViewItem.SubItems.Add(strDist);

                if (avatar != null)
                {
                    string ViewerName;
                    Dictionary<UUID, string> ClientNames = ClientTags.ToDictionary();

                    if (avatar.Textures == null)
                        ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unknown");
                    else
                    {
                        if (avatar.Textures.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint] != null)
                        {
                            if (!ClientNames.TryGetValue(avatar.Textures.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint].TextureID, out ViewerName))
                                ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unidentified");
                        }
                        else
                            ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unknown");
                    }

                    trackedAvatar.ListViewItem.SubItems.Add(ViewerName);
                }
                else
                {
                    trackedAvatar.ListViewItem.SubItems.Add(bot.Localization.clResourceManager.getText("Viewer.Unknown"));
                }

                trackedAvatar.ListViewItem.SubItems.Add(coarsePosition.ToString());

                if (avatar != null)
                {
                    trackedAvatar.Name = avatar.Name;
                    trackedAvatar.ListViewItem.Text = avatar.Name;

                    lock (_TrackedAvatars)
                    {
                        if (_TrackedAvatars.ContainsKey(avatarID))
                            _TrackedAvatars.Remove(avatarID);

                        _TrackedAvatars.Add(avatar.LocalID, avatarID, trackedAvatar);
                    }

                    if (OnAvatarAdded != null)
                    {
                        try
                        {
                            OnAvatarAdded(trackedAvatar);
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex);
                        }
                    }
                }
                else
                {
                    lock (_UntrackedAvatars)
                    {
                        _UntrackedAvatars.Add(avatarID, trackedAvatar);

                        trackedAvatar.ListViewItem.ForeColor = Color.FromKnownColor(KnownColor.GrayText);

                        if (avatarID == _Client.Self.AgentID)
                        {
                            trackedAvatar.Name = _Client.Self.Name;
                            trackedAvatar.ListViewItem.Text = _Client.Self.Name;
                        }
                        else if (_Client.Network.Connected)
                            Client.Avatars.RequestAvatarName(avatarID);
                    }
                }

            }
        }

        private void RemoveAvatar(UUID id)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    RemoveAvatar(id);
                });
            else
            {
                TrackedAvatar trackedAvatar;

                lock (_TrackedAvatars)
                {
                    if (_TrackedAvatars.TryGetValue(id, out trackedAvatar))
                    {
                        this.Items.Remove(trackedAvatar.ListViewItem);
                        _TrackedAvatars.Remove(id);
                    }
                }

                lock (_UntrackedAvatars)
                {
                    if (_UntrackedAvatars.TryGetValue(id, out trackedAvatar))
                    {
                        this.Items.Remove(trackedAvatar.ListViewItem);
                        _UntrackedAvatars.Remove(trackedAvatar.ID);
                    }
                }

                if (OnAvatarRemoved != null)
                {
                    try
                    {
                        OnAvatarRemoved(trackedAvatar);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex);
                    }
                }
            }
        }

        private void UpdateAvatar(Avatar avatar)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateAvatar(avatar);
                });
            else
            {
                TrackedAvatar trackedAvatar;
                bool found;

                lock (_UntrackedAvatars)
                    found = _UntrackedAvatars.TryGetValue(avatar.ID, out trackedAvatar);

                if (found)
                {
                    trackedAvatar.Name = avatar.Name;
                    trackedAvatar.ListViewItem.Text = avatar.Name;
                    trackedAvatar.ListViewItem.ForeColor = Color.FromKnownColor(KnownColor.ControlText);

                    lock (_TrackedAvatars)
                        _TrackedAvatars.Add(avatar.LocalID, avatar.ID, trackedAvatar);
                    _UntrackedAvatars.Remove(avatar.ID);
                }

                lock (_TrackedAvatars)
                    found = _TrackedAvatars.TryGetValue(avatar.ID, out trackedAvatar);

                if (found)
                {
                    string strDist = avatar.ID == _Client.Self.AgentID ? "--" : (int)Vector3.Distance(_Client.Self.SimPosition, avatar.Position) + "m";
                    trackedAvatar.ListViewItem.SubItems[1].Text = strDist;
                    trackedAvatar.ListViewItem.SubItems[3].Text = avatar.Position.ToString();
                }
                else
                {
                    AddAvatar(avatar.ID, avatar, Vector3.Zero);
                }               

                this.Sort();
            }
        }

        private void UpdateCoarseInfo(Simulator sim, List<UUID> newEntries, List<UUID> removedEntries)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateCoarseInfo(sim, newEntries, removedEntries);
                });
            else
            {
                if (sim == null)
                    return;

                if (removedEntries != null)
                {
                    for (int i = 0; i < removedEntries.Count; i++)
                        RemoveAvatar(removedEntries[i]);
                }

                if (newEntries != null)
                {
                    for (int i = 0; i < newEntries.Count; i++)
                    {
                        int index = this.Items.IndexOfKey(newEntries[i].ToString());
                        if (index == -1)
                        {
                            Vector3 coarsePos;
                            if (!sim.AvatarPositions.TryGetValue(newEntries[i], out coarsePos))
                                continue;

                            AddAvatar(newEntries[i], null, coarsePos);
                        }
                    }
                }
            }
        }

        private void defaultMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            int MenuIndex;

            if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.CopyID"))
                MenuIndex = 1;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ShowProfile"))
                MenuIndex = 2;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferTP"))
                MenuIndex = 3;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.TPTo"))
                MenuIndex = 4;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WalkTo"))
                MenuIndex = 5;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.SendMessage"))
                MenuIndex = 6;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriendship"))
                MenuIndex = 7;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ListAttachs"))
                MenuIndex = 8;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpAttachs"))
                MenuIndex = 9;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpOutfit"))
                MenuIndex = 10;
            else
                return;

            switch (MenuIndex)
            {
                case 1:
                    {
                        Clipboard.SetText(_SelectedAvatar.ID.ToString().ToUpper(), TextDataFormat.Text);
                        break;
                    }
                case 2:
                    {
                        _frmProfile = new frmProfile(_Client, _SelectedAvatar.ID);
                        _frmProfile.RefreshInfo();
                        _frmProfile.Show();
                        break;
                    }
                case 3:
                    {
                        frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferTP.DialogTitle"),
                                                   String.Format(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferTP.DialogLabel"), _SelectedAvatar.Name),
                                                   bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Message"));

                        DialogResult _result = _frmDialog.ShowDialog();
                        if (_result == DialogResult.OK)
                        {
                            this.Client.Self.SendTeleportLure(_SelectedAvatar.ID, _frmDialog._output);
                        }

                        break;
                    }
                case 4:
                    {
                        Vector3 pos;
                        if (Client.Network.CurrentSim.AvatarPositions.TryGetValue(_SelectedAvatar.ID, out pos))
                            Client.Self.Teleport(Client.Network.CurrentSim.Name, pos);

                        break;
                    }
                case 5:
                    {
                        Vector3 pos;
                        if (Client.Network.CurrentSim.AvatarPositions.TryGetValue(_SelectedAvatar.ID, out pos))
                            Client.Self.AutoPilotLocal((int)pos.X, (int)pos.Y, pos.Z);

                        break;
                    }
                case 6:
                    {
                        frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.SendIM.DialogTitle"),
                                                   String.Format(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.SendIM.DialogLabel"), _SelectedAvatar.Name),
                                                   "");

                        DialogResult _result = _frmDialog.ShowDialog();
                        if (_result == DialogResult.OK)
                        {
                            this.Client.Self.InstantMessage(_SelectedAvatar.ID, _frmDialog._output);

                            bot.Chat.structInstantMessage sim;
                            InstantMessage im = new InstantMessage();

                            im.Message = _frmDialog._output;
                            im.FromAgentID = _SelectedAvatar.ID;
                            im.FromAgentName = _SelectedAvatar.Name;
                            im.Dialog = InstantMessageDialog.MessageFromAgent;

                            sim.client = this._Client;
                            sim.isReceived = false;
                            sim.message = im;
                            sim.simulator = this._Client.Network.CurrentSim;
                            sim.timestamp = DateTime.Now;

                            bot.Chat.receivedIM(sim);
                        }

                        break;
                    }
                case 7:
                    {
                        if (_Client.Friends.FriendList.ContainsKey(_SelectedAvatar.ID))
                        {
                            MessageBox.Show(String.Format(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriend.AlreadyDialogLabel"), _SelectedAvatar.Name),
                                bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriend.AlreadyDialogTitle"), MessageBoxButtons.OK);
                        }
                        else
                        {
                            frmDialog _frmDialog = new frmDialog(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriend.DialogTitle"),
                                                       String.Format(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriend.DialogLabel"), _SelectedAvatar.Name),
                                                       bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.OfferFriend.Message"));

                            DialogResult _result = _frmDialog.ShowDialog();
                            if (_result == DialogResult.OK)
                            {
                                this.Client.Friends.OfferFriendship(_SelectedAvatar.ID, _frmDialog._output);
                            }
                        }
                        break;
                    }
                case 8:
                    {
                        string _result = _Client.DoCommandReturn("attachmentsuuid " + _SelectedAvatar.ID.ToString(),
                                             _Client.MasterKey, true);
                        frmResult _frmResult = new frmResult(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ListAttachsResultLabel"),
                                                   String.Format(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.ListAttachsResultTitle"), SelectedAvatar.Name),
                                                   _result);

                        _frmResult.ShowDialog();
                        break;
                    }
                case 9:
                    {
                        DialogResult _dresult = MessageBox.Show(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine1") + System.Environment.NewLine +
                                                bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine2") + System.Environment.NewLine +
                                                bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine3"), bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine3"),
                                                    MessageBoxButtons.YesNo);
                        if (_dresult == DialogResult.Yes)
                        {
                            string _result = _Client.DoCommandReturn("dumpattachments " + _SelectedAvatar.ID.ToString(),
                                                 _Client.MasterKey, true);
                            frmResult _frmResult = new frmResult(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpAttachsDumped"), "", _result);

                            _frmResult.ShowDialog();
                        }

                        break;
                    }
                case 10:
                    {
                        DialogResult _dresult = MessageBox.Show(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine1") + System.Environment.NewLine +
                                                bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine2") + System.Environment.NewLine +
                                                bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine3"), bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.WarningLine3"),
                                                    MessageBoxButtons.YesNo);
                        if (_dresult == DialogResult.Yes)
                        {
                            string _result = _Client.DoCommandReturn("dumpoutfit " + _SelectedAvatar.ID.ToString(),
                                                 _Client.MasterKey, true);
                            frmResult _frmResult = new frmResult(bot.Localization.clResourceManager.getText("Controls.AvatarList.Menu.DumpOutfitDumped"), "", _result);

                            _frmResult.ShowDialog();
                        }

                        break;
                    }
            }
        }

        void AvatarList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _ColumnSorter.SortColumn = e.Column;
            if ((_ColumnSorter.Ascending = (this.Sorting == SortOrder.Ascending)))
                this.Sorting = SortOrder.Descending;
            else
                this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;
        }

        void AvatarList_DoubleClick(object sender, EventArgs e)
        {
            if (OnAvatarDoubleClick != null)
            {
                ListView list = (ListView)sender;
                if (list.SelectedItems.Count > 0)
                {
                    TrackedAvatar trackedAvatar;
                    if (!_TrackedAvatars.TryGetValue(new UUID(list.SelectedItems[0].Name), out trackedAvatar)
                        && !_UntrackedAvatars.TryGetValue(new UUID(list.SelectedItems[0].Name), out trackedAvatar))
                        return;

                    try
                    {
                        OnAvatarDoubleClick(trackedAvatar);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex);
                    }
                }
            }
        }

        void Avatars_OnAvatarAppearance(object sender, AvatarAppearanceEventArgs e)
        {
            TrackedAvatar trackedAvatar;
            bool foundAvatar;

            lock (_TrackedAvatars)
                foundAvatar = _TrackedAvatars.TryGetValue(e.AvatarID, out trackedAvatar);
            
            if (e.VisualParams.Count > 31)
            {
                if (foundAvatar)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        byte param = e.VisualParams[31];
                        if (param > 0)
                            trackedAvatar.ListViewItem.ForeColor = Color.Blue;
                        else
                            trackedAvatar.ListViewItem.ForeColor = Color.Magenta;
                    });
                }
            }

            if (foundAvatar)
            {
             
                this.BeginInvoke((MethodInvoker)delegate
                {
                    string ViewerName;
                    Dictionary<UUID, string> ClientNames = ClientTags.ToDictionary();
                        
                    if (e.FaceTextures == null)
                        ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unknown");
                    else
                    {
                        if (e.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint] != null)
                        {
                            if (!ClientNames.TryGetValue(e.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint].TextureID, out ViewerName))
                                ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unidentified");
                        }
                        else
                            ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unidentified");
                    }

                    trackedAvatar.ListViewItem.SubItems[2].Text = ViewerName;
                });
            }
        }

        void Network_OnCurrentSimChanged(object sender, SimChangedEventArgs e)
        {
            lock (_TrackedAvatars)
                _TrackedAvatars.Clear();

            lock (_UntrackedAvatars)
                _UntrackedAvatars.Clear();

            ClearItems();
        }

        void Objects_OnNewAvatar(object sender, AvatarUpdateEventArgs e)
        {
            UpdateAvatar(e.Avatar);
        }

        void Objects_OnObjectUpdated(object sender, TerseObjectUpdateEventArgs e)
        {
            bool found;
            lock (_TrackedAvatars)
                found = _TrackedAvatars.ContainsKey(e.Update.LocalID);

            if (found)
            {
                Avatar av;
                if (e.Simulator.ObjectsAvatars.TryGetValue(e.Update.LocalID, out av))
                    UpdateAvatar(av);
            }
        }
    }

    /// <summary>
    /// Contains any available information for an avatar in the simulator.
    /// A null value for .Avatar indicates coarse data for an avatar outside of visible range.
    /// </summary>
    public class TrackedAvatar
    {
        /// <summary>Assigned if the avatar is within visible range</summary>
        public Avatar Avatar = null;

        /// <summary>Last known coarse location of avatar</summary>
        public Vector3 CoarseLocation;

        /// <summary>Avatar ID</summary>
        public UUID ID;

        /// <summary>ListViewItem associated with this avatar</summary>
        public ListViewItem ListViewItem;

        /// <summary>Populated by RequestAvatarName if avatar is not visible</summary>
        public string Name = bot.Localization.clResourceManager.getText("Controls.Loading");
    }

}
