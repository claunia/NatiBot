/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmMap.cs
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
using System.Reflection;
using OpenMetaverse;
using System.Drawing.Imaging;
using System.Net;
using System.Timers;
using System.Diagnostics;
using OpenMetaverse.Imaging;
using OpenMetaverse.Assets;

namespace bot.GUI
{
    public partial class frmMap : Form
    {
        private SecondLifeBot _client;
        // = new GridClient();
        private UUID _MapImageID;
        private Image _MapLayer;
        private int px = 128;
        private int py = 128;
        private Simulator sim;
        private Point mouse_offset;

        public frmMap(SecondLifeBot Client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();

            this._client = Client;
            this.sim = this._client.Network.CurrentSim;
            this._client.Grid.CoarseLocationUpdate += new EventHandler<CoarseLocationUpdateEventArgs>(this.Grid_CoarseLocationUpdate);
            this._client.Network.SimChanged += new EventHandler<SimChangedEventArgs>(this.Network_SimChanged);
            btnTeleport.Text = bot.Localization.clResourceManager.getText("frmMap.btnTeleport");
            lblSelectedPosition.Text = bot.Localization.clResourceManager.getText("frmMap.lblSelectedPosition");
            lblMapDownloading.Text = bot.Localization.clResourceManager.getText("frmMap.lblMapDownloading");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.btnTeleport.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.idle");
            this.btnTeleport.Image = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.idle");
            this.btnTeleport.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.onclick");
            this.btnTeleport.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmFriends.btnTeleport.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmMap");
        }

        private void Assets_OnImageReceived(TextureRequestState image, AssetTexture texture)
        {
            if (texture.AssetID == this._MapImageID)
            {
                ManagedImage nullImage;
                OpenJPEG.DecodeToImage(texture.AssetData, out nullImage, out _MapLayer);

                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.UpdateMiniMap(sim);
                });
            }
        }

        private void Network_SimChanged(object sender, SimChangedEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.GetMap();
            });
        }

        private void GetMap()
        {
            GridRegion region;

            if (this._MapLayer == null || this.sim != this._client.Network.CurrentSim)
            {

                this.sim = this._client.Network.CurrentSim;
                this.lblSim.Text = String.Format(bot.Localization.clResourceManager.getText("frmMap.lblSim"), this.sim.Name, this.sim.SimVersion.Remove(0, 18));
                this.Text = String.Format(bot.Localization.clResourceManager.getText("frmMap.Text"), this.sim.Name);

                if (this._client.Grid.GetGridRegion(this._client.Network.CurrentSim.Name, GridLayerType.Objects, out region))
                {
                    _MapImageID = region.MapImageID;
                    this._client.Assets.RequestImage(_MapImageID, ImageType.Baked, this.Assets_OnImageReceived);
                }
            }
            else
            {
                this.UpdateMiniMap(sim);
            }
        }

        private void UpdateMiniMap(Simulator sim)
        {
            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.UpdateMiniMap(sim);
                });
            else
            {
                Bitmap bmp = this._MapLayer == null ? new Bitmap(256, 256) : (Bitmap)this._MapLayer.Clone();
                Graphics g = Graphics.FromImage(bmp);

                if (this._MapLayer == null)
                {
                    g.Clear(this.BackColor);
                    g.FillRectangle(Brushes.White, 0f, 0f, 256f, 256f);
                    this.lblMapDownloading.Visible = true;
                }
                else
                {
                    this.lblMapDownloading.Visible = false;
                }

                // Rollback change from 9.2.1
                if (!this.sim.AvatarPositions.ContainsKey(this._client.Self.AgentID))
                    return;
                //if (sim.PositionIndexYou == -1 || sim.PositionIndexYou >= sim.AvatarPositions.Count) return;

                int i = 0;

                // Rollback change from 9.2.1
                Vector3 myPos = this.sim.AvatarPositions[this._client.Self.AgentID];
                //Vector3 myPos = sim.AvatarPositions[sim.PositionIndexYou];

                // Rollback change from 9.2.1
                this._client.Network.CurrentSim.AvatarPositions.ForEach(
                    delegate(KeyValuePair<UUID, Vector3> pos)
                    {
                        int x = (int)pos.Value.X;
                        int y = 255 - (int)pos.Value.Y;
                        if (pos.Key != this._client.Self.AgentID)
                        {
                            //if (myPos.Z - (pos.Z * 4) > 5)
                            if (myPos.Z - pos.Value.Z > 20)
                            {
                                Point[] points = new Point[3] { new Point(x - 4, y + 4), new Point(x + 4, y + 4), new Point(x, y - 4) };
                                g.FillPolygon(Brushes.DarkRed, points);
                                g.DrawPolygon(new Pen(Brushes.Red, 1), points);
                            }
                            else if (myPos.Z - pos.Value.Z > -21 && myPos.Z - pos.Value.Z < 21)
                            {
                                Rectangle rect = new Rectangle((int)Math.Round(pos.Value.X, 0) - 2, 255 - ((int)Math.Round(pos.Value.Y, 0) - 2), 7, 7);
                                g.FillEllipse(Brushes.LightGreen, rect);
                                g.DrawEllipse(new Pen(Brushes.Green, 1), rect);
                            }
                            else
                            {
                                Rectangle rect = new Rectangle((int)Math.Round(pos.Value.X, 0) - 2, 255 - ((int)Math.Round(pos.Value.Y, 0) - 2), 7, 7);
                                g.FillRectangle(Brushes.MediumBlue, rect);
                                g.DrawRectangle(new Pen(Brushes.Red, 1), rect);
                            }
                        }
                        else
                        {
                            Rectangle myrect = new Rectangle((int)Math.Round(this._client.Self.SimPosition.X, 0) - 2, 255 - ((int)Math.Round(this._client.Self.SimPosition.Y, 0) - 2), 7, 7);
                            g.FillEllipse(new SolidBrush(Color.Yellow), myrect);
                            g.DrawEllipse(new Pen(Brushes.Red, 3), myrect);
                        }

                        i++;
                    }
                );

                myPos = this._client.Self.SimPosition;
                //myPos = sim.AvatarPositions[this._client.Self.AgentID];

                i = 0;

                g.DrawImage(bmp, 0, 0);
                picMap.Image = bmp;
                picMap.Cursor = Cursors.Cross;

                g.Dispose();

                string strInfo = string.Format(bot.Localization.clResourceManager.getText("frmMap.lblAvatars"), this._client.Network.CurrentSim.AvatarPositions.Count);
                lblAvatars.Text = strInfo;

                strInfo = string.Format("{0}/{1}/{2}/{3}", this._client.Network.CurrentSim.Name,
                    Math.Round(this._client.Self.SimPosition.X, 0),
                    Math.Round(this._client.Self.SimPosition.Y, 0),
                    Math.Round(this._client.Self.SimPosition.Z, 0));
                this.txtSLUrl.Text = "http://slurl.com/secondlife/" + strInfo;
                ;
            }
        }

        private void Grid_CoarseLocationUpdate(object sencder, CoarseLocationUpdateEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                this.UpdateMiniMap(e.Simulator);
            });
        }

        private void frmMapClient_Load(object sender, EventArgs e)
        {
            this.GetMap();

            Vector3 apos = this._client.Self.SimPosition;
            float aZ = apos.Z;

            //printMap();
            nudX.Value = 128;
            nudY.Value = 128;
            nudZ.Value = (decimal)aZ;
        }

        private void btnTeleport_Click(object sender, EventArgs e)
        {
            try
            {
                this._client.Netcom.Teleport(this._client.Network.CurrentSim.Name, new Vector3((float)nudX.Value, (float)nudY.Value, (float)nudY.Value));
            }
            catch (Exception ex)
            {
                MessageBox.Show(bot.Localization.clResourceManager.getText("frmMap.TeleportError"), "Natibot");
                return;
            }
        }

        private void world_MouseUp(object sender, MouseEventArgs e)
        {
            px = e.X;
            py = 255 - e.Y;

            nudX.Value = (decimal)px;
            nudY.Value = (decimal)py;
            nudZ.Value = (decimal)10;

            this.PlotSelected(e.X, e.Y);

            btnTeleport.Enabled = true;
        }

        private void PlotSelected(int x, int y)
        {
            this.UpdateMiniMap(sim);

            Bitmap map = (Bitmap)picMap.Image;
            Graphics g = Graphics.FromImage(map);

            Rectangle selectedrect = new Rectangle(x - 2, y - 2, 10, 10);
            g.DrawEllipse(new Pen(Brushes.Red, 2), selectedrect);
            this.picMap.Image = map;

            g.Dispose(); 
        }

        private void frmMapClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._client.Grid.CoarseLocationUpdate -= this.Grid_CoarseLocationUpdate;
            this._client.Network.SimChanged -= this.Network_SimChanged;
        }

        private void frmMapClient_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void frmMap_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        void frmMap_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }
    }
}
