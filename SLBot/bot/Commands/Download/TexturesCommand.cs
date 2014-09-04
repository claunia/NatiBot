/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : TexturesCommand.cs
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

    public class TexturesCommand : Command
    {
        Dictionary<UUID, UUID> alreadyRequested = new Dictionary<UUID, UUID>();
        bool enabled = false;

        public TexturesCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "textures";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Textures.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Textures.Usage");

            enabled = SecondLifeBot.Account.LoginDetails.BotConfig.GetTextures;
            SecondLifeBot.Objects.ObjectUpdate += new EventHandler<PrimEventArgs>(Objects_OnNewPrim);
            SecondLifeBot.Objects.AvatarUpdate += Objects_OnNewAvatar;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Textures.Usage");

            if (args[0].ToLower() == "on")
            {
                enabled = true;
                return bot.Localization.clResourceManager.getText("Commands.Textures.Enabled");
            }
            else if (args[0].ToLower() == "off")
            {
                enabled = false;
                return bot.Localization.clResourceManager.getText("Commands.Textures.Disabled");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Textures.Usage");
            }
        }

        void Objects_OnNewAvatar(object sender, AvatarUpdateEventArgs e)
        {
            if (enabled && base.Client.Account.LoginDetails.BotConfig.GetTextures)
            {
                // Search this avatar for textures
                for (int i = 0; i < e.Avatar.Textures.FaceTextures.Length; i++)
                {
                    Primitive.TextureEntryFace face = e.Avatar.Textures.FaceTextures[i];

                    if (face != null)
                    {
                        if (!alreadyRequested.ContainsKey(face.TextureID))
                        {
                            alreadyRequested[face.TextureID] = face.TextureID;

                            // Determine if this is a baked outfit texture or a normal texture
                            ImageType type = ImageType.Normal;
                            AvatarTextureIndex index = (AvatarTextureIndex)i;
                            switch (index)
                            {
                                case AvatarTextureIndex.EyesBaked:
                                case AvatarTextureIndex.HeadBaked:
                                case AvatarTextureIndex.LowerBaked:
                                case AvatarTextureIndex.SkirtBaked:
                                case AvatarTextureIndex.UpperBaked:
                                    type = ImageType.Baked;
                                    break;
                            }

                            if (!File.Exists("textures/" + face.TextureID + ".jp2"))
                                Client.Assets.RequestImage(face.TextureID, type, Assets_OnImageReceived);
                        }
                    }
                }
            }
        }

        void Objects_OnNewPrim(object sender, PrimEventArgs e)
        {
            Primitive prim = e.Prim;

            if (enabled && base.Client.Account.LoginDetails.BotConfig.GetTextures)
            {
                // Search this prim for textures
                for (int i = 0; i < prim.Textures.FaceTextures.Length; i++)
                {
                    Primitive.TextureEntryFace face = prim.Textures.FaceTextures[i];

                    if (face != null)
                    {
                        if (!alreadyRequested.ContainsKey(face.TextureID))
                        {
                            alreadyRequested[face.TextureID] = face.TextureID;
                            if (!File.Exists("textures/" + face.TextureID + ".jp2"))
                                Client.Assets.RequestImage(face.TextureID, ImageType.Normal, Assets_OnImageReceived);
                        }
                    }
                }
            }
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            if (state == TextureRequestState.Finished && enabled && alreadyRequested.ContainsKey(asset.AssetID))
            {
                if (state == TextureRequestState.Finished)
                {
                    if (!Directory.Exists("textures"))
                        Directory.CreateDirectory("textures");

                    try
                    {
                        File.WriteAllBytes("textures/" + asset.AssetID + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
                    }

                    if (asset.Decode())
                    {
                        try
                        {
                            File.WriteAllBytes("textures/" + asset.AssetID + ".tga", asset.Image.ExportTGA());
                        }
                        catch (Exception ex)
                        {
                            bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
                        }
                    }
                    else
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.FailDecode"), asset.AssetID);
                    }

                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.Downloaded"), asset.AssetID, asset.AssetData.Length);

                }
                else
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.FailDownload"), asset.AssetID, state);
            }
        }
    }
}
