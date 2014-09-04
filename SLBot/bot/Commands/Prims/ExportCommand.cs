/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ExportCommand.cs
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
    using OpenMetaverse.StructuredData;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using OpenMetaverse.Assets;

    public class ExportCommand : Command
    {
        List<UUID> Textures = new List<UUID>();
        AutoResetEvent GotPermissionsEvent = new AutoResetEvent(false);
        Primitive.ObjectProperties Properties;
        bool GotPermissions = false;
        UUID SelectedObject = UUID.Zero;

        Dictionary<UUID, Primitive> PrimsWaiting = new Dictionary<UUID, Primitive>();
        AutoResetEvent AllPropertiesReceived = new AutoResetEvent(false);

        public ExportCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Objects.ObjectPropertiesFamily += new EventHandler<ObjectPropertiesFamilyEventArgs>(Objects_OnObjectPropertiesFamily);
            SecondLifeBot.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_OnObjectProperties);
            SecondLifeBot.Avatars.ViewerEffectPointAt += new EventHandler<ViewerEffectPointAtEventArgs>(Avatars_ViewerEffectPointAt);
            base.Name = "export";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Export.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Export.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length != 2 && !(args.Length == 1 && SelectedObject != UUID.Zero))
                return bot.Localization.clResourceManager.getText("Commands.Export.Usage");

            UUID id;
            uint localid;
            string file;

            if (args.Length == 2)
            {
                file = args[1];
                if (!UUID.TryParse(args[0], out id))
                    return bot.Localization.clResourceManager.getText("Commands.Export.Usage");
            }
            else
            {
                file = args[0];
                id = SelectedObject;
            }

            Primitive exportPrim;

            exportPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                delegate(Primitive prim)
                {
                    return prim.ID == id;
                }
            );

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting object {2} on sim {3}.", DateTime.Now.ToString(), Client, id, Client.Network.CurrentSim.Name));

            if (exportPrim != null)
            {
                if (exportPrim.ParentID != 0)
                    localid = exportPrim.ParentID;
                else
                    localid = exportPrim.LocalID;

                // Check for export permission first
                Client.Objects.SelectObject(Client.Network.CurrentSim, localid);
                Client.Objects.RequestObjectPropertiesFamily(Client.Network.CurrentSim, id);
                GotPermissionsEvent.WaitOne(1000 * 10, false);

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
                        return bot.Localization.clResourceManager.getText("NoPermissions");
                    }
                }
#endif

                List<Primitive> prims = Client.Network.CurrentSim.ObjectsPrimitives.FindAll(
                                            delegate(Primitive prim)
                    {
                        return (prim.LocalID == localid || prim.ParentID == localid);
                    }
                                        );

                bool complete = RequestObjectProperties(prims, 250);

                if (!complete)
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Export.NotProperties"));
                    //foreach (UUID uuid in PrimsWaiting.Keys)
                    //    bot.Console.WriteLine(uuid.ToString());
                }

                string output = OSDParser.SerializeLLSDXmlString(Helpers.PrimListToOSD(prims));

                if (Directory.Exists("objects/") == false)
                {
                    Directory.CreateDirectory("objects/");
                }

                try
                {
                    File.WriteAllText("objects/" + file, output);
                }
                catch (Exception e)
                {
                    return e.Message;
                }

                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Export.Exported"), prims.Count, file);

                // Create a list of all of the textures to download
                List<ImageRequest> textureRequests = new List<ImageRequest>();

                lock (Textures)
                {
                    for (int i = 0; i < prims.Count; i++)
                    {
                        Primitive prim = prims[i];

                        if (prim.Textures != null)
                        {
                            if (prim.Textures.DefaultTexture != null) //WTF
                            {
                                if (prim.Textures.DefaultTexture.TextureID != Primitive.TextureEntry.WHITE_TEXTURE &&
                                    !Textures.Contains(prim.Textures.DefaultTexture.TextureID))
                                {
                                    Textures.Add(prim.Textures.DefaultTexture.TextureID);
                                }
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
                        }

                        if (prim.Sculpt != null && prim.Sculpt.SculptTexture != UUID.Zero && !Textures.Contains(prim.Sculpt.SculptTexture))
                        {
                            Textures.Add(prim.Sculpt.SculptTexture);
                        }
                    }

                    // Create a request list from all of the images
                    for (int i = 0; i < Textures.Count; i++)
                        textureRequests.Add(new ImageRequest(Textures[i], ImageType.Normal, 1013000.0f, 0));
                }

                // Download all of the textures in the export list
                foreach (ImageRequest request in textureRequests)
                {
                    Client.Assets.RequestImage(request.ImageID, request.Type, Assets_OnImageReceived);
                }

                Client.Objects.DeselectObject(base.Client.Network.CurrentSim, localid);
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Export.Downloading"), Textures.Count);
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Export.NotFound"), id.ToString(),
                    Client.Network.CurrentSim.ObjectsPrimitives.Count);
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

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            if (Directory.Exists("textures/") == false)
            {
                Directory.CreateDirectory("textures/");
            }

            if (state == TextureRequestState.Finished && Textures.Contains(asset.AssetID))
            {
                lock (Textures)
                    Textures.Remove(asset.AssetID);

                if (state == TextureRequestState.Finished)
                {
                    try
                    {
                        File.WriteAllBytes("textures/" + asset.AssetID + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
                    }

                    if (asset.Decode())
                    {
                        try
                        {
                            File.WriteAllBytes("textures/" + asset.AssetID + ".tga", asset.Image.ExportTGA());
                        }
                        catch (Exception ex)
                        {
                            bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, Client);
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
    }
}

