using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class ChangeTimer
    {
        [CascadingParameter]
        PomodoroSet PomoSetWithTimerInfoOnly { get; set; } = new PomodoroSet();
        private bool IsSaveNotificationVisible { get; set; } = false;
        private async Task ChangeTimerProps() 
        {
            //This is for future use (when implementing sets of pomodoro timers).
            //string NewPomodoroId = Guid.NewGuid().ToString();
            //PomoSetWithTimerInfoOnly.Id = NewPomodoroId;

            Debug.WriteLine($"Set info updated.");

            //Debug.WriteLine($"ID: {PomoSetWithTimerInfoOnly.Id}");
            //Debug.WriteLine($"Name: {PomoSetWithTimerInfoOnly.Name}");

            //These should ALL have new user values:

            Debug.WriteLine($"Work: {PomoSetWithTimerInfoOnly.WorkTime}");
            Debug.WriteLine($"Short break: {PomoSetWithTimerInfoOnly.ShortBreak}");
            Debug.WriteLine($"Long break: {PomoSetWithTimerInfoOnly.LongBreak}");
            Debug.WriteLine($"Reps before long break: {PomoSetWithTimerInfoOnly.RepsBeforeLongBreak}");

            IsSaveNotificationVisible = true;
            await Task.Delay(4000);
            IsSaveNotificationVisible = false;
        }
    }
}
