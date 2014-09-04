/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DetectLindensCommand.cs
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
    using libsecondlife;
    using libsecondlife.Packets;
    using System;

    public class DetectBotCommand : Command
    {
        public DetectBotCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "detectlindens";
            base.Description = "Runs in the background, reporting any potential bots";
            SecondLifeBot.Network.RegisterCallback(PacketType.AgentUpdate, new NetworkManager.PacketCallback(this.AgentUpdatePacketHandler));
        }

        private void AvatarAppearanceHandler(Packet packet, Simulator simulator)
        {
            AvatarAppearancePacket packet2 = (AvatarAppearancePacket)packet;
            packet2.ObjectData[
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            return "This command is always running";
        }

        private bool IsNullOrZero(LLObject.TextureEntryFace face)
        {
            if (face != null)
            {
                return (face.TextureID == UUID.Zero);
            }
            return true;
        }
    }
}
*/

