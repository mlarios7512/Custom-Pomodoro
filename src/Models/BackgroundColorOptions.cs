﻿using CustomPomodoro.Models.Helpers.BusinessLogic.PomTimer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models
{
    public class BackgroundColorOptions
    {
        public HSLColor NoActivityBgColor { get; set; } = HslColorSelection.GetDefaultNoActivityBgColor();
        public HSLColor NoActivityTextColor { get; set; } = HslColorSelection.GetDefaultTextColor();

        public HSLColor ActivityInProgressColor { get; set; } = HslColorSelection.GetDefaultActivityInProgressBgColor();
        public HSLColor ActivityInProgressTextColor { get; set; } = HslColorSelection.GetDefaultTextColor();

        public HSLColor PausedActivityColor { get; set; } = HslColorSelection.GetDefaultPausedActivityBgColor();
        public HSLColor PausedActivityTextColor { get; set; } = HslColorSelection.GetDefaultTextColor();

        public BackgroundColorOptions() { }

        public void SetDefaultColorsValues()
        {
            NoActivityBgColor = HslColorSelection.GetDefaultNoActivityBgColor();
            ActivityInProgressColor = HslColorSelection.GetDefaultActivityInProgressBgColor();
            PausedActivityColor = HslColorSelection.GetDefaultPausedActivityBgColor();

            NoActivityTextColor = HslColorSelection.GetDefaultTextColor();
            ActivityInProgressTextColor = HslColorSelection.GetDefaultTextColor();
            PausedActivityTextColor = HslColorSelection.GetDefaultTextColor();
        }

    }
}
