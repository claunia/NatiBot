/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmObjects.cs
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
using bot;
using bot.Objects;
using OpenMetaverse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using clControls;
using System.Text;

namespace bot.GUI
{
    public partial class frmObjects : Form
    {
        private SecondLifeBot client;
        private bool firsttick = true;

        private delegate void StartExportCallback(int maxvalue);

        private delegate void SetProgressValueCallback(int value,int maxvalue);

        private delegate void StopExportCallback(bool oldtimer);

        private Point mouse_offset;
        private int LastFound;

        private struct ObjectInSim
        {
            public string Name;
            public string ID;
            public string Location;
        }

        public frmObjects(SecondLifeBot client)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.InitializeComponent();

            this.client = client;
            this.client.Network.Disconnected += this.Network_OnDisconnected;

            //Puts language resources
            this.btnRefresh.Text = bot.Localization.clResourceManager.getText("frmObjects.btnRefresh");
            this.btnExport.Text = bot.Localization.clResourceManager.getText("frmObjects.btnExport");
            this.btnExportAll.Text = bot.Localization.clResourceManager.getText("frmObjects.btnExportAll");
            this.chkRefresh.Text = bot.Localization.clResourceManager.getText("frmObjects.chkRefresh");
            this.lblDistance.Text = bot.Localization.clResourceManager.getText("frmObjects.lblDistance");
            this.Text = bot.Localization.clResourceManager.getText("frmObjects.Text");
            this.lblfrmObjects.Text = bot.Localization.clResourceManager.getText("frmObjects.Text") + " - " + client.ToString();
            this.copyToClipboardToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.copyToClipboardToolStripMenuItem");
            this.namesToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.namesToolStripMenuItem");
            this.uUIDsToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.UUIDsToolStripMenuItem");
            this.locationsToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.locationsToolStripMenuItem");
            this.sitOnItToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.sitOnItToolStripMenuItem");
            this.touchThemToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.touchThemToolStripMenuItem");
            this.firstSelectedToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.firstSelectedToolStripMenuItem");
            this.allSelectedToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.allSelectedToolStripMenuItem");
            this.buyThemToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.buyThemToolStripMenuItem");
            this.exportToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.exportToolStripMenuItem");
            this.objectToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.objectToolStripMenuItem");
            this.particlesToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.particlesToolStripMenuItem");
            this.objectAndParticlesToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.objectAndParticlesToolStripMenuItem");
            this.requestTakeToInventoryToolStripMenuItem.Text = bot.Localization.clResourceManager.getText("frmObjects.requestTakeToInventoryToolStripMenuItem");
            //Ends putting language resources

            //Starts putting buttons
            this.btnExport.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExport.idle");
            this.btnExport.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnExport.idle");
            this.btnExport.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExport.onclick");
            this.btnExport.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExport.onhover");
            this.btnExportAll.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExportAll.idle");
            this.btnExportAll.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnExportAll.idle");
            this.btnExportAll.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExportAll.onclick");
            this.btnExportAll.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnExportAll.onhover");
            this.btnRefresh.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnRefresh.idle");
            this.btnRefresh.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnRefresh.idle");
            this.btnRefresh.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnRefresh.onclick");
            this.btnRefresh.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnRefresh.onhover");
            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.btnSearch.ButtonBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnFindNext.idle");
            this.btnSearch.Image = bot.Localization.clResourceManager.getButton("frmObjects.btnFindNext.idle");
            this.btnSearch.OnMouseClickBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnFindNext.onclick");
            this.btnSearch.OnMouseOverBitmap = bot.Localization.clResourceManager.getButton("frmObjects.btnFindNext.onhover");
            //Ends putting buttons

            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmObjects");
            lstObjects.Client = this.client;
            lstObjects.OnObjectAdded += new ObjectList.ObjectCallback(lstObjects_OnObjectAdded);
            lstObjects.OnObjectPropertiesUpdated += new ObjectList.ObjectCallback(lstObjects_OnObjectPropertiesUpdated);
            lstObjects.OnObjectUpdated += new ObjectList.ObjectCallback(lstObjects_OnObjectUpdated);
        }

        void lstObjects_OnObjectUpdated(TrackedObject trackedObject)
        {
            lblStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.Status.NewObject"), trackedObject.ID);
        }

        void lstObjects_OnObjectPropertiesUpdated(TrackedObject trackedObject)
        {
            lblStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.Status.GotName"), trackedObject.ID);
        }

        void lstObjects_OnObjectAdded(TrackedObject trackedObject)
        {
            lblStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.Status.Object"), trackedObject.ID);
        }

        private void frmObjects_Load(object sender, EventArgs e)
        {
            lstObjects.InitializateObjects();
            txtMeters.Text = client.Self.Movement.Camera.Far.ToString();
            LastFound = -1;
        }

        void Network_OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Close();
        }

        private void RefreshItemsCounters()
        {
            int totalinLST;
            int unkLST, knwLST;
            unkLST = 0;
            knwLST = unkLST;
            totalinLST = lstObjects.Items.Count;
            foreach (ListViewItem item in lstObjects.Items)
            {
                if (item.Text == bot.Localization.clResourceManager.getText("Controls.Loading"))
                    unkLST++;
                else
                    knwLST++;
            }

            lblObjects.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.Status.Statistics"), totalinLST, knwLST, unkLST);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshItemsCounters();
            lstObjects.RefreshObjects();
        }

        private void chkRefresh_CheckedChanged(object sender, EventArgs e)
        {
            tmrRefreshTimer.Enabled = chkRefresh.Checked;
        }

        private void tmrRefreshTimer_Tick(object sender, EventArgs e)
        {
            if (firsttick)
            {
                tmrRefreshTimer.Interval = 60000;
                firsttick = false;
            }

            RefreshItemsCounters();
            lstObjects.RefreshObjects();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<ObjectInSim> AllItems = new List<ObjectInSim>();

            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                if (item != null)
                {
                    ObjectInSim newItem;

                    newItem.ID = item.Name;
                    newItem.Location = item.SubItems[1].Text;
                    newItem.Name = item.Text;

                    AllItems.Add(newItem);
                }
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting {2} objects on sim {3}.", DateTime.Now.ToString(), this.client, AllItems.Count, this.client.Network.CurrentSim.Name));

            Thread thExport = new Thread(delegate()
            {
                int counter = 0;
                bool old_timer_status = tmrRefreshTimer.Enabled;

                StartExport(AllItems.Count);

                foreach (ObjectInSim item in AllItems)
                {

                    string filename;

                    if (item.Name == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        filename = CreateFileName(item.ID, item.Location);
                    else
                        filename = CreateFileName(item.ID, item.Location, item.Name);

                    counter++;
                    SetProgressValue(counter, AllItems.Count);

                    string cmd = "";
                    cmd = "export " +
                    item.ID +
                    " \"" + filename + "\"";
                    this.client.DoCommand(cmd, UUID.Zero, true);

                }

                StopExport(old_timer_status);
            });
            thExport.IsBackground = true;
            thExport.Name = "Export";
            thExport.Start();
        }

        public string CreateFileName(string UUID, string Location)
        {
            return CreateFileName(UUID, Location, bot.Localization.clResourceManager.getText("Object"));
        }

        public string CreateFileName(string UUID, string Location, string PrimName)
        {
            string corrected_PrimName;
            string corrected_Location;
            string FinalName;

            corrected_PrimName = PrimName.Replace(" ", "_");
            corrected_PrimName = corrected_PrimName.Replace(":", ";");
            corrected_PrimName = corrected_PrimName.Replace("*", "+");
            corrected_PrimName = corrected_PrimName.Replace("|", "I");
            corrected_PrimName = corrected_PrimName.Replace("\\", "[");
            corrected_PrimName = corrected_PrimName.Replace("/", "]");
            corrected_PrimName = corrected_PrimName.Replace("?", "¿");
            corrected_PrimName = corrected_PrimName.Replace(">", "}");
            corrected_PrimName = corrected_PrimName.Replace("<", "{");
            corrected_PrimName = corrected_PrimName.Replace("\"", "'");
            corrected_PrimName = corrected_PrimName.Replace("\n", " ");

            corrected_Location = Location.Replace(">", "}");
            corrected_Location = corrected_Location.Replace("<", "{");

            FinalName = corrected_PrimName + " (" + UUID + ", " + corrected_Location + ").xml";

            return FinalName;
        }

        private void StartExport(int maxValue)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    StartExport(maxValue);
                });
            else
            {
                prgExporting.Visible = true;
                prgExporting.Minimum = 0;
                prgExporting.Maximum = maxValue;
                lblStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.ExportingObjects"), maxValue.ToString());
                tmrRefreshTimer.Enabled = false;
                btnExport.Enabled = false;
                chkRefresh.Enabled = false;
                btnExportAll.Enabled = false;
                btnRefresh.Enabled = false;
                txtMeters.Enabled = false;
                lstObjects.StopRefreshing = true;
            }
        }

        private void SetProgressValue(int value, int maxvalue)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    SetProgressValue(value, maxvalue);
                });
            else
            {
                prgExporting.Value = value;
                lblStatus.Text = String.Format(bot.Localization.clResourceManager.getText("frmObjects.ExportingObject"), value.ToString(), maxvalue.ToString());
            }
        }

        private void StopExport(bool oldtimer)
        {
            if (!this.IsHandleCreated)
                return;

            if (this.InvokeRequired)
                this.BeginInvoke((MethodInvoker)delegate
                {
                    StopExport(oldtimer);
                });
            else
            {
                prgExporting.Visible = false;
                tmrRefreshTimer.Enabled = oldtimer;
                lblStatus.Text = bot.Localization.clResourceManager.getText("frmObjects.Idle");
                btnExport.Enabled = true;
                chkRefresh.Enabled = true;
                btnExportAll.Enabled = true;
                btnRefresh.Enabled = true;
                txtMeters.Enabled = true;
                lstObjects.StopRefreshing = false;
            }
        }

        private void btnExportAll_Click(object sender, EventArgs e)
        {
            List<ObjectInSim> AllItems = new List<ObjectInSim>();

            foreach (ListViewItem item in lstObjects.Items)
            {
                if (item != null)
                {
                    ObjectInSim newItem;

                    newItem.ID = item.Name;
                    newItem.Location = item.SubItems[1].Text;
                    newItem.Name = item.Text;

                    AllItems.Add(newItem);
                }
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting {2} objects (all) on sim {3}.", DateTime.Now.ToString(), this.client, AllItems.Count, this.client.Network.CurrentSim.Name));

            Thread thExAll = new Thread(delegate()
            {
                int counter = 0;
                bool old_timer_status = tmrRefreshTimer.Enabled;

                StartExport(lstObjects.Items.Count);

                foreach (ObjectInSim item in AllItems)
                {
                    string filename;

                    if (item.Name == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        filename = CreateFileName(item.ID, item.Location);
                    else
                        filename = CreateFileName(item.ID, item.Location, item.Name);

                    counter++;
                    SetProgressValue(counter, lstObjects.Items.Count);

                    string cmd = "";
                    cmd = "export " +
                    item.ID +
                    " \"" + filename + "\"";
                    this.client.DoCommand(cmd, UUID.Zero, true);
                }

                StopExport(old_timer_status);
            });
            thExAll.IsBackground = true;
            thExAll.Name = "Export all";
            thExAll.Start();
        }

        private void txtMeters_TextChanged(object sender, EventArgs e)
        {
            float distance;

            if (!float.TryParse(txtMeters.Text, out distance))
                txtMeters.Text = client.Self.Movement.Camera.Far.ToString();
            else
            {
                client.Self.Movement.Camera.Far = distance;
                txtMeters.Text = client.Self.Movement.Camera.Far.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmObjects_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmObjects_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bool SomethingFound;
            int counter;
            string rowPrimName;

            SomethingFound = false;
            chkRefresh.Checked = false;

            lstObjects.StopRefreshing = true;

            for (counter = 0; counter < lstObjects.Items.Count; counter++)
            {
                rowPrimName = lstObjects.Items[counter].Text;

                if (System.Text.RegularExpressions.Regex.IsMatch(rowPrimName,
                        txtSearch.Text, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    if (LastFound < counter)
                    {
                        SomethingFound = true;
                        LastFound = counter;

                        lstObjects.SelectedItems.Clear();
                        lstObjects.Items[counter].Selected = true;
                        lstObjects.Select();

                        break;
                    }
                }
            }

            lstObjects.StopRefreshing = false;

            if (SomethingFound)
                return;

            if (LastFound != -1 && SomethingFound == false)
            {
                LastFound = -1;
                btnSearch_Click(sender, e);
            }
        }

        private void namesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sbNames = new StringBuilder();

            lstObjects.StopRefreshing = true;
            if (lstObjects.SelectedItems.Count == 1)
            {
                Clipboard.SetText(lstObjects.SelectedItems[0].Text);
                lstObjects.StopRefreshing = false;
                return;
            }
            else
            {
                foreach (ListViewItem item in lstObjects.SelectedItems)
                    sbNames.AppendLine(item.Text);
            }
            lstObjects.StopRefreshing = false;

            Clipboard.SetText(sbNames.ToString());
        }

        private void locationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sbLocations = new StringBuilder();

            lstObjects.StopRefreshing = true;
            if (lstObjects.SelectedItems.Count == 1)
            {
                Clipboard.SetText(lstObjects.SelectedItems[0].SubItems[1].Text);
                lstObjects.StopRefreshing = false;
                return;
            }
            else
            {
                foreach (ListViewItem item in lstObjects.SelectedItems)
                    sbLocations.AppendLine(item.SubItems[1].Text);
            }
            lstObjects.StopRefreshing = false;

            Clipboard.SetText(sbLocations.ToString());
        }

        private void UUIDsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder sbUUIDs = new StringBuilder();

            lstObjects.StopRefreshing = true;
            if (lstObjects.SelectedItems.Count == 1)
            {
                Clipboard.SetText(lstObjects.SelectedItems[0].Name);
                lstObjects.StopRefreshing = false;
                return;
            }
            else
            {
                foreach (ListViewItem item in lstObjects.SelectedItems)
                    sbUUIDs.AppendLine(item.Name);
            }
            lstObjects.StopRefreshing = false;

            Clipboard.SetText(sbUUIDs.ToString());
        }

        private void firstSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.client.DoCommand("siton " + lstObjects.SelectedItems[0].Name, UUID.Zero, true);
        }

        private void allSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstObjects.StopRefreshing = true;
            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                this.client.DoCommand("siton " + item.Name, UUID.Zero, true);
                this.client.DoCommand("stand", UUID.Zero, true);
            }
            lstObjects.StopRefreshing = false;
        }

        private void touchThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstObjects.StopRefreshing = true;
            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                this.client.DoCommand("touch " + item.Name, UUID.Zero, true);
            }
            lstObjects.StopRefreshing = false;
        }

        private void buyThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstObjects.StopRefreshing = true;
            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                this.client.DoCommand("buy " + item.Name, UUID.Zero, true);
            }
            lstObjects.StopRefreshing = false;
        }

        private void requestTakeToInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstObjects.StopRefreshing = true;
            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                this.client.DoCommand("takeitem " + item.Name, UUID.Zero, true);
            }
            lstObjects.StopRefreshing = false;
        }

        private void objectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ObjectInSim> AllItems = new List<ObjectInSim>();

            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                if (item != null)
                {
                    ObjectInSim newItem;

                    newItem.ID = item.Name;
                    newItem.Location = item.SubItems[1].Text;
                    newItem.Name = item.Text;

                    AllItems.Add(newItem);
                }
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting {2} objects on sim {3}.", DateTime.Now.ToString(), this.client, AllItems.Count, this.client.Network.CurrentSim.Name));

            Thread thExport = new Thread(delegate()
            {
                int counter = 0;
                bool old_timer_status = tmrRefreshTimer.Enabled;

                StartExport(AllItems.Count);

                foreach (ObjectInSim item in AllItems)
                {

                    string filename;

                    if (item.Name == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        filename = CreateFileName(item.ID, item.Location);
                    else
                        filename = CreateFileName(item.ID, item.Location, item.Name);

                    counter++;
                    SetProgressValue(counter, AllItems.Count);

                    string cmd = "";
                    cmd = "export " +
                    item.ID +
                    " \"" + filename + "\"";
                    this.client.DoCommand(cmd, UUID.Zero, true);

                }

                StopExport(old_timer_status);
            });
            thExport.IsBackground = true;
            thExport.Name = "Export";
            thExport.Start();
        }

        private void particlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ObjectInSim> AllItems = new List<ObjectInSim>();

            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                if (item != null)
                {
                    ObjectInSim newItem;

                    newItem.ID = item.Name;
                    newItem.Location = item.SubItems[1].Text;
                    newItem.Name = item.Text;

                    AllItems.Add(newItem);
                }
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting {2} particles on sim {3}.", DateTime.Now.ToString(), this.client, AllItems.Count, this.client.Network.CurrentSim.Name));

            Thread thExport = new Thread(delegate()
            {
                int counter = 0;
                bool old_timer_status = tmrRefreshTimer.Enabled;

                StartExport(AllItems.Count);

                foreach (ObjectInSim item in AllItems)
                {

                    string filename;

                    if (item.Name == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        filename = CreateFileName(item.ID, item.Location);
                    else
                        filename = CreateFileName(item.ID, item.Location, item.Name);

                    counter++;
                    SetProgressValue(counter, AllItems.Count);

                    string cmd = "";
                    cmd = "exportparticles " +
                    item.ID;
                    this.client.DoCommand(cmd, UUID.Zero, true);

                }

                StopExport(old_timer_status);
            });
            thExport.IsBackground = true;
            thExport.Name = "Export";
            thExport.Start();
        }

        private void objectAndParticlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<ObjectInSim> AllItems = new List<ObjectInSim>();

            foreach (ListViewItem item in lstObjects.SelectedItems)
            {
                if (item != null)
                {
                    ObjectInSim newItem;

                    newItem.ID = item.Name;
                    newItem.Location = item.SubItems[1].Text;
                    newItem.Name = item.Text;

                    AllItems.Add(newItem);
                }
            }

            Program.NBStats.AddStatData(String.Format("{0}: {1} exporting {2} objects/particles on sim {3}.", DateTime.Now.ToString(), this.client, AllItems.Count, this.client.Network.CurrentSim.Name));

            Thread thExport = new Thread(delegate()
            {
                int counter = 0;
                bool old_timer_status = tmrRefreshTimer.Enabled;

                StartExport(AllItems.Count);

                foreach (ObjectInSim item in AllItems)
                {

                    string filename;

                    if (item.Name == bot.Localization.clResourceManager.getText("Controls.Loading"))
                        filename = CreateFileName(item.ID, item.Location);
                    else
                        filename = CreateFileName(item.ID, item.Location, item.Name);

                    counter++;
                    SetProgressValue(counter, AllItems.Count);

                    string cmd = "";
                    cmd = "export " +
                    item.ID +
                    " \"" + filename + "\"";
                    this.client.DoCommand(cmd, UUID.Zero, true);
                    cmd = "exportparticles " +
                    item.ID;
                    this.client.DoCommand(cmd, UUID.Zero, true);

                }

                StopExport(old_timer_status);
            });
            thExport.IsBackground = true;
            thExport.Name = "Export";
            thExport.Start();
        }
    }
}
