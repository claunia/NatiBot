/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ChangeDirectoryCommand.cs
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

    public class ChangeDirectoryCommand : Command
    {
        private InventoryManager Manager;
        private OpenMetaverse.Inventory Inventory;

        public ChangeDirectoryCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "cd";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            Manager = Client.Inventory;
            Inventory = Client.Inventory.Store;

            if (args.Length > 1)
                return bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.Usage");
            string pathStr = "";
            string[] path = null;
            if (args.Length == 0)
            {
                path = new string[] { "" };
                // cd without any arguments doesn't do anything.
            }
            else if (args.Length == 1)
            {
                pathStr = args[0];
                path = pathStr.Split(new char[] { '/' });
                // Use '/' as a path seperator.
            }
            InventoryFolder currentFolder = Client.CurrentDirectory;
            if (pathStr.StartsWith("/"))
                currentFolder = Inventory.RootFolder;

            if (currentFolder == null) // We need this to be set to something. 
                //return "Error: Sesión no iniciada.";
                currentFolder = Inventory.RootFolder;

            // Traverse the path, looking for the 
            for (int i = 0; i < path.Length; ++i)
            {
                string nextName = path[i];
                if (string.IsNullOrEmpty(nextName) || nextName == ".")
                    continue; // Ignore '.' and blanks, stay in the current directory.
                if (nextName == ".." && currentFolder != Inventory.RootFolder)
                {
                    // If we encounter .., move to the parent folder.
                    currentFolder = Inventory[currentFolder.ParentUUID] as InventoryFolder;
                }
                else
                {
                    List<InventoryBase> currentContents = Inventory.GetContents(currentFolder);
                    // Try and find an InventoryBase with the corresponding name.
                    bool found = false;
                    foreach (InventoryBase item in currentContents)
                    {
                        // Allow lookup by UUID as well as name:
                        if (item.Name == nextName || item.UUID.ToString() == nextName)
                        {
                            found = true;
                            if (item is InventoryFolder)
                            {
                                currentFolder = item as InventoryFolder;
                            }
                            else
                            {
                                return String.Format(bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.NotFolder"), item.Name);
                            }
                        }
                    }
                    if (!found)
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.NotFound"), nextName, currentFolder.Name);
                }
            }
            Client.CurrentDirectory = currentFolder;
            return String.Format(bot.Localization.clResourceManager.getText("Commands.ChangeDirectory.CurrentFolder"), currentFolder.Name);
        }


    }
}
