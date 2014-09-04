/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AccountFile.cs
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
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;

    public class AccountFile
    {
        private string m_file;

        public AccountFile(string file)
        {
            this.m_file = file;
        }

        public List<LoginDetails> Load()
        {
            return this.LoadXML();
        }

        public List<LoginDetails> LoadXML()
        {
            List<LoginDetails> list = new List<LoginDetails>();
            if (!File.Exists(this.m_file))
            {
                MessageBox.Show("The file " + this.m_file + " was not found");
                return list;
            }
            XmlDocument document = new XmlDocument();
            document.Load(this.m_file);
            foreach (XmlNode node in document.DocumentElement.ChildNodes)
            {
                try
                {
                    LoginDetails item = new LoginDetails();
                    item.FirstName = node.Attributes["Firstname"].InnerText;
                    item.LastName = node.Attributes["Lastname"].InnerText;
                    item.Password = node.Attributes["Password"].InnerText;
                    item.MasterName = node.Attributes["MasterName"].InnerText;
                    item.MasterKey = (OpenMetaverse.UUID)node.Attributes["MasterKey"].InnerText;
                    item.StartLocation = node.Attributes["StartLocation"].InnerText;
                    list.Add(item);
                    continue;
                }
                catch (Exception exception)
                {
                    bot.Console.WriteLine(exception.Message);
                    continue;
                }
            }
            return list;
        }

        public void Save(List<LoginDetails> accounts)
        {
            this.SaveXML(accounts);
        }

        public void SaveXML(List<LoginDetails> accounts)
        {
            XmlDocument document = new XmlDocument();
            XmlNode newChild = document.CreateElement("Accounts");
            document.AppendChild(newChild);
            foreach (LoginDetails details in accounts)
            {
                XmlNode node2 = document.CreateElement(string.Format("Account", details.FirstName, details.LastName));
                XmlAttribute node = document.CreateAttribute("Firstname");
                node.InnerText = details.FirstName;
                node2.Attributes.Append(node);
                node = document.CreateAttribute("Lastname");
                node.InnerText = details.LastName;
                node2.Attributes.Append(node);
                node = document.CreateAttribute("Password");
                node.InnerText = details.Password;
                node2.Attributes.Append(node);
                node = document.CreateAttribute("MasterName");
                node.InnerText = details.MasterName;
                node2.Attributes.Append(node);
                node = document.CreateAttribute("MasterKey");
                node.InnerText = details.MasterKey.ToString();
                node2.Attributes.Append(node);
                node = document.CreateAttribute("StartLocation");
                node.InnerText = details.StartLocation;
                node2.Attributes.Append(node);
                newChild.AppendChild(node2);
            }
            document.Save(this.m_file);
        }
    }
}

