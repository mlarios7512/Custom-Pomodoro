using CustomPomodoro.ViewModels.InputClones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    public class PomodoroTimerSettings
    {
        public PomodoroSet StoredPomSet { get; set; } = new();
    }
}
