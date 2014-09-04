/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : StartLocationParser.cs
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
    using System;

    public class StartLocationParser
    {
        private string location;

        public StartLocationParser(string location)
        {
            if (location == null)
            {
                throw new Exception("Location cannot be null.");
            }
            this.location = location;
        }

        private string GetSim(string location)
        {
            if (!location.Contains("/"))
            {
                return location;
            }
            return location.Split(new char[] { '/' })[0];
        }

        private int GetX(string location)
        {
            int num;
            if (location.Contains("/") && int.TryParse(location.Split(new char[] { '/' })[1], out num))
            {
                return num;
            }
            return 0x80;
        }

        private int GetY(string location)
        {
            int num;
            if (location.Contains("/") && int.TryParse(location.Split(new char[] { '/' })[2], out num))
            {
                return num;
            }
            return 0x80;
        }

        private int GetZ(string location)
        {
            int num;
            if (location.Contains("/") && int.TryParse(location.Split(new char[] { '/' })[3], out num))
            {
                return num;
            }
            return 0;
        }

        public string Sim
        {
            get
            {
                return this.GetSim(this.location);
            }
        }

        public int X
        {
            get
            {
                return this.GetX(this.location);
            }
        }

        public int Y
        {
            get
            {
                return this.GetY(this.location);
            }
        }

        public int Z
        {
            get
            {
                return this.GetZ(this.location);
            }
        }
    }
}

