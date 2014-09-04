/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CreateEyesCommand.cs
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

    public class CreateEyesCommand : Command
    {
        public CreateEyesCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "createeyes";
            base.Description = bot.Localization.clResourceManager.getText("Commands.CreateEyes.Description") + " " + bot.Localization.clResourceManager.getText("Commands.CreateEyes.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string finalmessage = "";
            string NL = "\n";
            UUID uuid1;
            uuid1 = UUID.Zero;

            if (args.Length != 2)
                return Description;

            if (!UUID.TryParse(args[1], out uuid1))
                return bot.Localization.clResourceManager.getText("Commands.CreateEyes.ExpectedID");

            Program.NBStats.AddStatData(String.Format("{0}: {1} creating eyes named {2} with uuid {3}.", DateTime.Now.ToString(), Client, args[0], args[1]));

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
            sbcloth.Append((int)WearableType.Eyes);
            sbcloth.Append(NL);

            sbcloth.Append("parameters ");
            sbcloth.Append(2);
            sbcloth.Append(NL);
            sbcloth.Append("98 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("99 ");
            sbcloth.Append(0);
            sbcloth.Append(NL);
            sbcloth.Append("textures ");
            sbcloth.Append(1);
            sbcloth.Append(NL);
            sbcloth.Append("3 ");
            sbcloth.Append(uuid1.ToString());
            sbcloth.Append(NL);

            AssetBodypart bodypart = new AssetBodypart(sbcloth.ToString());


            bodypart.Decode();

            Client.Inventory.RequestCreateItemFromAsset(bodypart.AssetData, args[0], String.Format(bot.Localization.clResourceManager.getText("Commands.CreateNotecard.CreatedBy"), args[0], DateTime.Now), AssetType.Bodypart,
                InventoryType.Wearable, Client.Inventory.FindFolderForType(AssetType.Bodypart),
                delegate(bool success, string status, UUID itemID, UUID assetID)
                {
                    if (success)
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateEyes.Created"), assetID);
                        Client.Inventory.GiveItem(itemID, args[0], AssetType.Bodypart, Client.MasterKey, false);
                    }
                    else
                    {
                        finalmessage = String.Format(bot.Localization.clResourceManager.getText("Commands.CreateEyes.Failed"), status);
                    }
                }
            );

            return finalmessage;
        }
    }
}