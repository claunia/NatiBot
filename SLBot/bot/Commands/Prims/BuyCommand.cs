/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BuyCommand.cs
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
    using System.Threading;

    public class BuyCommand : Command
    {
        private ManualResetEvent ObjectPropertiesEvent = new ManualResetEvent(false);

        public BuyCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "buy";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Buy.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Buy.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID target;
            ObjectPropertiesEvent = new ManualResetEvent(false);

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.Buy.Usage");

            if (UUID.TryParse(args[0], out target))
            {
                Primitive targetPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                                           delegate(Primitive prim)
                    {
                        return prim.ID == target;
                    }
                                       );

                if (targetPrim != null)
                {
                    Client.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_OnObjectProperties);
                    Client.Objects.RequestObject(Client.Network.CurrentSim, targetPrim.LocalID);
                    ObjectPropertiesEvent.WaitOne(10000, false);
                    Client.Objects.ObjectProperties -= Objects_OnObjectProperties;
                    Client.Objects.BuyObject(Client.Network.CurrentSim, targetPrim.LocalID, targetPrim.Properties.SaleType, targetPrim.Properties.SalePrice, Client.GroupID, Client.Inventory.FindFolderForType(AssetType.RootFolder));
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Buy.Bought"), targetPrim.Properties.Name);
                }
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.Buy.NotFound"), args[0]);
        }

        void Objects_OnObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            ObjectPropertiesEvent.Set();
        }
    }
}

