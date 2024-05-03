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
        private List<HSLControl> PrimaryActivityStatusColorControls { get; set; } = new List<HSLControl>(3)
        {
            new HSLControl(),
            new HSLControl(),
            new HSLControl()
        };
        private List<HSLControl> SecondaryActivityStatusColorControls { get; set; } = new List<HSLControl>(3) 
        {
            new HSLControl(),
            new HSLControl(),
            new HSLControl()
        };
        private List<HSLControl> BgColorControls { get; set; } = new List<HSLControl>()
        {
            new HSLControl(),
            new HSLControl(),
            new HSLControl()
        };

        protected override async Task OnInitializedAsync()
        {
            ColorBarInputs.SetAllColorsToDefaultValues();
        }

        public void Cha() 
        {
            Debug.WriteLine($"{ColorBarInputs.WorkColors.First().Hue}");
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
