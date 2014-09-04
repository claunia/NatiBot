/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SoundsCommand.cs
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
    using System.Collections.Generic;

    public class SoundsCommand : Command
    {
        Dictionary<UUID, UUID> alreadyRequested = new Dictionary<UUID, UUID>();
        bool enabled = false;

        public SoundsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "sounds";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Sounds.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Textures.Usage");

            enabled = SecondLifeBot.Account.LoginDetails.BotConfig.GetSounds;
            SecondLifeBot.Sound.SoundTrigger += new EventHandler<SoundTriggerEventArgs>(Sound_SoundTrigger);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Sounds.Usage");

            if (args[0].ToLower() == "on")
            {
                enabled = true;
                return bot.Localization.clResourceManager.getText("Commands.Sounds.Enabled");
            }
            else if (args[0].ToLower() == "off")
            {
                enabled = false;
                return bot.Localization.clResourceManager.getText("Commands.Sounds.Disabled");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Sounds.Usage");
            }
        }

        void Sound_SoundTrigger(object sender, SoundTriggerEventArgs e)
        {
            if (enabled && base.Client.Account.LoginDetails.BotConfig.GetTextures)
            {
#if DEBUG
                bot.Console.WriteLine(this.Client, "GETTING SOUND: Gain: {0}, Object: {1}, Owner: {2}, Parent: {3}, Position: {4}, Region: {5}, ID: {6}",
                    e.Gain.ToString(), e.ObjectID.ToString(), e.OwnerID.ToString(), e.ParentID.ToString(),
                    e.Position.ToString(), e.RegionHandle.ToString(), e.SoundID.ToString());
#endif
                if (!System.IO.File.Exists("./sounds/" + e.SoundID.ToString() + ".ogg"))
                    base.Client.Assets.RequestAsset(e.SoundID, AssetType.Sound, true, Assets_OnSoundReceived);
            }
        }

        public void Assets_OnSoundReceived(AssetDownload transfer, Asset asset)
        {
            if (transfer.Success)
            {
                if (!System.IO.Directory.Exists("./sounds"))
                    System.IO.Directory.CreateDirectory("./sounds");
                System.IO.File.WriteAllBytes("./sounds/" + asset.AssetID.ToString() + ".ogg", asset.AssetData);

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Sounds.Downloaded"), asset.AssetID.ToString(), asset.AssetData.Length);
            }
            else
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Sounds.Failed"), transfer.AssetID, transfer.Status);
            }
        }
    }
}
