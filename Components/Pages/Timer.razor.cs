using CustomPomodoro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class Timer
    {
        public enum TimerMode
        {
            NotStarted = -1,
            Paused = 0,
            Started = 1
        }

        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public TimerMode MainTimerState { get; set; } = TimerMode.NotStarted;
        public string PomodoroWorkState { get; set; } = "Work";  //"Work", "Short Break", or "Long Break".
        public TimeSpan VisibleTimerDisplay { get; set; }
        public int SessionCount { get; set; } = 0;

        public TimeSpan GetEndTimeInSecondsFormat(string workTimeInput)
        {
            string[] workTimeAsArray =  CurPomodoroSet.WorkTime.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(workTimeAsArray[0]) * SECONDS_PER_MINUTE) + int.Parse(workTimeAsArray[1]);
            return TimeSpan.FromSeconds(WorkTimeInTotalSeconds);
        }

        public async Task StartTimer(PomoderoSet PomoSet)
        {
            //Start timer countdown after obtaining it.
            VisibleTimerDisplay =  GetEndTimeInSecondsFormat(PomoSet.WorkTime);

            //To do: Create logic for starting a timer.
            SessionCount++;
            MainTimerState = TimerMode.Started;

            //while (VisibleTimerDisplay > TimeSpan.Zero)
            //{
            //    VisibleTimerDisplay.Subtract(TimeSpan.FromSeconds(1));
            //}

        }
    }
}
