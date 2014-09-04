/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DownloadTextureCommand.cs
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

    public class DownloadTextureCommand : Command
    {
        UUID TextureID;
        AutoResetEvent DownloadHandle = new AutoResetEvent(false);
        TextureRequestState resultState;
        AssetTexture Asset;

        public DownloadTextureCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "downloadtexture";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Usage");

            //SecondLifeBot.Assets.OnImageReceiveProgress += new AssetManager.ImageReceiveProgressCallback(Assets_OnImageReceiveProgress);
            //SecondLifeBot.Assets.OnImageReceived += new AssetManager.ImageReceivedCallback(Assets_OnImageReceived);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Usage");

            if (args.Length != 1 && args.Length != 2)
                return bot.Localization.clResourceManager.getText("Commands.DownloadTexture.UsageLong");

            TextureID = UUID.Zero;
            DownloadHandle.Reset();
            Asset = null;

            if (UUID.TryParse(args[0], out TextureID))
            {
                int discardLevel = 0;

                if (args.Length > 1)
                {
                    if (!Int32.TryParse(args[1], out discardLevel))
                        return bot.Localization.clResourceManager.getText("Commands.DownloadTexture.UsageLong");
                }

                Program.NBStats.AddStatData(String.Format("{0}: {1} downloading texture {2}.", DateTime.Now.ToString(), Client, args[0]));

                Client.Assets.RequestImage(TextureID, ImageType.Normal, Assets_OnImageReceived);

                if (DownloadHandle.WaitOne(120 * 1000, false))
                {
                    if (resultState == TextureRequestState.Finished)
                    {
                        if (Asset != null && Asset.Decode())
                        {
                            if (!Directory.Exists("textures/"))
                                Directory.CreateDirectory("textures/");

                            try
                            {
                                File.WriteAllBytes("textures/" + Asset.AssetID + ".jp2", Asset.AssetData);
                            }
                            catch (Exception ex)
                            {
                                bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client, ex);
                            }

                            try
                            {
                                File.WriteAllBytes("textures/" + Asset.AssetID + ".tga", Asset.Image.ExportTGA());
                            }
                            catch (Exception ex)
                            {
                                bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
                            }

                            return String.Format(bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Saved"), Asset.AssetID, Asset.Image.Width, Asset.Image.Height);
                        }
                        else
                        {
                            return String.Format(bot.Localization.clResourceManager.getText("Assets.Image.FailDecode"), TextureID.ToString());
                        }
                    }
                    else if (resultState == TextureRequestState.NotFound)
                    {
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.DownloadTexture.NotFound"), TextureID.ToString());
                    }
                    else
                    {
                        return String.Format(bot.Localization.clResourceManager.getText("Assets.Image.FailDownload"), TextureID, resultState);
                    }
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Timeout");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.DownloadTexture.Usage");
            }
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            resultState = state;
            Asset = asset;

            DownloadHandle.Set();
        }

        /*private void Assets_OnImageReceiveProgress(UUID image, int lastPacket, int recieved, int total)
        {
            bot.Console.WriteLine(String.Format("Textura {0}: Recibidos {1} / {2} bytes", image, recieved, total));
        }*/
    }
}
