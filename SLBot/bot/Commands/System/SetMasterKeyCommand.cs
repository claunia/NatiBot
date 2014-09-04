/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SetMasterKeyCommand.cs
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

    public class SetMasterKeyCommand : Command
    {
        public DateTime Created = DateTime.Now;

        public SetMasterKeyCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "setMasterKey";
            base.Description = bot.Localization.clResourceManager.getText("Commands.SetMasterKey.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SetMasterKey.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Client.MasterKey = UUID.Parse(args[0]);

            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Avatar master = Client.Network.Simulators[i].ObjectsAvatars.Find(
                                        delegate(Avatar avatar)
                        {
                            return avatar.ID == Client.MasterKey;
                        }
                                    );

                    if (master != null)
                    {
                        Client.Self.InstantMessage(master.ID,
                            bot.Localization.clResourceManager.getText("Commands.SetMaster.Greet"));
                        break;
                    }
                }
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.SetMasterKey.Set"), Client.MasterKey.ToString());
        }
    }
}

