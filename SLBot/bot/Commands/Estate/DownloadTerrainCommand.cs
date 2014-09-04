/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DownloadTerrainCommand.cs
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
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Threading;
    using OpenMetaverse;

    public class DownloadTerrainCommand : Command
    {
        /// <summary>
        /// Create a Synchronization event object
        /// </summary>
        private static AutoResetEvent xferTimeout = new AutoResetEvent(false);

        /// <summary>A string we use to report the result of the request with.</summary>
        private static System.Text.StringBuilder result = new System.Text.StringBuilder();

        private static string fileName;

        /// <summary>
        /// Download a simulators raw terrain data and save it to a file
        /// </summary>
        /// <param name="testClient"></param>
        public DownloadTerrainCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "downloadterrain";
            Description = bot.Localization.clResourceManager.getText("Commands.DownloadTerrain.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DownloadTerrain.Usage");
        }

        /// <summary>
        /// Execute the application
        /// </summary>
        /// <param name="args">arguments passed to this module</param>
        /// <param name="fromAgentID">The ID of the avatar sending the request</param>
        /// <returns></returns>
        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            int timeout = 120000; // default the timeout to 2 minutes
            fileName = "terrain/" + Client.Network.CurrentSim.Name + ".raw";

            if (args.Length > 0 && int.TryParse(args[0], out timeout) != true)
                return bot.Localization.clResourceManager.getText("Commands.DownloadTerrain.Usage");

            // Create a delegate which will be fired when the simulator receives our download request
            // Starts the actual transfer request
            AssetManager.InitiateDownloadCallback initiateDownloadDelegate = delegate(string simFilename, string viewerFileName)
            {
                Client.Assets.RequestAssetXfer(simFilename, false, false, UUID.Zero, AssetType.Unknown, false);
            };

            // Subscribe to the event that will tell us the status of the download
            Client.Assets.OnXferReceived += new AssetManager.XferReceivedCallback(Assets_OnXferReceived);

            // subscribe to the event which tells us when the simulator has received our request
            Client.Assets.OnInitiateDownload += initiateDownloadDelegate;

            // configure request to tell the simulator to send us the file
            List<string> parameters = new List<string>();
            parameters.Add("download filename");
            parameters.Add(fileName);
            // send the request
            Client.Estate.EstateOwnerMessage("terrain", parameters);

            // wait for (timeout) seconds for the request to complete (defaults 2 minutes)
            if (!xferTimeout.WaitOne(timeout, false))
            {
                result.Append(bot.Localization.clResourceManager.getText("Commands.DownloadTerrain.Timeout"));
            }

            // unsubscribe from events
            Client.Assets.OnInitiateDownload -= initiateDownloadDelegate;
            Client.Assets.OnXferReceived -= new AssetManager.XferReceivedCallback(Assets_OnXferReceived);

            // return the result
            return result.ToString();
        }

        /// <summary>
        /// Handle the reply to the OnXferReceived event
        /// </summary>
        /// <param name="xfer"></param>
        private void Assets_OnXferReceived(XferDownload xfer)
        {
            if (xfer.Success)
            {
                // set the result message
                result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.DownloadTerrain.Success"), xfer.Filename, xfer.Size, fileName);

                // write the file to disk
                FileStream stream = new FileStream(fileName, FileMode.Create);
                BinaryWriter w = new BinaryWriter(stream);
                w.Write(xfer.AssetData);
                w.Close();

                // tell the application we've gotten the file
                xferTimeout.Set();

            }
        }
    }
}
