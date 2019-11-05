using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AFEDIVE.DataAccess.Constants;
using System.Data;
using AFEDIVE.DataAccess.Models;

namespace AFEDIVE.DataAccess.Repositories
{
    public class DrillingRepository : DapperDb, IDrillingRepository
    {
        public DrillingRepository(string connectionString)
           : base(connectionString)
        {
        }

        //TODO: Need to refator the code to use Automapper


        /// <summary>
        /// Get Drill AFEs for well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillAFEDTO>> GetDrillAFEs(string api10)
        {
            using (var connection = CreateConnection())
            {
                // Feteching data from database
                var drillsAFEs = await connection.QueryAsync<DrillAFEDTO>(StoredProcedureNames.GET_DRILL_AFE, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                return drillsAFEs.AsList<DrillAFEDTO>();

                
            }
        }

        /// <summary>
        /// Get Drill Daily Cost for well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillDailyCostDTO>> GetDrillDailyCosts(string api10)
        {
            using (var connection = CreateConnection())
            {            
                var drillDailyCost= await connection.QueryAsync<DrillDailyCostDTO>(StoredProcedureNames.GET_DRILL_DAILY_COSTS,
                    new { api10 = api10 }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return drillDailyCost.AsList<DrillDailyCostDTO>();
            }
        }

        /// <summary>
        /// Get Drill Mean Daily Cost for well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillMeanDailyCostDTO>> GetDrillMeanDailyCosts(string api10)
        {
            using (var connection = CreateConnection())
            {
                var drillMeanDailyCost = await connection.QueryAsync<DrillMeanDailyCostDTO>(StoredProcedureNames.GET_MEAN_DAILY_COST_FOR_OFFSET_WELLS,
                    new { api10 = api10 }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return drillMeanDailyCost.AsList<DrillMeanDailyCostDTO>();
            }
        }


        public async Task<List<DrillDailyCostDTO>> GetDrillDailyCostsForOffsetWells(string api10)
        {
            using (var connection = CreateConnection())
            {
                var drillDailyCost = await connection.QueryAsync<DrillDailyCostDTO>(StoredProcedureNames.GET_DRILL_DAILY_COSTS_FOR_OFFSET_WELLS,
                    new { api10 = api10 }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return drillDailyCost.AsList<DrillDailyCostDTO>();
            }
        }


        /// <summary>
        /// Get Drill Time Summary for well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillTimeSummaryDTO>> GetDrillTimeSummary(string api10)
        {
            using (var connection = CreateConnection())
            {
                // Geting data for drill time summary from database using Store proc 
                var drills = await connection.QueryAsync<DrillTimeSummaryDTO>(StoredProcedureNames.GET_DRILL_TIME_SUMMARY, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                return drills.AsList<DrillTimeSummaryDTO>();

            }
        }
        

        /// <summary>
        /// Get Drill Time Summary for a off set well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillTimeSummaryDTO>> GetDrillTimeSummaryForOffsetWells(string api10)
        {
            using (var connection = CreateConnection())
            {
                // Geting data for drill time summary from database using Store proc 
                var drills = await connection.QueryAsync<DrillTimeSummaryDTO>(StoredProcedureNames.GET_DRILL_TIME_SUMMARY_FOR_OFFSET_WELLS, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                return drills.AsList<DrillTimeSummaryDTO>();

            }
        }

        /// <summary>
        /// Get Drill Time Summary for a off set well
        /// </summary>
        /// <param name="api10"></param>
        /// <returns></returns>
        public async Task<List<DrillMeanTimeSummaryDTO>> GetDrillTimeMeanSummaryForOffsetWells(string api10)
        {
            using (var connection = CreateConnection())
            {
                // Geting data for drill time summary from database using Store proc 
                var drills = await connection.QueryAsync<DrillMeanTimeSummaryDTO>(StoredProcedureNames.GET_DRILL_MEAN_TIME_SUMMARY_FOR_OFFSET_WELLS, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                return drills.AsList<DrillMeanTimeSummaryDTO>();

            }
        }

        /// <summary>
        /// Get All Wells
        /// </summary>
        /// <returns></returns>
        public async Task<List<WellDTO>> GetWells()
        {
            using (var connection = CreateConnection())
            {
                var wells = new List<WellDTO>();
                try
                {
                    // Geting data for Wells from database using Store proc 
                    var wellsEntities = await connection.QueryAsync<WellDTO>(StoredProcedureNames.GET_WELLS, null, commandType: CommandType.StoredProcedure);
                    wells= wellsEntities.AsList<WellDTO>();
                }
                catch(Exception ex)
                {
                    throw ex;

                }
                return wells;

            }
        }

        /// <summary>
        /// Get Function and Phase Entities
        /// </summary>
        /// <returns></returns>
        public async Task<List<FunctionAndPhaseDTO>> GetFunctionAndPhases()
        {
            using (var connection = CreateConnection())
            {
                var functionAndPhase = new List<FunctionAndPhaseDTO>();
                try
                {
                    // Geting data for Wells from database using Store proc 
                    var functionAndPhaseEntities = await connection.QueryAsync<FunctionAndPhaseDTO>(StoredProcedureNames.GET_FUNCTION_PHASE, null, commandType: CommandType.StoredProcedure);
                    functionAndPhase = functionAndPhaseEntities.AsList<FunctionAndPhaseDTO>();
                }
                catch (Exception ex)
                {

                }
                return functionAndPhase;

            }
        }

        /// <summary>
        /// All Wells
        /// </summary>
        /// <returns></returns>
        public async Task<WellDTO> GetWellByName(string wellName)
        {
            using (var connection = CreateConnection())
            {
                var well = new WellDTO();
                try
                {
                    // Geting data for Wells from database using Store proc 
                     well = await connection.QuerySingleAsync<WellDTO>(StoredProcedureNames.GET_WELL_BY_NAME, new { WELL_NAME = wellName },null,null, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                     
                }
                catch (Exception ex)
                {

                }
                return well;

            }
        }


        /// <summary>
        /// Get Drill Variance Cost for Well
        /// </summary>
        /// <returns></returns>
        public async Task<List<DrillVarianceCostDTO>> GetDrillVarianceCost(string api10)
        {
            using (var connection = CreateConnection())
            {
                // Geting data for Wells from database using Store proc 
                
                var drillvarianceCost = await connection.QueryAsync<DrillVarianceCostDTO>(StoredProcedureNames.GET_DRILL_VARIANCE_COST, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                return drillvarianceCost.AsList<DrillVarianceCostDTO>();

            }
        }


        /// <summary>
        /// Get Drill Variance Duration for Well
        /// </summary>
        /// <returns></returns>
        public async Task<List<DrillVarianceDurationDTO>> GetDrillVarianceDuration(string api10)
        {

            var drillvarianceDurations = new List<DrillVarianceDurationDTO>();
            using (var connection = CreateConnection())
            {
                try
                {
                    // Geting data for Wells from database using Store proc 
                    // Geting data for Wells from database using Store proc 
                    
                    var drillvarianceDuration = await connection.QueryAsync<DrillVarianceDurationDTO>(StoredProcedureNames.GET_DRILL_VARIANCE_DURATION, new { api10 = api10 }, commandType: CommandType.StoredProcedure);
                    drillvarianceDurations = drillvarianceDuration.AsList<DrillVarianceDurationDTO>();
                }
                catch
                (Exception ex)
                {

                }

            }
            return drillvarianceDurations;
        }

        public async Task<List<VarianceDTO>> GetDrillVarianceDurationByDate()
        {

            var drillvarianceDurations = new List<VarianceDTO>();
            using (var connection = CreateConnection())
            {
                try
                {

                    var drillvarianceDuration = await connection.QueryAsync<VarianceDTO>(StoredProcedureNames.Get_DRILL_VARIANCE_BY_DATE, null, commandType: CommandType.StoredProcedure);
                    drillvarianceDurations = drillvarianceDuration.AsList<VarianceDTO>();
                }
                catch (Exception ex)
                {

                }

            }
            return drillvarianceDurations;
        }

        public async void UpdateLastRefreshDate(string API10, DateTimeOffset createdDate)
        {

            using (var connection = CreateConnection())
            {
                try
                {
                    var updateSettings = await connection.ExecuteAsync(StoredProcedureNames.UPDATE_SETTINGS, new { createdDate = createdDate, api10 = API10 }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                }
                catch (Exception ex)
                {

                }

            }
        }

    }
}
