/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : LoginDetails.cs
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
namespace bot
{
    using OpenMetaverse;
    using System;
    using System.Xml.Serialization;

    [XmlRoot("LoginDetails")]
    public class LoginDetails
    {
        private string author = string.Empty;
        private bot.BotConfig botConfig = new bot.BotConfig();
        private string firstName = string.Empty;
        private string grid;
        private string gridCustomLoginUri = string.Empty;
        private bool isPasswordMD5;
        private string lastName = string.Empty;
        private UUID masterKey = UUID.Zero;
        private string masterName = string.Empty;
        private string password = string.Empty;
        private string startLocation = string.Empty;
        private string userAgent = string.Empty;
        private Boolean Greet = true;
        private Boolean Aupdate = true;
        private Boolean relaychat = false;

        private IRCSettings ircSet;

        [XmlIgnore]
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
            }
        }

        public bot.BotConfig BotConfig
        {
            get
            {
                return this.botConfig;
            }
            set
            {
                this.botConfig = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;
            }
        }

        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.firstName) && !string.IsNullOrEmpty(this.lastName))
                {
                    return (this.firstName + " " + this.lastName);
                }
                return string.Empty;
            }
        }

        public string Grid
        {
            get
            {
                return this.grid;
            }
            set
            {
                this.grid = value;
            }
        }

        public string GridCustomLoginUri
        {
            get
            {
                return this.gridCustomLoginUri;
            }
            set
            {
                this.gridCustomLoginUri = value;
            }
        }

        public bool IsPasswordMD5
        {
            get
            {
                return this.isPasswordMD5;
            }
            set
            {
                this.isPasswordMD5 = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        public UUID MasterKey
        {
            get
            {
                return this.masterKey;
            }
            set
            {
                this.masterKey = value;
            }
        }

        public string MasterName
        {
            get
            {
                return this.masterName;
            }
            set
            {
                this.masterName = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string StartLocation
        {
            get
            {
                return this.startLocation;
            }
            set
            {
                this.startLocation = value;
            }
        }

        [XmlIgnore]
        public string UserAgent
        {
            get
            {
                return this.userAgent;
            }
            set
            {
                this.userAgent = value;
            }
        }

        public IRCSettings IRC_Settings
        {
            get
            {
                return this.ircSet;
            }
            set
            {
                this.ircSet = value;
            }
        }



        public Boolean GreetMaster
        {
            get
            {
                return this.Greet;
            }
            set
            {
                this.Greet = value;
            }
        }

        public Boolean SendAgentUpdatePacket
        {
            get
            {
                return this.Aupdate;
            }
            set
            {
                this.Aupdate = value;
            }
        }

        public Boolean RelayChatToIRC
        {
            get
            {
                return this.relaychat;
            }
            set
            {
                this.relaychat = value;
            }
        }
    }
}

