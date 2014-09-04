/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Chat.cs
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
    using bot.GUI;
    using System;
    using System.Collections.Generic;
    using OpenMetaverse;

    public static class Chat
    {
        public struct structInstantMessage
        {
            public SecondLifeBot client;
            public InstantMessage message;
            public Simulator simulator;
            public DateTime timestamp;
            public bool isReceived;
        }

        public struct structGeneralChat
        {
            public SecondLifeBot client;
            public string message;
            public ChatAudibleLevel audible;
            public ChatType type;
            public ChatSourceType sourceType;
            public string fromName;
            public UUID id;
            public UUID ownerid;
            public Vector3 position;
            public DateTime timestamp;
        }

        public delegate void OnIMReceivedCallback(bot.Chat.structInstantMessage message);

        public delegate void OnChatReceivedCallback(bot.Chat.structGeneralChat chat);

        public static event OnIMReceivedCallback OnIMReceived;
        public static event OnChatReceivedCallback OnChatReceived;

        private static void BotForm_OnInputSend(SecondLifeBot client, InstantMessage im, Simulator simulator, DateTime timestamp)
        {
            structInstantMessage preBuffer = new structInstantMessage();

            preBuffer.client = client;
            preBuffer.message = im;
            preBuffer.simulator = simulator;
            preBuffer.timestamp = timestamp;
            preBuffer.isReceived = false;

            receivedIM(preBuffer);
        }

        public static void Initialize(frmChat mainForm)
        {
            mainForm.OnInputSend += new frmChat.InputSendCallback(bot.Chat.BotForm_OnInputSend);
        }

        public static void receivedIM(structInstantMessage preBuffer)
        {
            if (OnIMReceived != null)
                OnIMReceived(preBuffer);
        }

        public static void receivedChat(structGeneralChat preBuffer)
        {
            if (OnChatReceived != null)
                OnChatReceived(preBuffer);
        }
    }
}
