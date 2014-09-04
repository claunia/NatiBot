/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AttachmentsCommand.cs
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
    using System.Collections.Generic;

    public class AttachmentsCommand : Command
    {
        public AttachmentsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Client = SecondLifeBot;
            base.Name = "attachments";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Attachments.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder builder = new StringBuilder();

            List<Primitive> attachments = Client.Network.CurrentSim.ObjectsPrimitives.FindAll(
                                              delegate(Primitive prim)
                {
                    return prim.ParentID == Client.Self.LocalID;
                }
                                          );

            for (int i = 0; i < attachments.Count; i++)
            {
                Primitive prim = attachments[i];
                NBAttachmentPoint point = StateToAttachmentPoint(prim.PrimData.State);

                // TODO: Fetch properties for the objects with missing property sets so we can show names
                //Logger.Log(String.Format("[Attachment @ {0}] LocalID: {1} UUID: {2} Offset: {3}",
                //    point, prim.LocalID, prim.ID, prim.Position), Helpers.LogLevel.Info, Client);

                builder.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Attachments.Attachment"),
                    point, prim.LocalID, prim.ID, prim.Position);

                builder.AppendLine();
            }

            builder.AppendLine(String.Format(bot.Localization.clResourceManager.getText("Commands.Attachments.Found"), attachments.Count));

            return builder.ToString();
        }

        public static NBAttachmentPoint StateToAttachmentPoint(uint state)
        {
            const uint ATTACHMENT_MASK = 0xF0;
            uint fixedState = (((byte)state & ATTACHMENT_MASK) >> 4) | (((byte)state & ~ATTACHMENT_MASK) << 4);
            return (NBAttachmentPoint)fixedState;
        }
    }
}

