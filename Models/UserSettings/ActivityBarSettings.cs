using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings
{
    public class ActivityBarSettings
    {
        public List<HSLColor> WorkColors { get; set; } = new List<HSLColor>() { new HSLColor(0,0,0), new HSLColor(0, 0, 0) };
        public List<HSLColor> ShortBreakColors { get; set; } = new List<HSLColor>() { new HSLColor(0,0,0), new HSLColor(0,0,0) };
        public List<HSLColor> LongBreakColors { get; set; } = new List<HSLColor>() { new HSLColor(0,0,0), new HSLColor(0, 0, 0) };


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
            WorkColors.First().SetDefaultSaturationAndBrightLight();
            WorkColors.Last().SetDefaultSaturationAndDimLight();

            ShortBreakColors.First().SetDefaultSaturationAndBrightLight();
            ShortBreakColors.Last().SetDefaultSaturationAndDimLight();

            LongBreakColors.First().SetDefaultSaturationAndBrightLight();
            LongBreakColors.Last().SetDefaultSaturationAndDimLight();
        }
    }
}
