/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : JoinGroupCommand.cs
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

    public class JoinGroupCommand : Command
    {
        ManualResetEvent GetGroupsSearchEvent = new ManualResetEvent(false);
        private UUID queryID = UUID.Zero;
        private UUID resolvedGroupID;
        private string groupName;
        private string resolvedGroupName;
        private bool joinedGroup;

        public JoinGroupCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "joingroup";
            base.Description = bot.Localization.clResourceManager.getText("Commands.JoinGroup.Description") + " " + bot.Localization.clResourceManager.getText("Commands.JoinGroup.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return Description;

            groupName = String.Empty;
            resolvedGroupID = UUID.Zero;
            resolvedGroupName = String.Empty;

            if (args.Length < 2)
            {
                if (!UUID.TryParse((resolvedGroupName = groupName = args[0]), out resolvedGroupID))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.JoinGroup.InvalidUUID"), resolvedGroupName);
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                    groupName += args[i] + " ";
                groupName = groupName.Trim();

                Client.Directory.DirGroupsReply += Directory_DirGroups;

                queryID = Client.Directory.StartGroupSearch(groupName, 0);

                GetGroupsSearchEvent.WaitOne(60000, false);

                Client.Directory.DirGroupsReply -= Directory_DirGroups;

                GetGroupsSearchEvent.Reset();
            }

            if (resolvedGroupID == UUID.Zero)
            {
                if (string.IsNullOrEmpty(resolvedGroupName))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.JoinGroup.UUIDNotFound"), groupName);
                else
                    return resolvedGroupName;
            }

            Client.Groups.GroupJoinedReply += Groups_OnGroupJoined;
            Client.Groups.RequestJoinGroup(resolvedGroupID);

            /* A.Biondi 
             * TODO: implement the pay to join procedure.
             */

            GetGroupsSearchEvent.WaitOne(60000, false);

            Client.Groups.GroupJoinedReply -= Groups_OnGroupJoined;
            GetGroupsSearchEvent.Reset();

            if (joinedGroup)
                return String.Format(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Joined"), resolvedGroupName);
            return String.Format(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Failed"), resolvedGroupName);
        }

        void Groups_OnGroupJoined(object sender, GroupOperationEventArgs e)
        {
            if (e.Success)
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Joined"), e.GroupID.ToString());
                joinedGroup = true;
            }
            else
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Failed"), e.GroupID.ToString());
                joinedGroup = false;
            }
        }

        void Directory_DirGroups(object sender, DirGroupsReplyEventArgs e)
        {
            if (queryID == e.QueryID)
            {
                queryID = UUID.Zero;
                if (e.MatchedGroups.Count < 1)
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Empty"));
                }
                else
                {
                    if (e.MatchedGroups.Count > 1)
                    {
                        /* A.Biondi 
                         * The Group search doesn't work as someone could expect...
                         * It'll give back to you a long list of groups even if the 
                         * searchText (groupName) matches esactly one of the groups 
                         * names present on the server, so we need to check each result.
                         * UUIDs of the matching groups are written on the console.
                         */
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Matching") + System.Environment.NewLine);
                        foreach (DirectoryManager.GroupSearchData groupRetrieved in e.MatchedGroups)
                        {
                            bot.Console.WriteLine(groupRetrieved.GroupName + "\t\t\t(" +
                            Name + " UUID " + groupRetrieved.GroupID.ToString() + ")");

                            if (groupRetrieved.GroupName.ToLower() == groupName.ToLower())
                            {
                                resolvedGroupID = groupRetrieved.GroupID;
                                resolvedGroupName = groupRetrieved.GroupName;
                                break;
                            }
                        }
                        if (string.IsNullOrEmpty(resolvedGroupName))
                            resolvedGroupName = String.Format(bot.Localization.clResourceManager.getText("Commands.JoinGroup.Ambigous"), e.MatchedGroups.Count.ToString());
                    }

                }
                GetGroupsSearchEvent.Set();
            }
        }
    }
}

