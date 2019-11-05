using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
    public class ADGroup
    {
        [JsonProperty("@odata.context")]
        public string odatacontext { get; set; }

        [JsonProperty("value")]
        public List<Member> Members { get; set; }
    }

    public class Member
    {
        [JsonProperty("@odata.type")]
        public string odatatype { get; set; }
        public string id { get; set; }
        public object[] businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public string jobTitle { get; set; }
        public string mail { get; set; }
        public string mobilePhone { get; set; }
        public object officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
    }


}
