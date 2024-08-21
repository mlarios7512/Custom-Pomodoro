using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models
{
    public class PomodoroSet
    {
        public string Id { get; set; }
        public string Name { get; set; } = null;
        public string WorkTime { get; set; } = "25:00";
        public string ShortBreak { get; set; } = "5:00";
        public int RepsBeforeLongBreak { get; set; } = 3;
        public string LongBreak { get; set; } = "15:00";
    }
}
