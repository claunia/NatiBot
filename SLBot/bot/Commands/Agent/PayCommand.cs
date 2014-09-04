/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : PayCommand.cs
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

    public class PayCommand : Command
    {
        public PayCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "pay";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Pay.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Pay.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            int amount;
            UUID avatarID;
            string avatarName;

            if (args.Length == 1)
            {
                avatarID = Client.MasterKey;
            }
            else if (args.Length == 2)
            {
                if (!UUID.TryParse(args[1], out avatarID))
                {
                    return bot.Localization.clResourceManager.getText("Commands.Pay.Usage");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Pay.Usage");
            }

            if (!Int32.TryParse(args[0], out amount))
            if (args[0].ToLower().Equals("all"))
                amount = Client.Self.Balance;
            else
                return bot.Localization.clResourceManager.getText("Commands.Pay.Usage");

            if (!Client.key2Name(avatarID, out avatarName))
                avatarName = avatarID.ToString();

            if (amount > Client.Self.Balance)
                amount = Client.Self.Balance;

            Client.Self.GiveAvatarMoney(avatarID, amount, String.Format(bot.Localization.clResourceManager.getText("Commands.Pay.Message"), amount));
            return String.Format(bot.Localization.clResourceManager.getText("Commands.Pay.Gave"), amount, avatarName);
        }
    }
}

