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

        private const string NoActivityBgColor = "#18181b";
        private const string WorkBgColor = "#a12a4e"; //de0043 //#991b1b
        private const string ShortBreakBgColor = "#2e1065";  
        private const string LongBreakBgColor = "#0369a1";


        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomodoroSet CurPomodoroSet { get; set; } = new();
        private string BgColor = NoActivityBgColor;
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
        public void NextSession()
        {
            if (NextWorkState == WorkState.Work || SessionCount <= 0)
            {
                ++SessionCount;
            }
            //else if (NextWorkState == WorkState.ShortBreak)
            //{
            //    //++SessionCount;
            //}
            if(NextWorkState == WorkState.LongBreak)
            {
                SessionCount = 0;
            }

            EndSessionAndTimer(CurPomodoroSet);
        }

        public void SetUpForTimerPropertiesForWork(PomodoroSet pomoSet) 
        {
            CurWorkStateDisplay = "Current session: Work";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(pomoSet.WorkTime);
            CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);
            MainTimerState = TimerState.Started;
            SessionCount++;

            if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                NextWorkState = WorkState.LongBreak;
            else
                NextWorkState = WorkState.ShortBreak;
        }

        public void SetUpTimerPropertiesForCorrectBreak(PomodoroSet pomoSet) 
        {
            NextWorkState = WorkState.Work;
            if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
            {
                CurWorkStateDisplay = "Current session: Long break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.LongBreak);
                SessionCount = 0;
            }
            else
            {
                CurWorkStateDisplay = "Current session: Short break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);
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
        public async Task EndSessionAndMakeItRepeatable() 
        {
            if(NextWorkState != WorkState.Work) 
            {
                NextWorkState = WorkState.Work;
            }
            else if (NextWorkState == WorkState.Work && SessionCount == 0)
            {
                Debug.WriteLine("Reached it");
                NextWorkState = WorkState.LongBreak;
            }
            else if(NextWorkState == WorkState.Work && SessionCount > 0) 
            {
                NextWorkState = WorkState.ShortBreak;
            }
           
            EndSessionAndTimer(CurPomodoroSet);
        }

        public async Task EndSessionAndTimer(PomodoroSet pomoSet) 
        {
            MainTimerState = TimerState.NotStarted;
            ActualCountdownTimer.Enabled = false;
            BgColor = PomTimerHelpers.TransitionToColor(NoActivityBgColor);

            //The original (and working version was: if (NextWorkState == WorkState.Work)
            if (NextWorkState != WorkState.Work)
            {
                if (NextWorkState == WorkState.LongBreak)
                {
                    Debug.WriteLine("Long break started.");
                    NextWorkState = WorkState.LongBreak;

                    CurWorkStateDisplay = "Next session: Long break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                    CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.LongBreak);

                    SessionCount = CurPomodoroSet.RepsBeforeLongBreak;
                }
                else if(NextWorkState == WorkState.ShortBreak)
                {
                    Debug.WriteLine("Short break started");
                    NextWorkState = WorkState.ShortBreak;

                    CurWorkStateDisplay = "Next session: Short break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                    CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);

                }
            }
            else {
                NextWorkState = WorkState.Work;
 
                CurWorkStateDisplay = "Next session: Work";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
                CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);

                SessionCount--;
            }
            MainTimerState = TimerState.NotStarted;

        }

        public async Task StartTimer(PomodoroSet pomoSet)
        {
            if (NextWorkState != WorkState.Work)
            {
                NextWorkState = WorkState.Work;
                if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                {
                    CurWorkStateDisplay = "Current session: Long break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                    SessionCount = 0;
                    
                    BgColor = PomTimerHelpers.TransitionToColor(LongBreakBgColor);
                }
                else
                {
                    CurWorkStateDisplay = "Current session: Short break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                    BgColor = PomTimerHelpers.TransitionToColor(ShortBreakBgColor);
                }
                MainTimerState = TimerState.Started;
            }
            else 
            {
                SetUpForTimerPropertiesForWork(pomoSet);
                BgColor = PomTimerHelpers.TransitionToColor(WorkBgColor);
            }
            
            ActualCountdownTimer = new System.Timers.Timer(1000);
            ActualCountdownTimer.Enabled = true;
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Elapsed += CountDownTimer;
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
                    CountdownTimerDisplay = PomTimerHelpers.PrintCountdownTimer(TimerInSeconds);
                }
                else
                {
                    ActualCountdownTimer.Enabled = false;
                    BgColor = PomTimerHelpers.TransitionToColor(NoActivityBgColor);
                    if (NextWorkState == WorkState.Work) 
                    {
                        CurWorkStateDisplay = "Next session: Work";
                        CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.WorkTime);
                    }
                       
                    else if (NextWorkState == WorkState.ShortBreak) 
                    {
                        CurWorkStateDisplay = "Next session: Short break";
                        CountdownTimerDisplay = SetCountdownTimer(CurPomodoroSet.ShortBreak);
                    }

                    else if(NextWorkState == WorkState.LongBreak) 
                    {
                        CurWorkStateDisplay = "Next session: Long break";
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
