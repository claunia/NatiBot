/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GroupEjectCommand.cs
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
    public class GroupEjectCommand : Command
    {
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        private string GroupName;
        private UUID GroupUUID;
        private UUID GroupRequestID;
        private Dictionary<UUID, GroupMember> GroupMembers;

        public GroupEjectCommand(SecondLifeBot secondLifeBot)
        {
            Name = "groupeject";
            Description = bot.Localization.clResourceManager.getText("Commands.GroupEject.Description") + " " + bot.Localization.clResourceManager.getText("Commands.GroupEject.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID avatarID;
            bool isGroupKey;
            string avatarName;
            GroupMembers = new Dictionary<UUID, GroupMember>();
            GroupsEvent.Reset();

            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.Usage");

            if (args.Length == 2)
            {
                if (!UUID.TryParse(args[1], out GroupUUID))
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.ExpectedGroupID");

                if (!Client.key2Name(GroupUUID, out GroupName, out isGroupKey))
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.GroupNotFound");
                if (!isGroupKey)
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.AvatarID");
            }
            else
            {
                if (Client.Self.ActiveGroup == UUID.Zero)
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.NoGroupActive");

                GroupUUID = Client.Self.ActiveGroup;
            }

            if (!UUID.TryParse(args[0], out avatarID))
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.ExpectedAvatarID");

            if (!Client.key2Name(avatarID, out avatarName, out isGroupKey))
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.AvatarNotFound");
            if (isGroupKey)
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.GroupID");

            Client.Groups.GroupMembersReply += GroupMembersHandler;                
            GroupRequestID = Client.Groups.RequestGroupMembers(GroupUUID);
            if (!GroupsEvent.WaitOne(30000, false))
            {
                Client.Groups.GroupMembersReply -= GroupMembersHandler;
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.ErrorMembers");
            }

            Client.Groups.GroupMembersReply -= GroupMembersHandler;

            if (!GroupMembers.ContainsKey(avatarID))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupEject.NotMember"), avatarName, GroupName);

            Client.Groups.EjectUser(GroupUUID, avatarID);
            return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupEject.Ejected"), avatarName, GroupName);
        }

        private void GroupMembersHandler(object sender, GroupMembersReplyEventArgs e)
        {
            if (e.RequestID == GroupRequestID)
            {
                GroupMembers = e.Members;
                GroupsEvent.Set();
            } 
        }
    }
}
