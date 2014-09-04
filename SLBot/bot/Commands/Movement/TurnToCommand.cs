/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : TurnToCommand.cs
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

    public class TurnToCommand : Command
    {
        public TurnToCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "turnto";
            base.Description = bot.Localization.clResourceManager.getText("Commands.TurnTo.Description") + " " + bot.Localization.clResourceManager.getText("Commands.TurnTo.Usage");
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

            if (args.Length == 1)
            {
                if (!UUID.TryParse(args[0], out targetID))
                    return bot.Localization.clResourceManager.getText("Commands.TurnTo.Usage");
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
                    return bot.Localization.clResourceManager.getText("Commands.TurnTo.Usage");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.TurnTo.Usage");
            }

            if (args.Length != 3)
            {
                if (targetID != UUID.Zero)
                {
                    Client.key2Name(targetID, out avatarName, out isGroupKey);

                    if (isGroupKey)
                        return bot.Localization.clResourceManager.getText("Commands.TurnTo.CannotGroup");

                    if (avatarName != "")
                    {
                        foundAv = Client.Network.CurrentSim.ObjectsAvatars.Find(
                            delegate(Avatar avatar)
                            {
                                return (avatar.Name == avatarName);
                            }
                        );

                        if (foundAv == null)
                            return String.Format(bot.Localization.clResourceManager.getText("Commands.TurnTo.AvNotInSim"), avatarName);

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
                                return String.Format(bot.Localization.clResourceManager.getText("Commands.TurnTo.AvSit"), avatarName);
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
                            return String.Format(bot.Localization.clResourceManager.getText("Commands.TurnTo.ObjNotInSim"), targetID);

                        newDirection = foundPrim.Position;
                    }
                }
                else
                {
                    return bot.Localization.clResourceManager.getText("Commands.TurnTo.Usage");
                }
            }
            else
            {
                newDirection.X = (float)x;
                newDirection.Y = (float)y;
                newDirection.Z = (float)z;
            }

            Client.Self.Movement.TurnToward(newDirection);
            Client.Self.Movement.SendUpdate(false);
            return String.Format(bot.Localization.clResourceManager.getText("Commands.TurnTo.Turned"), newDirection.ToString());
        }
    }
}
