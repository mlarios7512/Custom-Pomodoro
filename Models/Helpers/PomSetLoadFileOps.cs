using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers
{
    internal class PomSetLoadFileOps
    {
        public static List<PomodoroSet> GetExistingPomodoroSets()
        {
            string JsonSavePath = Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json");
            List<PomodoroSet> ExistingPomLists = new List<PomodoroSet>();
            try 
            {
                string PomListsFromJsonFile = File.ReadAllText(JsonSavePath);
                ExistingPomLists = JsonConvert.DeserializeObject<List<PomodoroSet>>(PomListsFromJsonFile);
            }
            catch (System.IO.FileNotFoundException) 
            {
                return new List<PomodoroSet>();
            }

            if (ExistingPomLists == null)
                return new List<PomodoroSet>();

            return ExistingPomLists;
        }
    }
}
