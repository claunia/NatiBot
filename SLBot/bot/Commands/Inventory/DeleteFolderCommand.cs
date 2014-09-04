/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DeleteCommand.cs
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

    public class DeleteCommand : Command
    {
        public DeleteCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "rmdir";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DeleteFolder.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DeleteFolder.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length >= 2)
            {
                // parse the command line
                string target = String.Empty;
                for (int ct = 0; ct < args.Length; ct++)
                    target = target + args[ct] + " ";
                target = target.TrimEnd();

                // initialize results list
                List<InventoryBase> found = new List<InventoryBase>();

                // find the folder
                found = Client.Inventory.LocalFind(Client.Inventory.Store.RootFolder.UUID, target.Split('/'), 0, true);
                if (found.Count.Equals(1))
                {
                    // move the folder to the trash folder
                    Client.Inventory.MoveFolder(found[0].UUID, Client.Inventory.FindFolderForType(AssetType.TrashFolder));
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.DeleteFolder.Deleted"), found[0].Name);
                }
                else
                {
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.DeleteFolder.NotFound"), "");
                }
            }
            return bot.Localization.clResourceManager.getText("Commands.DeleteFolder.Usage");
        }


    }
}

