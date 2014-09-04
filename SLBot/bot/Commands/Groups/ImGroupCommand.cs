/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ImGroupCommand.cs
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
    using System.Threading;

    public class ImGroupCommand : Command
    {
        UUID ToGroupID = UUID.Zero;
        ManualResetEvent WaitForSessionStart = new ManualResetEvent(false);

        public ImGroupCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "imgroup";
            base.Description = bot.Localization.clResourceManager.getText("Commands.IMGroup.Description") + " " + bot.Localization.clResourceManager.getText("Commands.IMGroup.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 2)
                return bot.Localization.clResourceManager.getText("Commands.IMGroup.Usage");



            if (UUID.TryParse(args[0], out ToGroupID))
            {
                string message = String.Empty;
                for (int ct = 1; ct < args.Length; ct++)
                    message += args[ct] + " ";
                message = message.TrimEnd();
                if (message.Length > 1023)
                    message = message.Remove(1023);

                Client.Self.GroupChatJoined += Self_GroupChatJoined;
                if (!Client.Self.GroupChatSessions.ContainsKey(ToGroupID))
                {
                    WaitForSessionStart.Reset();
                    Client.Self.RequestJoinGroupChat(ToGroupID);
                }
                else
                {
                    WaitForSessionStart.Set();
                }

                if (WaitForSessionStart.WaitOne(20000, false))
                {
                    Client.Self.InstantMessageGroup(ToGroupID, message);
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.IMGroup.Timeout");
                }

                Client.Self.GroupChatJoined -= Self_GroupChatJoined;
                return String.Format(bot.Localization.clResourceManager.getText("Commands.IMGroup.Success"), ToGroupID.ToString(), message);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.IMGroup.Fail");
            }
        }

        void Self_GroupChatJoined(object sender, GroupChatJoinedEventArgs e)
        {
            if (e.Success)
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.IMGroup.Joined"), e.SessionName);
                WaitForSessionStart.Set();
            }
            else
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.IMGroup.JoinFail"));
            }
        }
    }
}

