/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : LeftCommand.cs
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

    public class LeftCommand : Command
    {
        public LeftCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "left";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Left.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Left.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length > 1)
                return bot.Localization.clResourceManager.getText("Commands.Left.Usage");

            if (args.Length == 0)
            {
                Client.Self.Movement.SendManualUpdate(AgentManager.ControlFlags.AGENT_CONTROL_LEFT_POS, Client.Self.Movement.Camera.Position,
                    Client.Self.Movement.Camera.AtAxis, Client.Self.Movement.Camera.LeftAxis, Client.Self.Movement.Camera.UpAxis,
                    Client.Self.Movement.BodyRotation, Client.Self.Movement.HeadRotation, Client.Self.Movement.Camera.Far, AgentFlags.None,
                    AgentState.None, true);
            }
            else
            {
                // Parse the number of seconds
                int duration;
                if (!Int32.TryParse(args[0], out duration))
                    return bot.Localization.clResourceManager.getText("Commands.Left.Usage");
                // Convert to milliseconds
                duration *= 1000;

                int start = Environment.TickCount;

                Client.Self.Movement.LeftPos = true;

                while (Environment.TickCount - start < duration)
                {
                    // The movement timer will do this automatically, but we do it here as an example
                    // and to make sure updates are being sent out fast enough
                    Client.Self.Movement.SendUpdate(false);
                    System.Threading.Thread.Sleep(100);
                }

                Client.Self.Movement.LeftPos = false;
            }

            return bot.Localization.clResourceManager.getText("Commands.Left.Moved");
        }


    }
}
