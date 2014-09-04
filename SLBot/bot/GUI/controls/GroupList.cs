/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GroupList.cs
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

// Copied from OpenMetaverse
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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenMetaverse;

namespace bot.GUI
{

    /// <summary>
    /// ListView GUI component for viewing a client's group list
    /// </summary>
    public class GroupList : ListView
    {
        private GridClient _Client;
        private ListColumnSorterNormal _ColumnSorter = new ListColumnSorterNormal();

        public delegate void GroupDoubleClickCallback(Group group);

        /// <summary>
        /// Triggered when the user double clicks on a group in the list
        /// </summary>
        public event GroupDoubleClickCallback OnGroupDoubleClick;

        /// <summary>
        /// Gets or sets the GridClient associated with this control
        /// </summary>
        public GridClient Client
        {
            get { return _Client; }
            set
            {
                if (value != null)
                    InitializeClient(value);
            }
        }

        /// <summary>
        /// TreeView control for an unspecified client's group list
        /// </summary>
        public GroupList()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            ColumnHeader header1 = this.Columns.Add(bot.Localization.clResourceManager.getText("Controls.GroupList.Column"));
            header1.Width = this.Width + 20;

            this.DoubleBuffered = true;
            this.ListViewItemSorter = _ColumnSorter;
            this.View = View.Details;

            this.ColumnClick += new ColumnClickEventHandler(GroupList_ColumnClick);
            this.DoubleClick += new System.EventHandler(GroupList_DoubleClick);
        }

        /// <summary>
        /// TreeView control for the specified client's group list
        /// </summary>
        public GroupList(GridClient client) : this()
        {
            InitializeClient(client);
        }

        private void InitializeClient(GridClient client)
        {
            _Client = client;
            _Client.Groups.CurrentGroups += Groups_CurrentGroups;
            _Client.Groups.RequestCurrentGroups();
        }

        void Groups_CurrentGroups(object sender, CurrentGroupsEventArgs e)
        {
            RefreshGroups(e.Groups);
        }

        private void RefreshGroups(Dictionary<UUID, Group> groups)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    RefreshGroups(groups);
                });
            else
            {
                this.Items.Clear();
                foreach (KeyValuePair<UUID, Group> group in groups)
                    this.Items.Add(group.Key.ToString(), group.Value.Name, null).Tag = group.Value;
            }
        }

        private void GroupList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if ((_ColumnSorter.Ascending = (this.Sorting == SortOrder.Ascending)))
                this.Sorting = SortOrder.Descending;
            else
                this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;
        }

        private void GroupList_DoubleClick(object sender, System.EventArgs e)
        {
            if (OnGroupDoubleClick != null)
            {
                ListView list = (ListView)sender;
                if (list.SelectedItems.Count > 0 && list.SelectedItems[0].Tag is Group)
                {
                    Group group = (Group)list.SelectedItems[0].Tag;
                    try
                    {
                        OnGroupDoubleClick(group);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Helpers.LogLevel.Error, Client, ex);
                    }
                }
            }
        }

    }
}
