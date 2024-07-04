using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings
{
    public class PomSetLoadFileOps
    {

        public enum PomodoroLoadSetStatus 
        {
            //Error = -1,
            NoSetFound = 0,
            Success = 1,
        }

        private const string _saveFileName = "PomodoroSets.json";

        //public static List<PomodoroSet> GetExistingPomodoroSets()
        //{
        //    string JsonSavePath = Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json");
        //    List<PomodoroSet> ExistingPomLists = new List<PomodoroSet>();
        //    try
        //    {
        //        string PomListsFromJsonFile = File.ReadAllText(JsonSavePath);
        //        ExistingPomLists = JsonConvert.DeserializeObject<List<PomodoroSet>>(PomListsFromJsonFile);
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        return new List<PomodoroSet>();
        //    }

        //    if (ExistingPomLists == null)
        //        return new List<PomodoroSet>();

        //    return ExistingPomLists;
        //}

        public static PomodoroLoadSetStatus LoadCurrentPomodoroSetFromFile(ref PomodoroSet pomSet)
        {
            string JsonSavePath = Path.Combine(AppContext.BaseDirectory, _saveFileName);
            PomodoroLoadSetStatus LoadSetStatus = PomodoroLoadSetStatus.Success;

            if (File.Exists(JsonSavePath)) 
            {
                string PomSetFromJsonFile = File.ReadAllText(JsonSavePath);
                pomSet = JsonConvert.DeserializeObject<PomodoroSet>(PomSetFromJsonFile);

                if (pomSet == null) 
                {
                    LoadSetStatus = PomodoroLoadSetStatus.NoSetFound;
                    pomSet = new PomodoroSet();
                }
                
            }
            else 
            {
                pomSet = new PomodoroSet();
                LoadSetStatus = PomodoroLoadSetStatus.NoSetFound;
            }
            
            return LoadSetStatus;
        }
    }
}
