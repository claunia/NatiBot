/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ImportOutfitCommand.cs
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
/*namespace bot.Commands
{
    using bot;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using OpenMetaverse.StructuredData;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;

    public class ImportOutfitCommand : Command
    {
        private Vector3 currentPosition;
        private Primitive currentPrim;
        private List<uint> linkQueue;
        private AutoResetEvent primDone = new AutoResetEvent(false);
        private List<Primitive> primsCreated;
        private uint rootLocalID;
        private ImporterState state = ImporterState.Idle;
        private uint SerialNum = 2;

        private enum ImporterState
        {
            RezzingParent,
            RezzingChildren,
            Linking,
            Idle
        }

        private class Linkset
        {
            public List<Primitive> Children;
            public Primitive RootPrim;

            public Linkset()
            {
                this.Children = new List<Primitive>();
                this.RootPrim = new Primitive();
            }

            public Linkset(Primitive rootPrim)
            {
                this.Children = new List<Primitive>();
                this.RootPrim = rootPrim;
            }
        }

        public ImportOutfitCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "importoutfit";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ImportOutfit.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ImportOutfit.Usage");
           
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
        /*    if (args.Length != 1)
            {
                return "Uso: importoutfit apariencia.xml";
            }
            string path = args[0];
            string str2;
            try
            {
                str2 = File.ReadAllText(path);
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
            if (str2.Length == 0)
                return "El archivo exportado está dañado.";

            AvatarAppearancePacket packet= (AvatarAppearancePacket)AvatarAppearancePacket.FromLLSD(LLSDParser.DeserializeXml(str2));
            AgentSetAppearancePacket packet2 = new AgentSetAppearancePacket();

            packet2.AgentData.AgentID = base.Client.Self.AgentID;
            packet2.AgentData.SessionID = base.Client.Self.SessionID;
            packet2.AgentData.SerialNum = this.SerialNum++;
            packet2.AgentData.Size = new Vector3(2f, 2f, 2f);
            packet2.WearableData = new AgentSetAppearancePacket.WearableDataBlock[0];
            packet2.VisualParam = new AgentSetAppearancePacket.VisualParamBlock[packet.VisualParam.Length];
            for (int j = 0; j < packet.VisualParam.Length; j++)
            {
                packet2.VisualParam[j] = new AgentSetAppearancePacket.VisualParamBlock();
                packet2.VisualParam[j].ParamValue = packet.VisualParam[j].ParamValue;
            }
            packet2.ObjectData.TextureEntry = packet.ObjectData.TextureEntry;
            base.Client.Appearance.AddAttachments(new List<InventoryBase>(), true);
            base.Client.Network.SendPacket(packet2);
            return ("Importado" + args[0]);*/
/*            return bot.Localization.clResourceManager.getText("Exception");
        }
    }
}

*/