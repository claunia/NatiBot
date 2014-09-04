/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AvatarInfoCommand.cs
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
    using System.Collections.Generic;
    using System.Text;
    using System.Threading;

    public class AvatarInfoCommand : Command
    {
        ManualResetEvent WaitforAvatar = new ManualResetEvent(false);
        Avatar.AvatarProperties foundAvProperties;
        Avatar.Interests foundAvInterests;
        List<AvatarGroup> foundAvGroups = new List<AvatarGroup>();
        UUID foundAvUUID;
        bool foundAvPropertiesCorrectlyGot = false;
        bool foundAvInterestsCorrectlyGot = false;
        bool foundAvGroupsCorrectlyGot = false;
        bool moreThanOneAvFound = false;

        public AvatarInfoCommand(SecondLifeBot SecondLifeBot)
        {
            Name = "avatarinfo";
            Description = bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Description") + " " + bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Usage");
        }

        void Avatars_AvatarPropertiesReply(object sender, AvatarPropertiesReplyEventArgs e)
        {
            if (e.AvatarID == foundAvUUID)
            {
                foundAvPropertiesCorrectlyGot = true;
                foundAvProperties = e.Properties;
            }
            else
            {
                foundAvPropertiesCorrectlyGot = false;
            }

            WaitforAvatar.Set();
            return;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            Client.Avatars.AvatarPropertiesReply += new EventHandler<AvatarPropertiesReplyEventArgs>(Avatars_AvatarPropertiesReply);

            moreThanOneAvFound = false;

            foundAvUUID = UUID.Zero;

            Avatar foundAv = null;

            foundAvProperties = new Avatar.AvatarProperties();
            foundAvInterests = new Avatar.Interests();
            foundAvGroups = new List<AvatarGroup>();
            WaitforAvatar = new ManualResetEvent(false);

            if (args.Length != 2)
                return bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Usage");

            string targetName = String.Format("{0} {1}", args[0], args[1]);

            Client.FindOneAvatar(targetName, out foundAvUUID, out moreThanOneAvFound);

            foundAv = Client.Network.CurrentSim.ObjectsAvatars.Find(
                delegate(Avatar avatar)
                {
                    return (avatar.Name == targetName);
                }
            );

            if (foundAvUUID != UUID.Zero)
            {
                StringBuilder output = new StringBuilder();

                if (moreThanOneAvFound)
                {
                    output.AppendFormat("More than one avatar found with that search terms.");
                    output.AppendLine();
                    output.AppendFormat("{0} ({1})", targetName, foundAvUUID);
                }
                else
                {
                    output.AppendFormat("{0} ({1})", targetName, foundAvUUID);
                }

                output.AppendLine();

                Client.Avatars.RequestAvatarProperties(foundAvUUID);

                if (!WaitforAvatar.WaitOne(10000, false))
                {
                    Client.Avatars.AvatarPropertiesReply -= Avatars_AvatarPropertiesReply;
                    output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.NotProfile"));
                }
                else
                {
                    Client.Avatars.AvatarPropertiesReply -= Avatars_AvatarPropertiesReply;
                    if (foundAvPropertiesCorrectlyGot == true)
                    {
                        // CLAUNIA
                        // For some reason it is getting offline
                        /*
                        switch (foundAvProperties.Online)
                        {
                            case true:
                                output.AppendFormat("Avatar conectado");
                                output.AppendLine();
                                break;
                            case false:
                                output.AppendFormat("Avatar NO conectado");
                                output.AppendLine();
                                break;
                        }

                        */

                        output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.SecondLife"));
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Born"), foundAvProperties.BornOn);
                        output.AppendLine();
                        //output.AppendFormat("    Flags: {0}", foundAvProperties.Flags.ToString()); output.AppendLine();
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.SecondPhoto"), foundAvProperties.ProfileImage.ToString());
                        output.AppendLine();

                        output.Append(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Account"));
                        if (foundAvProperties.Identified)
                            output.Append(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Identified"));
                        if (foundAvProperties.MaturePublish)
                            output.Append(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Adult"));
                        if (foundAvProperties.Transacted)
                            output.Append(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Payment"));
                        if (foundAvProperties.AllowPublish)
                            output.Append(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Public"));
                        output.AppendLine(".");

                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Partner"), foundAvProperties.Partner.ToString());
                        output.AppendLine();
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Charter"), foundAvProperties.CharterMember);
                        output.AppendLine();

                        WaitforAvatar.Reset();
                        Client.Avatars.AvatarGroupsReply += new EventHandler<AvatarGroupsReplyEventArgs>(Avatars_OnAvatarGroups);
                        Client.Avatars.RequestAvatarProperties(foundAvUUID);

                        if (!WaitforAvatar.WaitOne(2500, false))
                        {
                            Client.Avatars.AvatarGroupsReply -= Avatars_OnAvatarGroups;
                            output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.NotGroups"));
                        }
                        else
                        {
                            Client.Avatars.AvatarGroupsReply -= Avatars_OnAvatarGroups;
                            if (foundAvGroupsCorrectlyGot)
                            {
                                output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Groups"));
                                foreach (AvatarGroup avGroup in foundAvGroups)
                                {
                                    output.AppendFormat("        {0} ({1})", avGroup.GroupName, avGroup.GroupID);
                                    output.AppendLine();
                                }
                            }
                        }

                        output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.About"));
                        output.AppendFormat("    {0}", foundAvProperties.AboutText);
                        output.AppendLine();
                        output.AppendLine();
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.WebProfile"), foundAvProperties.ProfileURL);
                        output.AppendLine();
                        output.AppendLine();
                        
                        WaitforAvatar.Reset();
                        Client.Avatars.AvatarInterestsReply += new EventHandler<AvatarInterestsReplyEventArgs>(Avatars_OnAvatarInterests);
                        Client.Avatars.RequestAvatarProperties(foundAvUUID);

                        if (!WaitforAvatar.WaitOne(1000, false))
                        {
                            Client.Avatars.AvatarInterestsReply -= Avatars_OnAvatarInterests;
                            output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.NotInterests"));
                        }
                        else
                        {
                            Client.Avatars.AvatarInterestsReply -= Avatars_OnAvatarInterests;
                            if (foundAvInterestsCorrectlyGot)
                            {
                                output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Interests"));
                                output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Wants"), foundAvInterests.WantToMask.ToString("X"), foundAvInterests.WantToText);
                                output.AppendLine();
                                output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Skills"), foundAvInterests.SkillsMask.ToString("X"), foundAvInterests.SkillsText);
                                output.AppendLine();
                                output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Languages"), foundAvInterests.LanguagesText);
                                output.AppendLine();
                                output.AppendLine();
                            }
                        }

                        output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.FirstLife"));
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.FirstPhoto"), foundAvProperties.FirstLifeImage.ToString());
                        output.AppendLine();
                        output.AppendFormat(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Information"), foundAvProperties.FirstLifeText);
                        output.AppendLine();
                        output.AppendLine();
                    }
                    else
                    {
                        output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Error"));
                    }
                }

                // CLAUNIA
                // There is no event nor method for requesting statistics.
                /*
                output.AppendLine("Estadísticas:");
                output.AppendFormat("    Apariencia: {0} votos positivos, {1} votos negativos", foundAv.ProfileStatistics.AppearancePositive, foundAv.ProfileStatistics.AppearanceNegative); output.AppendLine();
                output.AppendFormat("    Comportamiento: {0} votos positivos, {1} votos negativos", foundAv.ProfileStatistics.BehaviorPositive, foundAv.ProfileStatistics.BehaviorNegative); output.AppendLine();
                output.AppendFormat("    Construcción: {0} votos positivos, {1} votos negativos", foundAv.ProfileStatistics.BuildingPositive, foundAv.ProfileStatistics.BuildingNegative); output.AppendLine();
                output.AppendFormat("    Votos emitidos: {0} positivos, {1} negativos", foundAv.ProfileStatistics.GivenPositive, foundAv.ProfileStatistics.GivenNegative); output.AppendLine();

                output.AppendLine();*/

                if (foundAv != null)
                {
                    if (foundAv.Textures != null)
                    {
                        output.AppendLine(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.Textures"));
                        for (int i = 0; i < foundAv.Textures.FaceTextures.Length; i++)
                        {
                            if (foundAv.Textures.FaceTextures[i] != null)
                            {
                                Primitive.TextureEntryFace face = foundAv.Textures.FaceTextures[i];
                                AvatarTextureIndex type = (AvatarTextureIndex)i;

                                output.AppendFormat("    {0}: {1}", type, face.TextureID);
                                output.AppendLine();
                            }
                        }
                    }
                }

                return output.ToString();
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.AvatarInfo.NotFound"), targetName);
            }
        }

        void Avatars_OnAvatarGroups(object sender, AvatarGroupsReplyEventArgs e)
        {
            if (e.AvatarID == foundAvUUID)
            {
                foundAvGroupsCorrectlyGot = true;
                foundAvGroups = e.Groups;
            }
            else
            {
                foundAvGroupsCorrectlyGot = false;
            }

            WaitforAvatar.Set();
            return;
        }

        void Avatars_OnAvatarInterests(object sender, AvatarInterestsReplyEventArgs e)
        {
            if (e.AvatarID == foundAvUUID)
            {
                foundAvInterestsCorrectlyGot = true;
                foundAvInterests = e.Interests;
            }
            else
            {
                foundAvInterestsCorrectlyGot = false;
            }

            WaitforAvatar.Set();
            return;
        }
    }
}
