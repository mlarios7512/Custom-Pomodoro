using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.ColorSettings
{
    public class LoadColorSettingsOps
    {
        private const string _saveFileName = "ColorSettings.json";
        public LoadColorSettingsOps() { }

        public static BackgroundColorOptions LoadBackgroundColorsSettings()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(saveFile))
                {
                    string fileContents = File.ReadAllText(saveFile);
                    JObject fileContentsAsJObj = JObject.Parse(fileContents);

                    return JsonConvert.DeserializeObject<BackgroundColorOptions>(fileContentsAsJObj["BackgroundColorSettings"].ToString());
                }
                else
                {
                    return new BackgroundColorOptions();
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. (File contents likely null or empty string.) ERROR INFO: {ex}.");
                return new BackgroundColorOptions();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. (File contents likely malformed.) ERROR INFO: {ex}.");
                return new BackgroundColorOptions();
            }
        }

        public static ActivityBarOptions LoadActivityBarSettings()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(saveFile))
                {
                    string fileContents = File.ReadAllText(saveFile);

                    JObject fileContentsAsJObj = JObject.Parse(fileContents);
                    var activityColorSettings = JObject.Parse(fileContentsAsJObj.SelectToken("ActivityColorSettings").ToString());

                    //Alternative approach notes (below)--------
                    //This might be returning 4 colors because it's getting both "getters" & "setters"? Need to check return contents first.
                    //Both default values & json values are being loaded. (Haven't figured out why.)

                    //var z = JsonConvert.DeserializeObject<ActivityBarSettings>(activityColorSettings.ToString());
                    //return z;

                    //Alternative approach notes (above) ---------------


                    //Not ideal, but works for now.
                    var workAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("WorkColors").ToString());
                    var shortBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("ShortBreakColors").ToString());
                    var longBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("LongBreakColors").ToString());
                    bool enableActivityBar = (bool)activityColorSettings.SelectToken("EnableActivityBar");

                    ActivityBarOptions settingsToLoad = new ActivityBarOptions()
                    {
                        WorkColors = workAsObj,
                        ShortBreakColors = shortBreakAsObj,
                        LongBreakColors = longBreakAsObj,
                        EnableActivityBar = enableActivityBar,
                    };

                    return settingsToLoad;
                }
                else
                {
                    return new ActivityBarOptions();
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. (File contents likely null or empty string.)\n ERROR INFO: {ex}.");
                return new ActivityBarOptions();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. (File contents likely malformed.)\n ERROR INFO: {ex}.");
                return new ActivityBarOptions();
            }
        }
    }
}
