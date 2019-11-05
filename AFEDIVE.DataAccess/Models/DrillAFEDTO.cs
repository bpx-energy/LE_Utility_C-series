using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class DrillAFEDTO
    {
        public string API10 { get; set; }
        public string ACTIVITY_PHASE { get; set; }
        public string DATE_YMD { get; set; }
        public decimal DEPTH { get; set; }
        public decimal CUM_Well_DURATION { get; set; }
        public decimal CUM_WELL_COST { get; set; }
    }
}
