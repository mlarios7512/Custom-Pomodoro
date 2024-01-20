using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Reusable
{
    public partial class PomodoroSetEdit
    {
        [Parameter]
        public PomoderoSet CurSet { get; set; }
    }
}
