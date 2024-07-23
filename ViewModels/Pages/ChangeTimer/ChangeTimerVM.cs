using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using CustomPomodoro.ViewModels.InputClones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.ViewModels.Pages.ChangeTimer
{
    public partial class ChangeTimerVM
    {

        public PomTimerSetInputClone PomSetInput { get; set; } = new();

        //Platform specific property inputs - below: (assumes these inputs will be stored within MAUI "preferences"):

        //Android
        public bool VibrateOnSessionEnd { get; set; } = false;

        public static void DummyPomSetInputToRealPomSet(PomTimerSetInputClone userInput, ref PomodoroSet pomSet)
        {
            int workTimeInSeconds = Int32.Parse(userInput.WT_Minutes) * 60;
            workTimeInSeconds += Int32.Parse(userInput.WT_Seconds);
            pomSet.WorkTime = PomTimerHelpers.PrintCountdownTimer(workTimeInSeconds);

            int shortBreakInSeconds = Int32.Parse(userInput.SBT_Minutes) * 60;
            shortBreakInSeconds += Int32.Parse(userInput.SBT_Seconds);
            pomSet.ShortBreak = PomTimerHelpers.PrintCountdownTimer(shortBreakInSeconds);

            int longBreakInSeconds = Int32.Parse(userInput.LBT_Seconds) * 60;
            longBreakInSeconds += Int32.Parse(userInput.LBT_Seconds);
            pomSet.LongBreak = PomTimerHelpers.PrintCountdownTimer(longBreakInSeconds);

            pomSet.RepsBeforeLongBreak = userInput.RepsBeforeLongBreak;
        }
    }
}
