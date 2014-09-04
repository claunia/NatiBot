/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ListColumnSorter.cs
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
Copyright (C) 2007-2009 openmetaverse.org
****************************************************************************/

/*
 * Copyright (c) 2007-2009, openmetaverse.org
 * All rights reserved.
 *
 * - Redistribution and use in source and binary forms, with or without 
 *   modification, are permitted provided that the following conditions are met:
 *
 * - Redistributions of source code must retain the above copyright notice, this
 *   list of conditions and the following disclaimer.
 * - Neither the name of the openmetaverse.org nor the names 
 *   of its contributors may be used to endorse or promote products derived from
 *   this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 * POSSIBILITY OF SUCH DAMAGE.
 */

using System.Collections;
using System.Windows.Forms;

namespace bot.GUI
{

    class ListColumnSorter : IComparer
    {
        public bool Ascending = true;
        public int SortColumn = 0;

        public int Compare(object a, object b)
        {
            ListViewItem itemA = (ListViewItem)a;
            ListViewItem itemB = (ListViewItem)b;

            if (SortColumn == 1)
            {
                int valueA = itemB.SubItems.Count > 1 ? int.Parse(itemA.SubItems[1].Text.Replace("m", "").Replace("--", "0")) : 0;
                int valueB = itemB.SubItems.Count > 1 ? int.Parse(itemB.SubItems[1].Text.Replace("m", "").Replace("--", "0")) : 0;
                if (Ascending)
                {
                    if (valueA == valueB)
                        return 0;
                    return valueA < valueB ? -1 : 1;
                }
                else
                {
                    if (valueA == valueB)
                        return 0;
                    return valueA < valueB ? 1 : -1;
                }
            }
            else
            {
                if (Ascending)
                    return string.Compare(itemA.Text, itemB.Text);
                else
                    return -string.Compare(itemA.Text, itemB.Text);
            }
        }
    }

    class ListColumnSorterNormal : IComparer
    {
        public bool Ascending = true;
        public int SortColumn = 0;

        public int Compare(object a, object b)
        {
            ListViewItem itemA = (ListViewItem)a;
            ListViewItem itemB = (ListViewItem)b;

            if (Ascending)
                return string.Compare(itemA.Text, itemB.Text);
            else
                return -string.Compare(itemA.Text, itemB.Text);
        }
    }
}
