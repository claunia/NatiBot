/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BotConfig.cs
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
    using System;
    using System.Xml.Serialization;

    [XmlRoot("BotConfig")]
    public class BotConfig
    {
        private CampChair lastSitposition;
        private bool saveSitPosition;

        private bool getTextures;
        private bool getSounds;
        private bool getAnimations;
        private bool informFriends;
        private bool touchMidnightMania;
        private bool haveLuck;
        private bool acceptInventoryOffers;

        public CampChair LastSitposition
        {
            get
            {
                return this.lastSitposition;
            }
            set
            {
                this.lastSitposition = value;
            }
        }

        public bool SaveSitPosition
        {
            get
            {
                return this.saveSitPosition;
            }
            set
            {
                this.saveSitPosition = value;
            }
        }

        public bool GetTextures
        {
            get
            {
                return this.getTextures;
            }
            set
            {
                this.getTextures = value;
            }
        }

        public bool GetSounds
        {
            get
            {
                return this.getSounds;
            }
            set
            {
                this.getSounds = value;
            }
        }

        public bool GetAnimations
        {
            get
            {
                return this.getAnimations;
            }
            set
            {
                this.getAnimations = value;
            }
        }

        public bool InformFriends
        {
            get
            {
                return this.informFriends;
            }
            set
            {
                this.informFriends = value;
            }
        }

        public bool TouchMidnightMania
        {
            get
            {
                return this.touchMidnightMania;
            }
            set
            {
                this.touchMidnightMania = value;
            }
        }

        public bool HaveLuck
        {
            get
            {
                return this.haveLuck;
            }
            set
            {
                this.haveLuck = value;
            }
        }

        public bool AcceptInventoryOffers
        {
            get
            {
                return this.acceptInventoryOffers;
            }
            set
            {
                this.acceptInventoryOffers = value;
            }
        }
    }
}

