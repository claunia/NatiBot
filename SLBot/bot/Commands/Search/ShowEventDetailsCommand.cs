/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ShowEventDetailsCommand.cs
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
    using System.Text;
    using System.Threading;

    internal class ShowEventDetailsCommand : Command
    {
        StringBuilder sb = new StringBuilder();
        ManualResetEvent detailsEvent = new ManualResetEvent(false);

        public ShowEventDetailsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "showevent";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            sb = new StringBuilder();
            detailsEvent.Reset();

            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Usage");

            Client.Directory.EventInfoReply += Directory_EventDetails;
            uint eventID;

            if (UInt32.TryParse(args[0], out eventID))
            {
                Client.Directory.EventInfoRequest(eventID);
                sb.AppendLine(bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Query"));
                detailsEvent.WaitOne(15000, false);
                return sb.ToString();
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Usage");
            }
        }

        void Directory_EventDetails(object sender, EventInfoReplyEventArgs e)
        {
            float x, y;
            Helpers.GlobalPosToRegionHandle((float)e.MatchedEvent.GlobalPos.X, (float)e.MatchedEvent.GlobalPos.Y, out x, out y);
            sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Name") + System.Environment.NewLine, e.MatchedEvent.Name, e.MatchedEvent.ID);
            sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Location") + System.Environment.NewLine, e.MatchedEvent.SimName, x, y);
            sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.Date") + System.Environment.NewLine, e.MatchedEvent.Date);
            sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ShowEventDetails.EventDescription") + System.Environment.NewLine, e.MatchedEvent.Desc);
            Client.Directory.EventInfoReply -= Directory_EventDetails;
            detailsEvent.Set();
        }
    }
}

