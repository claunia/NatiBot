/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : LanguageCodes.cs
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
using System;
using System.Collections.Generic;
using System.Text;

namespace bot
{
    public class LanguageCodes
    {
        private Dictionary<string, string> _LangCodes;

        public LanguageCodes()
        {
            GenerateLangCodes();
        }

        public Dictionary<string, string> LangCodes
        {
            get
            {
                if (_LangCodes == null)
                {
                    GenerateLangCodes();
                }

                return this._LangCodes;
            }
            set
            {
            }
        }

        private void GenerateLangCodes()
        {
            _LangCodes = new Dictionary<string, string>();
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ca"), "ca");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.cy"), "cy");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.da"), "da");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.de"), "de");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.el"), "el");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.en"), "en");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.es"), "es");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.et"), "et");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.fi"), "fi");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.fr"), "fr");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ga"), "ga");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.gl"), "gl");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.hi"), "hi");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.hr"), "hr");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.hu"), "hu");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.id"), "id");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.is"), "is");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.it"), "it");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.iw"), "iw");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ja"), "ja");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ko"), "ko");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.lt"), "lt");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.lv"), "lv");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.mk"), "mk");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ms"), "ms");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.mt"), "mt");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.nl"), "nl");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.no"), "no");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.pl"), "pl");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.pt"), "pt");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ro"), "ro");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.ru"), "ru");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.sk"), "sk");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.sl"), "sl");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.sr"), "sr");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.sv"), "sv");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.sw"), "sw");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.th"), "th");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.tl"), "tl");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.tr"), "tr");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.uk"), "uk");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.vi"), "vi");
            _LangCodes.Add(bot.Localization.clResourceManager.getText("LanguageCode.yi"), "yi");
        }
    }
}
