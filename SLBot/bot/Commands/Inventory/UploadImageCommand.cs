/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : UploadImageCommand.cs
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
    using OpenMetaverse.Imaging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Drawing;
    using System.IO;

    public class UploadImageCommand : Command
    {
        AutoResetEvent UploadCompleteEvent = new AutoResetEvent(false);
        UUID TextureID = UUID.Zero;
        DateTime start;
        System.Text.StringBuilder returnString = new System.Text.StringBuilder();

        public UploadImageCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "uploadimage";
            base.Description = bot.Localization.clResourceManager.getText("Commands.UploadImage.Description") + " " + bot.Localization.clResourceManager.getText("Commands.UploadImage.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length >= 2)
            {
                string inventoryName;
                string fileName;

                if (args.Length != 2)
                    return bot.Localization.clResourceManager.getText("Commands.UploadImage.Usage");

                TextureID = UUID.Zero;
                inventoryName = args[0];
                fileName = args[1];
                
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.UploadImage.Loading"), fileName);
                byte[] jpeg2k = LoadImage(fileName);
                if (jpeg2k == null)
                    return bot.Localization.clResourceManager.getText("Commands.UploadImage.FailedConvert");
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.UploadImage.Uploading"));
                start = DateTime.Now;
                DoUpload(jpeg2k, inventoryName);

                if (UploadCompleteEvent.WaitOne(10000, false))
                {
                    return returnString.ToString();
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.UploadImage.Timeout");
                }
            }
            return bot.Localization.clResourceManager.getText("Commands.UploadImage.Usage");
        }

        private void DoUpload(byte[] UploadData, string FileName)
        {
            if (UploadData != null)
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(FileName);
                string desc = String.Format(bot.Localization.clResourceManager.getText("Commands.UploadImage.CreatedBy"), FileName, DateTime.Now);

                Client.Inventory.RequestCreateItemFromAsset(UploadData, name, desc,
                    AssetType.Texture, InventoryType.Texture, Client.Inventory.FindFolderForType(AssetType.Texture),

                   /* delegate(CapsClient client, long bytesReceived, long bytesSent, long totalBytesToReceive, long totalBytesToSend)
                    {
                        if (bytesSent > 0)
                            bot.Console.WriteLine(String.Format("Textura subida: {0} / {1}", bytesSent, totalBytesToSend));
                    },*/
                   // CLAUNIA: Seems that libomv changes nulled this functionality

                    delegate(bool success, string status, UUID itemID, UUID assetID)
                    {
                        bot.Console.WriteLine(String.Format(
                            bot.Localization.clResourceManager.getText("Commands.UploadImage.Returned"),
                            success, status, itemID, assetID));

                        bot.Console.WriteLine(String.Format(bot.Localization.clResourceManager.getText("Commands.UploadImage.UploadTook"), DateTime.Now.Subtract(start)));

                        if (!success)
                        {
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.UploadImage.FailedUpload"), status);
                            returnString.AppendLine();
                        }
                        else
                        {
                            Client.Inventory.GiveItem(itemID, FileName, AssetType.Texture, Client.MasterKey, true);
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.UploadImage.UploadedUUID"), assetID);
                            returnString.AppendLine();
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.UploadImage.ImageSent"), Client.MasterName, Client.MasterKey);
                            returnString.AppendLine();
                        }

                        TextureID = assetID;
                        
                        UploadCompleteEvent.Set();
                    }
                );
            }
        }

        private byte[] LoadImage(string fileName)
        {
            byte[] UploadData;
            string lowfilename = fileName.ToLower();
            Bitmap bitmap = null;

            try
            {
                if (lowfilename.EndsWith(".jp2") || lowfilename.EndsWith(".j2c"))
                {
                    Image image;
                    ManagedImage managedImage;

                    // Upload JPEG2000 images untouched
                    UploadData = System.IO.File.ReadAllBytes(fileName);
                    
                    OpenJPEG.DecodeToImage(UploadData, out managedImage, out image);
                    bitmap = (Bitmap)image;
                }
                else
                {
                    if (lowfilename.EndsWith(".tga"))
                        bitmap = LoadTGAClass.LoadTGA(fileName);
                    else
                        bitmap = (Bitmap)System.Drawing.Image.FromFile(fileName);

                    int oldwidth = bitmap.Width;
                    int oldheight = bitmap.Height;

                    if (!IsPowerOfTwo((uint)oldwidth) || !IsPowerOfTwo((uint)oldheight))
                    {
                        Bitmap resized = new Bitmap(256, 256, bitmap.PixelFormat);
                        Graphics graphics = Graphics.FromImage(resized);

                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.InterpolationMode =
                           System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(bitmap, 0, 0, 256, 256);

                        bitmap.Dispose();
                        bitmap = resized;

                        oldwidth = 256;
                        oldheight = 256;
                    }

                    // Handle resizing to prevent excessively large images
                    if (oldwidth > 1024 || oldheight > 1024)
                    {
                        int newwidth = (oldwidth > 1024) ? 1024 : oldwidth;
                        int newheight = (oldheight > 1024) ? 1024 : oldheight;

                        Bitmap resized = new Bitmap(newwidth, newheight, bitmap.PixelFormat);
                        Graphics graphics = Graphics.FromImage(resized);

                        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        graphics.InterpolationMode =
                           System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(bitmap, 0, 0, newwidth, newheight);

                        bitmap.Dispose();
                        bitmap = resized;
                    }

                    UploadData = OpenJPEG.EncodeFromImage(bitmap, false);
                }
            }
            catch (Exception ex)
            {
                bot.Console.WriteLine(ex.ToString() + " SL Image Upload ");
                return null;
            }
            return UploadData;
        }

        private static bool IsPowerOfTwo(uint n)
        {
            return (n & (n - 1)) == 0 && n != 0;
        }
    }
}
