/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ParcelSelectObjectsCommand.cs
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

    public class ParcelSelectObjectsCommand : Command
    {
        public ParcelSelectObjectsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "selectobjects";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length < 2)
                return bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.Usage");

            int parcelID;
            UUID ownerUUID;

            int counter = 0;
            StringBuilder result = new StringBuilder();
            // test argument that is is a valid integer, then verify we have that parcel data stored in the dictionary
            if (Int32.TryParse(args[0], out parcelID)
                && UUID.TryParse(args[1], out ownerUUID))
            {
                AutoResetEvent wait = new AutoResetEvent(false);
                EventHandler<ForceSelectObjectsReplyEventArgs> callback = delegate(object sender, ForceSelectObjectsReplyEventArgs e)
                {
                    //result.AppendLine("New List: " + resetList.ToString());
                    for (int i = 0; i < e.ObjectIDs.Count; i++)
                    {
                        result.Append(e.ObjectIDs[i].ToString() + " ");
                        counter++;
                    }
                    //result.AppendLine("Got " + objectIDs.Count.ToString() + " Objects in packet");
                    if (e.ObjectIDs.Count < 251)
                        wait.Set();
                };

                Client.Parcels.ForceSelectObjectsReply += callback;
                Client.Parcels.RequestSelectObjects(parcelID, (ObjectReturnType)16, ownerUUID);

                Client.Parcels.RequestObjectOwners(Client.Network.CurrentSim, parcelID);
                if (!wait.WaitOne(30000, false))
                {
                    result.AppendLine(bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.Timeout"));
                }

                Client.Parcels.ForceSelectObjectsReply -= callback;
                result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.Found"), counter);
                return result.ToString();
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ParcelSelectObjects.NotFound"), args[0]);
            }
        }
    }
}
