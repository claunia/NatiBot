/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ClientDisconnectEventArgs.cs
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

    public class ClientDisconnectEventArgs : EventArgs
    {
        private string message;
        private NetworkManager.DisconnectType type;

        public ClientDisconnectEventArgs(NetworkManager.DisconnectType type, string message)
        {
            this.type = type;
            this.message = message;
        }

        public string Message
        {
            get
            {
                return this.message;
            }
        }

        public NetworkManager.DisconnectType Type
        {
            get
            {
                return this.type;
            }
        }
    }
}

