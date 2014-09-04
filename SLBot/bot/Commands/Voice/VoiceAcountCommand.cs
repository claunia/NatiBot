/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : VoiceAccountCommand.cs
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
Copyright (C) 2007-2010 openmetaverse.org
****************************************************************************/
namespace bot.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using OpenMetaverse;
    using OpenMetaverse.Packets;

    public class VoiceAccountCommand : Command
    {
        private AutoResetEvent ProvisionEvent = new AutoResetEvent(false);
        private string VoiceAccount = null;
        private string VoicePassword = null;

        public VoiceAccountCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "voiceaccount";
            base.Description = bot.Localization.clResourceManager.getText("Commands.VoiceAccount.Description");
        }

        private bool registered = false;

        private bool IsVoiceManagerRunning()
        {
            if (null == Client.VoiceManager)
                return false;

            if (!registered)
            {
                Client.VoiceManager.OnProvisionAccount += Voice_OnProvisionAccount;
                registered = true;
            }
            return true;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (!IsVoiceManagerRunning())
                return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceAccount.NotRunning"), Client.Self.Name);

            if (!Client.VoiceManager.RequestProvisionAccount())
            {
                return bot.Localization.clResourceManager.getText("Commands.VoiceAccount.GridFail");
            }
            ProvisionEvent.WaitOne(30 * 1000, false);

            if (String.IsNullOrEmpty(VoiceAccount) && String.IsNullOrEmpty(VoicePassword))
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceAccount.LookupFail"), Client.Self.Name);
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceAccount.VoiceInfo"),
                Client.Self.Name, VoiceAccount, VoicePassword);
        }

        void Voice_OnProvisionAccount(string username, string password)
        {
            VoiceAccount = username;
            VoicePassword = password;

            ProvisionEvent.Set();
        }
    }
}
