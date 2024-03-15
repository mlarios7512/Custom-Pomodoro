using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using CustomPomodoro.Models.Helpers;

namespace CustomPomodoro.Components.Reusable
{
    public partial class PomodoroSetEdit
    {
        PomoderoSet NewPomodoroSet = new PomoderoSet();

        private async Task SaveSetInfo()
        {
            string NewPomodoroId = Guid.NewGuid().ToString();
            NewPomodoroSet.Id = NewPomodoroId;

            Debug.WriteLine($"Set info updated.");

            Debug.WriteLine($"ID: {NewPomodoroSet.Id}");
            Debug.WriteLine($"Name: {NewPomodoroSet.Name}");
            Debug.WriteLine($"Work: {NewPomodoroSet.WorkTime}");
            Debug.WriteLine($"Short break: {NewPomodoroSet.ShortBreak}");
            Debug.WriteLine($"Long break: {NewPomodoroSet.LongBreak}");
            Debug.WriteLine($"Reps before long break: {NewPomodoroSet.RepsBeforeLongBreak}");

            if (NewPomodoroSet != null)
            {
                string JsonSavePath = Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json");
                if (File.Exists(JsonSavePath))
                {
                    Debug.WriteLine("File exists. Adding set to file...");
                    string PomListsFromJsonFile = File.ReadAllText(JsonSavePath);
                    var ExistingPomLists = JsonConvert.DeserializeObject<List<PomoderoSet>>(PomListsFromJsonFile);

                    if(ExistingPomLists == null) 
                    {
                        PomSetFileOps.CreateNewSaveFile(NewPomodoroSet);
                    }
                    else 
                    {
                        ExistingPomLists.Add(NewPomodoroSet);
                        var convertedJson = JsonConvert.SerializeObject(ExistingPomLists, Formatting.Indented);

                        //Need to see where data is saved on Android (try it on emulator).
                        File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);
                    }
                }
                else
                {
                    PomSetFileOps.CreateNewSaveFile(NewPomodoroSet);
                }
            }
            
        }
    }
}
