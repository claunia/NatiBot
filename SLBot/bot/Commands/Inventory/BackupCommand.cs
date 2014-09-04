/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : BackupCommand.cs
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
    using System.ComponentModel;
    using System.IO;
    using System.Text;
    using System.Threading;
    using OpenMetaverse.Assets;

    public class BackupCommand : Command
    {
        private BackgroundWorker BackupWorker;
        private List<QueuedDownloadInfo> CurrentDownloads = new List<QueuedDownloadInfo>(10);
        private const int MAX_TRANSFERS = 10;
        private Queue<QueuedDownloadInfo> PendingDownloads = new Queue<QueuedDownloadInfo>();
        private BackgroundWorker QueueWorker;
        private int TextItemErrors;
        private int TextItemsFound;
        private int TextItemsTransferred;

        #region Properties

        /// <summary>
        /// true if either of the background threads is running
        /// </summary>
        private bool BackgroundBackupRunning
        {
            get { return InventoryWalkerRunning || QueueRunnerRunning; }
        }

        /// <summary>
        /// true if the thread walking inventory is running
        /// </summary>
        private bool InventoryWalkerRunning
        {
            get { return BackupWorker != null; }
        }

        /// <summary>
        /// true if the thread feeding the queue to the server is running
        /// </summary>
        private bool QueueRunnerRunning
        {
            get { return QueueWorker != null; }
        }

        /// <summary>
        /// returns a string summarizing activity
        /// </summary>
        /// <returns></returns>
        private string BackgroundBackupStatus
        {
            get
            {
                StringBuilder sbResult = new StringBuilder();
                sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Backup.Running"), Name, BoolToNot(BackgroundBackupRunning));
                if (TextItemErrors != 0 || TextItemsFound != 0 || TextItemsTransferred != 0)
                {
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Backup.Walker"),
                        Name, BoolToNot(InventoryWalkerRunning), TextItemsFound);
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Backup.Transfer"),
                        Name, BoolToNot(QueueRunnerRunning), TextItemsTransferred, TextItemErrors);
                    sbResult.AppendFormat(bot.Localization.clResourceManager.getText("Commands.Backup.Queue"),
                        Name, PendingDownloads.Count, CurrentDownloads.Count);
                }
                return sbResult.ToString();
            }
        }

        #endregion Properties

        public BackupCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "backup";
            base.Description = bot.Localization.clResourceManager.getText("Commands.Backup.Description") + " " + String.Format(bot.Localization.clResourceManager.getText("Commands.Backup.Usage"), Name);
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            Program.NBStats.AddStatData(String.Format("{0}: {1} backing up all inventory.", DateTime.Now.ToString(), Client));

            StringBuilder sbResult = new StringBuilder();

            if (args.Length == 1 && args[0] == "status")
            {
                return BackgroundBackupStatus;
            }
            else if (args.Length == 1 && args[0] == "abort")
            {
                if (!BackgroundBackupRunning)
                    return BackgroundBackupStatus;

                BackupWorker.CancelAsync();
                QueueWorker.CancelAsync();

                Thread.Sleep(500);

                // check status
                return BackgroundBackupStatus;
            }
            else if (args.Length != 2)
            {
                return String.Format(bot.Localization.clResourceManager.getText("Commands.Backup.Usage"), Name);
            }
            else if (BackgroundBackupRunning)
            {
                return BackgroundBackupStatus;
            }

            QueueWorker = new BackgroundWorker();
            QueueWorker.WorkerSupportsCancellation = true;
            QueueWorker.DoWork += new DoWorkEventHandler(bwQueueRunner_DoWork);
            QueueWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwQueueRunner_RunWorkerCompleted);

            QueueWorker.RunWorkerAsync();

            BackupWorker = new BackgroundWorker();
            BackupWorker.WorkerSupportsCancellation = true;
            BackupWorker.DoWork += new DoWorkEventHandler(bwBackup_DoWork);
            BackupWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwBackup_RunWorkerCompleted);

            BackupWorker.RunWorkerAsync(args);
            return bot.Localization.clResourceManager.getText("Commands.Backup.Started");
        }

        void bwQueueRunner_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            QueueWorker = null;
            bot.Console.WriteLine(BackgroundBackupStatus);
        }

        void bwQueueRunner_DoWork(object sender, DoWorkEventArgs e)
        {
            TextItemErrors = TextItemsTransferred = 0;

            while (QueueWorker.CancellationPending == false)
            {
                // have any timed out?
                if (CurrentDownloads.Count > 0)
                {
                    lock (CurrentDownloads)
                    {
                        foreach (QueuedDownloadInfo qdi in CurrentDownloads)
                        {
                            if ((qdi.WhenRequested + TimeSpan.FromSeconds(60)) < DateTime.Now)
                            {
                                bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.Timeout"), Name, qdi.AssetID.ToString());
                                // submit request again
                                if (qdi.Type == AssetType.Notecard || qdi.Type == AssetType.LSLText || qdi.Type == AssetType.Texture)
                                {
                                    if (qdi.Type == AssetType.Texture)
                                    {
                                        Client.Assets.RequestImage(qdi.AssetID, Assets_OnImageReceived);
                                    }
                                    else
                                    {
                                        Client.Assets.RequestInventoryAsset(
                                            qdi.AssetID, qdi.ItemID, qdi.TaskID, qdi.OwnerID, qdi.Type, true, Assets_OnAssetReceived);
                                    }
                                }
                                else
                                {
                                    Client.Assets.RequestAsset(qdi.AssetID, qdi.Type, true, Assets_OnAssetReceived);
                                }
                                qdi.WhenRequested = DateTime.Now;
                                qdi.IsRequested = true;
                            }
                        }
                    }
                }

                if (PendingDownloads.Count != 0)
                {
                    // room in the server queue?
                    if (CurrentDownloads.Count < MAX_TRANSFERS)
                    {
                        // yes
                        QueuedDownloadInfo qdi = PendingDownloads.Dequeue();
                        qdi.WhenRequested = DateTime.Now;
                        qdi.IsRequested = true;
                        if (qdi.Type == AssetType.Notecard || qdi.Type == AssetType.LSLText || qdi.Type == AssetType.Texture)
                        {
                            if (qdi.Type == AssetType.Texture)
                            {
                                Client.Assets.RequestImage(qdi.AssetID, Assets_OnImageReceived);
                            }
                            else
                            {
                                Client.Assets.RequestInventoryAsset(
                                    qdi.AssetID, qdi.ItemID, qdi.TaskID, qdi.OwnerID, qdi.Type, true, Assets_OnAssetReceived);
                            }
                        }
                        else
                        {
                            Client.Assets.RequestAsset(qdi.AssetID, qdi.Type, true, Assets_OnAssetReceived);
                        }

                        lock (CurrentDownloads)
                            CurrentDownloads.Add(qdi);
                    }
                }

                if (CurrentDownloads.Count == 0 && PendingDownloads.Count == 0 && BackupWorker == null)
                {
                    bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.AllDone"), Name);
                    return;
                }

                Thread.Sleep(100);
            }
        }

        void bwBackup_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.WalkingDone"), Name);
            BackupWorker = null;
        }

        private void bwBackup_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] args;

            TextItemsFound = 0;

            args = (string[])e.Argument;

            lock (CurrentDownloads)
                CurrentDownloads.Clear();

            // FIXME:
            //Client.Inventory.RequestFolderContents(Client.Inventory.Store.RootFolder.UUID, Client.Self.AgentID, 
            //    true, true, false, InventorySortOrder.ByName);

            DirectoryInfo di = new DirectoryInfo(args[1]);

            // recurse on the root folder into the entire inventory
            BackupFolder(Client.Inventory.Store.RootNode, di.FullName);
        }

        /// <summary>
        /// BackupFolder - recurse through the inventory nodes sending scripts and notecards to the transfer queue
        /// </summary>
        /// <param name="folder">The current leaf in the inventory tree</param>
        /// <param name="sPathSoFar">path so far, in the form @"c:\here" -- this needs to be "clean" for the current filesystem</param>
        private void BackupFolder(InventoryNode folder, string sPathSoFar)
        {
            StringBuilder sbRequests = new StringBuilder();

            // FIXME:
            //Client.Inventory.RequestFolderContents(folder.Data.UUID, Client.Self.AgentID, true, true, false, 
            //    InventorySortOrder.ByName);

            // first scan this folder for text
            foreach (InventoryNode iNode in folder.Nodes.Values)
            {
                if (BackupWorker.CancellationPending)
                    return;
                if (iNode.Data is OpenMetaverse.InventoryItem)
                {
                    InventoryItem ii = iNode.Data as InventoryItem;
                    
                    string sExtension;
                    string sPath;

                    switch (ii.AssetType)
                    {
                        case AssetType.Animation:
                            sExtension = ".animatn";
                            break;
                        case AssetType.Bodypart:
                            sExtension = ".bodypart";
                            break;
                        case AssetType.CallingCard:
                            continue; // They really don't exist. Are not backable.
                        case AssetType.Clothing:
                            sExtension = ".clothing";
                            break;
                        case AssetType.Folder:
                            continue;
                        case AssetType.Gesture:
                            sExtension = ".gesture";
                            break;
                        case AssetType.ImageJPEG:
                            sExtension = ".jpg";
                            break;
                        case AssetType.ImageTGA:
                            sExtension = ".tga";
                            break;
                        case AssetType.Landmark:
                            sExtension = ".landmark";
                            break;
                        case AssetType.LostAndFoundFolder:
                            continue;
                        case AssetType.LSLBytecode:
                            sExtension = ".lso";
                            break;
                        case AssetType.LSLText:
                            if ((ii.Permissions.OwnerMask & PermissionMask.Copy) == PermissionMask.None || (ii.Permissions.OwnerMask & PermissionMask.Modify) == PermissionMask.None)
                            {
                                continue; // Nocopy scripts are not readable (SecondLife Jira VWR-5238). Nomod scripts will never be.
                            }
                            else
                            {
                                sExtension = ".lsl";
                                break;
                            }
                        case AssetType.Notecard:
                            if ((ii.Permissions.OwnerMask & PermissionMask.Copy) == PermissionMask.None)
                            {
                                continue; // Nocopy notecards are not readable (SecondLife Jira VWR-5238)
                            }
                            else
                            {
                                sExtension = ".notecard";
                                break;
                            }
                        case AssetType.Object:
                            /*sExtension=".object";
                            break;*/
                            continue; // They cannot be copied from the inventory, they must be rezzed
                        case AssetType.RootFolder:
                            continue;
                        case AssetType.Simstate:
                            sExtension = ".simstate";
                            break;
                        case AssetType.SnapshotFolder:
                            continue;
                        case AssetType.Sound:
                            sExtension = ".ogg";
                            break;
                        case AssetType.SoundWAV:
                            sExtension = ".wav";
                            break;
                        case AssetType.Texture:
                            sExtension = ".jp2";
                            break;
                        case AssetType.TextureTGA:
                            sExtension = ".tga";
                            break;
                        case AssetType.TrashFolder:
                            continue;
                        case AssetType.Unknown:
                        default:
                            sExtension = ".unk";
                            break;
                    }

                    // make the output file
                    sPath = sPathSoFar + @"\" + MakeValid(ii.Name.Trim()) + sExtension;

                    // create the new qdi
                    QueuedDownloadInfo qdi = new QueuedDownloadInfo(sPath, ii.AssetUUID, iNode.Data.UUID, UUID.Zero,
                                                 Client.Self.AgentID, ii.AssetType);

                    // add it to the queue
                    lock (PendingDownloads)
                    {
                        TextItemsFound++;
                        PendingDownloads.Enqueue(qdi);
                    }
                }
            }

            // now run any subfolders
            foreach (InventoryNode i in folder.Nodes.Values)
            {
                if (BackupWorker.CancellationPending)
                    return;
                else if (i.Data is OpenMetaverse.InventoryFolder)
                    BackupFolder(i, sPathSoFar + @"\" + MakeValid(i.Data.Name.Trim()));
            }
        }

        private string MakeValid(string path)
        {
            string FinalName;

            //FinalName = path.Replace(" ", "_"); // This is not needed for exporting the inventory
            FinalName = path.Replace(":", ";");
            FinalName = FinalName.Replace("*", "+");
            FinalName = FinalName.Replace("|", "I");
            FinalName = FinalName.Replace("\\", "[");
            FinalName = FinalName.Replace("/", "]");
            FinalName = FinalName.Replace("?", "¿");
            FinalName = FinalName.Replace(">", "}");
            FinalName = FinalName.Replace("<", "{");
            FinalName = FinalName.Replace("\"", "'");
            FinalName = FinalName.Replace("\n", " ");

            return FinalName;
        }

        private void Assets_OnAssetReceived(AssetDownload asset, Asset blah)
        {
            lock (CurrentDownloads)
            {
                // see if we have this in our transfer list
                QueuedDownloadInfo r = CurrentDownloads.Find(delegate(QueuedDownloadInfo q)
                {
                    return q.AssetID == asset.AssetID;
                });

                if (r != null && r.AssetID == asset.AssetID)
                {
                    if (asset.Success)
                    {
                        // create the directory to put this in
                        Directory.CreateDirectory(Path.GetDirectoryName(r.FileName));

                        // write out the file
                        File.WriteAllBytes(r.FileName, asset.AssetData);
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.Wrote"), Name, r.FileName);
                        TextItemsTransferred++;
                    }
                    else
                    {
                        TextItemErrors++;
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.Failed"), Name, r.FileName,
                            r.AssetID.ToString(), asset.Status.ToString());
                    }

                    // remove the entry
                    CurrentDownloads.Remove(r);
                }
            }
        }

        /// <summary>
        /// returns blank or "not" if false
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string BoolToNot(bool b)
        {
            return b ? String.Empty : bot.Localization.clResourceManager.getText("Commands.Backup.Not");
        }

        private void Assets_OnImageReceived(TextureRequestState state, AssetTexture asset)
        {
            lock (CurrentDownloads)
            {
                // see if we have this in our transfer list
                QueuedDownloadInfo r = CurrentDownloads.Find(delegate(QueuedDownloadInfo q)
                {
                    return q.AssetID == asset.AssetID;
                });

                if (r != null && r.AssetID == asset.AssetID)
                {
                    if (asset != null/* && asset.Decode()*/)
                    {
                        // create the directory to put this in
                        Directory.CreateDirectory(Path.GetDirectoryName(r.FileName));

                        // write out the file
                        File.WriteAllBytes(r.FileName, asset.AssetData);
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.Wrote"), Name, r.FileName);

                        // This, even being a desiderable feature, timeouts the bot and it gets ejected from SL.
                        /*File.WriteAllBytes(r.FileName.Substring(0, r.FileName.Length - 4) + ".tga", asset.Image.ExportTGA());
                        bot.Console.WriteLine("Wrote " + r.FileName.Substring(0, r.FileName.Length - 4) + ".tga");
                        Logger.DebugLog(Name + " Wrote: " + r.FileName.Substring(0, r.FileName.Length - 4) + ".tga", Client);*/
                        TextItemsTransferred++;
                    }
                    else
                    {
                        TextItemErrors++;
                        bot.Console.WriteLine(bot.Localization.clResourceManager.getText("Commands.Backup.Failed"), Name, r.FileName,
                            r.AssetID.ToString(), bot.Localization.clResourceManager.getText("Commands.Backup.Unknown"));
                    }

                    // remove the entry
                    CurrentDownloads.Remove(r);
                }
            }
        }
    }
}

