﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings
{
    public class PomSetLoadFileOps
    {
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

        public static Task LoadCurrentPomodoroSetFromFile(ref PomodoroSet pomSet)
        {
            string JsonSavePath = Path.Combine(AppContext.BaseDirectory, _saveFileName);

            if (File.Exists(JsonSavePath)) 
            {
                string PomSetFromJsonFile = File.ReadAllText(JsonSavePath);
                pomSet = JsonConvert.DeserializeObject<PomodoroSet>(PomSetFromJsonFile);

                if (pomSet == null) 
                {
                    pomSet = new PomodoroSet();
                }
                
            }
            else 
            {
                pomSet = new PomodoroSet();
            }
            return Task.FromResult(pomSet);
        }
    }
}
