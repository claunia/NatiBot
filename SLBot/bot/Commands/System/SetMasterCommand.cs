/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SetMasterCommand.cs
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
    using System.Collections.Generic;
    using System;
    using System.Threading;

    public class SetMasterCommand : Command
    {
        public DateTime Created = DateTime.Now;
        private UUID resolvedMasterKey = UUID.Zero;
        private ManualResetEvent keyResolution = new ManualResetEvent(false);
        private UUID query = UUID.Zero;

        public SetMasterCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "setmaster";
            base.Description = bot.Localization.clResourceManager.getText("Commands.SetMaster.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SetMaster.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            string masterName = String.Empty;
            for (int ct = 0; ct < args.Length; ct++)
                masterName = masterName + args[ct] + " ";
            masterName = masterName.TrimEnd();

            if (masterName.Length == 0)
                return bot.Localization.clResourceManager.getText("Commands.SetMaster.Usage");

            EventHandler<DirPeopleReplyEventArgs> callback = KeyResolvHandler;
            Client.Directory.DirPeopleReply += callback;

            query = Client.Directory.StartPeopleSearch(masterName, 0);

            if (keyResolution.WaitOne(TimeSpan.FromMinutes(1), false))
            {
                Client.MasterKey = resolvedMasterKey;
                keyResolution.Reset();
                Client.Directory.DirPeopleReply -= callback;
            }
            else
            {
                keyResolution.Reset();
                Client.Directory.DirPeopleReply -= callback;
                return String.Format(bot.Localization.clResourceManager.getText("Commands.SetMaster.UUIDNotFound"), masterName);
            }

            // Send an Online-only IM to the new master
            Client.Self.InstantMessage(
                Client.MasterKey, bot.Localization.clResourceManager.getText("Commands.SetMaster.Greet"));

            return String.Format(bot.Localization.clResourceManager.getText("Commands.SetMaster.Set"), masterName, Client.MasterKey.ToString());
        }

        private void KeyResolvHandler(object sender, DirPeopleReplyEventArgs e)
        {
            if (query != e.QueryID)
                return;

            resolvedMasterKey = e.MatchedPeople[0].AgentID;
            keyResolution.Set();
            query = UUID.Zero;
        }
    }
}

