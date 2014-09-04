/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : InformFriendCommand.cs
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
    using OpenMetaverse;

    public class InformFriendCommand : Command
    {
        public InformFriendCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "informfriend";
            base.Description = bot.Localization.clResourceManager.getText("Commands.InformFriend.Description") + " " + bot.Localization.clResourceManager.getText("Commands.InformFriend.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length == 0)
            {
                if (Client.InformFriends)
                    return bot.Localization.clResourceManager.getText("Commands.InformFriend.Inform");
                else
                    return bot.Localization.clResourceManager.getText("Commands.InformFriend.NotInform");
            }

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.InformFriend.Usage");

            if (args[0].ToLower() == "on")
            {
                Client.InformFriends = true;
                return bot.Localization.clResourceManager.getText("Commands.InformFriend.WillInform");
            }
            else if (args[0].ToLower() == "off")
            {
                Client.InformFriends = false;
                return bot.Localization.clResourceManager.getText("Commands.InformFriend.WontInform");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.InformFriend.Usage");
            }
        }
    }
}
