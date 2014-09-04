/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : LoginGrids.cs
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
using System.Reflection;
using System.Collections.Generic;
using OpenMetaverse;

namespace bot
{
    public struct LoginGrid
    {
        public string _GridName;
        public string _GridURI;

        public LoginGrid(string GridName, string GridURI)
        {
            _GridName = GridName;
            _GridURI = GridURI;
        }
    }

    public static class LoginGrids
    {
        public readonly static LoginGrid AGNI = new LoginGrid("Second Life Production Grid (AGNI)", "https://login.agni.lindenlab.com/cgi-bin/login.cgi");
        public readonly static LoginGrid ADITI = new LoginGrid("Second Life Beta Grid (ADITI)", "https://login.aditi.lindenlab.com/cgi-bin/login.cgi");
        public readonly static LoginGrid TRDROCK = new LoginGrid("3rd Rock Grid", "http://grid.3rdrockgrid.com:8002");
        public readonly static LoginGrid AHANGOUT = new LoginGrid("Avatar Hangout", "http://login.avatarhangout.com:8002");
        public readonly static LoginGrid CUONGRID = new LoginGrid("Cuon Grid", "http://sim-linuxmain.org:8002");
        public readonly static LoginGrid CYBERLANDIA = new LoginGrid("CyberLandia", "http://grid.cyberlandia.net:8002");
        public readonly static LoginGrid DSGRID = new LoginGrid("DSGrid", "http://login.dsgrid.eu:8002");
        public readonly static LoginGrid EMNET = new LoginGrid("Emerald Network", "http://emeraldnetwork.webhop.net:8002");
        public readonly static LoginGrid GERMANGRID = new LoginGrid("German Grid", "http://germangrid.eu:8002");
        public readonly static LoginGrid GIANTGRID = new LoginGrid("Giant Grid", "http://Gianttest.no-ip.biz:8002");
        public readonly static LoginGrid GRID4US = new LoginGrid("Grid4US", "http://grid4us.net:8002");
        public readonly static LoginGrid JAMLAND = new LoginGrid("Jamland", "http://jamland.de:8002");
        public readonly static LoginGrid KGRID = new LoginGrid("K-Grid ", "http://grid.k-grid.com:8002");
        public readonly static LoginGrid LCITYONL = new LoginGrid("Legend City Online", "http://login.legendcityonline.com:9000");
        public readonly static LoginGrid LISAT = new LoginGrid("Lisat", "http://lisat.zapto.org:8002");
        public readonly static LoginGrid LOGICAMP = new LoginGrid("LogiCamp", "http://logicamp.dyndns.org:8002");
        public readonly static LoginGrid METROPOLIS = new LoginGrid("Metropolis", "http://hypergrid.org:8002");
        public readonly static LoginGrid MYOPENGRID = new LoginGrid("MyOpenGrid", "http://www.myopengrid.com:8002");
        public readonly static LoginGrid OPENKANSAI = new LoginGrid("OpenKansai", "http://os.taf-jp.com:8002");
        public readonly static LoginGrid OLGRID = new LoginGrid("Open Life Grid", "http://logingrid.net:8002");
        public readonly static LoginGrid OPENNEUL = new LoginGrid("Open-Neuland", "http://login-on.talentraspel.de:9000");
        public readonly static LoginGrid OPENVUE = new LoginGrid("Openvue", "http://virtual.aiai.ed.ac.uk:8002");
        public readonly static LoginGrid OSGRID = new LoginGrid("OS Grid", "http://osgrid.org:8002");
        public readonly static LoginGrid PSEUDOSPACE = new LoginGrid("PseudoSpace ", "http://grid.pseudospace.net:8002");
        public readonly static LoginGrid REACGRID = new LoginGrid("Reaction Grid ", "http://reactiongrid.com:8008");
        public readonly static LoginGrid SCHWEIZ = new LoginGrid("Schweiz", "http://2lifegrid.game-host.org:8002");
        public readonly static LoginGrid SEGARRA = new LoginGrid("Segarra Grid", "http://segarragrid.com:8002");
        public readonly static LoginGrid TERTIARY = new LoginGrid("Tertiary Grid", "http://tertiarygrid.com:8002");
        public readonly static LoginGrid GORGRID = new LoginGrid("The Gor Grid", "http://thegorgrid.com:8002");
        public readonly static LoginGrid NWGRID = new LoginGrid("The New World Grid", "http://grid.newworldgrid.com:8002");
        public readonly static LoginGrid TWISTEDSKY = new LoginGrid("Twisted Sky ", "http://twistedsky.net:8002");
        public readonly static LoginGrid UNICA = new LoginGrid("Unica", "http://grid.unica.it:9000");
        public readonly static LoginGrid VBUSINESS = new LoginGrid("v-Business", "http://grid.v-business.com:8002");
        public readonly static LoginGrid VIRTYOU = new LoginGrid("virtyou", "http://virtyou.com:11002");
        public readonly static LoginGrid WILDERW = new LoginGrid("WilderWesten", "http://login-ww.talentraspel.de:9000");
        public readonly static LoginGrid WSTERRA = new LoginGrid("World Sim Terra", "http://wsterra.com:8002");
        public readonly static LoginGrid YALNE = new LoginGrid("Your Alternative Life", "http://grid01.from-ne.com:8002");
        public readonly static LoginGrid YALATH = new LoginGrid("Your Alternative Life (ATH)", "http://tiog.ath.cx:8002");
        public readonly static LoginGrid LOCALHOST = new LoginGrid("Localhost", "http://127.0.0.1:9000");

        /// <summary>
        /// A dictionary containing all known client tags
        /// </summary>
        /// <returns>A dictionary containing the known client tags, 
        /// where the key is the tag ID, and the value is a string
        /// containing the client name</returns>
        public static Dictionary<string, string> ToDictionary()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Type type = typeof(LoginGrids);
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                LoginGrid _Tag = (LoginGrid)field.GetValue(type);

                dict.Add(_Tag._GridName, _Tag._GridURI);
            }
            return dict;
        }
    }
}
