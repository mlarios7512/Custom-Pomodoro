using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    internal class SoloPomodoroSettings
    {
        public PomodoroSet SetToEdit { get; set; }
        public bool AutoStartTimerSessions { get; set; }

        public SoloPomodoroSettings() 
        {
        }
    }
}
