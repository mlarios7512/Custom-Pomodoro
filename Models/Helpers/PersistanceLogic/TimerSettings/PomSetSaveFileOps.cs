using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings
{
    internal class PomSetSaveFileOps
    {
        private const string _saveFileName = "PomodoroSets.json";
        //// This version was intended when multiple pomodoro sets were going to be implemented.
        //public static void CreateNewSaveFile(PomodoroSet newPomoderoSet)
        //{
        //    List<PomodoroSet> PomSetsToCreate = new List<PomodoroSet> { newPomoderoSet };

        //    var convertedJson = JsonConvert.SerializeObject(PomSetsToCreate, Formatting.Indented);
        //    File.WriteAllText(Path.Combine(AppContext.BaseDirectory, "PomodoroSets.json"), convertedJson);
        //}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="newPomodoroSet"></param>
        /// <returns>A bool indicating if save was sucessful. (True if successful. False if otherwise)</returns>
        public static bool CreateNewSaveFile(PomodoroSet newPomodoroSet) 
        {
            try 
            {
                var convertedJson = JsonConvert.SerializeObject(newPomodoroSet, Formatting.Indented);
                File.WriteAllText(Path.Combine(AppContext.BaseDirectory, _saveFileName), convertedJson);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
           
        }

    }
}
