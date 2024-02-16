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
    public partial class Timer
    {

        //protected override async Task OnInitializedAsync() 
        //{ 
            
        //}


        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public string PomodoroWorkState { get; set; } = "Work";  //"Work", "Short Break", or "Long Break".
        public int TimerInSeconds { get; set; } = -1;
        public System.Timers.Timer ActualCountdownTimer { get; set; } = new();
        public int SessionCount { get; set; } = 0;

        public int GetEndTimeInSecondsFormat(string workTimeInput)
        {
            string[] workTimeAsArray =  CurPomodoroSet.WorkTime.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(workTimeAsArray[0]) * SECONDS_PER_MINUTE) + int.Parse(workTimeAsArray[1]);
            return WorkTimeInTotalSeconds;
        }

        public async Task StartTimer(PomoderoSet PomoSet)
        {
            //Start timer countdown after obtaining it.
            TimerInSeconds = GetEndTimeInSecondsFormat(PomoSet.WorkTime);

            //To do: Create logic for starting a timer.
            SessionCount++;


            ActualCountdownTimer = new System.Timers.Timer(1000); //5 Sec timer.
            ActualCountdownTimer.Enabled = true;
            ActualCountdownTimer.Elapsed += CountDownTimer;
        }

        public string PrintCountdownTimer() 
        {
            return $"{TimeSpan.FromSeconds(TimerInSeconds):hh\\:mm\\:ss}";
        }

        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e) 
        {
            if(TimerInSeconds > 0) 
            {
                TimerInSeconds--;
            }
            else 
            {
                ActualCountdownTimer.Enabled = false;   
            }
            InvokeAsync(StateHasChanged);
        }
    }
}
