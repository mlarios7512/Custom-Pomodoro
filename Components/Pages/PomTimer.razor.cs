﻿//using Android.OS;
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
            None = -1, //Used for "LastWorkState" when app first starts up.
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
        private WorkState LastWorkState { get; set; } = WorkState.None;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int CompletedWorkSessionCount { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            CountdownTimerDisplay = CurPomodoroSet.WorkTime;
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
        }




        //INCOMPLETE: Works fine for cases where the timer is running. Does NOT work in cases where the timer is "TimerState.NotStarted".
        public void NextSession()
        {
            if (NextWorkState == WorkState.Work)
            {

            }
            else
            {

            }
        }

        public void SetUpForTimerPropertiesForWork(PomodoroSet pomoSet, bool setAsCurSession)
        {
            if (setAsCurSession == true)
            {
                CurWorkStateDisplay = "Current session: Work";
            }
            else
            {
                CurWorkStateDisplay = "Next session: Work";
            }
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(pomoSet.WorkTime);
            CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.WorkTime);

            if (setAsCurSession == true)
            {
                MainTimerState = TimerState.Started;
            }
            else
            {
                MainTimerState = TimerState.NotStarted;
            }
            //MainTimerState = TimerState.Started;

            ////May require testing.
            //if (SessionCount > CurPomodoroSet.RepsBeforeLongBreak)
            //    NextWorkState = WorkState.LongBreak;
            //else
            //    NextWorkState = WorkState.ShortBreak;
        }

        public void SetUpTimerPropertiesForCorrectBreak(bool setAsCurSession)
        {
            bool? NeedSetUpShortSession = null;
            if (setAsCurSession == false)
            {
                CurWorkStateDisplay = "Next session: ";
                if (CompletedWorkSessionCount + 1 >= CurPomodoroSet.RepsBeforeLongBreak)
                    NeedSetUpShortSession = false;
                else
                    NeedSetUpShortSession = true;

            }
            else
            {
                CurWorkStateDisplay = "Current session: ";

                //This "if-else" statement needs testing. Also, need to make use of this in test cases.
                if (CompletedWorkSessionCount + 1 >= CurPomodoroSet.RepsBeforeLongBreak)
                    NeedSetUpShortSession = false;
                else
                    NeedSetUpShortSession = true;
            }



            if (NeedSetUpShortSession == false)
            {
                if (setAsCurSession == false)
                    NextWorkState = WorkState.LongBreak;

                CurWorkStateDisplay += "Long break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.LongBreak);
                CompletedWorkSessionCount = CurPomodoroSet.RepsBeforeLongBreak;
            }
            else
            {
                if (setAsCurSession == false)
                    NextWorkState = WorkState.ShortBreak;

                CurWorkStateDisplay += "Short break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.ShortBreak);
            }

            if (setAsCurSession == false)
                MainTimerState = TimerState.NotStarted;
            else
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
            //if(NextWorkState != WorkState.Work) 
            //{
            //    NextWorkState = WorkState.Work;
            //}
            //else if (NextWorkState == WorkState.Work && SessionCount == 0)
            //{
            //    Debug.WriteLine("Reached it");
            //    NextWorkState = WorkState.LongBreak;
            //}
            //else if(NextWorkState == WorkState.Work && SessionCount > CurPomodoroSet.RepsBeforeLongBreak)
            //{
            //    NextWorkState = WorkState.ShortBreak;
            //}

            //EndSessionAndTimer(CurPomodoroSet);
        }

        public async Task EndSessionAndTimer(PomodoroSet pomoSet)
        {
            //MainTimerState = TimerState.NotStarted;
            //ActualCountdownTimer.Enabled = false;
            //BgColor = PomTimerHelpers.TransitionToColor(NoActivityBgColor);

            ////The original (and working version was: if (NextWorkState == WorkState.Work)
            //if (NextWorkState != WorkState.Work)
            //{

            //   SetUpTimerPropertiesForCorrectBreak(CurPomodoroSet, false);


            //}
            //else {
            //    NextWorkState = WorkState.Work;

            //    CurWorkStateDisplay = "Next session: Work";
            //    TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
            //    CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.WorkTime);

            //    SessionCount--;
            //}
            //MainTimerState = TimerState.NotStarted;

        }

        public async Task StartTimer(PomodoroSet pomoSet)
        {
            if (NextWorkState == WorkState.Work &&
                (LastWorkState == WorkState.None || LastWorkState == WorkState.LongBreak))
            {
                //start work session (do NOT increment work session count or setup any time!)
                LastWorkState = WorkState.Work;

                if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                    NextWorkState = WorkState.ShortBreak;
                else
                    NextWorkState = WorkState.LongBreak;
            }
            else
            {
                if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                    LastWorkState = WorkState.ShortBreak;
                else
                    LastWorkState = WorkState.LongBreak;

                NextWorkState = WorkState.Work;
            }


            ActualCountdownTimer = new System.Timers.Timer(1000);
            ActualCountdownTimer.Enabled = true;
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Elapsed += CountDownTimer;
        }

        public string GetCountdownTimerDisplay(string workTime)
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
                        SetUpForTimerPropertiesForWork(CurPomodoroSet, false);


                        if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                            LastWorkState = WorkState.ShortBreak;
                        else
                            LastWorkState = WorkState.LongBreak;
                    }

                    else if (NextWorkState == WorkState.ShortBreak)
                    {
                        SetUpTimerPropertiesForCorrectBreak(false);
                        LastWorkState = WorkState.ShortBreak;
                        CompletedWorkSessionCount++;
                        //Next work state is still a short break. (We set it from the start of the timer in case the user skips that session).
                    }

                    else if (NextWorkState == WorkState.LongBreak)
                    {
                        SetUpTimerPropertiesForCorrectBreak(false);
                        LastWorkState = WorkState.Work;
                        CompletedWorkSessionCount++;
                        //Next work state is still a long break. (We set it from the start of the timer in case the user skips that session).
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
