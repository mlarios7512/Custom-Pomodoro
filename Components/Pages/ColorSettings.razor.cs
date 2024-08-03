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
        private MainColorSettings LocalColorInputs = new();
        private bool DisplayAdvancedColorSettings { get; set; } = false;
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
        protected override async Task OnInitializedAsync()
        {
            ActivityBarControlsVisibility = string.Empty;
            UserSettings.LoadAllColorSettings();

            //Best to transfer global "UserSettings" bgColors & activityBarColors into local colors.
            // (This is because even if you don't click "save", any color changes made during the current session will be visible in the "timer" page.)
            LocalColorInputs.BackgroundColorSettings = UserSettings.GetBackgroundColorSettings();
            LocalColorInputs.ActivityColorSettings = UserSettings.GetActivityBarSettings();

            if (UserSettings.GetActivityBarSettings().EnableActivityBar) 
            {
                LocalColorInputs.ActivityColorSettings.EnableActivityBar = true;
                ActivityBarControlsVisibility = string.Empty;
            }
            else 
            {
                LocalColorInputs.ActivityColorSettings.EnableActivityBar = false;
                ActivityBarControlsVisibility = ActivityBarHiddenHTMLKeyword;
            }
        }

        private async Task GetDefaultBgColorValues()
        {
             LocalColorInputs.BackgroundColorSettings.SetDefaultColorsValues();
        }

        private async Task GetDefaultActivityBarColorValues()
        {
            LocalColorInputs.ActivityColorSettings.SetAllColorsToDefaultValues();
        }

        private void ToggleActivityBarOperationalStatus() 
        {
            LocalColorInputs.ActivityColorSettings.EnableActivityBar = !LocalColorInputs.ActivityColorSettings.EnableActivityBar;
            if (LocalColorInputs.ActivityColorSettings.EnableActivityBar == true) 
            {
                ActivityBarControlsVisibility = "";
                LocalColorInputs.ActivityColorSettings.EnableActivityBar = true;
            }
            else 
            {
                ActivityBarControlsVisibility = ActivityBarHiddenHTMLKeyword;
                LocalColorInputs.ActivityColorSettings.EnableActivityBar = false;
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
                LocalColorInputs.ActivityColorSettings.ResetAllColorValuesExceptPrimaryHue();

                foreach (var control in ConnectedActivityStatusColorControls)
                    control.DisplayHueControlsOnly();
            }
        }

        private void MakeSimilarStageOneAndTwoColors()
        {
            LocalColorInputs.ActivityColorSettings.WorkColors.Last().Hue = LocalColorInputs.ActivityColorSettings.WorkColors.First().Hue;
            LocalColorInputs.ActivityColorSettings.ShortBreakColors.Last().Hue = LocalColorInputs.ActivityColorSettings.ShortBreakColors.First().Hue;
            LocalColorInputs.ActivityColorSettings.LongBreakColors.Last().Hue = LocalColorInputs.ActivityColorSettings.LongBreakColors.First().Hue;
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
