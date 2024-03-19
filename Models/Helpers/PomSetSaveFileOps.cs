using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers
{
    internal class PomSetSaveFileOps
    {
        public static void CreateNewSaveFile(PomodoroSet newPomoderoSet) 
        {
            List<PomodoroSet> PomSetsToCreate = new List<PomodoroSet> { newPomoderoSet };

            var convertedJson = JsonConvert.SerializeObject(PomSetsToCreate, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);
        }
       
    }
}
