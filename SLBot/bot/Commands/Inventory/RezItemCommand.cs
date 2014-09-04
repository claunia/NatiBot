/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : RezItemCommand.cs
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

    public class RezItemCommand : Command
    {
        private InventoryManager Manager;
        private OpenMetaverse.Inventory Inventory;

        public RezItemCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "rezitem";
            base.Description = bot.Localization.clResourceManager.getText("Commands.RezItem.Description") + " " + bot.Localization.clResourceManager.getText("Commands.RezItem.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length != 1)
            {
                return bot.Localization.clResourceManager.getText("Commands.RezItem.Usage");
            }
            UUID dest;
            if (!UUID.TryParse(args[0], out dest))
            {
                return bot.Localization.clResourceManager.getText("Commands.RezItem.ExpectedID");
            }
            Manager = Client.Inventory;
            Inventory = Manager.Store;

            string inventoryName = args[0];
            // WARNING: Uses local copy of inventory contents, need to download them first.
            List<InventoryBase> contents = Inventory.GetContents(Client.CurrentDirectory);
            foreach (InventoryBase b in contents)
            {
                if (inventoryName == b.Name || inventoryName.ToLower() == b.UUID.ToString())
                {
                    if (b is InventoryItem)
                    {
                        InventoryItem item = b as InventoryItem;
                        Vector3 Position = new Vector3();

                        Position = Client.Self.SimPosition;
                        Position.Z += 3.0f;

                        Manager.RequestRezFromInventory(Client.Network.CurrentSim, Quaternion.Identity, Position, item);
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.RezItem.Requesting"), item.Name);
                    }
                    else
                    {
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.RezItem.CannotFolder"), b.Name);
                    }
                }
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.RezItem.NotFound"), inventoryName);
        }
    }
}