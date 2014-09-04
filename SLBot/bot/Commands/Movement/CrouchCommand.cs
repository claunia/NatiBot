/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CrouchCommand.cs
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

    public class CrouchCommand : Command
    {
        public CrouchCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "crouch";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Crouch.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Crouch.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            bool start = true;

            if (args.Length == 1 && args[0].ToLower() == "stop")
                start = false;

            if (start)
            {
                Client.Self.Crouch(true);
                return bot.Localization.clResourceManager.getText("Commands.Crouch.Started");
            }
            else
            {
                Client.Self.Crouch(false);
                return bot.Localization.clResourceManager.getText("Commands.Crouch.Stopped");
            }
        }
    }
}

