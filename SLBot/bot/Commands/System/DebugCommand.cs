/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DebugCommand.cs
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

    /*
    public class DebugCommand : Command
    {
        public DebugCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "debug";
            base.Description = "Turn debug messages on or off. Usage: debug [on/off]";
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length == 1)
            {
                if (args[0].ToLower() == "on")
                {
                    base.Client.Settings.DEBUG = true;
                    return "Debug logging is on";
                }
                if (args[0].ToLower() == "off")
                {
                    base.Client.Settings.DEBUG = false;
                    return "Debug logging is off";
                }
            }
            return "Usage: debug [on/off]";
        }
    }*/
}

