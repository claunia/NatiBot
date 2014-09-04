/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SearchBuildableCommand.cs
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
/*

namespace bot.Commands
{
    using bot;
    using OpenMetaverse;
    using System;
    using System.Threading;
    using System.Collections.Generic;

	internal class SearchBuildableCommand : Command
	{
        private AutoResetEvent ParcelsDownloaded = new AutoResetEvent(false);
        private int ParcelCount = 0;

        public SearchBuildableCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "searchbuildable";
            base.Description = bot.Localization.clResourceManager.getText("Commands.SearchBuildable.Description") + " " + bot.Localization.clResourceManager.getText("Commands.SearchBuildable.Usage");

            SecondLifeBot.Network.Disconnected += new EventHandler<DisconnectedEventArgs> Network_OnDisconnected;
	    }


        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            String result = "";

            ParcelManager.SimParcelsDownloaded del = delegate(Simulator simulator, InternalDictionary<int, Parcel> simParcels, int[,] parcelMap)
            {
                
                ParcelCount = simParcels.Count;

                simParcels.ForEach(delegate(Parcel parcel)
                {
                    if(parcel.Flags == ParcelFlags.CreateObjects)
                    {
                        base.Client.sendIRCMessage(String.Format(
                            bot.Localization.clResourceManager.getText("Commands.SearchBuildable.Found"),
                            parcel.Name,
                            simulator.Name
                        ));
                    }
                });
                ParcelsDownloaded.Set();

            };

            ParcelsDownloaded.Reset();
            Client.Parcels.OnSimParcelsDownloaded += del;
            Client.Parcels.RequestAllSimParcels(Client.Network.CurrentSim);

            if (ParcelsDownloaded.WaitOne(20000, false) && Client.Network.Connected)
                result = bot.Localization.clResourceManager.getText("Commands.SearchBuildable.Ready");
            else
                result = bot.Localization.clResourceManager.getText("Commands.SearchBuildable.Failed");

            Client.Parcels.OnSimParcelsDownloaded -= del;
            return result;
        }

        void Network_OnDisconnected(NetworkManager.DisconnectType reason, string message)
        {
            ParcelsDownloaded.Set();
        }
    }
}
*/