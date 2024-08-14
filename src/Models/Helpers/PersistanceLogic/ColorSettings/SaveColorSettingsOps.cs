using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using CustomPomodoro.Models.UserSettings.Concrete;

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
                string finalJson = JsonConvert.SerializeObject(colorSettings);
                File.WriteAllText(Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName), finalJson);

                return true;
            }
            catch (Exception ex) 
            {
                Debug.WriteLine($"An error occured when saving to file: {ex}.");
                return false;
            }
        }
        
    }
}
