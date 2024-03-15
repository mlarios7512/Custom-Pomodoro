using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers
{
    internal class PomSetFileOps
    {
        public static void CreateNewSaveFile(PomoderoSet newPomoderoSet) 
        {
            List<PomoderoSet> PomSetsToCreate = new List<PomoderoSet> { newPomoderoSet };

            var convertedJson = JsonConvert.SerializeObject(PomSetsToCreate, Formatting.Indented);
            File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);
        }
       
    }
}
