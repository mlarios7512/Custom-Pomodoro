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
        public string Header { get; set; } = string.Empty;

        [Parameter]
        public HSLColor BgColor { get; set; }

        [Parameter]
        public HSLColor TextColor { get; set; }
    }
}
