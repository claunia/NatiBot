/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmConsole.cs
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
namespace bot.GUI
{
    using bot;
    using bot.license;
    using bot.GUI;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using clControls;
    using System.IO;

    public partial class frmConsole : Form
    {
        public delegate void InputSendCallback(string message);

        public event InputSendCallback OnInputSend;

        public delegate void OutputSendCallback(string message);

        public event OutputSendCallback OnOutputSend;

        public delegate void RealOnConsoleWriteCallBack(string message);

        private Point mouse_offset;

        RealOnConsoleWriteCallBack RealOnConsoleWrite;

        public frmConsole()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();

            bot.Console.Initialize(this);
            bot.Console.OnConsoleWrite += new bot.Console.ConsoleWriteCallback(this.Program_OnConsoleWrite);
            this.Text = bot.Localization.clResourceManager.getText("frmMain.tabConsole");
            lblConsole.Text = bot.Localization.clResourceManager.getText("frmMain.tabConsole");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmConsole");
            RealOnConsoleWrite = new RealOnConsoleWriteCallBack(Real_OnConsoleWrite);
        }

        private void Program_OnConsoleWrite(string msg)
        {
            try
            {
                this.Invoke(RealOnConsoleWrite, msg);
            }
            catch
            {
            }
        }

        private void Real_OnConsoleWrite(string msg)
        {
            if (msg != null)
            {
                this.rtbConsole.AppendText(string.Format("{0}\n", msg));
                this.rtbConsole.Update();
                this.rtbConsole.ScrollToCaret();

                if (Program.getWriteConsoleToFileSetting())
                {
                    StreamWriter consoleFile;

                    if (File.Exists("./console.log"))
                        consoleFile = File.AppendText("./console.log");
                    else
                        consoleFile = File.CreateText("./console.log");

                    consoleFile.WriteLine("(" + DateTime.Now.ToString() + "): " + msg);
                    consoleFile.Close();
                }
            }
        }

        private void edtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (this.OnInputSend != null)
                {
                    this.OnInputSend(string.Format("Console> {0}", this.edtInput.Text));
                }
                this.OnOutputSend(this.edtInput.Text);
                this.edtInput.Text = string.Empty;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmConsole_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmConsole_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }
    }
}
