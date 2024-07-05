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
        private static string FormatHSLToCSS(HSLColor color) 
        {
            return $"hsl({color.Hue}, {color.Saturation}%, {color.Lightness}%)";
        } 
        public static string GetNoActivityBgColor(HSLColor? color = null)
        {
            HSLColor DefaultNoActivityBgColor = new HSLColor(240, 4, 46);

            if (color == null) 
                return FormatHSLToCSS(DefaultNoActivityBgColor);
            
            return FormatHSLToCSS((HSLColor)color);
        }
        public static string GetActivityInProgressBgColor(HSLColor? color = null)
        {
            HSLColor DefaultActivityInProgressBgColor = new HSLColor(240, 6, 10);

            if (color == null) 
                return FormatHSLToCSS((HSLColor)DefaultActivityInProgressBgColor);
            
            return FormatHSLToCSS((HSLColor)color);
        }

        public static string GetPausedActivityBgColor(HSLColor? color = null) 
        {
            HSLColor DefaultPausedActivityBgColor = new HSLColor(202, 96, 22);

            if (color == null) 
                return FormatHSLToCSS((HSLColor)DefaultPausedActivityBgColor);

            return FormatHSLToCSS((HSLColor)color);
        }

        /*----------------*/

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



        private const int BarActivityStatusSaturation = 99;
        private const int BarActivityStatusLightnessDim = 27;
        private const int BarActivityStatusLightnessBright = 44;
        
        private static (string,string) GetDefaultOrUserDefinedBarColors(int adminDefaultColor, HSLColor? colorOne, HSLColor? colorTwo ) 
        {
            HSLColor DefaultStatusColorBright = new HSLColor(
               adminDefaultColor,
               BarActivityStatusSaturation,
               BarActivityStatusLightnessBright);

            HSLColor DefaultStatusColorDim = new HSLColor(
                adminDefaultColor,
                BarActivityStatusSaturation,
                BarActivityStatusLightnessDim);

            string colorOneAsHSL = string.Empty;
            string colorTwoAsHSL = string.Empty;
            if (colorOne == null)
                colorOneAsHSL = FormatHSLToCSS(DefaultStatusColorBright);
            else
                colorOneAsHSL = FormatHSLToCSS(colorOne);

            if (colorTwo == null)
                colorTwoAsHSL = FormatHSLToCSS(DefaultStatusColorDim);
            else
                colorTwoAsHSL = FormatHSLToCSS(colorTwo);

            return (colorOneAsHSL, colorTwoAsHSL);
        }

        //If we decide to limit the HSL color options for "activity status bar", we do it from the
        //input form's backend (to avoid limiting potential reuse).
        public static (string,string) GetWorkStatusBarColor(HSLColor? colorOne = null, HSLColor? colorTwo = null) 
        {
            return GetDefaultOrUserDefinedBarColors(0, colorOne, colorTwo);
        }

        public static (string,string) GetShortBreakStatusBarColor(HSLColor? colorOne = null, HSLColor? colorTwo = null)
        {
            return GetDefaultOrUserDefinedBarColors(191, colorOne, colorTwo);
        }

        public static (string, string) GetLongBreakStatusBarColor(HSLColor? colorOne = null, HSLColor? colorTwo = null)
        {
            return GetDefaultOrUserDefinedBarColors(121, colorOne, colorTwo);
        }
    }

}
