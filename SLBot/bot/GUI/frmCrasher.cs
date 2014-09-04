/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : frmCrasher.cs
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
using OpenMetaverse;
using OpenMetaverse.Packets;
using System.Text;
using System.Windows.Forms;

namespace bot.GUI
{

    public class frmCrasher : Form
    {
        // Fields
        private Button buttonFieldCopy;
        private Button buttonFieldCreate;
        private Button buttonFieldDelete;
        private Button buttonLogin;
        private Button buttonSendPacket;
        private Button buttonTypeDataDelete;
        private Button buttonTypeDataDown;
        private Button buttonTypeDataUp;
        private CheckBox cbAgentIDMe;
        private ComboBox cbFieldType;
        private CheckBox cbIDRandom;
        private CheckBox cbLoginStart;
        private ComboBox cbType;
        private GridClient client;
        private System.ComponentModel.IContainer components = null;
        private Dictionary<string, byte[]> CustomFields = new Dictionary<string, byte[]>();
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private ListBox lbFieldNames;
        private ListBox lbTypeData;
        private byte[] MyTypeData = new byte[0];
        private TextBox tbAgentID;
        private TextBox tbColorA;
        private TextBox tbColorB;
        private TextBox tbColorG;
        private TextBox tbColorR;
        private TextBox tbDuration;
        private TextBox tbFieldInput;
        private TextBox tbFieldName;
        private TextBox tbID;
        private Dictionary<int, object[]> TypeDataFields = new Dictionary<int, object[]>();

        // Methods
        public frmCrasher(SecondLifeBot lol)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            this.client = lol;
            this.Icon = bot.Localization.clResourceManager.getIcon();
            this.InitializeComponent();
        }

        private void buttonFieldCopy_Click(object sender, EventArgs e)
        {
            if (this.lbFieldNames.SelectedItem != null)
            {
                string str = this.lbFieldNames.SelectedItem.ToString();
                this.TypeDataFields.Add(this.TypeDataFields.Count, new object[] { str, this.CustomFields[str] });
                this.RefreshTypeDataFields();
            }
        }

        private void buttonFieldCreate_Click(object sender, EventArgs e)
        {
            string text = this.tbFieldInput.Text;
            string str2 = this.cbFieldType.Text.ToLower();
            byte[] bytes = new byte[0];
            switch (str2)
            {
                case "byte":
                    try
                    {
                        bytes = new byte[] { byte.Parse(text) };
                    }
                    catch
                    {
                        MessageBox.Show("Invalid value for byte.");
                        return;
                    }
                    goto Label_0277;

                case "lluuid":
                    UUID lluuid;
                    try
                    {
                        lluuid = new UUID(text);
                    }
                    catch
                    {
                        MessageBox.Show("Bad UUID");
                        return;
                    }
                    bytes = lluuid.GetBytes();
                    goto Label_0277;

                case "llvector3d":
                case "llvector3":
                    {
                        string[] strArray = text.Trim(" <>".ToCharArray()).Split(",".ToCharArray());
                        if (strArray.Length == 3)
                        {
                            double[] numArray = new double[3];
                            for (int i = 0; i < 3; i++)
                            {
                                try
                                {
                                    numArray[i] = double.Parse(strArray[i]);
                                }
                                catch
                                {
                                    MessageBox.Show("Something's wrong with value " + ((i + 1)).ToString() + " for the vector.");
                                    return;
                                }
                            }
                            switch (str2)
                            {
                                case "llvector3d":
                                    bytes = new Vector3d(numArray[0], numArray[1], numArray[2]).GetBytes();
                                    break;

                                case "llvector3":
                                    bytes = new Vector3((float)numArray[0], (float)numArray[1], (float)numArray[2]).GetBytes();
                                    break;
                            }
                            goto Label_0277;
                        }
                        MessageBox.Show("Need three comma separated values to make a vector.");
                        return;
                    }
                case "utf8encoding":
                    bytes = new UTF8Encoding().GetBytes(text.ToCharArray());
                    goto Label_0277;

                case "null-terminated utf8encoding":
                    {
                        char[] chars = new char[text.ToCharArray().Length + 1];
                        chars[text.ToCharArray().Length] = '\0';
                        bytes = new UTF8Encoding().GetBytes(chars);
                        goto Label_0277;
                    }
            }
            MessageBox.Show("Invalid type.");
            return;
            Label_0277:
            if (this.CustomFields.ContainsKey(this.tbFieldName.Text))
            {
                MessageBox.Show("A custom field with that name already exists.");
            }
            else
            {
                this.CustomFields.Add(this.tbFieldName.Text, bytes);
                this.RefreshCustomFields();
            }
        }

        private void buttonFieldDelete_Click(object sender, EventArgs e)
        {
            if (this.lbFieldNames.SelectedItem != null)
            {
                this.CustomFields.Remove(this.lbFieldNames.SelectedItem.ToString());
                this.RefreshCustomFields();
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
        
        }

        private void buttonSendPacket_Click(object sender, EventArgs e)
        {
            UUID lluuid;
            UUID agentID;
            byte num;
            float num2;
            this.RefreshTypeDataFields();
            byte[] buffer = new byte[4];
            if (this.cbIDRandom.Checked)
            {
                lluuid = UUID.Random();
            }
            else
            {
                try
                {
                    lluuid = new UUID(this.tbID.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid ID for EffectBlock.");
                    return;
                }
            }
            if (this.cbAgentIDMe.Checked)
            {
                agentID = this.client.Self.AgentID;
            }
            else
            {
                try
                {
                    agentID = new UUID(this.tbAgentID.Text);
                }
                catch
                {
                    MessageBox.Show("Invalid AgentID for EffectBlock.");
                    return;
                }
            }
            try
            {
                num2 = float.Parse(this.tbDuration.Text);
            }
            catch
            {
                MessageBox.Show("Invalid Duration for EffectBlock");
                return;
            }
            try
            {
                buffer = new byte[] { byte.Parse(this.tbColorR.Text), byte.Parse(this.tbColorG.Text), byte.Parse(this.tbColorB.Text), byte.Parse(this.tbColorA.Text) };
            }
            catch
            {
                MessageBox.Show("Invalid Color for EffectBlock");
                return;
            }
            switch (this.cbType.Text.ToLower())
            {
                case "text":
                    num = 0;
                    break;

                case "icon":
                    num = 1;
                    break;

                case "connector":
                    num = 2;
                    break;

                case "flexibleobject":
                    num = 3;
                    break;

                case "animalcontrols":
                    num = 4;
                    break;

                case "animationobject":
                    num = 5;
                    break;

                case "cloth":
                    num = 6;
                    break;

                case "beam":
                    num = 7;
                    break;

                case "glow":
                    num = 8;
                    break;

                case "point":
                    num = 9;
                    break;

                case "trail":
                    num = 10;
                    break;

                case "sphere":
                    num = 11;
                    break;

                case "spiral":
                    num = 12;
                    break;

                case "edit":
                    num = 13;
                    break;

                case "lookat":
                    num = 14;
                    break;

                case "pointat":
                    num = 15;
                    break;

                default:
                    MessageBox.Show("Invalid Type for EffectBlock.");
                    return;
            }
            ViewerEffectPacket packet = new ViewerEffectPacket();
            packet.AgentData.AgentID = this.client.Self.AgentID;
            packet.AgentData.SessionID = this.client.Self.SessionID;
            packet.Effect = new ViewerEffectPacket.EffectBlock[] { new ViewerEffectPacket.EffectBlock() };
            packet.Effect[0].AgentID = agentID;
            packet.Effect[0].Color = buffer;
            packet.Effect[0].Duration = num2;
            packet.Effect[0].ID = lluuid;
            packet.Effect[0].Type = num;
            packet.Effect[0].TypeData = this.MyTypeData;
            this.client.Network.SendPacket(packet);
        }

        private void buttonTypeDataDelete_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.lbTypeData.SelectedIndex;
            if (selectedIndex != -1)
            {
                this.TypeDataFields.Remove(selectedIndex);
                Dictionary<int, object[]> dictionary = new Dictionary<int, object[]>();
                int key = 0;
                foreach (KeyValuePair<int, object[]> pair in this.TypeDataFields)
                {
                    dictionary.Add(key++, pair.Value);
                }
                int count = this.TypeDataFields.Count;
                this.TypeDataFields.Clear();
                for (key = 0; key < count; key++)
                {
                    this.TypeDataFields.Add(key, dictionary[key]);
                }
                this.RefreshTypeDataFields();
            }
        }

        private void buttonTypeDataDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.lbTypeData.SelectedIndex;
            if ((selectedIndex != -1) && (selectedIndex < (this.TypeDataFields.Count - 1)))
            {
                object[] objArray = this.TypeDataFields[selectedIndex + 1];
                this.TypeDataFields[selectedIndex + 1] = this.TypeDataFields[selectedIndex];
                this.TypeDataFields[selectedIndex] = objArray;
                this.RefreshTypeDataFields();
                this.lbTypeData.SelectedIndex = selectedIndex + 1;
            }
        }

        private void buttonTypeDataUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.lbTypeData.SelectedIndex;
            if (selectedIndex > 0)
            {
                object[] objArray = this.TypeDataFields[selectedIndex - 1];
                this.TypeDataFields[selectedIndex - 1] = this.TypeDataFields[selectedIndex];
                this.TypeDataFields[selectedIndex] = objArray;
                this.RefreshTypeDataFields();
                this.lbTypeData.SelectedIndex = selectedIndex - 1;
            }
        }

        private string BytesToHex(byte[] hval)
        {
            char[] chArray = new char[hval.Length * 2];
            char[] chArray2 = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            for (int i = 0; i < hval.Length; i++)
            {
                chArray[i * 2] = chArray2[(hval[i] >> 4) & 15];
                chArray[(i * 2) + 1] = chArray2[hval[i] & 15];
            }
            return new string(chArray);
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CustomFields.Add("*Self.GlobalPosition", new byte[0]);
            this.CustomFields.Add("*Self.GlobalPosition+<0,0,2>", new byte[0]);
            this.CustomFields.Add("*Vector3d.Zero", new byte[0]);
            this.CustomFields.Add("*Self.AgentID", new byte[0]);
            this.CustomFields.Add("*UUID.Zero", new byte[0]);
            this.RefreshCustomFields();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCrasher));
            this.cbLoginStart = new System.Windows.Forms.CheckBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbFieldName = new System.Windows.Forms.TextBox();
            this.buttonFieldCreate = new System.Windows.Forms.Button();
            this.cbFieldType = new System.Windows.Forms.ComboBox();
            this.tbFieldInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonTypeDataDelete = new System.Windows.Forms.Button();
            this.buttonTypeDataDown = new System.Windows.Forms.Button();
            this.buttonTypeDataUp = new System.Windows.Forms.Button();
            this.lbTypeData = new System.Windows.Forms.ListBox();
            this.buttonFieldCopy = new System.Windows.Forms.Button();
            this.buttonFieldDelete = new System.Windows.Forms.Button();
            this.lbFieldNames = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbAgentIDMe = new System.Windows.Forms.CheckBox();
            this.cbIDRandom = new System.Windows.Forms.CheckBox();
            this.tbColorR = new System.Windows.Forms.TextBox();
            this.tbColorG = new System.Windows.Forms.TextBox();
            this.tbColorB = new System.Windows.Forms.TextBox();
            this.tbColorA = new System.Windows.Forms.TextBox();
            this.tbDuration = new System.Windows.Forms.TextBox();
            this.cbType = new System.Windows.Forms.ComboBox();
            this.tbAgentID = new System.Windows.Forms.TextBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSendPacket = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLoginStart
            // 
            this.cbLoginStart.AutoSize = true;
            this.cbLoginStart.Location = new System.Drawing.Point(7, 48);
            this.cbLoginStart.Name = "cbLoginStart";
            this.cbLoginStart.Size = new System.Drawing.Size(92, 17);
            this.cbLoginStart.TabIndex = 8;
            this.cbLoginStart.Text = "Start Location";
            this.cbLoginStart.UseVisualStyleBackColor = true;
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(234, 18);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "Login";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbFieldName);
            this.groupBox1.Controls.Add(this.buttonFieldCreate);
            this.groupBox1.Controls.Add(this.cbFieldType);
            this.groupBox1.Controls.Add(this.tbFieldInput);
            this.groupBox1.Location = new System.Drawing.Point(13, 173);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 83);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "New TypeData Field";
            // 
            // tbFieldName
            // 
            this.tbFieldName.Location = new System.Drawing.Point(133, 47);
            this.tbFieldName.Name = "tbFieldName";
            this.tbFieldName.Size = new System.Drawing.Size(95, 20);
            this.tbFieldName.TabIndex = 3;
            this.tbFieldName.Text = "Name";
            // 
            // buttonFieldCreate
            // 
            this.buttonFieldCreate.Location = new System.Drawing.Point(234, 45);
            this.buttonFieldCreate.Name = "buttonFieldCreate";
            this.buttonFieldCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonFieldCreate.TabIndex = 2;
            this.buttonFieldCreate.Text = "Create";
            this.buttonFieldCreate.UseVisualStyleBackColor = true;
            this.buttonFieldCreate.Click += new System.EventHandler(this.buttonFieldCreate_Click);
            // 
            // cbFieldType
            // 
            this.cbFieldType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFieldType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbFieldType.FormattingEnabled = true;
            this.cbFieldType.Items.AddRange(new object[]
            {
                "UUID",
                "Vector3",
                "Vector3d",
                "UTF8Encoding",
                "Null-terminated UTF8Encoding",
                "Byte"
            });
            this.cbFieldType.Location = new System.Drawing.Point(7, 47);
            this.cbFieldType.Name = "cbFieldType";
            this.cbFieldType.Size = new System.Drawing.Size(120, 21);
            this.cbFieldType.TabIndex = 1;
            this.cbFieldType.Text = "Type...";
            // 
            // tbFieldInput
            // 
            this.tbFieldInput.Location = new System.Drawing.Point(7, 19);
            this.tbFieldInput.Name = "tbFieldInput";
            this.tbFieldInput.Size = new System.Drawing.Size(302, 20);
            this.tbFieldInput.TabIndex = 0;
            this.tbFieldInput.Text = "Data";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonTypeDataDelete);
            this.groupBox2.Controls.Add(this.buttonTypeDataDown);
            this.groupBox2.Controls.Add(this.buttonTypeDataUp);
            this.groupBox2.Controls.Add(this.lbTypeData);
            this.groupBox2.Controls.Add(this.buttonFieldCopy);
            this.groupBox2.Controls.Add(this.buttonFieldDelete);
            this.groupBox2.Controls.Add(this.lbFieldNames);
            this.groupBox2.Location = new System.Drawing.Point(13, 262);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 256);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TypeData Builder";
            // 
            // buttonTypeDataDelete
            // 
            this.buttonTypeDataDelete.Location = new System.Drawing.Point(268, 225);
            this.buttonTypeDataDelete.Name = "buttonTypeDataDelete";
            this.buttonTypeDataDelete.Size = new System.Drawing.Size(41, 23);
            this.buttonTypeDataDelete.TabIndex = 6;
            this.buttonTypeDataDelete.Text = "Del";
            this.buttonTypeDataDelete.UseVisualStyleBackColor = true;
            this.buttonTypeDataDelete.Click += new System.EventHandler(this.buttonTypeDataDelete_Click);
            // 
            // buttonTypeDataDown
            // 
            this.buttonTypeDataDown.Location = new System.Drawing.Point(216, 225);
            this.buttonTypeDataDown.Name = "buttonTypeDataDown";
            this.buttonTypeDataDown.Size = new System.Drawing.Size(46, 23);
            this.buttonTypeDataDown.TabIndex = 5;
            this.buttonTypeDataDown.Text = "Down";
            this.buttonTypeDataDown.UseVisualStyleBackColor = true;
            this.buttonTypeDataDown.Click += new System.EventHandler(this.buttonTypeDataDown_Click);
            // 
            // buttonTypeDataUp
            // 
            this.buttonTypeDataUp.Location = new System.Drawing.Point(164, 225);
            this.buttonTypeDataUp.Name = "buttonTypeDataUp";
            this.buttonTypeDataUp.Size = new System.Drawing.Size(46, 23);
            this.buttonTypeDataUp.TabIndex = 4;
            this.buttonTypeDataUp.Text = "Up";
            this.buttonTypeDataUp.UseVisualStyleBackColor = true;
            this.buttonTypeDataUp.Click += new System.EventHandler(this.buttonTypeDataUp_Click);
            // 
            // lbTypeData
            // 
            this.lbTypeData.FormattingEnabled = true;
            this.lbTypeData.Location = new System.Drawing.Point(164, 20);
            this.lbTypeData.Name = "lbTypeData";
            this.lbTypeData.ScrollAlwaysVisible = true;
            this.lbTypeData.Size = new System.Drawing.Size(145, 199);
            this.lbTypeData.TabIndex = 3;
            // 
            // buttonFieldCopy
            // 
            this.buttonFieldCopy.Location = new System.Drawing.Point(83, 225);
            this.buttonFieldCopy.Name = "buttonFieldCopy";
            this.buttonFieldCopy.Size = new System.Drawing.Size(69, 23);
            this.buttonFieldCopy.TabIndex = 2;
            this.buttonFieldCopy.Text = ">>";
            this.buttonFieldCopy.UseVisualStyleBackColor = true;
            this.buttonFieldCopy.Click += new System.EventHandler(this.buttonFieldCopy_Click);
            // 
            // buttonFieldDelete
            // 
            this.buttonFieldDelete.Location = new System.Drawing.Point(7, 225);
            this.buttonFieldDelete.Name = "buttonFieldDelete";
            this.buttonFieldDelete.Size = new System.Drawing.Size(70, 23);
            this.buttonFieldDelete.TabIndex = 1;
            this.buttonFieldDelete.Text = "Delete";
            this.buttonFieldDelete.UseVisualStyleBackColor = true;
            this.buttonFieldDelete.Click += new System.EventHandler(this.buttonFieldDelete_Click);
            // 
            // lbFieldNames
            // 
            this.lbFieldNames.FormattingEnabled = true;
            this.lbFieldNames.Location = new System.Drawing.Point(7, 20);
            this.lbFieldNames.Name = "lbFieldNames";
            this.lbFieldNames.ScrollAlwaysVisible = true;
            this.lbFieldNames.Size = new System.Drawing.Size(145, 199);
            this.lbFieldNames.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbAgentIDMe);
            this.groupBox3.Controls.Add(this.cbIDRandom);
            this.groupBox3.Controls.Add(this.tbColorR);
            this.groupBox3.Controls.Add(this.tbColorG);
            this.groupBox3.Controls.Add(this.tbColorB);
            this.groupBox3.Controls.Add(this.tbColorA);
            this.groupBox3.Controls.Add(this.tbDuration);
            this.groupBox3.Controls.Add(this.cbType);
            this.groupBox3.Controls.Add(this.tbAgentID);
            this.groupBox3.Controls.Add(this.tbID);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(13, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 153);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "EffectBlock Misc";
            // 
            // cbAgentIDMe
            // 
            this.cbAgentIDMe.AutoSize = true;
            this.cbAgentIDMe.Checked = true;
            this.cbAgentIDMe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbAgentIDMe.Location = new System.Drawing.Point(58, 49);
            this.cbAgentIDMe.Name = "cbAgentIDMe";
            this.cbAgentIDMe.Size = new System.Drawing.Size(41, 17);
            this.cbAgentIDMe.TabIndex = 14;
            this.cbAgentIDMe.Text = "Me";
            this.cbAgentIDMe.UseVisualStyleBackColor = true;
            // 
            // cbIDRandom
            // 
            this.cbIDRandom.AutoSize = true;
            this.cbIDRandom.Checked = true;
            this.cbIDRandom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIDRandom.Location = new System.Drawing.Point(30, 23);
            this.cbIDRandom.Name = "cbIDRandom";
            this.cbIDRandom.Size = new System.Drawing.Size(66, 17);
            this.cbIDRandom.TabIndex = 13;
            this.cbIDRandom.Text = "Random";
            this.cbIDRandom.UseVisualStyleBackColor = true;
            // 
            // tbColorR
            // 
            this.tbColorR.Location = new System.Drawing.Point(115, 127);
            this.tbColorR.Name = "tbColorR";
            this.tbColorR.Size = new System.Drawing.Size(45, 20);
            this.tbColorR.TabIndex = 12;
            this.tbColorR.Text = "255";
            this.tbColorR.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbColorG
            // 
            this.tbColorG.Location = new System.Drawing.Point(166, 127);
            this.tbColorG.Name = "tbColorG";
            this.tbColorG.Size = new System.Drawing.Size(45, 20);
            this.tbColorG.TabIndex = 11;
            this.tbColorG.Text = "255";
            this.tbColorG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbColorB
            // 
            this.tbColorB.Location = new System.Drawing.Point(217, 127);
            this.tbColorB.Name = "tbColorB";
            this.tbColorB.Size = new System.Drawing.Size(45, 20);
            this.tbColorB.TabIndex = 10;
            this.tbColorB.Text = "255";
            this.tbColorB.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbColorA
            // 
            this.tbColorA.Location = new System.Drawing.Point(268, 127);
            this.tbColorA.Name = "tbColorA";
            this.tbColorA.Size = new System.Drawing.Size(45, 20);
            this.tbColorA.TabIndex = 9;
            this.tbColorA.Text = "255";
            this.tbColorA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbDuration
            // 
            this.tbDuration.Location = new System.Drawing.Point(201, 100);
            this.tbDuration.Name = "tbDuration";
            this.tbDuration.Size = new System.Drawing.Size(113, 20);
            this.tbDuration.TabIndex = 8;
            this.tbDuration.Text = "10";
            this.tbDuration.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cbType
            // 
            this.cbType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbType.FormattingEnabled = true;
            this.cbType.Items.AddRange(new object[]
            {
                "Text",
                "Icon",
                "Connector",
                "FlexibleObject",
                "AnimalControls",
                "AnimationObject",
                "Cloth",
                "Beam",
                "Glow",
                "Point",
                "Trail",
                "Sphere",
                "Spiral",
                "Edit",
                "LookAt",
                "PointAt"
            });
            this.cbType.Location = new System.Drawing.Point(201, 73);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(113, 21);
            this.cbType.TabIndex = 7;
            this.cbType.Text = "Type...";
            // 
            // tbAgentID
            // 
            this.tbAgentID.Location = new System.Drawing.Point(102, 47);
            this.tbAgentID.Name = "tbAgentID";
            this.tbAgentID.Size = new System.Drawing.Size(212, 20);
            this.tbAgentID.TabIndex = 6;
            this.tbAgentID.Text = "00000000-0000-0000-0000-000000000000";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(102, 21);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(212, 20);
            this.tbID.TabIndex = 5;
            this.tbID.Text = "00000000-0000-0000-0000-000000000000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Color";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 103);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Duration";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Type";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "AgentID";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            // 
            // buttonSendPacket
            // 
            this.buttonSendPacket.Location = new System.Drawing.Point(13, 525);
            this.buttonSendPacket.Name = "buttonSendPacket";
            this.buttonSendPacket.Size = new System.Drawing.Size(320, 23);
            this.buttonSendPacket.TabIndex = 4;
            this.buttonSendPacket.Text = "Send Packet";
            this.buttonSendPacket.UseVisualStyleBackColor = true;
            this.buttonSendPacket.Click += new System.EventHandler(this.buttonSendPacket_Click);
            // 
            // frmCrasher
            // 
            this.ClientSize = new System.Drawing.Size(345, 560);
            this.Controls.Add(this.buttonSendPacket);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCrasher";
            this.Text = "ViewerEffectTypes";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }



        private void RefreshCustomFields()
        {
            this.lbFieldNames.Items.Clear();
            foreach (KeyValuePair<string, byte[]> pair in this.CustomFields)
            {
                this.lbFieldNames.Items.Add(pair.Key);
            }
        }

        private void RefreshTypeDataFields()
        {
            string str;
            byte[] bytes;
            this.lbTypeData.Items.Clear();
            int num = 0;
            foreach (KeyValuePair<int, object[]> pair in this.TypeDataFields)
            {
                str = (string)pair.Value[0];
                if (str == "*Self.GlobalPosition")
                {
                    bytes = this.client.Self.GlobalPosition.GetBytes();
                }
                else if (str == "*Self.GlobalPostion+<0,0,2>")
                {
                    Vector3d vectord = new Vector3d(this.client.Self.GlobalPosition.X, this.client.Self.GlobalPosition.Y, this.client.Self.GlobalPosition.Z + 2.0);
                    bytes = vectord.GetBytes();
                }
                else if (str == "*Vector3d.Zero")
                {
                    bytes = Vector3d.Zero.GetBytes();
                }
                else if (str == "*Self.AgentID")
                {
                    bytes = this.client.Self.AgentID.GetBytes();
                }
                else if (str == "*UUID.Zero")
                {
                    bytes = UUID.Zero.GetBytes();
                }
                else
                {
                    bytes = (byte[])pair.Value[1];
                }
                num += bytes.Length;
                this.lbTypeData.Items.Add(str);
            }
            this.MyTypeData = new byte[num];
            int dstOffset = 0;
            foreach (KeyValuePair<int, object[]> pair in this.TypeDataFields)
            {
                str = (string)pair.Value[0];
                if (str == "*Self.GlobalPosition")
                {
                    bytes = this.client.Self.GlobalPosition.GetBytes();
                }
                else if (str == "*Self.GlobalPostion+<0,0,2>")
                {
                    bytes = new Vector3d(this.client.Self.GlobalPosition.X, this.client.Self.GlobalPosition.Y, this.client.Self.GlobalPosition.Z + 2.0).GetBytes();
                }
                else if (str == "*Vector3d.Zero")
                {
                    bytes = Vector3d.Zero.GetBytes();
                }
                else if (str == "*Self.AgentID")
                {
                    bytes = this.client.Self.AgentID.GetBytes();
                }
                else if (str == "*UUID.Zero")
                {
                    bytes = UUID.Zero.GetBytes();
                }
                else
                {
                    bytes = (byte[])pair.Value[1];
                }
                Buffer.BlockCopy(bytes, 0, this.MyTypeData, dstOffset, bytes.Length);
                dstOffset += bytes.Length;
            }
        }

        // Nested Types
        private delegate void BoolInvoker(bool param);
    }

 


}
