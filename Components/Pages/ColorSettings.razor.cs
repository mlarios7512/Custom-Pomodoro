using CustomPomodoro.Components.Reusable;
using CustomPomodoro.Models.UserSettings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class ColorSettings
    {
        private BackgroundColorSettings BgColorInputs = new BackgroundColorSettings();
        private ActivityBarSettings ColorBarInputs { get; set; } = new ActivityBarSettings();
        private bool DisplayAdvancedColorSettings { get; set; } = false;
        private List<LinkedHSLControl> PrimaryActivityStatusColorControls { get; set; } = new List<LinkedHSLControl>(3)
        {
            new LinkedHSLControl(),
            new LinkedHSLControl(),
            new LinkedHSLControl()
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
            ColorBarInputs.SetAllColorsToDefaultValues();
        }

        public void Cha() 
        {
            Debug.WriteLine($"No Activity BgColor\n" +
                $"Hue: {BgColorInputs.NoActivityBgColor.Hue}\n" +
                $"Saturation: {BgColorInputs.NoActivityBgColor.Saturation}\n" +
                $"Lightness: {BgColorInputs.NoActivityBgColor.Lightness}\n");

            //See the "Preferences API for saving user data (NOT SURE THIS IS THE BEST WAY for cross-platform saving. NEED TO VERIFY):
            //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows

            

        }

        private void ToggleAdvancedSettings()
        {
            DisplayAdvancedColorSettings = !DisplayAdvancedColorSettings;
            if (DisplayAdvancedColorSettings == true)
            {
                foreach (var control in PrimaryActivityStatusColorControls)
                    control.DisplayAllHslColorControls();
                
                foreach (var control in SecondaryActivityStatusColorControls)
                    control.DisplayAllHslColorControls();   
            }
            else
            {
                ColorBarInputs.ResetAllColorValuesExceptPrimaryHue();
                foreach (var control in PrimaryActivityStatusColorControls) 
                {
                    control.DisplayHueControlsOnly();
                }
                foreach(var control in SecondaryActivityStatusColorControls) 
                {
                    control.HideAllHslControls();
                }
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
            if(DisplayAdvancedColorSettings == false) 
            {
                MakeSimilarStageOneAndTwoColors();
            }
            
        }
    }
}
