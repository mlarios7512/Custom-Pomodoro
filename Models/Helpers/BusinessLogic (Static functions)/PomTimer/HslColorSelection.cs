using CustomPomodoro.Models.UserSettings;
using Microsoft.Maui.ApplicationModel.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.BusinessLogic.PomTimer
{
    internal class HslColorSelection
    {
        private static string FormatHSLToCSS(HSLColor color)
        {
            return $"hsl({color.Hue}, {color.Saturation}%, {color.Lightness}%)";
        }
        public static string GetNoActivityBgColor(HSLColor? color = null)
        {
            HSLColor DefaultNoActivityBgColor = new HSLColor(240, 4, 46);

            if (color == null)
                return FormatHSLToCSS(DefaultNoActivityBgColor);

            return FormatHSLToCSS(color);
        }
        public static string GetActivityInProgressBgColor(HSLColor? color = null)
        {
            HSLColor DefaultActivityInProgressBgColor = new HSLColor(240, 6, 10);

            if (color == null)
                return FormatHSLToCSS(DefaultActivityInProgressBgColor);

            return FormatHSLToCSS(color);
        }

        public static string GetPausedActivityBgColor(HSLColor? color = null)
        {
            HSLColor DefaultPausedActivityBgColor = new HSLColor(202, 96, 22);

            if (color == null)
                return FormatHSLToCSS(DefaultPausedActivityBgColor);

            return FormatHSLToCSS(color);
        }

        public static HSLColor GetDefaultNoActivityBgColor()
        {
            return new HSLColor(0, 0, 0);
        }
        public static HSLColor GetDefaultActivityInProgressBgColor()
        {
            return new HSLColor(0, 0, 0);
        }

        public static HSLColor GetDefaultPausedActivityBgColor()
        {
            return new HSLColor(207, 100, 26);
        }
    }

}
