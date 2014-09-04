/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ImportCommand.cs
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

    public class ImportCommand : Command
    {
        private enum ImporterState
        {
            RezzingParent,
            RezzingChildren,
            Linking,
            Idle
        }

        private class Linkset
        {
            public Primitive RootPrim;
            public List<Primitive> Children = new List<Primitive>();

            public Linkset()
            {
                RootPrim = new Primitive();
            }

            public Linkset(Primitive rootPrim)
            {
                RootPrim = rootPrim;
            }
        }

        Primitive currentPrim;
        Vector3 currentPosition;
        AutoResetEvent primDone = new AutoResetEvent(false);
        List<Primitive> primsCreated;
        List<uint> linkQueue;
        uint rootLocalID;
        ImporterState state = ImporterState.Idle;

        public ImportCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "import";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Import.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Import.Usage");
            SecondLifeBot.Objects.ObjectUpdate += Objects_OnNewPrim;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            primDone.Reset();

            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.Import.Usage");
            
            string filename = args[0];
            UUID GroupID = (args.Length > 1) ? Client.GroupID : UUID.Zero;
            string xml;
            List<Primitive> prims;

            if (File.Exists(filename))
            {
                try
                {
                    xml = File.ReadAllText(filename);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            else
            {
                try
                {
                    xml = File.ReadAllText("./objects/" + filename);
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }

            try
            {
                prims = Helpers.OSDToPrimList(OSDParser.DeserializeLLSDXml(xml));
            }
            catch (Exception e)
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Import.DeserializeFail"), filename, e.Message);
            }

            // Build an organized structure from the imported prims
            Dictionary<uint, Linkset> linksets = new Dictionary<uint, Linkset>();
            for (int i = 0; i < prims.Count; i++)
            {
                Primitive prim = prims[i];

                if (prim.ParentID == 0)
                {
                    if (linksets.ContainsKey(prim.LocalID))
                        linksets[prim.LocalID].RootPrim = prim;
                    else
                        linksets[prim.LocalID] = new Linkset(prim);
                }
                else
                {
                    if (!linksets.ContainsKey(prim.ParentID))
                        linksets[prim.ParentID] = new Linkset();

                    linksets[prim.ParentID].Children.Add(prim);
                }
            }

            primsCreated = new List<Primitive>();
            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Import.Importing"), linksets.Count);

            foreach (Linkset linkset in linksets.Values)
            {
                if (linkset.RootPrim.LocalID != 0)
                {
                    state = ImporterState.RezzingParent;
                    currentPrim = linkset.RootPrim;
                    // HACK: Import the structure just above our head
                    // We need a more elaborate solution for importing with relative or absolute offsets
                    linkset.RootPrim.Position = Client.Self.SimPosition;
                    linkset.RootPrim.Position.Z += 3.0f;
                    currentPosition = linkset.RootPrim.Position;

                    // Rez the root prim with no rotation
                    Quaternion rootRotation = linkset.RootPrim.Rotation;
                    linkset.RootPrim.Rotation = Quaternion.Identity;

                    Client.Objects.AddPrim(Client.Network.CurrentSim, linkset.RootPrim.PrimData, GroupID,
                        linkset.RootPrim.Position, linkset.RootPrim.Scale, linkset.RootPrim.Rotation);

                    if (!primDone.WaitOne(15000, false))
                    {
                        primsCreated.Clear();
                        return bot.Localization.clResourceManager.getText("Commands.Import.RootFail");
                    }

                    Client.Objects.SetPosition(Client.Network.CurrentSim, primsCreated[primsCreated.Count - 1].LocalID, linkset.RootPrim.Position);

                    state = ImporterState.RezzingChildren;

                    // Rez the child prims
                    foreach (Primitive prim in linkset.Children)
                    {
                        currentPrim = prim;
                        currentPosition = prim.Position + linkset.RootPrim.Position;

                        Client.Objects.AddPrim(Client.Network.CurrentSim, prim.PrimData, GroupID, currentPosition,
                            prim.Scale, prim.Rotation);

                        if (!primDone.WaitOne(15000, false))
                        {
                            primsCreated.Clear();
                            return bot.Localization.clResourceManager.getText("Commands.Import.ChildFail");
                        }
                        Client.Objects.SetPosition(Client.Network.CurrentSim, primsCreated[primsCreated.Count - 1].LocalID, currentPosition);

                    }

                    // Create a list of the local IDs of the newly created prims
                    List<uint> primIDs = new List<uint>(primsCreated.Count);
                    primIDs.Add(rootLocalID); // Root prim is first in list.

                    if (linkset.Children.Count != 0)
                    {
                        // Add the rest of the prims to the list of local IDs
                        foreach (Primitive prim in primsCreated)
                        {
                            if (prim.LocalID != rootLocalID)
                                primIDs.Add(prim.LocalID);
                        }
                        linkQueue = new List<uint>(primIDs.Count);
                        linkQueue.AddRange(primIDs);

                        // Link and set the permissions + rotation
                        state = ImporterState.Linking;
                        Client.Objects.LinkPrims(Client.Network.CurrentSim, linkQueue);

                        if (primDone.WaitOne(1000 * linkset.Children.Count, false))
                            Client.Objects.SetRotation(Client.Network.CurrentSim, rootLocalID, rootRotation);
                        else
                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Import.LinkFail"), linkQueue.Count);

                    }
                    else
                    {
                        Client.Objects.SetRotation(Client.Network.CurrentSim, rootLocalID, rootRotation);
                    }

                    // Set permissions on newly created prims
                    Client.Objects.SetPermissions(Client.Network.CurrentSim, primIDs,
                        PermissionWho.Everyone | PermissionWho.Group | PermissionWho.NextOwner,
                        PermissionMask.All, true);

                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Import.DeRezzing"));
                    Client.Inventory.RequestDeRezToInventory(rootLocalID);

                    state = ImporterState.Idle;
                }
                else
                {
                    // Skip linksets with a missing root prim
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Import.MissingRoot"));
                }

                // Reset everything for the next linkset
                primsCreated.Clear();
            }

            return bot.Localization.clResourceManager.getText("Commands.Import.Complete");
        }

        void Objects_OnNewPrim(object sender, PrimEventArgs e)
        {
            Primitive prim = e.Prim;

            if ((prim.Flags & PrimFlags.CreateSelected) == 0)
                return; // We received an update for an object we didn't create

            switch (state)
            {
                case ImporterState.RezzingParent:
                    rootLocalID = prim.LocalID;
                    goto case ImporterState.RezzingChildren;
                case ImporterState.RezzingChildren:
                    if (!primsCreated.Contains(prim))
                    {
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Import.Properties"), prim.LocalID);
                        // TODO: Is there a way to set all of this at once, and update more ObjectProperties stuff?
                        Client.Objects.SetPosition(e.Simulator, prim.LocalID, currentPosition);
                        Client.Objects.SetTextures(e.Simulator, prim.LocalID, currentPrim.Textures);

                        if (currentPrim.Light.Intensity > 0)
                        {
                            Client.Objects.SetLight(e.Simulator, prim.LocalID, currentPrim.Light);
                        }

                        Client.Objects.SetFlexible(e.Simulator, prim.LocalID, currentPrim.Flexible);

                        if (currentPrim.Sculpt.SculptTexture != UUID.Zero)
                        {
                            Client.Objects.SetSculpt(e.Simulator, prim.LocalID, currentPrim.Sculpt);
                        }

                        if (!String.IsNullOrEmpty(currentPrim.Properties.Name))
                            Client.Objects.SetName(e.Simulator, prim.LocalID, currentPrim.Properties.Name);
                        if (!String.IsNullOrEmpty(currentPrim.Properties.Description))
                            Client.Objects.SetDescription(e.Simulator, prim.LocalID, currentPrim.Properties.Description);

                        primsCreated.Add(prim);
                        primDone.Set();
                    }
                    break;
                case ImporterState.Linking:
                    lock (linkQueue)
                    {
                        int index = linkQueue.IndexOf(prim.LocalID);
                        if (index != -1)
                        {
                            linkQueue.RemoveAt(index);
                            if (linkQueue.Count == 0)
                                primDone.Set();
                        }
                    }
                    break;
            }
        }
    }
}

