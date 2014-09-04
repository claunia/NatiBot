/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GotoCommand.cs
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

    public class GotoCommand : Command
    {
        public GotoCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "goto";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Goto.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Goto.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.Goto.Usage");

            string destination = String.Empty;

            // Handle multi-word sim names by combining the arguments
            foreach (string arg in args)
            {
                destination += arg + " ";
            }
            destination = destination.Trim();

            string[] tokens = destination.Split(new char[] { '/' });
            if (tokens.Length != 4)
                return bot.Localization.clResourceManager.getText("Commands.Goto.Usage");

            string sim = tokens[0];
            float x, y, z;
            if (!float.TryParse(tokens[1], out x) ||
                !float.TryParse(tokens[2], out y) ||
                !float.TryParse(tokens[3], out z))
            {
                return bot.Localization.clResourceManager.getText("Commands.Goto.Usage");
            }

            if (Client.Self.Teleport(sim, new Vector3(x, y, z)))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Goto.Success"), Client.Network.CurrentSim);
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Goto.Fail"), Client.Self.TeleportMessage);
        }
    }
}

