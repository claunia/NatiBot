/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : clResourceManager.cs
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
using System.Xml;
using System.Globalization;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Drawing;
using Microsoft.Win32;
using Claunia.clUtils;

namespace bot.Localization
{
    public static class clResourceManager
    {
        private const String RESOURCES_PREFIX = "language_";
        private const String DEFAULT_LOCALE = "en";
        private const String ELEMENT_NAME = "resource";
        private const String ATTRIBUTE_TYPE = "type";
        private const String ATTRIBUTE_ID = "id";

        private static XmlDocument resourceFile;

        public enum ResourceType
        {
            Text
        }

        private static string[] availableLocales =
        {
            "ca",
            "en",
            "es",
            "fr"
        };

        private static string[] availableSkins =
        {
            "blueish",
            "redish"
        };

        public static String getText(String key, String def)
        {
            String text = getText(key);

            if (text == "")
                text = def;

            return text;

        }

        public static List<String> getAvailableSkins()
        {
            List<String> AvailableSkins = new List<string>();

            for (int i = 0; i < availableSkins.Length; i++)
            {
                AvailableSkins.Add(availableSkins[i]);
            }

            return AvailableSkins;
        }

        public static String[] getAvailableLanguages()
        {
            String[] availableLanguages = new String[availableLocales.Length];

            for (int i = 0; i < availableLocales.Length; i++)
            {
                availableLanguages[i] = getText("LanguageCode." + availableLocales[i]);
            }

            return availableLanguages;
        }

        public static string getCurrentLanguage()
        {
            return getText("LanguageCode." + getLanguageCode());
        }

        public static void setCurrentLanguage(string language)
        {
            string languageCode = null;

            for (int i = 0; i < availableLocales.Length; i++)
            {
                if (getText("LanguageCode." + availableLocales[i]) == language)
                {
                    languageCode = availableLocales[i];
                    break;
                }
            }

            if (languageCode == null)
                languageCode = DEFAULT_LOCALE;

            setLanguageCode(languageCode);

            return;
        }

        public static void setLanguageCode(string languageCode)
        {
            bool languageIsAvailable = false;

            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            for (int i = 0; i < availableLocales.Length; i++)
            {
                if (availableLocales[i] == languageCode)
                    languageIsAvailable = true;
            }

            if (!languageIsAvailable)
                languageCode = DEFAULT_LOCALE;

            nbRegistry.SetValue("Language", languageCode);

            return;
        }

        public static void setSkin(string skinName)
        {
            List<String> _skins = getAvailableSkins();

            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            if (!_skins.Contains(skinName))
                return;

            nbRegistry.SetValue("Skin", skinName);

            return;
        }

        public static String getText(String key)
        {
            Hashtable textTable = getResourceValuesTable(ResourceType.Text);

            String text = "";
            try
            {
                text = (String)textTable[key];
            }
            catch (InvalidCastException ex)
            {
                text = "";
            }

            if (text == null)
            {
                text = "";
            }

            return text;
        }

        private static Hashtable getResourceValuesTable(ResourceType type)
        {
            // retrieve data from xml file
            XmlDocument xmlDocument = loadXMLFile();
            XmlNodeList values = xmlDocument.GetElementsByTagName("resource");

            String resourceType = "";
            if (type == ResourceType.Text)
            {
                resourceType = "Text";
            }
            Hashtable valueTable = new Hashtable();
            foreach (XmlElement currentElement in values)
            {
                if (currentElement.GetAttribute(ATTRIBUTE_TYPE).Equals(resourceType))
                {
                    {
                        if (!valueTable.ContainsKey(currentElement.GetAttribute(ATTRIBUTE_ID)))
                            valueTable.Add(currentElement.GetAttribute(ATTRIBUTE_ID), currentElement.InnerText);
                    }
                }
            }

            return valueTable;
        }

        private static XmlDocument loadXMLFile()
        {
            XmlDocument xmlDocument = new XmlDocument();

            if (resourceFile != null)
            {
                xmlDocument = resourceFile;
            }
            else
            {
                String locale = getLanguageCode();

                String pathToXMLFile = "OpenMetaverse.bot.Localization." + RESOURCES_PREFIX + locale + ".xml";
                Stream xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToXMLFile);

                if (xmlStream == null)
                {
                    pathToXMLFile = "OpenMetaverse.bot.Localization." + RESOURCES_PREFIX + DEFAULT_LOCALE + ".xml";
                    xmlStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToXMLFile);
                }

                xmlDocument.Load(new XmlTextReader(xmlStream));

                resourceFile = xmlDocument;
            }

            return xmlDocument;
        }

        public static string getCurrentSkin()
        {
            List<String> _avSkins = getAvailableSkins();
            bool skinIsAvailable = false;

            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            object rkSkin = nbRegistry.GetValue("Skin");

            if (rkSkin == null)
                skinIsAvailable = false;
            else
            {
                if (_avSkins.Contains((string)rkSkin))
                    skinIsAvailable = true;
                else
                    skinIsAvailable = false;
            }

            if (skinIsAvailable)
                return (string)rkSkin;
            else
            {
                string _skin = null;
                if (Utilities.GetRunningRuntime() == Utilities.Runtime.Mono)
                    _skin = "redish";
                else
                    _skin = "blueish";

                nbRegistry.SetValue("Skin", _skin);

                return _skin;
            }
        }

        public static String getLanguageCode()
        {
            string languageCode;
            bool languageIsAvailable = false;

            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            object rkLanguage = nbRegistry.GetValue("Language");

            if (rkLanguage == null)
                languageCode = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            else
                languageCode = (string)rkLanguage;

            for (int i = 0; i < availableLocales.Length; i++)
            {
                if (availableLocales[i] == languageCode)
                    languageIsAvailable = true;
            }

            if (!languageIsAvailable)
                languageCode = DEFAULT_LOCALE;

            if (rkLanguage == null)
                nbRegistry.SetValue("Language", languageCode);

            return languageCode;
        }

        public static System.Drawing.Image getButton(string button)
        {
            String locale = getLanguageCode();

            String pathToButtonImage = "OpenMetaverse.bot.GUI.skins." + getCurrentSkin() + ".buttons." + locale + "." + button + ".png";
            Stream buttonStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToButtonImage);

            if (buttonStream == null)
            {
                pathToButtonImage = "OpenMetaverse.bot.GUI.skins." + getCurrentSkin() + ".buttons." + DEFAULT_LOCALE + "." + button + ".png";
                buttonStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToButtonImage);
            }

            Image buttonImage = Image.FromStream(buttonStream);

            return buttonImage;
        }

        public static System.Drawing.Image getNoLanguageButton(string button)
        {
            String pathToButtonImage = "OpenMetaverse.bot.GUI.skins." + getCurrentSkin() + ".buttons." + button + ".png";
            Stream buttonStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToButtonImage);

            Image buttonImage = Image.FromStream(buttonStream);

            return buttonImage;
        }

        public static System.Drawing.Image getWindow(string window)
        {
            String pathToWindowImage = "OpenMetaverse.bot.GUI.skins." + getCurrentSkin() + ".windows." + window + ".png";
            Stream windowStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToWindowImage);

            Image windowImage = Image.FromStream(windowStream);

            return windowImage;
        }

        public static System.Drawing.Icon getIcon()
        {
            String pathToIcon;

            if (Utilities.GetRunningRuntime() == Utilities.Runtime.Microsoft)
            {
                pathToIcon = "OpenMetaverse.natibot.ico";
            }
            else
            {
                pathToIcon = "OpenMetaverse.natibot-mono.ico";
            }

            Stream iconStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToIcon);
            System.Drawing.Icon icon = new Icon(iconStream);

            return icon;
        }
    }
}
