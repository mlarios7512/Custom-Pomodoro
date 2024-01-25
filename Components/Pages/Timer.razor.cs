using CustomPomodoro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class Timer
    {
        public PomoderoSet PomoderoSet { get; set; }
        public int SessionCount { get; set; }

        public TimeSpan GetEndTime() 
        {
            string[] WorkTime =  PomoderoSet.WorkTime.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(WorkTime[0]) * SECONDS_PER_MINUTE) + int.Parse(WorkTime[1]);
            return TimeSpan.FromSeconds(WorkTimeInTotalSeconds);
        }
    }
}
