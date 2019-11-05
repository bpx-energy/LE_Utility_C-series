using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDive.Common.Models
{
     public class AppSettings
    {
        public string AuthorityUri { get; set; }
        public string ResourceUrl { get; set; }
        public string RedirectUrl { get; set; }
        public string ApiUrl { get; set; }
        public string ApplicationId { get; set; }
        public string LoggingRequestUrl { get; set; }
        public string GroupId { get; set; }
        public string ReportId { get; set; }
        public string ClientSecret { get; set; }

        public string DBConnectionString { get; set; }
        public string TenantId { get; set; }
        public string FromEmailAddress { get; set; }
        public string SMTP_Password { get; set; }
        public string SMTP_Host { get; set; }
        public string Client_Url { get; set; }
    }
}
