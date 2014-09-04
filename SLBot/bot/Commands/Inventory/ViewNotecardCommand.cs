/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ViewNotecardCommand.cs
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
    using System;
    using System.Collections.Generic;
    using OpenMetaverse.Assets;

    public class ViewNotecardCommand : Command
    {
        public ViewNotecardCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "viewnote";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ViewNotecard.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ViewNotecard.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length < 1)
            {
                return bot.Localization.clResourceManager.getText("Commands.ViewNotecard.Usage");
            }
            UUID note;
            if (!UUID.TryParse(args[0], out note))
            {
                return bot.Localization.clResourceManager.getText("Commands.ViewNotecard.ExpectedUUID");
            }

            System.Threading.AutoResetEvent waitEvent = new System.Threading.AutoResetEvent(false);

            System.Text.StringBuilder result = new System.Text.StringBuilder();

            // verify asset is loaded in store
            if (Client.Inventory.Store.Contains(note))
            {
                // retrieve asset from store
                InventoryItem ii = (InventoryItem)Client.Inventory.Store[note];

                // make request for asset
                Client.Assets.RequestInventoryAsset(ii, true,
                    delegate(AssetDownload transfer, Asset asset)
                    {
                        if (transfer.Success)
                        {
                            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ViewNotecard.NotecardData"), Utils.BytesToString(asset.AssetData));
                            waitEvent.Set();
                        }
                    }
                );

                // wait for reply or timeout
                if (!waitEvent.WaitOne(10000, false))
                {
                    result.Append(bot.Localization.clResourceManager.getText("Commands.ViewNotecard.Timeout"));
                }
                // unsubscribe from reply event
            }
            else
            {
                result.Append(bot.Localization.clResourceManager.getText("Commands.ViewNotecard.NotFound"));
            }

            // return results
            return result.ToString();
        }


    }
}
