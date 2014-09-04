/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CreateNotecardCommand.cs
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
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading;
    using OpenMetaverse;
    using OpenMetaverse.Assets;

    public class CreateNotecardCommand : Command
    {
        const int NOTECARD_CREATE_TIMEOUT = 2500 * 10;
        const int NOTECARD_FETCH_TIMEOUT = 1500 * 10;
        const int INVENTORY_FETCH_TIMEOUT = 1500 * 10;

        public CreateNotecardCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "createnotecard";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            UUID embedItemID = UUID.Zero, notecardItemID = UUID.Zero, notecardAssetID = UUID.Zero;
            string filename, fileData;
            bool success = false, finalUploadSuccess = false;
            string message = String.Empty;
            AutoResetEvent notecardEvent = new AutoResetEvent(false);

            if (args.Length == 1)
            {
                filename = args[0];
            }
            else if (args.Length == 2)
            {
                filename = args[0];
                UUID.TryParse(args[1], out embedItemID);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Usage");
            }

            if (!File.Exists(filename))
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.NotFound"), filename);

            try
            {
                fileData = File.ReadAllText(filename);
            }
            catch (Exception ex)
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.OpenFailed"), filename, ex.Message);
            }

            #region Notecard asset data

            AssetNotecard notecard = new AssetNotecard();
            notecard.BodyText = fileData;

            // Item embedding
            if (embedItemID != UUID.Zero)
            {
                // Try to fetch the inventory item
                InventoryItem item = FetchItem(embedItemID);
                if (item != null)
                {
                    notecard.EmbeddedItems = new List<InventoryItem> { item };
                    notecard.BodyText += (char)0xdbc0 + (char)0xdc00;
                }
                else
                {
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.FetchFailed"), embedItemID);
                }
            }

            notecard.Encode();

            #endregion Notecard asset data

            Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.Notecard),
                filename, String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), filename, DateTime.Now), AssetType.Notecard,
                UUID.Random(), InventoryType.Notecard, PermissionMask.All,
                delegate(bool createSuccess, InventoryItem item)
                {
                    if (createSuccess)
                    {
                        #region Upload an empty notecard asset first

                        AutoResetEvent emptyNoteEvent = new AutoResetEvent(false);
                        AssetNotecard empty = new AssetNotecard();
                        empty.BodyText = "\n";
                        empty.Encode();

                        Client.Inventory.RequestUploadNotecardAsset(empty.AssetData, item.UUID,
                            delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                            {
                                notecardItemID = itemID;
                                notecardAssetID = assetID;
                                success = uploadSuccess;
                                message = status ?? bot.Localization.clResourceManager.getText("Commands.CreateNotecard.UnknownError");
                                emptyNoteEvent.Set();
                            });

                        emptyNoteEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

                        #endregion Upload an empty notecard asset first

                        if (success)
                        {
                            // Upload the actual notecard asset
                            Client.Inventory.RequestUploadNotecardAsset(notecard.AssetData, item.UUID,
                                delegate(bool uploadSuccess, string status, UUID itemID, UUID assetID)
                                {
                                    notecardItemID = itemID;
                                    notecardAssetID = assetID;
                                    finalUploadSuccess = uploadSuccess;
                                    message = status ?? bot.Localization.clResourceManager.getText("Commands.CreateNotecard.UnknownError");
                                    notecardEvent.Set();
                                });
                        }
                        else
                        {
                            notecardEvent.Set();
                        }
                    }
                    else
                    {
                        message = bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreateFail");
                        notecardEvent.Set();
                    }
                }
            );

            notecardEvent.WaitOne(NOTECARD_CREATE_TIMEOUT, false);

            if (finalUploadSuccess)
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Success"), notecardItemID, notecardAssetID);
                Client.Inventory.GiveItem(notecardItemID, filename, AssetType.Notecard, Client.MasterKey, false);
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Sending"));
                return DownloadNotecard(notecardItemID, notecardAssetID);
            }
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreateFailDetails"), message);
        }

        private InventoryItem FetchItem(UUID itemID)
        {
            InventoryItem fetchItem = null;
            AutoResetEvent fetchItemEvent = new AutoResetEvent(false);

            EventHandler<ItemReceivedEventArgs> itemReceivedCallback =
                delegate(object sender, ItemReceivedEventArgs e)
                {
                    if (e.Item.UUID == itemID)
                    {
                        fetchItem = e.Item;
                        fetchItemEvent.Set();
                    }
                };

            Client.Inventory.ItemReceived += itemReceivedCallback;

            Client.Inventory.RequestFetchInventory(itemID, Client.Self.AgentID);

            fetchItemEvent.WaitOne(INVENTORY_FETCH_TIMEOUT, false);

            Client.Inventory.ItemReceived -= itemReceivedCallback;

            return fetchItem;
        }

        private string DownloadNotecard(UUID itemID, UUID assetID)
        {
            AutoResetEvent assetDownloadEvent = new AutoResetEvent(false);
            byte[] notecardData = null;
            string error = bot.Localization.clResourceManager.getText("Commands.CreateNotecard.Timeout");

            Client.Assets.RequestInventoryAsset(assetID, itemID, UUID.Zero, Client.Self.AgentID, AssetType.Notecard, true,
                delegate(AssetDownload transfer, Asset asset)
                {
                    if (transfer.Success)
                        notecardData = transfer.AssetData;
                    else
                        error = transfer.Status.ToString();
                    assetDownloadEvent.Set();
                }
            );

            assetDownloadEvent.WaitOne(NOTECARD_FETCH_TIMEOUT, false);

            if (notecardData != null)
                return Encoding.UTF8.GetString(notecardData);
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.DownloadError"), error);
        }
    }
}