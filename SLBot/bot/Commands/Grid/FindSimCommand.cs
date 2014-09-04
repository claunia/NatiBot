/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FindSimCommand.cs
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

    public class FindSimCommand : Command
    {
        public FindSimCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "findsim";
            base.Description = bot.Localization.clResourceManager.getText("Commands.FindSim.Description") + " " + bot.Localization.clResourceManager.getText("Commands.FindSim.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.FindSim.Usage");

            // Build the simulator name from the args list
            string simName = string.Empty;
            for (int i = 0; i < args.Length; i++)
                simName += args[i] + " ";
            simName = simName.TrimEnd().ToLower();

            //if (!GridDataCached[Client])
            //{
            //    Client.Grid.RequestAllSims(GridManager.MapLayerType.Objects);
            //    System.Threading.Thread.Sleep(5000);
            //    GridDataCached[Client] = true;
            //}

            GridRegion region;

            if (Client.Grid.GetGridRegion(simName, GridLayerType.Objects, out region))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.FindSim.Info"), region.Name, region.RegionHandle, region.X, region.Y);
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.FindSim.LookupFail"), simName);
        }
    }
}

