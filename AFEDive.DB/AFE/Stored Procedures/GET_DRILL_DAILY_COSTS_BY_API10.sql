﻿CREATE PROCEDURE [AFEDive].[GET_DRILL_DAILY_COSTS_BY_API10]
	@api10 varchar(10)
AS
	Select	[ACTIVITY_PHASE] 
			[DATE_YMD], 
			[MAX_DEPTH], 
			[TOTAL_COST], 
			[CUM_PHASE_COST],
			[CUM_WELL_COST]
	
		   From [AFEDive].[DRILL_DAILY_COST]
		   where API10=@api10

	

