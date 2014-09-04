/***************************************************************************
The Disc Image Chef
----------------------------------------------------------------------------
 
Filename       : ShowEffectsCommand.cs
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

    public class ShowEffectsCommand : Command
    {
        private bool ShowEffects;

        public ShowEffectsCommand(SecondLifeBot SecondLifeBot)
        {
            base.Name = "showeffects";
            base.Description = bot.Localization.clResourceManager.getText("Commands.ShowEffects.Description") + " " + bot.Localization.clResourceManager.getText("Commands.ShowEffects.Usage");
            SecondLifeBot.Avatars.ViewerEffect += new EventHandler<ViewerEffectEventArgs>(Avatars_ViewerEffect);
            SecondLifeBot.Avatars.ViewerEffectPointAt += new EventHandler<ViewerEffectPointAtEventArgs>(Avatars_ViewerEffectPointAt);
            SecondLifeBot.Avatars.ViewerEffectLookAt += new EventHandler<ViewerEffectLookAtEventArgs>(Avatars_ViewerEffectLookAt);            
        }

        public override string Execute(string[] args, UUID fromAgentID, bool fromSL)
        {
            if (args.Length == 0)
            {
                ShowEffects = true;
                return bot.Localization.clResourceManager.getText("Commands.ShowEffects.On");
            }
            else if (args.Length == 1)
            {
                if (args[0] == "on")
                {
                    ShowEffects = true;
                    return bot.Localization.clResourceManager.getText("Commands.ShowEffects.On");
                }
                else
                {
                    ShowEffects = false;
                    return bot.Localization.clResourceManager.getText("Commands.ShowEffects.Off");
                }
            }
            else
            {
                return bot.Localization.clResourceManager.getText("Commands.ShowEffects.Usage");
            }
        }

        void Avatars_ViewerEffectPointAt(object sender, ViewerEffectPointAtEventArgs e)
        {
            if (ShowEffects)
                bot.Console.WriteLine(
                    bot.Localization.clResourceManager.getText("Commands.ShowEffects.PointAt"),
                    e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.PointType, e.Duration,
                    e.EffectID.ToString());
        }

        void Avatars_ViewerEffectLookAt(object sender, ViewerEffectLookAtEventArgs e)
        {
            if (ShowEffects)
                bot.Console.WriteLine(
                    bot.Localization.clResourceManager.getText("Commands.ShowEffects.LookAt"),
                    e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.LookType, e.Duration,
                    e.EffectID.ToString());
        }

        void Avatars_ViewerEffect(object sender, ViewerEffectEventArgs e)
        {
            if (ShowEffects)
                bot.Console.WriteLine(
                    bot.Localization.clResourceManager.getText("Commands.ShowEffects.Other"),
                    e.Type, e.SourceID.ToString(), e.TargetID.ToString(), e.TargetPosition, e.Duration,
                    e.EffectID.ToString());
        }
    }
}

