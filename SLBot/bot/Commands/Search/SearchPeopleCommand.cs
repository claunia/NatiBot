/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchPeopleCommand.cs
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
    class SearchPeopleCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        int resultCount = 0;

        public SearchPeopleCommand(SecondLifeBot secondLifeBot)
        {
            Name = "searchpeople";
            Description = bot.Localization.clResourceManager.getText("Commands.SearchPeople.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchPeople.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            // process command line arguments
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.SearchPeople.Usage");

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();

            waitQuery.Reset();

            
            Client.Directory.DirPeopleReply += Directory_DirPeople;

            // send the request to the directory manager
            Client.Directory.StartPeopleSearch(searchText, 0);
            
            string result;
            if (waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result = String.Format(bot.Localization.clResourceManager.getText("Commands.SearchPeople.Matching"), searchText, resultCount);
            }
            else
            {
                result = bot.Localization.clResourceManager.getText("Commands.SearchPeople.Timeout");
            }

            Client.Directory.DirPeopleReply -= Directory_DirPeople;

            return result;
        }

        void Directory_DirPeople(object sender, DirPeopleReplyEventArgs e)
        {
            if (e.MatchedPeople.Count > 0)
            {
                foreach (DirectoryManager.AgentSearchData agent in e.MatchedPeople)
                {
                    Console.WriteLine("{0} {1} ({2})", agent.FirstName, agent.LastName, agent.AgentID);                   
                }
            }
            else
            {
                Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.SearchPeople.NotFound"));
            }
            waitQuery.Set();
        }
    }
}
