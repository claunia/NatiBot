/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DownloadSoundCommand.cs
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
    using System.Threading;
    using OpenMetaverse;
    using OpenMetaverse.Assets;

    public class DownloadSoundCommand : Command
    {
        AutoResetEvent DownloadHandle = new AutoResetEvent(false);
        string resultState;

        public DownloadSoundCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "downloadsound";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DownloadSound.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DownloadSound.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            UUID SoundID;

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.DownloadSound.Usage");

            DownloadHandle.Reset();

            if (UUID.TryParse(args[0], out SoundID))
            {
                Program.NBStats.AddStatData(String.Format("{0}: {1} downloading sound {2}.", DateTime.Now.ToString(), Client, args[0]));

                base.Client.Assets.RequestAsset(SoundID, AssetType.Sound, true, Assets_OnSoundReceived);

                if (DownloadHandle.WaitOne(120 * 1000, false))
                {
                    return resultState;
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.DownloadSound.Timeout");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.InvalidUUID");
            }
        }

        public void Assets_OnSoundReceived(AssetDownload transfer, Asset asset)
        {
            if (transfer.Success)
            {
                if (!System.IO.Directory.Exists("./sounds"))
                    System.IO.Directory.CreateDirectory("./sounds");
                System.IO.File.WriteAllBytes("./sounds/" + asset.AssetID.ToString() + ".ogg", asset.AssetData);

                resultState = String.Format(bot.Localization.clResourceManager.getText("Commands.Sounds.Downloaded"), asset.AssetID.ToString(), asset.AssetData.Length);
            }
            else
            {
                resultState = String.Format(bot.Localization.clResourceManager.getText("Commands.Sounds.Failed"), transfer.AssetID, transfer.Status);
            }
            DownloadHandle.Set();
        }
    }
}
