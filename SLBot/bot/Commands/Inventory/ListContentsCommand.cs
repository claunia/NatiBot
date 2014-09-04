/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ListContentsCommand.cs
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
namespace bot.Commands
{
    using bot;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using OpenMetaverse;

    public class ListContentsCommand : Command
    {
        private InventoryManager Manager;
        private OpenMetaverse.Inventory Inventory;

        public ListContentsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "ls";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ListContents.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ListContents.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length > 1)
                return bot.Localization.clResourceManager.getText("Commands.ListContents.Usage");
            bool longDisplay = false;
            if (args.Length > 0 && args[0] == "-l")
                longDisplay = true;

            List<InventoryBase> contents;

            Manager = Client.Inventory;
            Inventory = Manager.Store;
            // WARNING: Uses local copy of inventory contents, need to download them first.
            if (Client.CurrentDirectory != null)
                contents = Manager.FolderContents(Client.CurrentDirectory.UUID, Inventory.RootFolder.OwnerID, true, true, InventorySortOrder.SystemFoldersToTop | InventorySortOrder.FoldersByName | InventorySortOrder.ByName, 1000);
            else
                contents = Manager.FolderContents(Inventory.RootFolder.UUID, Inventory.RootFolder.OwnerID, true, true, InventorySortOrder.SystemFoldersToTop | InventorySortOrder.FoldersByName | InventorySortOrder.ByName, 1000);

            string displayString = "";
            string nl = "\n"; // New line character
            // Pretty simple, just print out the contents.
            if (contents != null)
            {
                foreach (InventoryBase b in contents)
                {
                    if (longDisplay)
                    {
                        // Generate a nicely formatted description of the item.
                        // It kinda looks like the output of the unix ls.
                        // starts with 'd' if the inventory is a folder, '-' if not.
                        // 9 character permissions string
                        // UUID of object
                        // Name of object
                        if (b is InventoryFolder)
                        {
                            InventoryFolder folder = b as InventoryFolder;
                            displayString += "d--------- ";
                            displayString += " " + "<DIR>";
                            displayString += " " + DateTime.MinValue.ToShortDateString() + " " + DateTime.MinValue.ToShortTimeString();
                            displayString += folder.UUID.ToString().ToUpperInvariant();
                            displayString += " " + folder.Name;
                        }
                        else if (b is InventoryItem)
                        {
                            InventoryItem item = b as InventoryItem;
                            string iteminvType;

                            switch (item.AssetType)
                            {
                                case AssetType.Animation:
                                    iteminvType = "<ANM>";
                                    break;
                                case AssetType.Bodypart:
                                    iteminvType = "<BDY>";
                                    break;
                                case AssetType.CallingCard:
                                    iteminvType = "<CCD>";
                                    break;
                                case AssetType.Clothing:
                                    iteminvType = "<CLT>";
                                    break;
                                case AssetType.Folder:
                                    iteminvType = "<DIR>";
                                    break;
                                case AssetType.Gesture:
                                    iteminvType = "<GES>";
                                    break;
                                case AssetType.ImageJPEG:
                                    iteminvType = "<JPG>";
                                    break;
                                case AssetType.ImageTGA:
                                    iteminvType = "<TGA>";
                                    break;
                                case AssetType.Landmark:
                                    iteminvType = "<LND>";
                                    break;
                                case AssetType.LostAndFoundFolder:
                                    iteminvType = "<L&F>";
                                    break;
                                case AssetType.LSLBytecode:
                                    iteminvType = "<LSO>";
                                    break;
                                case AssetType.LSLText:
                                    iteminvType = "<LSL>";
                                    break;
                                case AssetType.Notecard:
                                    iteminvType = "<NOT>";
                                    break;
                                case AssetType.Object:
                                    iteminvType = "<OBJ>";
                                    break;
                                case AssetType.RootFolder:
                                    iteminvType = "< / >";
                                    break;
                                case AssetType.Simstate:
                                    iteminvType = "<SIM>";
                                    break;
                                case AssetType.SnapshotFolder:
                                    iteminvType = "<SNA>";
                                    break;
                                case AssetType.Sound:
                                    iteminvType = "<OGG>";
                                    break;
                                case AssetType.SoundWAV:
                                    iteminvType = "<WAV>";
                                    break;
                                case AssetType.Texture:
                                    iteminvType = "<JP2>";
                                    break;
                                case AssetType.TextureTGA:
                                    iteminvType = "<TGA>";
                                    break;
                                case AssetType.TrashFolder:
                                    iteminvType = "<TRS>";
                                    break;
                                case AssetType.Unknown:
                                default:
                                    iteminvType = "<?¿?>";
                                    break;
                            }

                            displayString += "-";
                            displayString += PermMaskString(item.Permissions.OwnerMask);
                            displayString += PermMaskString(item.Permissions.GroupMask);
                            displayString += PermMaskString(item.Permissions.EveryoneMask);
                            displayString += " " + iteminvType;
                            displayString += " " + item.CreationDate.ToShortDateString() + " " + item.CreationDate.ToShortTimeString();
                            displayString += " " + item.UUID.ToString().ToUpperInvariant();
                            displayString += " " + item.Name;
                        }
                    }
                    else
                    {
                        displayString += b.Name;
                    }
                    displayString += nl;
                }
                return displayString;
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.ListContents.NotReady");
            }
        }

        /// <summary>
        /// Returns a 3-character summary of the PermissionMask
        /// CMT if the mask allows copy, mod and transfer
        /// -MT if it disallows copy
        /// --T if it only allows transfer
        /// --- if it disallows everything
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        private static string PermMaskString(PermissionMask mask)
        {
            string str = "";
            if (((uint)mask | (uint)PermissionMask.Copy) == (uint)PermissionMask.Copy)
                str += "C";
            else
                str += "-";
            if (((uint)mask | (uint)PermissionMask.Modify) == (uint)PermissionMask.Modify)
                str += "M";
            else
                str += "-";
            if (((uint)mask | (uint)PermissionMask.Transfer) == (uint)PermissionMask.Transfer)
                str += "T";
            else
                str += "-";
            return str;
        }

    }
}
