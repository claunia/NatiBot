/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : GroupInviteCommand.cs
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
    public class GroupInviteCommand : Command
    {
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        private UUID roleID;
        private UUID GroupRequestID;
        private Dictionary<UUID, GroupRole> Roles;

        public GroupInviteCommand(SecondLifeBot secondLifeBot)
        {
            Name = "invitegroup";
            Description = bot.Localization.clResourceManager.getText("Commands.GroupInvite.Description") + " " + bot.Localization.clResourceManager.getText("Commands.GroupInvite.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.Usage");

            UUID avatarID;
            string avatarName, groupName;
            bool isGroupKey = false;
            Roles = new Dictionary<UUID,GroupRole>();

            if (!UUID.TryParse(args[0], out avatarID))
                return Description;

            roleID = UUID.Zero;

            if (args.Length == 2)
            if (!UUID.TryParse(args[1], out roleID))
                return Description;

            if (Client.Self.ActiveGroup == UUID.Zero)
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.NoGroupActive");

            if (!Client.key2Name(avatarID, out avatarName, out isGroupKey))
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.AvNotFound");

            if (isGroupKey)
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.CannotGroup");

            Client.key2Name(Client.Self.ActiveGroup, out groupName);

            Client.Groups.GroupRoleDataReply += Groups_GroupRoles;                
            GroupRequestID = Client.Groups.RequestGroupRoles(Client.Self.ActiveGroup);
            if (!GroupsEvent.WaitOne(30000, false))
            {
                GroupsEvent.Reset();
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.CannotRoles");
            }
            else
            {
                GroupsEvent.Reset();
            }
            Client.Groups.GroupRoleDataReply -= Groups_GroupRoles;

            if (!Roles.ContainsKey(roleID))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupInvite.NotRole"), roleID);

            List<UUID> inviteRoles = new List<UUID>();

            inviteRoles.Add(roleID);

            GroupRole role;

            if (!Roles.TryGetValue(roleID, out role))
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.ErrorRole");

            Client.Groups.Invite(Client.Self.ActiveGroup, inviteRoles, avatarID);

            return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupInvite.Inviting"), avatarName, groupName, role.Name);
        }

        void Groups_GroupRoles(object sender, GroupRolesDataReplyEventArgs e)
        {
            if (e.RequestID == GroupRequestID)
            {
                Roles = e.Roles;
                GroupsEvent.Set();
            } 
        }
    }
}
