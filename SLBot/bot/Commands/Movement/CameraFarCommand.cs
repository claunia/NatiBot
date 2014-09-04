/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CameraFarCommand.cs
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

    public class CameraFarCommand : Command
    {
        public CameraFarCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "camerafar";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CameraFar.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CameraFar.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            float distance;

            if (args.Length > 1)
                return bot.Localization.clResourceManager.getText("Commands.CameraFar.Usage");

            if (args.Length == 0)
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CameraFar.Actual"), Client.Self.Movement.Camera.Far.ToString());
            }
            else
            {
                if (!float.TryParse(args[0], out distance))
                    return bot.Localization.clResourceManager.getText("Commands.CameraFar.Usage");

                Client.Self.Movement.Camera.Far = distance;

                return String.Format(bot.Localization.clResourceManager.getText("Commands.CameraFar.Changed"), Client.Self.Movement.Camera.Far.ToString());
            }
        }
    }
}

