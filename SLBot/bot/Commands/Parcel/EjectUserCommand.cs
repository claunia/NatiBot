/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : EjectUserCommand.cs
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
    using OpenMetaverse.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;

    public class EjectUserCommand : Command
    {
        public EjectUserCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "ejectuser";
            Description = bot.Localization.clResourceManager.getText("Commands.EjectUser.Description") + " " + bot.Localization.clResourceManager.getText("Commands.EjectUser.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID avatarID = UUID.Zero;
            string avatarName;
            bool isGroupKey = false;
            int CurrentParcel = Client.Parcels.GetParcelLocalID(Client.Network.CurrentSim, Client.Self.SimPosition);

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.EjectUser.Usage");
            if (!UUID.TryParse(args[0], out avatarID))
                return bot.Localization.clResourceManager.getText("Commands.EjectUser.ExpectedAvID");
            if (!Client.key2Name(avatarID, out avatarName, out isGroupKey))
                return bot.Localization.clResourceManager.getText("Commands.EjectUser.AvNotFound");
            if (isGroupKey)
                return bot.Localization.clResourceManager.getText("Commands.EjectUser.CannotGroup");

            Client.Parcels.EjectUser(avatarID, false);
            return String.Format(bot.Localization.clResourceManager.getText("Commands.EjectUser.Ejecting"), avatarName);
        }
    }
}

