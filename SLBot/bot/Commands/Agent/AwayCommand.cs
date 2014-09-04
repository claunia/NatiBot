/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AwayCommand.cs
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

    public class AwayCommand : Command
    {
        private static readonly UUID AWAYID = new UUID("FD037134-85D4-F241-72C6-4F42164FEDEE");

        public AwayCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "away";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Away.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Away.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length == 0)
            {
                if (Client.isAway)
                    return bot.Localization.clResourceManager.getText("Commands.Away.Afk");
                else
                    return bot.Localization.clResourceManager.getText("Commands.Away.NotAfk");
            }

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Away.Usage");

            if (args[0].ToLower() == "on")
            {
                Client.isAway = true;
                Client.Self.AnimationStart(AWAYID, true);
                return bot.Localization.clResourceManager.getText("Commands.Away.WillAfk");
            }
            else if (args[0].ToLower() == "off")
            {
                Client.isAway = false;
                Client.Self.AnimationStop(AWAYID, true);
                return bot.Localization.clResourceManager.getText("Commands.Away.WontAfk");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Away.Usage");
            }
        }
    }
}
