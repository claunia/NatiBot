/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ChatEventArgs.cs
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
namespace bot.NetCom
{
    using OpenMetaverse;
    using System;

    public class ChatEventArgs : EventArgs
    {
        private ChatAudibleLevel audible;
        private string fromName;
        private UUID id;
        private string message;
        private UUID ownerid;
        private Vector3 position;
        private ChatSourceType sourceType;
        private ChatType type;

        public ChatEventArgs(string message, ChatAudibleLevel audible, ChatType type, ChatSourceType sourceType, string fromName, UUID id, UUID ownerid, Vector3 position)
        {
            this.message = message;
            this.audible = audible;
            this.type = type;
            this.sourceType = sourceType;
            this.fromName = fromName;
            this.id = id;
            this.ownerid = ownerid;
            this.position = position;
        }

        public ChatAudibleLevel Audible
        {
            get
            {
                return this.audible;
            }
        }

        public string FromName
        {
            get
            {
                return this.fromName;
            }
        }

        public UUID ID
        {
            get
            {
                return this.id;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public UUID OwnerID
        {
            get
            {
                return this.ownerid;
            }
        }

        public Vector3 Position
        {
            get
            {
                return this.position;
            }
        }

        public ChatSourceType SourceType
        {
            get
            {
                return this.sourceType;
            }
        }

        public ChatType Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

