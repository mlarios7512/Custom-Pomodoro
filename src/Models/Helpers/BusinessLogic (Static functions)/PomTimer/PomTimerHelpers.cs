using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static CustomPomodoro.Components.Pages.PomTimer;
using static Microsoft.Maui.ApplicationModel.Permissions;

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

        public static string PrintCountdownTimer(int timeInSeconds)
        {
            string formattedInput = $"{TimeSpan.FromSeconds(timeInSeconds):mm\\:ss}";
            if (formattedInput.Length == 5)
            {
                if (formattedInput.First() == '0')
                    formattedInput = formattedInput.Substring(1);
            }

            return formattedInput;
        }
    }
}
