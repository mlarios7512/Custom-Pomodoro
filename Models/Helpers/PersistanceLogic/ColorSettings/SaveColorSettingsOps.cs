using CustomPomodoro.Models.UserSettings.Concrete;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings
{
    public class SaveColorSettingsOps
    {
        private const string _saveFileName = "ColorSettings.json";
        public SaveColorSettingsOps() 
        {

        }

        public async static Task SaveSettingsToJSON(BackgroundColorSettings bgColorInputs, ActivityBarSettings colorBarInputs) 
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);
            if (!File.Exists(saveFile))
            {
                File.Create(saveFile);
            }

            string bgSettingsAsJson = JsonConvert.SerializeObject(bgColorInputs);
            JObject bgSettingsWithTitle = new JObject();
            bgSettingsWithTitle["BackgroundColorSettings"] = bgSettingsAsJson;

            string barSettingsAsJson = JsonConvert.SerializeObject(colorBarInputs);
            JObject barSettingsWithTitle = new JObject();
            barSettingsWithTitle["ActivityColorSettings"] = barSettingsAsJson;


            //Merging solution taken from "David Rettenbacher": https://stackoverflow.com/questions/21160337/how-can-i-merge-two-jobject
            var mergeSettings = new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            };

            bgSettingsWithTitle.Merge(barSettingsWithTitle, mergeSettings);
            string finalJson = bgSettingsWithTitle.ToString();

            File.WriteAllText(saveFile, finalJson);

            //See the "Preferences API for saving user data (NOT SURE THIS IS THE BEST WAY for cross-platform saving. NEED TO VERIFY):
            //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows

            await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
        }
        
    }
}
