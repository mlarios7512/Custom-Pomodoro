using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.InputClones
{
    public class PomTimerSetInputClone
    {
        public PomTimerSetInputClone() { }

        public string WT_Minutes { get; set; } = "25";
        public string WT_Seconds { get; set; } = "00";

        public string SBT_Minutes { get; set; } = "5";
        public string SBT_Seconds { get; set; } = "00";

        public int RepsBeforeLongBreak { get; set; } = 3;

        public string LBT_Minutes { get; set; } = "15";
        public string LBT_Seconds { get; set; } = "00";

    }
}
