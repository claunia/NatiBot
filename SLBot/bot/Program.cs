/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Program.cs
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
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using Microsoft.Win32;
    using OpenMetaverse;
    using Claunia.clUtils;

    internal static class Program
    {
        private static bool writeConsoleToFile;
        private static bool writeChatToFile;

        public static Statistics NBStats = new Statistics();

        private static void Application_Idle(object sender, EventArgs e)
        {
            //NBStats.SendStatistics();
        }

        [STAThread]
        private static void Main()
        {
#if !DEBUG
            if (Utilities.GetRunningRuntime() == Utilities.Runtime.Microsoft)
            {
                Mutex mutex = null;
                bool createdNew = false;
                try
                {
                    mutex = new Mutex(true, "NatiBot OneInstance Mutex", out createdNew);
                    Process instance = InstanceHandler.RunningInstance();
                    if ((instance != null) || !createdNew)
                    {
                        InstanceHandler.HandleRunningInstance(instance);
                    }
                    else
                    {
                        run();
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("{0}{1}{1}{2}", exception.GetType().FullName, Environment.NewLine, exception.ToString()), string.Format("{0} ({1})", exception.Source, exception.GetType().Name));
                }
                finally
                {
                    if (mutex != null)
                    {
                        if (createdNew)
                        {
                            mutex.ReleaseMutex();
                        }
                        mutex = null;
                    }
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
            else
            {
#endif
            run();
#if !DEBUG
            }
#endif
        }

        private static frmCheckLicense CheckLicense;
        private static frmMain MainForm;

        private static void run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            string myDocumentsPath;

            if (Utilities.GetRunningPlatform() == Utilities.Platform.MacOSX)
                myDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Documents";
            else
                myDocumentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (Utilities.IsUpdateAvailable(bot.license.Version.AppUUID, bot.license.Version.AppVersion))
            {
                Utilities.GetUpdate(bot.license.Version.AppUUID);
                Application.Run();
            }
            else
            {

                if (!Directory.Exists(myDocumentsPath + "/" + "NatiBot"))
                    Directory.CreateDirectory(myDocumentsPath + "/" + "NatiBot");

                System.Environment.CurrentDirectory = myDocumentsPath + "/" + "NatiBot";

                if (!Directory.Exists("./config/"))
                {
                    Directory.CreateDirectory("./config/");
                }

                Application.Idle += new EventHandler(Program.Application_Idle);

                CheckLicense = new frmCheckLicense();

                if (Utilities.GetRunningRuntime() == Utilities.Runtime.Mono)
                {
                    MainForm = new frmMain();
                    CheckLicense.ShowDialog();
                }
                else
                {
                    CheckLicense.Show();
                }

#if DEBUG
                Application.Run();
#else
            try
            {
                Application.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unhandled exception: " + ex.ToString(), "Unhandled exception.");
            }
#endif
                //Application.Run(CheckLicense);
            }
        }

        public static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            NBStats.SendException(ex);
            MessageBox.Show(bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line1") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line2") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line3") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line4"),
                bot.Localization.clResourceManager.getText("Program.Stats.Crash"), MessageBoxButtons.OK); 
            System.Environment.Exit(255);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            NBStats.SendException(e.Exception);
            MessageBox.Show(bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line1") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line2") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line3") + System.Environment.NewLine +
            bot.Localization.clResourceManager.getText("Program.Stats.Crash.Line4"),
                bot.Localization.clResourceManager.getText("Program.Stats.Crash"), MessageBoxButtons.OK);
            System.Environment.Exit(255);
        }

        public static void Authenticated(bool status)
        {
            if (status && MainForm != null)
            {
                if (Utilities.GetRunningRuntime() == Utilities.Runtime.Mono)
                    MainForm.Show();
            }
        }

        public static bool getWriteConsoleToFileSetting()
        {
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            object rkDumpConsole = nbRegistry.GetValue("DumpConsole");

            if (rkDumpConsole == null)
            {
                nbRegistry.SetValue("DumpConsole", true);
                writeConsoleToFile = true;
            }
            else
            {
                writeConsoleToFile = bool.Parse((string)rkDumpConsole);
            }

            return writeConsoleToFile;
        }

        public static void setWriteConsoleToFileSetting(bool value)
        {
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            nbRegistry.SetValue("DumpConsole", value);
            writeConsoleToFile = value;
            return;
        }

        public static bool getWriteChatToFileSetting()
        {
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            object rkDumpChat = nbRegistry.GetValue("DumpChat");

            if (rkDumpChat == null)
            {
                nbRegistry.SetValue("DumpChat", true);
                writeChatToFile = true;
            }
            else
            {
                writeChatToFile = bool.Parse((string)rkDumpChat);
            }

            return writeChatToFile;
        }

        public static void setWriteChatToFileSetting(bool value)
        {
            RegistryKey hkcu = Registry.CurrentUser;
            RegistryKey nbRegistry = hkcu.CreateSubKey("Software\\Claunia.com\\NatiBot");

            nbRegistry.SetValue("DumpChat", value);
            writeChatToFile = value;
            return;
        }

    }
}

