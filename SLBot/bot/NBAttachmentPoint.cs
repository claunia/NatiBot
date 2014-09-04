/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : NBAttachmentPoint.cs
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

namespace bot
{
    /// <summary>
    /// Attachment points for objects on avatar bodies
    /// </summary>
    /// <remarks>
    /// Both InventoryObject and InventoryAttachment types can be attached
    ///</remarks>
    public enum NBAttachmentPoint : byte
    {
        /// <summary>Right hand if object was not previously attached</summary>
        Default = 0,
        /// <summary>Chest</summary>
        Chest = 1,
        /// <summary>Skull</summary>
        Skull,
        /// <summary>Left shoulder</summary>
        LeftShoulder,
        /// <summary>Right shoulder</summary>
        RightShoulder,
        /// <summary>Left hand</summary>
        LeftHand,
        /// <summary>Right hand</summary>
        RightHand,
        /// <summary>Left foot</summary>
        LeftFoot,
        /// <summary>Right foot</summary>
        RightFoot,
        /// <summary>Spine</summary>
        Spine,
        /// <summary>Pelvis</summary>
        Pelvis,
        /// <summary>Mouth</summary>
        Mouth,
        /// <summary>Chin</summary>
        Chin,
        /// <summary>Left ear</summary>
        LeftEar,
        /// <summary>Right ear</summary>
        RightEar,
        /// <summary>Left eyeball</summary>
        LeftEyeball,
        /// <summary>Right eyeball</summary>
        RightEyeball,
        /// <summary>Nose</summary>
        Nose,
        /// <summary>Right upper arm</summary>
        RightUpperArm,
        /// <summary>Right forearm</summary>
        RightForearm,
        /// <summary>Left upper arm</summary>
        LeftUpperArm,
        /// <summary>Left forearm</summary>
        LeftForearm,
        /// <summary>Right hip</summary>
        RightHip,
        /// <summary>Right upper leg</summary>
        RightUpperLeg,
        /// <summary>Right lower leg</summary>
        RightLowerLeg,
        /// <summary>Left hip</summary>
        LeftHip,
        /// <summary>Left upper leg</summary>
        LeftUpperLeg,
        /// <summary>Left lower leg</summary>
        LeftLowerLeg,
        /// <summary>Stomach</summary>
        Stomach,
        /// <summary>Left pectoral</summary>
        LeftPec,
        /// <summary>Right pectoral</summary>
        RightPec,
        /// <summary>HUD Center position 2</summary>
        HUDCenter2,
        /// <summary>HUD Top-right</summary>
        HUDTopRight,
        /// <summary>HUD Top</summary>
        HUDTop,
        /// <summary>HUD Top-left</summary>
        HUDTopLeft,
        /// <summary>HUD Center</summary>
        HUDCenter,
        /// <summary>HUD Bottom-left</summary>
        HUDBottomLeft,
        /// <summary>HUD Bottom</summary>
        HUDBottom,
        /// <summary>HUD Bottom-right</summary>
        HUDBottomRight,
        // Emerald viewer extra attachment points
        /// <summary>Chest 2</summary>
        Chest2 = 39,
        /// <summary>Skull 2</summary>
        Skull2,
        /// <summary>Left shoulder 2</summary>
        LeftShoulder2,
        /// <summary>Right shoulder 2</summary>
        RightShoulder2,
        /// <summary>Left hand 2</summary>
        LeftHand2,
        /// <summary>Right hand 2</summary>
        RightHand2,
        /// <summary>Left foot 2</summary>
        LeftFoot2,
        /// <summary>Right foot 2</summary>
        RightFoot2,
        /// <summary>Spine 2</summary>
        Spine2,
        /// <summary>Pelvis 2</summary>
        Pelvis2,
        /// <summary>Mouth 2</summary>
        Mouth2,
        /// <summary>Chin 2</summary>
        Chin2,
        /// <summary>Left ear 2</summary>
        LeftEar2,
        /// <summary>Right ear 2</summary>
        RightEar2,
        /// <summary>Left eyeball 2</summary>
        LeftEyeball2,
        /// <summary>Right eyeball 2</summary>
        RightEyeball2,
        /// <summary>Nose 2</summary>
        Nose2,
        /// <summary>Right upper arm 2</summary>
        RightUpperArm2,
        /// <summary>Right forearm 2</summary>
        RightForearm2,
        /// <summary>Left upper arm 2</summary>
        LeftUpperArm2,
        /// <summary>Left forearm 2</summary>
        LeftForearm2,
        /// <summary>Right hip 2</summary>
        RightHip2,
        /// <summary>Right upper leg 2</summary>
        RightUpperLeg2,
        /// <summary>Right lower leg 2</summary>
        RightLowerLeg2,
        /// <summary>Left hip 2</summary>
        LeftHip2,
        /// <summary>Left upper leg 2</summary>
        LeftUpperLeg2,
        /// <summary>Left lower leg 2</summary>
        LeftLowerLeg2,
        /// <summary>Stomach 2</summary>
        Stomach2,
        /// <summary>Left pectoral 2</summary>
        LeftPec2,
        /// <summary>Right pectoral 2</summary>
        RightPec2,
        /// <summary>Left Knee</summary>
        LeftKnee,
        /// <summary>Right Knee</summary>
        RightKnee,
        /// <summary>Bridge</summary>
        Bridge = 128
    }
}
