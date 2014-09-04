/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : NetstatsCommand.cs
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
using OpenMetaverse;
using OpenMetaverse.Packets;

namespace bot.Commands
{
    public class NetstatsCommand : Command
    {
        public NetstatsCommand(SecondLifeBot secondLifeBot)
        {
            Name = "netstats";
            Description = bot.Localization.clResourceManager.getText("Commands.NetStats.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder output = new StringBuilder();
            if (!Client.Settings.TRACK_UTILIZATION)
            {
                return bot.Localization.clResourceManager.getText("Commands.NetStats.NoStats");
            }


            StringBuilder packetOutput = new StringBuilder();
            StringBuilder capsOutput = new StringBuilder();

            packetOutput.AppendFormat("{0,-30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, "Packet Name", "Sent", "Recv",
                " TX Bytes ", " RX Bytes ");

            capsOutput.AppendFormat("{0,-30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, "Message Name", "Sent", "Recv",
                " TX Bytes ", " RX Bytes ");
            //                "    RX    "

            long packetsSentCount = 0;
            long packetsRecvCount = 0;
            long packetBytesSent = 0;
            long packetBytesRecv = 0;

            long capsSentCount = 0;
            long capsRecvCount = 0;
            long capsBytesSent = 0;
            long capsBytesRecv = 0;

            foreach (KeyValuePair<string, OpenMetaverse.Stats.UtilizationStatistics.Stat> kvp in Client.Stats.GetStatistics())
            {
                if (kvp.Value.Type == OpenMetaverse.Stats.Type.Message)
                {                              
                    capsOutput.AppendFormat("{0,-30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, kvp.Key, kvp.Value.TxCount, kvp.Value.RxCount,
                        FormatBytes(kvp.Value.TxBytes), FormatBytes(kvp.Value.RxBytes));

                    capsSentCount += kvp.Value.TxCount;
                    capsRecvCount += kvp.Value.RxCount;
                    capsBytesSent += kvp.Value.TxBytes;
                    capsBytesRecv += kvp.Value.RxBytes;
                }
                else if (kvp.Value.Type == OpenMetaverse.Stats.Type.Packet)
                {
                    packetOutput.AppendFormat("{0,-30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, kvp.Key, kvp.Value.TxCount, kvp.Value.RxCount, 
                        FormatBytes(kvp.Value.TxBytes), FormatBytes(kvp.Value.RxBytes));

                    packetsSentCount += kvp.Value.TxCount;
                    packetsRecvCount += kvp.Value.RxCount;
                    packetBytesSent += kvp.Value.TxBytes;
                    packetBytesRecv += kvp.Value.RxBytes;
                }
            }

            capsOutput.AppendFormat("{0,30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, "Capabilities Totals", capsSentCount, capsRecvCount,
                FormatBytes(capsBytesSent), FormatBytes(capsBytesRecv));

            packetOutput.AppendFormat("{0,30}|{1,4}|{2,4}|{3,-10}|{4,-10}|" + System.Environment.NewLine, "Packet Totals", packetsSentCount, packetsRecvCount,
                FormatBytes(packetBytesSent), FormatBytes(packetBytesRecv));

            return System.Environment.NewLine + capsOutput.ToString() + System.Environment.NewLine + System.Environment.NewLine + packetOutput.ToString();
        }

        public string FormatBytes(long bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GiB", "MiB", "KiB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (bytes > max)
                    return string.Format("{0:##.##} {1}", decimal.Divide(bytes, max), order);

                max /= scale;
            }
            return "0";
        }
    }
}
