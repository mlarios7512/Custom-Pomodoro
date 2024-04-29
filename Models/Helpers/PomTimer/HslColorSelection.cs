using CustomPomodoro.Models.UserSettings;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PomTimer
{
    internal class HslColorSelection
    {
        private static readonly HSLColor DefaultNoActivityBgColor = new HSLColor(240, 4, 46);
        private static readonly HSLColor DefaultActivityInProgressBgColor = new HSLColor(240, 6, 10);
        private static readonly HSLColor DefaultPausedActivityBgColor = new HSLColor(202, 96, 22);


        public static string GetNoActivityHLSColor(HSLColor? color = null)
        {

            if (color == null) 
            {
                return $"hsl({DefaultNoActivityBgColor.Hue}, " +
                    $"{DefaultNoActivityBgColor.Saturation}, " +
                    $"{DefaultNoActivityBgColor.Lightness})";
            }

            return $"hsl({color.Hue}" +
                $"{color.Saturation}" +
                $"{color.Lightness})";
        }
        public static string GetActivityInProgressHSLColor(HSLColor? color = null)
        {
            if(color == null) 
            {
                return $"{DefaultActivityInProgressBgColor.Hue}" +
                    $"{DefaultActivityInProgressBgColor.Saturation}" +
                    $"{DefaultActivityInProgressBgColor.Lightness})";
            }

            return $"hsl({color.Hue}" +
                $"{color.Saturation}" +
                $"{color.Lightness})";
        }

        public static string GetPausedActivity(HSLColor? color = null) 
        {
            if(color == null) 
            {
                return $"hsl({DefaultPausedActivityBgColor.Hue}" +
                    $"{DefaultPausedActivityBgColor.Saturation}" +
                    $"{DefaultPausedActivityBgColor.Lightness})";
            }

            return $"hsl({color.Hue}" +
                $"{color.Saturation}" +
                $"{color.Lightness})";
        }
    }

}
