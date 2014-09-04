/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ObjectsListItem.cs
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
namespace bot.Objects
{
    using OpenMetaverse;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class ObjectsListItem
    {
        private GridClient client;
        private bool gettingProperties;
        private bool gotProperties;
        private ListBox listBox;
        private Primitive prim;

        public event EventHandler PropertiesReceived;

        public ObjectsListItem(Primitive prim, GridClient client, ListBox listBox)
        {
            this.prim = prim;
            this.client = client;
            this.listBox = listBox;
        }

        void Objects_OnObjectPropertiesFamily(object sender, ObjectPropertiesFamilyEventArgs e)
        {
            if (e.Properties.ObjectID == this.prim.ID)
            {
                this.gettingProperties = false;
                this.gotProperties = true;
                this.prim.Properties = e.Properties;
                this.listBox.BeginInvoke(new OnPropReceivedRaise(this.OnPropertiesReceived), new object[] { EventArgs.Empty });
            }
        }

        protected virtual void OnPropertiesReceived(EventArgs e)
        {
            if (this.PropertiesReceived != null)
            {
                this.PropertiesReceived(this, e);
            }
        }

        public void RequestProperties()
        {
            if (this.prim.Properties == null)
            //if (string.IsNullOrEmpty(this.prim.Properties.Name))
            {
                this.gettingProperties = true;
                this.client.Objects.ObjectPropertiesFamily += new EventHandler<ObjectPropertiesFamilyEventArgs>(this.Objects_OnObjectPropertiesFamily);
                this.client.Objects.RequestObjectPropertiesFamily(this.client.Network.CurrentSim, this.prim.ID);
            }
            else
            {
                this.gotProperties = true;
                this.OnPropertiesReceived(EventArgs.Empty);
            }
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(this.prim.Properties.Name))
            {
                return this.prim.Properties.Name;
            }
            return "...";
        }

        public bool GettingProperties
        {
            get
            {
                return this.gettingProperties;
            }
        }

        public bool GotProperties
        {
            get
            {
                return this.gotProperties;
            }
        }

        public Primitive Prim
        {
            get
            {
                return this.prim;
            }
        }

        private delegate void OnPropReceivedRaise(EventArgs e);
    }
}

