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

        //protected override async Task OnInitializedAsync() 
        //{ 
            
        //}

        public enum TimerState 
        {
            NotStarted = -1,
            Paused = 0,
            Started = 1
        }
        //Enum below not used yet. Just a thought.
        public enum WorkState 
        {
            ShortBreak = 0,
            LongBreak = 1,
            Work = 2
        }

        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public string CurWorkStateDisplay { get; set; } = "Next session: Work";  //"Work", "Short Break", or "Long Break".
        public int TimerInSeconds { get; set; } = 0;
        public TimerState MainTimerState { get; set; } = TimerState.NotStarted;
        private WorkState NextWorkState { get; set; } = WorkState.Work;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int SessionCount { get; set; } = 0;

        public async Task PauseTimer(PomoderoSet PomoSet) 
        {
            MainTimerState = TimerState.Paused;
            ActualCountdownTimer.Enabled = false;
            //ActualCountdownTimer.Elapsed += null;
        }
        public async Task ContinueTimer(PomoderoSet PomoSet) 
        {
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Enabled = true;
            //ActualCountdownTimer.Elapsed += CountDownTimer;
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
                }
                else
                {
                    CurWorkStateDisplay = "Current session: Short break";
                    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                }
                MainTimerState = TimerState.Started;
            }
            else 
            {
                CurWorkStateDisplay = "Current session: Work";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(pomoSet.WorkTime);
                MainTimerState = TimerState.Started;
                SessionCount++;

                if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                    NextWorkState = WorkState.LongBreak;
                else
                    NextWorkState = WorkState.ShortBreak;

           
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

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e) 
        {
            if (MainTimerState == TimerState.Started)
            {
                if (TimerInSeconds > 0)
                {
                    TimerInSeconds--;
                }
                else
                {
                    ActualCountdownTimer.Enabled = false;

                    if (NextWorkState == WorkState.Work)
                        CurWorkStateDisplay = "Next session: Work";
                    else if (NextWorkState == WorkState.ShortBreak)
                        CurWorkStateDisplay = "Next session: Short break";
                    else if(NextWorkState == WorkState.LongBreak)
                        CurWorkStateDisplay = "Next session: Long break";

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
