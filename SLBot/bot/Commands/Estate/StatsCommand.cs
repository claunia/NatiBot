/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : StatsCommand.cs
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

    public class StatsCommand : Command
    {
        public StatsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "stats";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Stats.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder output = new StringBuilder();

            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Simulator sim = Client.Network.Simulators[i];

                    output.AppendLine(String.Format(
                        bot.Localization.clResourceManager.getText("Commands.Stats.Info1"),
                        sim.ToString(), sim.Stats.Dilation, sim.Stats.IncomingBPS, sim.Stats.OutgoingBPS,
                        sim.Stats.ResentPackets, sim.Stats.ReceivedResends));
                }
            }

            Simulator csim = Client.Network.CurrentSim;

            output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Stats.Packets"), Client.Network.InboxCount);
            output.AppendLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Stats.Info2"),
                csim.Stats.FPS, csim.Stats.PhysicsFPS, csim.Stats.AgentUpdates, csim.Stats.Objects, csim.Stats.ScriptedObjects));
            output.AppendLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Stats.Info3"),
                csim.Stats.FrameTime, csim.Stats.NetTime, csim.Stats.ImageTime, csim.Stats.PhysicsTime, csim.Stats.ScriptTime, csim.Stats.OtherTime));
            output.AppendLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Stats.Info4"),
                csim.Stats.Agents, csim.Stats.ChildAgents, csim.Stats.ActiveScripts));

            return output.ToString();
        }
    }
}

