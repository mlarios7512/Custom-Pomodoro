using CustomPomodoro.Models.UserSettings.Concrete;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings
{
    internal class PomSetSaveFileOps
    {
        private const string _saveFileName = "PomodoroSets.json";
        //// This version was intended when multiple pomodoro sets were going to be implemented.
        //public static void CreateNewSaveFile(PomodoroSet newPomoderoSet)
        //{
        //    List<PomodoroSet> PomSetsToCreate = new List<PomodoroSet> { newPomoderoSet };

        //    var convertedJson = JsonConvert.SerializeObject(PomSetsToCreate, Formatting.Indented);
        //    File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPomSetSettings"></param>
        /// <returns>A bool indicating if save was sucessful. (True if successful. False if otherwise)</returns>
        public static bool CreateNewSaveFile(PomodoroTimerSettings newPomSetSettings) 
        {
            try 
            {
                var finalJson = JsonConvert.SerializeObject(newPomSetSettings, Formatting.Indented);

                var savePath = FileSystem.Current.AppDataDirectory;
                if(!Path.Exists(savePath)) 
                {
                    Directory.CreateDirectory(savePath);
                }

                File.WriteAllText(Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName), finalJson);
                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"An error occured when attempting to create a pomodoro set save file. INFO: {ex}");
                return false;
            }
        }

        public static async Task SaveAndroidVibrationOnTimerEndDecision(bool vibrateOnTimerEnd) 
        {

            //See the "Preferences" API for saving user data cross-platform.
            // (Note: Currently using this only for minor differences, such as vibration on Android vs toast on Windows for timer end):
            //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows

            if (Permissions.CheckStatusAsync<Permissions.Vibrate>().Result == PermissionStatus.Granted
                && vibrateOnTimerEnd == true)
            {
                Preferences.Default.Set("vibrate-on-timer-end", true);
            }
            else
            {
                Preferences.Default.Set("vibrate-on-timer-end", false);
            }   
        }

    }
}
