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
            return $"{TimeSpan.FromSeconds(timeInSeconds):mm\\:ss}";
        }

        //The "hexColor" input will eventually be input via "CurPomodoroSet" for the class "PomTimer".
        public static string TransitionToColor(string? hexColor)
        {
            Regex HexColorVerification = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$");
            if(String.IsNullOrEmpty(hexColor) == false) 
            {
                if (HexColorVerification.IsMatch(hexColor) == true) 
                    return hexColor;
                
            }
            return "#44403c";
        }
    }
}
