using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class EventFilterParameters
    {
        public string WellId { get; set; }
        public string Area { get; set; }
        public string Function { get; set; }
        public string Phase { get; set; }
        public string Operation { get; set; }
        public Guid? Level1 { get; set; }
        public Guid? Level2 { get; set; }
        public string EventStatus { get; set; }
        public string UserId { get; set; }
        public Enums.Filter Filter { get; set; }
        public List<string> Status { get; set; }


    }
}
