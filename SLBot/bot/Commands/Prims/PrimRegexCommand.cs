/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : PrimRegexCommand.cs
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
    using System.Text.RegularExpressions;

    public class PrimRegexCommand : Command
    {
        public PrimRegexCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "primregex";
            base.Description = bot.Localization.clResourceManager.getText("Commands.PrimRegex.Description") + " " + bot.Localization.clResourceManager.getText("Commands.PrimRegex.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length < 1)
                return bot.Localization.clResourceManager.getText("Commands.PrimRegex.Usage");

            try
            {
                // Build the predicat from the args list
                string predicatPrim = string.Empty;
                for (int i = 0; i < args.Length; i++)
                    predicatPrim += args[i] + " ";
                predicatPrim = predicatPrim.TrimEnd();

                // Build Regex
                Regex regexPrimName = new Regex(predicatPrim.ToLower());

                // Print result
                bot.Console.WriteLine(string.Format(bot.Localization.clResourceManager.getText("Commands.PrimRegex.Searching"), predicatPrim,
                    Client.Network.CurrentSim.ObjectsPrimitives.Count));

                Client.Network.CurrentSim.ObjectsPrimitives.ForEach(
                    delegate(Primitive prim)
                    {
                        if (prim.Text != null && regexPrimName.IsMatch(prim.Text.ToLower()))
                        {
                            bot.Console.WriteLine(string.Format(bot.Localization.clResourceManager.getText("Commands.PrimRegex.Prim"), prim.Properties.Name,
                                prim.ID, prim.Flags.ToString(), prim.Text, prim.Properties.Description));
                        }
                        else if (prim.Properties.Name != null && regexPrimName.IsMatch(prim.Properties.Name.ToLower()))
                        {
                            bot.Console.WriteLine(string.Format(bot.Localization.clResourceManager.getText("Commands.PrimRegex.Prim"), prim.Properties.Name,
                                prim.ID, prim.Flags.ToString(), prim.Text, prim.Properties.Description));
                        }
                        else if (prim.Properties.Description != null && regexPrimName.IsMatch(prim.Properties.Description.ToLower()))
                        {
                            bot.Console.WriteLine(string.Format(bot.Localization.clResourceManager.getText("Commands.PrimRegex.Prim"), prim.Properties.Name,
                                prim.ID, prim.Flags.ToString(), prim.Text, prim.Properties.Description));
                        }
                    }
                );
            }
            catch (System.Exception e)
            {
                bot.Console.WriteLine(e.Message);
                return bot.Localization.clResourceManager.getText("Commands.PrimRegex.Error");
            }

            return bot.Localization.clResourceManager.getText("Commands.PrimRegex.Done");
        }
    }
}

