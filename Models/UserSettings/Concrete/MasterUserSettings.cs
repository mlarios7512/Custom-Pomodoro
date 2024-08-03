using CustomPomodoro.Components.Pages;
using CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings;
using CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings;
using CustomPomodoro.Models.UserSettings.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings.PomSetLoadFileOps;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class MasterUserSettings:IMasterUserSettings
    {
        public ActivityBarSettings _activityBarSettings = new ();
        public BackgroundColorSettings _backgroundColorSettings = new ();
        public PomodoroTimerSettings _curPomodoroSetSetttings = new();

        public BackgroundColorSettings GetBackgroundColorSettings() 
        {
            return _backgroundColorSettings;
        }

        public ActivityBarSettings GetActivityBarSettings() 
        {
            return _activityBarSettings;
        }

        public PomodoroSet GetCurPomodoroSet() 
        {
            return _curPomodoroSetSetttings.StoredPomSet;
        }


        public async Task SaveUserPomodoroSet(PomodoroTimerSettings settingsToSave) 
        {            
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
            {
                bool SaveSucessful = PomSetSaveFileOps.CreateNewSaveFile(settingsToSave);

                if (SaveSucessful)
                    await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
                else
                    await Application.Current.MainPage.DisplayAlert("Alert", "An error occured when saving changes.", "OK");
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Changes saved for this session only.", "OK");
            }
        }

        public async Task SaveUserColorSettings(MainColorSettings settingsToSave) 
        {
            await Permissions.RequestAsync<Permissions.StorageWrite>();
            PermissionStatus writePermission = PermissionStatus.Unknown;
            writePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();


            if (writePermission == PermissionStatus.Granted) 
            {
                //To do: implement save logic here.
                //Alert user if success OR failure occurs. 
                bool saveSucess = await SaveColorSettingsOps.SaveAllColorSettings(settingsToSave);
                if (saveSucess) 
                    await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
                else 
                    await Application.Current.MainPage.DisplayAlert("Error", "An error occured when saving changes.", "OK");
            }
            else 
            {
                //To do: Write logic to save to "MainColorSettings" within "UserSettings" (once you create it for "UserSettings")
                // so that setting are saved for the duration of the session.
                await Application.Current.MainPage.DisplayAlert("Alert", "Changes saved for this session only.", "OK");
            }
        }


        //General rule: If a function invovles LOADING settings that required user permissions to create a
        // save file for it, CHECK the current permissions for it but do NOT REQUEST permissions for it
        // (such as with an alert). There is a high chance it will choke the UI during boot up (& probably annoy the user).
        // See official android docs for guidelines: https://developer.android.com/training/permissions/requesting

        //For SAVING settings: ALWAYS check, then request user permissions within the same attempt.

        /// <summary>
        /// Loads a pomodoro set and its settings and as the current one in use by the user.
        /// </summary>
        public async Task LoadCurPomodoroSet() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if(status == PermissionStatus.Granted) 
            {
                PomSetLoadFileOps.LoadCurrentPomodoroSetFromFile(ref _curPomodoroSetSetttings);
            }
        }

        //Used when starting the app.
        public async Task LoadAllColorSettings() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                _activityBarSettings = LoadColorSettingsOps.LoadActivityBarSettings_V2();
                _backgroundColorSettings = LoadColorSettingsOps.LoadBackgroundColorsSettings();
            }
            else
            {
                _activityBarSettings = new ActivityBarSettings();
                _backgroundColorSettings = new BackgroundColorSettings();
            }
        }


    }
}
