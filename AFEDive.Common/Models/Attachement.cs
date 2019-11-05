using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class Attachment
    {
        public Guid AttachmentId { get; set; }
        public string Path { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
