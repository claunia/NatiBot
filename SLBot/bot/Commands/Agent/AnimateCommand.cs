/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AnimateCommand.cs
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
using OpenMetaverse;
using System.Collections.Generic;
using System.Text;

namespace bot.Core.Commands
{
    public class AnimateCommand : bot.Commands.Command
    {
        private Dictionary<UUID, string> m_BuiltInAnimations = new Dictionary<UUID, string>(Animations.ToDictionary());

        public AnimateCommand(SecondLifeBot SecondLifeBot)
        {
            Client = SecondLifeBot;
            Name = "animate";
            Description = bot.Localization.clResourceManager.getText("Commands.Animate.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Animate.Usage");
        }

        private UUID animID;

        public override string Execute(string[] args, UUID fromAgentID, bool from_SL)
        {
            string arg;
            StringBuilder result = new StringBuilder();

            if (args.Length == 1)
            {
                arg = args[0].Trim();

                if (UUID.TryParse(args[0], out animID))
                {
                    Client.Self.AnimationStart(animID, true);
                    result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.Starting"), animID);
                }
                else if (arg.ToLower().Equals("list"))
                {
                    foreach (string key in m_BuiltInAnimations.Values)
                    {
                        result.AppendLine(key);
                    }
                }
                else if (arg.ToLower().Equals("show"))
                {
                    Client.Self.SignaledAnimations.ForEach(delegate(KeyValuePair<UUID, int> kvp)
                    {
                        if (m_BuiltInAnimations.ContainsKey(kvp.Key))
                        {
                            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.SystemAnimation"), m_BuiltInAnimations[kvp.Key], kvp.Value);
                        }
                        else
                        {
                            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.AssetAnimation"), kvp.Key, kvp.Value);
                        }
                    });
                }
                else if (args[0].ToLower() == "stop")
                {
                    Client.Self.SignaledAnimations.ForEach(delegate(KeyValuePair<UUID, int> kvp)
                    {
                        Client.Self.AnimationStop(kvp.Key, true);
                        result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.Stopping"), kvp.Key);
                    });
                }
                else if (m_BuiltInAnimations.ContainsValue(args[0].Trim().ToUpper()))
                {
                    foreach (var kvp in m_BuiltInAnimations)
                    {
                        if (kvp.Value.Equals(arg.ToUpper()))
                        {
                            Client.Self.AnimationStart(kvp.Key, true);
                            break;
                        }
                    }
                }
            }
            else if (args.Length == 2)
            {
                if (args[0].ToLower() == "stop" && UUID.TryParse(args[1], out animID))
                {
                    Client.Self.AnimationStop(animID, true);
                    result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.Stopping"), animID);
                }
                else if (m_BuiltInAnimations.ContainsValue(args[0].Trim().ToUpper()))
                {
                    foreach (var kvp in m_BuiltInAnimations)
                    {
                        if (kvp.Value.Equals(args[1].ToUpper()))
                        {
                            Client.Self.AnimationStop(kvp.Key, true);
                            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Animate.Stopping"), kvp.Key);
                            break;
                        }
                    }
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.Animate.Usage");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Animate.Usage");
            }

            return result.ToString();
        }
    }
}
