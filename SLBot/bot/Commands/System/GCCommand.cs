/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GCCommand.cs
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
    using System;
    using OpenMetaverse;
    using bot;
    using System.Text;

    class GCCommand : Command
    {
        public GCCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "gc";
            base.Description = bot.Localization.clResourceManager.getText("Commands.GC.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder sbResult = new StringBuilder();
            DateTime Start;

            sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GC.Before"), GC.GetTotalMemory(false) / 1048576).AppendLine();
            sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GC.Starting")).AppendLine();
            Start = DateTime.Now;
            GC.Collect();
            sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GC.After"), GC.GetTotalMemory(true) / 1048576).AppendLine();
            sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GC.Took"), (DateTime.Now - Start).TotalSeconds).AppendLine();

            return sbResult.ToString();
        }
    }
}
