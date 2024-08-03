using CustomPomodoro.Models.UserSettings.Concrete;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings
{
    public class SaveColorSettingsOps
    {
        private const string _saveFileName = "ColorSettings.json";
        public SaveColorSettingsOps() 
        {

        }

        /// <summary>
        /// Saves new user colorSettings
        /// </summary>
        /// <param name="colorSettings">The new user color settings to save.</param>
        /// <returns>Returns False if an error occured when saving. Returns true otherwise (including if permission was denied).</returns>
        public async static Task<bool> SaveAllColorSettings(MainColorSettings colorSettings)
        {
            try
            {
                string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);
                if (!File.Exists(saveFile))
                {
                    File.Create(saveFile);
                }
                string finalJson = JsonConvert.SerializeObject(colorSettings);
                File.WriteAllText(saveFile, finalJson);

                //See the "Preferences API for saving user data cross-platform
                // (Note: Currently using this only for minor differences, such as vibration on Android vs toast on Windows for timer end):
                //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows

                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"An unknown error occured when saving to file: {ex}.");
                return false;
            }
        }
        
    }
}
