/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ActivateRoleCommand.cs
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
    public class ActivateRoleCommand : Command
    {
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        private string GroupName;
        private UUID GroupUUID;
        private UUID RoleUUID;
        private UUID GroupRequestID;
        private Dictionary<UUID, GroupRole> GroupRoles;

        public ActivateRoleCommand(SecondLifeBot secondLifeBot)
        {
            Name = "activaterole";
            Description = bot.Localization.clResourceManager.getText("Commands.ActivateRole.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ActivateRole.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            bool isGroupKey = false;

            if (args.Length > 2 || args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.ActivateRole.Usage");

            if (args.Length == 2)
            {
                if (!UUID.TryParse(args[1], out GroupUUID))
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.ExpectedGroupID");
            }
            else
            {
                if (Client.Self.ActiveGroup == UUID.Zero)
                    return bot.Localization.clResourceManager.getText("Commands.GroupEject.NoGroupActive");

                GroupUUID = Client.Self.ActiveGroup;
            }

            if (!UUID.TryParse(args[0], out RoleUUID))
                return bot.Localization.clResourceManager.getText("Commands.AddToRole.ExpectedRoleID");

            if (!Client.key2Name(GroupUUID, out GroupName, out isGroupKey))
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.GroupNotFound");
            if (!isGroupKey)
                return bot.Localization.clResourceManager.getText("Commands.GroupEject.AvatarID");

            Client.ReloadGroupsCache();

            if (Client.GroupsCache == null)
                return bot.Localization.clResourceManager.getText("Commands.Groups.CacheFailed");
            if (Client.GroupsCache.Count == 0)
                return bot.Localization.clResourceManager.getText("Commands.Groups.NoGroups");
            if (!Client.GroupsCache.ContainsKey(GroupUUID))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.AddToRole.NotMemberSelf"), GroupName);

            Client.Groups.GroupRoleDataReply += Groups_GroupRoles;                
            GroupRequestID = Client.Groups.RequestGroupRoles(GroupUUID);
            if (!GroupsEvent.WaitOne(30000, false))
            {
                GroupsEvent.Reset();
                Client.Groups.GroupRoleDataReply -= Groups_GroupRoles;
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.CannotRoles");
            }
            else
            {
                GroupsEvent.Reset();
                Client.Groups.GroupRoleDataReply -= Groups_GroupRoles;
            }

            GroupRole chosenRole;

            if (!GroupRoles.TryGetValue(RoleUUID, out chosenRole))
                return bot.Localization.clResourceManager.getText("Commands.GroupInvite.NotRole");

            Client.Groups.ActivateTitle(GroupUUID, RoleUUID);
            return String.Format(bot.Localization.clResourceManager.getText("Commands.ActivateRole.Activating"), chosenRole.Name, GroupName);
        }

        private void Groups_GroupRoles(object sender, GroupRolesDataReplyEventArgs e)
        {
            if (e.RequestID == GroupRequestID)
            {
                GroupRoles = e.Roles;
                GroupsEvent.Set();
            } 
        }
    }
}
