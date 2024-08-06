using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models.UserSettings.Concrete
{
    //This is intended as a POCO to serialize JSON data.
    public class MainColorSettings
    {
        public ActivityBarOptions ActivityColorSettings { get; set; } = new();
        public BackgroundColorOptions BackgroundColorSettings { get; set; } = new();
    }
}
