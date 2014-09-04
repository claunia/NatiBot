/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Help2NCCommand.cs
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

    public class Help2NCCommand : Command
    {
        const int NOTECARD_CREATE_TIMEOUT = 2500 * 10;
        const int NOTECARD_FETCH_TIMEOUT = 1500 * 10;
        const int INVENTORY_FETCH_TIMEOUT = 1500 * 10;

        public Help2NCCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "help2nc";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Help2NC.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            UUID notecardItemID = UUID.Zero, notecardAssetID = UUID.Zero;
            string notename, notedata;
            bool success = false, finalUploadSuccess = false;
            string message = String.Empty;
            AutoResetEvent notecardEvent = new AutoResetEvent(false);
            bot.license.Version version = new license.Version();

            string str = version.ToString() + " " + version.v_rev;
            notename = string.Format(bot.Localization.clResourceManager.getText("Commands.Help2NC.Commands"), str);

            #region Create notecard
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Help2NC.Commands"), str).AppendLine();
            builder.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Help.Message"), new object[0]).AppendLine();

            string[] entries = new string[base.Client.Commands.Values.Count];

            int i = 0;
            foreach (Command command in base.Client.Commands.Values)
            {
                entries[i] = string.Format(" * {0} - {1}\n", command.Name, command.Description);
                i++;
            }

            Array.Sort(entries);

            foreach (string entry in entries)
            {
                builder.Append(entry);
            }

            notedata = builder.ToString();
            #endregion


            #region Notecard asset data

            AssetNotecard notecard = new AssetNotecard();
            notecard.BodyText = notedata;

            notecard.Encode();

            #endregion Notecard asset data

            Client.Inventory.RequestCreateItem(Client.Inventory.FindFolderForType(AssetType.Notecard),
                notename, String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), notename, DateTime.Now), AssetType.Notecard,
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
                Client.Inventory.GiveItem(notecardItemID, notename, AssetType.Notecard, Client.MasterKey, false);
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Help2NC.Sending"));
            }
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreateFailDetails"), message);
        }
    }
}