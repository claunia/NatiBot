/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ImCommand.cs
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
    using System.Threading;

    public class ImCommand : Command
    {
        string ToAvatarName = String.Empty;
        ManualResetEvent NameSearchEvent = new ManualResetEvent(false);
        Dictionary<string, UUID> Name2Key = new Dictionary<string, UUID>();

        public ImCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Avatars.AvatarPickerReply += Avatars_AvatarPickerReply;
            base.Name = "im";
            base.Description = bot.Localization.clResourceManager.getText("Commands.IM.Description") + " " + bot.Localization.clResourceManager.getText("Commands.IM.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 3)
                return bot.Localization.clResourceManager.getText("Commands.IM.Usage");

            ToAvatarName = args[0] + " " + args[1];

            // Build the message
            string message = String.Empty;
            for (int ct = 2; ct < args.Length; ct++)
                message += args[ct] + " ";
            message = message.TrimEnd();
            if (message.Length > 1023)
                message = message.Remove(1023);

            if (!Name2Key.ContainsKey(ToAvatarName.ToLower()))
            {
                // Send the Query
                Client.Avatars.RequestAvatarNameSearch(ToAvatarName, UUID.Random());

                NameSearchEvent.WaitOne(6000, false);
            }

            if (Name2Key.ContainsKey(ToAvatarName.ToLower()))
            {
                UUID id = Name2Key[ToAvatarName.ToLower()];

                Client.Self.InstantMessage(id, message);

                bot.Chat.structInstantMessage sim;
                InstantMessage im = new InstantMessage();

                im.Message = message;
                im.FromAgentID = id;
                im.FromAgentName = ToAvatarName;
                im.Dialog = InstantMessageDialog.MessageFromAgent;

                sim.client = this.Client;
                sim.isReceived = false;
                sim.message = im;
                sim.simulator = this.Client.Network.CurrentSim;
                sim.timestamp = DateTime.Now;

                bot.Chat.receivedIM(sim);

                return String.Format(bot.Localization.clResourceManager.getText("Commands.IM.Success"), id.ToString(), message);
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.IM.LookupFail"), ToAvatarName);
            }
        }

        void Avatars_AvatarPickerReply(object sender, AvatarPickerReplyEventArgs e)
        {
            foreach (KeyValuePair<UUID, string> kvp in e.Avatars)
            {
                if (kvp.Value.ToLower() == ToAvatarName.ToLower())
                {
                    Name2Key[ToAvatarName.ToLower()] = kvp.Key;
                    NameSearchEvent.Set();
                    return;
                }
            }
        }
    }
}

