//using Android.OS;
using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class PomTimer
    {
        public enum TimerState 
        {
            NotStarted = -1,
            Paused = 0,
            Started = 1
        }

        public enum WorkState 
        {
            ShortBreak = 0,
            LongBreak = 1,
            Work = 2
        }

        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public string CurWorkStateDisplay { get; set; } = "Next session: Work";  //"Work", "Short Break", or "Long Break".
        public string CountdownTimerDisplay { get; set; } = "00:00";
        public int TimerInSeconds { get; set; } = 0;
        public TimerState MainTimerState { get; set; } = TimerState.NotStarted;
        private WorkState NextWorkState { get; set; } = WorkState.Work;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int SessionCount { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            CountdownTimerDisplay = CurPomodoroSet.WorkTime;
        }

        public void SetUpForTimerPropertiesForWork(PomoderoSet pomoSet) 
        {
            CurWorkStateDisplay = "Current session: Work";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(pomoSet.WorkTime);
            CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);  //UNTESTED LINE OF CODE.
            MainTimerState = TimerState.Started;
            SessionCount++;

            if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                NextWorkState = WorkState.LongBreak;
            else
                NextWorkState = WorkState.ShortBreak;
        }

        public void SetUpTimerPropertiesForCorrectBreak(PomoderoSet pomoSet) 
        {
            NextWorkState = WorkState.Work;
            if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
            {
                CurWorkStateDisplay = "Current session: Long break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.LongBreak);    //UNTESTED LINE OF CODE.
                SessionCount = 0;
            }
            else
            {
                CurWorkStateDisplay = "Current session: Short break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);   //UNTESTED LINE OF CODE.
            }
            MainTimerState = TimerState.Started;
        }

        public async Task PauseTimer() 
        {
            MainTimerState = TimerState.Paused;
            ActualCountdownTimer.Enabled = false;
        }
        public async Task ContinueTimer() 
        {
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Enabled = true;
        }

        public async Task EndSessionAndTimer(PomoderoSet pomoSet) 
        {
            MainTimerState = TimerState.NotStarted;
            ActualCountdownTimer.Enabled = false;
            BgColor = "#44403c";

            if (NextWorkState == WorkState.Work)
            {
                if (SessionCount == 0)
                {
                    Debug.WriteLine("Long break started.");
                    NextWorkState = WorkState.LongBreak;

                    CurWorkStateDisplay = "Next session: Long break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                    CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.LongBreak);     //UNTESTED LINE OF CODE.

                    SessionCount = CurPomodoroSet.RepsBeforeLongBreak;
                }
                else 
                {
                    Debug.WriteLine("Short break started");
                    NextWorkState = WorkState.ShortBreak;

                    CurWorkStateDisplay = "Next session: Short break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                    CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);   //UNTESTED LINE OF CODE.

                }
            }
            else {
                NextWorkState = WorkState.Work;
 
                CurWorkStateDisplay = "Next session: Work";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);     //UNTESTED LINE OF CODE.

                SessionCount--;
            }
            MainTimerState = TimerState.NotStarted;

        }

        public async Task StartTimer(PomoderoSet pomoSet)
        {
            if (NextWorkState != WorkState.Work)
            {
                NextWorkState = WorkState.Work;
                if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                {
                    CurWorkStateDisplay = "Current session: Long break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                    SessionCount = 0;
                    BgColor = "#0369a1";
                }
                else
                {
                    CurWorkStateDisplay = "Current session: Short break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                    BgColor = " #2e1065";
                }
                MainTimerState = TimerState.Started;
            }
            else 
            {
                //Tested, works correctly.
                SetUpForTimerPropertiesForWork(pomoSet);
                BgColor = "#991b1b";
            }
            
            ActualCountdownTimer = new System.Timers.Timer(1000);
            ActualCountdownTimer.Enabled = true;
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Elapsed += CountDownTimer;
        }

        public string PrintCountdownTimer()
        {
            return $"{TimeSpan.FromSeconds(TimerInSeconds):hh\\:mm\\:ss}";
        }

        public string SetCountdownTimer(string workTime) 
        {
            int WorkTimeInSec = PomTimerHelpers.GetEndTimeInSecondsFormat(workTime);
            return $"{TimeSpan.FromSeconds(WorkTimeInSec):hh\\:mm\\:ss}";
        }

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e) 
        {
            if (MainTimerState == TimerState.Started)
            {
                if (TimerInSeconds > 0)
                {
                    TimerInSeconds--;

                    //UNTESTED LINE (below):
                    CountdownTimerDisplay = PrintCountdownTimer();
                }
                else
                {
                    ActualCountdownTimer.Enabled = false;
                    BgColor = "#44403c";
                    if (NextWorkState == WorkState.Work) 
                    {
                        CurWorkStateDisplay = "Next session: Work";
                        //UNTESTED LINE (below):
                        CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);
                    }
                       
                    else if (NextWorkState == WorkState.ShortBreak) 
                    {
                        CurWorkStateDisplay = "Next session: Short break";

                        //UNTESTED LINE (below):
                        CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);
                    }

                    else if(NextWorkState == WorkState.LongBreak) 
                    {
                        CurWorkStateDisplay = "Next session: Long break";

                        //UNTESTED LINE (below):
                        CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.LongBreak);
                    }

                    MainTimerState = TimerState.NotStarted;
                }
            }
            else 
            {
                ActualCountdownTimer.Enabled = false;
            }            
            InvokeAsync(StateHasChanged);
        }
    }
}
