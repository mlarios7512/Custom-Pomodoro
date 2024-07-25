﻿using CustomPomodoro.Components.Reusable;
using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings;
using CustomPomodoro.Models.UserSettings.Abstract;
using CustomPomodoro.Models.UserSettings.Concrete;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class ColorSettings
    {
        [Inject]
        protected IMasterUserSettings UserSettings { get; set; }
        private BackgroundColorSettings BgColorInputs = new BackgroundColorSettings();
        private ActivityBarSettings ColorBarInputs { get; set; } = new ActivityBarSettings();
        private bool DisplayAdvancedColorSettings { get; set; } = false;
        private bool DisplayActivityBarColorControls { get; set; } = true;
        private string ActivityBarControlsVisibility { get; set; } = string.Empty;
        private List<ConnectedHSLControl> ConnectedActivityStatusColorControls { get; set; } = new(3) 
        {
            new ConnectedHSLControl(),
            new ConnectedHSLControl(),
            new ConnectedHSLControl()
        };
        private List<SoloHSLControl> BgColorControls { get; set; } = new List<SoloHSLControl>()
        {
            new SoloHSLControl(),
            new SoloHSLControl(),
            new SoloHSLControl()
        };

        //Question: Can you call a method on a child component from "OnInitializedAsync"?
        protected override async Task OnInitializedAsync()
        {
            ActivityBarControlsVisibility = string.Empty;
            //Don't use "await" here or you risk the wrong colors as starting input in the subsequent code.
            UserSettings.LoadAllColorSettings();

            //Best to transfer global "UserSettings" bgColors & activityBarColors into local colors.
            // (This is because even if you don't click "save", any color changes made during the current session will be visible in the "timer" page.)
            BgColorInputs = UserSettings.GetBackgroundColorSettings();
            ColorBarInputs = UserSettings.GetActivityBarSettings();
        }

        private async Task GetDefaultBgColorValues()
        {
            BgColorInputs.SetDefaultColorsValues();
        }

        private async Task GetDefaultActivityBarColorValues()
        {
            ColorBarInputs.SetAllColorsToDefaultValues();
        }

        private void ToggleActivityBarOperationalStatus() 
        {
            //To do:
            //--Hide inputs when switched off.
            //--Show inputs when switched on.
            //--Modify JSON to accomodate for the additional property enable/disable property.
            //-----Appropriately handle saving of options to make sure there are no errors.
            DisplayActivityBarColorControls = !DisplayActivityBarColorControls;
            if (DisplayActivityBarColorControls == true) 
            {
                //Display controls. (Alter saving logic if needed).
                ActivityBarControlsVisibility = "";
            }
            else 
            {
                //Hide controls. (Alter saving logic if needed).
                ActivityBarControlsVisibility = "hidden-and-minimized";
            }
        }

        private void ToggleAdvancedSettings() 
        {
            DisplayAdvancedColorSettings  = !DisplayAdvancedColorSettings;
            if(DisplayAdvancedColorSettings == true) 
            {
                foreach(var control in ConnectedActivityStatusColorControls)
                    control.DisplayAllHslColorControls();
            }
            else 
            {
                ColorBarInputs.ResetAllColorValuesExceptPrimaryHue();

                foreach (var control in ConnectedActivityStatusColorControls)
                    control.DisplayHueControlsOnly();
            }
        }

        private void MakeSimilarStageOneAndTwoColors()
        {
            ColorBarInputs.WorkColors.Last().Hue = ColorBarInputs.WorkColors.First().Hue;
            ColorBarInputs.ShortBreakColors.Last().Hue = ColorBarInputs.ShortBreakColors.First().Hue;
            ColorBarInputs.LongBreakColors.Last().Hue = ColorBarInputs.LongBreakColors.First().Hue;
        }

        private void UpdateSecondaryColorIfNeeded()
        {
            if (DisplayAdvancedColorSettings == false)
            {
                MakeSimilarStageOneAndTwoColors();
            }

        }
    }
}
