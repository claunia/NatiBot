/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ParcelPrimOwnersCommand.cs
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
    using System.Text;
    using System.Threading;

    public class ParcelPrimOwnersCommand : Command
    {
        public ParcelPrimOwnersCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "primowners";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.Usage");

            int parcelID;
            Parcel parcel;
            StringBuilder result = new StringBuilder();
            // test argument that is is a valid integer, then verify we have that parcel data stored in the dictionary
            if (Int32.TryParse(args[0], out parcelID) && Client.Network.CurrentSim.Parcels.TryGetValue(parcelID, out parcel))
            {
                AutoResetEvent wait = new AutoResetEvent(false);
                EventHandler<ParcelObjectOwnersReplyEventArgs> callback = delegate(object sender, ParcelObjectOwnersReplyEventArgs e)
                {
                    for (int i = 0; i < e.PrimOwners.Count; i++)
                    {
                        result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.Info") + System.Environment.NewLine, e.PrimOwners[i].OwnerID, e.PrimOwners[i].Count);
                        wait.Set();
                    }
                };

                Client.Parcels.ParcelObjectOwnersReply += callback;
                ;

                Client.Parcels.RequestObjectOwners(Client.Network.CurrentSim, parcelID);
                if (!wait.WaitOne(10000, false))
                {
                    result.AppendLine(bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.TimeOut"));
                }
                Client.Parcels.ParcelObjectOwnersReply -= callback;

                return result.ToString();
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ParcelPrimOwners.NotFound"), args[0]);
            }
        }

        void Parcels_OnPrimOwnersListReply(Simulator simulator, List<ParcelManager.ParcelPrimOwners> primOwners)
        {
            throw new Exception(bot.Localization.clResourceManager.getText("Exception"));
        }
    }
}
