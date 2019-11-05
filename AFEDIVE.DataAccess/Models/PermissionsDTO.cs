using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Models
{
    public class PermissionsDTO
    {
        public int Id { get; set; }
        public string Business_Unit { get; set; }
        public string Group { get; set; }
        public int Permission { get; set; }
        public string GroupId { get; set; }
    }
}
