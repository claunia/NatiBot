/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ObjectList.cs
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
Portions copyright (C) 2007-2009 openmetaverse.org
****************************************************************************/

// Derived from AvatarList.cs
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
    public class ObjectList : ListView
    {
        private SecondLifeBot _Client;
        private ListColumnSorterNormal _ColumnSorter = new ListColumnSorterNormal();
        private TrackedObject _SelectedObject;

        private Dictionary<UUID, TrackedObject> _TrackedObjects = new Dictionary<UUID, TrackedObject>();
        private Dictionary<UUID, TrackedObject> _UntrackedObjects = new Dictionary<UUID, TrackedObject>();
        private Dictionary<uint, UUID> _TrackedIDs = new Dictionary<uint, UUID>();

        public delegate void ObjectCallback(TrackedObject trackedObject);

        public event ObjectCallback OnObjectAdded;
        public event ObjectCallback OnObjectPropertiesUpdated;
        public event ObjectCallback OnObjectUpdated;
        public event ObjectCallback OnObjectRemoved;

        public bool StopRefreshing = false;

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
        public TrackedObject SelectedObject
        {
            get { return _SelectedObject; }
        }

        /// <summary>
        /// TreeView control for an unspecified client's nearby avatar list
        /// </summary>
        public ObjectList()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            ColumnHeader header1 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.ObjectList.Name"));
            header1.Width = 192;

            ColumnHeader header2 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.ObjectList.Position"));
            header2.Width = 58;

            ColumnHeader header3 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.ObjectList.ID"));
            header3.Width = 58;

            this.MultiSelect = true;
            this.SelectedIndexChanged += new EventHandler(ObjectList_SelectedIndexChanged);

            _ColumnSorter.SortColumn = 1;
            this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;

            this.DoubleBuffered = true;
            this.ListViewItemSorter = _ColumnSorter;
            this.View = View.Details;
            this.ColumnClick += new ColumnClickEventHandler(ObjectList_ColumnClick);
            this.DoubleClick += new EventHandler(ObjectList_DoubleClick);
        }

        void ObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lock (_TrackedObjects)
            {
                lock (_UntrackedObjects)
                {
                    if (this.SelectedItems.Count > 0)
                    {
                        UUID selectedID = new UUID(this.SelectedItems[0].Name);
                        TrackedObject selectedOBJ;
                        if (!_TrackedObjects.TryGetValue(selectedID, out selectedOBJ) && !_UntrackedObjects.TryGetValue(selectedID, out selectedOBJ))
                            selectedOBJ = null;

                        _SelectedObject = selectedOBJ;
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

        public TrackedObject GetObject(UUID objectID)
        {
            TrackedObject obj;
            _TrackedObjects.TryGetValue(objectID, out obj);
            return obj;
        }

        private void InitializeClient(SecondLifeBot client)
        {
            _Client = client;
            _Client.Network.SimChanged += Network_OnCurrentSimChanged;
            _Client.Objects.ObjectUpdate += new EventHandler<PrimEventArgs>(Objects_ObjectUpdate);
            _Client.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_ObjectProperties);
            _Client.Objects.KillObject += new EventHandler<KillObjectEventArgs>(Objects_KillObject);
        }

        void Objects_KillObject(object sender, KillObjectEventArgs e)
        {
            UUID objID;
            bool found;

            if (StopRefreshing)
                return;

            lock (_TrackedIDs)
                found = _TrackedIDs.TryGetValue(e.ObjectLocalID, out objID);

            if (found)
                RemoveObject(objID);
        }

        void Objects_ObjectUpdate(object sender, PrimEventArgs e)
        {
            bool found = false;

            if (StopRefreshing)
                return;
            
            lock (_TrackedObjects)
                found = _TrackedObjects.ContainsKey(e.Prim.ID);

            if (!found && e.IsNew && !e.IsAttachment && e.Prim.ParentID == 0)
            {
                AddObject(e.Prim.ID, e.Prim, e.Prim.Position);
                return;
            }
            
            if (found)
            {
                Primitive obj;
                if (e.Simulator.ObjectsPrimitives.TryGetValue(e.Prim.LocalID, out obj))
                    UpdateObject(obj);
            }
        }

        private void Objects_ObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            if (StopRefreshing)
                return;

            UpdateObjectProperties(e.Properties.ObjectID, e.Properties);
        }

        private void UpdateObjectProperties(UUID objID, Primitive.ObjectProperties objProperties)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateObjectProperties(objID, objProperties);
                });
            else
            {
                TrackedObject trackedObject;
                bool found;

                lock (_TrackedObjects)
                    found = _TrackedObjects.TryGetValue(objID, out trackedObject);

                if (found)
                {
                    trackedObject.ListViewItem.Text = objProperties.Name;
                    if (OnObjectPropertiesUpdated != null)
                        OnObjectPropertiesUpdated(trackedObject);
                    if (_Client.Account.LoginDetails.BotConfig.TouchMidnightMania)
                    {
                        if (objProperties.Name.ToLower().Contains("midnight") && objProperties.Name.ToLower().Contains("mania"))
                        {
                            _Client.Self.Touch(trackedObject.Primitive.LocalID);
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Controls.ObjectList.Midnight"), objID, objProperties.Name);
                        }
                    }
                    if (_Client.Account.LoginDetails.BotConfig.HaveLuck)
                    {
                        if (objProperties.Name.ToLower().Contains("lucky") && objProperties.Name.ToLower().Contains("advent"))
                        {
                            _Client.Self.Touch(trackedObject.Primitive.LocalID);
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Controls.ObjectList.Advent"), objID, objProperties.Name);
                        }
                        if (objProperties.Name.ToLower().Contains("prize") && objProperties.Name.ToLower().Contains("pyramid"))
                        {
                            _Client.Self.Touch(trackedObject.Primitive.LocalID);
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Controls.ObjectList.Pyramid"), objID, objProperties.Name);
                        }
                        if (objProperties.Name.ToLower().Contains("lucky") && objProperties.Name.ToLower().Contains("dip"))
                        {
                            Client.Self.RequestSit(objID, Vector3.Zero);
                            Client.Self.Sit();
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Controls.ObjectList.Dip"), objID, objProperties.Name);
                        }
                        if (objProperties.Name.ToLower().Contains("lucky") && objProperties.Name.ToLower().Contains("cupcake"))
                        {
                            Client.Self.RequestSit(objID, Vector3.Zero);
                            Client.Self.Sit();
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Controls.ObjectList.Cupcake"), objID, objProperties.Name);
                        }
                    }
                }
            }
        }

        public void RefreshObjects()
        {
            if (StopRefreshing)
                return;

            lock (Client.Network.Simulators)
            {
                if (Client.Network.Simulators.Count > 0)
                {
                    Client.Network.Simulators[0].ObjectsPrimitives.ForEach(
                        delegate(Primitive obj)
                        {
                            TrackedObject trackedObject;
                            bool found;

                            if (obj.ParentID == 0)
                            {
                                lock (_TrackedObjects)
                                    found = _TrackedObjects.TryGetValue(obj.ID, out trackedObject);

                                if (found)
                                {
                                    if (trackedObject.Primitive != null)
                                    {
                                        if (trackedObject.Primitive.Properties == null)
                                        {
                                            this._Client.Objects.RequestObject(this._Client.Network.CurrentSim, obj.LocalID);
                                            this._Client.Objects.SelectObject(this._Client.Network.CurrentSim, obj.LocalID);
                                        }
                                    }
                                    else
                                    {
                                        this._Client.Objects.RequestObject(this._Client.Network.CurrentSim, obj.LocalID);
                                        this._Client.Objects.SelectObject(this._Client.Network.CurrentSim, obj.LocalID);
                                    }
                                }
                                //else
                                //   this.BeginInvoke((MethodInvoker)delegate { AddObject(obj.ID, obj, obj.Position); });
                            }
                        });
                }
            }
        }

        public void InitializateObjects()
        {
            lock (Client.Network.Simulators)
            {
                if (Client.Network.Simulators.Count > 0)
                {
                    Client.Network.Simulators[0].ObjectsPrimitives.ForEach(
                        delegate(Primitive obj)
                        {
                            if (obj.ParentID == 0)
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    AddObject(obj.ID, obj, obj.Position);
                                });
                        });
                }
            }
        }

        private void AddObject(UUID objectID, Primitive obj, Vector3 Position)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    AddObject(obj.ID, obj, Position);
                });
            else
            {
                if (obj.Properties == null)
                {
                    this._Client.Objects.RequestObject(this._Client.Network.CurrentSim, obj.LocalID);
                    this._Client.Objects.SelectObject(this._Client.Network.CurrentSim, obj.LocalID);
                }

                TrackedObject trackedObject = new TrackedObject();
                trackedObject.CoarseLocation = Position;
                trackedObject.ID = objectID;
                trackedObject.Primitive = obj;
                trackedObject.ListViewItem = this.Items.Add(objectID.ToString(), trackedObject.Name, null);
                trackedObject.ListViewItem.Name = objectID.ToString();

                trackedObject.ListViewItem.SubItems.Add(Position.ToString());
                trackedObject.ListViewItem.SubItems.Add(objectID.ToString());

                if (obj != null)
                {
                    if (obj.Properties != null)
                        trackedObject.Name = obj.Properties.Name;
                    else
                        trackedObject.Name = bot.Localization.clResourceManager.getText("Controls.Loading");

                    lock (_TrackedObjects)
                    {
                        if (_TrackedObjects.ContainsKey(objectID))
                        {
                            _TrackedObjects.Remove(objectID);
                            _TrackedIDs.Remove(obj.LocalID);
                        }

                        _TrackedObjects.Add(objectID, trackedObject);
                        _TrackedIDs.Add(obj.LocalID, objectID);
                    }
                }
                else
                {
                    lock (_UntrackedObjects)
                    {
                        _UntrackedObjects.Add(objectID, trackedObject);

                        trackedObject.ListViewItem.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
                    }
                }

                if (OnObjectAdded != null)
                    OnObjectAdded(trackedObject);
            }
        }

        private void RemoveObject(UUID id)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    RemoveObject(id);
                });
            else
            {
                TrackedObject trackedObject;

                lock (_TrackedObjects)
                {
                    if (_TrackedObjects.TryGetValue(id, out trackedObject))
                    {
                        this.Items.Remove(trackedObject.ListViewItem);
                        _TrackedObjects.Remove(id);
                        _TrackedIDs.Remove(trackedObject.Primitive.LocalID);
                    }
                }

                lock (_UntrackedObjects)
                {
                    if (_UntrackedObjects.TryGetValue(id, out trackedObject))
                    {
                        this.Items.Remove(trackedObject.ListViewItem);
                        _UntrackedObjects.Remove(trackedObject.ID);
                    }
                }

                if (OnObjectRemoved != null)
                    OnObjectRemoved(trackedObject);
            }
        }

        private void UpdateObject(Primitive obj)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateObject(obj);
                });
            else
            {
                TrackedObject trackedObject;
                bool found;

                lock (_UntrackedObjects)
                    found = _UntrackedObjects.TryGetValue(obj.ID, out trackedObject);

                if (found)
                {
                    if (obj.Properties != null)
                        trackedObject.Name = obj.Properties.Name;
                    else
                        trackedObject.Name = bot.Localization.clResourceManager.getText("Controls.Loading");
                    trackedObject.ListViewItem.Text = trackedObject.Name;
                    trackedObject.ListViewItem.ForeColor = Color.FromKnownColor(KnownColor.ControlText);

                    lock (_TrackedObjects)
                        _TrackedObjects.Add(obj.ID, trackedObject);
                    lock (_TrackedIDs)
                        _TrackedIDs.Add(obj.LocalID, obj.ID);
                    _UntrackedObjects.Remove(obj.ID);
                }

                lock (_TrackedObjects)
                    found = _TrackedObjects.TryGetValue(obj.ID, out trackedObject);

                if (found)
                {
                    trackedObject.ListViewItem.SubItems[1].Text = obj.Position.ToString();
                    if (OnObjectUpdated != null)
                        OnObjectUpdated(trackedObject);
                }
                else
                {
                    AddObject(obj.ID, obj, obj.Position);
                }               

            }
        }

        void ObjectList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _ColumnSorter.SortColumn = e.Column;
            if ((_ColumnSorter.Ascending = (this.Sorting == SortOrder.Ascending)))
                this.Sorting = SortOrder.Descending;
            else
                this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;
        }

        void ObjectList_DoubleClick(object sender, EventArgs e)
        {
            /*if (OnAvatarDoubleClick != null)
            {
                ListView list = (ListView)sender;
                if (list.SelectedItems.Count > 0)
                {
                    TrackedObject trackedAvatar;
                    if (!_TrackedAvatars.TryGetValue(new UUID(list.SelectedItems[0].Name), out trackedAvatar)
                        && !_UntrackedAvatars.TryGetValue(new UUID(list.SelectedItems[0].Name), out trackedAvatar))
                        return;

                    try { OnAvatarDoubleClick(trackedAvatar); }
                    catch (Exception ex) { Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex); }
                }
            }*/
        }

        void Network_OnCurrentSimChanged(object sender, SimChangedEventArgs e)
        {
            lock (_TrackedObjects)
                _TrackedObjects.Clear();

            lock (_UntrackedObjects)
                _UntrackedObjects.Clear();

            ClearItems();
        }
    }

    /// <summary>
    /// Contains any available information for an avatar in the simulator.
    /// A null value for .Avatar indicates coarse data for an avatar outside of visible range.
    /// </summary>
    public class TrackedObject
    {
        /// <summary>Assigned if the avatar is within visible range</summary>
        public Primitive Primitive = null;

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
