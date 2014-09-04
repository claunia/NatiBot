/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : UptimeCommand.cs
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

    public class UptimeCommand : Command
    {
        public DateTime Created = DateTime.Now;

        public UptimeCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "uptime";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Uptime.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            string name = Client.ToString();
            return String.Format(bot.Localization.clResourceManager.getText("Commands.Uptime.Uptime"), name, Created, (DateTime.Now - Created));
        }
    }
}

