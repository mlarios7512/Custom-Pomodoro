﻿using CustomPomodoro.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Components.Layout
{
    public partial class MainLayout
    {
        //Set up using this link: https://stackoverflow.com/questions/71713761/how-can-i-declare-a-global-variables-model-in-blazor -- Daniël J.M. Hoffman
        public static PomodoroSet CurPomodoroSet { get; set; } = new();
        
    }
}