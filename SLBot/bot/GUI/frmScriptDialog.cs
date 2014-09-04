/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmScriptDialog.cs
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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenMetaverse;

namespace bot.GUI
{
    public partial class frmScriptDialog : Form
    {
        private SecondLifeBot _client;
        private ScriptDialogEventArgs _eventArgs;
        private Point mouse_offset;

        public frmScriptDialog(SecondLifeBot client, ScriptDialogEventArgs eventArgs)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();

            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmScriptDialog");

            this._client = client;
            this._eventArgs = eventArgs;
        }

        private void frmScriptDialog_Load(object sender, EventArgs e)
        {
            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmScriptDialog.Text"), _eventArgs.ObjectName);
            lblObject.Text = String.Format(bot.Localization.clResourceManager.getText("frmScriptDialog.lblObject"), _eventArgs.ObjectName);
            txtObject.Text = _eventArgs.ObjectID.ToString();
            lblOwner.Text = String.Format(bot.Localization.clResourceManager.getText("frmScriptDialog.lblOwner"), _eventArgs.FirstName, _eventArgs.LastName);
            lblMessage.Text = _eventArgs.Message;
            PrepareButtons();
            this.Focus();
        }

        private void PrepareButtons()
        {
            for (int i = 1; i <= _eventArgs.ButtonLabels.Count; i++)
            {
                switch (i)
                {
                    case 1:
                        {
                            btn1.Text = _eventArgs.ButtonLabels[i - 1];
                            btn1.Visible = true;
                            break;
                        }
                    case 2:
                        {
                            btn2.Text = _eventArgs.ButtonLabels[i - 1];
                            btn2.Visible = true;
                            break;
                        }
                    case 3:
                        {
                            btn3.Text = _eventArgs.ButtonLabels[i - 1];
                            btn3.Visible = true;
                            break;
                        }
                    case 4:
                        {
                            btn4.Text = _eventArgs.ButtonLabels[i - 1];
                            btn4.Visible = true;
                            break;
                        }
                    case 5:
                        {
                            btn5.Text = _eventArgs.ButtonLabels[i - 1];
                            btn5.Visible = true;
                            break;
                        }
                    case 6:
                        {
                            btn6.Text = _eventArgs.ButtonLabels[i - 1];
                            btn6.Visible = true;
                            break;
                        }
                    case 7:
                        {
                            btn7.Text = _eventArgs.ButtonLabels[i - 1];
                            btn7.Visible = true;
                            break;
                        }
                    case 8:
                        {
                            btn8.Text = _eventArgs.ButtonLabels[i - 1];
                            btn8.Visible = true;
                            break;
                        }
                    case 9:
                        {
                            btn9.Text = _eventArgs.ButtonLabels[i - 1];
                            btn9.Visible = true;
                            break;
                        }
                    case 10:
                        {
                            btn10.Text = _eventArgs.ButtonLabels[i - 1];
                            btn10.Visible = true;
                            break;
                        }
                    case 11:
                        {
                            btn11.Text = _eventArgs.ButtonLabels[i - 1];
                            btn11.Visible = true;
                            break;
                        }
                    case 12:
                        {
                            btn12.Text = _eventArgs.ButtonLabels[i - 1];
                            btn12.Visible = true;
                            break;
                        }
                }
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn1.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn2.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn3.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn4.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn5.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn6.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn7.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn8.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn9.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn10_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn10.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn11_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn11.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        private void btn12_Click(object sender, EventArgs e)
        {
            this._client.Self.Chat(btn12.Text, _eventArgs.Channel, ChatType.Normal);
            this.Dispose();
        }

        void frmScriptDialog_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmScriptDialog_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
