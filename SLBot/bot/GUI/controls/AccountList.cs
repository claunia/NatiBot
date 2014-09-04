/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : AccountList.cs
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
using OpenMetaverse;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace bot.GUI
{
    /// <summary>
    /// ListView GUI component for viewing a client's nearby avatars list
    /// </summary>
    public class AccountList : ListView
    {
        private ListColumnSorterNormal _ColumnSorter = new ListColumnSorterNormal();

        /// <summary>
        /// TreeView control for an unspecified client's nearby avatar list
        /// </summary>
        public AccountList()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);

            ColumnHeader header1 = this.Columns.Add(bot.Localization.clResourceManager.getText("frmMain.ClName"));
            header1.Width = 170;

            ColumnHeader header2 = this.Columns.Add(bot.Localization.clResourceManager.getText("frmMain.ClStatus"));
            header2.Width = 118;

            ColumnHeader header3 = this.Columns.Add(bot.Localization.clResourceManager.getText("frmMain.clMaster"));
            header3.Width = 170;

            ColumnHeader header4 = this.Columns.Add(bot.Localization.clResourceManager.getText("frmMain.clLocation"));
            header4.Width = 188;

            ColumnHeader header5 = this.Columns.Add(bot.Localization.clResourceManager.getText("frmMain.clMoney"));
            header5.Width = 78;

            this.MultiSelect = false;
            this.SelectedIndexChanged += new EventHandler(AvatarList_SelectedIndexChanged);

            _ColumnSorter.SortColumn = 1;
            this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;

            this.DoubleBuffered = true;
            this.FullRowSelect = true;
            this.ListViewItemSorter = _ColumnSorter;
            this.View = View.Details;
            this.ColumnClick += new ColumnClickEventHandler(AvatarList_ColumnClick);
            this.DoubleClick += new EventHandler(AvatarList_DoubleClick);
        }

        void AvatarList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        void AvatarList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            _ColumnSorter.SortColumn = e.Column;
            if ((_ColumnSorter.Ascending = (this.Sorting == SortOrder.Ascending))) this.Sorting = SortOrder.Descending;
            else this.Sorting = SortOrder.Ascending;
            this.ListViewItemSorter = _ColumnSorter;
        }

        void AvatarList_DoubleClick(object sender, EventArgs e)
        {
            
        }

    }
}
