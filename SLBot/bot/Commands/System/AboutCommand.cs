/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AboutCommand.cs
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

    public class AboutCommand : Command
    {
        public AboutCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "about";
            base.Description = bot.Localization.clResourceManager.getText("Commands.About.Description");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            bot.license.Version botVersion = new bot.license.Version();
            StringBuilder sbAbout = new StringBuilder();

            sbAbout.AppendLine();
            sbAbout.Append("NatiBot " + botVersion.ToString() + " " + botVersion.v_rev);
            sbAbout.AppendLine();
            sbAbout.Append("© 2009-2010 Claunia.com " + bot.Localization.clResourceManager.getText("frmAbout.Copyright"));
            sbAbout.AppendLine();
            sbAbout.AppendLine();
            sbAbout.Append(bot.Localization.clResourceManager.getText("frmAbout.Coding"));
            sbAbout.Append("Natalia Portillo");
            sbAbout.AppendLine();
            sbAbout.Append(bot.Localization.clResourceManager.getText("frmAbout.Interface"));
            sbAbout.Append("Ana Sánchez");
            sbAbout.AppendLine();
            sbAbout.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.English"),
                "Natalia Portillo");
            sbAbout.AppendLine();
            sbAbout.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.Spanish"),
                "Natalia Portillo");
            sbAbout.AppendLine();
            sbAbout.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.Catalan"),
                "Alejandro Sánchez");
            sbAbout.AppendLine();
            sbAbout.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.French"),
                "Natalia Portillo & Google");
            sbAbout.AppendLine();

            return sbAbout.ToString();
        }
    }
}

