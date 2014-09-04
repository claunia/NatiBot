/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Parsing.cs
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
Portions copyright (c) 2006-2010, openmetaverse.org
****************************************************************************/
namespace bot
{
    using System;
    using System.Collections.Generic;

    internal class Parsing
    {
        public static string[] ParseArguments(string str)
        {
            List<string> list = new List<string>();
            string str2 = string.Empty;
            string item = null;
            bool flag = false;
            bool flag2 = false;
            foreach (char ch in str)
            {
                switch (ch)
                {
                    case '"':
                        if (flag2)
                        {
                            str2 = str2 + '"';
                            flag2 = false;
                        }
                        else
                        {
                            str2 = str2 + '"';
                            flag = !flag;
                        }
                        break;

                    case ' ':
                    case '\t':
                        if (flag2 || flag)
                        {
                            str2 = str2 + ch;
                            flag2 = false;
                        }
                        else
                        {
                            item = str2.Trim();
                            if (item.StartsWith("\"") && item.EndsWith("\""))
                            {
                                item = item.Remove(0, 1);
                                item = item.Remove(item.Length - 1).Trim();
                            }
                            if (item.Length > 0)
                            {
                                list.Add(item);
                            }
                            str2 = string.Empty;
                        }
                        break;

                    default:
                        if (ch == '\\')
                        {
                            if (flag2)
                            {
                                str2 = str2 + '\\';
                                flag2 = false;
                            }
                            else
                            {
                                flag2 = true;
                            }
                        }
                        else
                        {
                            if (flag2)
                            {
                                throw new FormatException(String.Format(bot.Localization.clResourceManager.getText("Parsing.NotEscapable"), ch.ToString()));
                            }
                            str2 = str2 + ch;
                        }
                        break;
                }
            }
            item = str2.Trim();
            if (item.StartsWith("\"") && item.EndsWith("\""))
            {
                item = item.Remove(0, 1);
                item = item.Remove(item.Length - 1).Trim();
            }
            if (item.Length > 0)
            {
                list.Add(item);
            }
            return list.ToArray();
        }

        public static Dictionary<string, string> ParseWebResponse(string resp)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            string[] strArray = resp.Split(new char[] { '&' });
            for (int i = 0; i < strArray.Length; i++)
            {
                string[] strArray2 = strArray[i].Split(new char[] { '=' });
                if (strArray2.Length == 2)
                {
                    dictionary[strArray2[0]] = strArray2[1];
                }
                else if (strArray2.Length == 1)
                {
                    dictionary[strArray2[0]] = string.Empty;
                }
            }
            return dictionary;
        }
    }
}

