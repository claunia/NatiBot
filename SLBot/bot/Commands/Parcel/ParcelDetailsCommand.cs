/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ParcelDetailsCommand.cs
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
    using System.Text;

    public class ParcelDetailsCommand : Command
    {
        public ParcelDetailsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "parceldetails";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ParcelDetails.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ParcelDetails.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.ParcelDetails.Usage");

            int parcelID;
            Parcel parcel;

            // test argument that is is a valid integer, then verify we have that parcel data stored in the dictionary
            if (Int32.TryParse(args[0], out parcelID) && Client.Network.CurrentSim.Parcels.TryGetValue(parcelID, out parcel))
            {
                // this request will update the parcels dictionary
                Client.Parcels.RequestParcelProperties(Client.Network.CurrentSim, parcelID, 0);

                // Use reflection to dynamically get the fields from the Parcel struct
                Type t = parcel.GetType();
                System.Reflection.FieldInfo[] fields = t.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

                StringBuilder sb = new StringBuilder();
                foreach (System.Reflection.FieldInfo field in fields)
                {
                    sb.AppendFormat("{0} = {1}" + System.Environment.NewLine, field.Name, field.GetValue(parcel));
                }
                return sb.ToString();
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ParcelDetails.NotFound"), args[0]);
            }
        }
    }
}
