using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings
{
    internal class HSLColor
    {
        private int _hue;

        public int Hue
        {
            get => _hue;
            set
            {
                if (value >= 0 && value <= 360)
                    _hue = value;
            }
        }

        private int _saturation;
        public int Saturation
        {
            get => _saturation;
            set
            {
                if (value >= 0 && value <= 100)
                    _saturation = value;
            }
        }

        private int _lightness;
        public int Lightness
        {
            get => _lightness;
            set
            {
                if (value >= 0 && value <= 100)
                    _lightness = value;
            }
        }

        public HSLColor(int hue, int saturation, int lightness)
        {
            Hue = hue;
            Saturation = saturation;
            Lightness = lightness;
        }
        //public static bool IsHSLValid(int hue, int saturation, int lightness)
        //{
        //    if (hue < 0 || hue > 360)
        //        return false;
        //    if (saturation < 0 || saturation > 100)
        //        return false;
        //    if (lightness < 0 || lightness > 100)
        //        return false;

        //    return true;
        //}
    }
}
