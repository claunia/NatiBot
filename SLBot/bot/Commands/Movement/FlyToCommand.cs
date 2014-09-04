/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FlyToCommand.cs
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
    using System;
    using System.Collections.Generic;
    using OpenMetaverse;
    using OpenMetaverse.Packets;
    using bot;

    class FlyToCommand : Command
    {
        Vector3 myPos = new Vector3();
        Vector2 myPos0 = new Vector2();
        Vector3 target = new Vector3();
        Vector2 target0 = new Vector2();
        float diff, olddiff, saveolddiff;
        int startTime = 0;
        int duration = 10000;

        public FlyToCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "flyto";
            base.Description = bot.Localization.clResourceManager.getText("Commands.FlyTo.Description") + " " + bot.Localization.clResourceManager.getText("Commands.FlyTo.Usage");

            SecondLifeBot.Objects.TerseObjectUpdate += Objects_OnObjectUpdated;
        }

        public override string Execute(string[] args, UUID fromAgentID, bool something_else)
        {
            if (args.Length > 4 || args.Length < 3)
                return bot.Localization.clResourceManager.getText("Commands.FlyTo.Usage");

            if (!float.TryParse(args[0], out target.X) ||
                !float.TryParse(args[1], out target.Y) ||
                !float.TryParse(args[2], out target.Z))
            {
                return bot.Localization.clResourceManager.getText("Commands.FlyTo.Usage");
            }
            target0.X = target.X;
            target0.Y = target.Y;

            if (args.Length == 4 && Int32.TryParse(args[3], out duration))
                duration *= 1000;

            startTime = Environment.TickCount;
            Client.Self.Movement.Fly = true;
            Client.Self.Movement.AtPos = true;
            Client.Self.Movement.AtNeg = false;
            ZMovement();
            Client.Self.Movement.TurnToward(target);

            return string.Format(bot.Localization.clResourceManager.getText("Commands.FlyTo.Flying"), target.ToString(), duration / 1000);
        }

        private void Objects_OnObjectUpdated(object sender, TerseObjectUpdateEventArgs e)
        {
            if (startTime == 0)
                return;
            if (e.Update.LocalID == Client.Self.LocalID)
            {
                XYMovement();
                ZMovement();
                if (Client.Self.Movement.AtPos || Client.Self.Movement.AtNeg)
                {
                    Client.Self.Movement.TurnToward(target);
                    Debug("Flyxy ");
                }
                else if (Client.Self.Movement.UpPos || Client.Self.Movement.UpNeg)
                {
                    Client.Self.Movement.TurnToward(target);
                    //Client.Self.Movement.SendUpdate(false);
                    Debug("Fly z ");
                }
                else if (Vector3.Distance(target, Client.Self.SimPosition) <= 2.0)
                {
                    EndFlyto();
                    Debug("At Target");
                }
            }
            if (Environment.TickCount - startTime > duration)
            {
                EndFlyto();
                Debug("End Flyto");
            }
        }

        private bool XYMovement()
        {
            bool res = false;

            myPos = Client.Self.SimPosition;
            myPos0.X = myPos.X;
            myPos0.Y = myPos.Y;
            diff = Vector2.Distance(target0, myPos0);
            Vector2 vvel = new Vector2(Client.Self.Velocity.X, Client.Self.Velocity.Y);
            float vel = vvel.Length();
            if (diff >= 10.0)
            {
                Client.Self.Movement.AtPos = true;
                res = true;
            }
            else if (diff >= 2 && vel < 5)
            {
                Client.Self.Movement.AtPos = true;
            }
            else
            {
                Client.Self.Movement.AtPos = false;
                Client.Self.Movement.AtNeg = false;
            }
            saveolddiff = olddiff;
            olddiff = diff;
            return res;
        }

        private void ZMovement()
        {
            Client.Self.Movement.UpPos = false;
            Client.Self.Movement.UpNeg = false;
            float diffz = (target.Z - Client.Self.SimPosition.Z);
            if (diffz >= 20.0)
                Client.Self.Movement.UpPos = true;
            else if (diffz <= -20.0)
                Client.Self.Movement.UpNeg = true;
            else if (diffz >= +5.0 && Client.Self.Velocity.Z < +4.0)
                Client.Self.Movement.UpPos = true;
            else if (diffz <= -5.0 && Client.Self.Velocity.Z > -4.0)
                Client.Self.Movement.UpNeg = true;
            else if (diffz >= +2.0 && Client.Self.Velocity.Z < +1.0)
                Client.Self.Movement.UpPos = true;
            else if (diffz <= -2.0 && Client.Self.Velocity.Z > -1.0)
                Client.Self.Movement.UpNeg = true;
        }

        private void EndFlyto()
        {
            startTime = 0;
            Client.Self.Movement.AtPos = false;
            Client.Self.Movement.AtNeg = false;
            Client.Self.Movement.UpPos = false;
            Client.Self.Movement.UpNeg = false;
            Client.Self.Movement.SendUpdate(false);
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void Debug(string x)
        {
            bot.Console.WriteLine(x + " {0,3:##0} {1,3:##0} {2,3:##0} diff {3,5:##0.0} olddiff {4,5:##0.0}  At:{5,5} {6,5}  Up:{7,5} {8,5}  v: {9} w: {10}",
                myPos.X, myPos.Y, myPos.Z, diff, saveolddiff,
                Client.Self.Movement.AtPos, Client.Self.Movement.AtNeg, Client.Self.Movement.UpPos, Client.Self.Movement.UpNeg,
                Client.Self.Velocity.ToString(), Client.Self.AngularVelocity.ToString());
        }
    }
}
