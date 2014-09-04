/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : WindCommand.cs
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
    using System.Collections.Generic;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using bot;

    class WindCommand : Command
    {
        public WindCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "wind";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Wind.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            // Get the agent's current "patch" position, where each patch of
            // wind data is a 16x16m square
            Vector3 agentPos = Client.Self.SimPosition;
            int xPos = (int)Utils.Clamp(agentPos.X, 0.0f, 255.0f) / 16;
            int yPos = (int)Utils.Clamp(agentPos.Y, 0.0f, 255.0f) / 16;

            Vector2 windSpeed = Client.Terrain.WindSpeeds[Client.Network.CurrentSim.Handle][yPos * 16 + xPos];

            return String.Format(bot.Localization.clResourceManager.getText("Commands.Wind.Speed"), windSpeed.ToString());
        }
    }
}
