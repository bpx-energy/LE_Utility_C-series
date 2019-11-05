using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AFEDIVE.DataAccess.Models;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AFEDive.API.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DrillingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        IDrillingRepository _drillingRepository;
        IEventRepository _eventRepository;

        public DrillingController(IDrillingRepository drillingRepository, IEventRepository eventRepository, IMapper mapper, ILogger<DrillingController> logger)
        {
            //  _configuration = configuration;
            _drillingRepository = drillingRepository;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/Drilling
        [HttpGet]
        public IEnumerable<string> Get()
        {
           

            return new string[] { "value1", "value2" };
        }

        // GET: api/Drilling/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }


        // GET: api/Drilling/5    
        [HttpGet]
        [Route("getwells")]
        public async Task<IEnumerable<Well>> GetWells()
        {
            var wells = new List<Well>();
            try
            {
                wells = _mapper.Map<List<WellDTO>, List<Well>>(await _drillingRepository.GetWells());
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Drilling", "Get Wells" } }))
                {
                    _logger.LogError(ex.Message);
                }

           //     throw ex;
            }
            return wells;
        }



        // POST: api/Drilling
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Drilling/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("GetCostVsDepth/{wellName}")]
        public async Task<CostVsDepthChart> GetCostVsDepth(string wellName)
        {
            // Preparing chart object
            var costvsDepthChart = new CostVsDepthChart();
            try
            {
                var well = await _drillingRepository.GetWellByName(wellName);
                // Getting data for AFE drill data
                costvsDepthChart.AFEs = _mapper.Map<List<DrillAFEDTO>, List<DrillAFE>>(await _drillingRepository.GetDrillAFEs(well.API10));

                costvsDepthChart.DailyCosts = _mapper.Map<List<DrillDailyCostDTO>, List<DrillDailyCost>>(await _drillingRepository.GetDrillDailyCosts(well.API10));

                costvsDepthChart.DrillVarianceCosts = _mapper.Map<List<DrillVarianceCostDTO>, List<DrillVarianceCost>>(await _drillingRepository.GetDrillVarianceCost(well.API10));

                // Getting data for AFE drill data for offset wells
                costvsDepthChart.DailyCostsForOffsetWells = _mapper.Map<List<DrillDailyCostDTO>, List<DrillDailyCost>>(await _drillingRepository.GetDrillDailyCostsForOffsetWells(well.API10));

                // Getting data for AFE drill mean data  cost offset wells
                costvsDepthChart.DailyMeanCosts = _mapper.Map<List<DrillMeanDailyCostDTO>, List<DrillMeanDailyCost>>(await _drillingRepository.GetDrillMeanDailyCosts(well.API10));
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Drilling", "GetCostVsDepth" } }))
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            return costvsDepthChart;
        }

        [HttpGet]
        [Route("GetTimeVsDepth/{wellName}")]
        public async Task<TimeVsDepthChart> GetTimeVsDepth(string wellName)
        {
            // Preparing chart object
            var timevsDepthChart = new TimeVsDepthChart();
            try
            {
                var well = await _drillingRepository.GetWellByName(wellName);
                // Getting drill data 
                timevsDepthChart.AFEs = _mapper.Map<List<DrillAFEDTO>, List<DrillAFE>>(await _drillingRepository.GetDrillAFEs(well.API10));

                // Getting Drill time summary data
                timevsDepthChart.DrillTimeSummaries = _mapper.Map<List<DrillTimeSummaryDTO>, List<DrillTimeSummary>>(await _drillingRepository.GetDrillTimeSummary(well.API10));

                // Getting Drill time Variances
                timevsDepthChart.DrillVarianceDurations = _mapper.Map<List<DrillVarianceDurationDTO>, List<DrillVarianceDuration>>(await _drillingRepository.GetDrillVarianceDuration(well.API10));

                // Getting Drill time summary data for offset wells
                timevsDepthChart.DrillTimeSummariesOffsetWell = _mapper.Map<List<DrillTimeSummaryDTO>, List<DrillTimeSummary>>(await _drillingRepository.GetDrillTimeSummaryForOffsetWells(well.API10));

                // Getting Drill mean time summary data for offset wells
                timevsDepthChart.DrillMeanTimeSummaries = _mapper.Map<List<DrillMeanTimeSummaryDTO>, List<DrillMeanTimeSummary>>(await _drillingRepository.GetDrillTimeMeanSummaryForOffsetWells(well.API10));
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Drilling", "GetTimeVsDepth" } }))
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            return timevsDepthChart;
        }

        




        [HttpGet]
        [Route("GetEventAndVariancesForWell/{wellName}/{variancetype}")]
        public async Task<IEnumerable<EventAndVariance>> GetEventAndVariancesForWell(string wellName, int variancetype)
        {
            var varianceandevents = new List<EventAndVariance>();
            try
            {
                var well = await _drillingRepository.GetWellByName(wellName);

                // Get variance types and events
                varianceandevents = _mapper.Map<List<EventAndVarianceDTO>, List<EventAndVariance>>(await _eventRepository.GetEventAndVariancesForWell(well.API10, variancetype));

            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Drilling", "GetEventAndVariancesForWell" } }))
                {
                    _logger.LogError(ex.StackTrace);
                }
            }
            return varianceandevents;
        }

    }
}
