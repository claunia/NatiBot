/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : HelpCommand.cs
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
    using System.Text;
    using System.Collections.Generic;

    public class HelpCommand : Command
    {
        public HelpCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "help";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Help.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            builder.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Help.Message"), new object[0]).AppendLine();

            string[] entries = new string[base.Client.Commands.Values.Count];

#if DEBUG
            bool doHTML = false;

            if (args.Length == 1)
            if (args[0].ToLower() == "html")
                doHTML = true;
#endif

            int i = 0;
            foreach (Command command in base.Client.Commands.Values)
            {
#if DEBUG
                if (doHTML)
                    entries[i] = string.Format("<p> * <span class=\"command\">{0}</span> - {1}</p>\n", command.Name, command.Description);
                else
                    entries[i] = string.Format(" * {0} - {1}\n", command.Name, command.Description);
#else
                entries[i] = string.Format(" * {0} - {1}\n", command.Name, command.Description);
#endif
                i++;
            }

            Array.Sort(entries);
            
            foreach (string entry in entries)
            {
                builder.Append(entry);
            }

            return builder.ToString();
        }
    }
}

