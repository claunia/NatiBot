/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmAbout.cs
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
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Text;
using clControls;

namespace bot.GUI
{
    partial class frmAbout : Form
    {
        private Point mouse_offset;

        public frmAbout()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            InitializeComponent();
            this.Text = String.Format("About {0} {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0} {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            this.Text = bot.Localization.clResourceManager.getText("frmMain.btnAbout");
            //Starts putting buttons
            this.okButton.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.okButton.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.okButton.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.okButton.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmAbout");
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion

        private void frmAbout_Load(object sender, EventArgs e)
        {
            bot.license.Version botVersion = new bot.license.Version();
            StringBuilder sbDescription = new StringBuilder();

            sbDescription.Append("NatiBot " + botVersion.ToString() + " " + botVersion.v_rev);
            sbDescription.AppendLine();
            sbDescription.Append("© 2009-2010 Claunia.com " + bot.Localization.clResourceManager.getText("frmAbout.Copyright"));
            sbDescription.AppendLine();
            sbDescription.AppendLine();
            sbDescription.Append(bot.Localization.clResourceManager.getText("frmAbout.Coding"));
            sbDescription.Append("Natalia Portillo");
            sbDescription.AppendLine();
            sbDescription.Append(bot.Localization.clResourceManager.getText("frmAbout.Interface"));
            sbDescription.Append("Ana Sánchez");
            sbDescription.AppendLine();
            sbDescription.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.English"),
                "Natalia Portillo");
            sbDescription.AppendLine();
            sbDescription.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.Spanish"),
                "Natalia Portillo");
            sbDescription.AppendLine();
            sbDescription.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.Catalan"),
                "Alejandro Sánchez");
            sbDescription.AppendLine();
            sbDescription.AppendFormat(bot.Localization.clResourceManager.getText("frmAbout.TranslationOrder"),
                bot.Localization.clResourceManager.getText("frmAbout.Translation"),
                bot.Localization.clResourceManager.getText("Language.French"),
                "Natalia Portillo & Google");
            sbDescription.AppendLine();

            this.Text = "NatiBot " + botVersion.ToString() + " " + botVersion.v_rev;
            this.labelProductName.Text = "NatiBot";
            this.labelVersion.Text = botVersion.ToString() + " " + botVersion.v_rev;
            this.labelCopyright.Text = "© 2009 " + bot.Localization.clResourceManager.getText("frmAbout.Copyright");
            this.labelCompanyName.Text = "Claunia.com";
            this.textBoxDescription.Text = sbDescription.ToString();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAbout_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmAbout_MouseMove(object sender, MouseEventArgs e)
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
