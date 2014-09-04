/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ClientTags.cs
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
    public struct ClientTag
    {
        public UUID _ClientID;
        public string _ClientName;

        public ClientTag(UUID ClientID, string ClientName)
        {
            _ClientID = ClientID;
            _ClientName = ClientName;
        }
    }

    public static class ClientTags
    {
        public readonly static ClientTag MOYMIX = new ClientTag(UUID.Parse("0bcd5f5d-a4ce-9ea4-f9e8-15132653b3d8"), "MoyMix");
        public readonly static ClientTag CRYOLIFE = new ClientTag(UUID.Parse("0f6723d2-5b23-6b58-08ab-308112b33786"), "CryoLife");
        public readonly static ClientTag VERTICALLIFE = new ClientTag(UUID.Parse("11ad2452-ce54-8d65-7c23-05589b59f516"), "VerticalLife");
        public readonly static ClientTag GEMINI = new ClientTag(UUID.Parse("1c29480c-c608-df87-28bb-964fb64c5366"), "Gemini");
        public readonly static ClientTag MEERKAT = new ClientTag(UUID.Parse("2a9a406c-f448-68f2-4e38-878f8c46c190"), "Meerkat");
        public readonly static ClientTag PHOXSL = new ClientTag(UUID.Parse("2c9c1e0b-e5d1-263e-16b1-7fc6d169f3d6"), "PhoxSL");
        public readonly static ClientTag VERTICALLIFE2 = new ClientTag(UUID.Parse("3ab7e2fa-9572-ef36-1a30-d855dbea4f92"), "VerticalLife");
        public readonly static ClientTag SAPPHIRE = new ClientTag(UUID.Parse("4e8dcf80-336b-b1d8-ef3e-08dacf015a0f"), "Sapphire");
        public readonly static ClientTag VERTICALLIFE3 = new ClientTag(UUID.Parse("58a8b7ec-1455-7162-5d96-d3c3ead2ed71"), "VerticalLife");
        public readonly static ClientTag LGG = new ClientTag(UUID.Parse("5aa5c70d-d787-571b-0495-4fc1bdef1500"), "LGG proxy");
        public readonly static ClientTag PSL = new ClientTag(UUID.Parse("77662f23-c77a-9b4d-5558-26b757b2144c"), "PSL");
        public readonly static ClientTag KUNGFU = new ClientTag(UUID.Parse("7c4d47a3-0c51-04d1-fa47-e4f3ac12f59b"), "Kung Fu");
        public readonly static ClientTag DAYOH = new ClientTag(UUID.Parse("8183e823-c443-2142-6eb6-2ab763d4f81c"), "Day Oh proxy");
        public readonly static ClientTag INFINITY = new ClientTag(UUID.Parse("81b3e921-ee31-aa57-ff9b-ec1f28e41da1"), "Infinity");
        public readonly static ClientTag VERTICALLIFE4 = new ClientTag(UUID.Parse("841ef25b-3b90-caf9-ea3d-5649e755db65"), "VerticalLife");
        public readonly static ClientTag FAG = new ClientTag(UUID.Parse("872c0005-3095-0967-866d-11cd71115c22"), "<-- Fag");
        public readonly static ClientTag BETALIFE = new ClientTag(UUID.Parse("9422e9d7-7b11-83e4-6262-4a8db4716a3b"), "BetaLife");
        public readonly static ClientTag TYK3N = new ClientTag(UUID.Parse("adcbe893-7643-fd12-f61c-0b39717e2e32"), "tyk3n");
        public readonly static ClientTag MERKAT2 = new ClientTag(UUID.Parse("b6820989-bf42-ff59-ddde-fd3fd3a74fe4"), "Meerkat");
        public readonly static ClientTag VLIFE = new ClientTag(UUID.Parse("c252d89d-6f7c-7d90-f430-d140d2e3fbbe"), "VLife");
        public readonly static ClientTag KUNGFU2 = new ClientTag(UUID.Parse("c5b570ca-bb7e-3c81-afd1-f62646b20014"), "Kung Fu");
        public readonly static ClientTag RUBY = new ClientTag(UUID.Parse("ccb509cf-cc69-e569-38f1-5086c687afd1"), "Ruby");
        public readonly static ClientTag EMERALD = new ClientTag(UUID.Parse("ccda2b3b-e72c-a112-e126-fee238b67218"), "Emerald");
        public readonly static ClientTag RIVLIFE = new ClientTag(UUID.Parse("d3eb4a5f-aec5-4bcb-b007-cce9efe89d37"), "rivlife");
        public readonly static ClientTag CRYOLIFE2 = new ClientTag(UUID.Parse("e52d21f7-3c8b-819f-a3db-65c432295dac"), "CryoLife");
        public readonly static ClientTag VERTICALLIFE5 = new ClientTag(UUID.Parse("e734563e-1c31-2a35-3ed5-8552c807439f"), "VerticalLife");
        public readonly static ClientTag PSL2 = new ClientTag(UUID.Parse("f3fd74a6-fee7-4b2f-93ae-ddcb5991da04"), "PSL");
        public readonly static ClientTag NEILLIFE = new ClientTag(UUID.Parse("f5a48821-9a98-d09e-8d6a-50cc08ba9a47"), "NeilLife");
        public readonly static ClientTag CORGI = new ClientTag(UUID.Parse("ffce04ff-5303-4909-a044-d37af7ab0b0e"), "Corgi");

        /// <summary>
        /// A dictionary containing all known client tags
        /// </summary>
        /// <returns>A dictionary containing the known client tags, 
        /// where the key is the tag ID, and the value is a string
        /// containing the client name</returns>
        public static Dictionary<UUID, string> ToDictionary()
        {
            Dictionary<UUID, string> dict = new Dictionary<UUID, string>();
            Type type = typeof(ClientTags);
            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                ClientTag _Tag = (ClientTag)field.GetValue(type);

                dict.Add(_Tag._ClientID, _Tag._ClientName);
            }
            return dict;
        }
    }
}
