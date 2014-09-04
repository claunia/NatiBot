/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BanListCommand.cs
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
    using OpenMetaverse.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;

    public class BanListCommand : Command
    {
        private AutoResetEvent BanListDownload = new AutoResetEvent(false);
        private List<ParcelManager.ParcelAccessEntry> BanList;

        public BanListCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "banlist";
            Description = bot.Localization.clResourceManager.getText("Commands.BanList.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder sb = new StringBuilder();

            int CurrentParcel = Client.Parcels.GetParcelLocalID(Client.Network.CurrentSim, Client.Self.SimPosition);

            Client.Parcels.ParcelAccessListReply += new EventHandler<ParcelAccessListReplyEventArgs>(Parcels_ParcelAccessListReply);
            Client.Parcels.RequestParcelAccessList(Client.Network.CurrentSim, CurrentParcel, AccessList.Ban, 0);

            if (!BanListDownload.WaitOne(15000, false))
            {
                Client.Parcels.ParcelAccessListReply -= Parcels_ParcelAccessListReply;
                BanListDownload.Reset();
                return bot.Localization.clResourceManager.getText("Commands.BanList.Timeout");
            }
            else
            {
                Client.Parcels.ParcelAccessListReply -= Parcels_ParcelAccessListReply;
                BanListDownload.Reset();
            }

            foreach (ParcelManager.ParcelAccessEntry entry in BanList)
            {
                string avatarName;

                if (!Client.key2Name(entry.AgentID, out avatarName))
                    sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.BanList.BannedID"), entry.AgentID).AppendLine();
                else
                    sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.BanList.Banned"), avatarName, entry.AgentID).AppendLine();
            }

            return sb.ToString();
        }

        void Parcels_ParcelAccessListReply(object sender, ParcelAccessListReplyEventArgs e)
        {
            BanList = e.AccessList;
            BanListDownload.Set();
        }
    }
}

