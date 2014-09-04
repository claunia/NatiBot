/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ParcelInfoCommand.cs
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

    public class ParcelInfoCommand : Command
    {
        private AutoResetEvent ParcelsDownloaded = new AutoResetEvent(false);

        public ParcelInfoCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "parcelinfo";
            Description = bot.Localization.clResourceManager.getText("Commands.ParcelInfo.Description");

            SecondLifeBot.Network.Disconnected += Network_OnDisconnected;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder sb = new StringBuilder();
            string result;

            EventHandler<SimParcelsDownloadedEventArgs> del = delegate(object sender, SimParcelsDownloadedEventArgs e)
            {
                ParcelsDownloaded.Set();
            };

            ParcelsDownloaded.Reset();
            Client.Parcels.SimParcelsDownloaded += del;
            Client.Parcels.RequestAllSimParcels(Client.Network.CurrentSim);

            if (Client.Network.CurrentSim.IsParcelMapFull())
                ParcelsDownloaded.Set();

            if (ParcelsDownloaded.WaitOne(60000, false) && Client.Network.Connected)
            {
                sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ParcelInfo.Downloaded") + System.Environment.NewLine,
                    Client.Network.CurrentSim.Parcels.Count, Client.Network.CurrentSim.Name);

                Client.Network.CurrentSim.Parcels.ForEach(delegate(Parcel parcel)
                {
                    sb.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ParcelInfo.Parcel") + System.Environment.NewLine,
                        parcel.LocalID, parcel.Name, parcel.Desc, parcel.AccessWhiteList.Count, parcel.Dwell);
                });

                result = sb.ToString();
            }
            else
                result = bot.Localization.clResourceManager.getText("Commands.ParcelInfo.Failed");

            Client.Parcels.SimParcelsDownloaded -= del;
            return result;
        }

        void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            ParcelsDownloaded.Set();
        }
    }
}

