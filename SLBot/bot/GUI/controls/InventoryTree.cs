/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : InventoryTree.cs
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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenMetaverse;

namespace bot.GUI
{

    /// <summary>
    /// TreeView GUI component for browsing a client's inventory
    /// </summary>
    public class InventoryTree : TreeView
    {
        private GridClient _Client;
        private ContextMenu _ContextMenu;
        private UUID _SelectedItemID;

        /// <summary>
        /// Gets or sets the context menu associated with this control
        /// </summary>
        public ContextMenu Menu
        {
            get { return _ContextMenu; }
            set { _ContextMenu = value; }
        }

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
        /// TreeView control for an unspecified client's inventory
        /// </summary>
        public InventoryTree()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            EventHandler clickHandler = new EventHandler(defaultMenuItem_Click);
            _ContextMenu = new ContextMenu();
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.CopyID"), clickHandler);
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.Delete"), clickHandler);
            _ContextMenu.MenuItems.Add("-");
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.Attach"), clickHandler);
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.Detach"), clickHandler);
            _ContextMenu.MenuItems.Add("-");
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyTrash"), clickHandler);
            _ContextMenu.MenuItems.Add(bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyLF"), clickHandler);

            this.NodeMouseClick += new TreeNodeMouseClickEventHandler(InventoryTree_NodeMouseClick);
            this.BeforeExpand += new TreeViewCancelEventHandler(InventoryTree_BeforeExpand);
        }

        /// <summary>
        /// TreeView control for the specified client's inventory
        /// </summary>
        /// <param name="client"></param>
        public InventoryTree(GridClient client) : this()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeClient(client);
        }

        /// <summary>
        /// Thread-safe method for clearing the TreeView control
        /// </summary>
        public void ClearNodes()
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ClearNodes();
                });
            else
                this.Nodes.Clear();
        }

        /// <summary>
        /// Thread-safe method for collapsing a TreeNode in the control
        /// </summary>
        /// <param name="node"></param>
        public void CollapseNode(TreeNode node)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    CollapseNode(node);
                });
            else if (!node.IsExpanded)
                node.Collapse();
        }

        /// <summary>
        /// Thread-safe method for expanding a TreeNode in the control
        /// </summary>
        /// <param name="node"></param>
        public void ExpandNode(TreeNode node)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    ExpandNode(node);
                });
            else if (!node.IsExpanded)
                node.Expand();
        }

        /// <summary>
        /// Thread-safe method for updating the contents of the specified folder UUID
        /// </summary>
        /// <param name="folderID"></param>
        public void UpdateFolder(UUID folderID)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    UpdateFolder(folderID);
                });
            else
            {
                TreeNode node = null;
                TreeNodeCollection children;

                if (folderID != Client.Inventory.Store.RootFolder.UUID)
                {
                    TreeNode[] found = Nodes.Find(folderID.ToString(), true);
                    if (found.Length > 0)
                    {
                        node = found[0];
                        children = node.Nodes;
                    }
                    else
                    {
                        Logger.Log("Received update for unknown TreeView node " + folderID, Helpers.LogLevel.Warning);
                        return;
                    }
                }
                else
                    children = this.Nodes;

                children.Clear();

                List<InventoryBase> contents = Client.Inventory.Store.GetContents(folderID);
                if (contents.Count == 0)
                {
                    TreeNode add = children.Add(null, bot.Localization.clResourceManager.getText("Controls.InventoryList.Empty"));
                    add.ForeColor = Color.FromKnownColor(KnownColor.GrayText);
                }
                else
                {
                    foreach (InventoryBase inv in contents)
                    {
                        string key = inv.UUID.ToString();

                        children.Add(key, inv.Name);
                        if (inv is InventoryFolder)
                        {
                            children[key].Nodes.Add(null, bot.Localization.clResourceManager.getText("Controls.Loading")).ForeColor = Color.FromKnownColor(KnownColor.GrayText);
                        }
                    }
                }
            }
        }

        private void InitializeClient(GridClient client)
        {
            _Client = client;
            _Client.Inventory.FolderUpdated += Inventory_OnFolderUpdated;
            _Client.Network.LoginProgress += Network_OnLogin;
            if (Client.Inventory.Store != null)
                UpdateFolder(Client.Inventory.Store.RootFolder.UUID);
        }

        private void defaultMenuItem_Click(object sender, EventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;

            InventoryItem item = (InventoryItem)Client.Inventory.Store[_SelectedItemID];

            int MenuIndex;

            if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.CopyID"))
                MenuIndex = 1;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.Delete"))
                MenuIndex = 2;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.Attach"))
                MenuIndex = 3;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.Detach"))
                MenuIndex = 4;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyTrash"))
                MenuIndex = 5;
            else if (menuItem.Text == bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyLF"))
                MenuIndex = 6;
            else
                return;

            switch (MenuIndex)
            {
                case 1:
                    {
                        Clipboard.SetText(item.AssetUUID.ToString());
                        break;
                    }
                case 2:
                    {
                        if (item.InventoryType == InventoryType.Folder)
                            _Client.Inventory.RemoveFolder(item.UUID);
                        else
                            _Client.Inventory.RemoveItem(item.UUID);
                        break;
                    }
                case 3:
                    {
                        _Client.Appearance.Attach(item, AttachmentPoint.Default);
                        break;
                    }
                case 4:
                    {
                        _Client.Appearance.Detach(item);
                        break;
                    }
                case 5:
                    {
                        _Client.Inventory.EmptyTrash();
                        break;
                    }
                case 6:
                    {
                        _Client.Inventory.EmptyLostAndFound();
                        break;
                    }
            }
        }

        void Network_OnLogin(object sender, LoginProgressEventArgs e)
        {
            /*  if (e.Status == LoginStatus.Success)
            {
                if (Client.Inventory.Store != null)
                    UpdateFolder(Client.Inventory.Store.RootFolder.UUID);
            }*/
        }

        private void Inventory_OnFolderUpdated(object sender, FolderUpdatedEventArgs e)
        {
            UpdateFolder(e.FolderID);
        }

        void InventoryTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _SelectedItemID = new UUID(e.Node.Name);
                _ContextMenu.Show(this, e.Location);
            }
        }

        private void InventoryTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            InventoryFolder folder = (InventoryFolder)Client.Inventory.Store[new UUID(e.Node.Name)];
            Client.Inventory.RequestFolderContents(folder.UUID, _Client.Self.AgentID, true, true, InventorySortOrder.ByDate | InventorySortOrder.FoldersByName);
        }

    }

}

