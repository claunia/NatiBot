﻿/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BalanceCommand.cs
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

    public class BalanceCommand : Command
    {
        public BalanceCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "balance";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Balance.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            System.Threading.AutoResetEvent waitBalance = new System.Threading.AutoResetEvent(false);
            EventHandler<BalanceEventArgs> del = delegate(object sender, BalanceEventArgs e)
            {
                waitBalance.Set();
            };
            Client.Self.MoneyBalance += del;    
            Client.Self.RequestBalance();
            String result = bot.Localization.clResourceManager.getText("Commands.Balance.Timeout");
            if (waitBalance.WaitOne(10000, false))
            {
                result = String.Format(bot.Localization.clResourceManager.getText("Commands.Balance.Balance"), Client.ToString(),
                    Client.Self.Balance);
            }
            Client.Self.MoneyBalance -= del;
            return result;
        }
    }
}
