/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ObjectInventoryCommand.cs
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

    public class ObjectInventoryCommand : Command
    {
        public ObjectInventoryCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "objectinventory";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Usage");

            uint objectLocalID;
            UUID objectID;
            if (!UUID.TryParse(args[0], out objectID))
                return bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Usage");

            Primitive found = Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim)
            {
                return prim.ID == objectID;
            });
            if (found != null)
                objectLocalID = found.LocalID;
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ObjectInventory.NotFound"), objectID.ToString());

            List<InventoryBase> items = Client.Inventory.GetTaskInventory(objectID, objectLocalID, 1000 * 30);

            if (items != null)
            {
                string result = String.Empty;

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i] is InventoryFolder)
                    {
                        result += String.Format(bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Folder"), items[i].Name) + Environment.NewLine;
                    }
                    else
                    {
                        InventoryItem item = (InventoryItem)items[i];
                        result += String.Format(bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Item"), item.Name, item.Description,
                            item.AssetType, item.UUID) + Environment.NewLine;
                    }
                }

                return result;
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ObjectInventory.Failed"), objectLocalID);
            }
        }
    }
}

