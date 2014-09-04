/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FriendsCommand.cs
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
    using System.Text;
    using System.Threading;

    public class FriendsCommand : Command
    {
        public FriendsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "friends";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Friends.Description");
        }


        /// <summary>
        /// Get a list of current friends
        /// </summary>
        /// <param name="args">optional testClient command arguments</param>
        /// <param name="fromAgentID">The <seealso cref="OpenMetaverse.UUID"/> 
        /// of the agent making the request</param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            // initialize a StringBuilder object used to return the results
            StringBuilder sb = new StringBuilder();

            // Only iterate the Friends dictionary if we actually have friends!
            if (Client.Friends.FriendList.Count > 0)
            {
                // iterate over the InternalDictionary using a delegate to populate
                // our StringBuilder output string
                Client.Friends.FriendList.ForEach(delegate(FriendInfo friend)
                {
                    // append the name of the friend to our output
                    sb.AppendLine(friend.Name + "(UUID: " + friend.UUID.ToString() + ")");
                });
            }
            else
            {
                // we have no friends :(
                sb.AppendLine(bot.Localization.clResourceManager.getText("Commands.Friends.NoFriends"));   
            }

            // return the result
            return sb.ToString();
        }
    }
}

