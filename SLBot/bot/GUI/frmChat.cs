/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmChat.cs
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
namespace bot.GUI
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using OpenMetaverse;
    using clControls;
    using System.Threading;
    using System.IO;
    using Claunia.clUtils;

    public partial class frmChat : Form
    {
        public delegate void InputSendCallback(SecondLifeBot client,InstantMessage im,Simulator simulator,DateTime timestamp);

        public event InputSendCallback OnInputSend;

        private bool isGroupIM;
        private string groupName;
        private UUID messageID;
        ManualResetEvent GetGroupNameEvent = new ManualResetEvent(false);
        ManualResetEvent JoinGroupChatEvent = new ManualResetEvent(false);

        private LanguageCodes _langCodes;

        public delegate void RealOnIMReceivedCallback(bot.Chat.structInstantMessage message);

        public delegate void RealOnChatReceivedCallback(bot.Chat.structGeneralChat chat);

        RealOnIMReceivedCallback RealOnIMReceived;
        RealOnChatReceivedCallback RealOnChatReceived;

        private bool AutoTranslate;
        private string FromLanguage;
        private string ToLanguage;
        private string translatedString;

        private struct Chat
        {
            public SecondLifeBot client;
            public bool isGeneralChat;
            public bool isGroupChat;
            public string fromAgentName;
            public UUID fromAgentUUID;
            public StringBuilder chat;
        }

        private List<Chat> chats;

        private Point mouse_offset;

        public frmChat()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();

            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");

            this.Text = bot.Localization.clResourceManager.getText("frmChat.Text");
            this.lblChat.Text = bot.Localization.clResourceManager.getText("frmChat.Text");
            this.chkAutoTranslate.Text = bot.Localization.clResourceManager.getText("frmChat.chkAutoTranslate");
            this.lblToLanguage.Text = bot.Localization.clResourceManager.getText("frmChat.lblToLanguage");

            this.Icon = bot.Localization.clResourceManager.getIcon();

            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmChat");

            bot.Chat.Initialize(this);
            bot.Chat.OnIMReceived += new bot.Chat.OnIMReceivedCallback(this.receivedIM);
            bot.Chat.OnChatReceived += new bot.Chat.OnChatReceivedCallback(this.receivedChat);
            _langCodes = new LanguageCodes();
            chats = new List<Chat>();
            RealOnIMReceived = new RealOnIMReceivedCallback(Real_receivedIM);
            RealOnChatReceived = new RealOnChatReceivedCallback(Real_receivedChat);

            AutoTranslate = false;
            chkAutoTranslate.Checked = AutoTranslate;
            ToLanguage = "";
            FromLanguage = "";
            cmbFromLanguage.Enabled = false;
            cmbToLanguage.Enabled = false;
        }

        private void receivedIM(bot.Chat.structInstantMessage message)
        {
            this.Invoke(RealOnIMReceived, message);
        }

        private void receivedChat(bot.Chat.structGeneralChat chat)
        {
            this.Invoke(RealOnChatReceived, chat);
        }


        private void searchForClient(SecondLifeBot client)
        {
            bool found = false;

            for (int i = 0; i < cmbBots.Items.Count; i++)
            {
                if (cmbBots.Items[i].ToString() == client.ToString())
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                cmbBots.Items.Add(client.ToString());
            }

            return;
        }

        private void Real_receivedChat(bot.Chat.structGeneralChat chat)
        {
            bool found = false;
            string strMessage;
            string translateMessage;

            if (chat.type == ChatType.StartTyping || chat.type == ChatType.StopTyping)
                return;

            if (chat.client.Account.LoginDetails.BotConfig.HaveLuck == true && chat.sourceType == ChatSourceType.Object)
                doIHaveLuck(chat);

            searchForClient(chat.client);

            for (int i = 0; i < chats.Count; i++)
            {
                if (chats[i].isGeneralChat == true)
                {
                    if (chats[i].client == chat.client)
                    {
                        found = true;
                        strMessage = "[" + chat.timestamp.Hour.ToString("00") + ":" + chat.timestamp.Minute.ToString("00") + "] ";
                        switch (chat.type)
                        {
                            case ChatType.Whisper:
                                strMessage += chat.fromName + " " + bot.Localization.clResourceManager.getText("frmChat.Whispers") + " ";
                                break;
                            case ChatType.Shout:
                                strMessage += chat.fromName + " " + bot.Localization.clResourceManager.getText("frmChat.Shouts") + " ";
                                break;
                            default:
                                strMessage += chat.fromName + ": ";
                                break;
                        }
                        if (chat.message == "")
                            break;
                        if (!AutoTranslate)
                        {
                            strMessage += chat.message;
                            chats[i].chat.AppendLine(strMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(chat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), strMessage, false);
                            }
                        }
                        else
                        {
                            translateMessage = Utilities.TranslateText(chat.message, FromLanguage, ToLanguage);
                            translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                            strMessage += chat.message;
                            chats[i].chat.AppendLine(strMessage);
                            chats[i].chat.AppendLine(translateMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(chat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), strMessage, false);
                                LogChat(chat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), translateMessage, false);
                            }
                        }
                        if (cmbBots.SelectedItem != null)
                        if (cmbBots.SelectedItem.ToString() == chat.client.ToString())
                        if (lstChatters.SelectedItem != null)
                        if (lstChatters.SelectedItem.ToString() == bot.Localization.clResourceManager.getText("frmChat.Chat"))
                            rtbChat.Text = chats[i].chat.ToString();
                        break;
                    }
                }
            }

            if (!found)
            {
                Chat newchat = new Chat();

                newchat.chat = new StringBuilder();

                newchat.client = chat.client;
                newchat.fromAgentName = bot.Localization.clResourceManager.getText("frmChat.Chat");
                newchat.fromAgentUUID = UUID.Zero;
                newchat.isGeneralChat = true;
                if (cmbBots.SelectedItem != null)
                if (cmbBots.SelectedItem.ToString() == chat.client.ToString())
                    lstChatters.Items.Add(bot.Localization.clResourceManager.getText("frmChat.Chat"));

                strMessage = "[" + chat.timestamp.Hour.ToString("00") + ":" + chat.timestamp.Minute.ToString("00") + "] ";
                switch (chat.type)
                {
                    case ChatType.Whisper:
                        strMessage += chat.fromName + " " + bot.Localization.clResourceManager.getText("frmChat.Whispers") + " ";
                        break;
                    case ChatType.Shout:
                        strMessage += chat.fromName + " " + bot.Localization.clResourceManager.getText("frmChat.Shouts") + " ";
                        break;
                    default:
                        strMessage += chat.fromName + ": ";
                        break;
                }

                if (!AutoTranslate)
                {
                    strMessage += chat.message;
                    newchat.chat.AppendLine(strMessage);
                    if (Program.getWriteChatToFileSetting())
                    {
                        LogChat(newchat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), strMessage, false);
                    }
                }
                else
                {
                    translateMessage = Utilities.TranslateText(chat.message, FromLanguage, ToLanguage);
                    translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                    strMessage += chat.message;
                    newchat.chat.AppendLine(strMessage);
                    newchat.chat.AppendLine(translateMessage);
                    if (Program.getWriteChatToFileSetting())
                    {
                        LogChat(newchat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), strMessage, false);
                        LogChat(newchat.client.ToString(), bot.Localization.clResourceManager.getText("frmChat.Chat"), translateMessage, false);
                    }
                }

                chats.Add(newchat);
            }
        }

        private void doIHaveLuck(bot.Chat.structGeneralChat chat)
        {
            if (chat.sourceType != ChatSourceType.Object)
                return;

            // This should work for almost all lucky chairs
            if (chat.fromName.ToLowerInvariant().Contains("lucky") && chat.fromName.ToLowerInvariant().Contains("chair"))
            {
                if (chat.message.StartsWith("Looking for a winner whose name begins with....", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.SitLuckyChair"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("siton " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
                else if (chat.message.StartsWith("I'm looking for a player whose name begins with", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(48, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(48, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.SitLuckyChair"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("siton " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
            }

            // Identical strings than a lucky chair, strangely, it is sit based. Would you sit on your presents on RL?
            if (chat.fromName.ToLowerInvariant().Contains("lucky") && chat.fromName.ToLowerInvariant().Contains("present"))
            {
                if (chat.message.StartsWith("Looking for a winner whose name begins with....", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.SitLuckyPresent"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("siton " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
                else if (chat.message.StartsWith("I'm looking for a player whose name begins with", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(48, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(48, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.SitLuckyPresent"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("siton " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
            }

            if (chat.fromName.ToLowerInvariant().Contains("lucky") && chat.fromName.ToLowerInvariant().Contains("santa"))
            {
                if (chat.message.StartsWith("This is a special Christmas present for somebody whose name begins with", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.SitLuckySanta"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("siton " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
            }

            // This should work for almost all lucky boards
            if (chat.fromName.ToLowerInvariant().Contains("lucky") && chat.fromName.ToLowerInvariant().Contains("board"))
            {
                if (chat.message.StartsWith("Looking for a winner whose name begins with....", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(chat.message.Length - 2, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.TouchLuckyBoard"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("touch " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
                else if (chat.message.StartsWith("I'm looking for a player whose name begins with", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(48, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(48, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.TouchLuckyBoard"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("touch " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
                else if (chat.message.StartsWith("Now looking for a winner whose name begins with...", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (chat.message.Substring(51, 1).ToLowerInvariant() == chat.client.ToString().Substring(0, 1).ToLowerInvariant()
                        || chat.message.Substring(51, 1).ToLowerInvariant() == "?")
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("frmChat.TouchLuckyBoard"), chat.client.ToString(), chat.id.ToString(), chat.message);
                        chat.client.DoCommand("touch " + chat.id.ToString(), UUID.Zero, false);
                    }
                }
            }
        }

        private void Real_receivedIM(bot.Chat.structInstantMessage message)
        {
            bool found = false;
            string strMessage;
            string fromAvatarName = "";
            string translateMessage;

            if (message.message.Dialog != InstantMessageDialog.StartTyping && message.message.Dialog != InstantMessageDialog.StopTyping)
            {
                searchForClient(message.client);

                messageID = message.message.IMSessionID;

                isGroupIM = isGroup(message.message, message.client);

                if (message.isReceived)
                {
                    lock (chats)
                    {
                        for (int i = 0; i < chats.Count; i++)
                        {
                            if (chats[i].client == message.client)
                            {
                                if (!isGroupIM)
                                {
                                    if (chats[i].fromAgentUUID == message.message.FromAgentID)
                                    {
                                        found = true;
                                        if (message.message.Timestamp == DateTime.Parse("01/01/0001 0:00:00"))
                                        {
                                            strMessage = "[" + message.timestamp.Hour.ToString("00") + ":" + message.timestamp.Minute.ToString("00") + "] ";
                                        }
                                        else
                                        {
                                            strMessage = "[" + message.message.Timestamp.Hour.ToString("00") + ":" + message.message.Timestamp.Minute.ToString("00") + " " + bot.Localization.clResourceManager.getText("frmChat.Offline") + "] ";
                                        }

                                        switch (message.message.Dialog)
                                        {
                                            case InstantMessageDialog.MessageFromAgent:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += message.message.FromAgentName + ": ";
                                                break;
                                            case InstantMessageDialog.MessageFromObject:
                                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                                {
                                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                                }
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Object"),
                                                    message.message.FromAgentName,
                                                    fromAvatarName,
                                                    message.message.RegionID.ToString(),
                                                    message.message.Position.ToString());
                                                break;
                                            case InstantMessageDialog.TaskInventoryOffered:
                                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                                {
                                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                                }
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.TaskInventoryOffered"),
                                                    message.message.FromAgentName,
                                                    fromAvatarName,
                                                    message.message.RegionID.ToString(),
                                                    message.message.Position.ToString());
                                                break;
                                            case InstantMessageDialog.GroupNotice:
                                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                                {
                                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                                }
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.GroupNotice"),
                                                    message.message.FromAgentName,
                                                    fromAvatarName);
                                                break;
                                            case InstantMessageDialog.RequestTeleport:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Teleport"),
                                                    message.message.FromAgentName,
                                                    message.message.RegionID.ToString(),
                                                    message.message.Position.ToString());
                                                break;
                                            case InstantMessageDialog.FriendshipAccepted:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.FriendshipAccepted"),
                                                    message.message.FromAgentName);
                                                break;
                                            case InstantMessageDialog.FriendshipDeclined:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.FriendshipDeclined"),
                                                    message.message.FromAgentName);
                                                break;
                                            case InstantMessageDialog.InventoryAccepted:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryAccepted"),
                                                    message.message.FromAgentName);
                                                break;
                                            case InstantMessageDialog.InventoryDeclined:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryDeclined"),
                                                    message.message.FromAgentName);
                                                break;
                                            case InstantMessageDialog.InventoryOffered:
                                                fromAvatarName = message.message.FromAgentName;
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryOffered"),
                                                    message.message.FromAgentName);
                                                break;
                                            default:
                                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                                {
                                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                                }
                                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Unhandled"),
                                                    message.message.FromAgentName,
                                                    message.message.RegionID.ToString(),
                                                    message.message.Position.ToString(),
                                                    message.message.Dialog.ToString());
                                                break;
                                        }

                                        if (!AutoTranslate)
                                        {
                                            strMessage += message.message.Message;
                                            chats[i].chat.AppendLine(strMessage);
                                            if (Program.getWriteChatToFileSetting())
                                            {
                                                LogChat(message.client.ToString(), fromAvatarName, strMessage, false);
                                            }
                                        }
                                        else
                                        {
                                            translateMessage = Utilities.TranslateText(message.message.Message, FromLanguage, ToLanguage);
                                            translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                                            strMessage += message.message.Message;
                                            chats[i].chat.AppendLine(strMessage);
                                            chats[i].chat.AppendLine(translateMessage);
                                            if (Program.getWriteChatToFileSetting())
                                            {
                                                LogChat(message.client.ToString(), fromAvatarName, strMessage, false);
                                                LogChat(message.client.ToString(), fromAvatarName, translateMessage, false);
                                            }
                                        }

                                        if (cmbBots.SelectedItem != null)
                                        if (cmbBots.SelectedItem.ToString() == message.client.ToString())
                                        if (lstChatters.SelectedItem != null)
                                        if (lstChatters.SelectedItem.ToString() == fromAvatarName)
                                            rtbChat.Text = chats[i].chat.ToString();
                                        break;
                                    }
                                }
                                else
                                {
                                    if (chats[i].fromAgentUUID == message.message.IMSessionID)
                                    {
                                        found = true;
                                        strMessage = "[" + message.timestamp.Hour.ToString("00") + ":" + message.timestamp.Minute.ToString("00") + "] ";

                                        strMessage += message.message.FromAgentName + ": ";

                                        if (!AutoTranslate)
                                        {
                                            strMessage += message.message.Message;
                                            chats[i].chat.AppendLine(strMessage);
                                            if (Program.getWriteChatToFileSetting())
                                            {
                                                LogChat(message.client.ToString(), chats[i].fromAgentName, strMessage, false);
                                            }
                                        }
                                        else
                                        {
                                            translateMessage = Utilities.TranslateText(message.message.Message, FromLanguage, ToLanguage);
                                            translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                                            strMessage += message.message.Message;
                                            chats[i].chat.AppendLine(strMessage);
                                            chats[i].chat.AppendLine(translateMessage);
                                            if (Program.getWriteChatToFileSetting())
                                            {
                                                LogChat(message.client.ToString(), chats[i].fromAgentName, strMessage, false);
                                                LogChat(message.client.ToString(), chats[i].fromAgentName, translateMessage, false);
                                            }
                                        }

                                        if (cmbBots.SelectedItem != null)
                                        if (cmbBots.SelectedItem.ToString() == message.client.ToString())
                                        if (lstChatters.SelectedItem != null)
                                        if (lstChatters.SelectedItem.ToString() == groupName)
                                            rtbChat.Text = chats[i].chat.ToString();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chats.Count; i++)
                    {
                        if (chats[i].client == message.client)
                        {
                            if (chats[i].fromAgentUUID == message.message.FromAgentID)
                            {
                                found = true;
                                strMessage = "[" + message.timestamp.Hour.ToString("00") + ":" + message.timestamp.Minute.ToString("00") + "] ";
                                strMessage += message.client.ToString() + ": ";
                                strMessage += message.message.Message;
                                if (Program.getWriteChatToFileSetting())
                                {
                                    LogChat(message.client.ToString(), message.message.FromAgentName, strMessage, false);
                                }
                                chats[i].chat.AppendLine(strMessage);
                                if (cmbBots.SelectedItem != null)
                                if (cmbBots.SelectedItem.ToString() == message.client.ToString())
                                if (lstChatters.SelectedItem != null)
                                if (lstChatters.SelectedItem.ToString() == message.message.FromAgentName)
                                    rtbChat.Text = chats[i].chat.ToString();
                                break;
                            }
                        }
                    }
                }

                if (!found)
                {
                    Chat newchat = new Chat();

                    newchat.chat = new StringBuilder();

                    if (!isGroupIM)
                    {
                        if (message.message.Timestamp == DateTime.Parse("01/01/0001 0:00:00"))
                        {
                            strMessage = "[" + message.timestamp.Hour.ToString("00") + ":" + message.timestamp.Minute.ToString("00") + "] ";
                        }
                        else
                        {
                            strMessage = "[" + message.message.Timestamp.Hour.ToString("00") + ":" + message.message.Timestamp.Minute.ToString("00") + " (offline)] ";
                        }

                        switch (message.message.Dialog)
                        {
                            case InstantMessageDialog.MessageFromAgent:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += message.message.FromAgentName + ": ";
                                break;
                            case InstantMessageDialog.MessageFromObject:
                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                {
                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                }
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Object"),
                                    message.message.FromAgentName,
                                    fromAvatarName,
                                    message.message.RegionID.ToString(),
                                    message.message.Position.ToString());
                                break;
                            case InstantMessageDialog.TaskInventoryOffered:
                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                {
                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                }
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.TaskInventoryOffered"),
                                    message.message.FromAgentName,
                                    fromAvatarName,
                                    message.message.RegionID.ToString(),
                                    message.message.Position.ToString());
                                break;
                            case InstantMessageDialog.GroupNotice:
                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                {
                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                }
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.GroupNotice"),
                                    message.message.FromAgentName,
                                    fromAvatarName);
                                break;
                            case InstantMessageDialog.FriendshipAccepted:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.FriendshipAccepted"),
                                    message.message.FromAgentName);
                                break;
                            case InstantMessageDialog.FriendshipDeclined:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.FriendshipDeclined"),
                                    message.message.FromAgentName);
                                break;
                            case InstantMessageDialog.InventoryAccepted:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryAccepted"),
                                    message.message.FromAgentName);
                                break;
                            case InstantMessageDialog.InventoryDeclined:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryDeclined"),
                                    message.message.FromAgentName);
                                break;
                            case InstantMessageDialog.InventoryOffered:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.InventoryOffered"),
                                    message.message.FromAgentName);
                                break;
                            case InstantMessageDialog.RequestTeleport:
                                fromAvatarName = message.message.FromAgentName;
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Teleport"),
                                    message.message.FromAgentName,
                                    message.message.RegionID.ToString(),
                                    message.message.Position.ToString());
                                break;
                            default:
                                if (!message.client.key2Name(message.message.FromAgentID, out fromAvatarName))
                                {
                                    fromAvatarName = bot.Localization.clResourceManager.getText("frmChat.Unknown");
                                }
                                strMessage += String.Format(bot.Localization.clResourceManager.getText("frmChat.Unhandled"),
                                    message.message.FromAgentName,
                                    message.message.RegionID.ToString(),
                                    message.message.Position.ToString(),
                                    message.message.Dialog.ToString());
                                break;
                        }

                        if (!AutoTranslate)
                        {
                            strMessage += message.message.Message;
                            newchat.chat.AppendLine(strMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(message.client.ToString(), fromAvatarName, strMessage, false);
                            }
                        }
                        else
                        {
                            translateMessage = Utilities.TranslateText(message.message.Message, FromLanguage, ToLanguage);
                            translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                            strMessage += message.message.Message;
                            newchat.chat.AppendLine(strMessage);
                            newchat.chat.AppendLine(translateMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(message.client.ToString(), fromAvatarName, strMessage, false);
                                LogChat(message.client.ToString(), fromAvatarName, translateMessage, false);
                            }
                        }

                        newchat.client = message.client;
                        newchat.fromAgentName = fromAvatarName;
                        newchat.fromAgentUUID = message.message.FromAgentID;
                        newchat.isGeneralChat = false;
                        newchat.isGroupChat = false;
                        if (cmbBots.SelectedItem != null)
                        if (cmbBots.SelectedItem.ToString() == message.client.ToString())
                            lstChatters.Items.Add(fromAvatarName);
                    }
                    else
                    {
                        strMessage = "[" + message.timestamp.Hour.ToString("00") + ":" + message.timestamp.Minute.ToString("00") + "] ";

                        strMessage += message.message.FromAgentName + ": ";

                        if (!AutoTranslate)
                        {
                            strMessage += message.message.Message;
                            newchat.chat.AppendLine(strMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(message.client.ToString(), groupName, strMessage, false);
                            }
                        }
                        else
                        {
                            translateMessage = Utilities.TranslateText(message.message.Message, FromLanguage, ToLanguage);
                            translateMessage = strMessage + bot.Localization.clResourceManager.getText("frmChat.Translated") + translateMessage;
                            strMessage += message.message.Message;
                            newchat.chat.AppendLine(strMessage);
                            newchat.chat.AppendLine(translateMessage);
                            if (Program.getWriteChatToFileSetting())
                            {
                                LogChat(message.client.ToString(), groupName, strMessage, false);
                                LogChat(message.client.ToString(), groupName, translateMessage, false);
                            }
                        }

                        newchat.client = message.client;
                        newchat.fromAgentName = groupName;
                        newchat.fromAgentUUID = message.message.IMSessionID;
                        newchat.isGeneralChat = false;
                        newchat.isGroupChat = true;
                        if (cmbBots.SelectedItem != null)
                        if (cmbBots.SelectedItem.ToString() == message.client.ToString())
                            lstChatters.Items.Add(groupName);
                    }
                    chats.Add(newchat);
                }
            }

            return;
        }

        void OnGroupNames(Dictionary<UUID, string> groupNames)
        {
            if (groupNames[messageID] != "")
            {
                isGroupIM = true;
                groupName = groupNames[messageID];
            }

            GetGroupNameEvent.Set();
        }

        private void frmChat_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> kvp in _langCodes.LangCodes)
            {
                cmbFromLanguage.Items.Add(kvp.Key);
                cmbToLanguage.Items.Add(kvp.Key);
            }
            cmbFromLanguage.SelectedIndex = 0;
            cmbToLanguage.SelectedIndex = 0;
        }

        private void lstChatters_SelectedValueChanged(object sender, EventArgs e)
        {
            if (lstChatters.SelectedItem != null)
            {
                for (int i = 0; i < chats.Count; i++)
                {
                    if (chats[i].client.ToString() == cmbBots.SelectedItem.ToString())
                    {
                        if (chats[i].fromAgentName == lstChatters.SelectedItem.ToString())
                        {
                            rtbChat.Text = chats[i].chat.ToString();
                            break;
                        }
                    }
                }
            }
        }

        private void edtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            string translatedMessage;

            if (lstChatters.SelectedItem != null)
            {
                if (e.KeyChar == '\r')
                {
                    if (this.OnInputSend != null)
                    {
                        for (int i = 0; i < chats.Count; i++)
                        {
                            if (chats[i].fromAgentName == lstChatters.SelectedItem.ToString() && chats[i].client.ToString() == cmbBots.SelectedItem.ToString())
                            {
                                if (chats[i].isGeneralChat)
                                {
                                    string RealChat;
                                    int RealChannel;

                                    if (edtInput.Text.StartsWith("/"))
                                    {
                                        RealChat = edtInput.Text.Split(" ".ToCharArray())[0];
                                        RealChat = RealChat.TrimStart("/".ToCharArray());
                                        if (Int32.TryParse(RealChat.TrimStart("/".ToCharArray()), out RealChannel))
                                        {
                                            RealChat = edtInput.Text.Substring(1 + edtInput.Text.Split(" ".ToCharArray()).Length);
                                        }
                                        else
                                        {
                                            RealChat = edtInput.Text;
                                            RealChannel = 0;
                                        }
                                    }
                                    else
                                    {
                                        RealChat = edtInput.Text;
                                        RealChannel = 0;
                                    }

                                    if (AutoTranslate)
                                    {
                                        translatedMessage = Utilities.TranslateText(RealChat, ToLanguage, FromLanguage);
                                        translatedMessage = "(" + translatedString.ToUpperInvariant() + ") " + translatedMessage;
                                        chats[i].client.Self.Chat(RealChat, RealChannel, ChatType.Normal);
                                        chats[i].client.Self.Chat(translatedMessage, RealChannel, ChatType.Normal);
                                    }
                                    else
                                        chats[i].client.Self.Chat(RealChat, RealChannel, ChatType.Normal);

                                    break;
                                }
                                else
                                {
                                    if (chats[i].isGroupChat)
                                    {
                                        if (chats[i].client.Self.GroupChatSessions.ContainsKey(chats[i].fromAgentUUID))
                                        {
                                            chats[i].client.Self.InstantMessageGroup(chats[i].fromAgentUUID, edtInput.Text);
                                            break;
                                        }
                                        else
                                        {
                                            JoinGroupChatEvent = new ManualResetEvent(false);
                                            chats[i].client.Self.GroupChatJoined += OnGroupChatJoin;
                                            chats[i].client.Self.RequestJoinGroupChat(chats[i].fromAgentUUID);
                                            JoinGroupChatEvent.WaitOne(15000, false);
                                            chats[i].client.Self.GroupChatJoined -= OnGroupChatJoin;

                                            if (AutoTranslate)
                                            {
                                                translatedMessage = Utilities.TranslateText(edtInput.Text, ToLanguage, FromLanguage);
                                                translatedMessage = "(" + translatedString.ToUpperInvariant() + ") " + translatedMessage;
                                                chats[i].client.Self.InstantMessageGroup(chats[i].fromAgentUUID, edtInput.Text);
                                                chats[i].client.Self.InstantMessageGroup(chats[i].fromAgentUUID, translatedMessage);
                                            }
                                            else
                                                chats[i].client.Self.InstantMessageGroup(chats[i].fromAgentUUID, edtInput.Text);

                                            break;
                                        }
                                    }
                                    else
                                    {
                                        InstantMessage im = new InstantMessage();

                                        im.Message = edtInput.Text;
                                        im.FromAgentID = chats[i].fromAgentUUID;
                                        im.FromAgentName = chats[i].fromAgentName;
                                        im.Dialog = InstantMessageDialog.MessageFromAgent;

                                        if (AutoTranslate)
                                        {
                                            translatedMessage = Utilities.TranslateText(edtInput.Text, ToLanguage, FromLanguage);
                                            translatedMessage = "(" + translatedString.ToUpperInvariant() + ") " + translatedMessage;
                                            chats[i].client.Self.InstantMessage(chats[i].fromAgentUUID, edtInput.Text);
                                            chats[i].client.Self.InstantMessage(chats[i].fromAgentUUID, translatedMessage);
                                        }
                                        else
                                            chats[i].client.Self.InstantMessage(chats[i].fromAgentUUID, edtInput.Text);

                                        this.OnInputSend(chats[i].client, im, null, DateTime.Now);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    this.edtInput.Text = string.Empty;
                }
            }
        }

        // I do not see whay this should do nothing here.
        void OnGroupChatJoin(object sender, GroupChatJoinedEventArgs e)
        {
            JoinGroupChatEvent.Set();
            return;
        }

        private void cmbBots_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbBots.SelectedItem != null)
            {
                lstChatters.Items.Clear();

                for (int i = 0; i < chats.Count; i++)
                {
                    if (chats[i].client.ToString() == cmbBots.SelectedItem.ToString())
                    {
                        lstChatters.Items.Add(chats[i].fromAgentName);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private bool isGroup(InstantMessage message, SecondLifeBot imClient)
        {
            GetGroupNameEvent = new ManualResetEvent(false);

            isGroupIM = false;
            groupName = "";

            imClient.key2Name(message.IMSessionID, out groupName, out isGroupIM);

            if (isGroupIM)
                return true;
            else
                return false;
        }

        private void lstChatters_DrawItem(object sender, DrawItemEventArgs e)
        {
            string colorName;
            Rectangle rect = e.Bounds;
            string myObject = lstChatters.Items[e.Index].ToString();
            if (e.State == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.White, rect);
            }
            else
            {
                e.Graphics.FillRectangle(Brushes.Black, rect);
            }

            if (myObject == bot.Localization.clResourceManager.getText("frmChat.Chat"))
            {
                colorName = "Red";
            }
            else
            {
                if (e.State == DrawItemState.Selected)
                {
                    colorName = "Black";
                }
                else
                {
                    colorName = "White";
                }
            }

            SolidBrush myBrush = new SolidBrush(Color.FromName(colorName));

            e.Graphics.DrawString(myObject, e.Font, myBrush, rect.X + 1, rect.Y + 2);

            myBrush.Dispose();
        }

        private void frmChat_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmChat_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void LogChat(string BotName, string AvatarName, string msg, bool isGroup)
        {
            StreamWriter chatFile;
            string filename;

            msg = "[" + DateTime.Now.Year.ToString("0000") + "/" + DateTime.Now.Month.ToString("00") + "/" + DateTime.Now.Day.ToString("00") + "]" + msg;

            if (!Directory.Exists("./logs"))
                Directory.CreateDirectory("./logs");
            if (!Directory.Exists("./logs/" + BotName))
                Directory.CreateDirectory("./logs/" + BotName);
            if (isGroup)
            if (!Directory.Exists("./logs/" + BotName + "/groups"))
                Directory.CreateDirectory("./logs/" + BotName + "/groups");

            if (isGroup)
                filename = "./logs/" + BotName + "/groups/" + AvatarName + ".txt";
            else
                filename = "./logs/" + BotName + "/" + AvatarName + ".txt";

            if (File.Exists(filename))
                chatFile = File.AppendText(filename);
            else
                chatFile = File.CreateText(filename);

            chatFile.WriteLine(msg);
            chatFile.Close();
        }

        private void cmbFromLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbFromLanguage.SelectedItem != null)
            {
                _langCodes.LangCodes.TryGetValue(cmbFromLanguage.SelectedItem.ToString(), out FromLanguage);
                translatedString = Utilities.TranslateText("translated", "en", FromLanguage);
            }
        }

        private void cmbToLanguage_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbToLanguage.SelectedItem != null)
            {
                _langCodes.LangCodes.TryGetValue(cmbToLanguage.SelectedItem.ToString(), out ToLanguage);
            }
        }

        private void chkAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoTranslate.Checked)
            {
                cmbFromLanguage.Enabled = true;
                cmbToLanguage.Enabled = true;
                AutoTranslate = true;
            }
            else
            {
                cmbFromLanguage.Enabled = false;
                cmbToLanguage.Enabled = false;
                AutoTranslate = false;
            }
        }
    }
}