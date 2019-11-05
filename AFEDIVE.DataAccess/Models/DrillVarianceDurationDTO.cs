using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class DrillVarianceDurationDTO
    {
        public string API10 { get; set; }
        public string ACTIVITY_PHASE { get; set; }
        public decimal DEPTH { get; set; }
        public decimal CUM_PHASE_DURATION { get; set; }
        public decimal AFE_CUM_PHASE_DURATION { get; set; }
        public decimal CUM_WELL_DURATION { get; set; }
        public string ACTIVITY_ID { get; set; }

        public decimal VARIANCE_AMOUNT { get; set; }
        public string VARIANCE_DURATION_ID { get; set; }
        public DateTimeOffset ROW_CREATE_DATE { get; set; }
        public string VARIANCE_TYPE { get; set; }
        public string VARIANCE_COMMENT { get; set; }
    }
}
