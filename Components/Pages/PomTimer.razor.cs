//using Android.OS;
using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using CustomPomodoro.Models.Helpers.Colors;
using CustomPomodoro.Models.Helpers.PomTimer;
using CustomPomodoro.Models.UserSettings.Abstract;
using CustomPomodoro.Models.UserSettings.Concrete;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings.PomSetLoadFileOps;

namespace CustomPomodoro.Components.Pages
{
    public partial class PomTimer
    {
        private enum TimerState
        {
            NotStarted = -1,
            Paused = 0,
            Started = 1
        }

        private enum WorkState
        {
            ShortBreak = 0,
            LongBreak = 1,
            Work = 2
        }

        private bool ShouldNavBarBeHidden { get; set; } = false;

        [Inject]
        protected IMasterUserSettings? UserSettings { get; set; }

        //The "ActivityBarColors" is in the form of a list to work with current functions
        // (& to allow possible adaptability in the future).
        private List<string> CurActivityBarColors { get; set; } = new () {"hsl(0 0% 0%)", "hsl(0 0% 0%)" };
        private string BgColor = HslColorSelection.GetNoActivityBgColor();
        private string[] AltWorkStateDisplay { get; set; } = { "Next session: ", "Work" };
        private string CountdownTimerDisplay { get; set; } = "00:00";
        private int TimerInSeconds { get; set; } = 0;
        private TimerState MainTimerState { get; set; } = TimerState.NotStarted;
        private WorkState NextWorkState { get; set; } = WorkState.Work;
        private WorkState LastWorkState { get; set; } = WorkState.LongBreak;
        private System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        private int CompletedWorkSessionCount { get; set; } = 0;
        private string CurNavBarDisplay { get; set; } = "block";
        private string CurNavBarVisibility { get; set; } = "visible";

        protected override async Task OnInitializedAsync()
        {
            
            await UserSettings.LoadCurPomodoroSet();


            await UserSettings.LoadAllColorSettings();

            BgColor = HslColorSelection.GetNoActivityBgColor(UserSettings.GetBackgroundColorSettings().NoActivityBgColor);
            CurActivityBarColors = ActivityBarColorHelpers.TransformHSLListToCSSCompatibleStringList(UserSettings.GetActivityBarSettings().WorkColors);
            //Loading user settings (above)---------

            CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().WorkTime);
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().WorkTime);
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
        

        public void PrevSession() 
        {
            //Vibration.Vibrate(5000);
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

                    case WorkState.LongBreak:
                        NextWorkState = WorkState.LongBreak;
                        LastWorkState = WorkState.Work;
                        CompletedWorkSessionCount = UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak;
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

        public void RestartFullPomSession() //Move to a "helper" class (not sure which class yet).
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
                        if (CompletedWorkSessionCount < UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
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

            if (CompletedWorkSessionCount >= 1)
            {
                HideNavBar();
            }
            else
            {
                ShowNavBar();
            }

        }

        public void SetUpWork() //Move to a "helper" class (not sure which class yet).
        {
            AltWorkStateDisplay[1] = "Work";
            AltWorkStateDisplay[0] = "Next session: ";

            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().WorkTime);
            CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().WorkTime);

            MainTimerState = TimerState.NotStarted;
        }

        /// <summary>
        /// Sets up a "short break" or "long break" (depending on current work session count). The only helper function which controlls "NextWorkState" & "LastWorkState" (all others
        /// are determined by a function triggered by the press of a button). 
        /// </summary>
        public void SetUpTimerPropertiesForCorrectBreak() //Move to a "helper" class (not sure which class yet).
        {
            bool? NeedSetUpShortSession = null;

            AltWorkStateDisplay[0] = "Next session: ";
            if (CompletedWorkSessionCount + 1 >= UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
                NeedSetUpShortSession = false;
            else
                NeedSetUpShortSession = true;

            if (NeedSetUpShortSession == false)
            {
                NextWorkState = WorkState.LongBreak;

                AltWorkStateDisplay[1] = "Long break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().LongBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().LongBreak);
            }
            else
            {
                 NextWorkState = WorkState.ShortBreak;

                AltWorkStateDisplay[1] = "Short break";
                TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().ShortBreak);
                CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().ShortBreak);
            }

            MainTimerState = TimerState.NotStarted;
        }

        public async Task PauseTimer()
        {
            BgColor = HslColorSelection.GetPausedActivityBgColor(UserSettings.GetBackgroundColorSettings().PausedActivityColor);
            MainTimerState = TimerState.Paused;
            ActualCountdownTimer.Enabled = false;
        }
        public async Task ContinueTimer()
        {
            BgColor = HslColorSelection.GetActivityInProgressBgColor(UserSettings.GetBackgroundColorSettings().ActivityInProgressColor);
            MainTimerState = TimerState.Started;
            ActualCountdownTimer.Enabled = true;
        }

        public async Task SetUpShortBreak() //Move to a "helper" class (not sure which class yet).
        {
            AltWorkStateDisplay[0] = "Next session: ";
            AltWorkStateDisplay[1] = "Short break";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().ShortBreak);
            CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().ShortBreak);

            MainTimerState = TimerState.NotStarted;
        }

        public async Task SetUpLongBreak() //Move to a "helper" class (not sure which class yet).
        {
            AltWorkStateDisplay[0] = "Next session: ";
            AltWorkStateDisplay[1] = "Long break";
            TimerInSeconds = PomTimerHelpers.GetEndTimeInSecondsFormat(UserSettings.GetCurPomodoroSet().LongBreak);
            CountdownTimerDisplay = GetCountdownTimerDisplay(UserSettings.GetCurPomodoroSet().LongBreak);

            MainTimerState = TimerState.NotStarted;
        }

        public async Task CancelSessionAndMakeItRepeatable()
        {
            BgColor = HslColorSelection.GetNoActivityBgColor(UserSettings.GetBackgroundColorSettings().NoActivityBgColor);
            MainTimerState = TimerState.NotStarted;
            ActualCountdownTimer.Enabled = false;

            switch (LastWorkState) 
            {
                case WorkState.Work:

                    SetUpWork();
                    NextWorkState = WorkState.Work;

                    //Keep in mind the CompletedSessionCount should've been incremented when the previous timer ended.
                    if (CompletedWorkSessionCount >= UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak ||
                        (CompletedWorkSessionCount == 0 && NextWorkState == WorkState.Work))
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

        public async Task StartTimer()
        {
            if (NextWorkState == WorkState.Work &&
                (LastWorkState == WorkState.LongBreak || LastWorkState == WorkState.ShortBreak))
            {
                //start work session (do NOT increment work session count or setup any time!)
                LastWorkState = WorkState.Work;

                if (CompletedWorkSessionCount < UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
                    NextWorkState = WorkState.ShortBreak;
                else
                    NextWorkState = WorkState.LongBreak;

                CurActivityBarColors = ActivityBarColorHelpers.TransformHSLListToCSSCompatibleStringList(UserSettings.GetActivityBarSettings().WorkColors);
            }
            else
            {
                if (CompletedWorkSessionCount < UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
                {
                    LastWorkState = WorkState.ShortBreak;
                    CurActivityBarColors = ActivityBarColorHelpers.TransformHSLListToCSSCompatibleStringList(UserSettings.GetActivityBarSettings().ShortBreakColors);
                }

                else
                {
                    LastWorkState = WorkState.LongBreak;
                    CurActivityBarColors = ActivityBarColorHelpers.TransformHSLListToCSSCompatibleStringList(UserSettings.GetActivityBarSettings().LongBreakColors);
                }

                NextWorkState = WorkState.Work;

            }

            BgColor = HslColorSelection.GetActivityInProgressBgColor(UserSettings.GetBackgroundColorSettings().ActivityInProgressColor);
            HideNavBar();
            ActualCountdownTimer = new System.Timers.Timer(1000);
            ActualCountdownTimer.Enabled = true;
            MainTimerState = TimerState.Started;
            AltWorkStateDisplay[0] = "Current session: ";
            ActualCountdownTimer.Elapsed += CountDownTimer;
        }

        public string GetCountdownTimerDisplay(string workTime) //Move to a "helper" class (not sure which class yet).
        {
            int WorkTimeInSec = PomTimerHelpers.GetEndTimeInSecondsFormat(workTime);


            string formattedInput = $"{TimeSpan.FromSeconds(WorkTimeInSec):mm\\:ss}";
            if (formattedInput.Length == 5)
            {
                if (formattedInput.First() == '0')
                    formattedInput = formattedInput.Substring(1);
            }
            return formattedInput;
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
                    BgColor = HslColorSelection.GetNoActivityBgColor(UserSettings.GetBackgroundColorSettings().NoActivityBgColor);
                    ActualCountdownTimer.Enabled = false;

                    if (CompletedWorkSessionCount + 1 > UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
                        CompletedWorkSessionCount = 0;

                    switch (NextWorkState) 
                    {
                        case WorkState.Work:
                            SetUpWork();

                            if (CompletedWorkSessionCount >=1 && CompletedWorkSessionCount < UserSettings.GetCurPomodoroSet().RepsBeforeLongBreak)
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
