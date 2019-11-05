using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class ListAssociation
    {
        public Guid ListAssicationId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public Guid ParentId { get; set; }
        public bool IsDeleted { get; set; }
        public int Order { get; set; }
    }
}
