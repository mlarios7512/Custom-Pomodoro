using CustomPomodoro.Models.Helpers.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings
{
    public class ActivityBarSettings
    {
        public List<HSLColor> WorkColors { get; set; } =ActivityBarColorHelpers.GetDefaultWorkColors();
        public List<HSLColor> ShortBreakColors { get; set; } = ActivityBarColorHelpers.GetDefaultShortBreakColors();
        public List<HSLColor> LongBreakColors { get; set; } =  ActivityBarColorHelpers.GetDefaultLongBreakColors();


        public ActivityBarSettings()
        {
            
        }
        public void Save() 
        {
            
        }

        public void Load() 
        {
            
        }
        public void Update() 
        {

        }



        public void ChooseSaturationAndLightForUser() 
        {
            ActivityBarColorHelpers.SetDefaultWorkColors(WorkColors);
            ActivityBarColorHelpers.SetDefaultShortBreakColors(ShortBreakColors);
            ActivityBarColorHelpers.SetDefaultLongBreakColors(LongBreakColors);

            //WorkColors.First().SetDefaultSaturationAndBrightLight();
            //WorkColors.Last().SetDefaultSaturationAndDimLight();

            //ShortBreakColors.First().SetDefaultSaturationAndBrightLight();
            //ShortBreakColors.Last().SetDefaultSaturationAndDimLight();

            //LongBreakColors.First().SetDefaultSaturationAndBrightLight();
            //LongBreakColors.Last().SetDefaultSaturationAndDimLight();
        }
    }
}
