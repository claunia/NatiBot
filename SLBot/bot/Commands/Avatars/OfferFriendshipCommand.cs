﻿/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : OfferFriendshipCommand.cs
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
    using System.Threading;

    public class OfferFriendshipCommand : Command
    {
        public OfferFriendshipCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "offerfriendship";
            base.Description = bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Description") + " " + bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID avatarID = UUID.Zero;
            string avatarName = "";
            bool isGroupKey = false;

            if (args.Length == 1)
            {
                if (!UUID.TryParse(args[0], out avatarID))
                    return bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Usage");

                if (!Client.key2Name(avatarID, out avatarName, out isGroupKey))
                    return bot.Localization.clResourceManager.getText("Commands.OfferFriendship.AvNotFound");

                if (isGroupKey)
                    return bot.Localization.clResourceManager.getText("Commands.OfferFriendship.CannotGroup");
            }
            else if (args.Length == 2)
            {
                avatarName = args[0] + " " + args[1];

                if (!Client.FindOneAvatar(avatarName, out avatarID))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.OfferFriendship.NameNotFound"), avatarName);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Usage");
            }

            if (avatarID != UUID.Zero)
            {
                if (Client.Friends.FriendList.ContainsKey(avatarID))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.OfferFriendship.AlreadyFriend"), avatarName);

                Client.Friends.OfferFriendship(avatarID, bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Message"));

                return String.Format(bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Offered"), avatarName);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.OfferFriendship.Error");
            }
        }
    }
}

