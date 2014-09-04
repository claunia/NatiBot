/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GiveItemCommand.cs
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
    using OpenMetaverse;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class GiveItemCommand : Command
    {
        private InventoryManager Manager;
        private OpenMetaverse.Inventory Inventory;

        public GiveItemCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "give";
            base.Description = bot.Localization.clResourceManager.getText("Commands.GiveItem.Description") + " " + bot.Localization.clResourceManager.getText("Commands.GiveItem.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length < 2)
            {
                return bot.Localization.clResourceManager.getText("Commands.GiveItem.Usage");
            }
            UUID dest;
            if (!UUID.TryParse(args[0], out dest))
            {
                return bot.Localization.clResourceManager.getText("Commands.GiveItem.InvalidUUID");
            }
            Manager = Client.Inventory;
            Inventory = Manager.Store;
            string ret = "";
            string nl = "\n";
            for (int i = 1; i < args.Length; ++i)
            {
                string inventoryName = args[i];
                // WARNING: Uses local copy of inventory contents, need to download them first.
                List<InventoryBase> contents = Inventory.GetContents(Client.CurrentDirectory);
                bool found = false;
                foreach (InventoryBase b in contents)
                {
                    if (inventoryName == b.Name || inventoryName == b.UUID.ToString())
                    {
                        found = true;
                        if (b is InventoryItem)
                        {
                            InventoryItem item = b as InventoryItem;
                            Manager.GiveItem(item.UUID, item.Name, item.AssetType, dest, true);
                            ret += String.Format(bot.Localization.clResourceManager.getText("Commands.GiveItem.Gave"), item.Name);
                        }
                        else
                        {
                            ret += String.Format(bot.Localization.clResourceManager.getText("Commands.GiveItem.Folder"), b.Name);
                        }
                    }
                }
                if (!found)
                    ret += String.Format(bot.Localization.clResourceManager.getText("Commands.GiveItem.NotFound"), inventoryName);
            }
            return ret;
        }
    }
}