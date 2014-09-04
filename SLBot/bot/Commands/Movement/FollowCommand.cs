/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FollowCommand.cs
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

    public class FollowCommand : Command
    {
        public FollowCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "follow";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Follow.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Follow.Usage");
            SecondLifeBot.Network.RegisterCallback(PacketType.AlertMessage, AlertMessageHandler);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            string target = String.Empty;
            for (int ct = 0; ct < args.Length; ct++)
                target = target + args[ct] + " ";
            target = target.TrimEnd();

            if (target.Length > 0)
            {
                if (args[0] == "stop")
                {
                    Active = false;
                    return bot.Localization.clResourceManager.getText("Commands.Follow.Stopped");
                }

                if (Follow(target))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.Following"), target);
                else
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.Unable"), target);
            }
            else
            {
                if (Client.MasterKey != UUID.Zero)
                {
                    if (Follow(Client.MasterKey))
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.FollowingUUID"), Client.MasterKey);
                    else
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.UnableUUID"), Client.MasterKey);
                }
                else if (Client.MasterName != String.Empty)
                {
                    if (Follow(Client.MasterName))
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.Following"), Client.MasterName);
                    else
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Follow.Unable"), Client.MasterName);
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.Follow.NoMaster") + " " + bot.Localization.clResourceManager.getText("Commands.Follow.Usage");
                }
            }
        }

        const float DISTANCE_BUFFER = 3.0f;
        uint targetLocalID = 0;

        bool Follow(string name)
        {
            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Avatar target = Client.Network.Simulators[i].ObjectsAvatars.Find(
                                        delegate(Avatar avatar)
                        {
                            return avatar.Name == name;
                        }
                                    );

                    if (target != null)
                    {
                        targetLocalID = target.LocalID;
                        Active = true;
                        return true;
                    }
                }
            }

            Active = false;
            return false;
        }

        bool Follow(UUID id)
        {
            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Avatar target = Client.Network.Simulators[i].ObjectsAvatars.Find(
                                        delegate(Avatar avatar)
                        {
                            return avatar.ID == id;
                        }
                                    );

                    if (target != null)
                    {
                        targetLocalID = target.LocalID;
                        Active = true;
                        return true;
                    }
                }
            }

            Active = false;
            return false;
        }

        public override void Think()
        {
            // Find the target position
            lock (Client.Network.Simulators)
            {
                for (int i = 0; i < Client.Network.Simulators.Count; i++)
                {
                    Avatar targetAv;

                    if (Client.Network.Simulators[i].ObjectsAvatars.TryGetValue(targetLocalID, out targetAv))
                    {
                        float distance = 0.0f;

                        if (Client.Network.Simulators[i] == Client.Network.CurrentSim)
                        {
                            distance = Vector3.Distance(targetAv.Position, Client.Self.SimPosition);
                        }
                        else
                        {
                            // FIXME: Calculate global distances
                        }

                        if (distance > DISTANCE_BUFFER)
                        {
                            uint regionX, regionY;
                            Utils.LongToUInts(Client.Network.Simulators[i].Handle, out regionX, out regionY);

                            double xTarget = (double)targetAv.Position.X + (double)regionX;
                            double yTarget = (double)targetAv.Position.Y + (double)regionY;
                            double zTarget = targetAv.Position.Z - 2f;

                            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Follow.Autopilot"),
                                distance, xTarget, yTarget, zTarget);

                            Client.Self.AutoPilot(xTarget, yTarget, zTarget);
                        }
                        else
                        {
                            // We are in range of the target and moving, stop moving
                            Client.Self.AutoPilotCancel();
                        }
                    }
                }
            }

            base.Think();
        }

        private void AlertMessageHandler(object sender, PacketReceivedEventArgs e)
        {
            AlertMessagePacket alert = (AlertMessagePacket)e.Packet;
            string message = Utils.BytesToString(alert.AlertData.Message);

            if (message.Contains("Autopilot cancel"))
            {
                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Follow.AutopilotCancelled"));
            }
        }
    }
}

