/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : InventoryCommand.cs
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
    using System.Text;
    using System.Collections.Generic;

    public class InventoryCommand : Command
    {
        private Inventory Inventory;
        private InventoryManager Manager;

        public InventoryCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "i";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Inventory.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Manager = Client.Inventory;
            Inventory = Manager.Store;

            StringBuilder result = new StringBuilder();

            InventoryFolder rootFolder = Inventory.RootFolder;
            PrintFolder(rootFolder, result, 0);

            return result.ToString();
        }

        void PrintFolder(InventoryFolder f, StringBuilder result, int indent)
        {
            List<InventoryBase> contents = Manager.FolderContents(f.UUID, Client.Self.AgentID,
                                               true, true, InventorySortOrder.ByName, 3000);

            if (contents != null)
            {
                foreach (InventoryBase i in contents)
                {
                    result.AppendFormat("{0}{1} ({2})\n", new String(' ', indent * 2), i.Name, i.UUID);
                    if (i is InventoryFolder)
                    {
                        InventoryFolder folder = (InventoryFolder)i;
                        PrintFolder(folder, result, indent + 1);
                    }
                }
            }
        }
    }
}

