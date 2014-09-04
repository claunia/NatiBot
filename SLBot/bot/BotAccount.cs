/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BotAccount.cs
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
    using System.Windows.Forms;
    using System.Drawing;

    public class BotAccount
    {
        private SecondLifeBot client;
        private bool connecting;
        private bool deleted;
        //private DataGridViewClientRow gridViewRow;
        private ListViewItem listItem;
        private bot.LoginDetails loginDetails;

        public delegate void OnDialogScriptReceivedCallback(SecondLifeBot bot,ScriptDialogEventArgs args);

        public event OnDialogScriptReceivedCallback OnDialogScriptReceived;

        public delegate void StatusChangeCallback(string newStatus,System.Drawing.Color color,ListViewItem item);

        public event StatusChangeCallback OnStatusChange;

        public delegate void MasterChangeCallback(string newMaster,ListViewItem item);

        public event MasterChangeCallback OnMasterChange;

        public delegate void AccountRemovedCallback(ListViewItem item);

        public event AccountRemovedCallback OnAccountRemoved;

        public delegate void LocationChangeCallback(string newLocation,ListViewItem item);

        public event LocationChangeCallback OnLocationChanged;

        public BotAccount(bot.LoginDetails loginDetails)
        {
            this.loginDetails = loginDetails;
        }

        public void LocationChange(string newLocation)
        {
            if (OnLocationChanged != null)
                OnLocationChanged(newLocation, this.listItem);
        }

        public void Connect()
        {
            if (this.client != null)
            {
                this.Disconnect(true);
            }
            else
            {
                this.connecting = true;
                if (OnStatusChange != null)
                    OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.Connecting"),
                        System.Drawing.Color.Green, this.listItem);
                //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.Connecting");
                this.client = new SecondLifeBot(this);
                this.client.Netcom.ClientLoginStatus += new EventHandler<ClientLoginEventArgs>(this.Netcom_ClientLoginStatus);
                this.client.Netcom.ClientLoggedOut += new EventHandler(this.Netcom_ClientLoggedOut);
                this.client.OnDialogScriptReceived += new SecondLifeBot.OnDialogScriptReceivedCallback(this.client_OnDialogScriptReceived);
            }
        }

        void client_OnDialogScriptReceived(SecondLifeBot bot, ScriptDialogEventArgs args)
        {
            if (OnDialogScriptReceived != null)
                OnDialogScriptReceived(bot, args);
        }

        public bool Delete()
        {
            this.deleted = true;
            if (!this.connecting)
            {
                this.Disconnect(false);
                if (OnAccountRemoved != null)
                    OnAccountRemoved(this.listItem);
                //this.GridViewRow.Delete();
                return true;
            }
            return true;
        }

        public void Disconnect(bool reconnect)
        {
            if (this.client != null)
            {
                this.client.Dispose();
                this.client = null;
                if (reconnect)
                {
                    this.Connect();
                }
            }
        }

        private void Netcom_ClientLoggedOut(object sender, EventArgs e)
        {
            SecondLifeBot client = ((NetCommunication)sender).Client;
            if (!this.deleted)
            {
                bot.Console.WriteLine(client, bot.Localization.clResourceManager.getText("botAccount.Offline"));
            }
            if (client == this.client)
            {
                if (OnStatusChange != null)
                    OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.Offline"),
                        System.Drawing.Color.Red, this.listItem);
                //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.Offline");
            }
        }

        private void Netcom_ClientLoginStatus(object sender, ClientLoginEventArgs e)
        {
            //THREADING
            if (((NetCommunication)sender).Client == this.client)
            {
                switch (e.Status)
                {
                    case LoginStatus.Failed:
                        this.connecting = false;
                        if (OnStatusChange != null)
                            OnStatusChange(e.Message, Color.Red, this.listItem);
                        //this.gridViewRow.Status = e.Message;
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.LoginFailed"), e.Message);
                        return;

                    case LoginStatus.None:
                        return;

                    case LoginStatus.ConnectingToLogin:
                        //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.ConnectingLogin");
                        if (OnStatusChange != null)
                            OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.ConnectingLogin"),
                                Color.White, this.listItem);
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.ConnectingLogin"));
                        return;

                    case LoginStatus.ReadingResponse:
                        //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.ReadingResponse");
                        if (OnStatusChange != null)
                            OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.ReadingResponse"),
                                Color.White, this.listItem);
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.ReadingResponse"));
                        return;

                    case LoginStatus.ConnectingToSim:
                        //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.ConnectingRegion");
                        if (OnStatusChange != null)
                            OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.ConnectingRegion"),
                                Color.White, this.listItem);
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.ConnectingRegion"));
                        return;

                    case LoginStatus.Redirecting:
                        //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.Redirecting");
                        if (OnStatusChange != null)
                            OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.Redirecting"),
                                Color.White, this.listItem);
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.Redirecting"));
                        return;

                    case LoginStatus.Success:
                        this.connecting = false;
                        //this.gridViewRow.Status = bot.Localization.clResourceManager.getText("botAccount.Online");
                        if (OnStatusChange != null)
                            OnStatusChange(bot.Localization.clResourceManager.getText("botAccount.Online"),
                                Color.Green, this.listItem);
                        bot.Console.WriteLine(this.client, bot.Localization.clResourceManager.getText("botAccount.Online"));
                        if (this.deleted)
                        {
                            this.Delete();
                        }
                        return;
                }
            }
        }

        public void SetMaster(string masterName)
        {
            SetMaster(masterName, false);
        }

        public void SetMaster(string masterName, bool save)
        {
            if (save)
                this.LoginDetails.MasterName = masterName;

            if (OnMasterChange != null)
                OnMasterChange(masterName, this.listItem);
            //this.gridViewRow.MasterName = masterName;
        }

        public SecondLifeBot Client
        {
            get
            {
                return this.client;
            }
        }

        public ListViewItem ListItem
        {
            get
            {
                return this.listItem;
            }
            set
            {
                this.listItem = value;
            }
        }

        public bot.LoginDetails LoginDetails
        {
            get
            {
                return this.loginDetails;
            }
        }
    }
}

