using CustomPomodoro.Models.UserSettings.Concrete;
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

        public static BackgroundColorSettings LoadBackgroundColorsSettings()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(saveFile))
                {
                    string fileContents = File.ReadAllText(saveFile);
                    JObject fileContentsAsJObj = JObject.Parse(fileContents);

                    return JsonConvert.DeserializeObject<BackgroundColorSettings>(fileContentsAsJObj["BackgroundColorSettings"].ToString());
                }
                else
                {
                    return new BackgroundColorSettings();
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely null or empty string. INFO: {ex}.");
                return new BackgroundColorSettings();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely malformed. INFO: {ex}.");
                return new BackgroundColorSettings();
            }
        }

        public static BackgroundColorSettings LoadBackgroundColorsSettings_V2()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(saveFile))
                {
                    string fileContents = File.ReadAllText(saveFile);
                    JObject fileContentsAsJObj = JObject.Parse(fileContents);

                    return JsonConvert.DeserializeObject<BackgroundColorSettings>(fileContentsAsJObj["BackgroundColorSettings"].ToString());
                }
                else
                {
                    return new BackgroundColorSettings();
                }
            }
            catch (System.NullReferenceException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely null or empty string. INFO: {ex}.");
                return new BackgroundColorSettings();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely malformed. INFO: {ex}.");
                return new BackgroundColorSettings();
            }
        }

        //public static ActivityBarSettings LoadActivityBarSettings() 
        //{
        //    string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

        //    try 
        //    {
        //        if (File.Exists(saveFile))
        //        {
        //            string fileContents = File.ReadAllText(saveFile);

        //            JObject fileContentsAsJObj = JObject.Parse(fileContents);
        //            var activityColorSettings = JObject.Parse(fileContentsAsJObj["ActivityColorSettings"].ToString());

        //            var workAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("WorkColors").ToString());
        //            var shortBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("ShortBreakColors").ToString());
        //            var longBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("LongBreakColors").ToString());

        //            ActivityBarSettings settingsToLoad = new ActivityBarSettings()
        //            {
        //                WorkColors = workAsObj,
        //                ShortBreakColors = shortBreakAsObj,
        //                LongBreakColors = longBreakAsObj
        //            };

        //            return settingsToLoad;
        //        }
        //        else 
        //        {
        //            return new ActivityBarSettings();
        //        }
        //    }
        //    //Newtonsoft.Json.JsonReaderException: occurs when 'fileContents' is null or empty string.
        //    //System.NullReferenceException: occurs when data within 'fileContents' is malformed. 
        //    catch (System.NullReferenceException ex) 
        //    {
        //        Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely null or empty string. INFO: {ex}.");
        //        return new ActivityBarSettings();
        //    }
        //    catch (Newtonsoft.Json.JsonReaderException ex) 
        //    {
        //        Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely malformed. INFO: {ex}.");
        //        return new ActivityBarSettings();
        //    }
        //}

        public static ActivityBarSettings LoadActivityBarSettings_V2()
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

            try
            {
                if (File.Exists(saveFile))
                {
                    string fileContents = File.ReadAllText(saveFile);

                    JObject fileContentsAsJObj = JObject.Parse(fileContents);
                    var activityColorSettings = JObject.Parse(fileContentsAsJObj.SelectToken("ActivityColorSettings").ToString());

                    //Approach 1 starts here--------
                    //This might be returning 4 colors because it's getting both "getters" & "setters"? Need to check return contents first.
                    //Both default values & json values are being loaded. Don't know why. Could be becuase of private properties & public ones?

                    //var z = JsonConvert.DeserializeObject<ActivityBarSettings>(activityColorSettings.ToString());
                    //return z;

                    //End of aproach 1 ---------------

                    //Approach 2 starts here

                    var workAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("WorkColors").ToString());
                    var shortBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("ShortBreakColors").ToString());
                    var longBreakAsObj = JsonConvert.DeserializeObject<List<HSLColor>>(activityColorSettings.SelectToken("LongBreakColors").ToString());
                    bool enableActivityBar = (bool)activityColorSettings.SelectToken("EnableActivityBar");

                    ActivityBarSettings settingsToLoad = new ActivityBarSettings()
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
                    return new ActivityBarSettings();
                }
            }
            //Newtonsoft.Json.JsonReaderException: occurs when 'fileContents' is null or empty string.
            //System.NullReferenceException: occurs when data within 'fileContents' is malformed. 
            catch (System.NullReferenceException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely null or empty string. INFO: {ex}.");
                return new ActivityBarSettings();
            }
            catch (Newtonsoft.Json.JsonReaderException ex)
            {
                Debug.WriteLine($"Error extracting 'activity bar colors' from file. File contents likely malformed. INFO: {ex}.");
                return new ActivityBarSettings();
            }
        }
    }
}
