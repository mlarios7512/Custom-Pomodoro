using CustomPomodoro.Components.Pages;
using CustomPomodoro.Models.UserSettings.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class MasterUserSettings:IMasterUserSettings
    {
        //public static MasterUserSettings MasterSettings { get; set; }
        //private static MasterUserSettings _masterSettings = new MasterUserSettings();
        public PomodoroSet _curPomodoroSet = new PomodoroSet();
        public ActivityBarSettings _activityBarSettings = new ActivityBarSettings();
        public BackgroundColorSettings _backgroundColorSettings = new BackgroundColorSettings();
        //private MasterUserSettings() 
        //{
        //}

        //public static MasterUserSettings GetSingletonInstance()
        //{
        //    return _masterSettings;

        //}

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



        //This will be used when starting the app.
        public async Task LoadAllColorSettings() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Need storage permission", "Storage permission is required to load user settings.", "OK");
            }

            status = await Permissions.RequestAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                _activityBarSettings = ColorSettings.LoadActivityBarSettings();
                _backgroundColorSettings = ColorSettings.LoadBackgroundColorsSettings();
            }
            else
            {
                _activityBarSettings = new ActivityBarSettings();
                _backgroundColorSettings = new BackgroundColorSettings();
            }
            //TEMP CODE (below)
            //_activityBarSettings = new ActivityBarSettings();
            //_backgroundColorSettings = new BackgroundColorSettings();
        }


    }
}
