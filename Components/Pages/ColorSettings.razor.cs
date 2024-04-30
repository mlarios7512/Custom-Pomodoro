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
        ActivityBarSettings BarSettings { get; set; } = new ActivityBarSettings();

        public void Cha() 
        {
            Debug.WriteLine($"{BarSettings.WorkColors.First().Hue}");
        }
    }
}
