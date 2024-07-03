using CustomPomodoro.Components.Reusable;
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
        private const string _saveFileName = "ColorSettings.json";
        private List<LinkedHSLControl> PrimaryActivityStatusColorControls { get; set; } = new List<LinkedHSLControl>(3)
        {
            new LinkedHSLControl(),
            new LinkedHSLControl(),
            new LinkedHSLControl()
        };

        private List<ConnectedHSLControl> ConnectedActivityStatusColorControls { get; set; } = new(3) 
        {
            new ConnectedHSLControl(),
            new ConnectedHSLControl(),
            new ConnectedHSLControl()
        };

        private List<LinkedHSLControl> SecondaryActivityStatusColorControls { get; set; } = new List<LinkedHSLControl>(3)
        {
            new LinkedHSLControl(),
            new LinkedHSLControl(),
            new LinkedHSLControl()
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
                //TO DO: Hide all other controls. Display 1st color hue only.
            }
        }

        //Will need to change this to make use of "ConnectedHSLControl" instead.
        //private void ToggleAdvancedSettings()
        //{
        //    DisplayAdvancedColorSettings = !DisplayAdvancedColorSettings;
        //    if (DisplayAdvancedColorSettings == true)
        //    {
        //        foreach (var control in PrimaryActivityStatusColorControls)
        //            control.DisplayAllHslColorControls();

        //        foreach (var control in SecondaryActivityStatusColorControls)
        //            control.DisplayAllHslColorControls();
        //    }
        //    else
        //    {
        //        ColorBarInputs.ResetAllColorValuesExceptPrimaryHue();
        //        foreach (var control in PrimaryActivityStatusColorControls)
        //        {
        //            control.DisplayHueControlsOnly();
        //        }
        //        foreach (var control in SecondaryActivityStatusColorControls)
        //        {
        //            control.HideAllHslControls();
        //        }
        //    }
        //}

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
