using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomPomodoro.Components.Pages.PomTimer;

namespace CustomPomodoro.Models.Helpers
{
    internal class PomTimerHelpers
    {
        public static int GetEndTimeInSecondsFormat(string workTimeInput)
        {
            string[] workTimeAsArray = workTimeInput.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(workTimeAsArray[0]) * SECONDS_PER_MINUTE) + int.Parse(workTimeAsArray[1]);
            return WorkTimeInTotalSeconds;
        }
    }
}
