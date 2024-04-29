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

        private static string FormatHSLToCSS(HSLColor color) 
        {
            return $"hsl({color.Hue}, {color.Saturation}%, {color.Lightness}%)";
        } 
        public static string GetNoActivityBgColor(HSLColor? color = null)
        {
            if (color == null) 
                return FormatHSLToCSS(DefaultNoActivityBgColor);
            
            return FormatHSLToCSS((HSLColor)color);
        }
        public static string GetActivityInProgressBgColor(HSLColor? color = null)
        {
            if(color == null) 
                return FormatHSLToCSS((HSLColor)DefaultActivityInProgressBgColor);
            
            return FormatHSLToCSS((HSLColor)color);
        }

        public static string GetPausedActivityBgColor(HSLColor? color = null) 
        {
            if(color == null) 
                return FormatHSLToCSS((HSLColor)DefaultPausedActivityBgColor);

            return FormatHSLToCSS((HSLColor)color);
        }
    }

}
