//using Android.OS;
using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using CustomPomodoro.Models.Helpers.PomTimer;
using Microsoft.AspNetCore.Components;
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

        //0 == RED
        //266 == PURPLE
        //191 == AQUA
        //121 == GREEN
        private const int WorkBarColor = 0;
        private const int ShortBreakBarColor = 191;
        private const int LongBreakBarColor = 121;

        public bool ShouldNavBarBeHidden { get; set; } = false;

        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        [CascadingParameter]
        public PomodoroSet CurPomodoroSet { get; set; } = new();
        private string BgColor = HslColorSelection.GetNoActivityBgColor();
        public string[] AltWorkStateDisplay { get; set; } = { "Next session: ", "Work" };
        public string CountdownTimerDisplay { get; set; } = "00:00";
        public int TimerInSeconds { get; set; } = 0;
        public TimerState MainTimerState { get; set; } = TimerState.NotStarted;
        private WorkState NextWorkState { get; set; } = WorkState.Work;
        private WorkState LastWorkState { get; set; } = WorkState.LongBreak;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int CompletedWorkSessionCount { get; set; } = 0;
        private int CurActivityBarColor = 0;

        public string IntilaizationTimeString { get; set; } = "";
        public string CurNavBarDisplay { get; set; } = "block";
        public string CurNavBarVisibility { get; set; } = "visible";


        protected override async Task OnInitializedAsync()
        {
            CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.WorkTime);
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
            IntilaizationTimeString = $"Initialized at {DateTime.Now}";
        }
        private void ShowNavBar() 
        {
            CurNavBarDisplay = "block" ;
            CurNavBarVisibility = "visible";
        }
        private void HideNavBar() 
        {
            CurNavBarDisplay = "none";
            CurNavBarVisibility = "hidden";
        }
        

        //Noteable event: suffers from a glitch where: Sometimes gives you a "short break" when it should give you a "Long break".
        public void PrevSession() 
        {
            if(MainTimerState == TimerState.NotStarted) 
            {
                switch (LastWorkState)
                {
                    case WorkState.Work:
                        NextWorkState = WorkState.Work;

                        if(CompletedWorkSessionCount -1 <= 0) 
                        {
                            LastWorkState = WorkState.LongBreak;

                            if(CompletedWorkSessionCount > 0)
                                CompletedWorkSessionCount--;
                        }
                        else
                        {
                            LastWorkState = WorkState.ShortBreak;
                            CompletedWorkSessionCount--;
                        }
                        SetUpWork();
                        break;

                    case WorkState.ShortBreak:
                        NextWorkState = WorkState.ShortBreak;
                        LastWorkState = WorkState.Work;
                        SetUpShortBreak();
                        break;

                    //Do NOT let the user go back if they try to use this to take a long break 1st (that does NOT make much sense).
                    //(Plus, they could just use the "skip" feature to do that.)...


                    case WorkState.LongBreak:
                        NextWorkState = WorkState.LongBreak;
                        LastWorkState = WorkState.Work;
                        CompletedWorkSessionCount = CurPomodoroSet.RepsBeforeLongBreak;
                        SetUpLongBreak();
                        break;
                }
            }


            if (CompletedWorkSessionCount <= 0)
            {
                ShowNavBar();
            }
            else
            {
                HideNavBar();
            }
        }

        public void RestartFullPomSession() 
        {
            MainTimerState = TimerState.NotStarted;
            ActualCountdownTimer.Enabled = false;
            CompletedWorkSessionCount = 0;
            SetUpWork();

            NextWorkState = WorkState.Work;
            LastWorkState = WorkState.LongBreak;
            ShowNavBar();
        }

        public void NextSession()
        {
            if(MainTimerState == TimerState.NotStarted) 
            {
                switch (NextWorkState) 
                {
                    case WorkState.Work: //May inccorectly set "LastWorkState" to "ShortBreak? (need to verify)
                        SetUpTimerPropertiesForCorrectBreak();
                        CompletedWorkSessionCount++;

                        //NEED TO VERIFY THAT THIS CONDITIONAL LINE WORKS (New note: I'm not even sure this gets used):
                        if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                            NextWorkState = WorkState.ShortBreak;
                        else
                            NextWorkState = WorkState.LongBreak;

                        LastWorkState = WorkState.Work; //New line of CODE. (NEEDS TESTING)

                        break;

                    case WorkState.ShortBreak:
                        NextWorkState = WorkState.Work;
                        LastWorkState = WorkState.ShortBreak;
                        SetUpWork();
                        break;

                    case WorkState.LongBreak:
                        NextWorkState = WorkState.Work;
                        LastWorkState = WorkState.LongBreak;
                        SetUpWork();
                        CompletedWorkSessionCount = 0;
                        break;
                }
            }
            //else 
            //{
            //    switch (NextWorkState)
            //    {
            //        case WorkState.Work:
            //            if (LastWorkState == WorkState.LongBreak)
            //                CompletedWorkSessionCount = 0;

            //            SetUpWork();

            //            if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
            //                LastWorkState = WorkState.ShortBreak;
            //            else
            //                LastWorkState = WorkState.LongBreak;
            //            break;

            //        case WorkState.ShortBreak:
            //            SetUpTimerPropertiesForCorrectBreak();
            //            LastWorkState = WorkState.ShortBreak;
            //            CompletedWorkSessionCount++;

            //            break;

            //        case WorkState.LongBreak:
            //            NextWorkState = WorkState.Work;
            //            LastWorkState = WorkState.LongBreak;
            //            SetUpWork();

            //            break;
            //    }
            //}

            if (CompletedWorkSessionCount >= 1)
            {
                HideNavBar();
            }
            else
            {
                ShowNavBar();
            }

        }

        public void SetUpWork()
        {
            AltWorkStateDisplay[1] = "Work";
            AltWorkStateDisplay[0] = "Next session: ";
            
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.WorkTime);
            CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.WorkTime);


                MainTimerState = TimerState.NotStarted;
            
        }

        /// <summary>
        /// Sets up a "short break" or "long break" (depending on current work session count). The only helper function which controlls "NextWorkState" & "LastWorkState" (all others
        /// are determined by a function triggered by the press of a button). 
        /// </summary>
        public void SetUpTimerPropertiesForCorrectBreak()
        {
            bool? NeedSetUpShortSession = null;

            AltWorkStateDisplay[0] = "Next session: ";
            if (CompletedWorkSessionCount + 1 >= CurPomodoroSet.RepsBeforeLongBreak)
                NeedSetUpShortSession = false;
            else
                NeedSetUpShortSession = true;

            if (NeedSetUpShortSession == false)
            {
                NextWorkState = WorkState.LongBreak;

                AltWorkStateDisplay[1] = "Long break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.LongBreak);
            }
            else
            {
                 NextWorkState = WorkState.ShortBreak;

                AltWorkStateDisplay[1] = "Short break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.ShortBreak);
            }

            MainTimerState = TimerState.NotStarted;
        }

        public async Task PauseTimer()
        {
            BgColor = HslColorSelection.GetPausedActivityBgColor();
            MainTimerState = TimerState.Paused;
            ActualCountdownTimer.Enabled = false;
        }
        public async Task ContinueTimer()
        {
            BgColor = HslColorSelection.GetActivityInProgressBgColor();
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Enabled = true;
        }

        public async Task SetUpShortBreak() 
        {
            AltWorkStateDisplay[0] = "Next session: ";
            AltWorkStateDisplay[1] = "Short break";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.ShortBreak);
            CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.ShortBreak);

            MainTimerState = TimerState.NotStarted;
        }

        public async Task SetUpLongBreak() 
        {
            AltWorkStateDisplay[0] = "Next session: ";
            AltWorkStateDisplay[1] = "Long break";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(CurPomodoroSet.LongBreak);
            CountdownTimerDisplay = GetCountdownTimerDisplay(CurPomodoroSet.LongBreak);

            MainTimerState = TimerState.NotStarted;
        }

        public async Task CancelSessionAndMakeItRepeatable()
        {
            BgColor = HslColorSelection.GetNoActivityBgColor();
            MainTimerState = TimerState.NotStarted;
            ActualCountdownTimer.Enabled = false;

            switch (LastWorkState) 
            {
                case WorkState.Work:

                    SetUpWork();
                    NextWorkState = WorkState.Work;

                    //Keep in mind the CompletedSessionCount should've been incremented when the previous timer ended.
                    if (CompletedWorkSessionCount >= CurPomodoroSet.RepsBeforeLongBreak)
                        LastWorkState = WorkState.LongBreak;
                    else
                        LastWorkState = WorkState.ShortBreak;

                    break;

                case WorkState.ShortBreak:
                    SetUpShortBreak();
                    NextWorkState = WorkState.ShortBreak;
                    LastWorkState = WorkState.Work;
                    break;

                case WorkState.LongBreak:
                    SetUpLongBreak();
                    NextWorkState = WorkState.LongBreak;
                    LastWorkState = WorkState.Work;
                    break;
            }

            if(CompletedWorkSessionCount <= 0) 
            {
                ShowNavBar();
            }

        }

        public async Task StartTimer(PomodoroSet pomoSet)
        {
            if (NextWorkState == WorkState.Work &&
                (LastWorkState == WorkState.LongBreak || LastWorkState == WorkState.ShortBreak) )
            {
                //start work session (do NOT increment work session count or setup any time!)
                LastWorkState = WorkState.Work;

                if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                    NextWorkState = WorkState.ShortBreak;
                else
                    NextWorkState = WorkState.LongBreak;

                //Optional: Trigger a bgColor.
                CurActivityBarColor = WorkBarColor;
            }
            else
            {
                if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak) 
                {
                    LastWorkState = WorkState.ShortBreak;
                    CurActivityBarColor = ShortBreakBarColor;
                }

                else
                {
                    LastWorkState = WorkState.LongBreak;
                    CurActivityBarColor = LongBreakBarColor;
                }

                NextWorkState = WorkState.Work;

            }

            BgColor = HslColorSelection.GetActivityInProgressBgColor();
            HideNavBar();
            ActualCountdownTimer = new System.Timers.Timer(1000);
            ActualCountdownTimer.Enabled = true;
            MainTimerState = TimerState.Started;
            AltWorkStateDisplay[0] = "Current session: ";
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
                    //BgColor = PomTimerHelpers.TransitionToColor(NoActivityBgColor);

                    if (CompletedWorkSessionCount + 1 > CurPomodoroSet.RepsBeforeLongBreak)
                        CompletedWorkSessionCount = 0;

                    switch (NextWorkState) 
                    {
                        case WorkState.Work:
                            SetUpWork();

                            if (CompletedWorkSessionCount < CurPomodoroSet.RepsBeforeLongBreak)
                                LastWorkState = WorkState.ShortBreak;
                            else
                                LastWorkState = WorkState.LongBreak;
                            break;

                        case WorkState.ShortBreak:
                            SetUpTimerPropertiesForCorrectBreak();
                            LastWorkState = WorkState.ShortBreak;
                            CompletedWorkSessionCount++;
                            //Next work state is still a short break. (We set it from the start of the timer in case the user skips that session).
                            break;
                    }

                    if(CompletedWorkSessionCount <= 0) 
                    {
                        ShowNavBar();
                    }
                    else
                    {
                        HideNavBar();
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
