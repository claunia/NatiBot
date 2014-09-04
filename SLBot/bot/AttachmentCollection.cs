/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AttachmentCollection.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using OpenMetaverse;
using OpenMetaverse.StructuredData;

namespace bot
{
    [XmlRoot("Attachment")]
    class AttachmentCollection
    {
        private Dictionary<UUID, Array> prims;

        public AttachmentCollection()
        {
            prims = new Dictionary<UUID, Array>();
        }

        public void Add(List<Primitive> Niggers)
        {
            foreach (Primitive prim in Niggers)
            {
                Array arr = new object[2]
                {
                    StateToAttachmentPoint(prim.PrimData.State), 
                    OSDParser.SerializeLLSDXmlString(prim.GetOSD())
                };
                this.prims.Add(prim.ID, arr);
            }
        }

        public void Remove(UUID uuid)
        {
            prims.Remove(uuid);
        }

        public static NBAttachmentPoint StateToAttachmentPoint(uint state)
        {
            const uint ATTACHMENT_MASK = 0xF0;
            uint fixedState = (((byte)state & ATTACHMENT_MASK) >> 4) | (((byte)state & ~ATTACHMENT_MASK) << 4);
            return (NBAttachmentPoint)fixedState;
        }

        public Dictionary<UUID, Array> Prims
        {
            get
            {
                return this.prims;
            }
            set
            {
                this.prims = value;
            }
        }

           
    }
}
