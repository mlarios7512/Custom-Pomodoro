using CustomPomodoro.Models.Helpers.Colors;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class ActivityBarSettings
    {
        public List<HSLColor> WorkColors { get; set; } = ActivityBarColorHelpers.GetDefaultWorkColors();
        public List<HSLColor> ShortBreakColors { get; set; } = ActivityBarColorHelpers.GetDefaultShortBreakColors();
        public List<HSLColor> LongBreakColors { get; set; } = ActivityBarColorHelpers.GetDefaultLongBreakColors();
        public bool EnableActivityBar { get; set; } = true;

        public ActivityBarSettings()
        {
            
        }
        public void SetAllColorsToDefaultValues()
        {
            ActivityBarColorHelpers.SetDefaultWorkColors(WorkColors);
            ActivityBarColorHelpers.SetDefaultShortBreakColors(ShortBreakColors);
            ActivityBarColorHelpers.SetDefaultLongBreakColors(LongBreakColors);
        }

        public void ResetAllColorValuesExceptPrimaryHue()
        {

            ActivityBarColorHelpers.SetDefaultSaturationAndBrightLight(WorkColors.First());
            ActivityBarColorHelpers.SetDefaultSaturationAndBrightLight(ShortBreakColors.First());
            ActivityBarColorHelpers.SetDefaultSaturationAndBrightLight(LongBreakColors.First());


            ActivityBarColorHelpers.SetDefaultSaturationAndDimLight(WorkColors.Last());
            ActivityBarColorHelpers.SetDefaultSaturationAndDimLight(ShortBreakColors.Last());
            ActivityBarColorHelpers.SetDefaultSaturationAndDimLight(LongBreakColors.Last());

            WorkColors.Last().Hue = WorkColors.First().Hue;
            ShortBreakColors.Last().Hue = ShortBreakColors.First().Hue;
            LongBreakColors.Last().Hue = LongBreakColors.First().Hue;
        }
    }
}
