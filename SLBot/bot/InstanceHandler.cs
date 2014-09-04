/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : InstanceHandler.cs
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
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public sealed class InstanceHandler
    {
        private const int WS_SHOWNORMAL = 1;

        public static void HandleRunningInstance(Process instance)
        {
            if (instance != null)
            {
                ShowWindowAsync(instance.MainWindowHandle, 1);
                SetForegroundWindow(instance.MainWindowHandle);
            }
        }

        public static Process RunningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process2 in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if ((process2.Id != currentProcess.Id) && (Assembly.GetExecutingAssembly().Location.Replace("/", @"\") == process2.MainModule.FileName))
                {
                    return process2;
                }
            }
            return null;
        }

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
    }
}

