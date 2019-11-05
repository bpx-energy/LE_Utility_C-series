using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class DrillVarianceDuration
    {
        public string Api10 { get; set; }
        public string ActivityPhase { get; set; }
        public decimal Depth { get; set; }
        public decimal CumPhaseDuration { get; set; }
        public decimal AfeCumPhaseDuration { get; set; }
        public decimal CumWellDuration { get; set; }
        public string ActivityId { get; set; }
        public string RecordedDate { get; set; }
        public decimal VarianceAmount { get; set; }
        public string VarianceDurationId { get; set; }
        public string VarianceType { get; set; }
        public string VarianceComment { get; set; }
    }
}
