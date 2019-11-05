using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class DrillTimeSummary
    {
        public string ActivityId { get; set; }
        public string ActivityPhase { get; set; }
        public decimal Depth { get; set; }
        public DateTime TimeFrom { get; set; }
        public string Date_Ymd { get; set; }
        public decimal ActivityDuration { get; set; }
        public decimal CumPhaseDuration { get; set; }
        public decimal CumWellDuration { get; set; }


    }
}
