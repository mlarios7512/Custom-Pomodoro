using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings;
using CustomPomodoro.Models.UserSettings.Abstract;
using CustomPomodoro.Models.UserSettings.Concrete;
using CustomPomodoro.ViewModels.InputClones;
using CustomPomodoro.ViewModels.Pages.ChangeTimer;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.Controls.PlatformConfiguration;
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
        private ChangeTimerVM SettingsVM { get; set; } = new ChangeTimerVM();
        protected override async Task OnInitializedAsync() 
        {
            this.PersistentPomSetToDummyPomSet(UserSettings.GetCurPomodoroSet());
        }

        private void PersistentPomSetToDummyPomSet(PomodoroSet savedSet)
        {
            string[] sessionTime = savedSet.WorkTime.Split(":");
            SettingsVM.PomSetInput.WT_Minutes = sessionTime[0];
            SettingsVM.PomSetInput.WT_Seconds = sessionTime[1];

            sessionTime = savedSet.ShortBreak.Split(":");
            SettingsVM.PomSetInput.SBT_Minutes = sessionTime[0];
            SettingsVM.PomSetInput.SBT_Seconds = sessionTime[1];

            sessionTime = savedSet.LongBreak.Split(":");
            SettingsVM.PomSetInput.LBT_Minutes = sessionTime[0];
            SettingsVM.PomSetInput.LBT_Seconds = sessionTime[1];

            SettingsVM.PomSetInput.RepsBeforeLongBreak = savedSet.RepsBeforeLongBreak;
        }

        private static string TransformInputAsFormattedStringForStroage(int sessionTimeInSeconds) 
        {
            string formattedInput = $"{TimeSpan.FromSeconds(sessionTimeInSeconds):mm\\:ss}";

            if (formattedInput.Length == 5)
            {
                if (formattedInput.First() == '0')
                    formattedInput = formattedInput.Substring(1);
            }

            return formattedInput;
        }

        private static PomodoroSet DummyPomSetInputToPersistentPomSet(PomTimerSetInputClone userInput, out PomodoroSet pomodoroSetOptionsToSave) 
        {
            pomodoroSetOptionsToSave = new PomodoroSet();
            int workTimeInSeconds = Int32.Parse(userInput.WT_Minutes) * 60;
            workTimeInSeconds += Int32.Parse(userInput.WT_Seconds);
            pomodoroSetOptionsToSave.WorkTime = TransformInputAsFormattedStringForStroage(workTimeInSeconds);

            int shortBreakInSeconds = Int32.Parse(userInput.SBT_Minutes) * 60;
            shortBreakInSeconds += Int32.Parse(userInput.SBT_Seconds);
            pomodoroSetOptionsToSave.ShortBreak = TransformInputAsFormattedStringForStroage(shortBreakInSeconds);

            int longBreakInSeconds = Int32.Parse(userInput.LBT_Minutes) * 60;
            longBreakInSeconds += Int32.Parse(userInput.LBT_Seconds);
            pomodoroSetOptionsToSave.LongBreak = TransformInputAsFormattedStringForStroage(longBreakInSeconds);

            pomodoroSetOptionsToSave.RepsBeforeLongBreak = userInput.RepsBeforeLongBreak;
            return pomodoroSetOptionsToSave;
        }

        private async Task RequestVibrationPermission() 
        {
            PermissionStatus vibration = PermissionStatus.Unknown;
            vibration = await Permissions.CheckStatusAsync<Permissions.Vibrate>();

            if(vibration != PermissionStatus.Granted) 
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Permission is required to enable vibration.", "OK");
                vibration = await Permissions.RequestAsync<Permissions.Vibrate>();
                SettingsVM.VibrateOnSessionEnd = false;
            }
            else 
            {
                SettingsVM.VibrateOnSessionEnd = true;
            }
        }

        private async Task SaveChanges() 
        {
            //This is for implementing sets of pomodoro timers, now a future feature.
            //string NewPomodoroId = Guid.NewGuid().ToString();
            //PomoSetWithTimerInfoOnly.Id = NewPomodoroId;


            PomodoroTimerSettings pomSetSettingsToSave = new ();
            PomodoroSet tempPomSet = new();

            DummyPomSetInputToPersistentPomSet(SettingsVM.PomSetInput, out tempPomSet);
            pomSetSettingsToSave.StoredPomSet = tempPomSet;

            
            await UserSettings.SaveUserPomodoroSet(pomSetSettingsToSave);

            
            //Append any other existing view-model pomodoro set settings (if any).
            if (DeviceInfo.Current.Platform == DevicePlatform.Android) 
            {
                await PomSetSaveFileOps.SaveAndroidVibrationOnTimerEndDecision(SettingsVM.VibrateOnSessionEnd);
            }
        }
    }
}
