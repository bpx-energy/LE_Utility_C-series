using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AFEDIVE.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AFEDive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VarianceNotificationsController : ControllerBase
    {
        private readonly ILogger _logger;
        IDrillingRepository _drillingRepository;
        IUserRepository _userRepository;
        private static IOptions<AppSettings> _appSettings;

        public VarianceNotificationsController(ILogger<VarianceNotificationsController> logger, 
            IDrillingRepository drillingRepository, 
            IUserRepository userRepository, 
            IConfiguration configuration, 
            IOptions<AppSettings> options)
        {
            _logger = logger;
            _drillingRepository = drillingRepository;
            _userRepository = userRepository;
            _appSettings = options;
        }

        [HttpGet]
        [Route("run")]
        public async Task<string> Run()
        {
            try
            {
                // Getting all the newly created variances
                var variances = await _drillingRepository.GetDrillVarianceDurationByDate();

                // Getting all user groups
                var userGroups = await _userRepository.GetUserGroups();

                // Looping through all variances 
                foreach (var item in variances)
                {
                    // get groups based on BU
                    var groups = userGroups.Where(x => x.Business_Unit.ToLower() == item.BU.ToLower());

                    if (groups.Any())
                    {
                        var accessToken = GetAccessToken();

                        if (accessToken != string.Empty)
                        {
                            foreach (var group in groups)
                            {
                                // Reading group members from Azure AD
                                var groupDetails = await GetGroupMembersAsync(accessToken, group.GroupId);

                                // Prepare to email body
                                var emailBody = GetEmailBody(item);
                                var emailSubject = "AFE DIVE Variance Notification " + item.WELL_COMMON_NAME;

                                // send email
                                SendEmail(groupDetails, emailBody, emailSubject);
                            }
                        }
                    }

                }

                // Getting last record of variance
                var lastRecord = variances.Last();

                // Update settings table with last variance details
                _drillingRepository.UpdateLastRefreshDate(lastRecord.API10, lastRecord.ROW_CREATE_DATE);
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "VarianceNotifications", "Run" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }
            return "Done";
        }

        private static string GetAccessToken()
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var configuration = _appSettings.Value;

                // Constructing URL for getting Azure AD token
                var requestBody = $"client_id=" + configuration.ApplicationId +
                                  $"&scope=https://graph.microsoft.com/.default" +
                                  $"&client_secret=" + configuration.ClientSecret +
                                  $"&grant_type=client_credentials";

                // Making a http service call
                using (var response = httpClient.PostAsync("https://login.microsoftonline.com/" + configuration.TenantId + "/oauth2/v2.0/token",
                    new StringContent(requestBody, Encoding.UTF8, "application/x-www-form-urlencoded")).Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                        var result = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                        // Reading token for the parsed jason
                        return result.Value<string>("access_token");
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }

        }

        private static async Task<ADGroup> GetGroupMembersAsync(string accessToken, string groupId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                httpClient.DefaultRequestHeaders.Add("Authorization", accessToken);

                // Making a http service call
                using (var response = await httpClient.GetAsync("https://graph.microsoft.com/v1.0/groups/" + groupId + "/members"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        //var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                        var result = JsonConvert.DeserializeObject<ADGroup>(response.Content.ReadAsStringAsync().Result);

                        // Reading token for the parsed jason
                        return result;
                    }
                    else
                    {
                        return null;
                    }
                }
            }

        }

        private void SendEmail(ADGroup aDGroup, string mailBody, string subject)
        {
            var configuration = _appSettings.Value;
            string fromEmail = configuration.FromEmailAddress;
            var password = configuration.SMTP_Password;
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = configuration.SMTP_Host;
            client.Credentials = new System.Net.NetworkCredential(fromEmail, password);
            mail.Subject = subject;
            mail.From = new MailAddress(fromEmail);
            mail.Body = mailBody;
            mail.IsBodyHtml = true;

            foreach (var item in aDGroup.Members)
            {
                mail.To.Add(new MailAddress(item.mail, item.displayName));
            }

            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "VarianceNotifications", "Send Email" } }))
                {
                    _logger.LogError(ex.Message);
                }
            }
        }

        private static string GetEmailBody(VarianceDTO varianceDTO)
        {
            string html = string.Empty;
            var configuration = _appSettings.Value;

            html = html + "<p>This notification is to inform you that a Variance was identified on well " + varianceDTO.WELL_COMMON_NAME + "</p>";
            html = html + "<table>" +
                    "<tr>" +
                        "<td>Area:</td>" +
                        "<td>" + varianceDTO.BU + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Well:</td>" +
                        "<td>" + varianceDTO.WELL_COMMON_NAME + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Date:</td>" +
                        "<td>" + varianceDTO.ROW_CREATE_DATE + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Function:</td>" +
                        "<td>Area:</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Phase:</td>" +
                        "<td>" + varianceDTO.ACTIVITY_PHASE + "</td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Operation:</td>" +
                        "<td></td>" +
                    "</tr>" +
                    "<tr>" +
                        "<td>Url:</td>" +
                        "<td><a  href=\"" + configuration.Client_Url + "/dashboard/" + varianceDTO.API10 + " \"> " + varianceDTO.WELL_COMMON_NAME + "</a> </td>" +
                    "</tr>" +
                "</table>";

            html = html + "<p><b>Thank you</b></p>";
            html = html + "<hr/>";
            // html = html + "<img src=\"" + configuration.Client_Url + "assets/images/bpx-email-logo.png" + "\">";
            return html;
        }
    }
}