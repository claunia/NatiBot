/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CreateSkinCommand.cs
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

    public class CreateSkinCommand : Command
    {
        public CreateSkinCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "createskin";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CreateSkin.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CreateSkin.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string finalmessage = "";
            string NL = "\n";
            UUID uuid1, uuid2, uuid3;
            uuid3 = uuid2 = uuid1 = UUID.Zero;

            if (args.Length != 4)
                return bot.Localization.clResourceManager.getText("Commands.CreateSkin.Usage");

            if (!UUID.TryParse(args[1], out uuid1))
                return bot.Localization.clResourceManager.getText("Commands.CreateSkin.ExpectedFaceID");

            if (!UUID.TryParse(args[2], out uuid2))
                return bot.Localization.clResourceManager.getText("Commands.CreateSkin.ExpectedUpID");

            if (!UUID.TryParse(args[3], out uuid3))
                return bot.Localization.clResourceManager.getText("Commands.CreateSkin.ExpectedLowID");

            Program.NBStats.AddStatData(String.Format("{0}: {1} creating skin named {2} with uuids {3} {4} {5}.", DateTime.Now.ToString(), Client, args[0], args[1], args[2], args[3]));

            #region Part common to all wearable types
            StringBuilder sbcloth = new StringBuilder("LLWearable version 22\n");
            sbcloth.Append(args[0]);
            sbcloth.Append(NL);
            sbcloth.Append(NL);
            sbcloth.Append("\tpermissions 0\n\t{\n");
            sbcloth.Append("\t\tbase_mask\t");
            sbcloth.Append("7fffffff");
            sbcloth.Append(NL);
            sbcloth.Append("\t\towner_mask\t");
            sbcloth.Append("7fffffff");
            sbcloth.Append(NL);
            sbcloth.Append("\t\tgroup_mask\t");
            sbcloth.Append("00000000");
            sbcloth.Append(NL);
            sbcloth.Append("\t\teveryone_mask\t");
            sbcloth.Append("00000000");
            sbcloth.Append(NL);
            sbcloth.Append("\t\tnext_owner_mask\t");
            sbcloth.Append("7fffffff");
            sbcloth.Append(NL);
            sbcloth.Append("\t\tcreator_id\t");
            sbcloth.Append(Client.Self.AgentID.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("\t\towner_id\t");
            sbcloth.Append(Client.Self.AgentID.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("\t\tlast_owner_id\t");
            sbcloth.Append(Client.Self.AgentID.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("\t\tgroup_id\t");
            sbcloth.Append(UUID.Zero.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("\t}\n");
            sbcloth.Append("\tsale_info\t0\n");
            sbcloth.Append("\t{\n");
            sbcloth.Append("\t\tsale_type\t");
            sbcloth.Append("not");
            sbcloth.Append(NL);
            sbcloth.Append("\t\tsale_price\t");
            sbcloth.Append("0");
            sbcloth.Append(NL);
            sbcloth.Append("\t}\n");
            #endregion #region Part common to all wearable types

            sbcloth.Append("type ");
            sbcloth.Append((int)WearableType.Skin);
            sbcloth.Append(NL);

            sbcloth.Append("parameters ");
            sbcloth.Append(26);
            sbcloth.Append(NL);
            sbcloth.Append("108 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("110 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("111 ");
            sbcloth.Append(".5");
            sbcloth.Append(NL);
            sbcloth.Append("116 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("117 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("150 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("162 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("163 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("165 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("700 ");
            sbcloth.Append(".25");
            sbcloth.Append(NL);
            sbcloth.Append("701 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("702 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("703 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("704 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("705 ");
            sbcloth.Append(".5");
            sbcloth.Append(NL);
            sbcloth.Append("706 ");
            sbcloth.Append(".6");
            sbcloth.Append(NL);
            sbcloth.Append("707 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("708 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("709 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("710 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("711 ");
            sbcloth.Append(".5");
            sbcloth.Append(NL);
            sbcloth.Append("712 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("713 ");
            sbcloth.Append(".7");
            sbcloth.Append(NL);
            sbcloth.Append("714 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("715 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("775 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("textures ");
            sbcloth.Append(3);
            sbcloth.Append(NL);
            sbcloth.Append("0 ");
            sbcloth.Append(uuid1.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("5 ");
            sbcloth.Append(uuid2.ToString());
            sbcloth.Append(NL);
            sbcloth.Append("6 ");
            sbcloth.Append(uuid3.ToString());
            sbcloth.Append(NL);

            AssetBodypart bodypart = new AssetBodypart(sbcloth.ToString());


            bodypart.Decode();

            Client.Inventory.RequestCreateItemFromAsset(bodypart.AssetData, args[0], String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), args[0], DateTime.Now), AssetType.Bodypart,
                InventoryType.Wearable, Client.Inventory.FindFolderForType(AssetType.Bodypart),
                delegate(bool success, string status, UUID itemID, UUID assetID)
                {
                    if (success)
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateSkin.Created"), assetID);
                        Client.Inventory.GiveItem(itemID, args[0], AssetType.Bodypart, Client.MasterKey, false);
                    }
                    else
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateSkin.Failed"), status);
                    }
                }
            );

            return finalmessage;
        }
    }
}