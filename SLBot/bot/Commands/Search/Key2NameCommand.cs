/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Key2NameCommand.cs
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
    using System.Threading;

    public class Key2NameCommand : Command
    {
        ManualResetEvent WaitforAvatar = new ManualResetEvent(false);
        UUID avatarID;
        string avatarName;

        public Key2NameCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "key2name";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Key2Name.Description") + " " +
            bot.Localization.clResourceManager.getText("Commands.Key2Name.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            WaitforAvatar = new ManualResetEvent(false);
            bool isGroupID = false;

            avatarID = UUID.Zero;
            avatarName = "";

            if (!UUID.TryParse(args[0], out avatarID))
                return bot.Localization.clResourceManager.getText("Commands.Key2Name.Usage");

            if (base.Client.key2Name(avatarID, out avatarName, out isGroupID))
            if (!isGroupID)
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Key2Name.Found"), avatarID, avatarName);
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Key2Name.Group"), avatarID, avatarName);
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Key2Name.Timeout"), avatarID);
        }
    }
}



