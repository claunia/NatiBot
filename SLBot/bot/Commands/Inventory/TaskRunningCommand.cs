/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : TaskRunningCommand.cs
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

    public class TaskRunningCommand : Command
    {
        public TaskRunningCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "taskrunning";
            base.Description = bot.Localization.clResourceManager.getText("Commands.TaskRunning.Description") + " " + bot.Localization.clResourceManager.getText("Commands.TaskRunning.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length != 1)
                return bot.Localization.clResourceManager.getText("Commands.TaskRunning.Usage");

            uint objectLocalID;
            UUID objectID;

            if (!UUID.TryParse(args[0], out objectID))
                return bot.Localization.clResourceManager.getText("Commands.TaskRunning.Usage");

            Primitive found = Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim)
            {
                return prim.ID == objectID;
            });
            if (found != null)
                objectLocalID = found.LocalID;
            else
                return String.Format(bot.Localization.clResourceManager.getText("Commands.TaskRunning.NotFound"), objectID);

            List<InventoryBase> items = Client.Inventory.GetTaskInventory(objectID, objectLocalID, 1000 * 30);

            //bool wantSet = false;
            bool setTaskTo = false;
            if (items != null)
            {
                string result = String.Empty;
                string matching = String.Empty;
                bool setAny = false;
                if (args.Length > 1)
                {
                    matching = args[1];

                    string tf;
                    if (args.Length > 2)
                    {
                        tf = args[2];
                    }
                    else
                    {
                        tf = matching.ToLower();
                    }
                    if (tf == "true")
                    {
                        setAny = true;
                        setTaskTo = true;
                    }
                    else if (tf == "false")
                    {
                        setAny = true;
                        setTaskTo = false;
                    }

                }
                bool wasRunning = false;

                EventHandler<ScriptRunningReplyEventArgs> callback;
                using (AutoResetEvent OnScriptRunningReset = new AutoResetEvent(false))
                {
                    callback = ((object sender, ScriptRunningReplyEventArgs e) =>
                    {
                        if (e.ObjectID == objectID)
                        {
                            result += String.Format(bot.Localization.clResourceManager.getText("Commands.TaskRunning.Running"), e.IsMono, e.IsRunning);
                            wasRunning = e.IsRunning;
                            OnScriptRunningReset.Set();
                        }
                    });

                    Client.Inventory.ScriptRunningReply += callback;

                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i] is InventoryFolder)
                        {
                            // this shouldn't happen this year
                            result += String.Format(bot.Localization.clResourceManager.getText("Commands.TaskRunning.Folder"), items[i].Name) + Environment.NewLine;
                        }
                        else
                        {
                            InventoryItem item = (InventoryItem)items[i];
                            AssetType assetType = item.AssetType;
                            result += String.Format(bot.Localization.clResourceManager.getText("Commands.TaskRunning.Item"), item.Name, item.Description,
                                assetType);
                            if (assetType == AssetType.LSLBytecode || assetType == AssetType.LSLText)
                            {
                                OnScriptRunningReset.Reset();
                                Client.Inventory.RequestGetScriptRunning(objectID, item.UUID);
                                if (!OnScriptRunningReset.WaitOne(10000, true))
                                {
                                    result += bot.Localization.clResourceManager.getText("Commands.TaskRunning.NoInfo");
                                }
                                if (setAny && item.Name.Contains(matching))
                                {
                                    if (wasRunning != setTaskTo)
                                    {
                                        OnScriptRunningReset.Reset();
                                        result += bot.Localization.clResourceManager.getText("Commands.TaskRunning.Setting") + setTaskTo + " => ";
                                        Client.Inventory.RequestSetScriptRunning(objectID, item.UUID, setTaskTo);
                                        if (!OnScriptRunningReset.WaitOne(10000, true))
                                        {
                                            result += bot.Localization.clResourceManager.getText("Commands.TaskRunning.NotSet");
                                        }
                                    }
                                }
                            }

                            result += Environment.NewLine;
                        }
                    }
                }
                Client.Inventory.ScriptRunningReply -= callback;
                return result;
            }
            else
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.TaskRunning.failed"), objectLocalID);
            }
        }
    }
}
