/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : DumpAttachmentCommand.cs
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
    using OpenMetaverse.Utilities;
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using OpenMetaverse.StructuredData;
    using OpenMetaverse.Assets;

    public class DumpAttachmentCommand : Command
    {
        List<UUID> Textures = new List<UUID>();
        AutoResetEvent GotPermissionsEvent = new AutoResetEvent(false);
        Primitive.ObjectProperties Properties;
        bool GotPermissions = false;
        UUID SelectedObject = UUID.Zero;
        Dictionary<UUID, Primitive> PrimsWaiting = new Dictionary<UUID, Primitive>();
        AutoResetEvent AllPropertiesReceived = new AutoResetEvent(false);
        private string DestinationDirectory = null;

        public DumpAttachmentCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Objects.ObjectPropertiesFamily += new EventHandler<ObjectPropertiesFamilyEventArgs>(Objects_OnObjectPropertiesFamily);
            SecondLifeBot.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_OnObjectProperties);
            SecondLifeBot.Avatars.ViewerEffectPointAt += new EventHandler<ViewerEffectPointAtEventArgs>(Avatars_ViewerEffectPointAt);

            base.Client = SecondLifeBot;
            base.Name = "dumpattachment";
            base.Description = bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Description") + " " + bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            StringBuilder builder = new StringBuilder();

            if (args.Length < 2)
            {
                return bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Usage");
            }

            switch (args[1])
            {
            // Chest
                case "Chest":
                    break;
            // Skull
                case "Skull":
                    break;
            // Left shoulder
                case "LeftShoulder":
                    break;
            // Right shoulder
                case "RightShoulder":
                    break;
            // Left hand
                case "LeftHand":
                    break;
            // Right hand
                case "RightHand":
                    break;
            // Left foot
                case "LeftFoot":
                    break;
            // Right foot
                case "RightFoot":
                    break;
            // Spine
                case "Spine":
                    break;
            // Pelvis
                case "Pelvis":
                    break;
            // Mouth
                case "Mouth":
                    break;
            // Chin
                case "Chin":
                    break;
            // Left ear
                case "LeftEar":
                    break;
            // Right ear
                case "RightEar":
                    break;
            // Left eyeball
                case "LeftEyeball":
                    break;
            // Right eyeball
                case "RightEyeball":
                    break;
            // Nose
                case "Nose":
                    break;
            // Right upper arm
                case "RightUpperArm":
                    break;
            // Right forearm
                case "RightForearm":
                    break;
            // Left upper arm
                case "LeftUpperArm":
                    break;
            // Left forearm
                case "LeftForearm":
                    break;
            // Right hip
                case "RightHip":
                    break;
            // Right upper leg
                case "RightUpperLeg":
                    break;
            // Right lower leg
                case "RightLowerLeg":
                    break;
            // Left hip
                case "LeftHip":
                    break;
            // Left upper leg
                case "LeftUpperLeg":
                    break;
            // Left lower leg
                case "LeftLowerLeg":
                    break;
            // Stomach
                case "Stomach":
                    break;
            // Left pectoral
                case "LeftPec":
                    break;
            // Right pectoral
                case "RightPec":
                    break;
            // Emerald viewer extra attachment points
            // Chest 2
                case "Chest2":
                    break;
            // Skull 2
                case "Skull2":
                    break;
            // Left shoulder 2
                case "LeftShoulder2":
                    break;
            // Right shoulder 2
                case "RightShoulder2":
                    break;
            // Left hand 2
                case "LeftHand2":
                    break;
            // Right hand 2
                case "RightHand2":
                    break;
            // Left foot 2
                case "LeftFoot2":
                    break;
            // Right foot 2
                case "RightFoot2":
                    break;
            // Spine 2
                case "Spine2":
                    break;
            // Pelvis 2
                case "Pelvis2":
                    break;
            // Mouth 2
                case "Mouth2":
                    break;
            // Chin 2
                case "Chin2":
                    break;
            // Left ear 2
                case "LeftEar2":
                    break;
            // Right ear 2
                case "RightEar2":
                    break;
            // Left eyeball 2
                case "LeftEyeball2":
                    break;
            // Right eyeball 2
                case "RightEyeball2":
                    break;
            // Nose 2
                case "Nose2":
                    break;
            // Right upper arm 2
                case "RightUpperArm2":
                    break;
            // Right forearm 2
                case "RightForearm2":
                    break;
            // Left upper arm 2
                case "LeftUpperArm2":
                    break;
            // Left forearm 2
                case "LeftForearm2":
                    break;
            // Right hip 2
                case "RightHip2":
                    break;
            // Right upper leg 2
                case "RightUpperLeg2":
                    break;
            // Right lower leg 2
                case "RightLowerLeg2":
                    break;
            // Left hip 2
                case "LeftHip2":
                    break;
            // Left upper leg 2
                case "LeftUpperLeg2":
                    break;
            // Left lower leg 2
                case "LeftLowerLeg2":
                    break;
            // Stomach 2
                case "Stomach2":
                    break;
            // Left pectoral 2
                case "LeftPec2":
                    break;
            // Right pectoral 2
                case "RightPec2":
                    break;
            // Left Knee
                case "LeftKnee":
                    break;
            // Right Knee
                case "RightKnee":
                    break;
            // Bridge
                case "Bridge":
                    break;
                default:
                    return bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Invalid");
            }

            Avatar av = base.Client.Network.CurrentSim.ObjectsAvatars.Find(
                            delegate(Avatar a)
                {
                    return a.ID.Equals((UUID)args[0]);
                }
                        );

            Program.NBStats.AddStatData(String.Format("{0}: {1} dumping attachment {2} of {3}.", DateTime.Now.ToString(), Client, args[1], args[0]));

            List<Primitive> list = base.Client.Network.CurrentSim.ObjectsPrimitives.FindAll(delegate(Primitive prim)
            {
                return prim.ParentID == av.LocalID;
            });

            for (int i = 0; i < list.Count; i++)
            {
                Primitive primitive = list[i];
                NBAttachmentPoint point = StateToAttachmentPoint(primitive.PrimData.State);

                if (point.ToString() == args[1])
                {
                    bool result;

                    if (args[1] == "false")
                    {
                        result = ExportAttachment(primitive.ID, primitive.LocalID, point.ToString(), (UUID)args[0], false);
                    }
                    else
                    {
                        result = ExportAttachment(primitive.ID, primitive.LocalID, point.ToString(), (UUID)args[0], true);
                    }

                    if (result)
                    {
                        builder.AppendLine(string.Format(bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Success"), new object[] { point }));
                    }
                    else
                    {
                        builder.AppendLine(string.Format(bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Fail"), new object[] { point }));
                    }
                }
            }

            DestinationDirectory = null;

            //builder.AppendLine("Exportados " + list.Count + " objetos");
            return builder.ToString();
        }

        public string CreateFileName(string AttachmentPointName, string PrimName)
        {
            string corrected_PrimName;
            string FinalName;

            if (PrimName == "")
                corrected_PrimName = "Object";
            else
                corrected_PrimName = PrimName;

            corrected_PrimName = corrected_PrimName.Replace(" ", "_");
            corrected_PrimName = corrected_PrimName.Replace(":", ";");
            corrected_PrimName = corrected_PrimName.Replace("*", "+");
            corrected_PrimName = corrected_PrimName.Replace("|", "I");
            corrected_PrimName = corrected_PrimName.Replace("\\", "[");
            corrected_PrimName = corrected_PrimName.Replace("/", "]");
            corrected_PrimName = corrected_PrimName.Replace("?", "¿");
            corrected_PrimName = corrected_PrimName.Replace(">", "}");
            corrected_PrimName = corrected_PrimName.Replace("<", "{");
            corrected_PrimName = corrected_PrimName.Replace("\"", "'");
            corrected_PrimName = corrected_PrimName.Replace("\n", " ");

            FinalName = corrected_PrimName + " (" + AttachmentPointName + ").xml";

            return FinalName;
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            string TextureDestination;

            if (DestinationDirectory == null)
                TextureDestination = "attachments/Textures/";
            else
                TextureDestination = DestinationDirectory + "Textures/";

            if (!Directory.Exists(TextureDestination))
                Directory.CreateDirectory(TextureDestination);

            if (state == TextureRequestState.Finished && Textures.Contains(asset.AssetID))
            {
                lock (Textures)
                    Textures.Remove(asset.AssetID);

                if (state == TextureRequestState.Finished)
                {
                    try
                    {
                        File.WriteAllBytes(TextureDestination + asset.AssetID + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.Message, Helpers.LogLevel.Error, Client);
                    }

                    if (asset.Decode())
                    {
                        try
                        {
                            File.WriteAllBytes(TextureDestination + asset.AssetID + ".tga", asset.Image.ExportTGA());
                        }
                        catch (Exception ex)
                        {
                            Logger.Log(ex.Message, Helpers.LogLevel.Error, Client);
                        }
                    }
                    else
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.FailDecode"), asset.AssetID);
                    }

                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.ImageDownloaded"), asset.AssetID);
                }
                else
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Assets.Image.FailDownload"), asset.AssetID, state);
                }
            }
        }

        void Avatars_ViewerEffectPointAt(object sender, ViewerEffectPointAtEventArgs e)
        {
            if (e.SourceID == Client.MasterKey)
            {
                //Client.DebugLog("Master is now selecting " + targetID.ToString());
                SelectedObject = e.TargetID;
            }
        }

        public static NBAttachmentPoint StateToAttachmentPoint(uint state)
        {
            const uint ATTACHMENT_MASK = 0xF0;
            uint fixedState = (((byte)state & ATTACHMENT_MASK) >> 4) | (((byte)state & ~ATTACHMENT_MASK) << 4);
            return (NBAttachmentPoint)fixedState;
        }

        private static bool CompareLocalIDs(uint id, uint SearchedID)
        {
            if (id == SearchedID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ExportAttachment(UUID ObjectUUID, uint LocalID, string AttachmentPosition, UUID AvatarID, bool DumpImages)
        {
            Predicate<Primitive> match = null;
            UUID id;

            //UUID.TryParse(ObjectUUID, out id);
            id = ObjectUUID;

            Primitive primitive = base.Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim)
            {
                return prim.ID == id;
            });
            if (primitive != null)
            {
                uint localid = LocalID;
                base.Client.Objects.SelectObject(base.Client.Network.CurrentSim, LocalID);
                base.Client.Objects.RequestObjectPropertiesFamily(base.Client.Network.CurrentSim, id);
                this.GotPermissionsEvent.WaitOne(0x2710, false);
#if !DEBUG
                if (!GotPermissions)
                {
                    //return "Couldn't fetch permissions for the requested object, try again";
                }
                else
                {
                    GotPermissions = false;
                    if (Properties.OwnerID != Client.Self.AgentID &&
                        Properties.OwnerID != Client.MasterKey || Properties.Permissions.EveryoneMask != PermissionMask.All)
                    {
                        return false;
                    }
                }
#endif
                this.GotPermissions = false;
                if (match == null)
                {
                    match = delegate(Primitive prim)
                    {
                        if (prim.LocalID != localid)
                        {
                            return prim.ParentID == localid;
                        }
                        return true;
                    };
                }
                List<Primitive> objects = base.Client.Network.CurrentSim.ObjectsPrimitives.FindAll(match);
                List<Primitive> objects2 = new List<Primitive>();

                bool complete = RequestObjectProperties(objects2, 250);

                if (!complete)
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.DumpAttachment.NotProperties"));
                    foreach (UUID uuid in PrimsWaiting.Keys)
                        bot.Console.WriteLine(uuid.ToString());
                }

                List<uint> ObjectsIDs = new List<uint>();

                for (int j = 0; j < objects.Count; j++)
                {
                    ObjectsIDs.Add(objects[j].LocalID);
                }

                for (int j = 0; j < objects.Count; j++)
                {
                    base.Client.Objects.SelectObject(base.Client.Network.CurrentSim, objects[j].LocalID);
                }

                for (int j = 0; j < objects.Count; j++)
                {
                    Primitive IntermediatePrimitive;

                    IntermediatePrimitive = objects[j];

                    if (ObjectsIDs.FindIndex(delegate(uint lid)
                    {
                        return lid == IntermediatePrimitive.ParentID;
                    }) == -1)
                    {
                        IntermediatePrimitive.ParentID = 0;
                    }

                    objects2.Add(IntermediatePrimitive);
                }

                for (int j = 0; j < objects.Count; j++)
                {
                    base.Client.Objects.DeselectObject(base.Client.Network.CurrentSim, objects[j].LocalID);
                }

                string output = OSDParser.SerializeLLSDXmlString(Helpers.PrimListToOSD(objects2));
                try
                {
                    string AvatarName;

                    if (DestinationDirectory == null)
                    {
                        if (!this.Client.key2Name(AvatarID, out AvatarName))
                            AvatarName = AvatarID.ToString();

                        DestinationDirectory = "./attachments/" + AvatarName + "_" +
                        System.DateTime.Now.Year.ToString() +
                        System.DateTime.Now.Month.ToString() +
                        System.DateTime.Now.Day.ToString() +
                        System.DateTime.Now.Hour.ToString() +
                            //System.DateTime.Now.Minute.ToString() +
                            //System.DateTime.Now.Second.ToString() +
                        "/";
                    }

                    string RealFileName;

                    if (objects[0].Properties != null)
                        RealFileName = CreateFileName(AttachmentPosition, objects[0].Properties.Name);
                    else
                        RealFileName = CreateFileName(AttachmentPosition, "Object");

                    if (!Directory.Exists(DestinationDirectory))
                        Directory.CreateDirectory(DestinationDirectory);
                    File.WriteAllText(DestinationDirectory + RealFileName, output);
                }
                catch (Exception e)
                {
                    base.Client.Objects.DeselectObject(base.Client.Network.CurrentSim, LocalID);
                    bot.Console.WriteLine(e.Message);
                    return false;
                }

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.DumpAttachment.Exported"), objects2.Count, DestinationDirectory, AttachmentPosition);

                // Create a list of all of the textures to download
                List<ImageRequest> textureRequests = new List<ImageRequest>();

                if (DumpImages == true)
                {
                    lock (Textures)
                    {
                        for (int i = 0; i < objects2.Count; i++)
                        {
                            Primitive prim = objects2[i];

                            if (prim.Textures.DefaultTexture.TextureID != Primitive.TextureEntry.WHITE_TEXTURE &&
                                !Textures.Contains(prim.Textures.DefaultTexture.TextureID))
                            {
                                Textures.Add(prim.Textures.DefaultTexture.TextureID);
                            }

                            for (int j = 0; j < prim.Textures.FaceTextures.Length; j++)
                            {
                                if (prim.Textures.FaceTextures[j] != null &&
                                    prim.Textures.FaceTextures[j].TextureID != Primitive.TextureEntry.WHITE_TEXTURE &&
                                    !Textures.Contains(prim.Textures.FaceTextures[j].TextureID))
                                {
                                    Textures.Add(prim.Textures.FaceTextures[j].TextureID);
                                }
                            }

                            if (prim.Sculpt != null)
                            {
                                if (prim.Sculpt.SculptTexture != UUID.Zero && !Textures.Contains(prim.Sculpt.SculptTexture))
                                {
                                    Textures.Add(prim.Sculpt.SculptTexture);
                                }
                            }
                        }

                        // Create a request list from all of the images
                        for (int i = 0; i < Textures.Count; i++)
                            textureRequests.Add(new ImageRequest(Textures[i], ImageType.Normal, 1013000.0f, 0));
                    }

                    // Download all of the textures in the export list
                    foreach (ImageRequest request in textureRequests)
                    {
                        base.Client.Assets.RequestImage(request.ImageID, request.Type, Assets_OnImageReceived);
                    }
                    
                }

                base.Client.Objects.DeselectObject(base.Client.Network.CurrentSim, LocalID);
                return true;
                //return ("XML exported, began downloading " + this.Textures.Count + " textures");
            }
            //return string.Concat(new object[] { "Couldn't find UUID ", id.ToString(), " in the ", base.Client.Network.CurrentSim.ObjectsPrimitives.Count, "objects currently indexed in the current simulator" });
            return false;
        }

        void Objects_OnObjectPropertiesFamily(object sender, ObjectPropertiesFamilyEventArgs e)
        {
            Properties = new Primitive.ObjectProperties();
            Properties.SetFamilyProperties(e.Properties);
            GotPermissions = true;
            GotPermissionsEvent.Set();
        }

        void Objects_OnObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            lock (PrimsWaiting)
            {
                PrimsWaiting.Remove(e.Properties.ObjectID);

                if (PrimsWaiting.Count == 0)
                    AllPropertiesReceived.Set();
            }
        }

        private bool RequestObjectProperties(List<Primitive> objects, int msPerRequest)
        {
            // Create an array of the local IDs of all the prims we are requesting properties for
            uint[] localids = new uint[objects.Count];

            lock (PrimsWaiting)
            {
                PrimsWaiting.Clear();

                for (int i = 0; i < objects.Count; ++i)
                {
                    localids[i] = objects[i].LocalID;
                    PrimsWaiting.Add(objects[i].ID, objects[i]);
                }
            }

            Client.Objects.SelectObjects(Client.Network.CurrentSim, localids);

            return AllPropertiesReceived.WaitOne(2000 + msPerRequest * objects.Count, false);
        }
    }
}
