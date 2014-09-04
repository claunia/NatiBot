/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ExportOutfitCommand.cs
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
    using OpenMetaverse.Packets;
    using OpenMetaverse.StructuredData;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    public class ExportOutfitCommand : Command
    {
        public ExportOutfitCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "exportoutfit";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ExportOutfit.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ExportOutfit.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID id;
            string path;

            if (args.Length == 1)
            {
                id = Client.Self.AgentID;
                path = args[0];
            }
            else if (args.Length == 2)
            {
                if (!UUID.TryParse(args[0], out id))
                    return bot.Localization.clResourceManager.getText("Commands.ExportOutfit.Usage");
                path = args[1];
            }
            else
                return bot.Localization.clResourceManager.getText("Commands.ExportOutfit.Usage");

            lock (Client.Appearances)
            {
                if (Client.Appearances.ContainsKey(id))
                {
                    try
                    {
                        File.WriteAllText(path, Packet.ToXmlString(Client.Appearances[id]));
                    }
                    catch (Exception e)
                    {
                        return e.ToString();
                    }

                    return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportOutfit.Exported"), id.ToString(), args[1]);
                }
                else
                {
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportOutfit.NotFound"), id.ToString());
                }
            }
        }
    }
}

