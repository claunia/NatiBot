/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchEventsCommand.cs
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
namespace bot.Commands.Commands
{
    using bot;
    using bot.Commands;
    using OpenMetaverse;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;

    internal class SearchEventsCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        int resultCount;
        StringBuilder sbResult = new StringBuilder();

        public SearchEventsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "searchevents";
            base.Description = bot.Localization.clResourceManager.getText("Commands.SearchEvents.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchEvents.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            sbResult = new StringBuilder();

            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.SearchEvents.Usage");

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();
            waitQuery.Reset();

            Client.Directory.DirEventsReply += Directory_DirEvents;
            Client.Directory.StartEventsSearch(searchText, 0);

            if (waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.SearchEvents.Matched"), searchText, resultCount);
            }
            else
            {
                sbResult.AppendLine(bot.Localization.clResourceManager.getText("Commands.SearchEvents.Timeout"));
            }
            Client.Directory.DirEventsReply -= Directory_DirEvents;
            return sbResult.ToString();
            ;
        }

        void Directory_DirEvents(object sender, DirEventsReplyEventArgs e)
        {
            if (e.MatchedEvents[0].ID == 0 && e.MatchedEvents.Count == 1)
            {
                sbResult.AppendLine(bot.Localization.clResourceManager.getText("Commands.SearchEvents.NoResults"));
            }
            else
            {
                foreach (DirectoryManager.EventsSearchData ev in e.MatchedEvents)
                {
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.SearchEvents.Event"), ev.ID, ev.Name, ev.Date);
                    sbResult.AppendLine();
                }
            }
            resultCount = e.MatchedEvents.Count;
            waitQuery.Set();
        }
    }
}

