using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class Comment
    {
        public Guid CommentId { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
