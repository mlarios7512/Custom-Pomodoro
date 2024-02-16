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
        //The "new ()" part of the statement below is for testing purposes ONLY. (Real data will be loaded from local machine.)
        public PomoderoSet CurPomodoroSet { get; set; } = new();
        public TimerMode MainTimerState { get; set; } = TimerMode.NotStarted;
        public TimeSpan VisibleTimerDisplay { get; set; }
        public int SessionCount { get; set; } = 0;

        public TimeSpan GetEndTimeInSecondsFormat(string workTimeInput)
        {
            string[] workTimeAsArray =  CurPomodoroSet.WorkTime.Split(':');

            const int SECONDS_PER_MINUTE = 60;
            int WorkTimeInTotalSeconds = (int.Parse(workTimeAsArray[0]) * SECONDS_PER_MINUTE) + int.Parse(workTimeAsArray[1]);
            return TimeSpan.FromSeconds(WorkTimeInTotalSeconds);
        }

        public enum TimerMode 
        {
            NotStarted = -1,
            Paused = 0,
            Started = 1
        }

        public TimerMode StartTimer(PomoderoSet PomoSet)
        {
            //Start timer countdown after obtaining it.
            TimeSpan Timer =  GetEndTimeInSecondsFormat(PomoSet.WorkTime);

            //To do: Create logic for starting a timer.


            SessionCount++;
            return TimerMode.Started;
        }
    }
}
