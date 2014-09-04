/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DumpOutfitCommand.cs
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
Copyright (C) 2007-2010 openmetaverse.org
****************************************************************************/
namespace bot.Commands
{
    using bot;
    using OpenMetaverse;
    using OpenMetaverse.Imaging;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using OpenMetaverse.Assets;

    public class DumpOutfitCommand : Command
    {
        List<UUID> OutfitAssets = new List<UUID>();
        AssetManager.AssetReceivedCallback ImageReceivedHandler;
        private string DestinationDirectory = null;
        ManualResetEvent AppearanceEvent;
        UUID target;

        public DumpOutfitCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "dumpoutfit";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DumpOutfit.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DumpOutfit.Usage");
            //this.ImageReceivedHandler = new AssetManager.ImageReceivedCallback(Assets_OnImageReceived);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            AppearanceEvent = new ManualResetEvent(false);

            DestinationDirectory = null;

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.DumpOutfit.Usage");

            if (!UUID.TryParse(args[0], out target))
                return bot.Localization.clResourceManager.getText("Commands.DumpOutfit.Usage");

            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Avatar targetAv;

                    targetAv = Client.Network.Simulators[i].ObjectsAvatars.Find(
                        delegate(Avatar avatar)
                        {
                            return avatar.ID == target;
                        }
                    );

                    Program.NBStats.AddStatData(String.Format("{0}: {1} dumping outfit of {2}.", DateTime.Now.ToString(), Client, target.ToString()));

                    if (targetAv != null)
                    {
                        StringBuilder output = new StringBuilder(bot.Localization.clResourceManager.getText("Commands.DumpOutfit.Downloading") + " ");

                        lock (OutfitAssets)
                            OutfitAssets.Clear();
                        //Client.Assets.OnImageReceived += ImageReceivedHandler;

                        if (targetAv.Textures == null)
                        {
                            Client.Avatars.AvatarAppearance += new EventHandler<AvatarAppearanceEventArgs>(Avatars_AvatarAppearance);
                            Client.Avatars.RequestAvatarProperties(target);
                            Client.Objects.RequestObject(Client.Network.CurrentSim, targetAv.LocalID);
                            if (!AppearanceEvent.WaitOne(15000, false))
                            {
                                Client.Avatars.AvatarAppearance -= Avatars_AvatarAppearance;
                                return "Unable to get appearance"; //TRANSLATE
                            }
                            Client.Avatars.AvatarAppearance -= Avatars_AvatarAppearance;
                        }

                        if (targetAv.Textures == null)
                            return "Unable to get appearance"; //TRANSLATE

                        for (int j = 0; j < targetAv.Textures.FaceTextures.Length; j++)
                        {
                            Primitive.TextureEntryFace face = targetAv.Textures.FaceTextures[j];

                            if (face != null)
                            {
                                ImageType type = ImageType.Normal;

                                switch ((AvatarTextureIndex)j)
                                {
                                    case AvatarTextureIndex.HeadBaked:
                                    case AvatarTextureIndex.EyesBaked:
                                    case AvatarTextureIndex.UpperBaked:
                                    case AvatarTextureIndex.LowerBaked:
                                    case AvatarTextureIndex.SkirtBaked:
                                        type = ImageType.Baked;
                                        break;
                                }

                                OutfitAssets.Add(face.TextureID);
                                //Client.Assets.RequestImage(face.TextureID, type, 100000.0f, 0);

                                if (DestinationDirectory == null)
                                {

                                    DestinationDirectory = "./outfits/" + targetAv.FirstName + "_" + targetAv.LastName +
                                    System.DateTime.Now.Year.ToString() +
                                    System.DateTime.Now.Month.ToString() +
                                    System.DateTime.Now.Day.ToString() +
                                    System.DateTime.Now.Hour.ToString() +
                                        //System.DateTime.Now.Minute.ToString() +
                                        //System.DateTime.Now.Second.ToString() +
                                    "/";
                                }

                                if (!Directory.Exists(DestinationDirectory))
                                    Directory.CreateDirectory(DestinationDirectory);

                                Client.Assets.RequestImage(face.TextureID, type, Assets_OnImageReceived);
                                //Client.Assets.RequestImage(face.TextureID, type, 100000.0f, 0, 0);

                                output.Append(((AvatarTextureIndex)j).ToString());
                                output.Append(" ");
                            }
                        }

                        return output.ToString();
                    }
                }
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.DumpOutfit.NotFound"), target.ToString());
        }

        private void Avatars_AvatarAppearance(object sender, AvatarAppearanceEventArgs e)
        {
            if (e.AvatarID == target)
                AppearanceEvent.Set();
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {

            if (state == TextureRequestState.Finished /*&& Textures.Contains(asset.AssetID)*/)
            {
                /*lock (Textures)
                    Textures.Remove(asset.AssetID);*/
                // CLAUNIA : Still trying to solve this

                if (state == TextureRequestState.Finished)
                {
                    try
                    {
                        File.WriteAllBytes(DestinationDirectory + asset.AssetID + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
                    }

                    if (asset.Decode())
                    {
                        try
                        {
                            File.WriteAllBytes(DestinationDirectory + asset.AssetID + ".tga", asset.Image.ExportTGA());
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

                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.ImageDownloaded"), asset.AssetID);
                }
                else
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.FailDownload"), asset.AssetID, state);
                }
            }
        }
    }
}

