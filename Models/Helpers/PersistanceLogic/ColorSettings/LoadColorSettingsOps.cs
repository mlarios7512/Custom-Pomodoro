using CustomPomodoro.Models.UserSettings.Concrete;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings
{
    public class LoadColorSettingsOps
    {
        private const string _saveFileName = "ColorSettings.json";
        public LoadColorSettingsOps() { }

        public static BackgroundColorSettings LoadBackgroundColorsSettings()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);
            if (File.Exists(saveFile))
            {
                string fileContents = File.ReadAllText(saveFile);
                JObject fileContentsAsJObj = JObject.Parse(fileContents);

                return JsonConvert.DeserializeObject<BackgroundColorSettings>(fileContentsAsJObj["BackgroundColorSettings"].ToString());
            }

            return new BackgroundColorSettings();
        }

        public static ActivityBarSettings LoadActivityBarSettings() 
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);
            if (File.Exists(saveFile))
            {
                string fileContents = File.ReadAllText(saveFile);

                if (fileContents != null)
                {
                    JObject fileContentsAsJObj = JObject.Parse(fileContents);
                    var activityColorSettings = JObject.Parse(fileContentsAsJObj["ActivityColorSettings"].ToString());

                    var workAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("WorkColors").ToString());
                    var shortBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("ShortBreakColors").ToString());
                    var longBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("LongBreakColors").ToString());

                    ActivityBarSettings settingsToLoad = new ActivityBarSettings()
                    {
                        WorkColors = workAsObj,
                        ShortBreakColors = shortBreakAsObj,
                        LongBreakColors = longBreakAsObj
                    };

                    return settingsToLoad;
                }

                return new ActivityBarSettings();

            }

            return new ActivityBarSettings();
        }
    }
}
