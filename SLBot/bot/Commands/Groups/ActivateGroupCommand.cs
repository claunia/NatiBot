/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ActivateGroupCommand.cs
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
    using OpenMetaverse.Packets;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    public class ActivateGroupCommand : Command
    {
        ManualResetEvent GroupsEvent = new ManualResetEvent(false);
        string activeGroup;

        public ActivateGroupCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "activategroup";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ActivateGroup.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ActivateGroup.Usage");
        }

        private void AgentDataUpdateHandler(object sender, PacketReceivedEventArgs e)
        {
            AgentDataUpdatePacket p = (AgentDataUpdatePacket)e.Packet;
            if (p.AgentData.AgentID == Client.Self.AgentID)
            {
                activeGroup = Utils.BytesToString(p.AgentData.GroupName) + " ( " + Utils.BytesToString(p.AgentData.GroupTitle) + " )";
                GroupsEvent.Set();
            }
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            bool isGroupID = false;

            if (args.Length < 1)
                return Description;

            activeGroup = string.Empty;

            string groupName = String.Empty;
            for (int i = 0; i < args.Length; i++)
                groupName += args[i] + " ";
            groupName = groupName.Trim();

            Client.Groups.RequestCurrentGroups();

            GroupsEvent.Reset();

            string realGroupName = "";

            UUID groupUUID = Client.GroupName2UUID(groupName);
            if (UUID.Zero != groupUUID)
            {
                EventHandler<PacketReceivedEventArgs> pcallback = AgentDataUpdateHandler;
                Client.Network.RegisterCallback(PacketType.AgentDataUpdate, pcallback);

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.ActivateGroup.Setting"), groupName);
                Client.Groups.ActivateGroup(groupUUID);
                GroupsEvent.WaitOne(30000, false);

                Client.Network.UnregisterCallback(PacketType.AgentDataUpdate, pcallback);
                GroupsEvent.Reset();

                /* A.Biondi 
                         * TODO: Handle titles choosing.
                         */

                Client.key2Name(groupUUID, out realGroupName, out isGroupID);

                if (!isGroupID)
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.GroupEject.AvatarID"), groupUUID);

                if (realGroupName == "")
                    realGroupName = groupUUID.ToString();

                if (String.IsNullOrEmpty(activeGroup))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.ActivateGroup.Failed"), Client.ToString(),
                        realGroupName);

                return String.Format(bot.Localization.clResourceManager.getText("Commands.ActivateGroup.Active"), Client.ToString(),
                    realGroupName);
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.ActivateGroup.NotInGroup"), Client.ToString(),
                realGroupName);

            return String.Format(bot.Localization.clResourceManager.getText("Commands.ActivateGroup.NoGroups"), Client.ToString());
        }
    }
}

