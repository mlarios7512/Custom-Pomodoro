using CustomPomodoro.Components.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class MasterUserSettings
    {
        public static MasterUserSettings MasterSettings 
        {
            get
            {
                if (_masterSettings == null)
                    _masterSettings = new MasterUserSettings();

                return _masterSettings;
            }
        }
        private static MasterUserSettings _masterSettings = new MasterUserSettings();
        public PomodoroSet _curPomodoroSet = new PomodoroSet();
        public ActivityBarSettings _activityBarSettings = new ActivityBarSettings();
        public BackgroundColorSettings _backgroundColorSettings = new BackgroundColorSettings();
        private MasterUserSettings() 
        {
        }

        public static MasterUserSettings GetSingletonInstance()
        {
            return _masterSettings;

        }



        //This will be used when starting the app.
        public async Task LoadAllSettings() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (status != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Need storage permission", "Storage permission is required to read save settings.", "OK");
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
        }


    }
}
