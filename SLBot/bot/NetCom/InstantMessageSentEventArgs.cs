/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : InstantMEssageSentEventArgs.cs
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

    public class InstantMessageSentEventArgs : EventArgs
    {
        private string _message;
        private UUID _sessionId;
        private UUID _targetId;
        private DateTime _timestamp;

        public InstantMessageSentEventArgs(string message, UUID targetId, UUID sessionId, DateTime timestamp)
        {
            this._message = message;
            this._targetId = targetId;
            this._sessionId = sessionId;
            this._timestamp = timestamp;
        }

        public string Message
        {
            get
            {
                return this._message;
            }
        }

        public UUID SessionId
        {
            get
            {
                return this._sessionId;
            }
        }

        public UUID TargetId
        {
            get
            {
                return this._targetId;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return this._timestamp;
            }
        }
    }
}

