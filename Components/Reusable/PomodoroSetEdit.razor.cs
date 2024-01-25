using CustomPomodoro.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Reusable
{
    public partial class PomodoroSetEdit
    {
        private async Task SaveSetInfo()
        {
            Debug.WriteLine($"Set info updated.");
            Console.WriteLine($"Set info updated");
        }

        [Parameter]
        public PomoderoSet CurSetEdit { get; set; }

    }
}
