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

        public async static Task SaveAllColorSettings(MainColorSettings colorSettings)
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Need storage permission", "Storage permission is required to create a save file.", "OK");
            }

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
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

                    //See the "Preferences API for saving user data cross-platform (Note: using this only for minor differences, such as vibration vs toast on timer end):
                    //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows

                    await Application.Current.MainPage.DisplayAlert("Alert", "Changes have been saved.", "OK");
                }
                catch (Newtonsoft.Json.JsonReaderException ex) 
                {
                    Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely malformed. INFO: {ex}.");
                    await Application.Current.MainPage.DisplayAlert("Error", "There was a problem saving the color settings to file", "OK");
                }
                
            }
        }

        
    }
}
