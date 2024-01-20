using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

using CustomPomodoro.Models;

namespace CustomPomodoro.Components.Pages
{
    public partial class PomodoroSets
    {
        [Parameter]
        public PomoderoSet TestSet { get; set; } = new();
       
    }
}
