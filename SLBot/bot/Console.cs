/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Console.cs
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
namespace bot
{
    using bot.GUI;
    using System;
    using System.Collections.Generic;


    public static class Console
    {
        public delegate void ConsoleWriteCallback(string message);

        public static event ConsoleWriteCallback OnConsoleWrite;

        private static void BotForm_OnInputSend(string msg)
        {
            WriteLine(msg);
        }

        public static void Initialize(frmConsole mainForm)
        {
            mainForm.OnInputSend += new frmConsole.InputSendCallback(bot.Console.BotForm_OnInputSend);
        }

        public static void WriteLine(string msg)
        {
            if (OnConsoleWrite != null)
            {
                OnConsoleWrite(msg);
            }
        }

        public static void WriteLine(SecondLifeBot client, string msg)
        {
            WriteLine(client.LoginDetails.FullName + "> " + msg);
        }

        public static void WriteLine(string format, object arg0)
        {
            WriteLine(string.Format(format, arg0));
        }

        public static void WriteLine(string format, params object[] arg)
        {
            WriteLine(string.Format(format, arg));
        }

        public static void WriteLine(SecondLifeBot client, string format, object arg0)
        {
            WriteLine(client, string.Format(format, arg0));
        }

        public static void WriteLine(SecondLifeBot client, string format, params object[] arg)
        {
            WriteLine(client, string.Format(format, arg));
        }
    }
}

