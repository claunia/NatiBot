/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmProfile.cs
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
using System.Threading;
using OpenMetaverse.Assets;
using System.IO;
using Claunia.clUtils;
using OpenMetaverse.Imaging;

namespace bot.GUI
{
    public partial class frmProfile : Form
    {
        SecondLifeBot _client;
        private UUID _targetID, _FirstLifePhotoID, _SecondLifePhotoID;
        private Image _FirstLifePhoto, _SecondLifePhoto;
        ManualResetEvent WaitforAvatar = new ManualResetEvent(false);

        private delegate void SetPropertiesCallBack(Avatar.AvatarProperties Properties);

        private delegate void SetGroupsCallBack(List<AvatarGroup> Groups);

        private delegate void SetInterestsCallBack(Avatar.Interests Interests);

        private delegate void SetFirstPhotoCallBack();

        private delegate void SetSecondPhotoCallBack();

        SetPropertiesCallBack p;
        SetGroupsCallBack g;
        SetInterestsCallBack i;
        SetFirstPhotoCallBack sfp;
        SetSecondPhotoCallBack ssp;

        private System.Windows.Forms.WebBrowser webProfile;

        private Point mouse_offset;

        public frmProfile(SecondLifeBot client, UUID targetID)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            _client = client;
            _targetID = targetID;
            _FirstLifePhotoID = UUID.Zero;
            _SecondLifePhotoID = UUID.Zero;

            InitializeComponent();

            this.BackgroundImage = bot.Localization.clResourceManager.getWindow("frmProfile");

            if (Utilities.GetRunningRuntime() != Utilities.Runtime.Mono)
            {
                this.webProfile = new System.Windows.Forms.WebBrowser();
                // 
                // webProfile
                // 
                this.webProfile.Location = new System.Drawing.Point(6, 32);
                this.webProfile.Size = new System.Drawing.Size(426, 490);
                this.webProfile.Name = "webProfile";
                this.webProfile.TabIndex = 2;
                this.webProfile.Visible = false;
                this.tabPageWeb.Controls.Add(this.webProfile);
                this.tabPageWeb.ResumeLayout(false);
                this.tabPageWeb.PerformLayout();
                this.PerformLayout();
            }

            p = new SetPropertiesCallBack(SetProperties);
            g = new SetGroupsCallBack(SetGroups);
            i = new SetInterestsCallBack(SetInterests);
            sfp = new SetFirstPhotoCallBack(SetFirstPhoto);
            ssp = new SetSecondPhotoCallBack(SetSecondPhoto);

            this.tabPageSecondLife.Text = bot.Localization.clResourceManager.getText("frmProfile.tabPageSecondLife");
            this.tabPageWeb.Text = bot.Localization.clResourceManager.getText("frmProfile.tabPageWeb");
            this.tabPageInterests.Text = bot.Localization.clResourceManager.getText("frmProfile.tabPageInterests");
            this.tabPageFirstLife.Text = bot.Localization.clResourceManager.getText("frmProfile.tabPageFirstLife");
            this.lblName.Text = bot.Localization.clResourceManager.getText("frmProfile.lblName");
            this.lblBorn.Text = bot.Localization.clResourceManager.getText("frmProfile.lblBorn");
            this.lblPhoto.Text = bot.Localization.clResourceManager.getText("frmProfile.lblPhoto");
            this.lblAccount.Text = bot.Localization.clResourceManager.getText("frmProfile.lblAccount");
            this.lblPartner.Text = bot.Localization.clResourceManager.getText("frmProfile.lblPartner");
            this.lblGroups.Text = bot.Localization.clResourceManager.getText("frmProfile.lblGroups");
            this.lblAbout.Text = bot.Localization.clResourceManager.getText("frmProfile.lblAbout");
            this.lblWeb.Text = bot.Localization.clResourceManager.getText("frmProfile.lblWeb");
            this.lblWants.Text = bot.Localization.clResourceManager.getText("frmProfile.lblWants");
            this.chkBuild.Text = bot.Localization.clResourceManager.getText("frmProfile.chkBuild");
            this.chkMeet.Text = bot.Localization.clResourceManager.getText("frmProfile.chkMeet");
            this.chkSell.Text = bot.Localization.clResourceManager.getText("frmProfile.chkSell");
            this.chkBeHired.Text = bot.Localization.clResourceManager.getText("frmProfile.chkBeHired");
            this.chkExplore.Text = bot.Localization.clResourceManager.getText("frmProfile.chkExplore");
            this.chkGroup.Text = bot.Localization.clResourceManager.getText("frmProfile.chkGroup");
            this.chkBuy.Text = bot.Localization.clResourceManager.getText("frmProfile.chkBuy");
            this.chkHire.Text = bot.Localization.clResourceManager.getText("frmProfile.chkHire");
            this.lblSkills.Text = bot.Localization.clResourceManager.getText("frmProfile.lblSkills");
            this.chkTextures.Text = bot.Localization.clResourceManager.getText("frmProfile.chkTextures");
            this.chkModeling.Text = bot.Localization.clResourceManager.getText("frmProfile.chkModeling");
            this.chkScripting.Text = bot.Localization.clResourceManager.getText("frmProfile.chkScripting");
            this.chkArchitecture.Text = bot.Localization.clResourceManager.getText("frmProfile.chkArchitecture");
            this.chkEventPlanning.Text = bot.Localization.clResourceManager.getText("frmProfile.chkEventPlanning");
            this.chkCustomChars.Text = bot.Localization.clResourceManager.getText("frmProfile.chkCustomChars");
            this.lblLanguages.Text = bot.Localization.clResourceManager.getText("frmProfile.lblLanguages");
            this.lblPhotoF.Text = bot.Localization.clResourceManager.getText("frmProfile.lblPhotoF");
            this.lblInfo.Text = bot.Localization.clResourceManager.getText("frmProfile.lblInfo");


            this.btnClose.ButtonBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.Image = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.idle");
            this.btnClose.OnMouseClickBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onclick");
            this.btnClose.OnMouseOverBitmap = bot.Localization.clResourceManager.getNoLanguageButton("btnClose.onhover");
        }

        public void RefreshInfo()
        {
            _client.Avatars.AvatarPropertiesReply += new EventHandler<AvatarPropertiesReplyEventArgs>(this.Avatars_AvatarPropertiesReply);
            _client.Avatars.AvatarGroupsReply += new EventHandler<AvatarGroupsReplyEventArgs>(this.Avatars_OnAvatarGroups);
            _client.Avatars.AvatarInterestsReply += new EventHandler<AvatarInterestsReplyEventArgs>(this.Avatars_OnAvatarInterests);

            FillInformation();
        }

        private void FillInformation()
        {
            string avatarName = "";

            _client.key2Name(_targetID, out avatarName);

            txtUUID.Text = _targetID.ToString().ToUpper();
            txtName.Text = avatarName;

            this.Text = String.Format(bot.Localization.clResourceManager.getText("frmProfile.Text"), avatarName);
            lblProfile.Text = this.Text;

            _client.Avatars.RequestAvatarProperties(_targetID);
        }

        void Avatars_AvatarPropertiesReply(object sender, AvatarPropertiesReplyEventArgs e)
        {
            if (e.AvatarID == _targetID)
            {
                _client.Avatars.AvatarPropertiesReply -= this.Avatars_AvatarPropertiesReply;
                this.Invoke(p, e.Properties);
            }
            return;
        }

        void Avatars_OnAvatarGroups(object sender, AvatarGroupsReplyEventArgs e)
        {
            if (e.AvatarID == _targetID)
            {
                _client.Avatars.AvatarGroupsReply -= this.Avatars_OnAvatarGroups;
                this.Invoke(g, e.Groups);
            }
            return;
        }

        void Avatars_OnAvatarInterests(object sender, AvatarInterestsReplyEventArgs e)
        {
            if (e.AvatarID == _targetID)
            {
                _client.Avatars.AvatarInterestsReply -= this.Avatars_OnAvatarInterests;
                this.Invoke(i, e.Interests);
            }
            return;
        }

        private void SetInterests(Avatar.Interests Interests)
        {
            txtSkills.Text = Interests.SkillsText;
            txtLanguages.Text = Interests.LanguagesText;
            txtWants.Text = Interests.WantToText;

            DecodedInterests _decodedInterests;

            _decodedInterests = DecodeInterests(Interests);

            chkHire.Checked = _decodedInterests.Hire;
            chkBeHired.Checked = _decodedInterests.BeHired;
            chkSell.Checked = _decodedInterests.Sell;
            chkBuy.Checked = _decodedInterests.Buy;
            chkGroup.Checked = _decodedInterests.Group;
            chkMeet.Checked = _decodedInterests.Meet;
            chkExplore.Checked = _decodedInterests.Explore;
            chkBuild.Checked = _decodedInterests.Build;
            chkCustomChars.Checked = _decodedInterests.CustomCharacters;
            chkScripting.Checked = _decodedInterests.Scripting;
            chkModeling.Checked = _decodedInterests.Modeling;
            chkEventPlanning.Checked = _decodedInterests.EventPlanning;
            chkArchitecture.Checked = _decodedInterests.Architecture;
            chkTextures.Checked = _decodedInterests.Textures;
        }

        private DecodedInterests DecodeInterests(Avatar.Interests Interests)
        {
            DecodedInterests _decodedInterests = new DecodedInterests();

            // SKILLS
            if ((Interests.SkillsMask & 0x01) == 0x01)
                _decodedInterests.Textures = true;
            else
                _decodedInterests.Textures = false;
            if ((Interests.SkillsMask & 0x02) == 0x02)
                _decodedInterests.Architecture = true;
            else
                _decodedInterests.Architecture = false;
            if ((Interests.SkillsMask & 0x04) == 0x04)
                _decodedInterests.EventPlanning = true;
            else
                _decodedInterests.EventPlanning = false;
            if ((Interests.SkillsMask & 0x08) == 0x08)
                _decodedInterests.Modeling = true;
            else
                _decodedInterests.Modeling = false;
            if ((Interests.SkillsMask & 0x10) == 0x10)
                _decodedInterests.Scripting = true;
            else
                _decodedInterests.Scripting = false;
            if ((Interests.SkillsMask & 0x20) == 0x20)
                _decodedInterests.CustomCharacters = true;
            else
                _decodedInterests.CustomCharacters = false;

            // WANTS
            if ((Interests.WantToMask & 0x01) == 0x01)
                _decodedInterests.Build = true;
            else
                _decodedInterests.Build = false;
            if ((Interests.WantToMask & 0x02) == 0x02)
                _decodedInterests.Explore = true;
            else
                _decodedInterests.Explore = false;
            if ((Interests.WantToMask & 0x04) == 0x04)
                _decodedInterests.Meet = true;
            else
                _decodedInterests.Meet = false;
            if ((Interests.WantToMask & 0x08) == 0x08)
                _decodedInterests.Group = true;
            else
                _decodedInterests.Group = false;
            if ((Interests.WantToMask & 0x10) == 0x10)
                _decodedInterests.Buy = true;
            else
                _decodedInterests.Buy = false;
            if ((Interests.WantToMask & 0x20) == 0x20)
                _decodedInterests.Sell = true;
            else
                _decodedInterests.Sell = false;
            if ((Interests.WantToMask & 0x40) == 0x40)
                _decodedInterests.BeHired = true;
            else
                _decodedInterests.BeHired = false;
            if ((Interests.WantToMask & 0x80) == 0x80)
                _decodedInterests.Hire = true;
            else
                _decodedInterests.Hire = false;

            return _decodedInterests;
        }

        private void SetGroups(List<AvatarGroup> Groups)
        {
            lstGroups.Clear();
            foreach (AvatarGroup group in Groups)
            {
                lstGroups.Items.Add(group.GroupID.ToString(), group.GroupName, 0);
            }
            for (int i = 0; i < lstGroups.Items.Count; i++)
            {
                lstGroups.Items[i].ForeColor = Color.White;
                lstGroups.Items[i].BackColor = Color.Black;
                lstGroups.Items[i].UseItemStyleForSubItems = true;
            }
        }

        private void SetProperties(Avatar.AvatarProperties Properties)
        {
            string partnerName = "";
            Uri WebURL;

            txtAbout.Text = Properties.AboutText;
            txtBorn.Text = Properties.BornOn;
            _FirstLifePhotoID = Properties.FirstLifeImage;
            txtInfo.Text = Properties.FirstLifeText;
            _client.key2Name(Properties.Partner, out partnerName);
            if (partnerName != "(???) (???)")
                txtPartner.Text = partnerName;
            else
                txtPartner.Text = "";
            _SecondLifePhotoID = Properties.ProfileImage;
            txtWeb.Text = Properties.ProfileURL;
            if (Properties.Identified)
                txtAccount.Text += (bot.Localization.clResourceManager.getText("frmProfile.Account.Identified") + System.Environment.NewLine);
            if (Properties.MaturePublish)
                txtAccount.Text += (bot.Localization.clResourceManager.getText("frmProfile.Account.Mature") + System.Environment.NewLine);
            if (Properties.Transacted)
                txtAccount.Text += (bot.Localization.clResourceManager.getText("frmProfile.Account.Payment") + System.Environment.NewLine);
            if (Properties.AllowPublish)
                txtAccount.Text += (bot.Localization.clResourceManager.getText("frmProfile.Account.Public") + System.Environment.NewLine);
            if (Utilities.GetRunningRuntime() != Utilities.Runtime.Mono)
            {
                if (Uri.TryCreate(txtWeb.Text, UriKind.Absolute, out WebURL))
                {
                    webProfile.Url = WebURL;
                    webProfile.Visible = true;
                }
                else
                {
                    webProfile.Url = null;
                    webProfile.Visible = false;
                }
            }
            if (_FirstLifePhotoID != UUID.Zero)
            {
                if (File.Exists("textures/" + _FirstLifePhotoID.ToString().ToLower() + ".jp2"))
                    this.Invoke(sfp);
                else
                    _client.Assets.RequestImage(_FirstLifePhotoID, ImageType.Normal, OnFirstLifePhotoReceived);
            }
            if (_SecondLifePhotoID != UUID.Zero)
            {
                if (File.Exists("textures/" + _SecondLifePhotoID.ToString().ToLower() + ".jp2"))
                    this.Invoke(ssp);
                else
                    _client.Assets.RequestImage(_SecondLifePhotoID, ImageType.Normal, OnSecondLifePhotoReceived);
            }
        }

        private void OnFirstLifePhotoReceived(TextureRequestState state, AssetTexture asset)
        {
            if (asset.AssetID == _FirstLifePhotoID)
            {
                if (asset != null)
                {
                    try
                    {
                        File.WriteAllBytes("textures/" + asset.AssetID.ToString().ToLower() + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, _client);
                    }
                    finally
                    {
                        this.Invoke(sfp);
                    }
                }
            }
        }

        private void OnSecondLifePhotoReceived(TextureRequestState state, AssetTexture asset)
        {
            if (asset.AssetID == _SecondLifePhotoID)
            {
                if (asset != null)
                {
                    try
                    {
                        File.WriteAllBytes("textures/" + asset.AssetID.ToString().ToLower() + ".jp2", asset.AssetData);
                    }
                    catch (Exception ex)
                    {
                        bot.Console.WriteLine(ex.Message, Helpers.LogLevel.Error, _client);
                    }
                    finally
                    {
                        this.Invoke(ssp);
                    }
                }
            }
        }

        private void SetFirstPhoto()
        {
            ManagedImage nullImage;
            byte[] jp2Data;
            if (File.Exists("textures/" + _FirstLifePhotoID.ToString().ToLower() + ".jp2"))
                jp2Data = File.ReadAllBytes("textures/" + _FirstLifePhotoID.ToString().ToLower() + ".jp2");
            else
                return;

            if (jp2Data != null)
            if (OpenJPEG.DecodeToImage(jp2Data, out nullImage, out _FirstLifePhoto))
                picPhotoF.Image = _FirstLifePhoto;
        }

        private void SetSecondPhoto()
        {
            ManagedImage nullImage;
            byte[] jp2Data;
            if (File.Exists("textures/" + _SecondLifePhotoID.ToString().ToLower() + ".jp2"))
                jp2Data = File.ReadAllBytes("textures/" + _SecondLifePhotoID.ToString().ToLower() + ".jp2");
            else
                return;

            if (jp2Data != null)
            if (OpenJPEG.DecodeToImage(jp2Data, out nullImage, out _SecondLifePhoto))
                picPhoto.Image = _SecondLifePhoto;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmProfile_MouseDown(object sender, MouseEventArgs e)
        {
            mouse_offset = new Point(-e.X, -e.Y);
        }

        private void frmProfile_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Point mousePos = Control.MousePosition;
                mousePos.Offset(mouse_offset.X, mouse_offset.Y);
                this.Location = mousePos; //move the form to the desired location
            }
        }

        private struct DecodedInterests
        {
            public bool Hire;
            public bool BeHired;
            public bool Sell;
            public bool Buy;
            public bool Group;
            public bool Meet;
            public bool Explore;
            public bool Build;
            public bool CustomCharacters;
            public bool Scripting;
            public bool Modeling;
            public bool EventPlanning;
            public bool Architecture;
            public bool Textures;
        }
    }
}
