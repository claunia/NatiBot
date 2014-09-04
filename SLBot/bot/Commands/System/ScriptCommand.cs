﻿/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ScriptCommand.cs
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
    using System.IO;
    using OpenMetaverse;

    public class ScriptCommand : Command
    {
        public ScriptCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "script";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Script.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Script.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Script.Usage");

            // Load the file
            string[] lines;
            try
            {
                lines = File.ReadAllLines(args[0]);
            }
            catch (Exception e)
            {
                return e.Message;
            }

            // Execute all of the commands
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();

                if (line.Length > 0)
                    //Client.ClientManager.DoCommandAll(line, UUID.Zero);
                    Client.DoCommand(line, fromAgentID, fromSL);
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.Script.Done"), lines.Length);
        }
    }
}