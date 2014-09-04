/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : UploadCommand.cs
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

    //    using OpenMetaverse.Capabilities;
    using OpenMetaverse.Imaging;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Drawing;
    using System.IO;
    using OpenMetaverse.Assets;

    public class UploadCommand : Command
    {
        AutoResetEvent UploadCompleteEvent = new AutoResetEvent(false);
        UUID TextureID = UUID.Zero;
        DateTime start;
        AssetType detectedAssetType = AssetType.Unknown;
        InventoryType detectedInventoryType = InventoryType.Unknown;
        System.Text.StringBuilder returnString;
        const int NOTECARD_CREATE_TIMEOUT = 1000 * 10;

        public UploadCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "upload";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Upload.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Upload.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            returnString = new System.Text.StringBuilder();

            if (args.Length >= 2)
            {
                string inventoryName;
                string fileName;

                if (args.Length != 2)
                    return bot.Localization.clResourceManager.getText("Commands.Upload.Usage");

                TextureID = UUID.Zero;
                inventoryName = args[0];
                fileName = args[1];

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Upload.Loading"), fileName);

                switch (System.IO.Path.GetExtension(fileName))
                {
                    case ".animatn":
                        detectedAssetType = AssetType.Animation;
                        detectedInventoryType = InventoryType.Animation;
                        break;
                    case ".bodypart":
                        detectedAssetType = AssetType.Bodypart;
                        detectedInventoryType = InventoryType.Wearable;
                        break;
                    case ".gesture":
                        detectedAssetType = AssetType.Gesture;
                        detectedInventoryType = InventoryType.Gesture;
                        break;
                    case ".clothing":
                        detectedAssetType = AssetType.Clothing;
                        detectedInventoryType = InventoryType.Wearable;
                        break;
                    case ".jpg":
                    case ".tga":
                    case ".jp2":
                    case ".j2c":
                        detectedAssetType = AssetType.Texture;
                        detectedInventoryType = InventoryType.Texture;
                        break;
                    case ".notecard":
                        detectedAssetType = AssetType.Notecard;
                        detectedInventoryType = InventoryType.Notecard;
                        break;
                    case ".landmark":
                        detectedAssetType = AssetType.Landmark;
                        detectedInventoryType = InventoryType.Landmark;
                        break;
                    case ".ogg":
                        detectedAssetType = AssetType.Sound;
                        detectedInventoryType = InventoryType.Sound;
                        break;
                    case ".lsl":
                        detectedAssetType = AssetType.LSLText;
                        detectedInventoryType = InventoryType.LSL;
                        break;
                    case ".lso":
                        detectedAssetType = AssetType.LSLBytecode;
                        detectedInventoryType = InventoryType.LSL;
                        break;
                    case ".wav":
                    default:
                        return bot.Localization.clResourceManager.getText("Commands.Upload.Unsupported");
                }

                switch (detectedAssetType)
                {
                    case AssetType.Texture:
                        byte[] jpeg2k;
                        try
                        {
                            jpeg2k = System.IO.File.ReadAllBytes(fileName);
                        }
                        catch (Exception e)
                        {
                            return e.Message;
                        }
                        if (jpeg2k == null)
                            return bot.Localization.clResourceManager.getText("Commands.Upload.FailedCompress");
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Upload.CompressedUpload"));
                        start = DateTime.Now;
                        DoUpload(jpeg2k, inventoryName);
                        break;
                    case AssetType.LSLText:
                        byte[] rawScriptData;
                        try
                        {
                            rawScriptData = System.IO.File.ReadAllBytes(fileName);
                        }
                        catch (Exception e)
                        {
                            return e.Message;
                        }
                        if (rawScriptData == null)
                            return bot.Localization.clResourceManager.getText("Commands.Upload.FailedLoad");
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Upload.LoadedUpload"));
                        start = DateTime.Now;
                        DoUploadScript(rawScriptData, inventoryName);
                        break;
                    case AssetType.Notecard:
                        byte[] rawNotecardData;
                        try
                        {
                            rawNotecardData = System.IO.File.ReadAllBytes(fileName);
                        }
                        catch (Exception e)
                        {
                            return e.Message;
                        }
                        if (rawNotecardData == null)
                            return bot.Localization.clResourceManager.getText("Commands.Upload.FailedLoad");
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Upload.LoadedUpload"));
                        start = DateTime.Now;
                        DoUploadNotecard(rawNotecardData, inventoryName);
                        break;
                    default:
                        byte[] rawData;
                        try
                        {
                            rawData = System.IO.File.ReadAllBytes(fileName);
                        }
                        catch (Exception e)
                        {
                            return e.Message;
                        }
                        if (rawData == null)
                            return bot.Localization.clResourceManager.getText("Commands.Upload.FailedLoad");
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Upload.LoadedUpload"));
                        start = DateTime.Now;
                        DoUpload(rawData, inventoryName);
                        break;
                }

                if (UploadCompleteEvent.WaitOne(15000, false))
                {
                    return returnString.ToString();
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.Upload.Timeout");
                }
            }
            return bot.Localization.clResourceManager.getText("Commands.Upload.Usage");
        }

        private void DoUploadNotecard(byte[] UploadData, string FileName)
        {
            try
            {
                string desc = String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.CreatedBy"), FileName, DateTime.Now);
                AutoResetEvent emptyNoteEvent = new AutoResetEvent(false);
                AutoResetEvent notecardEvent = new AutoResetEvent(false);
                AssetNotecard empty = new AssetNotecard();
                bool emptySuccess = false, finalUploadSuccess = false;
                string message = String.Empty;
                UUID notecardItemID = UUID.Zero, notecardAssetID = UUID.Zero;

                // create the asset
                Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.Notecard), FileName, desc, AssetType.Notecard, UUID.Random(), InventoryType.Notecard, PermissionMask.All,
                    delegate(bool success, InventoryItem item)
                    {
                        bot.Console.WriteLine(String.Format(
                            bot.Localization.clResourceManager.getText("Commands.Upload.Returned"),
                            success, item.UUID, item.AssetUUID));
                        if (success)
                        {
                            // upload the asset

                            #region Upload an empty notecard asset first
                            empty.BodyText = "\n";
                            empty.Encode();

                            Client.Inventory.RequestUploadNotecardAsset(empty.AssetData, item.UUID,
                                delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                                {
                                    notecardItemID = itemID;
                                    notecardAssetID = assetID;
                                    emptySuccess = uploadSuccess;
                                    message = status ?? bot.Localization.clResourceManager.getText("Commands.Upload.UnknownError");
                                    emptyNoteEvent.Set();
                                });

                            emptyNoteEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

                            #endregion Upload an empty notecard asset first

                            if (emptySuccess)
                            {
                                // Upload the actual notecard asset
                                Client.Inventory.RequestUploadNotecardAsset(UploadData, item.UUID,
                                    delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                                    {
                                        notecardItemID = itemID;
                                        notecardAssetID = assetID;
                                        finalUploadSuccess = uploadSuccess;
                                        message = status ?? bot.Localization.clResourceManager.getText("Commands.Upload.UnknownError");
                                        notecardEvent.Set();
                                    });
                            }
                            else
                            {
                                notecardEvent.Set();
                            }

                        }
                        
                    });

                notecardEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

                if (finalUploadSuccess)
                {
                    bot.Console.WriteLine(String.Format(
                        bot.Localization.clResourceManager.getText("Commands.Upload.ReturnedNotecard"),
                        finalUploadSuccess.ToString(), message, notecardItemID, notecardAssetID));
                    bot.Console.WriteLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.UploadTook"), DateTime.Now.Subtract(start)));
                    Client.Inventory.GiveItem(notecardItemID, FileName, AssetType.Notecard, Client.MasterKey, true);
                    returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.UploadedUUID"), notecardAssetID);
                    returnString.AppendLine();
                    returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.Sent"), Client.MasterName, Client.MasterKey);
                    returnString.AppendLine();
                    UploadCompleteEvent.Set();
                }
                else
                {
                    bot.Console.WriteLine(String.Format(
                        bot.Localization.clResourceManager.getText("Commands.Upload.ReturnedNotecard"),
                        finalUploadSuccess.ToString(), message, notecardItemID, notecardAssetID));
                    UploadCompleteEvent.Set();
                }
            }
            catch (System.Exception e)
            {
                bot.Console.WriteLine(e.ToString());
                returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.ErrorNotecard"), FileName);
            }
        }

        private void DoUploadScript(byte[] UploadData, string FileName)
        {
            try
            {
                string desc = String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.CreatedBy"), FileName, DateTime.Now);
                // create the asset
                Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.LSLText), FileName, desc, AssetType.LSLText, UUID.Random(), InventoryType.LSL, PermissionMask.All,
                    delegate(bool success, InventoryItem item)
                    {
                        bot.Console.WriteLine(String.Format(
                            bot.Localization.clResourceManager.getText("Commands.Upload.Returned"),
                            success, item.UUID, item.AssetUUID));
                        if (success)
                            // upload the asset
                            Client.Inventory.RequestUpdateScriptAgentInventory(UploadData, item.UUID, true, new InventoryManager.ScriptUpdatedCallback(delegate(bool success1, string status, UUID itemid, UUID assetid)
                            {
                                if (success1)
                                {
                                    bot.Console.WriteLine(String.Format(
                                        bot.Localization.clResourceManager.getText("Commands.Upload.ReturnedScript"),
                                        success1, status, itemid, assetid));
                                    bot.Console.WriteLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.UploadTook"), DateTime.Now.Subtract(start)));
                                    Client.Inventory.GiveItem(item.UUID, FileName, AssetType.LSLText, Client.MasterKey, true);
                                    returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.UploadUUID"), assetid);
                                    returnString.AppendLine();
                                    returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.Sent"), Client.MasterName, Client.MasterKey);
                                    returnString.AppendLine();
                                    UploadCompleteEvent.Set();
                                }
                            }));
                    });

            }
            catch (System.Exception e)
            {
                bot.Console.WriteLine(e.ToString());
                returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.ErrorScript"), FileName);
            }
        }

        private void DoUpload(byte[] UploadData, string FileName)
        {
            if (UploadData != null)
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(FileName);
                string desc = String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.CreatedBy"), FileName, DateTime.Now);

                Client.Inventory.RequestCreateItemFromAsset(UploadData, name, desc,
                    detectedAssetType, detectedInventoryType, Client.Inventory.FindFolderForType(detectedAssetType),

                    /*delegate(CapsClient client, long bytesReceived, long bytesSent, long totalBytesToReceive, long totalBytesToSend)
                    {
                        if (bytesSent > 0)
                            bot.Console.WriteLine(String.Format("Textura subida: {0} / {1}", bytesSent, totalBytesToSend));
                    },*/
                    // CLAUNIA: Seems that libomv changes nulled this functionality

                    delegate(bool success, string status, UUID itemID, UUID assetID)
                    {
                        bot.Console.WriteLine(String.Format(
                            bot.Localization.clResourceManager.getText("Commands.Upload.ReturnedAsset"),
                            success, status, itemID, assetID));

                        bot.Console.WriteLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Upload.UploadTook"), DateTime.Now.Subtract(start)));

                        if (!success)
                        {
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.Failed"), status);
                            returnString.AppendLine();
                        }
                        else
                        {
                            InventoryItem item = Client.Inventory.FetchItem(itemID, Client.Self.AgentID, 1000 * 15);
                            item.Permissions.NextOwnerMask = PermissionMask.All;
                            Client.Inventory.RequestUpdateItem(item);

                            Client.Inventory.GiveItem(itemID, FileName, detectedAssetType, Client.MasterKey, true);
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.UploadedUUID"), assetID);
                            returnString.AppendLine();
                            returnString.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Upload.Sent"), Client.MasterName, Client.MasterKey);
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
