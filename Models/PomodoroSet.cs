using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPomodoro.Models
{
    public class PomoderoSet
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;

        public TimeSpan? ShortWorkTime = null;
        public TimeSpan? ShortBreakTime = null;
        public int? ShortSessionReps = null;

        public TimeSpan? LongWorkTime = null;
        public TimeSpan? LongBreakTime = null;
    }
}
