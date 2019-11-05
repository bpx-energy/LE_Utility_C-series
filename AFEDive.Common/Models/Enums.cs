using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public static class Enums
    {
        public enum EventStatus
        {
            All = 0,
            OpenAssigned = 1,
            OpenUnassigned = 2,
            Closed = 3
        }

        public enum Filter
        {
            Charts = 0,
            Events = 1,
            
        }
    }
}
