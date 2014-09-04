**What is NatiBot**

NatiBot was an alternative client, automation, backup tool and bot for the SecondLife network as well as compatible ones.

**Where does it come from**

NatiBot started using the SL-Bot 2.2.3.1 rev 36 codebase, itself an underground modification of libomv 3-clause BSD licensed TestClient example program.

Over the course of the months, everything changed but the most basic commands, a lot of commands where added, and a handful other where synced with TestClient codebase.

**Why did you not open-sourced it on 2010**

When I created NatiBot I had a serious interest in SecondLife, and I monetized my work (as allowed by the 3-clause BSD license).

In 2010 I lost all interest on SecondLife, but misuse of NatiBot could be very high, so opensourcing it would have given people a lot of code making them very easy to steal others copyright.

**Why are you opensourcing it on 2014 then?**

Long time has passed. SecondLife and libomv have changed their APIs making NatiBot useless without a lot of corrections.

Also SecondLife's new EULA strictly prohibit NatiBot, making its source nothing more than an interest showcase of my coding abilities.

Finally, I think that all commercial software that has become useless because of its age should be opensourced and become a cultural piece.

**What's the license?**

Considering some codes come from TestClient and some other from SL-Bot, that code forces me to say:

```
  Copyright (c) 2006-2010, openmetaverse.org
  All rights reserved.
 
  - Redistribution and use in source and binary forms, with or without 
    modification, are permitted provided that the following conditions are met:
 
  - Redistributions of source code must retain the above copyright notice, this
    list of conditions and the following disclaimer.
  - Neither the name of the openmetaverse.org nor the names 
    of its contributors may be used to endorse or promote products derived from
    this software without specific prior written permission.
 
  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
  AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
  IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE 
  ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
  LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
  CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
  SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
  INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN 
  CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
  POSSIBILITY OF SUCH DAMAGE.
```

I don't know who created SL-Bot, and probably he doesn't want to be known either, but Portions Copyright to him.

The majority of the code is (C) 2008-2014 Claunia.com and licensed as GPL version 3 (or later), as shown in attached LICENSE file.

All translation XMLs are (C) 2008-2014 Claunia.com and licensed as GFDL version 1.3 (or later), as shown in attached LICENSE.GFDL file.

**DISCLAIMER**

For legal reasons:

Claunia.com and/or Natalia Portillo, as well as any of their friends, collaborators, employees, employers and/or affiliates take any responsibility, past, present, or future, of any use of this code for purposes that are illegal in any country. That is the sole responsibility of the user.

**But NatiBot does not compile**

No, it doesn't. If you want to use it, solve it.

**And there are missing files and/or dependencies**

All graphical material is still under the original usage license, as well as the webpage and the serial number algorithms.
Usage of that material is forbidden and will be prosecuted.

**Not included open source dependencies**

libopenmetaverse
SmartIRC4Net
DotNetZip
log4net

**What about the other libraries**

The other libraries cannot be distributed or used at all, with or without this code, outside of any specific license agreement you would have with Claunia.com.

**Can I use/distribute/decompile/anything the original NatiBot binaries**

No, you can't. That binaries reached an end-of-support and as stablished by the license ANY AND ALL use of them is strictly forbidden and a violation of international copyright laws.

**Can I <put whatever question you think here>**

No.

**SLOC count**
```
SLOC	Directory	SLOC-by-Language (Sorted)
25423   SLBot           cs=25423
16      packages        sh=16
1       top_dir         sh=1
0       InstallShield   (none)
0       Installer       (none)
0       mono-projects   (none)
0       nsis            (none)

Totals grouped by language (dominant language first):
cs:           25423 (99.93%)
sh:              17 (0.07%)

Total Physical Source Lines of Code (SLOC)                = 25,440
Development Effort Estimate, Person-Years (Person-Months) = 5.98 (71.78)
 (Basic COCOMO model, Person-Months = 2.4 * (KSLOC**1.05))
Schedule Estimate, Years (Months)                         = 1.06 (12.68)
 (Basic COCOMO model, Months = 2.5 * (person-months**0.38))
Estimated Average Number of Developers (Effort/Schedule)  = 5.66
Total Estimated Cost to Develop                           = $ 808,043
 (average salary = $56,286/year, overhead = 2.40).
Please credit this data as "generated using David A. Wheeler's 'SLOCCount'."
```

**Original subversion log**
```
------------------------------------------------------------------------
r326 | claunia | 2010-01-01 15:51:07 +0000 (vie, 01 ene 2010) | 1 line

Corrected update process.
------------------------------------------------------------------------
r325 | claunia | 2009-12-30 23:07:58 +0000 (mié, 30 dic 2009) | 1 line

NatiBot 1.0.0.325
------------------------------------------------------------------------
r324 | claunia | 2009-12-30 22:43:29 +0000 (mié, 30 dic 2009) | 1 line

Updated versions.
------------------------------------------------------------------------
r323 | claunia | 2009-12-30 22:25:00 +0000 (mié, 30 dic 2009) | 1 line

Wrong value on last commit.
------------------------------------------------------------------------
r322 | claunia | 2009-12-30 22:23:42 +0000 (mié, 30 dic 2009) | 1 line

Updated version string to final value.
------------------------------------------------------------------------
r321 | claunia | 2009-12-30 22:20:58 +0000 (mié, 30 dic 2009) | 1 line

Corrected greeting master on each sim change.
------------------------------------------------------------------------
r320 | claunia | 2009-12-30 21:40:22 +0000 (mié, 30 dic 2009) | 3 lines

Added skin chooser.
Finished translation.
RTM.
------------------------------------------------------------------------
r319 | claunia | 2009-12-30 15:43:49 +0000 (mié, 30 dic 2009) | 3 lines

Updated obfuscator to not include Paloma.TargaImage and include Ionic.Zip assemblies.
Added all missing buttons, corrected group message buttons.
Removed OpenMetaverse.GUI from solution as it is not being used.
------------------------------------------------------------------------
r318 | claunia | 2009-12-30 02:05:07 +0000 (mié, 30 dic 2009) | 3 lines

Added missing CA and ES buttons for redish.
Added EN buttons for redish (2 missing).
Added code for moving windows.
------------------------------------------------------------------------
r317 | claunia | 2009-12-29 11:43:33 +0000 (mar, 29 dic 2009) | 1 line

Added correct 1.0 download URLs
------------------------------------------------------------------------
r316 | claunia | 2009-12-29 11:13:20 +0000 (mar, 29 dic 2009) | 1 line

Added in-world SLURL
------------------------------------------------------------------------
r315 | claunia | 2009-12-29 10:34:00 +0000 (mar, 29 dic 2009) | 1 line

Added all new blueish skin screenshots
------------------------------------------------------------------------
r314 | claunia | 2009-12-29 08:58:45 +0000 (mar, 29 dic 2009) | 1 line

Changed all buttons to clImageButton.
------------------------------------------------------------------------
r313 | claunia | 2009-12-29 08:03:14 +0000 (mar, 29 dic 2009) | 2 lines

Added all missing ca, es, fr, en blueish buttons.
Added some but not all missing it, de blueish buttons.
------------------------------------------------------------------------
r312 | claunia | 2009-12-29 06:54:01 +0000 (mar, 29 dic 2009) | 1 line

Added redish Catalan buttons.
------------------------------------------------------------------------
r311 | claunia | 2009-12-27 17:33:53 +0000 (dom, 27 dic 2009) | 1 line

Added installer header for Mac OS X installer.
------------------------------------------------------------------------
r310 | claunia | 2009-12-27 17:24:29 +0000 (dom, 27 dic 2009) | 1 line

Added NSIS installer.
------------------------------------------------------------------------
r309 | claunia | 2009-12-27 03:04:12 +0000 (dom, 27 dic 2009) | 6 lines

Added blueish windows.
Corrected typo on redish frmAddAccount.
Changed redish frmInventory size.
Now when an exception occurs, it REALLY exists.
Updated spreadsheet.
Corrected redish windows in project.
------------------------------------------------------------------------
r308 | claunia | 2009-12-24 07:21:56 +0000 (jue, 24 dic 2009) | 4 lines

Added skin support to clResourceManager.
Moved all "old" skin to blueish skin-folder.
Added redish skin Spanish buttons and all windows.
Changed Designer values to use real resources instead of "global" ones.
------------------------------------------------------------------------
r307 | claunia | 2009-12-24 00:45:32 +0000 (jue, 24 dic 2009) | 1 line

Catalan translator sent the completed file, so using it.
------------------------------------------------------------------------
r306 | claunia | 2009-12-23 16:07:13 +0000 (mié, 23 dic 2009) | 4 lines

Catalan translator fired.
Translated to catalan.
Corrected a typo in catalan translation.
Added support to clResourceEditor to autotranslate to catalan.
------------------------------------------------------------------------
r305 | claunia | 2009-12-22 03:41:32 +0000 (mar, 22 dic 2009) | 1 line

Updated to libomv r3241.
------------------------------------------------------------------------
r304 | claunia | 2009-12-21 06:34:51 +0000 (lun, 21 dic 2009) | 3 lines

Finished log reader, now supports showing accounts, crash, log, and hardware key.
Added statistics to every "dangerous" function of NatiBot, and removed from idle() event as it just crashed the application flow.
In ObjectList check that account is really connected to a simulator, because it seems disposing is not working correctly, or the user can open the objects window before finishing login.
------------------------------------------------------------------------
r303 | claunia | 2009-12-21 05:18:04 +0000 (lun, 21 dic 2009) | 1 line

Added crash logging functions. They may be too much repeated but better to be safe.
------------------------------------------------------------------------
r302 | claunia | 2009-12-20 08:24:37 +0000 (dom, 20 dic 2009) | 1 line

Added statistics to main natibot and two ones for test.
------------------------------------------------------------------------
r301 | claunia | 2009-12-20 07:57:30 +0000 (dom, 20 dic 2009) | 5 lines

Added DotNetZip (custom project).
Added a test to post logs to server.
Added PHP script to take logs.
Added a preliminary log reader.
Added server's key checker to subversion repository.
------------------------------------------------------------------------
r300 | claunia | 2009-12-19 19:24:35 +0000 (sáb, 19 dic 2009) | 5 lines

Added list of windows and their sizes, buttons and their strings.
Corrected a NullReferenceException in who command and avatar list when checking for client tag texture.
dumpoutfit command now tries to get the appearance and in case it fails shows a message instead of exiting quietly.
Corrected a thread error while importing.
Threaded all export functions in objects window.
------------------------------------------------------------------------
r299 | claunia | 2009-12-17 21:11:12 +0000 (jue, 17 dic 2009) | 4 lines

Replace "new line" in filenames as it is a windows non supported character.
Changed AccountList columns sizes and removed openmetaverse license as most of the code is now mine.
Corrected a bug in objects form when copying just one name.
Corrected a "collection has changed" bug in export command.
------------------------------------------------------------------------
r298 | claunia | 2009-12-16 05:00:33 +0000 (mié, 16 dic 2009) | 1 line

Added InstallShield project, just to test out, should need to check license (does it matter?).
------------------------------------------------------------------------
r297 | claunia | 2009-12-15 19:39:14 +0000 (mar, 15 dic 2009) | 4 lines

TargaImage does not work well under Mac OS X. Removed.
Changed profile window to use OpenJPEG.DecodeToImage() function.
Corrected changing selected bot not changing parameters in main window.
animations, sounds and textures now download to same folder as other commands, that is, without "auto_".
------------------------------------------------------------------------
r296 | claunia | 2009-12-14 02:33:29 +0000 (lun, 14 dic 2009) | 1 line

Added Mac OS X Installer Package source.
------------------------------------------------------------------------
r295 | claunia | 2009-12-14 01:48:53 +0000 (lun, 14 dic 2009) | 1 line

Updated Property Lists and added icon.
------------------------------------------------------------------------
r294 | claunia | 2009-12-14 01:33:38 +0000 (lun, 14 dic 2009) | 1 line

Updated most of the web to 1.0
------------------------------------------------------------------------
r293 | claunia | 2009-12-14 01:33:12 +0000 (lun, 14 dic 2009) | 1 line

Translated changelog to Catalan.
------------------------------------------------------------------------
r292 | claunia | 2009-12-14 01:32:22 +0000 (lun, 14 dic 2009) | 1 line

Added launchers for Linux and Mac OS X.
------------------------------------------------------------------------
r291 | claunia | 2009-12-14 00:04:00 +0000 (lun, 14 dic 2009) | 7 lines

NEVER AGAIN SEEN: Textboxes have incorrect selected colors applied.
CANNOT CORRECT: The transparency fucks when above SecondLife window.
ADD TO FAQ
SOLVED: Speed and multicore usage will be optimized to improve NatiBot's performance.
SOLVED: [18:06] El avatar LaNati Nightfire de la región 00000000-0000-0000-0000-000000000000 en <0, 0, 0> envía un mensaje de tipo InventoryOffered diciendo:What is NatiBot? / Que es NatiBot?

Updated commands list.
------------------------------------------------------------------------
r290 | claunia | 2009-12-13 23:58:25 +0000 (dom, 13 dic 2009) | 2 lines

Changelog prepared to 1.0.
Changelog translated to Spanish.
------------------------------------------------------------------------
r289 | claunia | 2009-12-13 23:13:55 +0000 (dom, 13 dic 2009) | 4 lines

Corrected translation when error returned. Then returns untranslated sentence.
Added progress bar to ClResourceEditor.
Changed all window size to adapt to skin.
Finished translation in French and Spanish.
------------------------------------------------------------------------
r288 | claunia | 2009-12-13 21:40:35 +0000 (dom, 13 dic 2009) | 1 line

Added italian and german buttons.
------------------------------------------------------------------------
r287 | claunia | 2009-12-13 20:26:57 +0000 (dom, 13 dic 2009) | 1 line

Added french buttons.
------------------------------------------------------------------------
r286 | claunia | 2009-12-13 20:24:39 +0000 (dom, 13 dic 2009) | 1 line

Script for imagemagick to apply transparency.
------------------------------------------------------------------------
r285 | claunia | 2009-12-13 19:28:50 +0000 (dom, 13 dic 2009) | 1 line

Separate DLLs for each Mac OS X architecture, this scheme is working on PowerPC 10.5
------------------------------------------------------------------------
r284 | claunia | 2009-12-13 18:57:38 +0000 (dom, 13 dic 2009) | 5 lines

Last updates for working under Mono:
Timezones are different, changed them in clock command.
WebBrowser control requires an external library that I'll not supply disable it under Mono.
Profile images are not being loaded in Linux x64, not tested on another.
Under need of testing libopenjpeg-dotnet under Mac OS X, everything is working under Linux and Mac OS X right now.
------------------------------------------------------------------------
r283 | claunia | 2009-12-13 17:31:14 +0000 (dom, 13 dic 2009) | 2 lines

AccountList should use a "normal" column sorter.
Changed how the CheckLicense form is disposed to workaround a bug in Mono's garbage collector that was fucking up how Main form is showed.
------------------------------------------------------------------------
r282 | claunia | 2009-12-13 06:06:30 +0000 (dom, 13 dic 2009) | 1 line

This file is needed for Mono to find the adequate libraries.
------------------------------------------------------------------------
r281 | claunia | 2009-12-13 05:58:52 +0000 (dom, 13 dic 2009) | 1 line

Added OpenJPEG libraries for Mac OS X and Linux.
------------------------------------------------------------------------
r280 | claunia | 2009-12-13 04:41:34 +0000 (dom, 13 dic 2009) | 1 line

Changed DataGridView to a ListView control, this solves the Mac OS X crash.
------------------------------------------------------------------------
r279 | claunia | 2009-12-12 22:28:39 +0000 (sáb, 12 dic 2009) | 2 lines

Changed how the hardware key is generated in Mac OS X.
This contains better information that doesn't change every five minutes.
------------------------------------------------------------------------
r278 | claunia | 2009-12-12 20:01:02 +0000 (sáb, 12 dic 2009) | 1 line

Forgot to add ChangelogCommand.cs.
------------------------------------------------------------------------
r277 | claunia | 2009-12-12 19:54:28 +0000 (sáb, 12 dic 2009) | 1 line

One big step to working under Mono.
------------------------------------------------------------------------
r276 | claunia | 2009-12-12 07:32:16 +0000 (sáb, 12 dic 2009) | 1 line

Moved all strings to resource files. (And again)
------------------------------------------------------------------------
r275 | claunia | 2009-12-11 22:43:43 +0000 (vie, 11 dic 2009) | 1 line

Equaled all language resources.
------------------------------------------------------------------------
r274 | claunia | 2009-12-11 21:52:46 +0000 (vie, 11 dic 2009) | 2 lines

Support for answering script windows (typical blue windows with buttons).
Corrected missing return on objects window contextual menus.
------------------------------------------------------------------------
r273 | claunia | 2009-12-11 20:25:16 +0000 (vie, 11 dic 2009) | 4 lines

Can specify languages separated in Utilities.TranslateText.
LanguageCodes class and dictionary added.
Added languages names to resources.
Chat window now is able to AutoTranslate.
------------------------------------------------------------------------
r272 | claunia | 2009-12-11 18:44:33 +0000 (vie, 11 dic 2009) | 2 lines

Added obfuscator configuration.
Added a "normal" ListColumnSorter and use it on any list that isn't AvatarList.
------------------------------------------------------------------------
r271 | claunia | 2009-12-11 16:55:18 +0000 (vie, 11 dic 2009) | 1 line

Added banlist, allowedlist, banuser and ejectuser commands.
------------------------------------------------------------------------
r270 | claunia | 2009-12-11 16:17:06 +0000 (vie, 11 dic 2009) | 1 line

Added activaterole and addtorole commands, modified events on groupmembers and grouproles commands to be private.
------------------------------------------------------------------------
r269 | claunia | 2009-12-11 15:50:27 +0000 (vie, 11 dic 2009) | 3 lines

Forgot to add contextmenustrip to objects list.
Added commands to inventory menus.
avatarinfo was getting a NullReferenceException when avatar is in sim but no textures known.
------------------------------------------------------------------------
r268 | claunia | 2009-12-10 23:32:34 +0000 (jue, 10 dic 2009) | 1 line

Added createclothing, createeyes and createskin commands.
------------------------------------------------------------------------
r267 | claunia | 2009-12-10 05:30:02 +0000 (jue, 10 dic 2009) | 3 lines

Added attach, rezitem and createlm commands.
createlm for some reason always fail with ERROR 400.
takeitem was already implemented.
------------------------------------------------------------------------
r266 | claunia | 2009-12-10 04:58:49 +0000 (jue, 10 dic 2009) | 1 line

Changed revisions to internal svn revisions.
------------------------------------------------------------------------
r265 | claunia | 2009-12-10 04:12:28 +0000 (jue, 10 dic 2009) | 3 lines

Corrected time on general chat.
Log chat to file.
Luckies are working perfectly.
------------------------------------------------------------------------
r264 | claunia | 2009-12-10 02:55:41 +0000 (jue, 10 dic 2009) | 3 lines

Added groupeject and invitegroup commands.
Remove event on grouproles command.
CheckLicense now uses Madrid timezone the same as Claunia.com server, until server is changed to GMT.
------------------------------------------------------------------------
r263 | claunia | 2009-12-09 22:56:37 +0000 (mié, 09 dic 2009) | 4 lines

version command renamed to changelog.
Added version command that shows current bot and simulator versions.
lucky command will not be added.
downloadsound command was added without updating TODO.
------------------------------------------------------------------------
r262 | claunia | 2009-12-09 22:46:27 +0000 (mié, 09 dic 2009) | 1 line

Implemented support for Lucky Santa, Lucky Present, Lucky Dip, Prize Pyramid, Lucky CupCake and Lucky Advent.
------------------------------------------------------------------------
r261 | claunia | 2009-12-09 22:28:49 +0000 (mié, 09 dic 2009) | 2 lines

Support for Midnight Mania.
frmGroups colors applied on lstGroups.
------------------------------------------------------------------------
r260 | claunia | 2009-12-09 22:01:35 +0000 (mié, 09 dic 2009) | 8 lines

The objects window received a huge list of modifications:
It does now auto-refresh base on simulator events.
When simulator changes it auto-clears.
If initial list of objects is big it can take between 30 and 60 seconds to open, depending on client hardware.
Refresh button now only refresh names and get a statistic.
All the context menus now work perfectly.
It is now not multithreaded.
ListColumnSorter.cs added.
------------------------------------------------------------------------
r259 | claunia | 2009-12-08 20:34:21 +0000 (mar, 08 dic 2009) | 7 lines

NOT REPRODUCED: On line 255 of AvatarList.cs an untrapped NullReferenceException ocurred.
SOLVED: On teleport, it is always timing out, even when correctly teleported. -> Was a SL fail
Deleted Config class, not used.
InventoryTree will never caught onloing event.
Added Map window.
Inventory and Map window tested, need to add functions.

------------------------------------------------------------------------
r258 | claunia | 2009-12-08 07:40:36 +0000 (mar, 08 dic 2009) | 7 lines

Updated to libomv r3240.
Some bugs appeared.
Copied InventoryTree.cs from OpenMetaverse.GUI.
Animations, sounds and textures commands now check both the local variable and the bot one so when disabling in GUI also disables here.
Avatars window now shows current sim and known avatars.
Inventory window added and preliminary working.
In debug mode do not trap exceptions.
------------------------------------------------------------------------
r257 | claunia | 2009-12-07 22:18:10 +0000 (lun, 07 dic 2009) | 7 lines

Added animations and downloadsound commands.
Forgot to add SoundsCommand.cs in appropiate commit.
Modified bot configuration to include getTextures, getSounds, getAnimations, informFriends, touchMidnightMania, haveLuck and acceptInventoryOffers values.
Modified program configuration to include LogChat.
Modified main form to include this configurable values.
Renamed "button1" to "frmMap" and added button for inventory window.
Modified textures and sounds commands, and frmchat to use that configuration values.
------------------------------------------------------------------------
r256 | claunia | 2009-12-07 04:00:24 +0000 (lun, 07 dic 2009) | 2 lines

Removed viewprofile command, NO WAY to call a form from instant message, thread conflicts.
Added a try-catch clause in Program.cs.
------------------------------------------------------------------------
r255 | claunia | 2009-12-06 22:32:56 +0000 (dom, 06 dic 2009) | 7 lines

Added NON-WORKING viewprofile command. Commented it until I find the bug.
Friends window shows profile on double click.
Added results window.
Added "Copy UUID to clipboard", "Show profile", "Send message", "List attachments", "Dump attachments", "Dump outfit" and "Offer friendship" functions to avatars window.
Modified DoCommand() so it returns the output of the command as a string, and renamed to DoCommandReturn().
Added DoCommand() for compatibility.

------------------------------------------------------------------------
r254 | claunia | 2009-12-06 21:40:41 +0000 (dom, 06 dic 2009) | 5 lines

AVATARS WINDOW:
Named distance column.
Added position and viewer.
Corrected position on InitializateAvatars().
Changed lstAvatars default colors.
------------------------------------------------------------------------
r253 | claunia | 2009-12-06 20:04:36 +0000 (dom, 06 dic 2009) | 2 lines

Copied AvatarList from OpenMetaverse.GUI.
Added preliminary Avatars window.
------------------------------------------------------------------------
r252 | claunia | 2009-12-06 19:23:24 +0000 (dom, 06 dic 2009) | 2 lines

Added leave, message and activate functionality to groups window.
Corrected key2name not correctly reporting a key as a group when it is in groupcache.
------------------------------------------------------------------------
r251 | claunia | 2009-12-06 16:49:38 +0000 (dom, 06 dic 2009) | 3 lines

Added custom dialog.
Copied GroupList control from OpenMetaverse.GUI to allow translations and adaptations.
Added Groups window.
------------------------------------------------------------------------
r250 | claunia | 2009-12-06 06:41:40 +0000 (dom, 06 dic 2009) | 2 lines

Removed unused code on help2nc.
Friends window now can send im, teleport and remove a friend.
------------------------------------------------------------------------
r249 | claunia | 2009-12-06 05:50:43 +0000 (dom, 06 dic 2009) | 5 lines

Added TargaImage project.
Profile window loads any profile, decodes interests, decodes and shows images.
Profile button in friends window shows selected friend's profile.
Can be loaded and unloaded any number of times.
Uses existing texture files or downloads if they are not downloaded.
------------------------------------------------------------------------
r248 | claunia | 2009-12-05 23:02:41 +0000 (sáb, 05 dic 2009) | 2 lines

Added preliminary code to get profile on frmProfile.
Added code to call profile window from frmFriends.
------------------------------------------------------------------------
r247 | claunia | 2009-12-05 21:50:53 +0000 (sáb, 05 dic 2009) | 2 lines

Added buttons to frmFriends.
Added profile window.
------------------------------------------------------------------------
r246 | claunia | 2009-12-05 20:52:27 +0000 (sáb, 05 dic 2009) | 1 line

Added preliminary friends window.
------------------------------------------------------------------------
r245 | claunia | 2009-12-05 19:36:52 +0000 (sáb, 05 dic 2009) | 3 lines

Added beam, gsit, lookat and pick commands.
Renamed goto_landmark command to gotolm.
Enhanced moveto and turnto command to be able to do the same with objects and avatars.
------------------------------------------------------------------------
r244 | claunia | 2009-12-05 05:14:57 +0000 (sáb, 05 dic 2009) | 1 line

Added playsound command.
------------------------------------------------------------------------
r243 | claunia | 2009-12-05 05:09:48 +0000 (sáb, 05 dic 2009) | 1 line

Added health command.
------------------------------------------------------------------------
r242 | claunia | 2009-12-05 05:00:44 +0000 (sáb, 05 dic 2009) | 1 line

Added gesture command.
------------------------------------------------------------------------
r241 | claunia | 2009-12-04 20:12:57 +0000 (vie, 04 dic 2009) | 6 lines

SOLVED:
[20:45]  Tsuki Tyran: createnotecard "C:/lacunatext/CCS Boxed(Unpack)/!!!CCS INFO!!!.txt"
[20:46]  Lacuna Teardrop: Error descargando la nota: Tiempo de espera agotado..

Added help2nc command.
Added a newline in start of help.
------------------------------------------------------------------------
r240 | claunia | 2009-12-04 19:15:32 +0000 (vie, 04 dic 2009) | 1 line

Added informfriend command.
------------------------------------------------------------------------
r239 | claunia | 2009-12-04 19:01:35 +0000 (vie, 04 dic 2009) | 1 line

Removed unused or commented code.
------------------------------------------------------------------------
r238 | claunia | 2009-12-04 15:40:30 +0000 (vie, 04 dic 2009) | 1 line

Handled FriendshipAccepted, FriendshipDeclined, InventoryAccepted, InventoryDeclined and InventoryOffered messages.
------------------------------------------------------------------------
r237 | claunia | 2009-12-04 00:20:44 +0000 (vie, 04 dic 2009) | 1 line

Added quit and logout commands.
------------------------------------------------------------------------
r236 | claunia | 2009-12-03 22:36:03 +0000 (jue, 03 dic 2009) | 2 lines

Added clock command.
Uptime command already existed.
------------------------------------------------------------------------
r235 | claunia | 2009-12-03 22:10:13 +0000 (jue, 03 dic 2009) | 3 lines

Added FindOneAvatar to Client class.
Changed avatarinfo command to use it.
Added offerfriendship and endfriendship commands.
------------------------------------------------------------------------
r234 | claunia | 2009-12-03 21:27:39 +0000 (jue, 03 dic 2009) | 1 line

Added nadu command.
------------------------------------------------------------------------
r233 | claunia | 2009-12-03 21:19:04 +0000 (jue, 03 dic 2009) | 4 lines

[17:3] *NN* Lucky Board Black v1.2 susurra: Now looking for a winner whose name begins with... A.
Akasha did not touch it.

SOLVED
------------------------------------------------------------------------
r232 | claunia | 2009-12-03 18:46:30 +0000 (jue, 03 dic 2009) | 3 lines

Corrected NullReferenceException in export command when there is NO textures at all.
Lucky chairs with ? works, with letter (A, T) not.
InventoryOffered messages not handled.
------------------------------------------------------------------------
r231 | claunia | 2009-12-03 01:19:27 +0000 (jue, 03 dic 2009) | 2 lines

who command now shows unknown when the textures are just unknown.
updated to libomv r3239
------------------------------------------------------------------------
r230 | claunia | 2009-12-02 08:17:26 +0000 (mié, 02 dic 2009) | 2 lines

Modified sendtp to support sending the teleport lure to other avatars.
Localized strings are now incorrect.
------------------------------------------------------------------------
r229 | claunia | 2009-12-02 07:46:42 +0000 (mié, 02 dic 2009) | 2 lines

Removed giveall command.
Added pay command.
------------------------------------------------------------------------
r228 | claunia | 2009-12-02 07:23:12 +0000 (mié, 02 dic 2009) | 1 line

Categorized TODO commands.
------------------------------------------------------------------------
r227 | claunia | 2009-12-02 07:05:26 +0000 (mié, 02 dic 2009) | 1 line

Added about command.
------------------------------------------------------------------------
r226 | claunia | 2009-12-02 06:56:07 +0000 (mié, 02 dic 2009) | 1 line

Added away and busy commands.
------------------------------------------------------------------------
r225 | claunia | 2009-12-01 20:36:10 +0000 (mar, 01 dic 2009) | 1 line

Added gc and memfree commands.
------------------------------------------------------------------------
r224 | claunia | 2009-12-01 07:55:41 +0000 (mar, 01 dic 2009) | 1 line

Added PikkuBot commands that are useful AND feasible to 1.0.
------------------------------------------------------------------------
r223 | claunia | 2009-12-01 07:12:22 +0000 (mar, 01 dic 2009) | 2 lines

Help command now sorts all commands.
In DEBUG mode also allows to output direct HTML for the webpage!
------------------------------------------------------------------------
r222 | claunia | 2009-12-01 06:53:20 +0000 (mar, 01 dic 2009) | 3 lines

SOLVED: Will be added support to download sounds that are played where the bot should hear them.

Added sounds command.
------------------------------------------------------------------------
r221 | claunia | 2009-12-01 06:25:52 +0000 (mar, 01 dic 2009) | 3 lines

Deleted Patriotic Nigras Longcat Sim as it is no more working.
Added 35 known GRIDs.
Added localhost login URI.
------------------------------------------------------------------------
r220 | claunia | 2009-12-01 05:31:32 +0000 (mar, 01 dic 2009) | 1 line

BREAKING: Heavily changed the way login grid is stored, loaded, and used, to allow more grids to be added in a lot easier way. However, this changes makes old account files not working.
------------------------------------------------------------------------
r219 | claunia | 2009-12-01 04:02:02 +0000 (mar, 01 dic 2009) | 3 lines

SOLVED: Support for changing chat channel will be added on its window.

Added support as in official viewer "/<channel> message", however, channel and message MUST be separated by a space.
------------------------------------------------------------------------
r218 | claunia | 2009-12-01 02:48:02 +0000 (mar, 01 dic 2009) | 9 lines

Greatly enhanced animate command.
animate list shows all system animations.
animate show shows all playing animations.
animate <system animation> plays system animation.
animate <uuid> plays animation.
animate stop <system animation> stops system animation.
animate stop <uuid> stops animation.
animate stop stops all playing animations.
NOT CHECKED IF STOP WITH INDICATING ANIMATION NOT IN PLAY CRASHES!
------------------------------------------------------------------------
r217 | claunia | 2009-12-01 02:24:47 +0000 (mar, 01 dic 2009) | 1 line

Added netstats command.
------------------------------------------------------------------------
r216 | claunia | 2009-12-01 02:20:01 +0000 (mar, 01 dic 2009) | 2 lines

Added grouproles and groupmembers commands.
Indicated on new search commands that string should be localized.
------------------------------------------------------------------------
r215 | claunia | 2009-12-01 02:03:34 +0000 (mar, 01 dic 2009) | 1 line

Added searchclassifieds, searchgroups, searchland, searchpeople and searchplaces commands.
------------------------------------------------------------------------
r214 | claunia | 2009-12-01 01:45:33 +0000 (mar, 01 dic 2009) | 1 line

Moved search commands to a search folder.
------------------------------------------------------------------------
r213 | claunia | 2009-12-01 01:41:23 +0000 (mar, 01 dic 2009) | 1 line

Solved a NullReferenceException when avatar textures are not yet known.
------------------------------------------------------------------------
r212 | claunia | 2009-11-30 19:48:23 +0000 (lun, 30 nov 2009) | 2 lines

who command now says also client.
Changed who output string in locales to include it.
------------------------------------------------------------------------
r211 | claunia | 2009-11-30 19:29:54 +0000 (lun, 30 nov 2009) | 4 lines

SOLVED: Support to identify non-official viewers that identify themselves will be added (CryoLife, Emerald, etc).

Added all currently known client tags, and added client_list.xml to repository to be able to diff it in case it gets updated.
Added clienttags command to list all known client tags.
------------------------------------------------------------------------
r210 | claunia | 2009-11-30 18:11:40 +0000 (lun, 30 nov 2009) | 1 line

SOLVED: A bug on who command that's showing position relative to sit prim instead of to sim will be corrected.
------------------------------------------------------------------------
r209 | claunia | 2009-11-30 17:46:25 +0000 (lun, 30 nov 2009) | 3 lines

libomv will not integrate Emerald attachment points into AttachmentPoint enum.
Moved libomv AttachmentPoint enum to local NBAttachmentPoint enum, and changed all references in code.
DumpAttachmentCommand now uses avatar name as export folder and XML is now named "<Object_Name> (<Attachment Point>).xml"
------------------------------------------------------------------------
r208 | claunia | 2009-11-30 17:44:11 +0000 (lun, 30 nov 2009) | 1 line

Removed unneeded libjitson code.
------------------------------------------------------------------------
r207 | claunia | 2009-11-30 05:30:07 +0000 (lun, 30 nov 2009) | 3 lines

SOLVED: Support for non-official attachment places (bridge, ankle, etc)

Sent openmetaverse bug http://jira.openmv.org/browse/LIBOMV-769
------------------------------------------------------------------------
r206 | claunia | 2009-11-30 04:51:44 +0000 (lun, 30 nov 2009) | 1 line

GroupNotice is now handled.
------------------------------------------------------------------------
r205 | claunia | 2009-11-30 04:27:07 +0000 (lun, 30 nov 2009) | 2 lines

Improved key2Name() to ask for group name when group is not in cache.
Improved chat group detection to use key2Name(), this should be a lot faster and work even when (if this is possible) IMs are received from groups not from the avatar.
------------------------------------------------------------------------
r204 | claunia | 2009-11-30 04:07:58 +0000 (lun, 30 nov 2009) | 1 line

Added detectbots command.
------------------------------------------------------------------------
r203 | claunia | 2009-11-28 18:22:56 +0000 (sáb, 28 nov 2009) | 8 lines

SOLVED: When import finishes creating prims it takes too long to continue.
Do not wait for permissions set.

SOLVED: It is failing when importing more than one item.
It is clearly working.

When importing more than one item, only the first linkset is correctly detected as finished, however, all are finished.
Removed permissions strings from language resources.
------------------------------------------------------------------------
r202 | claunia | 2009-11-28 17:54:10 +0000 (sáb, 28 nov 2009) | 1 line

Add dependencies not compiled.
------------------------------------------------------------------------
r201 | claunia | 2009-11-28 17:51:23 +0000 (sáb, 28 nov 2009) | 1 line

Added prebuild. Still does not works, fails with resources namespace.
------------------------------------------------------------------------
r200 | claunia | 2009-11-28 17:49:50 +0000 (sáb, 28 nov 2009) | 1 line

Projects updated to Visual Studio 2010.
------------------------------------------------------------------------
r199 | claunia | 2009-11-28 07:25:27 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: how does showeventdetails and searchevents really work?

Both now show information on IM, not only on console.
------------------------------------------------------------------------
r198 | claunia | 2009-11-28 07:13:19 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: Is findobjects working at all?

Information is now shown on IM and not only on console.
------------------------------------------------------------------------
r197 | claunia | 2009-11-28 07:00:47 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: priminfo does nothing?

priminfo now shows a lot more of information.
------------------------------------------------------------------------
r196 | claunia | 2009-11-28 06:21:06 +0000 (sáb, 28 nov 2009) | 1 line

SOLVED: joingroup does not work with UUID. Does not says it worked with name.
------------------------------------------------------------------------
r195 | claunia | 2009-11-28 06:02:16 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: Letting GRID entry empty crashes the whole NatiBot. Default creating new account is empty also. AGNI should be default and check out for this issue when login.

AGNI is now default. Should do more sanity checks when login.
------------------------------------------------------------------------
r194 | claunia | 2009-11-28 05:59:28 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: follow does not inform it is stoping

Added stop option to correctly make it stop.
------------------------------------------------------------------------
r193 | claunia | 2009-11-28 05:48:13 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: translate is not working correctly.

clUtils: Translate helper Changed to official Google API, added litjson to clutils code, may be useful for more things.
------------------------------------------------------------------------
r192 | claunia | 2009-11-28 05:13:18 +0000 (sáb, 28 nov 2009) | 7 lines

SOLVED: avatarinfo stopped working.

It never stopped working

AvatarInfoCommand.cs
--------------------
Enhanced to work with avatars not present in current sim.
------------------------------------------------------------------------
r191 | claunia | 2009-11-28 02:47:30 +0000 (sáb, 28 nov 2009) | 4 lines

SOLVED:
activategroup shows incorrect group name, however, changes group correctly.

key2name command now also works with groups the bot belongs to (libomv is not solving groups...)
------------------------------------------------------------------------
r190 | claunia | 2009-11-28 01:59:47 +0000 (sáb, 28 nov 2009) | 3 lines

SOLVED: Import dialog shows openDialogFile1.

Dialogs are not getting translated at all.
------------------------------------------------------------------------
r189 | claunia | 2009-11-28 01:53:36 +0000 (sáb, 28 nov 2009) | 10 lines

SOLVED when no previous message by that avatar:
[04:06] El avatar Alika Update Server de la región 1c003b78-5e0e-484e-a3ef-c2833ff3cd1e en <223.254, 200.001, 1493.45> envía un mensaje de tipo TaskInventoryOffered diciendo:'Gorean Meter 3.0.4 Boxed'  ( http://slurl.com/secondlife/Gimli/223/200/1493 )
No inventory is agreed, and object name is not used as sender.

SOLVED:
[05:51] El avatar Tsuki Tyran envía un teletransporte a la región 6381d0f6-7883-42b9-164f-9061edeaba50 en <66.8335, 144.481, 36.4512> con el siguiente mensaje:Join me in Fujin
Avatar name empty.

SOLVED:
Avatar name empty when unhanheld type of message.
------------------------------------------------------------------------
r188 | claunia | 2009-11-28 01:33:47 +0000 (sáb, 28 nov 2009) | 6 lines

SOLVED:
[04:06] El avatar Alika Update Server de la región 1c003b78-5e0e-484e-a3ef-c2833ff3cd1e en <223.254, 200.001, 1493.45> envía un mensaje de tipo TaskInventoryOffered diciendo:'Gorean Meter 3.0.4 Boxed'  ( http://slurl.com/secondlife/Gimli/223/200/1493 )
No inventory is agreed, and object name is not used as sender.

----------
Inventory is not yet agreed but at least chat is well created.
------------------------------------------------------------------------
r187 | claunia | 2009-11-28 01:19:45 +0000 (sáb, 28 nov 2009) | 1 line

SOLVED: Chats are sent always from the first bot.
------------------------------------------------------------------------
r186 | claunia | 2009-11-28 01:04:43 +0000 (sáb, 28 nov 2009) | 2 lines

Solved chat and console bug requiring form to be opened.
Solution is not optimal (opening and closing the form), but works.
------------------------------------------------------------------------
r185 | claunia | 2009-11-27 05:05:30 +0000 (vie, 27 nov 2009) | 3 lines

Added newly discovered bugs.
Added list of commands to implement from TestClient.

------------------------------------------------------------------------
r184 | claunia | 2009-11-27 04:20:03 +0000 (vie, 27 nov 2009) | 2 lines

Resolved NullReferenceException on textures, flyto and follow commands.
Resolved master key not being searched correctly.
------------------------------------------------------------------------
r183 | claunia | 2009-11-27 04:04:25 +0000 (vie, 27 nov 2009) | 2 lines

 * Rev 94:
 *          Updated to libomv r3231
------------------------------------------------------------------------
r182 | claunia | 2009-11-27 00:06:20 +0000 (vie, 27 nov 2009) | 9 lines

Bugs:
Chats are sent always from the first bot.
[17:3] *NN* Lucky Board Black v1.2 susurra: Now looking for a winner whose name begins with... A.
Akasha did not touch it.
[04:06] El avatar Alika Update Server de la región 1c003b78-5e0e-484e-a3ef-c2833ff3cd1e en <223.254, 200.001, 1493.45> envía un mensaje de tipo TaskInventoryOffered diciendo:'Gorean Meter 3.0.4 Boxed'  ( http://slurl.com/secondlife/Gimli/223/200/1493 )
No inventory is agreed, and object name is not used as sender.
[05:51] El avatar Tsuki Tyran envía un teletransporte a la región 6381d0f6-7883-42b9-164f-9061edeaba50 en <66.8335, 144.481, 36.4512> con el siguiente mensaje:Join me in Fujin
Avatar name empty.
Letting GRID entry empty crashes the whole NatiBot. Default creating new account is empty also. AGNI should be default and check out for this issue when login.
------------------------------------------------------------------------
r181 | claunia | 2009-11-24 04:29:36 +0000 (mar, 24 nov 2009) | 1 line

Added 1.0 TODO.
------------------------------------------------------------------------
r180 | claunia | 2009-11-24 02:30:44 +0000 (mar, 24 nov 2009) | 7 lines

 * Rev 93:
 *	Removed chat and console buffers and put them async. Now the buffer cannot full out crashing the bot and the chat isn't de-synched.
 *	Added automatic update system.
 *	Improved importing speed.
 *	Improved chat system so it does not hang out the bot when chat or IM are received.
 *	Added a dependency for converting JPEG2000 to Targa on 64-bit systems.
 *	Version bumped to NatiBot 0.9.3 Rev 93.
------------------------------------------------------------------------
r179 | claunia | 2009-11-24 01:35:49 +0000 (mar, 24 nov 2009) | 1 line

Removed chat and console buffers and put them async. Now the buffer cannot full out crashing the bot and the chat isn't de-synched.
------------------------------------------------------------------------
r178 | claunia | 2009-11-24 00:28:10 +0000 (mar, 24 nov 2009) | 10 lines

** clUtils
 Added update scheme.
** ImportCommand.cs
 Removed a check in a timer that was not working neither needed.
** frmChat.cs
 Removed unneeded extra checks that slowed a hell the bot when chat is received.
** Version.cs
 Added applications IDs and version string.
** Program.cs
 Check for updates on running.
------------------------------------------------------------------------
r177 | claunia | 2009-10-08 19:07:44 +0100 (jue, 08 oct 2009) | 1 line

Added DebugHWKey project that outputs the hardware key pre-string in a text file.
------------------------------------------------------------------------
r176 | claunia | 2009-10-05 21:38:28 +0100 (lun, 05 oct 2009) | 2 lines

Forgot to add log4net.dll to the binary file.
Created debug version of 0.9.2.1.
------------------------------------------------------------------------
r175 | claunia | 2009-10-05 21:30:22 +0100 (lun, 05 oct 2009) | 1 line

Version 0.9.2.1 with changes in SSL certificate.
------------------------------------------------------------------------
r174 | claunia | 2009-10-03 17:22:58 +0100 (sáb, 03 oct 2009) | 22 lines

** DumpAttachmentCommand.cs
** ExportCommand.cs
 Check for permissions before allowing to export.
** frmChat.cs
 Implemented Lucky Chair detection.
 Corrected date/time format.
** frmMain.cs
 Added Map form button, still not working.
** language_ca.xml
** language_en.xml
** language_es.xml
** language_fr.xml
 Added resource to indicate not sufficient permissions to export.
** SecondLifeBot.cs
 Accept any inventory offer.
** Version.cs
** changelog_ca.txt
** changelog_en.txt
** changelog_es.txt
** changelog_fr.txt
 Version bumpted to 0.9.2 Rev 92
** Released NatiBot 0.9.2 Rev 92
------------------------------------------------------------------------
r173 | claunia | 2009-09-18 11:39:27 +0100 (vie, 18 sep 2009) | 2 lines

Upated to libomv r3111.
wear command stopped working.
------------------------------------------------------------------------
r172 | claunia | 2009-09-18 11:17:22 +0100 (vie, 18 sep 2009) | 1 line

Updated to libomv r3023.
------------------------------------------------------------------------
r171 | claunia | 2009-09-18 11:00:25 +0100 (vie, 18 sep 2009) | 1 line

Updated to libomv r3020
------------------------------------------------------------------------
r170 | claunia | 2009-09-18 10:52:14 +0100 (vie, 18 sep 2009) | 16 lines

** log4net.csproj
 Changed output
** DownloadAnimation.cs
** BackupCommand.cs
** BackupTextCommand.cs
** CreateNotecardCommand.cs
** ViewNotecardCommand.cs
** QueuedDownloadInfo.cs
 Updated to libomv r3004
 backup is not working by unknown reason, should be rewritten?
** frmObjects.cs
 Changed linebreaks
** Program.cs
 In debug mode do not catch the exceptions, let the debugger do it.
** SecondLifeBot.cs
 Automatically asset receiver never worked as expected. Removed.
------------------------------------------------------------------------
r169 | claunia | 2009-09-17 13:54:11 +0100 (jue, 17 sep 2009) | 1 line

Corrected typo.
------------------------------------------------------------------------
r168 | claunia | 2009-09-17 13:21:52 +0100 (jue, 17 sep 2009) | 1 line

Added comparison.\nCorrected fail on frame border sizes.\nAdded NatiBot 0.9.1 binary and overview.\nAdded key2name, buy, takeitem and translate commands.
------------------------------------------------------------------------
r167 | claunia | 2009-09-17 12:18:37 +0100 (jue, 17 sep 2009) | 1 line

Created NatiBot 0.9.1 Rev 91 installer.
------------------------------------------------------------------------
r166 | claunia | 2009-09-17 11:56:04 +0100 (jue, 17 sep 2009) | 8 lines

** AssemblyInfo.cs
 Version is now 0.9.1.0
** Added frmMain.btnChat resources.
** frmMain.btnChat is now clImageButton and uses the resources.
** Changed frmCheckLicense.cs linebreaks.
** Translated frmChat resources in Catalan.
** Embed french Changelog and frmMain.frmChat buttons.

------------------------------------------------------------------------
r165 | claunia | 2009-09-14 12:33:56 +0100 (lun, 14 sep 2009) | 17 lines

** Program.cs
 Changed linebraeaks to CRLF.
** language_ca.xml
** language_es.xml
 Added frmChat resources, they missed out.
** NatiBot.csproj
 Embed French resources and changelog.
** frmChat.cs
 Use group cache to recognize groups and cache other session IDs.
 Put the appropiate window icon.
 Ignore barely audible messages from 30m distance that came empty in general chat.
 Wait up to 15 seconds to join group chat, as this can take long.
 Create a isgroup() function that looks up in order, group cache, session id cache, and finally request for group name.
 Manually draw items in lstChatters allow us to stablish some colors, for now, general chat is in red.
 Put the functions to allow the window to be moved out.
** frmChat.Designer.cs
 Changed listbox parameters so custom drawing is applied.
------------------------------------------------------------------------
r164 | claunia | 2009-09-14 01:12:11 +0100 (lun, 14 sep 2009) | 2 lines

Added French and German names and codes to CA, EN and ES resources.
Added french resources and changelog.
------------------------------------------------------------------------
r163 | claunia | 2009-09-14 00:20:56 +0100 (lun, 14 sep 2009) | 2 lines

Corrected typo in changelog.
Translated to Catalan.
------------------------------------------------------------------------
r162 | claunia | 2009-09-13 16:10:19 +0100 (dom, 13 sep 2009) | 5 lines

Corrected encoding selection on TranslateCommand.
Corrected encoding selection when clResourceEditor asks for translation.
Make the translate function decode the HTML.

This eliminates all known gibblerish.
------------------------------------------------------------------------
r161 | claunia | 2009-09-13 16:02:51 +0100 (dom, 13 sep 2009) | 3 lines

Added translate functions in clUtils that use Google Language Tools.
Added translate command.
Added function in clResourceEditor to automatically translate ALL resources using clUtils.
------------------------------------------------------------------------
r160 | claunia | 2009-09-13 15:06:23 +0100 (dom, 13 sep 2009) | 3 lines

Translated to Spanish.
Corrected some typos in English.
Modified Catalan so the translator will translate from Spanish.
------------------------------------------------------------------------
r159 | claunia | 2009-09-13 14:57:35 +0100 (dom, 13 sep 2009) | 3 lines

Chat window is ready.
Version bumped to 0.9.1 Rev 91.
Awaiting for translators to publish it.
------------------------------------------------------------------------
r158 | claunia | 2009-09-13 14:43:25 +0100 (dom, 13 sep 2009) | 1 line

Added contextual menu to objects form.
------------------------------------------------------------------------
r157 | claunia | 2009-09-13 14:04:22 +0100 (dom, 13 sep 2009) | 1 line

Added takeitem command.
------------------------------------------------------------------------
r156 | claunia | 2009-09-13 13:56:36 +0100 (dom, 13 sep 2009) | 1 line

Added buy command.
------------------------------------------------------------------------
r155 | claunia | 2009-09-13 13:14:54 +0100 (dom, 13 sep 2009) | 2 lines

It now does chat with groups also.
Changed the chat form to not have borders and use clImageButton for a close button.
------------------------------------------------------------------------
r154 | claunia | 2009-09-13 12:20:39 +0100 (dom, 13 sep 2009) | 1 line

Finally this icon works, added to project.
------------------------------------------------------------------------
r153 | claunia | 2009-09-13 12:19:52 +0100 (dom, 13 sep 2009) | 2 lines

Changed license check so in Mono does not call System.Management.
Should get a way to obtain more information about the machine.
------------------------------------------------------------------------
r152 | claunia | 2009-09-13 12:13:15 +0100 (dom, 13 sep 2009) | 1 line

Changed Mono icon from PNG to a different icon format that should work.
------------------------------------------------------------------------
r151 | claunia | 2009-09-13 11:57:11 +0100 (dom, 13 sep 2009) | 2 lines

Changed "\\" to "/" that works both on Unix and Win32.
Added detection for Mac OS X so it does create NatiBot folder under Documents folder.
------------------------------------------------------------------------
r150 | claunia | 2009-09-13 11:52:46 +0100 (dom, 13 sep 2009) | 1 line

Fixed typo.
------------------------------------------------------------------------
r149 | claunia | 2009-09-13 11:51:34 +0100 (dom, 13 sep 2009) | 1 line

Solved Program.cs problems with Mono.
------------------------------------------------------------------------
r148 | claunia | 2009-09-13 11:49:56 +0100 (dom, 13 sep 2009) | 1 line

Solved problems with icons.
------------------------------------------------------------------------
r147 | claunia | 2009-09-13 11:47:54 +0100 (dom, 13 sep 2009) | 1 line

Fuck fuck fuck
------------------------------------------------------------------------
r146 | claunia | 2009-09-13 11:46:22 +0100 (dom, 13 sep 2009) | 1 line

Embedded icons in Mono project.\nAdd clUtils to Mono Project.
------------------------------------------------------------------------
r145 | claunia | 2009-09-13 11:45:44 +0100 (dom, 13 sep 2009) | 2 lines

Added clUtils reference to NatiBot and to solution.
Changed code in all forms so the icon is set at runtime depending on the runtime platform and disables autosize.
------------------------------------------------------------------------
r144 | claunia | 2009-09-13 11:39:05 +0100 (dom, 13 sep 2009) | 1 line

Added Claunia.clUtils
------------------------------------------------------------------------
r143 | claunia | 2009-09-13 11:05:17 +0100 (dom, 13 sep 2009) | 2 lines

clResourceEditor does not more have help file.
Removed mono binaries.
------------------------------------------------------------------------
r142 | claunia | 2009-09-13 11:04:54 +0100 (dom, 13 sep 2009) | 1 line

Added Linux Desktop shortcut.\nAdded icon in PNG format.
------------------------------------------------------------------------
r141 | claunia | 2009-09-13 10:59:39 +0100 (dom, 13 sep 2009) | 1 line

Added mono projects
------------------------------------------------------------------------
r140 | claunia | 2009-09-13 10:57:15 +0100 (dom, 13 sep 2009) | 2 lines

Added TODO for Mono Framework known problems.
Cleaned frmLicense.
------------------------------------------------------------------------
r139 | claunia | 2009-09-12 08:36:36 +0100 (sáb, 12 sep 2009) | 1 line

GUI finished but transparency fucks out.
------------------------------------------------------------------------
r138 | claunia | 2009-09-12 08:13:58 +0100 (sáb, 12 sep 2009) | 1 line

Added some cleanup when ImportCommand returns.
------------------------------------------------------------------------
r137 | claunia | 2009-09-10 10:00:48 +0100 (jue, 10 sep 2009) | 1 line

Corrected threading error in importing.
------------------------------------------------------------------------
r136 | claunia | 2009-09-06 02:16:49 +0100 (dom, 06 sep 2009) | 8 lines

** SecondLifeBot.cs
 Asks for offline IMs.
** ImCommand.cs
 Interfaces with chat window indicating the bot that an IM was sent.
** frmChat.cs
 Added a ComboBox to select the bot so the ListBox only reflexes its chats.
 Identifies when an IM comes from an object indicating the owner, region and position of the object.
 Handle when the IM cames from offline.
------------------------------------------------------------------------
r135 | claunia | 2009-09-03 05:12:09 +0100 (jue, 03 sep 2009) | 1 line

Deleted empty folder.
------------------------------------------------------------------------
r134 | claunia | 2009-09-03 05:10:40 +0100 (jue, 03 sep 2009) | 1 line

Organized commands on categorized folders.
------------------------------------------------------------------------
r133 | claunia | 2009-09-03 04:59:40 +0100 (jue, 03 sep 2009) | 1 line

Added folders for organizing commands.
------------------------------------------------------------------------
r132 | claunia | 2009-09-03 04:24:45 +0100 (jue, 03 sep 2009) | 1 line

Renamed Key2Name.cs to Key2NameCommand.cs
------------------------------------------------------------------------
r131 | claunia | 2009-09-03 04:06:53 +0100 (jue, 03 sep 2009) | 1 line

Finally solved the infamous avatarinfo command bug.
------------------------------------------------------------------------
r130 | claunia | 2009-09-03 04:03:06 +0100 (jue, 03 sep 2009) | 10 lines

**SecondLifeBot.cs
 Added key2Name() function to resolve an avatar's name using its UUID.
**Avatars
 Added new folder for avatar related commands.
**Key2NameCommand.cs
 Added new command to use Key2Name() function.
**language_ca.xml
**language_en.xml
**language_es.xml
 Added resources for key2name command.
------------------------------------------------------------------------
r129 | claunia | 2009-09-03 03:59:59 +0100 (jue, 03 sep 2009) | 1 line

Command "jump" now stops jumping also.
------------------------------------------------------------------------
r128 | claunia | 2009-09-03 03:59:16 +0100 (jue, 03 sep 2009) | 1 line

Do not ignore teleport lures.
------------------------------------------------------------------------
r127 | claunia | 2009-09-03 03:58:26 +0100 (jue, 03 sep 2009) | 1 line

Stop signing this assembly.
------------------------------------------------------------------------
r126 | claunia | 2009-09-03 01:38:16 +0100 (jue, 03 sep 2009) | 1 line

Finished cleaning from CVS import. Checked it compiles, everything is working out.
------------------------------------------------------------------------
r125 | claunia | 2009-09-03 01:22:27 +0100 (jue, 03 sep 2009) | 1 line

Added libomv external at revision 3003
------------------------------------------------------------------------
r124 | claunia | 2009-09-01 21:16:26 +0100 (mar, 01 sep 2009) | 2 lines

Moved backupcommand.cs to backuptextcommand.cs the old way.

------------------------------------------------------------------------
r123 | claunia | 2009-09-01 20:22:05 +0100 (mar, 01 sep 2009) | 4 lines

Changed frmObjects time to 30 seconds.
Added code from libomv documentation to automatically set the appearance.
Corrected a null reference exception on frmChat.

------------------------------------------------------------------------
r122 | claunia | 2009-09-01 12:34:14 +0100 (mar, 01 sep 2009) | 3 lines

Now also sends chat.
Flags every instant message received that is not from an Agent for debug and implementation.

------------------------------------------------------------------------
r121 | claunia | 2009-09-01 12:20:37 +0100 (mar, 01 sep 2009) | 2 lines

It now does receive general chat.

------------------------------------------------------------------------
r120 | claunia | 2009-09-01 11:56:18 +0100 (mar, 01 sep 2009) | 2 lines

Sends and receives IMs from the chat window.

------------------------------------------------------------------------
r119 | claunia | 2009-09-01 11:55:51 +0100 (mar, 01 sep 2009) | 2 lines

Sends and receives IMs from the chat window.

------------------------------------------------------------------------
r118 | claunia | 2009-09-01 10:36:46 +0100 (mar, 01 sep 2009) | 2 lines

Now the chat window separates the im senders.

------------------------------------------------------------------------
r117 | claunia | 2009-09-01 10:29:09 +0100 (mar, 01 sep 2009) | 2 lines

Now chat window have a list of chats.

------------------------------------------------------------------------
r116 | claunia | 2009-09-01 10:16:31 +0100 (mar, 01 sep 2009) | 5 lines

downloadtexture command now checks for texture command to exist.
Upped to revision 91.
All chat now goes to its own window.
Removed unused copybot license code.

------------------------------------------------------------------------
r115 | claunia | 2009-09-01 00:49:16 +0100 (mar, 01 sep 2009) | 2 lines

NatiBot 0.9 Rev 90 released.

------------------------------------------------------------------------
r114 | claunia | 2009-08-31 20:57:24 +0100 (lun, 31 ago 2009) | 2 lines

Added about box logo.

------------------------------------------------------------------------
r113 | claunia | 2009-08-31 20:56:46 +0100 (lun, 31 ago 2009) | 2 lines

Added logo

------------------------------------------------------------------------
r112 | claunia | 2009-08-31 20:49:56 +0100 (lun, 31 ago 2009) | 2 lines

Added icon.

------------------------------------------------------------------------
r111 | claunia | 2009-08-31 05:57:45 +0100 (lun, 31 ago 2009) | 3 lines

No more "Untitled Document".
Added screenshots.

------------------------------------------------------------------------
r110 | claunia | 2009-08-31 04:50:44 +0100 (lun, 31 ago 2009) | 3 lines

Deleted references to Galician, Romanian an Italian language.
Corrected a typo in catalan overview.

------------------------------------------------------------------------
r109 | claunia | 2009-08-31 04:49:56 +0100 (lun, 31 ago 2009) | 2 lines

Deleted reference to galician.

------------------------------------------------------------------------
r108 | claunia | 2009-08-31 04:48:54 +0100 (lun, 31 ago 2009) | 2 lines

Finished correction to catalan.

------------------------------------------------------------------------
r107 | claunia | 2009-08-31 04:18:34 +0100 (lun, 31 ago 2009) | 2 lines

Corrected typo.

------------------------------------------------------------------------
r106 | claunia | 2009-08-31 04:16:05 +0100 (lun, 31 ago 2009) | 3 lines

ProcessorID information is not available on virtual machines.
Added username variable to the hardware key.

------------------------------------------------------------------------
r105 | claunia | 2009-08-31 04:15:23 +0100 (lun, 31 ago 2009) | 2 lines

Added installer.

------------------------------------------------------------------------
r104 | claunia | 2009-08-31 04:14:32 +0100 (lun, 31 ago 2009) | 2 lines

Disable automatic signing as libopenmetaverse, SmartIRC4Net and XMLPRC dependencies are not signed...

------------------------------------------------------------------------
r103 | claunia | 2009-08-31 04:13:59 +0100 (lun, 31 ago 2009) | 3 lines

Marked a TODO for Rev 100.
Now appearance get automatically set on agent update.

------------------------------------------------------------------------
r102 | claunia | 2009-08-31 04:13:23 +0100 (lun, 31 ago 2009) | 3 lines

Added code to set settings language resources.
Moved lblAccountsInfo out of the groupbox line (it does overwrite it on Windows 7)

------------------------------------------------------------------------
r101 | claunia | 2009-08-30 23:03:59 +0100 (dom, 30 ago 2009) | 3 lines

Version bumped to NatiBot 0.9.0.
Added signing keys.

------------------------------------------------------------------------
r100 | claunia | 2009-08-30 22:41:15 +0100 (dom, 30 ago 2009) | 62 lines

 * Rev 89:
 *          Now you can change the language. The setting will be stored and applied on restart.
 *          Now the bot does offer the option to save all the console to a text file. Enabled by default.
 *          On some cases errors where showing message boxes, now all errors will show up in the console.
 *          Closing NatiBot now disconnects all bots.

** AccountFile.cs
** UploadCommand.cs
** DumpOutfitCommand.cs
** ExportCommand.cs
** frmMain.cs
** PrimRegexCommand.cs
 Moved exception to console.
** changelog_ca.txt
** changelog_en.txt
** changelog_es.txt
 Removed revision 36.
** clResourceManager.cs
 Added list of available locales.
 Added getAvailableLanguages().
 Added getCurrentLanguage().
 Added setCurrentLanguage().
 Added setLanguageCode().
 Added code to get and set the language code from registry.
** DataGridViewClientRow.cs
 Translated bot status.
** frmConsole.cs
 Added code to dump console to a log file.
** frmMain.cs
 Added code to change language.
 Added code to change dump console setting.
 Removed code that called the HTTP server.
 Commented code that disabled rest of items depending on bot status.
 Expanded code that intercepts closing, now works with ALT+F4.
 Now the bot informs in the console (and log) when it starts or stops.
** language_ca.xml
** language_en.xml
** language_es.xml
 Added following resources: LanguageCode.en, LanguageCode.es, LanguageCode.ca, LanguageCode.ga, LanguageCode.ro, LanguageCode.it, frmMain.gbSettings, frmMain.chkLogConsole, frmMain.lblLanguage, bot.Greeting, bot.FormClosing, bot.ButtonClosing, bot.Starts.
** language_ca.xml
 Translated following resources: LanguageCode.en, LanguageCode.es, LanguageCode.ca, LanguageCode.ga, LanguageCode.ro, LanguageCode.it.
** language_es.xml
 Translated following resources: LanguageCode.en, LanguageCode.es, LanguageCode.ca, LanguageCode.ga, LanguageCode.ro, LanguageCode.it, frmMain.gbSettings, frmMain.chkLogConsole, frmMain.lblLanguage, bot.Greeting, bot.FormClosing, bot.ButtonClosing, bot.Starts.
** Program.cs
 Removed CopyBot appearance.
 Added getWriteConsoleToFileSetting()
 Added setWriteConsoleToFileSetting()
** SecondLifeBot.cs
 Translated greeting.
** Version.cs
 Removed N3X15 comment.
 Bumped to revision 89.
** CPLHome.cs
** HTMLCPLCommand.cs
** CommandChk.cs
** CommandRegister.cs
** CommandTest.cs
** HTTPCommand.cs
** HTMLTemplates.cs
** HTTPDaemon.cs
All code commented not in use right now.

------------------------------------------------------------------------
r99 | claunia | 2009-08-30 20:29:45 +0100 (dom, 30 ago 2009) | 8 lines

 * Rev 88:
 *          Objects form now allows to search for an object's name.
 *          Default folder for creating all NatiBot's folders is now the user's Documents folder.
 *          Now the dumpattachment command exports correctly the names.
 *          Did the same change on the export command.
 *          Updated downloadtexture command so it now decodes the texture to TGA also.
 *          Command avatarinfo now does not show other's information when used more than one time.

------------------------------------------------------------------------
r98 | claunia | 2009-08-30 17:55:35 +0100 (dom, 30 ago 2009) | 4 lines

Added rest of buttons.
Added a new class to clResourceManager to allow language-less buttons.
Added frmLicense window.

------------------------------------------------------------------------
r97 | claunia | 2009-08-30 08:39:08 +0100 (dom, 30 ago 2009) | 2 lines

Added settings groupbox and logconsole and language controls.

------------------------------------------------------------------------
r96 | claunia | 2009-08-30 08:28:54 +0100 (dom, 30 ago 2009) | 2 lines

Threaded btnImport

------------------------------------------------------------------------
r95 | claunia | 2009-08-30 08:28:39 +0100 (dom, 30 ago 2009) | 2 lines

Missed a variable change.

------------------------------------------------------------------------
r94 | claunia | 2009-08-30 08:15:44 +0100 (dom, 30 ago 2009) | 2 lines

Threaded btnExport.

------------------------------------------------------------------------
r93 | claunia | 2009-08-29 17:27:44 +0100 (sáb, 29 ago 2009) | 14 lines

** clControls.csproj
 Added new clImageButton control.
** clImageButton.cs
** clImageButton.resx
 Derived from PictureBox instead of from button give almost all of a button functionality and the base.Paint() correctly handles PNG transparency.
** frmMain.cs
 Moved all clButton controls to clImageButton.
** frmObjects.cs
 Moved all clButton controls to clImageButton.
** frmCheckLicense.cs
 Moved all clButton controls to clImageButton.
** Resources
 Moved all idle english buttons so Visual Studio Designer have something to show when editing forms, instead of showing more Microsoft stupid bugs.

------------------------------------------------------------------------
r92 | claunia | 2009-08-29 06:16:08 +0100 (sáb, 29 ago 2009) | 2 lines

Put a workaround to not see the transparency on buttons like the window below this.

------------------------------------------------------------------------
r91 | claunia | 2009-08-29 05:57:26 +0100 (sáb, 29 ago 2009) | 27 lines

** Natibot.sln
 Added clControls to the solution.
** clControls.csproj
** clForm.cs
 Created new form that supports transparent PNGs. Non portable uses Win32 calls.
 Does not paint the controls :(.
** clButton.cs
** clButton.resx
** clCustomButtonGroup.cs
 Created new button that supports transparent PNGs. No problem with this! (should check if code is copyable?)
** NatiBot.csproj
 Embed all buttons (for now, catalan, english and spanish).
 Reference to clControls
** frmLicense.cs
 frmLicense was not getting the texts from language resources. Corrected.
** frmMain.cs
 Changed all buttons but btnConsole and btnCrash to clButton.
 Added code to get the button for the correct language.
** frmObjects.cs
 Changed all buttons but btnFindNext to clButton.
 Added code to get the button for the correct language.
 Now the label shows the bot that object's window belongs to.
** frmConsole.cs
 Now Form.Text gets correctly set so it shows the name in taskbar.
** Updated TODO
** Bumped to rev 87

------------------------------------------------------------------------
r90 | claunia | 2009-08-29 04:53:00 +0100 (sáb, 29 ago 2009) | 2 lines

Changed BMPs to much smaller PNGs on windows.

------------------------------------------------------------------------
r89 | claunia | 2009-08-29 03:03:40 +0100 (sáb, 29 ago 2009) | 2 lines

Added spanish buttons.

------------------------------------------------------------------------
r88 | claunia | 2009-08-29 03:03:27 +0100 (sáb, 29 ago 2009) | 2 lines

Added english buttons.

------------------------------------------------------------------------
r87 | claunia | 2009-08-29 03:01:49 +0100 (sáb, 29 ago 2009) | 2 lines

Added catalan buttons.

------------------------------------------------------------------------
r86 | claunia | 2009-08-29 02:23:14 +0100 (sáb, 29 ago 2009) | 2 lines

Bugs discorered testing Rev 86

------------------------------------------------------------------------
r85 | claunia | 2009-08-28 07:37:28 +0100 (vie, 28 ago 2009) | 4 lines

Added code to handle all windows movement.
Added exit button on frmMain.
Added code to close all bots on exit (does not work on ALT+F4 yet dunno why).

------------------------------------------------------------------------
r84 | claunia | 2009-08-28 07:00:47 +0100 (vie, 28 ago 2009) | 3 lines

Moved the console to an independent form while retaining its functionality.
Revision bumped to 86.

------------------------------------------------------------------------
r83 | claunia | 2009-08-28 06:16:38 +0100 (vie, 28 ago 2009) | 2 lines

Reduced innecesary border.

------------------------------------------------------------------------
r82 | claunia | 2009-08-28 06:14:07 +0100 (vie, 28 ago 2009) | 4 lines

frmMain de-messed.
Console MUST BE MOVED NOW to another form.
Bumped to revision 85 as all current windows are now in the new interface.

------------------------------------------------------------------------
r81 | claunia | 2009-08-28 05:48:04 +0100 (vie, 28 ago 2009) | 4 lines

Put the background on frmMain.
Got rid of tabs and interfaces and plugins by N3X15, at least until I really need them (doubt it).
frmMain is a REAL MESS now, until I move the console out.

------------------------------------------------------------------------
r80 | claunia | 2009-08-28 05:46:40 +0100 (vie, 28 ago 2009) | 2 lines

Reduced innecesary border.

------------------------------------------------------------------------
r79 | claunia | 2009-08-28 05:33:01 +0100 (vie, 28 ago 2009) | 2 lines

Changed frmObjects to the new interface.

------------------------------------------------------------------------
r78 | claunia | 2009-08-28 05:13:41 +0100 (vie, 28 ago 2009) | 2 lines

Changed frmAbout to the new interface.

------------------------------------------------------------------------
r77 | claunia | 2009-08-28 05:00:56 +0100 (vie, 28 ago 2009) | 2 lines

Changed frmAddAccount to the new interface.

------------------------------------------------------------------------
r76 | claunia | 2009-08-28 04:48:04 +0100 (vie, 28 ago 2009) | 2 lines

Missed type in last added resources.

------------------------------------------------------------------------
r75 | claunia | 2009-08-28 04:25:23 +0100 (vie, 28 ago 2009) | 2 lines

Added new interface windows.

------------------------------------------------------------------------
r74 | claunia | 2009-08-28 04:08:14 +0100 (vie, 28 ago 2009) | 4 lines

Moved language resources out of InitializeComponents() so the Visual Studio Designer does not mess my code.
Now it is in each class initializator.
Added frmAbout form title

------------------------------------------------------------------------
r73 | claunia | 2009-08-28 03:55:28 +0100 (vie, 28 ago 2009) | 3 lines

Added frmAccount strings to resource files.
A string was missed in frmMain, moved to resource files.

------------------------------------------------------------------------
r72 | claunia | 2009-08-28 03:27:11 +0100 (vie, 28 ago 2009) | 2 lines

Romanian translator gave up.

------------------------------------------------------------------------
r71 | claunia | 2009-08-26 17:31:21 +0100 (mié, 26 ago 2009) | 2 lines

Added webpage

------------------------------------------------------------------------
r70 | claunia | 2009-08-26 17:26:10 +0100 (mié, 26 ago 2009) | 2 lines

Added a TODO list.

------------------------------------------------------------------------
r69 | claunia | 2009-08-26 17:21:52 +0100 (mié, 26 ago 2009) | 2 lines

Corrected misnamed ID.

------------------------------------------------------------------------
r68 | claunia | 2009-08-14 16:42:30 +0100 (vie, 14 ago 2009) | 2 lines

Translated lastest resources to Catalan.

------------------------------------------------------------------------
r67 | claunia | 2009-08-14 16:18:53 +0100 (vie, 14 ago 2009) | 6 lines

Added an about box.
Moved a resource in version to the resource files.
Added the about box resources to English, Catalan and Spanish.
Translated the about box resources to Spanish.
Removed old objects form files.

------------------------------------------------------------------------
r66 | claunia | 2009-08-14 15:13:15 +0100 (vie, 14 ago 2009) | 2 lines

Added catalan language.

------------------------------------------------------------------------
r65 | claunia | 2009-08-14 01:54:46 +0100 (vie, 14 ago 2009) | 4 lines

* Rev 84:
 *          Removed unused and nonfunctional teleport and map tabs.
 *          Removed nonfunctional importoutfit command.

------------------------------------------------------------------------
r64 | claunia | 2009-08-14 01:38:30 +0100 (vie, 14 ago 2009) | 2 lines

Deleted unused and non-functional teleport and map tabs.

------------------------------------------------------------------------
r63 | claunia | 2009-08-14 01:35:19 +0100 (vie, 14 ago 2009) | 2 lines

Deleted old, unused and non-functional language manager, and all references to it.

------------------------------------------------------------------------
r62 | claunia | 2009-08-13 23:40:08 +0100 (jue, 13 ago 2009) | 2 lines

Added catalan changelog.

------------------------------------------------------------------------
r61 | claunia | 2009-08-13 23:09:34 +0100 (jue, 13 ago 2009) | 4 lines

Deleted two repeated resources.
Translated "changelog" word to spanish.
Added spanish language.

------------------------------------------------------------------------
r60 | claunia | 2009-08-13 22:48:55 +0100 (jue, 13 ago 2009) | 2 lines

Corrected some typos and changed some wording.

------------------------------------------------------------------------
r59 | claunia | 2009-08-13 15:43:18 +0100 (jue, 13 ago 2009) | 2 lines

Added locale attribute.

------------------------------------------------------------------------
r58 | claunia | 2009-08-13 15:24:37 +0100 (jue, 13 ago 2009) | 2 lines

No need to escape quotes.

------------------------------------------------------------------------
r57 | claunia | 2009-08-13 15:06:25 +0100 (jue, 13 ago 2009) | 2 lines

Added Spanish changelog.

------------------------------------------------------------------------
r56 | claunia | 2009-08-13 15:02:10 +0100 (jue, 13 ago 2009) | 4 lines

Added a function in clResourceManager to obtain the locale code, so in a future we'll check in the registry settings.
Changed version command so it reads an embedded resource with the changelog.
Added English changelog.

------------------------------------------------------------------------
r55 | claunia | 2009-08-13 14:44:37 +0100 (jue, 13 ago 2009) | 4 lines

 * Rev 83:
 *          Added multilanguage support.
 *          As a side-feature some commands now show more information in the console.

------------------------------------------------------------------------
r54 | claunia | 2009-08-09 18:29:43 +0100 (dom, 09 ago 2009) | 5 lines

 * Rev 82:
 *          Updated to libomv r3003
 *          Created a thread for export all button on objects form so it does not stuck the GUI.
 *          Did the same on the appearance command.

------------------------------------------------------------------------
r53 | claunia | 2009-07-19 17:09:35 +0100 (dom, 19 jul 2009) | 2 lines

Added old N3X15 revisions of copybot.

------------------------------------------------------------------------
r52 | claunia | 2009-07-13 00:48:37 +0100 (lun, 13 jul 2009) | 3 lines

Added binary releases 79, 80 and 81.
Updated COMMANDS file and added VERSION file.

------------------------------------------------------------------------
r51 | claunia | 2009-07-12 18:15:40 +0100 (dom, 12 jul 2009) | 3 lines

 * Rev 81:
 *          Corrected a bug that prevented dumpoutfit command to create folders as it was supposed to do.

------------------------------------------------------------------------
r50 | claunia | 2009-07-11 22:17:40 +0100 (sáb, 11 jul 2009) | 8 lines

 * Rev 80 Alpha 7:
 *          New objects form now indicates distance of view, and allows us to change it.
 *          Added camerafar command. for the same purpose.
 *          Added voiceaccount command.
 *          Added voiceparcel command.
 *          Now every message sent to the bot by a non-master (parcel, estate, object, group im, conference, im, so on) will be sent to the master as an IM.
 *          Changed how the upload command uploads notecards. Now it do uploads every notecard except ones with attachments. Surely is a question of permissions on them.

------------------------------------------------------------------------
r49 | claunia | 2009-07-11 22:16:10 +0100 (sáb, 11 jul 2009) | 2 lines

*** empty log message ***

------------------------------------------------------------------------
r48 | claunia | 2009-07-11 20:50:30 +0100 (sáb, 11 jul 2009) | 12 lines

 * Rev 80 Alpha 6:
 *          Updated to libomv r2977
 *          Added downloadterrain command that downloads the RAW sim terrain.
 *          Added uploadrawterrain command.
 *          Changed way of how upload command does upload scripts. This removes the HTTP 500 error on script uploading.
 *          Heavily modified createnotecard command.
 *          Added emptylostandfound and emptytrash commands.
 *          Added taskrunning command.
 *          Added wind command.
 *          Added flyto command.
 *          Added textures command.

------------------------------------------------------------------------
r47 | claunia | 2009-07-08 22:51:07 +0100 (mié, 08 jul 2009) | 12 lines

 * Rev 80 Alpha 5:
 *          Corrected a lack of lock in backuptext command that can give us a runtime exception.
 *          Corrected the same in the backup command.
 *          Backup command now uses different methods for textures, notecards, scripts and the rest.
 *           - Calling cards will never be backed up as they really do not exist. Backup command will ignore them.
 *           - Objects must be rezzed to be backed up, so for now backup command ignores them.
 *           - Nocopy scripts and notecards cannot be read. It's a SecondLife bug, JIRA VWR-5238. Backup command will ignore them.
 *           - Nomod scripts are not readable at all, forget about them. Backup command will ignore them.
 *           - All textures, gestures, sounds, clothing and body parts, will, however, be downloaded.
 *          Modified the import button so it can also upload assets (for example the ones downloaded by the backup command).
 *          Added a progress bar and text to the main window indicating us how the import is going out.

------------------------------------------------------------------------
r46 | claunia | 2009-07-07 00:20:45 +0100 (mar, 07 jul 2009) | 10 lines

Corrected when folder contents have still not arrived not to crash on ListContentsCommand.cs

 * Rev 80 Alpha 4:
 *          Added the backup command, able to backup EVERYTHING from the inventory with the following caveats:\
 *           - You first must have looked up for the contents of the folder where the item you want to backup is in.
 *           - Objects cannot be backed up at all, SL protocol seems to not allow us to do that, will check for a solution.
 *           - Scripts you cannot modify, you cannot read. What you cannot read, you cannot backup. Simple.
 *           - Some other items, including textures, fail because permission. However these items should be downloadable from the asset no matter your permissions on them.
 *           - Calling cards cannot be backed up. They may even not exist really as it, will check.

------------------------------------------------------------------------
r45 | claunia | 2009-07-06 19:35:09 +0100 (lun, 06 jul 2009) | 5 lines

Renamed BackupCommand.cs to BackupTextCommand.cs

 * Rev 80 Alpha 3:
 *          Import button able to import more than one item at a time.

------------------------------------------------------------------------
r44 | claunia | 2009-07-06 19:34:33 +0100 (lun, 06 jul 2009) | 3 lines

 * Rev 80 Alpha 2:
 *          Import command takes the object from world to inventory when it finishes.

------------------------------------------------------------------------
r43 | claunia | 2009-07-05 19:12:44 +0100 (dom, 05 jul 2009) | 18 lines

 * Rev 79b:
 *          Corrected dumpattachment, dumpattachments, export commands, and the GUI button objects.
 *          The GUI button now works, but is, unreliable like always.
 * Rev 80 Alpha 1:
 *          Version bumped to 0.8
 *          Corrected exporting objects WITHOUT texture (yeah, I've found ONE)
 *          Created a new objects window from scratch with the following features:
 *           - List of UUID, location and name, sortable at user's wishes.
 *           - Automatic autoupdate for taking names and new objects every 10 seconds.
 *           - That option can be disabled.
 *           - Export button is able to export ANY object no matter what name it has (even if it is unknown).
 *           - You can choose what to copy, the UUID, location, or name, to the clipboard, using right click or CTRL+C.
 *           - You can export more than one selected object at a time.
 *           - You can export ALL THE OBJECTS that the bot sees in one click.
 * Rev 80 Alpha 2:
 *          Import command takes the object from world to inventory when it finishes.
 *          Then it sends the object to the master (still untested).

------------------------------------------------------------------------
r42 | claunia | 2009-07-03 23:38:17 +0100 (vie, 03 jul 2009) | 12 lines

 * Rev 79:
 *          Updated to libomv r2947.
 *          Texture downloading system gets rewritten as part of this update.
 *          Advantages, supposedly, speed and less bugs.
 *          Disadvantages, no progress indicator on bot's console.
 *          Textures are stored in textures folder, when using export or downloadtexture commands.
 *          Outfits textures get stored in outfits subfolder, in a folder named as the avatar.
 *          Attachments textures get downloaded in a subfolder called textures in a folder with the UUID.
 *          This should end downloaded textures confusion.
 *          This is a minor revision, with big changes awaiting on NatiBot 0.8.0 rev 80.
 *          log4net.dll compiles with some kind of error and cannot be executed, should be manually copied from libomv's bin.

------------------------------------------------------------------------
r41 | claunia | 2008-12-18 16:40:27 +0000 (jue, 18 dic 2008) | 2 lines

Forgot a library in the binary. Corrected.

------------------------------------------------------------------------
r40 | claunia | 2008-12-18 15:42:16 +0000 (jue, 18 dic 2008) | 6 lines

Modified to work with libomv r2382.

Updated to revision 78.

Added binary.

------------------------------------------------------------------------
r39 | claunia | 2008-12-17 03:11:41 +0000 (mié, 17 dic 2008) | 2 lines

Added license checking and missing files.

------------------------------------------------------------------------
r38 | claunia | 2008-12-17 02:46:54 +0000 (mié, 17 dic 2008) | 2 lines

Forgot to put some binary files lol.

------------------------------------------------------------------------
r37 | claunia | 2008-12-16 01:56:47 +0000 (mar, 16 dic 2008) | 2 lines

Modified NatiBot r77 to compile with libomv r2381

------------------------------------------------------------------------
r36 | claunia | 2008-09-21 06:06:17 +0100 (dom, 21 sep 2008) | 7 lines

 * Rev 77:
 *          Modified "upload" command to catch exceptions when loading the file.
 *          Corrected animation extension on auto-download-from-asset event from .animtn to .animatn.
 *          Removed "test" command, as it was, just a test.
 *          Added "downloadanimation" to download an animation from the asset server given its UUID. (The one that appears with Animation Info enabled on the official client)
 *          Version bumped to 0.7 (20 new commands). YUPIIIIIIIIIII!!!!!!!!

------------------------------------------------------------------------
r35 | claunia | 2008-09-21 05:26:48 +0100 (dom, 21 sep 2008) | 5 lines

Removed a DeleteFolderCommand tryout.

 * Rev 76:
 *          Added "script" command to take a list of commands to execute from a given text file.

------------------------------------------------------------------------
r34 | claunia | 2008-09-21 05:08:14 +0100 (dom, 21 sep 2008) | 3 lines

 * Rev 75:
 *          Added "downloadtexture" command to download a texture from the asset server given the UUID. Still does not convert to Targa format.

------------------------------------------------------------------------
r33 | claunia | 2008-09-21 04:58:42 +0100 (dom, 21 sep 2008) | 3 lines

 * Rev 74:
 *          Added "forward", "back", "left" and "right" commands to move the bot.

------------------------------------------------------------------------
r32 | claunia | 2008-09-21 04:33:50 +0100 (dom, 21 sep 2008) | 3 lines

 * Rev 73:
 *          Added "selectobjects" command, to show detailed information about the prims owned by a determined avatar, in the specified parcel.

------------------------------------------------------------------------
r31 | claunia | 2008-09-21 04:27:56 +0100 (dom, 21 sep 2008) | 3 lines

 * Rev 72:
 *          Added "primowners" command, to show prims count and their owners in a parcel. It needs permissions.

------------------------------------------------------------------------
r30 | claunia | 2008-09-21 04:15:16 +0100 (dom, 21 sep 2008) | 5 lines

 * Rev 71:
 *          Corrected a NullException in "parcelinfo" command.
 *          Modified "parcelinfo" timeout from 30sec. to 60sec.
 *          Added "parceldetails" command, that with a given parcel ID, shows ALL information about that parcel.

------------------------------------------------------------------------
r29 | claunia | 2008-09-21 03:22:22 +0100 (dom, 21 sep 2008) | 15 lines

 * Rev 70:
 *          Modified "createnotecard" and "uploadimage" commands as accent in "dHa" is not correctly handled by asset server in description of uploaded item.
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

------------------------------------------------------------------------
r28 | claunia | 2008-09-20 05:42:36 +0100 (sáb, 20 sep 2008) | 4 lines

 * Rev 69:
 *          Modified "createnotecard" command to send the Notecard to the master.
 *          Added "uploadimage" command to upload an image/texture and send it to the master.

------------------------------------------------------------------------
r27 | claunia | 2008-09-20 04:59:25 +0100 (sáb, 20 sep 2008) | 3 lines

 * Rev 68:
 *          Added "viewnote" command to dump contents of an inventory's notecard to the console/IM.

------------------------------------------------------------------------
r26 | claunia | 2008-09-20 04:42:21 +0100 (sáb, 20 sep 2008) | 3 lines

 * Rev 67:
 *          Added "rmdir" command to move a folder from the inventory to the trash.

------------------------------------------------------------------------
r25 | claunia | 2008-09-20 04:29:27 +0100 (sáb, 20 sep 2008) | 7 lines

 * Rev 66:
 *          Modified "dumpoutfit" command to work with libOMV API (RequestImage()) changes in SVN r2227.

Deleted old SLBot solution.
Ignore project migration logs and backups.
Updated NatiBot solution.

------------------------------------------------------------------------
r24 | claunia | 2008-09-20 04:22:06 +0100 (sáb, 20 sep 2008) | 42 lines

Updated libOpenMetaverse to SVN revision 2229
------------------------------------------------------------------------------

r2229:
LIBOMV-385 Corrects incorrect seed caps names, Thanks Brandon Lockaby for the patch

r2228:
Fixing bugs in the previous commit for refreshing texture downloads

r2227:
* Modified ImageDownload to hold ImageType and DiscardLevel
* RequestImage() now takes a starting packet number as a parameter
* Image download refresh timer calculates which packet to restart the transfer at

r2226:
Fixed Simian instant message functionality between avatars (AgentID was not set in the AgentData block)

r2225:
* Defaulting AgentManager.Movement.AutoResetControls to false since most non-interactive bots will expect this behavior. May break a few bots that expect movement flags to be reset every update!
* Fixed a crashing typo in TestClient ScriptCommand

r2224:
Simian:
* Decode layer boundaries when storing a texture asset
* Started fleshing out correct texture downloading
* Add avatars to the scene along with prims

r2223:
* Added SleepCommand to TestClient which uses AgentPause and AgentResume to sleep for a given number of seconds
* Added ScriptCommand to TestClient to execute commands from a script

r2222:
Resolving [TC-51], added forward/back/left/right movement commands to TestClient and confirmed autopilot still functions

r2221:
LIBOMV-196 Exposes various group cache data we store to public using our InternalDictionary class
* Adds ActiveGroupPowers field to AgentManager which holds currently active groups GroupPowers for current avatar

r2220:
* Added ObjectDuplicate support to Simian
* Improved Simian PrimFlags handling for newly created objects

------------------------------------------------------------------------
r23 | claunia | 2008-09-17 07:50:26 +0100 (mié, 17 sep 2008) | 3 lines

 * Rev 65:
 *          Now the "avatarinfo" command gets also interests and groups. Statistics are disabled by Linden Lab because they are deprecated so apparently no way to get them.

------------------------------------------------------------------------
r22 | claunia | 2008-09-17 07:06:46 +0100 (mié, 17 sep 2008) | 3 lines

 * Rev 64:
 *          Now the "avatarinfo" command shows not only textures but profile text and images. Interests, groups, and statistics, are on the way.

------------------------------------------------------------------------
r21 | claunia | 2008-09-15 07:07:09 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 63:
 *          Added "give" command to give inventory items to an avatar.

------------------------------------------------------------------------
r20 | claunia | 2008-09-15 06:52:15 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 62:
 *          Added "createnotecard" command to create a Notecard from a local text file.

------------------------------------------------------------------------
r19 | claunia | 2008-09-15 06:28:11 +0100 (lun, 15 sep 2008) | 4 lines

 * Rev 61:
 *          Added "cd" command to change current inventory directory.
 *          Modified "ls" command so with '-l' option it says creation date/time and asset type of contents, and works with subfolders (previously it did not without first using "i" command to populate).

------------------------------------------------------------------------
r18 | claunia | 2008-09-15 05:12:42 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 60:
 *          Added "ls" command to list contents of currenty inventory directory.

------------------------------------------------------------------------
r17 | claunia | 2008-09-15 04:40:46 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 59:
 *          Added "avatarinfo" command that shows information about a nearby avatar.

------------------------------------------------------------------------
r16 | claunia | 2008-09-15 04:04:14 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 58:
 *          Commands "attachments" and "attachmentsuuid" now list on IM console.

------------------------------------------------------------------------
r15 | claunia | 2008-09-15 03:48:27 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 58:
 *          Commands "attachments" and "attachmentsuuid" now list on IM console.

------------------------------------------------------------------------
r14 | claunia | 2008-09-15 03:37:08 +0100 (lun, 15 sep 2008) | 4 lines

 * Rev 57:
 *          Command "objectinventory" never stopped working, was only lag!
 *          Command "exportparticles" was storing all particle systems in the same lsl file. Corrected.

------------------------------------------------------------------------
r13 | claunia | 2008-09-15 02:41:40 +0100 (lun, 15 sep 2008) | 4 lines

 * Rev 56:
 *          Corrected "import" command.
 *          Changed "CopyXML" folder to more appropiate "objects" folder.

------------------------------------------------------------------------
r12 | claunia | 2008-09-15 02:06:27 +0100 (lun, 15 sep 2008) | 3 lines

 * Rev 55:
 *          Corrected "export" command, now works as before!

------------------------------------------------------------------------
r11 | claunia | 2008-09-15 00:13:10 +0100 (lun, 15 sep 2008) | 2 lines

Added release 0.6.9 r54.

------------------------------------------------------------------------
r10 | claunia | 2008-09-14 18:53:06 +0100 (dom, 14 sep 2008) | 2 lines

Added old releases to repository

------------------------------------------------------------------------
r9 | claunia | 2008-09-14 18:50:34 +0100 (dom, 14 sep 2008) | 4 lines

Generated key for log4net.
Added log4net project to NatiBot solution.
Modified SmartIrc4Net to reference log4net from current solution.

------------------------------------------------------------------------
r8 | claunia | 2008-09-14 18:02:00 +0100 (dom, 14 sep 2008) | 2 lines

Created NatiBot solution.

------------------------------------------------------------------------
r7 | claunia | 2008-09-14 17:30:25 +0100 (dom, 14 sep 2008) | 2 lines

Added log4net library 1.2.0 (again)

------------------------------------------------------------------------
r6 | claunia | 2008-09-14 16:28:24 +0100 (dom, 14 sep 2008) | 2 lines

Added log4net library 1.2.0 (again)

------------------------------------------------------------------------
r5 | claunia | 2008-09-14 15:33:40 +0100 (dom, 14 sep 2008) | 2 lines

Removed log4net library 1.2.0

------------------------------------------------------------------------
r4 | claunia | 2008-09-14 14:52:15 +0100 (dom, 14 sep 2008) | 2 lines

Added log4net library 1.2.0

------------------------------------------------------------------------
r3 | claunia | 2008-09-14 07:06:44 +0100 (dom, 14 sep 2008) | 2 lines

Added log4net library 1.2.0

------------------------------------------------------------------------
r2 | claunia | 2008-09-14 05:11:21 +0100 (dom, 14 sep 2008) | 3 lines

NatiBot Revision 54.
First commit to CVS

------------------------------------------------------------------------
r1 | (no author) | 2008-09-14 05:11:21 +0100 (dom, 14 sep 2008) | 1 line

Standard project directories initialized by cvs2svn.
------------------------------------------------------------------------
``