/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CreateClothingCommand.cs
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

    public class CreateClothingCommand : Command
    {
        public CreateClothingCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "createclothing";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CreateClothing.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CreateClothing.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string LongUsage = bot.Localization.clResourceManager.getText("Commands.CreateClothing.UsageLine1") + System.Environment.NewLine +
                               bot.Localization.clResourceManager.getText("Commands.CreateClothing.UsageLine2");
            string finalmessage = "";
            string NL = "\n";
            WearableType wtype;
            UUID uuid1, uuid2;
            uuid2 = UUID.Zero;

            if (args.Length == 0)
                return Description;
            if (args.Length == 1)
            if (args[0].ToLower() == "help")
                return LongUsage;
            else
                return Description;
            if (args.Length != 3 && args.Length != 4)
                return Description;

            switch (args[1].ToLower())
            {
                case "gloves":
                    wtype = WearableType.Gloves;
                    break;
                case "jacket":
                    wtype = WearableType.Jacket;
                    if (args.Length != 4)
                        return bot.Localization.clResourceManager.getText("Commands.CreateClothing.Jacket");
                    break;
                case "pants":
                    wtype = WearableType.Pants;
                    break;
                case "shirt":
                    wtype = WearableType.Shirt;
                    break;
                case "shoes":
                    wtype = WearableType.Shoes;
                    break;
                case "skirt":
                    wtype = WearableType.Skirt;
                    break;
                case "socks":
                    wtype = WearableType.Socks;
                    break;
                case "underpants":
                    wtype = WearableType.Underpants;
                    break;
                case "undershirt":
                    wtype = WearableType.Undershirt;
                    break;
                default:
                    return bot.Localization.clResourceManager.getText("Commands.CreateClothing.Incorrect");
            }

            if (!UUID.TryParse(args[2], out uuid1))
                return bot.Localization.clResourceManager.getText("Commands.CreateClothing.ExpectedID1");

            if (args.Length == 4)
            if (!UUID.TryParse(args[3], out uuid2))
                return bot.Localization.clResourceManager.getText("Commands.CreateClothing.ExpectedID2");

            if (args[1].ToLower() == "jacket")
                Program.NBStats.AddStatData(String.Format("{0}: {1} creating clothing of type {2} named {3} with uuid {4} {5}.", DateTime.Now.ToString(), Client, args[1], args[0], args[2], args[3]));
            else
                Program.NBStats.AddStatData(String.Format("{0}: {1} creating clothing of type {2} named {3} with uuid {4}.", DateTime.Now.ToString(), Client, args[1], args[0], args[2]));

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
            sbcloth.Append((int)wtype);
            sbcloth.Append(NL);

            switch (wtype)
            {
                case WearableType.Gloves:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(5);
                    sbcloth.Append(NL);
                    sbcloth.Append("93 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("827 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("829 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("830 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("844 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("15 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Jacket:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(9);
                    sbcloth.Append(NL);
                    sbcloth.Append("606 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("607 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("608 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("609 ");
                    sbcloth.Append(".2");
                    sbcloth.Append(NL);
                    sbcloth.Append("780 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("834 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("835 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("836 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("877 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(2);
                    sbcloth.Append(NL);
                    sbcloth.Append("13 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    sbcloth.Append("14 ");
                    sbcloth.Append(uuid2.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Pants:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(9);
                    sbcloth.Append(NL);
                    sbcloth.Append("625 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("638 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("806 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("807 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("808 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("814 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("815 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("816 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("869 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("2 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Shirt:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(10);
                    sbcloth.Append(NL);
                    sbcloth.Append("781 ");
                    sbcloth.Append(".78");
                    sbcloth.Append(NL);
                    sbcloth.Append("800 ");
                    sbcloth.Append(".89");
                    sbcloth.Append(NL);
                    sbcloth.Append("801 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("802 ");
                    sbcloth.Append(".78");
                    sbcloth.Append(NL);
                    sbcloth.Append("803 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("804 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("805 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("828 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("840 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("868 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("1 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Shoes:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(10);
                    sbcloth.Append(NL);
                    sbcloth.Append("198 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("503 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("508 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("513 ");
                    sbcloth.Append(".5");
                    sbcloth.Append(NL);
                    sbcloth.Append("514 ");
                    sbcloth.Append(".5");
                    sbcloth.Append(NL);
                    sbcloth.Append("616 ");
                    sbcloth.Append(".1");
                    sbcloth.Append(NL);
                    sbcloth.Append("654 ");
                    sbcloth.Append(0);
                    sbcloth.Append(NL);
                    sbcloth.Append("812 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("813 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("817  ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("7 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Skirt:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(10);
                    sbcloth.Append(NL);
                    sbcloth.Append("848 ");
                    sbcloth.Append(".2");
                    sbcloth.Append(NL);
                    sbcloth.Append("858 ");
                    sbcloth.Append(".4");
                    sbcloth.Append(NL);
                    sbcloth.Append("859 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("860 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("861 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("862 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("863 ");
                    sbcloth.Append(".33");
                    sbcloth.Append(NL);
                    sbcloth.Append("921 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("922 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("923 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("18 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Socks:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(4);
                    sbcloth.Append(NL);
                    sbcloth.Append("617 ");
                    sbcloth.Append(".35");
                    sbcloth.Append(NL);
                    sbcloth.Append("818 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("819 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("820 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("12 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Underpants:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(5);
                    sbcloth.Append(NL);
                    sbcloth.Append("619 ");
                    sbcloth.Append(".3");
                    sbcloth.Append(NL);
                    sbcloth.Append("624 ");
                    sbcloth.Append(".8");
                    sbcloth.Append(NL);
                    sbcloth.Append("824 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("825 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("826 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("17 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
                case WearableType.Undershirt:
                    sbcloth.Append("parameters ");
                    sbcloth.Append(7);
                    sbcloth.Append(NL);
                    sbcloth.Append("603 ");
                    sbcloth.Append(".4");
                    sbcloth.Append(NL);
                    sbcloth.Append("604 ");
                    sbcloth.Append(".85");
                    sbcloth.Append(NL);
                    sbcloth.Append("605 ");
                    sbcloth.Append(".84");
                    sbcloth.Append(NL);
                    sbcloth.Append("779 ");
                    sbcloth.Append(".84");
                    sbcloth.Append(NL);
                    sbcloth.Append("821 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("822 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("823 ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("textures ");
                    sbcloth.Append(1);
                    sbcloth.Append(NL);
                    sbcloth.Append("16 ");
                    sbcloth.Append(uuid1.ToString());
                    sbcloth.Append(NL);
                    break;
            }

            AssetClothing clothing = new AssetClothing(sbcloth.ToString());

            clothing.Decode();

            Client.Inventory.RequestCreateItemFromAsset(clothing.AssetData, args[0], String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), args[0], DateTime.Now), AssetType.Clothing,
                InventoryType.Wearable, Client.Inventory.FindFolderForType(AssetType.Clothing),
                delegate(bool success, string status, UUID itemID, UUID assetID)
                {
                    if (success)
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateClothing.Created"), assetID);
                        Client.Inventory.GiveItem(itemID, args[0], AssetType.Clothing, Client.MasterKey, false);
                    }
                    else
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateClothing.Failed"), status);
                    }
                }
            );

            return finalmessage;
        }
    }
}