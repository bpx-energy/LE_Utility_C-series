using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class CommentDTO
    {
        public Guid COMMENT_ID { get; set; }
        public string COMMENT { get; set; }
        public int EVENT_ID { get; set; }
        public DateTime ROW_CREATE_DATE { get; set; }
        public string ROW_CREATE_ID { get; set; }
        public DateTime ROW_UPDATE_DATE { get; set; }
        public string ROW_UPDATE_ID { get; set; }
        public bool IS_DELETED { get; set; }

    }
}
