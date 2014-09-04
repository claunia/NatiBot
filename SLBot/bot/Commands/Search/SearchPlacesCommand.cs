/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchPlacesCommand.cs
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
    class SearchPlacesCommand : Command
    {
        System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);

        public SearchPlacesCommand(SecondLifeBot secondLifeBot)
        {
            Name = "searchplaces";
            Description = bot.Localization.clResourceManager.getText("Commands.SearchPlaces.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchPlaces.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.SearchPlaces.Usage");

            string searchText = string.Empty;
            for (int i = 0; i < args.Length; i++)
                searchText += args[i] + " ";
            searchText = searchText.TrimEnd();
            waitQuery.Reset();

            StringBuilder result = new StringBuilder();
         
            EventHandler<PlacesReplyEventArgs> callback = delegate(object sender, PlacesReplyEventArgs e)
            {
                result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.SearchPlaces.Results") + System.Environment.NewLine,
                    searchText, e.MatchedPlaces.Count);
                foreach (DirectoryManager.PlacesSearchData place in e.MatchedPlaces)
                {
                    result.AppendLine(place.ToString());
                }

                waitQuery.Set();
            };

            Client.Directory.PlacesReply += callback;
            Client.Directory.StartPlacesSearch(searchText);            

            if (!waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result.AppendLine(bot.Localization.clResourceManager.getText("Commands.SearchPlaces.Timeout"));
            }

            Client.Directory.PlacesReply -= callback;

            return result.ToString();
        }
    }
}
