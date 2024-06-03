using CustomPomodoro.Models;
using CustomPomodoro.Models.UserSettings.Concrete;
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
        MasterUserSettings UserSettings { get; set; }
        private async Task ChangeTimerProps() 
        {
            //This is for future use (if implementing sets of pomodoro timers).
            //string NewPomodoroId = Guid.NewGuid().ToString();
            //PomoSetWithTimerInfoOnly.Id = NewPomodoroId;

            //MasterUserSettings.MasterInstance.LoadAllSettings();

            Debug.WriteLine($"Work: {UserSettings._curPomodoroSet.WorkTime}");
            Debug.WriteLine($"Short break: {UserSettings._curPomodoroSet.ShortBreak}");
            Debug.WriteLine($"Long break: {UserSettings._curPomodoroSet.LongBreak}");
            Debug.WriteLine($"Reps before long break: {UserSettings._curPomodoroSet.RepsBeforeLongBreak}");

            await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");


        }
    }
}
