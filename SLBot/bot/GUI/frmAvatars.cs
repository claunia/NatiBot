/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmAvatars.cs
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
using System.Windows.Forms;
using OpenMetaverse;
using System.Drawing;

namespace bot.GUI
{
    public partial class frmAvatars : Form
    {
        SecondLifeBot _client;
        private Point mouse_offset;

        public frmAvatars(SecondLifeBot Client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();
            _client = Client;
            lstAvatars.Client = _client;
            Client.Network.SimChanged += Network_OnCurrentSimChanged;
            lblCurrentSim.Text = String.Format(bot.Localization.clResourceManager.getText("frmAvatars.lblCurrentSim"), _client.Network.CurrentSim.Name);
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAvatars");
        }

        void Network_OnCurrentSimChanged(object sender, SimChangedEventArgs e)
        {
            lblCurrentSim.Text = String.Format(bot.Localization.clResourceManager.getText("frmAvatars.lblCurrentSim"), _client.Network.CurrentSim.Name);
        }

        private void frmAvatars_Load(object sender, EventArgs e)
        {
            lstAvatars.InitializateAvatars();
            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmAvatars.Text"), _client.ToString());
        }

        private void lstAvatars_OnAvatarAdded(TrackedAvatar trackedAvatar)
        {
            lblAvatars.Text = String.Format(bot.Localization.clResourceManager.getText("frmAvatars.lblAvatars"), lstAvatars.Items.Count);
        }

        private void lstAvatars_OnAvatarRemoved(TrackedAvatar trackedAvatar)
        {
            lblAvatars.Text = String.Format(bot.Localization.clResourceManager.getText("frmAvatars.lblAvatars"), lstAvatars.Items.Count);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmAvatars_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmAvatars_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
