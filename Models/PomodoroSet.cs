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
        public string WorkTime { get; set; } = "0:05";
        public string ShortBreak { get; set; } = "0:02";
        public int RepsBeforeLongBreak { get; set; } = 2;
        public string LongBreak { get; set; } = "0:07";
    }
}
