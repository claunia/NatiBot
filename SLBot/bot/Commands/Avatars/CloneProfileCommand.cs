/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CloneProfileCommand.cs
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

    public class CloneProfileCommand : Command
    {
        Avatar.AvatarProperties Properties;
        Avatar.Interests Interests;
        List<UUID> Groups = new List<UUID>();
        bool ReceivedProperties = false;
        bool ReceivedInterests = false;
        bool ReceivedGroups = false;
        ManualResetEvent ReceivedProfileEvent = new ManualResetEvent(false);

        public CloneProfileCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Avatars.AvatarInterestsReply += new EventHandler<AvatarInterestsReplyEventArgs>(Avatars_AvatarInterestsReply);
            SecondLifeBot.Avatars.AvatarPropertiesReply += new EventHandler<AvatarPropertiesReplyEventArgs>(Avatars_AvatarPropertiesReply);
            SecondLifeBot.Avatars.AvatarGroupsReply += new EventHandler<AvatarGroupsReplyEventArgs>(Avatars_AvatarGroupsReply);
            SecondLifeBot.Groups.GroupJoinedReply += new EventHandler<GroupOperationEventArgs>(Groups_OnGroupJoined);
            SecondLifeBot.Avatars.AvatarPicksReply += new EventHandler<AvatarPicksReplyEventArgs>(Avatars_AvatarPicksReply);            

            base.Name = "cloneprofile";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CloneProfile.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CloneProfile.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.CloneProfile.Usage");

            UUID targetID;
            ReceivedProperties = false;
            ReceivedInterests = false;
            ReceivedGroups = false;

            try
            {
                targetID = new UUID(args[0]);
            }
            catch (Exception)
            {
                return bot.Localization.clResourceManager.getText("Commands.CloneProfile.Usage");
            }

            // Request all of the packets that make up an avatar profile
            Client.Avatars.RequestAvatarProperties(targetID);

            // Wait for all the packets to arrive
            ReceivedProfileEvent.Reset();
            ReceivedProfileEvent.WaitOne(5000, false);

            // Check if everything showed up
            if (!ReceivedInterests || !ReceivedProperties || !ReceivedGroups)
                return bot.Localization.clResourceManager.getText("Commands.CloneProfile.Fail");

            // Synchronize our profile
            Client.Self.UpdateInterests(Interests);
            Client.Self.UpdateProfile(Properties);

            // TODO: Leave all the groups we're currently a member of? This could
            // break TestClient connectivity that might be relying on group authentication

            // Attempt to join all the groups
            foreach (UUID groupID in Groups)
            {
                Client.Groups.RequestJoinGroup(groupID);
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.CloneProfile.Done"), targetID.ToString());
        }

        void Avatars_AvatarPropertiesReply(object sender, AvatarPropertiesReplyEventArgs e)
        {
            lock (ReceivedProfileEvent)
            {
                Properties = e.Properties;
                ReceivedProperties = true;

                if (ReceivedInterests && ReceivedProperties && ReceivedGroups)
                    ReceivedProfileEvent.Set();
            }
        }

        void Avatars_AvatarInterestsReply(object sender, AvatarInterestsReplyEventArgs e)
        {
            lock (ReceivedProfileEvent)
            {
                Interests = e.Interests;
                ReceivedInterests = true;

                if (ReceivedInterests && ReceivedProperties && ReceivedGroups)
                    ReceivedProfileEvent.Set();
            }
        }

        void Avatars_AvatarGroupsReply(object sender, AvatarGroupsReplyEventArgs e)
        {
            lock (ReceivedProfileEvent)
            {
                foreach (AvatarGroup group in e.Groups)
                {
                    Groups.Add(group.GroupID);
                }

                ReceivedGroups = true;

                if (ReceivedInterests && ReceivedProperties && ReceivedGroups)
                    ReceivedProfileEvent.Set();
            }
        }

        void Groups_OnGroupJoined(object sender, GroupOperationEventArgs e)
        {
            if (e.Success)
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.CloneProfile.Joined"), e.GroupID.ToString());
            else
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.CloneProfile.FailJoin"), e.GroupID.ToString());

            if (e.Success)
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.CloneProfile.Active"), Client.ToString(),
                    e.GroupID.ToString());
                Client.Groups.ActivateGroup(e.GroupID);
            }
        }

        void Avatars_PickInfoReply(object sender, PickInfoReplyEventArgs e)
        {
            Client.Self.PickInfoUpdate(e.PickID, e.Pick.TopPick, e.Pick.ParcelID, e.Pick.Name, e.Pick.PosGlobal, e.Pick.SnapshotID, e.Pick.Desc);
        }

        void Avatars_AvatarPicksReply(object sender, AvatarPicksReplyEventArgs e)
        {
            foreach (KeyValuePair<UUID, string> kvp in e.Picks)
            {
                if (e.AvatarID == Client.Self.AgentID)
                {
                    Client.Self.PickDelete(kvp.Key);
                }
                else
                {
                    Client.Avatars.RequestPickInfo(e.AvatarID, kvp.Key);
                }
            }
        }
    }
}

