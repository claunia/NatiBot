/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : UploadRawTerrainCommand.cs
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

    public class UploadRawTerrainCommand : Command
    {
        System.Threading.AutoResetEvent WaitForUploadComplete = new System.Threading.AutoResetEvent(false);

        /// <summary>
        /// Download a simulators raw terrain data and save it to a file
        /// </summary>
        /// <param name="testClient"></param>
        public UploadRawTerrainCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "uploadterrain";
            Description = bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.Description") + " " + bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string fileName = String.Empty;

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.Usage");


            fileName = args[0];

            if (!System.IO.File.Exists(fileName))
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.FileNotFound"), fileName);
            }

            // Setup callbacks for upload request reply and progress indicator 
            // so we can detect when the upload is complete
            Client.Assets.OnUploadProgress += new AssetManager.UploadProgressCallback(Assets_OnUploadProgress);

            byte[] fileData = File.ReadAllBytes(fileName);

            Client.Estate.UploadTerrain(fileData, fileName);

            // Wait for upload to complete. Upload request is fired in callback from first request
            if (!WaitForUploadComplete.WaitOne(120000, false))
            {
                Cleanup();
                return bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.Timeout");
            }
            else
            {
                Cleanup();
                return bot.Localization.clResourceManager.getText("Commands.UploadRawTerrain.Success");
            }
        }

        /// <summary>
        /// Unregister previously subscribed event handlers
        /// </summary>
        private void Cleanup()
        {
            Client.Assets.OnUploadProgress -= new AssetManager.UploadProgressCallback(Assets_OnUploadProgress);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="upload"></param>
        void Assets_OnUploadProgress(AssetUpload upload)
        {
            if (upload.Transferred == upload.Size)
            {
                WaitForUploadComplete.Set();
            }
            else
            {
                //Console.WriteLine("Progress: {0}/{1} {2}/{3} {4}", upload.XferID, upload.ID, upload.Transferred, upload.Size, upload.Success);
                bot.Console.WriteLine(".");
            }
        }
    }
}
