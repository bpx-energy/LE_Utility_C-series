using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class DrillDailyCost
    {
        public string Api10 { get; set; }
        public string ActivityPhase { get; set; }
        public string DateYmd { get; set; }
        public decimal MaxDepth { get; set; }
        public decimal CumPhaseCost { get; set; }
        public decimal CumWellCost { get; set; }

    }
}
