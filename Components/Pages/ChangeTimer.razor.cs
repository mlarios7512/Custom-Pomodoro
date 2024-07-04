using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings;
using CustomPomodoro.Models.UserSettings.Abstract;
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
        [Inject]
        protected IMasterUserSettings? UserSettings { get; set; }
        private PomodoroSet SetToEdit { get; set; }
        protected override async Task OnInitializedAsync() 
        {
            SetToEdit = UserSettings.GetCurPomodoroSet();
        }
        private async Task ChangeTimerProps() 
        {
            //This is for future use (if implementing sets of pomodoro timers).
            //string NewPomodoroId = Guid.NewGuid().ToString();
            //PomoSetWithTimerInfoOnly.Id = NewPomodoroId;

            bool SaveSucessful = PomSetSaveFileOps.CreateNewSaveFile(SetToEdit);

            //Preferences.Default.Set("auto-start-sessions", true);
            //if(DeviceInfo.Current.Platform == DevicePlatform.Android)
            //    Preferences.Default.Set("vibrate-on-timer-end", true);

            if (SaveSucessful) 
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "An error occured when saving changes.", "OK");
            }
            


        }
    }
}
