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

namespace CustomPomodoro.Components.Reusable
{
    public partial class PomodoroSetEdit
    {
        PomoderoSet NewPomodoroSet = new PomoderoSet();

        private async Task SaveSetInfo()
        {
            string IdToStore = Guid.NewGuid().ToString();


            Debug.WriteLine($"Set info updated.");

            Debug.WriteLine($"ID: {NewPomodoroSet.Id}");
            Debug.WriteLine($"Name: {NewPomodoroSet.Name}");
            Debug.WriteLine($"Work: {NewPomodoroSet.WorkTime}");
            Debug.WriteLine($"Short break: {NewPomodoroSet.ShortBreak}");
            Debug.WriteLine($"Long break: {NewPomodoroSet.LongBreak}");
            Debug.WriteLine($"Reps before long break: {NewPomodoroSet.RepsBeforeLongBreak}");

            if (NewPomodoroSet != null) 
            {
                //var options = new JsonSerializerOptions { WriteIndented = true };
                //string SetAsJson = System.Text.Json.JsonSerializer.Serialize(NewPomodoroSet, options);  //1st attempt specific.

                //Load file (check if it exists 1st)
                string JsonSavePath = Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json");

                string PomListsFromJsonFile = File.ReadAllText(JsonSavePath);




                var ExistingPomLists = JsonConvert.DeserializeObject<List<PomoderoSet>>(PomListsFromJsonFile);

                List<PomoderoSet> PomoderoSets = new List<PomoderoSet>();


                NewPomodoroSet.Id = Guid.NewGuid().ToString();

                ExistingPomLists.Add(NewPomodoroSet);
                var convertedJson = JsonConvert.SerializeObject(ExistingPomLists, Formatting.Indented);
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);

                /*  File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), SetAsJson); *///1st attempt specific.
            }
            

            //Need to review how to save to local file. (Also, run this on Android emulator to see how it works on there.


            //Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json");

        }
    }
}
