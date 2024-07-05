using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.Colors
{
    public class ActivityBarColorHelpers
    {
        private const int DefaultWorkHue = 0;
        private const int DefaultShortBreakHue = 121;
        private const int DefaultLongBreakHue = 60;

        private const int DefaultSaturation = 99;
        private const int DefaultLightBright = 44;
        private const int DefaultLightDim = 27;

        public static void SetDefaultSaturationAndBrightLight(HSLColor color)
        {
            color.Saturation = DefaultSaturation;
            color.Lightness = DefaultLightBright;
        }
        public static void SetDefaultSaturationAndDimLight(HSLColor color)
        {
            color.Saturation = DefaultSaturation;
            color.Lightness = DefaultLightDim;
        }

        public static List<HSLColor> GetDefaultWorkColors() 
        {
            List<HSLColor> WorkColors = new List<HSLColor>();

            HSLColor color = new HSLColor(DefaultWorkHue, 0,0);
            SetDefaultSaturationAndBrightLight(color);
            WorkColors.Add(color);

            color = new HSLColor(DefaultWorkHue, 0,0);
            SetDefaultSaturationAndDimLight(color);
            WorkColors.Add(color);
            return WorkColors;
        }

        public static List<HSLColor> GetDefaultShortBreakColors() 
        {
            List<HSLColor> WorkColors = new List<HSLColor>();

            HSLColor color = new HSLColor(DefaultShortBreakHue, 0, 0);
            SetDefaultSaturationAndBrightLight(color);
            WorkColors.Add(color);

            color = new HSLColor(DefaultShortBreakHue, 0, 0);
            SetDefaultSaturationAndDimLight(color);
            WorkColors.Add(color);
            return WorkColors;
        }

        //"Get" functions
        public static List<HSLColor> GetDefaultLongBreakColors() 
        {
            List<HSLColor> WorkColors = new List<HSLColor>();

            HSLColor color = new HSLColor(DefaultLongBreakHue, 0, 0);
            SetDefaultSaturationAndBrightLight(color);
            WorkColors.Add(color);

            color = new HSLColor(DefaultLongBreakHue, 0, 0);
            SetDefaultSaturationAndDimLight(color);
            WorkColors.Add(color);
            return WorkColors;
        }

        //"Set" functions
        public static void SetDefaultWorkColors(List<HSLColor> workColors) 
        {
            workColors.First().Hue = DefaultWorkHue;
            workColors.Last().Hue = DefaultWorkHue;
            SetDefaultSaturationAndBrightLight(workColors.First());
            SetDefaultSaturationAndDimLight(workColors.Last());
        }

        public static void SetDefaultShortBreakColors( List<HSLColor> shortBreakColors) 
        {
            shortBreakColors.First().Hue = DefaultShortBreakHue;
            shortBreakColors.Last().Hue = DefaultShortBreakHue;
            SetDefaultSaturationAndBrightLight(shortBreakColors.First());
            SetDefaultSaturationAndDimLight (shortBreakColors.Last());
        }

        public static void SetDefaultLongBreakColors( List<HSLColor> longBreakColors) 
        {
            longBreakColors.First().Hue = DefaultLongBreakHue;
            longBreakColors.Last().Hue = DefaultLongBreakHue;
            SetDefaultSaturationAndBrightLight(longBreakColors.First());
            SetDefaultSaturationAndDimLight(longBreakColors.Last());
        }

        public static List<string> TransformHSLListToCSSCompatibleStringList(List<HSLColor> hslColors)
        {
            List<string> result = new();
            foreach (HSLColor color in hslColors)
            {
                result.Add($"hsl({color.Hue} {color.Saturation}% {color.Lightness}%)");
            }
            return result;
        }
    }
}

