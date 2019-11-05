using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class DrillTimeSummaryDTO
    {
        public string ACTIVITY_ID { get; set; }
        public string ACTIVITY_PHASE { get; set; }
        public decimal DEPTH { get; set; }
        public DateTime TIME_FROM { get; set; }
        public string DATE_YMD { get; set; }
        public decimal ACTIVITY_DURATION { get; set; }
        public decimal CUM_PHASE_DURATION { get; set; }
        public decimal CUM_WELL_DURATION { get; set; }
    }
}
