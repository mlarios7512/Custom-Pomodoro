using CustomPomodoro.Models.Helpers.PomTimer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class BackgroundColorSettings
    {
        public HSLColor NoActivityBgColor { get; set; } = HslColorSelection.GetDefaultNoActivityBgColor();
        public HSLColor ActivityInProgressColor { get; set; } = HslColorSelection.GetDefaultActivityInProgressBgColor();
        public HSLColor PausedActivityColor { get; set; } = HslColorSelection.GetDefaultPausedActivityBgColor();
        public BackgroundColorSettings() { }

    }
}
