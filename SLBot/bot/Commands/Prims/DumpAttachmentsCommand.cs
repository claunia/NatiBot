/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DumpAttachmentsCommand.cs
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
    using OpenMetaverse.Utilities;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using OpenMetaverse.StructuredData;

    public class DumpAttachmentsCommand : Command
    {
        private Object thisLock = new Object();

        public DumpAttachmentsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Client = SecondLifeBot;
            base.Name = "dumpattachments";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DumpAttachments.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DumpAttachments.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
            {
                return bot.Localization.clResourceManager.getText("Commands.DumpAttachments.Usage");
            }

            Avatar av = base.Client.Network.CurrentSim.ObjectsAvatars.Find(
                            delegate(Avatar a)
                {
                    return a.ID.Equals((UUID)args[0]);
                }
                        );

            List<Primitive> list = base.Client.Network.CurrentSim.ObjectsPrimitives.FindAll(delegate(Primitive prim)
            {
                return prim.ParentID == av.LocalID;
            });

            Program.NBStats.AddStatData(String.Format("{0}: {1} dumping all attachments of {2}.", DateTime.Now.ToString(), Client, args[0]));

            for (int i = 0; i < list.Count; i++)
            {
                lock (thisLock)
                {
                    string cmd;

                    Primitive primitive = list[i];
                    NBAttachmentPoint point = StateToAttachmentPoint(primitive.PrimData.State);

                    cmd = "dumpattachment " + args[0] + " " + point.ToString()/* + " false"*/;

                    this.Client.DoCommand(cmd, fromAgentID, fromSL);
                }
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.DumpAttachments.Done"), list.Count);
        }

        public static NBAttachmentPoint StateToAttachmentPoint(uint state)
        {
            const uint ATTACHMENT_MASK = 0xF0;
            uint fixedState = (((byte)state & ATTACHMENT_MASK) >> 4) | (((byte)state & ~ATTACHMENT_MASK) << 4);
            return (NBAttachmentPoint)fixedState;
        }
    }
}
