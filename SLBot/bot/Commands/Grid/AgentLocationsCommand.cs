/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AgentLocationsCommand.cs
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
    using System.Text;

    public class AgentLocationsCommand : Command
    {
        public AgentLocationsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "agentlocations";
            base.Description = bot.Localization.clResourceManager.getText("Commands.AgentLocations.Description") + " " + bot.Localization.clResourceManager.getText("Commands.AgentLocations.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            ulong regionHandle;

            if (args.Length == 0)
                regionHandle = Client.Network.CurrentSim.Handle;
            else if (!(args.Length == 1 && UInt64.TryParse(args[0], out regionHandle)))
                return bot.Localization.clResourceManager.getText("Commands.AgentLocations.Usage");

            List<MapItem> items = Client.Grid.MapItems(regionHandle, GridItemType.AgentLocations,
                                      GridLayerType.Objects, 1000 * 20);

            if (items != null)
            {
                StringBuilder ret = new StringBuilder();
                ret.AppendLine(bot.Localization.clResourceManager.getText("Commands.AgentLocations.Locations"));

                for (int i = 0; i < items.Count; i++)
                {
                    MapAgentLocation location = (MapAgentLocation)items[i];

                    ret.AppendLine(String.Format(bot.Localization.clResourceManager.getText("Commands.AgentLocations.Avatar"), location.AvatarCount, location.LocalX,
                        location.LocalY));
                }

                return ret.ToString();
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.AgentLocations.Fail");
            }
        }
    }
}

