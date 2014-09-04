/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : QueuedDownloadInfo.cs
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
Copyright (C) 2007-2010 openmetaverse.org
****************************************************************************/
namespace bot.Commands
{
    using OpenMetaverse;
    using System;

    public class QueuedDownloadInfo
    {
        public UUID TransferID;
        public UUID AssetID;
        public UUID ItemID;
        public UUID TaskID;
        public UUID OwnerID;
        public AssetType Type;
        public string FileName;
        public DateTime WhenRequested;
        public bool IsRequested;

        public QueuedDownloadInfo(string file, UUID asset, UUID item, UUID task, UUID owner, AssetType type)
        {
            FileName = file;
            AssetID = asset;
            ItemID = item;
            TaskID = task;
            OwnerID = owner;
            Type = type;
            WhenRequested = DateTime.Now;
            IsRequested = false;
        }
    }
}

