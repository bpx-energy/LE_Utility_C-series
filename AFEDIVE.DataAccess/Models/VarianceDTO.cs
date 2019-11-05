using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class VarianceDTO
    {
        public string BU { get; set; }
        public string API10 { get; set; }
        public string WELL_COMMON_NAME { get; set; }
        public DateTimeOffset ROW_CREATE_DATE { get; set; }
        public string ACTIVITY_PHASE { get; set; }
    }
}
