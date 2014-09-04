/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : FindObjectsCommand.cs
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
namespace bot.Commands
{
    using bot;
    using OpenMetaverse;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Text;

    public class FindObjectsCommand : Command
    {
        Dictionary<UUID, Primitive> PrimsWaiting = new Dictionary<UUID, Primitive>();
        AutoResetEvent AllPropertiesReceived = new AutoResetEvent(false);
        StringBuilder sbResult = new StringBuilder();

        public FindObjectsCommand(SecondLifeBot SecondLifeBot)
        {
            SecondLifeBot.Objects.ObjectProperties += new EventHandler<ObjectPropertiesEventArgs>(Objects_OnObjectProperties);
            base.Name = "findobjects";
            base.Description = bot.Localization.clResourceManager.getText("Commands.FindObjects.Description") + " " + bot.Localization.clResourceManager.getText("Commands.FindObjects.Usage");
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            sbResult = new StringBuilder();

            // *** parse arguments ***
            if ((args.Length < 1) || (args.Length > 2))
                return bot.Localization.clResourceManager.getText("Commands.FindObjects.Usage");
            float radius = float.Parse(args[0]);
            string searchString = (args.Length > 1) ? args[1] : "";

            // *** get current location ***
            Vector3 location = Client.Self.SimPosition;

            // *** find all objects in radius ***
            List<Primitive> prims = Client.Network.CurrentSim.ObjectsPrimitives.FindAll(
                                        delegate(Primitive prim)
                {
                    Vector3 pos = prim.Position;
                    return ((prim.ParentID == 0) && (pos != Vector3.Zero) && (Vector3.Distance(pos, location) < radius));
                }
                                    );

            // *** request properties of these objects ***
            bool complete = RequestObjectProperties(prims, 250);

            foreach (Primitive p in prims)
            {
                string name = p.Properties.Name;
                if ((name != null) && (name.Contains(searchString)))
                {
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.FindObjects.Info"), name, p.ID.ToString());
                    sbResult.AppendLine();
                }
            }

            if (!complete)
            {
                sbResult.AppendLine(bot.Localization.clResourceManager.getText("Commands.FindObjects.Unable"));
                foreach (UUID uuid in PrimsWaiting.Keys)
                    sbResult.AppendLine(uuid.ToString());
            }

            sbResult.AppendFormat("Commands.FindObjects.Done");
            return sbResult.ToString();
        }

        private bool RequestObjectProperties(List<Primitive> objects, int msPerRequest)
        {
            // Create an array of the local IDs of all the prims we are requesting properties for
            uint[] localids = new uint[objects.Count];

            lock (PrimsWaiting)
            {
                PrimsWaiting.Clear();

                for (int i = 0; i < objects.Count; ++i)
                {
                    localids[i] = objects[i].LocalID;
                    PrimsWaiting.Add(objects[i].ID, objects[i]);
                }
            }

            Client.Objects.SelectObjects(Client.Network.CurrentSim, localids);

            return AllPropertiesReceived.WaitOne(2000 + msPerRequest * objects.Count, false);
        }

        void Objects_OnObjectProperties(object sender, ObjectPropertiesEventArgs e)
        {
            lock (PrimsWaiting)
            {
                Primitive prim;
                if (PrimsWaiting.TryGetValue(e.Properties.ObjectID, out prim))
                {
                    prim.Properties = e.Properties;
                }
                PrimsWaiting.Remove(e.Properties.ObjectID);

                if (PrimsWaiting.Count == 0)
                    AllPropertiesReceived.Set();
            }
        }
    }
}

