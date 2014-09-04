/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : LeaveGroupCommand.cs
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

    public class LeaveGroupCommand : Command
    {
        ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        Dictionary<UUID, Group> groups = new Dictionary<UUID, Group>();
        private bool leftGroup;

        public LeaveGroupCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "leavegroup";
            base.Description = bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Description") + " " + bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return Description;

            string groupName = String.Empty;
            for (int i = 0; i < args.Length; i++)
                groupName += args[i] + " ";
            groupName = groupName.Trim();

            UUID groupUUID = Client.GroupName2UUID(groupName);
            if (UUID.Zero != groupUUID)
            {                
                Client.Groups.GroupLeaveReply += Groups_GroupLeft;
                Client.Groups.LeaveGroup(groupUUID);

                GroupsEvent.WaitOne(30000, false);
                Client.Groups.GroupLeaveReply -= Groups_GroupLeft;

                GroupsEvent.Reset();
                Client.ReloadGroupsCache();

                if (leftGroup)
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Left"), Client.ToString(),
                        groupName);
                return String.Format(bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Failed"), Client.ToString(),
                    groupName);
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.LeaveGroup.NotInGroup"), Client.ToString(), groupName);
        }

        void Groups_GroupLeft(object sender, GroupOperationEventArgs e)
        {
            if (e.Success)
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Left"), Client.ToString(), e.GroupID.ToString());
            else
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.LeaveGroup.Failed"), Client.ToString(), e.GroupID.ToString());

            leftGroup = e.Success;
            GroupsEvent.Set();
        }
    }
}

