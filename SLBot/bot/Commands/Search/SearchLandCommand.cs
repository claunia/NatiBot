/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchLandCommand.cs
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
    public class SearchLandCommand : Command
    {
        private System.Threading.AutoResetEvent waitQuery = new System.Threading.AutoResetEvent(false);
        private StringBuilder result = new StringBuilder();

        /// <summary>
        /// Construct a new instance of the SearchLandCommand
        /// </summary>
        /// <param name="testClient"></param>
        public SearchLandCommand(SecondLifeBot secondLifeBot)
        {
            Name = "searchland";
            Description = bot.Localization.clResourceManager.getText("Commands.SearchLand.Description");
        }

        /// <summary>
        /// Show commandusage
        /// </summary>
        /// <returns>A string containing the parameter usage instructions</returns>
        public string ShowUsage()
        {
            return bot.Localization.clResourceManager.getText("Commands.SearchLand.UsageLine1") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Commands.SearchLand.UsageLine2") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Commands.SearchLand.UsageLine3") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Commands.SearchLand.UsageLine4");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <param name="fromAgentID"></param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            // process command line arguments
            if (args.Length < 3)
                return ShowUsage();

            string searchType = args[0].Trim().ToLower();
            int maxPrice;
            int minSize;

            DirectoryManager.SearchTypeFlags searchTypeFlags = DirectoryManager.SearchTypeFlags.Any;

            if (searchType.StartsWith("au"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Auction;
            else if (searchType.StartsWith("m"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Mainland;
            else if (searchType.StartsWith("e"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Estate;
            else if (searchType.StartsWith("al"))
                searchTypeFlags = DirectoryManager.SearchTypeFlags.Any;
            else
                return ShowUsage();

            // initialize some default flags we'll use in the search
            DirectoryManager.DirFindFlags queryFlags = DirectoryManager.DirFindFlags.SortAsc | DirectoryManager.DirFindFlags.PerMeterSort
                                                       | DirectoryManager.DirFindFlags.IncludeAdult | DirectoryManager.DirFindFlags.IncludePG | DirectoryManager.DirFindFlags.IncludeMature;

            // validate the parameters passed
            if (int.TryParse(args[1], out maxPrice) && int.TryParse(args[2], out minSize))
            {
                // if the [max price] parameter is greater than 0, we'll enable the flag to limit by price
                if (maxPrice > 0)
                    queryFlags |= DirectoryManager.DirFindFlags.LimitByPrice;

                // if the [min size] parameter is greater than 0, we'll enable the flag to limit by area
                if (minSize > 0)
                    queryFlags |= DirectoryManager.DirFindFlags.LimitByArea;
            }
            else
            {
                return ShowUsage();
            }

            //waitQuery.Reset();

            // subscribe to the event that returns the search results
            Client.Directory.DirLandReply += Directory_DirLand;

            // send the request to the directory manager
            Client.Directory.StartLandSearch(queryFlags, searchTypeFlags, maxPrice, minSize, 0);

            if (!waitQuery.WaitOne(20000, false) && Client.Network.Connected)
            {
                result.AppendLine(bot.Localization.clResourceManager.getText("Commands.SearchLand.Timeout"));
            }

            // unsubscribe to the event that returns the search results
            Client.Directory.DirLandReply -= Directory_DirLand;

            // return the results
            return result.ToString();
        }

        /// <summary>
        /// Process the search reply
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Directory_DirLand(object sender, DirLandReplyEventArgs e)
        {

            foreach (DirectoryManager.DirectoryParcel searchResult in e.DirParcels)
            {
                // add the results to the StringBuilder object that contains the results
                result.AppendLine(searchResult.ToString());
            }
            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.SearchLand.Results") + System.Environment.NewLine, e.DirParcels.Count);
            // let the calling method know we have data
            waitQuery.Set();
        }
    }
}
