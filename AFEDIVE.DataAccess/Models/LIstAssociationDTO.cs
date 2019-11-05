using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class ListAssociationDTO
    {
        public Guid LIST_ASSOCIATION_ID { get; set; }
        public string DESCRIPTION { get; set; }
        public string TYPE { get; set; }
        public Guid PARENT_ID { get; set; }
        public bool IS_DELETED { get; set; }
        public int ORDER { get; set; }

    }
}
