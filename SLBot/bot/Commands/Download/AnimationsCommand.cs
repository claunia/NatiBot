/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AnimationsCommand.cs
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

    public class AnimationsCommand : Command
    {
        Dictionary<UUID, UUID> alreadyRequested = new Dictionary<UUID, UUID>();
        bool enabled = false;

        public AnimationsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "animations";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Animations.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Animations.Usage");

            enabled = SecondLifeBot.Account.LoginDetails.BotConfig.GetSounds;
            SecondLifeBot.Avatars.AvatarAnimation += new EventHandler<AvatarAnimationEventArgs>(Avatars_AvatarAnimation);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Animations.Usage");

            if (args[0].ToLower() == "on")
            {
                enabled = true;
                return bot.Localization.clResourceManager.getText("Commands.Animations.Enabled");
            }
            else if (args[0].ToLower() == "off")
            {
                enabled = false;
                return bot.Localization.clResourceManager.getText("Commands.Animations.Disabled");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Animations.Usage");
            }
        }

        void Avatars_AvatarAnimation(object sender, AvatarAnimationEventArgs e)
        {
            Dictionary<UUID, string> BuiltInAnimations = Animations.ToDictionary();
            if (enabled && base.Client.Account.LoginDetails.BotConfig.GetSounds)
            {
                foreach (Animation an in e.Animations)
                {
                    if (!BuiltInAnimations.ContainsKey(an.AnimationID))
                    if (!System.IO.File.Exists("./animations/" + an.AnimationID.ToString() + ".animatn"))
                        base.Client.Assets.RequestAsset(an.AnimationID, AssetType.Animation, true, Assets_OnAnimationReceived);
                }
            }
        }

        public void Assets_OnAnimationReceived(AssetDownload transfer, Asset asset)
        {
            if (transfer.Success)
            {
                if (!System.IO.Directory.Exists("./animations"))
                    System.IO.Directory.CreateDirectory("./animations");
                System.IO.File.WriteAllBytes("./animations/" + asset.AssetID.ToString() + ".animatn", asset.AssetData);

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Downloaded"), asset.AssetID.ToString(), asset.AssetData.Length);
            }
            else
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.DownloadAnimation.Failed"), transfer.AssetID, transfer.Status);
            }
        }
    }
}
