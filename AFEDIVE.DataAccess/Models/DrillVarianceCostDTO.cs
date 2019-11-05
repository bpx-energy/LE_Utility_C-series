using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class DrillVarianceCostDTO
    {
        public string API10 { get; set; }
        public string ACTIVITY_PHASE { get; set; }
        public string DATE_YMD { get; set; }
        public decimal MAX_DEPTH { get; set; }
        public decimal CUM_PHASE_COST { get; set; }
        public decimal AFE_CUM_PHASE_COST { get; set; }
        public decimal CUM_WELL_COST { get; set; }
        public decimal VARIANCE_AMOUNT { get; set; }
        public string VARIANCE_COST_ID { get; set; }
        public string VARIANCE_TYPE { get; set; }
        public string VARIANCE_COMMENT { get; set; }
    }
}
