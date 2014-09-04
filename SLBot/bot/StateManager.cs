/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : StateManager.cs
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
    using bot.NetCom;
    using OpenMetaverse;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Timers;

    public class StateManager
    {
        private Timer agentUpdateTicker;
        private bool alwaysrun;
        private bool away;
        private UUID awayAnimationID = new UUID("fd037134-85d4-f241-72c6-4f42164fedee");
        private UUID beamID;
        private bool busy;
        private UUID busyAnimationID = new UUID("efcf670c-2d18-8128-973a-034ebc806b67");
        private SecondLifeBot client;
        private bool flying;
        private float followDistance = 3f;
        private bool following;
        private string followName = string.Empty;
        private NetCommunication netcom;
        private UUID pointID;
        private bool pointing;
        private bool sitting;
        private string statusMessage;
        private bool typing;
        private UUID typingAnimationID = new UUID("c541c47f-e0c0-058b-ad1a-d6ae3a4584d9");

        public event StatusMessageChangedCallback OnStatusMessageChanged;

        public StateManager(SecondLifeBot client)
        {
            this.client = client;
            this.netcom = this.client.Netcom;
            this.statusMessage = "Offline";
            this.AddNetcomEvents();
            this.AddClientEvents();
            this.InitializeAgentUpdateTimer();
        }

        private void AddClientEvents()
        {
            this.client.Objects.AvatarUpdate += new EventHandler<AvatarUpdateEventArgs>(Objects_AvatarUpdate);
        }

        void Objects_AvatarUpdate(object sender, AvatarUpdateEventArgs e)
        {
            if (e.Avatar != null && this.following)
            {
                Avatar avatar;
                this.client.Network.CurrentSim.ObjectsAvatars.TryGetValue(e.Avatar.LocalID, out avatar);
                if ((avatar != null) && (avatar.Name == this.followName))
                {
                    Vector3 position;
                    if (client.Self.SittingOn == 0)
                    {
                        position = avatar.Position;
                    }
                    else
                    {
                        Primitive primitive;
                        this.client.Network.CurrentSim.ObjectsPrimitives.TryGetValue(client.Self.SittingOn, out primitive);
                        if (primitive == null)
                        {
                            position = this.client.Self.SimPosition;
                        }
                        else
                        {
                            position = primitive.Position + avatar.Position;
                        }
                    }
                    if (Vector3.Distance(position, this.client.Self.SimPosition) > this.followDistance)
                    {
                        int num = (int)(e.Simulator.Handle >> 0x20);
                        int num2 = (int)(e.Simulator.Handle & 0xffffffffL);
                        ulong globalX = (ulong)(position.X + num);
                        ulong globalY = (ulong)(position.Y + num2);
                        this.client.Self.AutoPilotCancel();
                        this.client.Self.AutoPilot(globalX, globalY, position.Z);
                    }
                }
            }
        }

        private void AddNetcomEvents()
        {
            this.netcom.ClientLoginStatus += new EventHandler<ClientLoginEventArgs>(this.netcom_ClientLoginStatus);
            this.netcom.ClientLoggedOut += new EventHandler(this.netcom_ClientLoggedOut);
        }

        private void agentUpdateTicker_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.UpdateStatus();
        }

        public void Follow(string name)
        {
            this.followName = name;
            this.following = !string.IsNullOrEmpty(this.followName);
        }

        private void InitializeAgentUpdateTimer()
        {
            this.agentUpdateTicker = new Timer(500.0);
            this.agentUpdateTicker.Elapsed += new ElapsedEventHandler(this.agentUpdateTicker_Elapsed);
        }

        private void netcom_ClientLoggedOut(object sender, EventArgs e)
        {
            this.agentUpdateTicker.Enabled = false;
            this.typing = this.away = this.busy = false;
        }

        private void netcom_ClientLoginStatus(object sender, ClientLoginEventArgs e)
        {
            if (e.Status == LoginStatus.Success)
            {
                this.agentUpdateTicker.Enabled = true;
            }
        }

        public void SetAlwaysRun(bool alwaysrun)
        {
            this.client.Self.Movement.AlwaysRun = alwaysrun;
            this.alwaysrun = alwaysrun;
        }

        public void SetAway(bool away)
        {
            Dictionary<UUID, bool> animations = new Dictionary<UUID, bool>();
            animations.Add(this.awayAnimationID, away);
            this.client.Self.Animate(animations, false);
            this.away = away;
        }

        public void SetBusy(bool busy)
        {
            Dictionary<UUID, bool> animations = new Dictionary<UUID, bool>();
            animations.Add(this.busyAnimationID, busy);
            this.client.Self.Animate(animations, false);
            this.busy = busy;
        }

        public void SetFlying(bool flying)
        {
            this.flying = flying;
        }

        public void SetPointing(bool pointing, UUID target)
        {
            this.pointing = pointing;
            if (pointing)
            {
                this.pointID = UUID.Random();
                this.beamID = UUID.Random();
                this.client.Self.PointAtEffect(this.client.Self.AgentID, target, Vector3d.Zero, PointAtType.Select, this.pointID);
                this.client.Self.BeamEffect(this.client.Self.AgentID, target, Vector3d.Zero, new Color4(0xff, 0xff, 0xff, 0), 60f, this.beamID);
            }
            else if ((!this.pointID.Equals(0)) && (!this.beamID.Equals(0)))
            {
                this.client.Self.PointAtEffect(UUID.Zero, UUID.Zero, Vector3d.Zero, PointAtType.Select, this.pointID);
                this.client.Self.BeamEffect(UUID.Zero, UUID.Zero, Vector3d.Zero, new Color4(0xff, 0xff, 0xff, 0), 0f, this.beamID);
                this.pointID = UUID.Zero;
                this.beamID = UUID.Zero;
            }
        }

        public void SetSitting(bool sitting, UUID target)
        {
            this.sitting = sitting;
            if (sitting)
            {
                this.client.Self.RequestSit(target, Vector3.Zero);
                this.client.Self.Sit();
            }
            else
            {
                this.client.Self.Stand();
            }
        }

        public void SetTyping(bool typing)
        {
            Dictionary<UUID, bool> animations = new Dictionary<UUID, bool>();
            animations.Add(this.typingAnimationID, typing);
            this.client.Self.Animate(animations, false);
            if (typing)
            {
                this.client.Self.Chat(string.Empty, 0, ChatType.StartTyping);
            }
            else
            {
                this.client.Self.Chat(string.Empty, 0, ChatType.StopTyping);
            }
            this.typing = typing;
        }

        private void UpdateStatus()
        {
            // DISLIKE HARDCODED VALUES -.-'' by Claunia
            //this.client.Self.Movement.Camera.Far = 128f;
            this.client.Self.Movement.Fly = this.flying;
            this.client.Self.Movement.Away = this.away;
        }

        public UUID AwayAnimationID
        {
            get
            {
                return this.awayAnimationID;
            }
            set
            {
                this.awayAnimationID = value;
            }
        }

        public UUID BusyAnimationID
        {
            get
            {
                return this.busyAnimationID;
            }
            set
            {
                this.busyAnimationID = value;
            }
        }

        public float FollowDistance
        {
            get
            {
                return this.followDistance;
            }
            set
            {
                this.followDistance = value;
            }
        }

        public string FollowName
        {
            get
            {
                return this.followName;
            }
            set
            {
                this.followName = value;
            }
        }

        public bool IsAway
        {
            get
            {
                return this.away;
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.busy;
            }
        }

        public bool IsFlying
        {
            get
            {
                return this.flying;
            }
        }

        public bool IsFollowing
        {
            get
            {
                return this.following;
            }
        }

        public bool IsPointing
        {
            get
            {
                return this.pointing;
            }
        }

        public bool IsSitting
        {
            get
            {
                return this.sitting;
            }
        }

        public bool IsTyping
        {
            get
            {
                return this.typing;
            }
        }

        public string StatusMessage
        {
            get
            {
                return this.statusMessage;
            }
            set
            {
                if (this.OnStatusMessageChanged != null)
                {
                    this.OnStatusMessageChanged();
                }
                this.statusMessage = value;
            }
        }

        public UUID TypingAnimationID
        {
            get
            {
                return this.typingAnimationID;
            }
            set
            {
                this.typingAnimationID = value;
            }
        }

        public delegate void StatusMessageChangedCallback();
    }
}

