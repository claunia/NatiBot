/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ChangePermsCommand.cs
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
    using System.Threading;

    public class ChangePermsCommand : Command
    {
        AutoResetEvent GotPermissionsEvent = new AutoResetEvent(false);
        UUID SelectedObject = UUID.Zero;
        Dictionary<UUID, Primitive> Objects = new Dictionary<UUID, Primitive>();
        PermissionMask Perms = PermissionMask.None;
        bool PermsSent = false;
        int PermCount = 0;

        public ChangePermsCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_OnObjectProperties);
            base.Name = "changeperms";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ChangePerms.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ChangePerms.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID rootID;
            Primitive rootPrim;
            List<Primitive> childPrims;
            List<uint> localIDs = new List<uint>();

            // Reset class-wide variables
            PermsSent = false;
            Objects.Clear();
            Perms = PermissionMask.None;
            PermCount = 0;

            if (args.Length < 1 || args.Length > 4)
                return bot.Localization.clResourceManager.getText("Commands.ChangePerms.Usage");

            if (!UUID.TryParse(args[0], out rootID))
                return bot.Localization.clResourceManager.getText("Commands.ChangePerms.Usage");

            for (int i = 1; i < args.Length; i++)
            {
                switch (args[i].ToLower())
                {
                    case "copy":
                        Perms |= PermissionMask.Copy;
                        break;
                    case "mod":
                        Perms |= PermissionMask.Modify;
                        break;
                    case "xfer":
                        Perms |= PermissionMask.Transfer;
                        break;
                    default:
                        return bot.Localization.clResourceManager.getText("Commands.ChangePerms.Usage");
                }
            }

            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.ChangePerms.Permisson"), Perms.ToString());

            // Find the requested prim
            rootPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim)
            {
                return prim.ID == rootID;
            });
            if (rootPrim == null)
                return String.Format(bot.Localization.clResourceManager.getText("Commands.ChangePerms.NotFound"), rootID.ToString());
            else
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.ChangePerms.Found"), rootPrim.ID.ToString());

            if (rootPrim.ParentID != 0)
            {
                // This is not actually a root prim, find the root
                if (!Client.Network.CurrentSim.ObjectsPrimitives.TryGetValue(rootPrim.ParentID, out rootPrim))
                    return bot.Localization.clResourceManager.getText("Commands.ChangePerms.RootNotFound");
                else
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.ChangePerms.Root"), rootPrim.ID.ToString());
            }

            // Find all of the child objects linked to this root
            childPrims = Client.Network.CurrentSim.ObjectsPrimitives.FindAll(delegate(Primitive prim)
            {
                return prim.ParentID == rootPrim.LocalID;
            });

            // Build a dictionary of primitives for referencing later
            Objects[rootPrim.ID] = rootPrim;
            for (int i = 0; i < childPrims.Count; i++)
                Objects[childPrims[i].ID] = childPrims[i];

            // Build a list of all the localIDs to set permissions for
            localIDs.Add(rootPrim.LocalID);
            for (int i = 0; i < childPrims.Count; i++)
                localIDs.Add(childPrims[i].LocalID);

            // Go through each of the three main permissions and enable or disable them
            #region Set Linkset Permissions

            PermCount = 0;
            if ((Perms & PermissionMask.Modify) == PermissionMask.Modify)
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Modify, true);
            else
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Modify, false);
            PermsSent = true;

            if (!GotPermissionsEvent.WaitOne(1000 * 30, false))
                return bot.Localization.clResourceManager.getText("Commands.ChangePerms.ModFail");

            PermCount = 0;
            if ((Perms & PermissionMask.Copy) == PermissionMask.Copy)
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Copy, true);
            else
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Copy, false);
            PermsSent = true;

            if (!GotPermissionsEvent.WaitOne(1000 * 30, false))
                return bot.Localization.clResourceManager.getText("Commands.ChangePerms.CopyFail");

            PermCount = 0;
            if ((Perms & PermissionMask.Transfer) == PermissionMask.Transfer)
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Transfer, true);
            else
                Client.Objects.SetPermissions(Client.Network.CurrentSim, localIDs, PermissionWho.NextOwner, PermissionMask.Transfer, false);
            PermsSent = true;

            if (!GotPermissionsEvent.WaitOne(1000 * 30, false))
                return bot.Localization.clResourceManager.getText("Commands.ChangePerms.XferFail");

            #endregion Set Linkset Permissions

            // Check each prim for task inventory and set permissions on the task inventory
            int taskItems = 0;
            foreach (Primitive prim in Objects.Values)
            {
                if ((prim.Flags & PrimFlags.InventoryEmpty) == 0)
                {
                    List<InventoryBase> items = Client.Inventory.GetTaskInventory(prim.ID, prim.LocalID, 1000 * 30);

                    if (items != null)
                    {
                        for (int i = 0; i < items.Count; i++)
                        {
                            if (!(items[i] is InventoryFolder))
                            {
                                InventoryItem item = (InventoryItem)items[i];
                                item.Permissions.NextOwnerMask = Perms;

                                Client.Inventory.UpdateTaskInventory(prim.LocalID, item);
                                ++taskItems;
                            }
                        }
                    }
                }
            }

            return String.Format(bot.Localization.clResourceManager.getText("Commands.ChangePerms.Done"), Perms.ToString(), localIDs.Count,
                taskItems);
        }

        void Objects_OnObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            if (PermsSent)
            {
                if (Objects.ContainsKey(e.Properties.ObjectID))
                {
                    // FIXME: Confirm the current operation against properties.Permissions.NextOwnerMask

                    ++PermCount;
                    if (PermCount >= Objects.Count)
                        GotPermissionsEvent.Set();
                }
            }
        }
    }
}

