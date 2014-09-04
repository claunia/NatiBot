/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : WearCommand.cs
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

    public class WearCommand : Command
    {
        public WearCommand(SecondLifeBot SecondLifeBot)
        {
            base.Client = SecondLifeBot;
            base.Name = "wear";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Wear.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Wear.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.Wear.Usage");

            string target = String.Empty;
            bool bake = true;

            for (int ct = 0; ct < args.Length; ct++)
            {
                target += args[ct] + " ";
            }

            target = target.TrimEnd();

            UUID folder = Client.Inventory.FindObjectByPath(Client.Inventory.Store.RootFolder.UUID, Client.Self.AgentID, target, 20 * 1000);

            if (folder == UUID.Zero)
            {
                return "Outfit path " + target + " not found";
            }

            List<InventoryBase> contents = Client.Inventory.FolderContents(folder, Client.Self.AgentID, true, true, InventorySortOrder.ByName, 20 * 1000);
            List<InventoryItem> items = new List<InventoryItem>();

            if (contents == null)
            {
                return "Failed to get contents of " + target;
            }

            foreach (InventoryBase item in contents)
            {
                if (item is InventoryItem)
                    items.Add((InventoryItem)item);
            }

            Client.Appearance.ReplaceOutfit(items);

            return "Starting to change outfit to " + target;
        }
    }
}
