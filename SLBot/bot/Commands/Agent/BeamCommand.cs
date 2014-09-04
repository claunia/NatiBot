/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BeamCommand.cs
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
    using System;
    using OpenMetaverse;
    using System.Timers;

    public class BeamCommand : Command
    {
        private Timer b = new Timer();
        private bool doBeam;

        public BeamCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "beam";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Beam.Description") + " " + bot.Localization.clResourceManager.getText("Commands.Beam.Usage");

            b.Elapsed += new ElapsedEventHandler(b_Elapsed);
        }

        void b_Elapsed(object sender, ElapsedEventArgs e)
        {
            doBeam = false;
            b.Enabled = false;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            UUID targetID = UUID.Zero;
            string avatarName = "";
            bool isGroupKey = false;
            Avatar foundAv = null;
            Primitive foundPrim;
            Vector3d targetPosition;
            doBeam = true;
            b.Interval = 5000;

            if (args.Length == 1)
            {
                if (!UUID.TryParse(args[0], out targetID))
                    return bot.Localization.clResourceManager.getText("Commands.Beam.Usage");
            }
            else if (args.Length == 2)
            {
                avatarName = args[0] + " " + args[1];
                if (!Client.FindOneAvatar(avatarName, out targetID))
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Beam.AvNotFound"), avatarName);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Beam.Usage");
            }

            if (targetID != UUID.Zero)
            {
                Client.key2Name(targetID, out avatarName, out isGroupKey);

                if (isGroupKey)
                    return bot.Localization.clResourceManager.getText("Commands.Beam.CannotGroup");

                if (avatarName != "")
                {
                    foundAv = Client.Network.CurrentSim.ObjectsAvatars.Find(
                        delegate(Avatar avatar)
                        {
                            return (avatar.Name == avatarName);
                        }
                    );

                    if (foundAv == null)
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Beam.AvNotInSim"), avatarName);

                    targetPosition = new Vector3d(foundAv.Position);
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
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.Beam.ObjNotInSim"), targetID);

                    targetPosition = new Vector3d(foundPrim.Position);
                }

                b.Enabled = true;
                while (doBeam)
                {
                    Client.Self.BeamEffect(Client.Self.AgentID, targetID, targetPosition, new Color4(0, 0, 0, 255), 5000.0f,
                        new UUID("aec29773-c421-364e-4f29-e3f633222188"));
                }

                if (avatarName == "")
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Beam.BeamObj"), targetID);
                else
                    return String.Format(bot.Localization.clResourceManager.getText("Commands.Beam.BeamAv"), avatarName);
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.Beam.Usage");
            }
        }
    }
}
