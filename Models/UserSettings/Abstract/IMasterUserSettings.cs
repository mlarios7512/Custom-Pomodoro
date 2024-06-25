using CustomPomodoro.Models.UserSettings.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Abstract
{
    public interface IMasterUserSettings
    {
        Task LoadAllSettings();
        BackgroundColorSettings GetBackgroundColorSettings();
        ActivityBarSettings GetActivityBarSettings();
        PomodoroSet GetCurPomodoroSet();
    }
}
