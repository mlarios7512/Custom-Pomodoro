using CustomPomodoro.Components.Reusable;
using CustomPomodoro.Models.UserSettings.Concrete;
using Microsoft.AspNetCore.Components;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class ColorSettings
    {
        [CascadingParameter]
        MasterUserSettings UserSettings { get; set; }
        private BackgroundColorSettings BgColorInputs = new BackgroundColorSettings();
        private ActivityBarSettings ColorBarInputs { get; set; } = new ActivityBarSettings();
        private bool DisplayAdvancedColorSettings { get; set; } = false;
        private const string _saveFileName = "ColorSettings.json";
        private List<LinkedHSLControl> PrimaryActivityStatusColorControls { get; set; } = new List<LinkedHSLControl>(3)
        {
            new LinkedHSLControl(),
            new LinkedHSLControl(),
            new LinkedHSLControl()
        };
        private List<LinkedHSLControl> SecondaryActivityStatusColorControls { get; set; } = new List<LinkedHSLControl>(3) 
        {
            new LinkedHSLControl(),
            new LinkedHSLControl(),
            new LinkedHSLControl()
        };
        private List<SoloHSLControl> BgColorControls { get; set; } = new List<SoloHSLControl>()
        {
            new SoloHSLControl(),
            new SoloHSLControl(),
            new SoloHSLControl()
        };

        //Question: Can you call a method on a child component from "OnInitializedAsync"?
        protected override async Task OnInitializedAsync()
        {
            //Load all settings. If no save file exists, just skip to setting all inputs to default color values.
            await UserSettings.LoadAllSettings();


            //Best to transfer global "UserSettings" bgColors & activityBarColors into local colors.
            // (This is because even if you don't click "save", any color changes made during the current session will be visible in the "timer" page.)
            BgColorInputs = UserSettings._backgroundColorSettings;
            ColorBarInputs = UserSettings._activityBarSettings;
        }

        private async Task GetDefaultBgColorValues() 
        {
            BgColorInputs.SetDefaultColorsValues();
        }

        private async Task GetDefaultActivityBarColorValues() 
        {
            ColorBarInputs.SetAllColorsToDefaultValues();
        }

        //No async (uses a shared file).
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

        //No async (uses a shared file).
        public static ActivityBarSettings LoadActivityBarSettings() 
        {
            string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);
            if (File.Exists(saveFile)) 
            {
                string fileContents = File.ReadAllText(saveFile);
                JObject fileContentsAsJObj = JObject.Parse(fileContents);

                return JsonConvert.DeserializeObject<ActivityBarSettings>(fileContentsAsJObj["ActivityColorSettings"].ToString());
            }

            return new ActivityBarSettings();
        }


        //Just make 1 giant save method in this page. (& STOP ADDING NEW FEATURES! FIX WHAT YOU HAVE!)
        public async Task SaveColorChanges() 
        {
            PermissionStatus status = PermissionStatus.Unknown;
            status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted)
            {
                await Application.Current.MainPage.DisplayAlert("Need storage permission", "Storage permission is required to create a save file.", "OK");
            }

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if(status == PermissionStatus.Granted) 
            {
                string saveFile = Path.Combine(FileSystem.Current.AppDataDirectory, _saveFileName);

                if (!File.Exists(saveFile)) 
                {
                    File.Create(saveFile);
                }
                    



                string bgSettingsAsJson = JsonConvert.SerializeObject(BgColorInputs);
                JObject bgSettingsWithTitle = new JObject();
                bgSettingsWithTitle["BackgroundColorSettings"] = bgSettingsAsJson;

                string barSettingsAsJson = JsonConvert.SerializeObject(ColorBarInputs);
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
            }


           

            //See the "Preferences API for saving user data (NOT SURE THIS IS THE BEST WAY for cross-platform saving. NEED TO VERIFY):
            //https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/preferences?view=net-maui-8.0&tabs=windows


            //See YouTube video for what you were doing: https://www.youtube.com/watch?v=3xqIXS1SBaU
            //Before you continue, it might be best to create a class(es)/interface(s) for saving data as different formats. (In case of future use. It's also good practice for SOLID).
            //string saveDataLocation = FileSystem.Current.AppDataDirectory;
            //string saveFileLocationOfColorSettings = Path.Combine(saveDataLocation, "ColorSettings.json");

        }

        private void ToggleAdvancedSettings()
        {
            DisplayAdvancedColorSettings = !DisplayAdvancedColorSettings;
            if (DisplayAdvancedColorSettings == true)
            {
                foreach (var control in PrimaryActivityStatusColorControls)
                    control.DisplayAllHslColorControls();
                
                foreach (var control in SecondaryActivityStatusColorControls)
                    control.DisplayAllHslColorControls();   
            }
            else
            {
                ColorBarInputs.ResetAllColorValuesExceptPrimaryHue();
                foreach (var control in PrimaryActivityStatusColorControls) 
                {
                    control.DisplayHueControlsOnly();
                }
                foreach(var control in SecondaryActivityStatusColorControls) 
                {
                    control.HideAllHslControls();
                }
            }
        }

        private void MakeSimilarStageOneAndTwoColors()
        {
            ColorBarInputs.WorkColors.Last().Hue = ColorBarInputs.WorkColors.First().Hue;
            ColorBarInputs.ShortBreakColors.Last().Hue = ColorBarInputs.ShortBreakColors.First().Hue;
            ColorBarInputs.LongBreakColors.Last().Hue = ColorBarInputs.LongBreakColors.First().Hue;
        }

        private void UpdateSecondaryColorIfNeeded() 
        {
            if(DisplayAdvancedColorSettings == false) 
            {
                MakeSimilarStageOneAndTwoColors();
            }
            
        }
    }
}
