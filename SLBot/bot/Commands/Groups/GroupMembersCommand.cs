/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GroupMembersCommand.cs
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
using System.Collections.Generic;
using System.Threading;
using OpenMetaverse;
using OpenMetaverse.Packets;
using System.Text;

namespace bot.Commands
{
    public class GroupMembersCommand : Command
    {
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        private string GroupName;
        private UUID GroupUUID;
        private UUID GroupRequestID;
        StringBuilder sb;

        public GroupMembersCommand(SecondLifeBot secondLifeBot)
        {
            Name = "groupmembers";
            Description = bot.Localization.clResourceManager.getText("Commands.GroupMembers.Description") + " " + bot.Localization.clResourceManager.getText("Commands.GroupMembers.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.GroupMembers.Usage");

            sb = new StringBuilder();

            GroupName = String.Empty;
            for (int i = 0; i < args.Length; i++)
                GroupName += args[i] + " ";
            GroupName = GroupName.Trim();

            GroupUUID = Client.GroupName2UUID(GroupName);
            if (UUID.Zero != GroupUUID)
            {                
                Client.Groups.GroupMembersReply += GroupMembersHandler;                
                GroupRequestID = Client.Groups.RequestGroupMembers(GroupUUID);
                GroupsEvent.WaitOne(30000, false);
                GroupsEvent.Reset();
                Client.Groups.GroupMembersReply -= GroupMembersHandler;
                return sb.ToString();
                
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupMembers.NotMember"), Client.ToString(), GroupName);
        }

        private void GroupMembersHandler(object sender, GroupMembersReplyEventArgs e)
        {
            if (e.RequestID == GroupRequestID)
            {
                sb.AppendLine();
                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupMembers.GotMembers"), Client.ToString()).AppendLine();
                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupRoles.RequestID"), e.RequestID).AppendLine();
                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupRoles.GroupName"), GroupName).AppendLine();
                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupRoles.GroupID"), GroupUUID).AppendLine();
                if (e.Members.Count > 0)
                    foreach (KeyValuePair<UUID, GroupMember> member in e.Members)
                    {
                        string MemberName;

                        if (!Client.key2Name(member.Key, out MemberName))
                            MemberName = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Unknown");

                        sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupMembers.Member"), MemberName, member.Key.ToString()).AppendLine();
                        
                    }

                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.GroupMembers.MemberCount"), e.Members.Count).AppendLine();
                GroupsEvent.Set();
            } 
        }
    }
}
