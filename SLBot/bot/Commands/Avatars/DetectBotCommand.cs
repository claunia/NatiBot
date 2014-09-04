/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DetectBotCommand.cs
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
namespace bot.Commands
{
    using bot;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DetectBotCommand : Command
    {
        private Dictionary<UUID, bool> m_AgentList = new Dictionary<UUID, bool>();

        public DetectBotCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "detectbots";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DetectBot.Description");
            SecondLifeBot.Avatars.ViewerEffect += new EventHandler<ViewerEffectEventArgs>(Avatars_ViewerEffect);
            SecondLifeBot.Avatars.ViewerEffectLookAt += new EventHandler<ViewerEffectLookAtEventArgs>(Avatars_ViewerEffectLookAt);
            SecondLifeBot.Avatars.ViewerEffectPointAt += new EventHandler<ViewerEffectPointAtEventArgs>(Avatars_ViewerEffectPointAt);
        }

        private void Avatars_ViewerEffectPointAt(object sender, ViewerEffectPointAtEventArgs e)
        {
            lock (m_AgentList)
            {
                if (m_AgentList.ContainsKey(e.SourceID))
                    m_AgentList[e.SourceID] = true;
                else
                    m_AgentList.Add(e.SourceID, true);
            }
        }

        private void Avatars_ViewerEffectLookAt(object sender, ViewerEffectLookAtEventArgs e)
        {
            lock (m_AgentList)
            {
                if (m_AgentList.ContainsKey(e.SourceID))
                    m_AgentList[e.SourceID] = true;
                else
                    m_AgentList.Add(e.SourceID, true);
            }
        }

        private void Avatars_ViewerEffect(object sender, ViewerEffectEventArgs e)
        {
            lock (m_AgentList)
            {
                if (m_AgentList.ContainsKey(e.SourceID))
                    m_AgentList[e.SourceID] = true;
                else
                    m_AgentList.Add(e.SourceID, true);
            }
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder result = new StringBuilder();

            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Client.Network.Simulators[i].ObjectsAvatars.ForEach(
                        delegate(Avatar av)
                        {
                            lock (m_AgentList)
                            {
                                if (!m_AgentList.ContainsKey(av.ID))
                                {
                                    result.AppendLine();
                                    result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.DetectBot.Bot"),
                                        av.Name, av.GroupName, av.Position, av.ID, av.LocalID);
                                }
                            }
                        }
                    );
                }
            }

            return result.ToString();
        }
    }
}

