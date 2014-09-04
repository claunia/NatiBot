/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ParcelVoiceInfoCommand.cs
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
    using bot;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using OpenMetaverse;
    using OpenMetaverse.Packets;

    public class ParcelVoiceInfoCommand : Command
    {
        private AutoResetEvent ParcelVoiceInfoEvent = new AutoResetEvent(false);
        private string VoiceRegionName = null;
        private int VoiceLocalID = -1;
        private string VoiceChannelURI = null;

        public ParcelVoiceInfoCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "voiceparcel";
            base.Description = bot.Localization.clResourceManager.getText("Commands.VoiceParcel.Description");
        }

        private bool registered = false;

        private bool IsVoiceManagerRunning()
        {
            if (null == Client.VoiceManager)
                return false;

            if (!registered)
            {
                Client.VoiceManager.OnParcelVoiceInfo += Voice_OnParcelVoiceInfo;
                registered = true;
            }
            return true;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (!IsVoiceManagerRunning())
                return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceAccount.NotRunning"), fromAgentID);

            if (!Client.VoiceManager.RequestParcelVoiceInfo())
            {
                return bot.Localization.clResourceManager.getText("Commands.VoiceParcel.GridFail");
            }
            ParcelVoiceInfoEvent.WaitOne(30 * 1000, false);

            if (String.IsNullOrEmpty(VoiceRegionName) && -1 == VoiceLocalID)
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceParcel.Failed"), Client.Self.Name);
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.VoiceParcel.VoiceInfo"),
                Client.Self.Name, VoiceRegionName, VoiceLocalID, VoiceChannelURI);
        }


        void Voice_OnParcelVoiceInfo(string regionName, int localID, string channelURI)
        {
            VoiceRegionName = regionName;
            VoiceLocalID = localID;
            VoiceChannelURI = channelURI;

            ParcelVoiceInfoEvent.Set();
        }
    }
}
