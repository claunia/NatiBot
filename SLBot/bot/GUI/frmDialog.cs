/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmDialog.cs
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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace bot.GUI
{
    public partial class frmDialog : Form
    {
        public string _output;
        private Point mouse_offset;

        public frmDialog(string Title, string Description, string Output)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();
            this.Text = Title;
            lblDescription.Text = Description;
            txtOutput.Text = Output;
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmDialog");

            this.btnOK.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmDialog.btnOK.idle");
            this.btnOK.Image = bot.Localization.clResourceManager.getButton("frmDialog.btnOK.idle");
            this.btnOK.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmDialog.btnOK.onclick");
            this.btnOK.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmDialog.btnOK.onhover");
            this.btnCancel.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.Image = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.idle");
            this.btnCancel.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onclick");
            this.btnCancel.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmCheckLicense.button2.onhover");
        }

        private void txtOutput_TextChanged(object sender, EventArgs e)
        {
            _output = txtOutput.Text;
        }

        void btnOK_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        void btnCancel_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        void frmDialog_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmDialog_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
