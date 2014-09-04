/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : CloneCommand.cs
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
    using System.Threading;

    public class CloneCommand : Command
    {
        uint SerialNum = 2;

        public CloneCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "clone";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Clone.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Clone.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            string targetName = String.Empty;
            List<DirectoryManager.AgentSearchData> matches;

            for (int ct = 0; ct < args.Length; ct++)
                targetName = targetName + args[ct] + " ";
            targetName = targetName.TrimEnd();

            if (targetName.Length == 0)
                return bot.Localization.clResourceManager.getText("Commands.Clone.Usage");

            if (Client.Directory.PeopleSearch(DirectoryManager.DirFindFlags.People, targetName, 0, 1000 * 10,
                    out matches) && matches.Count > 0)
            {
                UUID target = matches[0].AgentID;
                targetName += String.Format(" ({0})", target);

                if (Client.Appearances.ContainsKey(target))
                {
                    #region AvatarAppearance to AgentSetAppearance

                    AvatarAppearancePacket appearance = Client.Appearances[target];

                    AgentSetAppearancePacket set = new AgentSetAppearancePacket();
                    set.AgentData.AgentID = Client.Self.AgentID;
                    set.AgentData.SessionID = Client.Self.SessionID;
                    set.AgentData.SerialNum = SerialNum++;
                    set.AgentData.Size = new Vector3(2f, 2f, 2f); // HACK

                    set.WearableData = new AgentSetAppearancePacket.WearableDataBlock[0];
                    set.VisualParam = new AgentSetAppearancePacket.VisualParamBlock[appearance.VisualParam.Length];

                    for (int i = 0; i < appearance.VisualParam.Length; i++)
                    {
                        set.VisualParam[i] = new AgentSetAppearancePacket.VisualParamBlock();
                        set.VisualParam[i].ParamValue = appearance.VisualParam[i].ParamValue;
                    }

                    set.ObjectData.TextureEntry = appearance.ObjectData.TextureEntry;

                    #endregion AvatarAppearance to AgentSetAppearance

                    // Detach everything we are currently wearing
                    Client.Appearance.AddAttachments(new List<InventoryItem>(), true);

                    // Send the new appearance packet
                    Client.Network.SendPacket(set);

                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Clone.Done"), targetName);
                }
                else
                {
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Clone.Unknown"), targetName);
                }
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Clone.NotFound"), targetName);
            }
        }
    }
}

