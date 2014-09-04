/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchClassifiedsCommand.cs
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
    class SearchClassifiedsCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);

        public SearchClassifiedsCommand(SecondLifeBot secondLifeBot)
        {
            Name = "searchclassifieds";
            Description = bot.Localization.clResourceManager.getText("Commands.SearchClassifieds.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchClassifieds.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.SearchClassifieds.Usage");

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();
            waitQuery.Reset();

            StringBuilder result = new StringBuilder();

            EventHandler<DirClassifiedsReplyEventArgs> callback = delegate(object sender, DirClassifiedsReplyEventArgs e)
            {
                result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.SearchClassifieds.Results") + System.Environment.NewLine,
                    searchText, e.Classifieds.Count);
                foreach (DirectoryManager.Classified ad in e.Classifieds)
                {
                    result.AppendLine(ad.ToString());
                }

                // classifieds are sent 16 ads at a time
                if (e.Classifieds.Count < 16)
                {
                    waitQuery.Set();
                }
            };

            Client.Directory.DirClassifiedsReply += callback;

            UUID searchID = Client.Directory.StartClassifiedSearch(searchText, DirectoryManager.ClassifiedCategories.Any, DirectoryManager.ClassifiedQueryFlags.Mature | DirectoryManager.ClassifiedQueryFlags.PG);

            if (!waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result.AppendLine(bot.Localization.clResourceManager.getText("Commands.SearchClassifieds.Timeout"));
            }

            Client.Directory.DirClassifiedsReply -= callback;

            return result.ToString();
        }
    }
}
