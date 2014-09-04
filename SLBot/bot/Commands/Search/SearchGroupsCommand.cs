/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchGroupsCommand.cs
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
using System.Text;
using OpenMetaverse;

namespace bot.Commands
{
    class SearchGroupsCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        int resultCount = 0;

        public SearchGroupsCommand(SecondLifeBot secondLifeBot)
        {
            Name = "searchgroups";
            Description = bot.Localization.clResourceManager.getText("Commands.SearchGroups.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchGroups.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            // process command line arguments
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.SearchGroups.Usage");

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();

            waitQuery.Reset();

            Client.Directory.DirGroupsReply += Directory_DirGroups;
            
            // send the request to the directory manager
            Client.Directory.StartGroupSearch(searchText, 0);
            
            string result;
            if (waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result = String.Format(bot.Localization.clResourceManager.getText("Commands.SearchGroups.Matching"), searchText, resultCount);
            }
            else
            {
                result = bot.Localization.clResourceManager.getText("Commands.SearchGroups.Timeout");
            }

            Client.Directory.DirGroupsReply -= Directory_DirGroups;

            return result;
        }

        void Directory_DirGroups(object sender, DirGroupsReplyEventArgs e)
        {
            if (e.MatchedGroups.Count > 0)
            {
                foreach (DirectoryManager.GroupSearchData group in e.MatchedGroups)
                {
                    Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.SearchGroups.Members"), group.GroupID, group.GroupName, group.Members);
                }
            }
            else
            {
                Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.SearchGroups.NotFound"));
            }
            waitQuery.Set();
        }
    }
}
