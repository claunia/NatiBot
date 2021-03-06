/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : PlaySoundCommand.cs
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
using System;
using OpenMetaverse;
using System.Collections.Generic;
using System.Text;

namespace bot.Core.Commands
{
    public class PlaySoundCommand : bot.Commands.Command
    {
        private Dictionary<UUID, string> m_BuiltInAnimations = new Dictionary<UUID, string>(Animations.ToDictionary());

        public PlaySoundCommand(SecondLifeBot SecondLifeBot)
        {
            Client = SecondLifeBot;
            Name = "playsound";
            Description = bot.Localization.clResourceManager.getText("Commands.PlaySound.Description") + " " + bot.Localization.clResourceManager.getText("Commands.PlaySound.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool from_SL)
        {
            UUID soundID = UUID.Zero;

            if (args.Length == 1)
            {
                if (UUID.TryParse(args[0], out soundID))
                {
                    if (soundID == UUID.Zero)
                        return bot.Localization.clResourceManager.getText("Commands.PlaySound.Usage");

                    Client.Sound.PlaySound(soundID);
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.PlaySound.Playing"), soundID);
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.PlaySound.Usage");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.PlaySound.Usage");
            }
        }
    }
}
