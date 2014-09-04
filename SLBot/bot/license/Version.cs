/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : Version.cs
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
/*
 * Changelog:
 * 
 * Rev 1:
 *          Added the export button to the contextual menu on the objects form, for easily exporting.
 *          Added the export_all button to the objects form. Still buggy!
 *          Added "animate" command.
 *          Added "sendtp" command.
 *          Added "version" command.
 *          Added OpenLife login URI.
 *          Partially translated to Spanish!
 *          Modified "objectinventory" so it shown UUID of items
 * Rev 2:
 *          Modified "friends" so it shows UUID of friends
 * Rev 3:
 *          Corrected a bug in "import" that did not set permissions on objects with only one prim.
 * Rev 4:
 *          Added "attachmentsuuid" that list attachments of an avatar by UUID.
 * Rev 5:
 *          Modified "attachments" so it shows information in IM and console, not in log.
 *          Upped to 0.5.5
 * Rev 6:
 *          Added "dumpattachments", to export all attachments. Export is in CopyXML under a folder with the user UUID and date of exporting start.
 * Rev 7:
 *          Added "dumpattachment", to export ONLY one attachment.
 *          Upped to 0.6
 * Rev 8:
 *          Corrected a bug in folder dating system in dumpattachment and dumpattachments.
 *          Added list of possible attachment places when dumpattachment is sent with incorrect arguments.
 *          Detect that requested attachment is a valid one.
 * Rev 9:
 *          Modified the way "dumpattachments" work. Still fails, but at least let you know what copied when this happens.
 * Rev 10:
 *          Modified "exportparticles" so it dumps to a .lsl file all particle systems on the requested object.
 * Rev 11:
 *          Heavily modified to work with libSL trunk revision 2218
 *          This means, some commands stopped to work, others works different, new commands available, lot of changes, lots of hours and headaches!!!
 *          In exporting the angel sculture, 101 textures exported? Something is not working in export
 *          It is exporting now to bot's folder, not CopyXML or textures.
 *          Importing also takes default in bot's root folder.
 *          Command "objectinventory" always timeout :(.
 *          Command "exportparticles" stopped working on objects with multiple particle systems.
 *          Command "backuptext" still not working.
 *          Command "mapfriend" requires more testing, seems to not work.
 *          Command "appearence" does ever worked?
 *          Command "gridmap" does something?
 *          Command "wear" does ever worked?
 *          Command "attachmentsuuid" counts attachs, but does not list them!
 *          It seems LOT SLOWER!!!
 *          Now it is able to download a request animation, just by UUID. (No need to be playing nearby)
 *          Most commands lost translation :(
 *          Version dumped to 0.6.9 Rev 54. 0.7.0 will come when above bugs are corrected again!
 * Rev 12:
 *          Corrected "export" command, now works as before!
 * Rev 13:
 *          Corrected "import" command.
 *          Changed "CopyXML" folder to more appropiate "objects" folder.
 * Rev 14:
 *          Command "objectinventory" never stopped working, was only lag!
 *          Command "exportparticles" was storing all particle systems in the same lsl file. Corrected.
 * Rev 15:
 *          Commands "attachments" and "attachmentsuuid" now list on IM console.
 * Rev 17:
 *          Added "avatarinfo" command that shows information about a nearby avatar.
 * Rev 18:
 *          Added "ls" command to list contents of current inventory directory.
 * Rev 19:
 *          Added "cd" command to change current inventory directory.
 *          Modified "ls" command so with '-l' option it says creation date/time and asset type of contents, and works with subfolders (previously it did not without first using "i" command to populate).
 * Rev 20:
 *          Added "createnotecard" command to create a Notecard from a local text file.
 * Rev 21:
 *          Added "give" command to give inventory items to an avatar.
 * Rev 22:
 *          Now the "avatarinfo" command shows not only textures but profile text and images. Interests, groups, and statistics, are on the way.
 * Rev 23:
 *          Now the "avatarinfo" command gets also interests and groups. Statistics are disabled by Linden Lab because they are deprecated so apparently no way to get them.
 * Rev 25:
 *          Modified "dumpoutfit" command to work with libOMV API (RequestImage()) changes in SVN r2227.
 * Rev 26:
 *          Added "rmdir" command to move a folder from the inventory to the trash.
 * Rev 27:
 *          Added "viewnote" command to dump contents of an inventory's notecard to the console/IM.
 * Rev 28:
 *          Modified "createnotecard" command to send the Notecard to the master.
 *          Added "uploadimage" command to upload an image/texture and send it to the master.
 * Rev 29:
 *          Modified "createnotecard" and "uploadimage" commands as accent in "día" is not correctly handled by asset server in description of uploaded item.
 *          Added "upload" command to upload <almost> anything that is supported for the asset server, detecting the correct type by extension, as following:
 *              .animatn for Animation. (Costs 10L on AGNI)
 *              .bodypart for Shape, Skin, Eyes and Hair.
 *              .gesture for Gesture.
 *              .clothing for Shirt, Pants, Shoes, Socks, Jacket, Skirt, Gloves, Undershirt and Underpants.
 *              .jpg, .tga, .jp2 and .j2c for Textures. (Costs 10L on AGNI)
 *              .notecard for Notecard.
 *              .landmark for LandMark.
 *              .ogg for Sound (Vorbis). (Costs 10L on AGNI)
 *              .lsl for LSL2 Script (as text).
 *              .lso for LSL2 Script (as bytecode).
 *          Old versions of the items, or, some of them, will fail without solution.
 * Rev 30:
 *          Corrected a NullException in "parcelinfo" command.
 *          Modified "parcelinfo" timeout from 30sec. to 60sec.
 *          Added "parceldetails" command, that with a given parcel ID, shows ALL information about that parcel.
 * Rev 31:
 *          Added "primowners" command, to show prims count and their owners in a parcel. It needs permissions.
 * Rev 32:
 *          Added "selectobjects" command, to show detailed information about the prims owned by a determined avatar, in the specified parcel.
 * Rev 33:
 *          Added "forward", "back", "left" and "right" commands to move the bot.
 * Rev 34:
 *          Added "downloadtexture" command to download a texture from the asset server given the UUID. Still does not convert to Targa format.
 * Rev 35:
 *          Added "script" command to take a list of commands to execute from a given text file.
 * Rev 36:
 *          Modified "upload" command to catch exceptions when loading the file.
 *          Corrected animation extension on auto-download-from-asset event from .animtn to .animatn.
 *          Removed "test" command, as it was, just a test.
 *          Added "downloadanimation" to download an animation from the asset server given its UUID. (The one that appears with Animation Info enabled on the official client)
 *          Version bumped to 0.7 (20 new commands). YUPIIIIIIIIIII!!!!!!!!
 * Rev 40:
 *          Added authorization checkout before loading the bot.
 *          Removed the fucking "Running webserver" message!
 *          Updated to libomv r2382
 * Rev 42:
 *          Updated to libomv r2818
 *          As part of this update the texture downloading system has been reworked.
 *          Advantages, supposedly, speed and less failures.
 *          Disadvantages, no progress indicator on bot console.
 *          Textures are downloaded to the textures folder, when using export or downloadtexture commands.
 *          Textures of outfits are downloaded to a outfits folder, in a subfolder containing the avatar's name.
 *          Textures of attachments are downloaded to a textures subfolder inside the UUID's folder where attachments were always stored.
 *          This should stop confusion with downloaded textures.
 *          This is a minor revision, major changes awaiting for NatiBot 0.8.0 rev 80.
 * Rev 43:
 *          Corrected dumpattachment, dumpattachments, export commands, and the GUI button objects.
 *          The GUI button now works, but is, unreliable like always.
 * Rev 50:
 *          Version bumped to 0.8
 *          Updated to libomv r2977
 *          Corrected exporting objects WITHOUT texture (yeah, I've found ONE)
 *          Created a new objects window from scratch with the following features:
 *           - List of UUID, location and name, sortable at user's wishes.
 *           - Automatic autoupdate for taking names and new objects every 10 seconds.
 *           - That option can be disabled.
 *           - Export button is able to export ANY object no matter what name it has (even if it is unknown).
 *           - You can choose what to copy, the UUID, location, or name, to the clipboard, using right click or CTRL+C.
 *           - You can export more than one selected object at a time.
 *           - You can export ALL THE OBJECTS that the bot sees in one click.
 *           - Indicates distance of view, and allows us to change it.
 *          Import command takes the object from world to inventory when it finishes.
 *          Import button able to import more than one item at a time.
 *          Added the backup command, able to backup EVERYTHING from the inventory with the following caveats:\
 *           - You first must have looked up for the contents of the folder where the item you want to backup is in.
 *           - Objects must be rezzed to be backed up, so for now backup command ignores them.
 *           - Calling cards will never be backed up as they really do not exist. Backup command will ignore them.
 *           - Nocopy scripts and notecards cannot be read. It's a SecondLife bug, JIRA VWR-5238. Backup command will ignore them.
 *           - Nomod scripts are not readable at all, forget about them. Backup command will ignore them.
 *           - All textures, gestures, sounds, clothing and body parts, will, however, be downloaded.
 *          Corrected a lack of lock in backuptext command that can give us a runtime exception.
 *          Backup command now uses different methods for textures, notecards, scripts and the rest.
 *          Modified the import button so it can also upload assets (for example the ones downloaded by the backup command).
 *          Added a progress bar and text to the main window indicating us how the import is going out.
 *          Added downloadterrain command that downloads the RAW sim terrain.
 *          Added uploadrawterrain command.
 *          Changed way of how upload command does upload scripts. This removes the HTTP 500 error on script uploading.
 *          Heavily modified createnotecard command.
 *          Added emptylostandfound and emptytrash commands.
 *          Added taskrunning command.
 *          Added wind command.
 *          Added flyto command.
 *          Added textures command.
 *          Added camerafar command. for the same purpose.
 *          Added voiceaccount command.
 *          Added voiceparcel command.
 *          Now every message sent to the bot by a non-master (parcel, estate, object, group im, conference, im, so on) will be sent to the master as an IM.
 *          Changed how the upload command uploads notecards. Now it do uploads every notecard except ones with attachments. Surely is a question of permissions on them.
 * Rev 51:
 *          Corrected a bug that prevented dumpoutfit command to create folders as it was supposed to do.
 * Rev 54:
 *          Updated to libomv r3003
 *          Created a thread for export all button on objects form so it does not stuck the GUI.
 *          Did the same on the appearance command.
 * Rev 55:
 *          Added multilanguage support.
 *          As a side-feature some commands now show more information in the console.
 * Rev 65:
 *          Removed unused and nonfunctional teleport and map tabs.
 *          Removed nonfunctional importoutfit command.
 * Rev 82:
 *          Changed all windows to the new interface. Console is now a miniature until it became moved to a separate window in next revision.
 * Rev 84:
 *          Moved the console to a new form.
 * Rev 91:
 *          Moved all buttons to a new one with a new visual style.
 * Rev 99:
 *          Objects form now allows to search for an object's name.
 *          Default folder for creating all NatiBot's folders is now the user's Documents folder.
 *          Now the dumpattachment command exports correctly the names.
 *          Did the same change on the export command.
 *          Updated downloadtexture command so it now decodes the texture to TGA also.
 *          Command avatarinfo now does not show other's information when used more than one time.
 * Rev 100:
 *          Now you can change the language. The setting will be stored and applied on restart.
 *          Now the bot does offer the option to save all the console to a text file. Enabled by default.
 *          On some cases errors where showing message boxes, now all errors will show up in the console.
 *          Closing NatiBot now disconnects all bots.
 * Rev 115:
 *          Version bumped to NatiBot 0.9.0 Rev 90.
 *          Bot now stablishes it's appearance continuosly
 *          Signed with verification key.
 *          Published on http://www.natibot.com/
 * Rev 169:
 *          Command downloadtexture now creates the textures folder if it does not exist.
 *          Command jump now also stops jumping.
 *          Added key2name command.
 *          Added buy command.
 *          Added takeitem command.
 *          Added translate command.
 *          Added contextual menu to objects window.
 *          Added chat window.
 *          Version bumped to NatiBot 0.9.1 Rev 91.
 * Rev 176:
 *          Corrected behaviour of date/time in chat window.
 *          Added preliminary support for auto-sit on lucky chairs.
 *          Added permissions check for export commands.
 *          Version bumped to NatiBot 0.9.2 Rev 92.
 * Rev 180:
 *          Removed chat and console buffers and put them async. Now the buffer cannot full out crashing the bot and the chat isn't de-synched.
 *          Added automatic update system.
 *          Improved importing speed.
 *          Improved chat system so it does not hang out the bot when chat or IM are received.
 *          Added a dependency for converting JPEG2000 to Targa on 64-bit systems.
 *          Version bumped to NatiBot 0.9.3 Rev 93.
 * Rev 183:
 *          Updated to libomv r3231
 * Rev 325:
 *          NatiBot 1.0.0.325.
 *          Fully support of Mac OS X (PowerPC and Intel) and Linux (x86 and x86-64).
 *          Heavily optimized to use multicore or multiprocessor systems and take less memory.
 *          Added about, activaterole, addtorole, allowedlist, animations, attach, away, banlist, banuser, beam, busy, changelog,
 *              clienttags, clock, createclothing, createeyes, createlm, createskin, detectbots, downloadsound, ejectuser, endfriendship,
 *              gc, gesture, groupeject, groupmembers, grouproles, gsit, health, help2nc, informfriend, invitegroup, logout, lookat,
 *              memfree, nadu, netstats, offerfriendship, pick, playsound, quit, rezitem, searchclassifieds, searchgroups, searchland,
 *              searchpeople, searchplaces and sounds commands.
 *          Added avatars window. It works as a radar showing the avatar, genre, viewer, location and distance, along with most
 *              avatar-based commands.
 *          Added friends window. Allows to IM, remove, teleport or view profiles.
 *          Added groups window. Allows to chat, activate or leave.
 *          Added inventory window. Allows to wear, detach, copy id, delete, empty trash and lost & found.
 *          Added map window. Shows the map with all the avatars and allow TP to coordinates.
 *          All attach based commands now support unofficial attach places introduced by Emerald Viewer.
 *          animate command now can stop animations, show currently running ones, or list the system animations.
 *          avatarinfo now works with any avatar, be it in the sim or not.
 *          Chat window now can automatic translate incoming and outgoing chat and IMs.
 *          Chat window now recognizes teleports, inventory offers from objects, notices from groups, friendship and inventory.
 *          Chat window now supports using another channel (/<channel> message).
 *          Corrected fails of license check on timezones different than Europe/Madrid. Now works globally.
 *          dumpattachment command now uses avatar name as output folder.
 *          findobjects, showeventdetails and searchevents commands now returns output to instant message.
 *          import command now takes lot less time to work, and imports sculpts with mirror or inside-out properties correctly.
 *          joingroup and key2name commands now also work with group IDs.
 *          moveto and turnto commands now can also work using an avatar or object instead of just coordinates.
 *          priminfo command is enhanced to show a lot more of information.
 *          Renamed giveall command to pay.
 *          Renamed goto_landmark command to gotolm.
 *          Revisions are now shown as internal development ones to give a better knowledge of how much work has been employed.
 *          Rewrote objects window. It can take a lot longer to first appear, but refreshes almost instantly, sees new object instantly,
 *              is multithreaded, changes on tp and see more objects and names.
 *          sendtp command now also allows sending TP to other people.
 *          Support for 35 new GRIDs.
 *          Support for Lucky Advent, Lucky CupCake, Lucky Dip, Lucky Present, Lucky Santa, Midnight Mania and Prize Pyramid.
 *          version command now shows also simulator server version.
 *          who command now shows the viewer the avatar is using and the correct location when it is sit.
 *          You can now answer to script dialog boxes (the blue ones). A new window will be opened when an object sends one to the avatar.
 */

namespace bot.license
{
    using System;
    using System.IO;
    using System.Reflection;

    public class Version
    {
        // Nati Bot 0.0 started with SL-Bot 2.2.3.1 rev 36
        private static int v_major = 1;
        private static int v_minor = 0;
        private static int v_patch = 0;
        //private static int v_build = 1;
        public string v_rev = "$" + bot.Localization.clResourceManager.getText("Revision") + " 325$";
        #if DEBUG
        public static Guid AppUUID = new Guid("B9692C48-6ACD-4EBC-BA06-17874B3B67B4");
        #else
        public static Guid AppUUID = new Guid("B9692C48-6ACD-4EBC-BA06-17874B3B67B3");
#endif
        public static string AppVersion = string.Format("{0}.{1}.{2}", v_major, v_minor, v_patch/*, v_build*/);

        public override string ToString()
        {
            return string.Format("{0}.{1}.{2}", v_major, v_minor, v_patch/*, v_build*/);
        }

        public string Changelog
        {
            get
            {
                string ChangeLog;

                String locale = bot.Localization.clResourceManager.getLanguageCode();

                String pathToChangeLogFile = "OpenMetaverse.bot.license." + "changelog_" + locale + ".txt";
                Stream ChangeLogStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToChangeLogFile);

                if (ChangeLogStream == null)
                {
                    pathToChangeLogFile = "OpenMetaverse.bot.license." + "changelog_" + "en" + ".txt";
                    ChangeLogStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(pathToChangeLogFile);
                }

                using (StreamReader reader = new StreamReader(ChangeLogStream))
                {
                    ChangeLog = reader.ReadToEnd();
                }

                return ChangeLog;
            }
            set
            {
                //
            }
        }
    }
}

