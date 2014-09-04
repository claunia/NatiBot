/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : IRCSettings.cs
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

namespace bot
{
    [XmlRoot("IRCSettings")]
    public class IRCSettings
    {
        private String host;
        private String channel;
        private int port;
        private String masterNick;
        private bool useIRC;

        public bool isUsingIRC
        {
            get
            {
                return this.useIRC;
            }
            set
            {
                this.useIRC = value;
            }
        }

        public String ServerHost
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        public int ServerPort
        {
            get
            {
                return this.port;
            }
            set
            {
                this.port = value;
            }
        }

        public String MainChannel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.channel = value;
            }
        }

        public String Master
        {
            get
            {
                return this.masterNick;
            }
            set
            {
                this.masterNick = value;
            }
        }

    }
}
