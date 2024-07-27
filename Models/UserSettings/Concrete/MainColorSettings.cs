using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    //This is intended as a POCO to serialize JSON data. Do NOT uses interfaces for this (or any of it's child classes).
    public class MainColorSettings
    {
        public ActivityBarSettings ActivityColorSettings { get; set; } = new();
        public BackgroundColorSettings BackgroundColorSettings { get; set; } = new();
    }
}
