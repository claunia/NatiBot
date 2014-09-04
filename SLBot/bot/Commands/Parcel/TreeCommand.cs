/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : TreeCommand.cs
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

    public class TreeCommand : Command
    {
        public TreeCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "tree";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Tree.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length == 1)
            {
                try
                {
                    string treeName = args[0].Trim(new char[] { ' ' });
                    Tree tree = (Tree)Enum.Parse(typeof(Tree), treeName);

                    Vector3 treePosition = Client.Self.SimPosition;
                    treePosition.Z += 3.0f;

                    Client.Objects.AddTree(Client.Network.CurrentSim, new Vector3(0.5f, 0.5f, 0.5f),
                        Quaternion.Identity, treePosition, tree, Client.GroupID, false);

                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Tree.Rezzed"), treeName);
                }
                catch (Exception)
                {
                    return bot.Localization.clResourceManager.getText("Commands.Tree.Help");
                }
            }

            string usage = bot.Localization.clResourceManager.getText("Commands.Tree.Usage") + " [";
            foreach (string value in Enum.GetNames(typeof(Tree)))
            {
                usage += value + ",";
            }
            usage = usage.TrimEnd(new char[] { ',' });
            usage += "]";
            return usage;
        }
    }
}

