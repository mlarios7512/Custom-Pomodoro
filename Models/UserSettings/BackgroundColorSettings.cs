using CustomPomodoro.Models.Helpers.PomTimer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings
{
    public class BackgroundColorSettings
    {
        public HSLColor NoActivityBgColor { get; set; } = HslColorSelection.GetDefaultNoActivityBgColor();
        public HSLColor ActivityInProgressColor { get; set; } = HslColorSelection.GetDefaultActivityInProgressBgColor();
        public HSLColor PausedActivityColor { get; set; } = HslColorSelection.GetDefaultPausedActivityBgColor();
        public BackgroundColorSettings() { }
    }
}
