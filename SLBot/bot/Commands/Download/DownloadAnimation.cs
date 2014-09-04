/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DownloadAnimationCommand.cs
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
    using OpenMetaverse.Packets;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.IO;
    using OpenMetaverse.Assets;

    public class DownloadAnimationCommand : Command
    {
        string downloadResult;
        System.Threading.AutoResetEvent waitEvent = new System.Threading.AutoResetEvent(false);

        public DownloadAnimationCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "downloadanimation";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            UUID AnimationUUID;
            
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Usage");

            if (!UUID.TryParse(args[0], out AnimationUUID))
            {
                return bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.InvalidUUID");
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} downloading animation {2}.", DateTime.Now.ToString(), Client, args[0]));

            base.Client.Assets.RequestAsset(AnimationUUID, AssetType.Animation, true, Assets_OnAnimationReceived);

            if (!waitEvent.WaitOne(10000, false))
            {
                return bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Timeout");
            }
            else
            {
                return downloadResult;
            }
        }

        public void Assets_OnAnimationReceived(AssetDownload transfer, Asset asset)
        {
            if (transfer.Success)
            {
                if (!System.IO.Directory.Exists("./animations"))
                    System.IO.Directory.CreateDirectory("./animations");
                System.IO.File.WriteAllBytes("./animations/" + asset.AssetID.ToString() + ".animatn", asset.AssetData);

                downloadResult = String.Format(bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Downloaded"), asset.AssetID.ToString(), asset.AssetData.Length);
            }
            else
            {
                downloadResult = String.Format(bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Failed"), transfer.AssetID, transfer.Status);
            }
            waitEvent.Set();
        }
    }
}

