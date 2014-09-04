/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : TouchCommand.cs
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

    public class TouchCommand : Command
    {
        public TouchCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "touch";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Touch.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Touch.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID target;

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Touch.Usage");

            if (UUID.TryParse(args[0], out target))
            {
                Primitive targetPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                                           delegate(Primitive prim)
                    {
                        return prim.ID == target;
                    }
                                       );

                if (targetPrim != null)
                {
                    Client.Self.Touch(targetPrim.LocalID);
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Touch.Touched"), targetPrim.LocalID);
                }
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.Touch.NotFound"), args[0]);
        }
    }
}

