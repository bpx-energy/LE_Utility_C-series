﻿CREATE PROCEDURE [AFEDive].[GET_DRILL_DAILY_COSTS_FOR_OFFSET_WELLS]
	@API10 VARCHAR(10) 
AS
	SELECT DTC.[ACTIVITY_PHASE] 
			,DTC.[DATE_YMD] 
			,DTC.[MAX_DEPTH]
			,DTC.[TOTAL_COST] 
			,DTC.[CUM_PHASE_COST]
			,DTC.[CUM_WELL_COST]
FROM [AFEDive].[DRILL_BEST_OFFSET] DBF
LEFT JOIN [AFEDive].[DRILL_WELL_OFFSET] DWO ON DWO.OFFSET_ID = DBF.OFFSET_ID
LEFT JOIN [AFEDive].[DRILL_DAILY_COST] DTC ON (DTC.API10 = DBF.BEST_API10 AND DTC.ACTIVITY_PHASE = DBF.ACTIVITY_PHASE)
WHERE DWO.API10 = @API10 AND DBF.BEST_TYPE = 'COST'
RETURN 0
