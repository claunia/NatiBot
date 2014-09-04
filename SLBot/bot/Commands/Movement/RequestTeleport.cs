/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : RequestTeleport.cs
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
using System;
using OpenMetaverse;

namespace bot.Core.Commands
{
    public class RequestTeleport : bot.Commands.Command
    {
        public RequestTeleport(SecondLifeBot SecondLifeBot)
        {
            Client = SecondLifeBot;
            Name = "sendtp";
            Description = bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Description") + " " + bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool unknown)
        {
            UUID targetID;
            string targetName;

            if (args.Length == 0)
            {
                targetID = Client.MasterKey;
            }
            else if (args.Length == 1)
            {
                if (!UUID.TryParse(args[0], out targetID))
                    return bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Usage");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Usage");
            }

            if (!Client.key2Name(targetID, out targetName))
                targetName = targetID.ToString();

            Client.Self.SendTeleportLure(targetID, bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Message"));

            return String.Format(bot.Localization.clResourceManager.getText("Commands.RequestTeleport.Sent"), targetName);
        }
    }
}