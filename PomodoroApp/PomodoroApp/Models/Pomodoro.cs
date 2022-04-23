using System;
using System.Collections.Generic;
using System.Text;

namespace PomodoroApp.Models
{
    public class Pomodoro
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int Duration { get; set; }
        public int BreakDuration { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime LastUsed { get; set; }
    }
}
