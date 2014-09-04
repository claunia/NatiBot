/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CreateLandMarkCommand.cs
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
// Does not work, HTTP error 400.
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

    public class CreateLandMarkCommand : Command
    {
        public CreateLandMarkCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "createlm";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CreateLandMark.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string finalmessage = "";

            AssetLandmark landmark = new AssetLandmark(this.Client.Network.CurrentSim.RegionID, this.Client.Self.SimPosition);

            landmark.Encode();
            landmark.Decode();

            Client.Inventory.RequestCreateItemFromAsset(landmark.AssetData, this.Client.Network.CurrentSim.Name, String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), this.Client.Network.CurrentSim.Name, DateTime.Now), AssetType.Landmark,
                InventoryType.Landmark, Client.Inventory.FindFolderForType(AssetType.Landmark),
                delegate(bool success, string status, UUID itemID, UUID assetID)
                {
                    if (success)
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateLandMark.Created"), assetID);
                        Client.Inventory.GiveItem(itemID, this.Client.Network.CurrentSim.Name, AssetType.Clothing, Client.MasterKey, false);
                    }
                    else
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateLandMark.Failed"), status);
                    }
                }
            );

            return finalmessage;
        }
    }
}