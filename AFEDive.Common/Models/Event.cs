using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class Event
    {
        public int EventId { get; set; }
        public string WellName { get; set; }
        public string VarianceDurationId { get; set; }
        public string VarianceCostId { get; set; }
        public string Title { get; set; }
        public Guid? Level1 { get; set; }
        public string Level1_Text { get; set; }
        public Guid? Level2 { get; set; }
        public string Level2_Text { get; set; }
        public Guid? ResponsibleParty { get; set; }
        public string ResponsibleParty_Text { get; set; }
        public string VarianceType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string LoggedInUserName { get; set; }
        public int EventStatus { get; set; }

        public string ClosedStatus { get; set; }

    }
}
