using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public  class EventDTO
    {
        public int EVENT_ID { get; set; }
        public string WELL_COMMON_NAME { get; set; }

        public string VARIANCE_DURATION_ID { get; set; }
        public string VARIANCE_COST_ID { get; set; }
        public string TITLE { get; set; }
        public Guid? LEVEL1 { get; set; }
        public string LEVEL1_TEXT { get; set; }
        public Guid? LEVEL2 { get; set; }
        public string LEVEL2_TEXT { get; set; }
        public Guid? RESPONSIBLE_PARTY { get; set; }
        public string RESPONSIBLE_PARTY_TEXT { get; set; }

        public string VARIANCE_TYPE { get; set; }
        public string ROW_CREATE_ID { get; set; }
        public DateTime ROW_CREATE_DATE { get; set; }
        public string LOGGED_IN_USERNAME { get; set; }
        public int EventStatus { get; set; }
        public string ClosedStatus { get; set; }

    }
}
