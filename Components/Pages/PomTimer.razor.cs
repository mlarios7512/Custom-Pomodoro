//using Android.OS;
using CustomPomodoro.Models;
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
            Interrupted = -1,
            ShortBreak = 0,
            LongBreak = 1,
            Working = 2
        }

        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public string CurWorkStateDisplay { get; set; } = "Next session: Work";  //"Work", "Short Break", or "Long Break".
        public int TimerInSeconds { get; set; } = 0;
        public TimerState MainTimerState { get; set; } = TimerState.NotStarted;
        private WorkState LastWorkState { get; set; } = WorkState.Interrupted;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int SessionCount { get; set; } = 0;

        public int GetEndTimeInSecondsFormat(string workTimeInput)
        {
            string[] workTimeAsArray =  workTimeInput.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(workTimeAsArray[0]) * SECONDS_PER_MINUTE) + int.Parse(workTimeAsArray[1]);
            return WorkTimeInTotalSeconds;
        }

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

        public async Task StartTimer(PomoderoSet PomoSet)
        {
            //Start timer countdown after obtaining it.
            if(LastWorkState != WorkState.Working) 
            {
                CurWorkStateDisplay = "Current session: Work";
                TimerInSeconds = GetEndTimeInSecondsFormat(PomoSet.WorkTime);
                MainTimerState = TimerState.Started;
                LastWorkState = WorkState.Working;
                SessionCount++;
            }
            else if(LastWorkState == WorkState.Working) 
            {
                if (SessionCount >= CurPomodoroSet.RepsBeforeLongBreak) 
                {
                    CurWorkStateDisplay = "Current session: Long break";
                    TimerInSeconds = GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                    LastWorkState = WorkState.LongBreak;
                    MainTimerState = TimerState.Started;
                }
                else 
                {
                    CurWorkStateDisplay = "Current session: Short break";
                    TimerInSeconds = GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                    LastWorkState = WorkState.ShortBreak;
                    MainTimerState = TimerState.Started;
                }
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

                    //Need to reset timer to "worktime".
                    if (SessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                    {
                        CurWorkStateDisplay = "Next session: Short break";
                        MainTimerState = TimerState.NotStarted;
                    }
                    else if (SessionCount == CurPomodoroSet.RepsBeforeLongBreak)
                    {
                        SessionCount = 0;
                        CurWorkStateDisplay = "Next session: Long break";
                        MainTimerState = TimerState.NotStarted;
                    }
                }
            }
            else 
            {
                ActualCountdownTimer.Enabled = false;
                //Handle logic of resetting the timer in another function.
            }            
            InvokeAsync(StateHasChanged);
        }
    }
}
