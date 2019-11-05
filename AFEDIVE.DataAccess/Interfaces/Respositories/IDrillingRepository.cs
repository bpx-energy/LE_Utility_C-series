using AFEDIVE.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFEDIVE.DataAccess.Interfaces.Respositories
{
    public interface IDrillingRepository
    {
        Task<List<DrillAFEDTO>> GetDrillAFEs(string api10);

        Task<List<DrillDailyCostDTO>> GetDrillDailyCosts(string api10);

        Task<List<DrillMeanDailyCostDTO>> GetDrillMeanDailyCosts(string api10);

        Task<List<DrillDailyCostDTO>> GetDrillDailyCostsForOffsetWells(string api10);

        Task<List<DrillTimeSummaryDTO>> GetDrillTimeSummary(string api10);

        Task<List<DrillTimeSummaryDTO>> GetDrillTimeSummaryForOffsetWells(string api10);

        Task<List<DrillMeanTimeSummaryDTO>> GetDrillTimeMeanSummaryForOffsetWells(string api10);

        Task<List<WellDTO>> GetWells();

        Task<List<FunctionAndPhaseDTO>> GetFunctionAndPhases();

        Task<WellDTO> GetWellByName(string wellName);

        Task<List<DrillVarianceDurationDTO>> GetDrillVarianceDuration(string api10);

        Task<List<DrillVarianceCostDTO>> GetDrillVarianceCost(string api10);

        Task<List<VarianceDTO>> GetDrillVarianceDurationByDate();

        void UpdateLastRefreshDate(string API10, DateTimeOffset createdDate);
    }
}
