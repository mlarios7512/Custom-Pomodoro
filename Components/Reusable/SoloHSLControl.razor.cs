using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

using CustomPomodoro.Models;

namespace CustomPomodoro.Components.Reusable
{
    public partial class SoloHSLControl
    {
        [Parameter]
        public HSLColor Color { get; set; }

        [Parameter]
        public string Header { get; set; } = string.Empty;
    }
}
