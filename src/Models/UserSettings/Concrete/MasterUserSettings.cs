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
        public MainColorSettings _mainColorSettings = new ();
        public PomodoroTimerSettings _curPomodoroSetSettings = new();

        public BackgroundColorOptions GetBackgroundColorSettings() 
        {
            return _mainColorSettings.BackgroundColorSettings;
        }

        public ActivityBarOptions GetActivityBarSettings() 
        {
            return _mainColorSettings.ActivityColorSettings;
        }

        public PomodoroSet GetCurPomodoroSet() 
        {
            return _curPomodoroSetSettings.StoredPomSet;
        }

        public MainColorSettings GetMainColorSettings() 
        {
            return _mainColorSettings;
        }


        public async Task SaveUserPomodoroSet(PomodoroTimerSettings settingsToSave) 
        {
            //await Permissions.RequestAsync<Permissions.StorageWrite>();
            //PermissionStatus status = PermissionStatus.Unknown;
            //status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            //if (status == PermissionStatus.Granted)
            //{
            //    bool SaveSucessful = PomSetSaveFileOps.CreateNewSaveFile(settingsToSave);
            //    if (SaveSucessful)
            //        await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
            //    else
            //        await Application.Current.MainPage.DisplayAlert("Alert", "An error occured when saving changes.", "OK");
            //}
            //else 
            //{
            //These lines assumes the parameter itself is 100% validated by this point.
                PomSetSaveFileOps.CreateSaveAsPreferences(settingsToSave);

                _curPomodoroSetSettings = settingsToSave;
                await Application.Current.MainPage.DisplayAlert("Alert", "Changes saved.", "OK");
            //await Application.Current.MainPage.DisplayAlert("Alert", "Changes saved for this session only.", "OK");
            //}
        }

        public async Task SaveUserColorSettings(MainColorSettings settingsToSave) 
        {
            //await Permissions.RequestAsync<Permissions.StorageWrite>();
            //PermissionStatus writePermission = PermissionStatus.Unknown;
            //writePermission = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();


            //if (writePermission == PermissionStatus.Granted) 
            //{
            //    bool saveSucess = await SaveColorSettingsOps.SaveAllColorSettings(settingsToSave);
            //    if (saveSucess) 
            //        await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
            //    else 
            //        await Application.Current.MainPage.DisplayAlert("Error", "An error occured when saving changes.", "OK");
            //}
            //else 
            //{
                _mainColorSettings = settingsToSave;
                await Application.Current.MainPage.DisplayAlert("Alert", "Changes saved for this session only.", "OK");
            //}
        }


        //General rule: If a function invovles LOADING settings that required user permissions to create a
        // save file for it, CHECK the current permissions for it but do NOT REQUEST permissions for it
        // (such as with an alert). There is a high chance it will choke the UI during boot up (& probably annoy the user).
        // See official android docs for guidelines: https://developer.android.com/training/permissions/requesting

        //For SAVING settings: ALWAYS check permissions, then request user permissions within the same attempt to save a file.

        /// <summary>
        /// Loads a pomodoro set and its settings as the current one in use by the user.
        /// </summary>
        public async Task LoadCurPomodoroSet() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if(status == PermissionStatus.Granted) 
            {
                //PomSetLoadFileOps.LoadCurrentPomodoroSetFromFile(ref _curPomodoroSetSettings);
                PomSetLoadFileOps.LoadCurrentPomodoroSetFromPreferences(ref _curPomodoroSetSettings);
            }
            else 
            {
                if (_curPomodoroSetSettings == null)
                    _curPomodoroSetSettings = new PomodoroTimerSettings();
            }
        }

        //Used when starting the app.
        public async Task LoadAllColorSettings() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                _mainColorSettings.ActivityColorSettings = LoadColorSettingsOps.LoadActivityBarSettings(_mainColorSettings);
                _mainColorSettings.BackgroundColorSettings = LoadColorSettingsOps.LoadBackgroundColorsSettings(_mainColorSettings);
            }
            else
            {
                if(_mainColorSettings.ActivityColorSettings == null)
                    _mainColorSettings.ActivityColorSettings = new ActivityBarOptions();

                if (_mainColorSettings.BackgroundColorSettings == null)
                    _mainColorSettings.BackgroundColorSettings = new BackgroundColorOptions();
            }
        }


    }
}
