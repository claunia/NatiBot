/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmInventory.cs
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
using OpenMetaverse.Utilities;

namespace bot.GUI
{
    public partial class frmInventory: Form
    {
        private SecondLifeBot Client;
        private Point mouse_offset;

        public frmInventory(SecondLifeBot client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.InitializeComponent();
            this.Client = client;
            tvInventory.Client = this.Client;
            fileToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.File");
            copyObjectUUIDToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.CopyID");
            deleteObjectFolderToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.Delete");
            wearToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.Attach");
            detachToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.Detach");
            emptyTrashToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyTrash");
            emptyLostFoundToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmInventory.Menu.EmptyLF");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmInventory");
        }

        private void frmInventory_Load(object sender, EventArgs e)
        {
            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmInventory.Text"), Client.LoginDetails.FullName);
        }

        private void copyObjectUUIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryItem item = (InventoryItem)this.Client.Inventory.Store[new UUID(tvInventory.SelectedNode.Name)];

            Clipboard.SetText(item.AssetUUID.ToString());
        }

        private void deleteObjectFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryItem item = (InventoryItem)this.Client.Inventory.Store[new UUID(tvInventory.SelectedNode.Name)];

            if (item.InventoryType == InventoryType.Folder)
                this.Client.Inventory.RemoveFolder(item.UUID);
            else
                this.Client.Inventory.RemoveItem(item.UUID);
        }

        private void wearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryItem item = (InventoryItem)this.Client.Inventory.Store[new UUID(tvInventory.SelectedNode.Name)];

            this.Client.Appearance.Attach(item, AttachmentPoint.Default);
        }

        private void detachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InventoryItem item = (InventoryItem)this.Client.Inventory.Store[new UUID(tvInventory.SelectedNode.Name)];

            this.Client.Appearance.Detach(item);
        }

        private void emptyTrashToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Client.Inventory.EmptyTrash();
        }

        private void emptyLostFoundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Client.Inventory.EmptyLostAndFound();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmInventory_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        void frmInventory_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
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
