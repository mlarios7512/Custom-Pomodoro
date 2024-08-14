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
    public class PomSetLoadFileOps
    {
        private const string _saveFileName = "PomodoroSets.json";

        public static Task LoadCurrentPomodoroSetFromFile(ref PomodoroTimerSettings pomSet)
        {
            string JsonSavePath = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(JsonSavePath))
                {
                    string PomSetFromJsonFile = File.ReadAllText(JsonSavePath);
                    pomSet = JsonConvert.DeserializeObject<PomodoroTimerSettings>(PomSetFromJsonFile);

                    if (pomSet == null)
                        pomSet = new PomodoroTimerSettings();


                }
                else if (pomSet == null)
                {
                    pomSet = new PomodoroTimerSettings();
                }
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"An error occured when loading the pomodoro set from file. INFO: {ex}");
                if(pomSet == null)
                    pomSet= new PomodoroTimerSettings();
            }

            return Task.FromResult(pomSet);
        }
    }
}
