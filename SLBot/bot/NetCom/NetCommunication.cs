/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : NetCommunication.cs
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
    using bot;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class NetCommunication
    {
        private const string BetaGridLogin = "https://login.aditi.lindenlab.com/cgi-bin/login.cgi";
        private SecondLifeBot client;
        private bool loggedIn;
        private bool loggingIn;
        private bot.LoginDetails loginDetails;
        private const string MainGridLogin = "https://login.agni.lindenlab.com/cgi-bin/login.cgi";
        private ISynchronizeInvoke netcomSync;
        private bool teleporting;

        public event EventHandler<AlertMessageEventArgs> AlertMessageReceived;

        public event EventHandler<ChatEventArgs> ChatReceived;

        public event EventHandler<ChatSentEventArgs> ChatSent;

        public event EventHandler<ClientDisconnectEventArgs> ClientDisconnected;

        public event EventHandler ClientLoggedOut;

        public event EventHandler<OverrideEventArgs> ClientLoggingIn;

        public event EventHandler<OverrideEventArgs> ClientLoggingOut;

        public event EventHandler<ClientLoginEventArgs> ClientLoginStatus;

        public event EventHandler<InstantMessageEventArgs> InstantMessageReceived;

        public event EventHandler<InstantMessageSentEventArgs> InstantMessageSent;

        public event EventHandler<MoneyBalanceEventArgs> MoneyBalanceUpdated;

        public event EventHandler<TeleportingEventArgs> Teleporting;

        public event EventHandler<TeleportStatusEventArgs> TeleportStatusChanged;

        public NetCommunication(SecondLifeBot client)
        {
            this.client = client;
            this.loginDetails = client.LoginDetails;
            this.AddClientEvents();
            this.AddPacketCallbacks();
        }

        private void AddClientEvents()
        {
            //this.client.Self.OnChat += new AgentManager.ChatCallback(this.Self_OnChat);
            //this.client.Self.OnInstantMessage += new AgentManager.InstantMessageCallback(this.Self_OnInstantMessage);
            //this.client.Self.OnBalanceUpdated += new AgentManager.BalanceCallback(this.Avatar_OnBalanceUpdated);
            //this.client.Self.OnTeleport += new AgentManager.TeleportCallback(this.Self_OnTeleport);
            this.client.Self.TeleportProgress += new EventHandler<TeleportEventArgs>(this.Self_TeleportProgress);
            this.client.Network.SimConnected += new EventHandler<SimConnectedEventArgs>(this.Network_OnConnected);
            //this.client.Network.OnConnected += new NetworkManager.ConnectedCallback(this.Network_OnConnected);
            //this.client.Network.OnDisconnected += new NetworkManager.DisconnectedCallback(this.Network_OnDisconnected);
            this.client.Network.Disconnected += new EventHandler<DisconnectedEventArgs>(this.Network_OnDisconnected);
            this.client.Network.LoginProgress += new EventHandler<LoginProgressEventArgs>(this.Network_OnLogin);
            //this.client.Network.OnLogin += new NetworkManager.LoginCallback(this.Network_OnLogin);
            this.client.Network.LoggedOut += new EventHandler<LoggedOutEventArgs>(this.Network_OnLogoutReply);
            //this.client.Network.OnLogoutReply += new NetworkManager.LogoutCallback(this.Network_OnLogoutReply);
        }

        private void AddPacketCallbacks()
        {
            this.client.Network.RegisterCallback(PacketType.AlertMessage, this.AlertMessageHandler);
        }

        private void AlertMessageHandler(object sender, PacketReceivedEventArgs e)
        {
            if (e.Packet.Type == PacketType.AlertMessage)
            {
                AlertMessagePacket packet2 = (AlertMessagePacket)e.Packet;
                AlertMessageEventArgs f = new AlertMessageEventArgs(Utils.BytesToString(packet2.AlertData.Message));
                if (this.netcomSync != null)
                {
                    this.netcomSync.BeginInvoke(new OnAlertMessageRaise(this.OnAlertMessageReceived), new object[] { f });
                }
                else
                {
                    this.OnAlertMessageReceived(f);
                }
            }
        }

        private void Avatar_OnBalanceUpdated(int balance)
        {
            MoneyBalanceEventArgs e = new MoneyBalanceEventArgs(balance);
            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnMoneyBalanceRaise(this.OnMoneyBalanceUpdated), new object[] { e });
            }
            else
            {
                this.OnMoneyBalanceUpdated(e);
            }
        }

        public void ChatOut(string chat, ChatType type, int channel)
        {
            if (this.loggedIn)
            {
                this.client.Self.Chat(chat, channel, type);
                this.OnChatSent(new ChatSentEventArgs(chat, type, channel));
            }
        }

        public string generateMAC()
        {
            Random random = new Random(15);
            string str = "";
            for (int i = 0; i < 0x20; i++)
            {
                str = str + this.sm_int_to_hex(random.Next(15));
            }
            return str;
        }

        public void Login()
        {
            this.loggingIn = true;
            OverrideEventArgs e = new OverrideEventArgs();
            this.OnClientLoggingIn(e);
            if (e.Cancel)
            {
                this.loggingIn = false;
            }
            else
            {
                string password;
                if ((string.IsNullOrEmpty(this.loginDetails.FirstName) || string.IsNullOrEmpty(this.loginDetails.LastName)) || string.IsNullOrEmpty(this.loginDetails.Password))
                {
                    //CLAUNIA: TRANSLATE ME
                    this.OnClientLoginStatus(new ClientLoginEventArgs(LoginStatus.Failed, "One or more fields are blank."));
                }
                string startLocation = string.Empty;
                if ((this.LoginDetails.StartLocation != "home") && (this.LoginDetails.StartLocation != "last"))
                {
                    StartLocationParser parser = new StartLocationParser(this.loginDetails.StartLocation.Trim());
                    startLocation = NetworkManager.StartLocation(parser.Sim, parser.X, parser.Y, parser.Z);
                }
                else
                {
                    startLocation = this.LoginDetails.StartLocation;
                }
                if (this.loginDetails.IsPasswordMD5)
                {
                    password = this.loginDetails.Password;
                }
                else
                {
                    password = Utils.MD5(this.loginDetails.Password);
                }
                LoginParams loginParams = this.client.Network.DefaultLoginParams(this.loginDetails.FirstName, this.loginDetails.LastName, password, this.loginDetails.UserAgent, this.loginDetails.Author);
                loginParams.Start = startLocation;
                if (this.LoginDetails.GridCustomLoginUri == "")
                {
                    System.Windows.Forms.MessageBox.Show(bot.Localization.clResourceManager.getText("NetCom.Updating.Line1") + System.Environment.NewLine +
                    bot.Localization.clResourceManager.getText("NetCom.Updating.Line2") + System.Environment.NewLine +
                    bot.Localization.clResourceManager.getText("NetCom.Updating.Line3"));

                    this.client.Settings.LOGIN_SERVER = "https://login.agni.lindenlab.com/cgi-bin/login.cgi";
                }
                this.client.Settings.LOGIN_SERVER = this.loginDetails.GridCustomLoginUri;
                loginParams.URI = this.client.Settings.LOGIN_SERVER;
                this.client.Network.BeginLogin(loginParams);
            }
        }

        public void Logout()
        {
            if (!this.loggedIn)
            {
                this.OnClientLoggedOut(EventArgs.Empty);
            }
            else
            {
                OverrideEventArgs e = new OverrideEventArgs();
                this.OnClientLoggingOut(e);
                if (!e.Cancel)
                {
                    this.client.Network.Logout();
                }
            }
        }

        private void Network_OnConnected(object sender, SimConnectedEventArgs e)
        {
            this.client.Self.RequestBalance();
            this.client.Appearance.SetPreviousAppearance(false);
        }

        private void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (this.loggedIn)
            {
                this.loggedIn = false;
                ClientDisconnectEventArgs f = new ClientDisconnectEventArgs(e.Reason, e.Message);
                if (this.netcomSync != null)
                {
                    this.netcomSync.BeginInvoke(new OnClientDisconnectRaise(this.OnClientDisconnected), new object[] { f });
                }
                else
                {
                    this.OnClientDisconnected(f);
                }
            }
        }

        private void Network_OnLogin(object sender, LoginProgressEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                this.loggedIn = true;
            }
            ClientLoginEventArgs f = new ClientLoginEventArgs(e.Status, e.Message);
            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnClientLoginRaise(this.OnClientLoginStatus), new object[] { f });
            }
            else
            {
                this.OnClientLoginStatus(f);
            }
        }

        private void Network_OnLogoutReply(object sender, LoggedOutEventArgs e)
        {
            this.loggedIn = false;
            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnClientLogoutRaise(this.OnClientLoggedOut), new object[] { EventArgs.Empty });
            }
            else
            {
                this.OnClientLoggedOut(EventArgs.Empty);
            }
        }

        protected virtual void OnAlertMessageReceived(AlertMessageEventArgs e)
        {
            if (this.AlertMessageReceived != null)
            {
                this.AlertMessageReceived(this, e);
            }
        }

        protected virtual void OnChatReceived(ChatEventArgs e)
        {
            if (this.ChatReceived != null)
            {
                this.ChatReceived(this, e);
            }
        }

        protected virtual void OnChatSent(ChatSentEventArgs e)
        {
            if (this.ChatSent != null)
            {
                this.ChatSent(this, e);
            }
        }

        protected virtual void OnClientDisconnected(ClientDisconnectEventArgs e)
        {
            if (this.ClientDisconnected != null)
            {
                this.ClientDisconnected(this, e);
            }
        }

        protected virtual void OnClientLoggedOut(EventArgs e)
        {
            if (this.ClientLoggedOut != null)
            {
                this.ClientLoggedOut(this, e);
            }
        }

        protected virtual void OnClientLoggingIn(OverrideEventArgs e)
        {
            if (this.ClientLoggingIn != null)
            {
                this.ClientLoggingIn(this, e);
            }
        }

        protected virtual void OnClientLoggingOut(OverrideEventArgs e)
        {
            if (this.ClientLoggingOut != null)
            {
                this.ClientLoggingOut(this, e);
            }
        }

        protected virtual void OnClientLoginStatus(ClientLoginEventArgs e)
        {
            if (this.ClientLoginStatus != null)
            {
                this.ClientLoginStatus(this, e);
            }
        }

        protected virtual void OnInstantMessageReceived(InstantMessageEventArgs e)
        {
            if (this.InstantMessageReceived != null)
            {
                this.InstantMessageReceived(this, e);
            }
        }

        protected virtual void OnInstantMessageSent(InstantMessageSentEventArgs e)
        {
            if (this.InstantMessageSent != null)
            {
                this.InstantMessageSent(this, e);
            }
        }

        protected virtual void OnMoneyBalanceUpdated(MoneyBalanceEventArgs e)
        {
            if (this.MoneyBalanceUpdated != null)
            {
                this.MoneyBalanceUpdated(this, e);
            }
        }

        protected virtual void OnTeleporting(TeleportingEventArgs e)
        {
            if (this.Teleporting != null)
            {
                this.Teleporting(this, e);
            }
        }

        protected virtual void OnTeleportStatusChanged(TeleportEventArgs e)
        {
            if (this.TeleportStatusChanged != null)
            {
                TeleportStatusEventArgs f = new TeleportStatusEventArgs(e.Message, e.Status, e.Flags);
                this.TeleportStatusChanged(this, f);
            }
        }

        private void Self_OnChat(string message, ChatAudibleLevel audible, ChatType type, ChatSourceType sourceType, string fromName, UUID id, UUID ownerid, Vector3 position)
        {
            ChatEventArgs e = new ChatEventArgs(message, audible, type, sourceType, fromName, id, ownerid, position);
            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnChatRaise(this.OnChatReceived), new object[] { e });
            }
            else
            {
                this.OnChatReceived(e);
            }
        }

        private void Self_OnInstantMessage(InstantMessage im, Simulator simulator)
        {
            InstantMessageEventArgs e = new InstantMessageEventArgs(im, simulator);
            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnInstantMessageRaise(this.OnInstantMessageReceived), new object[] { e });
            }
            else
            {
                this.OnInstantMessageReceived(e);
            }
        }

        private void Self_TeleportProgress(object sender, TeleportEventArgs e)
        {
            if ((e.Status == TeleportStatus.Finished) || (e.Status == TeleportStatus.Failed))
            {
                this.teleporting = false;
            }

            if (this.netcomSync != null)
            {
                this.netcomSync.BeginInvoke(new OnTeleportStatusRaise(this.OnTeleportStatusChanged), new object[] { e });
            }
            else
            {
                this.OnTeleportStatusChanged(e);
            }
        }

        public void SendIMStartTyping(UUID target, UUID session)
        {
            if (this.loggedIn)
            {
                this.client.Self.InstantMessage(this.loginDetails.FullName, target, "typing", session, InstantMessageDialog.StartTyping, InstantMessageOnline.Online, this.client.Self.SimPosition, this.client.Network.CurrentSim.ID, null);
            }
        }

        public void SendIMStopTyping(UUID target, UUID session)
        {
            if (this.loggedIn)
            {
                this.client.Self.InstantMessage(this.loginDetails.FullName, target, "typing", session, InstantMessageDialog.StopTyping, InstantMessageOnline.Online, this.client.Self.SimPosition, this.client.Network.CurrentSim.ID, null);
            }
        }

        public void SendInstantMessage(string message, UUID target, UUID session)
        {
            if (this.loggedIn)
            {
                this.client.Self.InstantMessage(this.loginDetails.FullName, target, message, session, InstantMessageDialog.MessageFromAgent, InstantMessageOnline.Online, this.client.Self.SimPosition, this.client.Network.CurrentSim.ID, null);
                this.OnInstantMessageSent(new InstantMessageSentEventArgs(message, target, session, DateTime.Now));
            }
        }

        private string sm_int_to_hex(int inp)
        {
            switch (inp)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return inp.ToString();

                case 10:
                    return "A";

                case 11:
                    return "B";

                case 12:
                    return "C";

                case 13:
                    return "D";

                case 14:
                    return "E";

                case 15:
                    return "F";
            }
            return "0";
        }

        public void Teleport(string sim, Vector3 coordinates)
        {
            if (this.loggedIn && !this.teleporting)
            {
                TeleportingEventArgs e = new TeleportingEventArgs(sim, coordinates);
                this.OnTeleporting(e);
                if (!e.Cancel)
                {
                    this.teleporting = true;
                    this.client.Self.Teleport(sim, coordinates);
                }
            }
        }

        public SecondLifeBot Client
        {
            get
            {
                return this.client;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return this.loggedIn;
            }
        }

        public bool IsLoggingIn
        {
            get
            {
                return this.loggingIn;
            }
        }

        public bool IsTeleporting
        {
            get
            {
                return this.teleporting;
            }
        }

        public bot.LoginDetails LoginDetails
        {
            get
            {
                return this.loginDetails;
            }
            set
            {
                this.loginDetails = value;
            }
        }

        public ISynchronizeInvoke NetcomSync
        {
            get
            {
                return this.netcomSync;
            }
            set
            {
                this.netcomSync = value;
            }
        }

        private delegate void OnAlertMessageRaise(AlertMessageEventArgs e);

        private delegate void OnChatRaise(ChatEventArgs e);

        private delegate void OnClientDisconnectRaise(ClientDisconnectEventArgs e);

        private delegate void OnClientLoginRaise(ClientLoginEventArgs e);

        private delegate void OnClientLogoutRaise(EventArgs e);

        private delegate void OnInstantMessageRaise(InstantMessageEventArgs e);

        private delegate void OnMoneyBalanceRaise(MoneyBalanceEventArgs e);

        private delegate void OnTeleportStatusRaise(TeleportEventArgs e);
    }
}

