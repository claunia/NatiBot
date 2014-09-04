/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ExportParticlesCommand.cs
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
    using System.Text;
    using System.Collections.Generic;
    using System.IO;
    using System.Globalization;

    public class ExportParticlesCommand : Command
    {
        public ExportParticlesCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "exportparticles";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ExportParticles.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ExportParticles.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Predicate<Primitive> match = null;
            UUID id;
            bool FoundParticleSystem = false;

            if (args.Length != 1)
            {
                return bot.Localization.clResourceManager.getText("Commands.ExportParticles.Usage");
            }
            if (!UUID.TryParse(args[0], out id))
            {
                return bot.Localization.clResourceManager.getText("Commands.ExportParticles.Usage");
            }

            lock (base.Client.Network.Simulators)
            {
                for (int i = 0; i < base.Client.Network.Simulators.Count; i++)
                {
                    Primitive primitive2 = base.Client.Network.CurrentSim.ObjectsPrimitives.Find(delegate(Primitive prim)
                    {
                        return prim.ID == id;
                    });

                    if (primitive2 != null)
                    {
                        uint localid;
                        if (primitive2.ParentID != 0)
                        {
                            localid = primitive2.ParentID;
                        }
                        else
                        {
                            localid = primitive2.LocalID;
                        }

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
                        List<Primitive> objects = base.Client.Network.Simulators[i].ObjectsPrimitives.FindAll(match);

                        if (objects != null)
                        {
                            for (int j = 0; j < objects.Count; j++)
                            {
                                Primitive exportPrim = objects[j];

                                if (exportPrim.ParticleSys.CRC != 0)
                                {
                                    Program.NBStats.AddStatData(String.Format("{0}: {1} exporting particles of object {2} on sim {3}.", DateTime.Now.ToString(), Client, id.ToString(), Client.Network.CurrentSim.Name));

                                    StringBuilder lsl = new StringBuilder();

                                    #region Particle System to LSL

                                    lsl.Append("default" + Environment.NewLine);
                                    lsl.Append("{" + Environment.NewLine);
                                    lsl.Append("    state_entry()" + Environment.NewLine);
                                    lsl.Append("    {" + Environment.NewLine);
                                    lsl.Append("         llParticleSystem([" + Environment.NewLine);

                                    lsl.Append("         PSYS_PART_FLAGS, 0");

                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.InterpColor) != 0)
                                        lsl.Append(" | PSYS_PART_INTERP_COLOR_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.InterpScale) != 0)
                                        lsl.Append(" | PSYS_PART_INTERP_SCALE_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.Bounce) != 0)
                                        lsl.Append(" | PSYS_PART_BOUNCE_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.Wind) != 0)
                                        lsl.Append(" | PSYS_PART_WIND_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.FollowSrc) != 0)
                                        lsl.Append(" | PSYS_PART_FOLLOW_SRC_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.FollowVelocity) != 0)
                                        lsl.Append(" | PSYS_PART_FOLLOW_VELOCITY_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.TargetPos) != 0)
                                        lsl.Append(" | PSYS_PART_TARGET_POS_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.TargetLinear) != 0)
                                        lsl.Append(" | PSYS_PART_TARGET_LINEAR_MASK");
                                    if ((exportPrim.ParticleSys.PartDataFlags & Primitive.ParticleSystem.ParticleDataFlags.Emissive) != 0)
                                        lsl.Append(" | PSYS_PART_EMISSIVE_MASK");

                                    lsl.Append(",");
                                    lsl.Append(Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_PATTERN, 0");

                                    if ((exportPrim.ParticleSys.Pattern & Primitive.ParticleSystem.SourcePattern.Drop) != 0)
                                        lsl.Append(" | PSYS_SRC_PATTERN_DROP");
                                    if ((exportPrim.ParticleSys.Pattern & Primitive.ParticleSystem.SourcePattern.Explode) != 0)
                                        lsl.Append(" | PSYS_SRC_PATTERN_EXPLODE");
                                    if ((exportPrim.ParticleSys.Pattern & Primitive.ParticleSystem.SourcePattern.Angle) != 0)
                                        lsl.Append(" | PSYS_SRC_PATTERN_ANGLE");
                                    if ((exportPrim.ParticleSys.Pattern & Primitive.ParticleSystem.SourcePattern.AngleCone) != 0)
                                        lsl.Append(" | PSYS_SRC_PATTERN_ANGLE_CONE");
                                    if ((exportPrim.ParticleSys.Pattern & Primitive.ParticleSystem.SourcePattern.AngleConeEmpty) != 0)
                                        lsl.Append(" | PSYS_SRC_PATTERN_ANGLE_CONE_EMPTY");

                                    lsl.Append("," + Environment.NewLine);

                                    lsl.Append("         PSYS_PART_START_ALPHA, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartStartColor.A) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_END_ALPHA, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartEndColor.A) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_START_COLOR, " + exportPrim.ParticleSys.PartStartColor.ToRGBString() + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_END_COLOR, " + exportPrim.ParticleSys.PartEndColor.ToRGBString() + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_START_SCALE, <" + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartStartScaleX) + ", " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartStartScaleY) + ", 0>, " + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_END_SCALE, <" + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartEndScaleX) + ", " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartEndScaleY) + ", 0>, " + Environment.NewLine);
                                    lsl.Append("         PSYS_PART_MAX_AGE, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.PartMaxAge) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_MAX_AGE, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.MaxAge) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_ACCEL, " + exportPrim.ParticleSys.PartAcceleration.ToString() + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_BURST_PART_COUNT, " + String.Format(new CultureInfo("en-US"), "{0:0}", exportPrim.ParticleSys.BurstPartCount) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_BURST_RADIUS, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.BurstRadius) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_BURST_RATE, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.BurstRate) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_BURST_SPEED_MIN, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.BurstSpeedMin) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_BURST_SPEED_MAX, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.BurstSpeedMax) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_INNERANGLE, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.InnerAngle) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_OUTERANGLE, " + String.Format(new CultureInfo("en-US"), "{0:0.00000}", exportPrim.ParticleSys.OuterAngle) + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_OMEGA, " + exportPrim.ParticleSys.AngularVelocity.ToString() + "," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_TEXTURE, (key)\"" + exportPrim.ParticleSys.Texture.ToString() + "\"," + Environment.NewLine);
                                    lsl.Append("         PSYS_SRC_TARGET_KEY, (key)\"" + exportPrim.ParticleSys.Target.ToString() + "\"" + Environment.NewLine);

                                    lsl.Append("         ]);" + Environment.NewLine);
                                    lsl.Append("    }" + Environment.NewLine);
                                    lsl.Append("}" + Environment.NewLine);

                                    #endregion Particle System to LSL

                                    if (!Directory.Exists("./particles"))
                                        Directory.CreateDirectory("./particles");

                                    File.WriteAllText("./particles/" + CreateFileName(id.ToString(), primitive2.Position.ToString(), primitive2.Text, exportPrim.LocalID), lsl.ToString());

                                    //File.WriteAllText("./particles/" + id.ToString() + "_" + exportPrim.LocalID.ToString() + ".lsl", lsl.ToString());

                                    //return builder.ToString();
                                    FoundParticleSystem = true;
                                }
                            }
                            if (FoundParticleSystem)
                            {
                                return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportParticles.Exported"), id.ToString());
                            }
                            else
                            {
                                return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportParticles.NoParticles"), id.ToString());
                            }

                        }
                    }
                    else
                    {
                        return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportParticles.ObjectNotFound"), id.ToString());
                    }
                }
            }
            return String.Format(bot.Localization.clResourceManager.getText("Commands.ExportParticles.PrimNotFound"), id.ToString());
        }

        private string CreateFileName(string UUID, string Location, string PrimName, uint localID)
        {
            string corrected_PrimName;
            string corrected_Location;
            string FinalName;

            if (PrimName == "")
                PrimName = "Object";

            corrected_PrimName = PrimName.Replace(" ", "_");
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

            corrected_Location = Location.Replace(">", "}");
            corrected_Location = corrected_Location.Replace("<", "{");

            FinalName = corrected_PrimName + " (" + UUID + ", " + corrected_Location + ", " + localID.ToString() + ").lsl";

            return FinalName;
        }
    }
}

