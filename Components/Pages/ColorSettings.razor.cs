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

        //You still need this. Do NOT remove it!
        private string ActivityBarControlsVisibility { get; set; } = string.Empty;
        private const string ActivityBarHiddenHTMLKeyword = "hidden-and-minimized";
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

            if (UserSettings.GetActivityBarSettings().EnableActivityBar) 
            {
                ColorBarInputs.EnableActivityBar = true;
                ActivityBarControlsVisibility = string.Empty;
            }
            else 
            {
                ColorBarInputs.EnableActivityBar = false;
                ActivityBarControlsVisibility = ActivityBarHiddenHTMLKeyword;
            }
                


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
            ColorBarInputs.EnableActivityBar = !ColorBarInputs.EnableActivityBar;
            if (ColorBarInputs.EnableActivityBar == true) 
            {
                ActivityBarControlsVisibility = "";
                ColorBarInputs.EnableActivityBar = true;
            }
            else 
            {
                ActivityBarControlsVisibility = ActivityBarHiddenHTMLKeyword;
                ColorBarInputs.EnableActivityBar = false;
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
