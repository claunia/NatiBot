/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FindTextureCommand.cs
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
    using OpenMetaverse;
    using System;

    public class FindTextureCommand : Command
    {
        public FindTextureCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "findtexture";
            base.Description = bot.Localization.clResourceManager.getText("Commands.FindTexture.Description") + " " + bot.Localization.clResourceManager.getText("Commands.FindTexture.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            int faceIndex;
            UUID textureID;

            if (args.Length != 2)
                return bot.Localization.clResourceManager.getText("Commands.FindTexture.Usage");

            if (Int32.TryParse(args[0], out faceIndex) &&
                UUID.TryParse(args[1], out textureID))
            {
                Client.Network.CurrentSim.ObjectsPrimitives.ForEach(
                    delegate(Primitive prim)
                    {
                        if (prim.Textures != null && prim.Textures.FaceTextures[faceIndex] != null)
                        {
                            if (prim.Textures.FaceTextures[faceIndex].TextureID == textureID)
                            {
                                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.FindTexture.Info"),
                                    prim.ID.ToString(), prim.LocalID, faceIndex, textureID.ToString());
                            }
                        }
                    }
                );

                return bot.Localization.clResourceManager.getText("Commands.FindTexture.Done");
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.FindTexture.Usage");
            }
        }
    }
}

