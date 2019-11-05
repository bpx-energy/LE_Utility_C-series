using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AFEDive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]

    //[EnableCors("AllowOrigin")]
    public class UserController : ControllerBase
    {

        private readonly ILogger _logger;

        private readonly IMapper _mapper;
        IDrillingRepository _drillingRepository;
        IUserRepository _userRepository;
        private static IOptions<AppSettings> _appSettings;

        public UserController(IDrillingRepository drillingRepository, IUserRepository userRepository, IMapper mapper,
            ILogger<UserController> logger,
            IOptions<AppSettings> options)
        {
            //  _configuration = configuration;
            _drillingRepository = drillingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _appSettings = options;
        }

        [HttpPost]
        [Route("GeneratePowerBIEmbedToken")]
        public async Task<ReportModel> GetReportData([FromBody] JObject bitoken)
        {
            var reportModel = new ReportModel();
            try
            {
                if (bitoken.ContainsKey("bitoken"))
                {
                    reportModel = await EmbedReport(bitoken["bitoken"].ToString());
                }
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "User", "GeneratePowerBIEmbedToken" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }



            return reportModel;
        }

        [HttpPost]
        [Route("GetUserPermissions/{wellName}")]
        public async Task<string> GetUserPermissions(string wellName, List<string> value)
        {
            try
            {
                // converting list to comma seprated to perform in operation
                var groupsString = string.Join(",", value);

                // Get Well Details
                var well = await _drillingRepository.GetWellByName(wellName);

                // get user permissions
                var permission = await _userRepository.GetUserPermission(well.API10, groupsString);


                var response = new { Permission = permission };
                return JsonConvert.SerializeObject(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static async Task<ReportModel> EmbedReport(string AccessToken)
        {
            try
            {
                var configuration = _appSettings.Value;

                // Making call to power bi client 
                // With Acuired AD Token
                using (var client = new PowerBIClient(new Uri(configuration.ApiUrl), new TokenCredentials(AccessToken, "Bearer")))
                {
                    // Get report by report id
                    var report = await client.Reports.GetReportAsync(configuration.ReportId);

                    // Building report model for UI
                    var reportModel = new ReportModel();
                    reportModel.EmbedUrl = report.EmbedUrl;
                    reportModel.ReportId = report.Id;
                    reportModel.AccessToken = AccessToken;

                    return reportModel;
                }
            }
            catch (Exception ex)
            {
                // TODO enable logging
                return null;
            }
        }

    }
}