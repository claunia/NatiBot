/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : SecondLifeBot.cs
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
Portions copyright (c) 2006-2010, openmetaverse.org
****************************************************************************/
namespace bot
{
    using bot.Commands;
    using bot.NetCom;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Timers;
    using Meebey.SmartIrc4net;
    using OpenMetaverse.Assets;
    using OpenMetaverse.Utilities;

    public class SecondLifeBot : GridClient, IDisposable
    {
        public BotAccount Account;
        public Dictionary<UUID, AvatarAppearancePacket> Appearances = new Dictionary<UUID, AvatarAppearancePacket>();
        public Dictionary<string, Command> Commands = new Dictionary<string, Command>();
        
        private bool disposed;
        public UUID GroupID = UUID.Zero;
        public Dictionary<UUID, GroupMember> GroupMembers;
        private ManualResetEvent keyResolution = new ManualResetEvent(false);
        private bot.LoginDetails loginDetails;
        private UUID masterKey = UUID.Zero;
        private string masterName = string.Empty;
        public NetCommunication Netcom;
        private UUID query = UUID.Zero;
        private UUID resolvedMasterKey = UUID.Zero;
        public StateManager State;
        private System.Timers.Timer updateTimer;
        // Shell-like inventory commands need to be aware of the 'current' inventory folder.
        public InventoryFolder CurrentDirectory = null;
        public VoiceManager VoiceManager;
        //This is for gettint avatar name from avatar uuid.
        private ManualResetEvent WaitforAvatarName = new ManualResetEvent(false);
        private string requestedAvatarName;
        private UUID requestedAvatarID;
        private bool previouslyConnected;

        public IrcClient irc;

        private UUID GroupMembersRequestID;
        public Dictionary<UUID, Group> GroupsCache = null;
        private ManualResetEvent GroupsEvent = new ManualResetEvent(false);

        // Not storable values
        public bool isAway;
        public bool isBusy;
        public bool isNadu;
        // Storable values
        public bool InformFriends;

        public delegate void OnDialogScriptReceivedCallback(SecondLifeBot bot,ScriptDialogEventArgs args);

        public event OnDialogScriptReceivedCallback OnDialogScriptReceived;

        public SecondLifeBot(BotAccount account)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.Account = account;

            this.loginDetails = account.LoginDetails;
            this.Netcom = new NetCommunication(this);
            this.State = new StateManager(this);
            this.updateTimer = new System.Timers.Timer(500.0);
            this.updateTimer.Elapsed += new ElapsedEventHandler(this.updateTimer_Elapsed);
            this.RegisterAllCommands(Assembly.GetExecutingAssembly());
            this.registerEvents();
            base.Settings.LOG_RESENDS = false;
            base.Settings.STORE_LAND_PATCHES = true;
            base.Settings.ALWAYS_DECODE_OBJECTS = true;
            base.Settings.ALWAYS_REQUEST_OBJECTS = true;
            base.Settings.SEND_AGENT_UPDATES = this.Account.LoginDetails.SendAgentUpdatePacket;
            this.Login();
            this.updateTimer.Start();

            this.InformFriends = Account.LoginDetails.BotConfig.InformFriends;

            VoiceManager = new VoiceManager(this);

            if (this.LoginDetails.IRC_Settings.isUsingIRC)
            {
                this.irc = new IrcClient();
                this.irc.AutoRejoin = true;
                this.irc.Connect(Account.LoginDetails.IRC_Settings.ServerHost, Account.LoginDetails.IRC_Settings.ServerPort);

                this.irc.Login(Account.LoginDetails.FirstName.Substring(0, 1) + Account.LoginDetails.LastName, "NatiBot");
                this.irc.RfcJoin(Account.LoginDetails.IRC_Settings.MainChannel);
                this.irc.OnJoin += new JoinEventHandler(IRCOnJoin);
                this.irc.OnChannelMessage += new IrcEventHandler(IRCOnMessage);
            }
        }

        public UUID GroupName2UUID(String groupName)
        {
            UUID tryUUID;
            if (UUID.TryParse(groupName, out tryUUID))
                return tryUUID;
            if (null == GroupsCache)
            {
                ReloadGroupsCache();
                if (null == GroupsCache)
                    return UUID.Zero;
            }
            lock (GroupsCache)
            {
                if (GroupsCache.Count > 0)
                {
                    foreach (Group currentGroup in GroupsCache.Values)
                        if (currentGroup.Name.ToLower() == groupName.ToLower())
                            return currentGroup.ID;
                }
            }
            return UUID.Zero;
        }

        private bool MainisGroupKey;

        public bool key2Name(UUID avatarID, out string avatarName)
        {
            bool isGroupID = false;
            return key2Name(avatarID, out avatarName, out isGroupID);
        }

        private bool RequestedmoreThanOneAvFound;

        public bool FindOneAvatar(string avatarName, out UUID avatarID)
        {
            bool moreThanOneAvatarFound = false;
            return FindOneAvatar(avatarName, out avatarID, out moreThanOneAvatarFound);
        }

        public bool FindOneAvatar(string avatarName, out UUID avatarID, out bool moreThanOneAvatarFound)
        {
            this.Directory.DirPeopleReply += new EventHandler<DirPeopleReplyEventArgs>(Directory_DirPeopleReply);
            this.WaitforAvatarName = new ManualResetEvent(false);

            requestedAvatarID = UUID.Zero;
            RequestedmoreThanOneAvFound = false;

            this.Directory.StartPeopleSearch(avatarName, 0);
            if (WaitforAvatarName.WaitOne(15000, false))
            {
                this.Directory.DirPeopleReply -= Directory_DirPeopleReply;

                if (requestedAvatarID != UUID.Zero)
                {
                    avatarID = requestedAvatarID;
                    moreThanOneAvatarFound = RequestedmoreThanOneAvFound;
                    return true;
                }
                else
                {
                    avatarID = requestedAvatarID;
                    moreThanOneAvatarFound = RequestedmoreThanOneAvFound;
                    return false;
                }
            }
            else
            {
                this.Directory.DirPeopleReply -= Directory_DirPeopleReply;

                avatarID = requestedAvatarID;
                moreThanOneAvatarFound = RequestedmoreThanOneAvFound;
                return false;
            }
        }

        void Directory_DirPeopleReply(object sender, DirPeopleReplyEventArgs e)
        {
            if (e.MatchedPeople.Count > 1)
                RequestedmoreThanOneAvFound = true;
            requestedAvatarID = e.MatchedPeople[0].AgentID;
            WaitforAvatarName.Set();
            return;
        }

        public bool key2Name(UUID avatarID, out string avatarName, out bool isGroupKey)
        {
            this.WaitforAvatarName = new ManualResetEvent(false);
            requestedAvatarName = "";
            requestedAvatarID = avatarID;
            MainisGroupKey = false;
            isGroupKey = false;
            Group groupValue = new Group();

            if (GroupsCache == null)
                this.ReloadGroupsCache();

            if (GroupsCache.TryGetValue(avatarID, out groupValue))
            {
                avatarName = groupValue.Name;
                MainisGroupKey = true;
                isGroupKey = MainisGroupKey;
                return true;
            }
            else
            {
                this.Avatars.UUIDNameReply += this.AvatarNamesReceived;
                this.Groups.GroupProfile += this.Groups_OnGroupProfile;
                this.Groups.RequestGroupProfile(avatarID);
                this.Avatars.RequestAvatarName(avatarID);
                if (WaitforAvatarName.WaitOne(15000, false))
                {
                    this.Avatars.UUIDNameReply -= this.AvatarNamesReceived;
                    this.Groups.GroupProfile -= this.Groups_OnGroupProfile;
                    avatarName = requestedAvatarName;
                    isGroupKey = MainisGroupKey;
                }
                else
                {
                    this.Avatars.UUIDNameReply -= this.AvatarNamesReceived;
                    this.Groups.GroupProfile -= this.Groups_OnGroupProfile;
                    avatarName = "";
                    isGroupKey = false;
                }
                
                if (avatarName == "(???) (???)" || avatarName == "")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void AvatarNamesReceived(object sender, UUIDNameReplyEventArgs e)
        {
            requestedAvatarName = e.Names[requestedAvatarID];
            MainisGroupKey = false;
            this.WaitforAvatarName.Set();
            return;
        }

        private void Groups_OnGroupProfile(object sender, GroupProfileEventArgs e)
        {
            requestedAvatarName = e.Group.Name;
            MainisGroupKey = true;
            this.WaitforAvatarName.Set();
            return;
        }

        private void AgentDataUpdateHandler(object sender, PacketReceivedEventArgs e)
        {
            AgentDataUpdatePacket p = (AgentDataUpdatePacket)e.Packet;
            if (p.AgentData.AgentID == e.Simulator.Client.Self.AgentID)
            {
                this.GroupID = p.AgentData.ActiveGroupID;

                GroupMembersRequestID = e.Simulator.Client.Groups.RequestGroupMembers(GroupID);

                bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.GotGroupID"), e.Simulator.Client.ToString());
            }
        }

        private void AlertMessageHandler(object sender, PacketReceivedEventArgs e)
        {
            Packet packet = e.Packet;

            AlertMessagePacket message = (AlertMessagePacket)packet;

            bot.Console.WriteLine(this, "[AlertMessage] " + Utils.BytesToString(message.AlertData.Message));
        }

        private void AvatarAppearanceHandler(object sender, PacketReceivedEventArgs e)
        {
            Packet packet = e.Packet;

            AvatarAppearancePacket appearance = (AvatarAppearancePacket)packet;

            lock (this.Appearances)
                this.Appearances[appearance.Sender.ID] = appearance;
        }

        public void Log(string shitcakes, Helpers.LogLevel what)
        {
        }

        public virtual void Dispose()
        {
            if (!this.disposed)
            {
                try
                {
                    this.Netcom.Logout();
                }
                finally
                {
                    this.disposed = true;
                    GC.SuppressFinalize(this);
                }
            }
        }

        public void DoCommand(string cmd, UUID fromAgentID, bool fromSL)
        {
            string _nothing = DoCommandReturn(cmd, fromAgentID, fromSL);
        }

        public string DoCommandReturn(string cmd, UUID fromAgentID, bool fromSL)
        {
            string[] strArray;
            try
            {
                strArray = Parsing.ParseArguments(cmd);
            }
            catch (FormatException exception)
            {
                if (fromSL)
                {
                    bot.Console.WriteLine(exception.Message);
                }
                else
                {
                    irc.SendMessage(SendType.Message, Account.LoginDetails.IRC_Settings.MainChannel, exception.Message);
                }
                return exception.Message;
            }
            if (strArray.Length != 0)
            {
                string key = strArray[0].ToLower();
                
                key = key.Replace("!", String.Empty);

                if (this.Commands.ContainsKey(key))
                {
                    string[] destinationArray = new string[strArray.Length - 1];
                    Array.Copy(strArray, 1, destinationArray, 0, destinationArray.Length);
                    string str2 = this.Commands[key].Execute(destinationArray, fromAgentID, fromSL);
                    if (!string.IsNullOrEmpty(str2))
                    {
                        bot.Console.WriteLine("<{0} {1}> {2}", new object[] { this.LoginDetails.FirstName, this.LoginDetails.LastName, str2 });
                        if (base.Network.Connected && (fromAgentID != UUID.Zero))
                        {
                            str2 = str2.Replace("\r", string.Empty);
                            if (fromSL.Equals(true))
                            {
                                this.SendResponseIM(this, fromAgentID, str2);
                            }
                            else
                            {
                                this.irc.SendMessage(SendType.Message, this.LoginDetails.IRC_Settings.MainChannel, str2);
                            }
                        }
                    }
                    return str2;
                }
            }
            return "";
        }

        ~SecondLifeBot()
        {
            this.Dispose();
        }

        private void GotoLastSeat()
        {
            bot.Console.WriteLine(this, "NOT IMPLEMENTED! Going to: {0}", this.LoginDetails.BotConfig.LastSitposition);
        }

        public void ReloadGroupsCache()
        {
            Groups.CurrentGroups += Groups_CurrentGroups;
            Groups.RequestCurrentGroups();
            GroupsEvent.WaitOne(10000, false);
            Groups.CurrentGroups -= Groups_CurrentGroups;
            GroupsEvent.Reset();
        }

        void Groups_CurrentGroups(object sender, CurrentGroupsEventArgs e)
        {
            if (null == GroupsCache)
                GroupsCache = e.Groups;
            else
                lock (GroupsCache)
                {
                    GroupsCache = e.Groups;
                }
            GroupsEvent.Set();
        }

        private void GroupMembersHandler(object sender, GroupMembersReplyEventArgs e)
        {
            if (e.RequestID != GroupMembersRequestID)
                return;

            this.GroupMembers = e.Members;

            bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.GotGroupMembers"), e.Members.Count);
        }

        // TODO Rev 100: Ask user for accepting inventory objects or not
        private void Inventory_OnInventoryObjectReceived(object sender, InventoryObjectOfferedEventArgs e)
        {
            if (this.Account.LoginDetails.BotConfig.AcceptInventoryOffers)
                e.Accept = true;
            else
                e.Accept = false;
            return;
        }

        private void Login()
        {
            if (!base.Network.Connected)
            {
                
                this.Netcom.Login();
                this.MasterName = this.LoginDetails.MasterName;
            }
        }

        private void Network_OnConnected(object sender, SimConnectedEventArgs e)
        {
            if (!previouslyConnected)
            {
                this.MasterName = this.LoginDetails.MasterName;
                if (irc != null)
                    new Thread(new ThreadStart(irc.Listen)).Start();

                if (this.LoginDetails.BotConfig.SaveSitPosition)
                {
                    this.GotoLastSeat();
                }

                this.Self.RetrieveInstantMessages();
                previouslyConnected = true;
            }
        }

        private void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            previouslyConnected = false;
            this.Account.Disconnect(false);
            if (this.irc != null)
                this.irc.Disconnect();
        }

        public void RegisterAllCommands(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                try
                {
                    if (type.IsSubclassOf(typeof(Command)))
                    {
                        Command command = (Command)type.GetConstructor(new Type[] { typeof(SecondLifeBot) }).Invoke(new object[] { this });
                        this.RegisterCommand(command);
                    }
                }
                catch (Exception exception)
                {
                    bot.Console.WriteLine(exception.ToString());
                }
            }
        }

        public void RegisterCommand(Command command)
        {
            command.Client = this;
            if (!this.Commands.ContainsKey(command.Name.ToLower()))
            {
                this.Commands.Add(command.Name.ToLower(), command);
            }
        }

        private void registerEvents()
        {
            base.Self.IM += this.Self_IM;
            base.Self.ChatFromSimulator += this.Self_OnChat;
            base.Groups.GroupMembersReply += this.GroupMembersHandler;
            base.Inventory.InventoryObjectOffered += this.Inventory_OnInventoryObjectReceived;            
            base.Network.RegisterCallback(PacketType.AgentDataUpdate, this.AgentDataUpdateHandler);
            base.Network.RegisterCallback(PacketType.AvatarAppearance, this.AvatarAppearanceHandler);
            base.Network.RegisterCallback(PacketType.AlertMessage, this.AlertMessageHandler);
            base.Network.SimConnected += new EventHandler<SimConnectedEventArgs>(this.Network_OnConnected);
            base.Network.Disconnected += new EventHandler<DisconnectedEventArgs>(this.Network_OnDisconnected);
            base.Friends.FriendOnline += new EventHandler<FriendInfoEventArgs>(Friends_FriendOnline);
            base.Friends.FriendOffline += new EventHandler<FriendInfoEventArgs>(Friends_FriendOffline);
            base.Self.ScriptDialog += new EventHandler<ScriptDialogEventArgs>(Self_ScriptDialog);
        }

        void Self_ScriptDialog(object sender, ScriptDialogEventArgs e)
        {
            if (OnDialogScriptReceived != null)
                OnDialogScriptReceived(this, e);
        }

        void Friends_FriendOffline(object sender, FriendInfoEventArgs e)
        {
            if (InformFriends)
            {
                bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("Bot.Friend.Offline"), e.Friend.Name);
                this.Self.InstantMessage(this.MasterKey, String.Format(bot.Localization.clResourceManager.getText("Bot.Friend.Offline"), e.Friend.Name));
            }
        }

        void Friends_FriendOnline(object sender, FriendInfoEventArgs e)
        {
            if (InformFriends)
            {
                bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("Bot.Friend.Online"), e.Friend.Name);
                this.Self.InstantMessage(this.MasterKey, String.Format(bot.Localization.clResourceManager.getText("Bot.Friend.Online"), e.Friend.Name));
            }
        }

        void Self_OnChat(object sender, OpenMetaverse.ChatEventArgs e)
        {
            bot.Chat.structGeneralChat preBuffer = new Chat.structGeneralChat();

            preBuffer.audible = e.AudibleLevel;
            preBuffer.client = this;
            preBuffer.fromName = e.FromName;
            preBuffer.id = e.SourceID;
            preBuffer.message = e.Message;
            preBuffer.ownerid = e.OwnerID;
            preBuffer.position = e.Position;
            preBuffer.sourceType = e.SourceType;
            preBuffer.type = e.Type;
            preBuffer.timestamp = DateTime.Now;

            bot.Chat.receivedChat(preBuffer);
        }

        private void resolveMasterName(string masterName)
        {
            this.resolvedMasterKey = UUID.Zero;

            EventHandler<DirPeopleReplyEventArgs> peopleDirCallback =
                delegate(object sender, DirPeopleReplyEventArgs e)
                {
                    if (e.QueryID == query)
                    {
                        if (e.MatchedPeople.Count != 1)
                        {
                            bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.MasterNotResolved"), this.masterName);
                        }
                        else
                        {
                            this.resolvedMasterKey = e.MatchedPeople[0].AgentID;
                            bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.MasterResolved"), new object[] { masterName, this.resolvedMasterKey });
                        }

                        this.keyResolution.Set();
                    }
                };

            this.Directory.DirPeopleReply += peopleDirCallback;
            query = this.Directory.StartPeopleSearch(this.MasterName, 0);
            this.keyResolution.WaitOne(5000, false);

            if (base.Network.Connected && this.resolvedMasterKey != UUID.Zero)
            {
                this.MasterKey = this.resolvedMasterKey;
                if (this.Account.LoginDetails.GreetMaster)
                    base.Self.InstantMessage(this.MasterKey, bot.Localization.clResourceManager.getText("bot.Greeting"));
                this.keyResolution.Reset();
                base.Directory.DirPeopleReply -= peopleDirCallback;
            }
            else
            {
                this.keyResolution.Reset();
                base.Directory.DirPeopleReply -= peopleDirCallback;
                this.Account.SetMaster(this.MasterName + " ERROR");
            }
        }

        // Rev 100 THIS IS NOT WORKING PUTTING A CHAT WINDOW IS A PRIORITY
        private void Self_IM(object sender, OpenMetaverse.InstantMessageEventArgs e)
        {
            bot.Chat.structInstantMessage preMessage = new bot.Chat.structInstantMessage();

            preMessage.client = this;
            preMessage.message = e.IM;
            preMessage.simulator = e.Simulator;
            preMessage.timestamp = DateTime.Now;
            preMessage.isReceived = true;

            bot.Chat.receivedIM(preMessage);
            if (this.MasterKey != UUID.Zero)
            {
                if (e.IM.FromAgentID != this.MasterKey)
                {
                    bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.MessageNotMaster"), new object[] { e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message, e.IM.RegionID, e.IM.Position });
                    if (e.IM.FromAgentID != UUID.Zero && e.IM.Dialog != InstantMessageDialog.StartTyping && e.IM.Dialog != InstantMessageDialog.StopTyping)
                    {
                        this.Self.InstantMessage(this.MasterKey, string.Format(bot.Localization.clResourceManager.getText("bot.MessageNotMaster"), new object[] { e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message, e.IM.RegionID, e.IM.Position }));
                    }
                    return;
                }
            }
            else if ((this.GroupMembers != null) && !this.GroupMembers.ContainsKey(e.IM.FromAgentID))
            {
                bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.MessageNotGroup"), new object[] { e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message, e.IM.RegionID, e.IM.Position });
                return;
            }
            bot.Console.WriteLine(this, "<{0} ({1})> {2}: {3} (@{4}:{5})", new object[] { e.IM.GroupIM ? "GroupIM" : "IM", e.IM.Dialog, e.IM.FromAgentName, e.IM.Message, e.IM.RegionID, e.IM.Position });
            if (e.IM.Dialog == InstantMessageDialog.RequestTeleport)
            {
                bot.Console.WriteLine(this, bot.Localization.clResourceManager.getText("bot.AcceptingTeleport"));
                base.Self.TeleportLureRespond(e.IM.FromAgentID, true);
            }
            else if ((e.IM.Dialog == InstantMessageDialog.MessageFromAgent) || (e.IM.Dialog == InstantMessageDialog.MessageFromObject))
            {
                this.DoCommand(e.IM.Message, e.IM.FromAgentID, true);
            }
            if (this.Account.LoginDetails.RelayChatToIRC)
            {
                string msg = string.Format("<{0}{1} ({2})> {3}",
                                 e.IM.GroupIM ? "[GroupIM]" : "[IM]",
                                 e.IM.FromAgentName,
                                 e.IM.FromAgentID.ToString(),
                                 e.IM.Message
                             );

                this.irc.SendMessage(SendType.Message, this.LoginDetails.IRC_Settings.MainChannel, msg);
            }
        }

        private void SendResponseIM(GridClient client, UUID fromAgentID, string data)
        {
            for (int i = 0; i < data.Length; i += 0x400)
            {
                int num2;
                if ((i + 0x3ff) > data.Length)
                {
                    num2 = data.Length - i;
                }
                else
                {
                    num2 = 0x3ff;
                }
                string message = data.Substring(i, num2);
                client.Self.InstantMessage(fromAgentID, message);
            }
        }

        private void updateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (base.Network.CurrentSim != null)
            {
                Vector3 simPosition = base.Self.SimPosition;
                if (this.Netcom.IsLoggedIn)
                {
                    Vector3 vector = base.Self.SimPosition;
                    string str = string.Format("{0} {1}, {2}, {3}", new object[] { base.Network.CurrentSim.Name, (int)vector.X, (int)vector.Y, (int)vector.Z });
                    this.Account.LocationChange(str);
                }
            }
            foreach (Command command in this.Commands.Values)
            {
                if (command.Active)
                {
                    command.Think();
                }
            }
        }

        public bot.LoginDetails LoginDetails
        {
            get
            {
                return this.loginDetails;
            }
        }

        public UUID MasterKey
        {
            get
            {
                return this.masterKey;
            }
            set
            {
                this.masterKey = value;
            }
        }

        public string MasterName
        {
            get
            {
                return this.masterName;
            }
            set
            {
                this.masterName = value;
                //THREADING
                this.Account.SetMaster(value);
                this.LoginDetails.MasterName = value;
                if (base.Network.Connected)
                {
                    this.resolveMasterName(value);
                }
            }
        }

        public void IRCOnJoin(Object sender, IrcEventArgs e)
        {
            if (this.irc.IsMe(e.Data.Nick))
            {
                irc.SendMessage(SendType.Message, e.Data.Channel, String.Format(bot.Localization.clResourceManager.getText("bot.IRCGreet"), this.Account.LoginDetails.FullName));
            }
        }

        public void IRCOnMessage(Object Sender, IrcEventArgs e)
        {
            String master = this.Account.LoginDetails.IRC_Settings.Master;
            Version v = new Version();

            if (!e.Data.Nick.Equals(master))
                return;
            this.DoCommand(e.Data.Message, UUID.Zero, false);
        }

        public void sendIRCMessage(string message)
        {
            this.irc.SendMessage(SendType.Message, this.loginDetails.IRC_Settings.MainChannel, message);
        }
    }
}

