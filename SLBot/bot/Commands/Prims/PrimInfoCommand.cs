/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : PrimInfoCommand.cs
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

    public class PrimInfoCommand : Command
    {
        StringBuilder sbResult = new StringBuilder();

        public PrimInfoCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "priminfo";
            base.Description = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Description") + " " + bot.Localization.clResourceManager.getText("Commands.PrimInfo.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID primID;
            sbResult = new StringBuilder();

            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.PrimInfo.Usage");

            if (UUID.TryParse(args[0], out primID))
            {
                Primitive target = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                                       delegate(Primitive prim)
                    {
                        return prim.ID == primID;
                    }
                                   );

                if (target != null)
                {
                    string targetName, targetDescription, targetCreator, targetOwner, targerLastOwner, targetGroup, targetSitName, targetTouchName;

                    if (target.Properties.Name == "")
                        targetName = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else
                        targetName = target.Properties.Name;
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Name"), targetName);
                    sbResult.AppendLine();

                    if (target.Properties.Description == "")
                        targetDescription = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else
                        targetDescription = target.Properties.Description;
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.PrimDescription"), targetDescription);
                    sbResult.AppendLine();

                    if (target.Properties.CreatorID == UUID.Zero)
                        targetCreator = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else if (!Client.key2Name(target.Properties.CreatorID, out targetCreator))
                        targetCreator = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Unknown");
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Creator"), targetCreator, target.Properties.CreatorID);
                    sbResult.AppendLine();

                    if (target.Properties.OwnerID == UUID.Zero)
                        targetOwner = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else if (!Client.key2Name(target.Properties.OwnerID, out targetOwner))
                        targetOwner = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Unknown");
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Owner"), targetOwner, target.Properties.OwnerID);
                    sbResult.AppendLine();

                    if (target.Properties.LastOwnerID == UUID.Zero)
                        targerLastOwner = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else if (!Client.key2Name(target.Properties.LastOwnerID, out targerLastOwner))
                        targerLastOwner = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Unknown");
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.LastOwner"), targerLastOwner, target.Properties.LastOwnerID);
                    sbResult.AppendLine();

                    if (target.Properties.GroupID == UUID.Zero)
                        targetGroup = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else if (!Client.key2Name(target.Properties.GroupID, out targetGroup))
                        targetGroup = bot.Localization.clResourceManager.getText("Commands.PrimInfo.Unknown");
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Group"), targetGroup, target.Properties.GroupID);
                    sbResult.AppendLine();

                    if (target.Properties.SitName == "")
                        targetSitName = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else
                        targetSitName = target.Properties.SitName;
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.SitName"), targetSitName);
                    sbResult.AppendLine();

                    if (target.Properties.TouchName == "")
                        targetTouchName = bot.Localization.clResourceManager.getText("Commands.PrimInfo.None");
                    else
                        targetTouchName = target.Properties.TouchName;
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.TouchName"), targetTouchName);
                    sbResult.AppendLine();

                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Position"), target.Position.ToString());
                    sbResult.AppendLine();
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Date"), target.Properties.CreationDate.ToString());
                    sbResult.AppendLine();

                    if (target.Text != String.Empty)
                    {
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Text") + target.Text);
                        sbResult.AppendLine();
                    }

                    if (target.Light != null)
                    {
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Light"), target.Light.ToString());
                        sbResult.AppendLine();
                    }

                    if (target.ParticleSys.CRC != 0)
                    {
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Particles"), target.ParticleSys.ToString());
                        sbResult.AppendLine();
                    }

                    if (target.Textures != null)
                    {
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.TextureEntry"));
                        sbResult.AppendLine();
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Default"),
                            target.Textures.DefaultTexture.TextureID.ToString());
                        sbResult.AppendLine();

                        for (int i = 0; i < target.Textures.FaceTextures.Length; i++)
                        {
                            if (target.Textures.FaceTextures[i] != null)
                            {
                                sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Face"), i,
                                    target.Textures.FaceTextures[i].TextureID.ToString());
                                sbResult.AppendLine();
                            }
                        }    
                    }
                    else
                    {
                        sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Null"));
                        sbResult.AppendLine();
                    }

                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.PrimInfo.Done"));

                    return sbResult.ToString();
                }
                else
                {
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.PrimInfo.NotFound"), primID.ToString());
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.PrimInfo.Usage");
            }
        }
    }
}

