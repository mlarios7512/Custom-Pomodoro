﻿using CustomPomodoro.Models.UserSettings.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomPomodoro.Models.Helpers.PersistanceLogic.TimerSettings.PomSetLoadFileOps;

namespace CustomPomodoro.Models.UserSettings.Abstract
{
    public interface IMasterUserSettings
    {
        Task LoadAllColorSettings();
        BackgroundColorOptions GetBackgroundColorSettings();
        ActivityBarOptions GetActivityBarSettings();
        MainColorSettings GetMainColorSettings();
        PomodoroSet GetCurPomodoroSet();
        Task SaveUserPomodoroSet(PomodoroTimerSettings setttingsToSave);
        Task LoadCurPomodoroSet();

        Task SaveUserColorSettings(MainColorSettings settingsToSave);

    }
}
