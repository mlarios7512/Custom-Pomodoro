using CustomPomodoro.Models;
using CustomPomodoro.Models.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Pages
{
    public partial class PomodoroSets
    {
        public List<PomodoroSet> UserPomodoroSets { get; set; } = PomSetLoadFileOps.GetExistingPomodoroSets();



        
    }
}
