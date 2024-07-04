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
        public PomodoroSet _curPomodoroSet = new PomodoroSet();
        public ActivityBarSettings _activityBarSettings = new ActivityBarSettings();
        public BackgroundColorSettings _backgroundColorSettings = new BackgroundColorSettings();

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
            return _curPomodoroSet;
        }


        //General rule: If a function invovles loading something that required user permissions to create a
        // save file for it, CHECK the current permissions for it but do NOT REQUEST permissions for it
        // (such as with an alert). There is a high chance it will choke the UI during boot up (& probably annoy the user).
        // See official android docs for guidelines: https://developer.android.com/training/permissions/requesting

        /// <summary>
        /// Loads a pomodoro set and loads it as the current pomodoro set.
        /// </summary>
        /// <returns>A status code indicating the result of the file loading operation.</returns>
        public async Task LoadCurPomodoroSet() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if(status == PermissionStatus.Granted) 
            {
                PomSetLoadFileOps.LoadCurrentPomodoroSetFromFile(ref _curPomodoroSet);
            }
        }

        //Used when starting the app.
        public async Task LoadAllColorSettings() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                _activityBarSettings = LoadColorSettingsOps.LoadActivityBarSettings();
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
