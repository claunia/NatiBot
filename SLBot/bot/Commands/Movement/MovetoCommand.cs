/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : MovetoCommand.cs
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

    internal class MovetoCommand : Command
    {
        public MovetoCommand(SecondLifeBot client)
        {
            base.Name = "moveto";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Moveto.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Moveto.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Vector3 newDirection;
            UUID targetID = UUID.Zero;
            string avatarName = "";
            double x = 0.0d;
            double y = x;
            double z = x;
            bool isGroupKey = false;
            Avatar foundAv = null;
            Primitive foundPrim;
            uint regionX, regionY;
            Utils.LongToUInts(Client.Network.CurrentSim.Handle, out regionX, out regionY);

            if (args.Length == 1)
            {
                if (!UUID.TryParse(args[0], out targetID))
                    return bot.Localization.clResourceManager.getText("Commands.Moveto.Usage");
            }
            else if (args.Length == 2)
            {
                avatarName = args[0] + " " + args[1];
                if (!Client.FindOneAvatar(avatarName, out targetID))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.TurnTo.AvNotFound"), avatarName);
            }
            else if (args.Length == 3)
            {
                if (!Double.TryParse(args[0], out x) ||
                    !Double.TryParse(args[1], out y) ||
                    !Double.TryParse(args[2], out z))
                {
                    return bot.Localization.clResourceManager.getText("Commands.Moveto.Usage");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Moveto.Usage");
            }

            if (args.Length != 3)
            {
                if (targetID != UUID.Zero)
                {
                    Client.key2Name(targetID, out avatarName, out isGroupKey);

                    if (isGroupKey)
                        return bot.Localization.clResourceManager.getText("Commands.Moveto.CannotGroup");

                    if (avatarName != "")
                    {
                        foundAv = Client.Network.CurrentSim.ObjectsAvatars.Find(
                            delegate(Avatar avatar)
                            {
                                return (avatar.Name == avatarName);
                            }
                        );

                        if (foundAv == null)
                            return String.Format(bot.Localization.clResourceManager.getText("Commands.Moveto.AvNotInSim"), avatarName);

                        if (foundAv.ParentID != 0)
                        {
                            Primitive SitPrim;

                            SitPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                                delegate(Primitive prim)
                                {
                                    return prim.LocalID == foundAv.ParentID;
                                }
                            );

                            if (SitPrim == null)
                                return String.Format(bot.Localization.clResourceManager.getText("Commands.Moveto.AvSit"), avatarName);
                            else
                                newDirection = SitPrim.Position;
                        }
                        else
                        {
                            newDirection = foundAv.Position;
                        }
                    }
                    else
                    {
                        foundPrim = Client.Network.CurrentSim.ObjectsPrimitives.Find(
                            delegate(Primitive prim)
                            {
                                return prim.ID == targetID;
                            }
                        );

                        if (foundPrim == null)
                            return String.Format(bot.Localization.clResourceManager.getText("Commands.Moveto.ObjNotInSim"), targetID);

                        newDirection = foundPrim.Position;
                    }
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.Moveto.Usage");
                }
            }
            else
            {
                newDirection.X = (float)x;
                newDirection.Y = (float)y;
                newDirection.Z = (float)z;
            }

            x = (double)newDirection.X;
            y = (double)newDirection.Y;
            z = (double)newDirection.Z;

            // Convert the local coordinates to global ones by adding the region handle parts to x and y
            x += (double)regionX;
            y += (double)regionY;

            Client.Self.AutoPilot(x, y, z);

            return String.Format(bot.Localization.clResourceManager.getText("Commands.Moveto.Moving"), (uint)(x - (double)regionX), (uint)(y - (double)regionY), (uint)z);
        }
    }
}

