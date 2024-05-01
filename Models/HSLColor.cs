using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models
{
    public class HSLColor
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

        public HSLColor() 
        {
        }

        public HSLColor(int hue, int saturation, int lightness)
        {
            Hue = hue;
            Saturation = saturation;
            Lightness = lightness;
        }

        private const int DefaultSaturation = 99;
        private const int DefaultLightBright = 44;
        private const int DefaultLightDim = 27;

        public void SetDefaultSaturationAndBrightLight() 
        {
            Saturation = DefaultSaturation;
            Lightness = DefaultLightBright;
        }
        public void SetDefaultSaturationAndDimLight()
        {
            Saturation = DefaultSaturation;
            Lightness = DefaultLightDim;
        }
    }
}
