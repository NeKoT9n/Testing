using System.Collections.Generic;
namespace TestWPF.Models
{
    class User
    {
        public string Name { get; set; }
        public int Steps { get; set; }
        public int AvarageResult { get; set; }
        public int BestResult { get; set; }
        public int WorstResult { get; set; }
        public Dictionary<int, DayResult> DaySteps { get; set; }

    }
}
