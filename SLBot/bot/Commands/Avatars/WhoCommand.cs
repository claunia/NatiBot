/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : WhoCommand.cs
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
    using System;
    using System.Text;
    using System.Collections.Generic;

    public class WhoCommand : Command
    {
        public WhoCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "who";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Who.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder result = new StringBuilder();
            Dictionary<UUID, string> ClientNames = ClientTags.ToDictionary();

            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Client.Network.Simulators[i].ObjectsAvatars.ForEach(
                        delegate(Avatar av)
                        {
                            Vector3 RealPosition;

                            if (av.ParentID != 0)
                            {
                                Primitive SitPrim;

                                SitPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                                    delegate(Primitive prim)
                                    {
                                        return prim.LocalID == av.ParentID;
                                    }
                                );

                                if (SitPrim == null)
                                    RealPosition = new Vector3(0, 0, 0);
                                else
                                    RealPosition = SitPrim.Position;
                            }
                            else
                            {
                                RealPosition = av.Position;
                            }

                            string ViewerName;

                            if (av.Textures == null)
                                ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unknown");
                            else
                            {
                                if (av.Textures.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint] != null)
                                {
                                    if (!ClientNames.TryGetValue(av.Textures.FaceTextures[(int)AvatarTextureIndex.HeadBodypaint].TextureID, out ViewerName))
                                        ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unidentified");
                                }
                                else
                                    ViewerName = bot.Localization.clResourceManager.getText("Viewer.Unknown");
                            }

                            result.AppendLine();
                            result.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Who.Info"),
                                av.Name, ViewerName, av.GroupName, RealPosition, av.ID.ToString());
                        }
                    );
                }
            }

            return result.ToString();
        }
    }
}

