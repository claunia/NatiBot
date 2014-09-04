/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SitCommand.cs
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

    public class SitCommand : Command
    {
        public SitCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "sit";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Sit.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Primitive closest = null;
            double closestDistance = Double.MaxValue;

            Client.Network.CurrentSim.ObjectsPrimitives.ForEach(
                delegate(Primitive prim)
                {
                    float distance = Vector3.Distance(Client.Self.SimPosition, prim.Position);

                    if (closest == null || distance < closestDistance)
                    {
                        closest = prim;
                        closestDistance = distance;
                    }
                }
            );

            if (closest != null)
            {
                Client.Self.RequestSit(closest.ID, Vector3.Zero);
                Client.Self.Sit();

                return String.Format(bot.Localization.clResourceManager.getText("Commands.Sit.Sit"), closest.ID, closest.LocalID, closestDistance);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Sit.NotFound");
            }
        }
    }
}

