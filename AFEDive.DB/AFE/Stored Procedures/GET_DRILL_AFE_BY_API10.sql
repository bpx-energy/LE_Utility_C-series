CREATE PROCEDURE [AFEDive].[GET_DRILL_AFE_BY_API10]
	@api10 varchar(10) = 0
	
AS
	SELECT [API10] ,
		   [ACTIVITY_PHASE],
		   [SEQUENCE_NO], 
		   [DEPTH] , 
		   [TARGET_COST] , 
		   [TARGET_DURATION], 
		   CAST([CUM_WELL_DURATION] as float)/24 as CUM_WELL_DURATION,
		   [CUM_WELL_COST], 
		   [HOLE_SECT_GROUP_ID]
      from [AFEDive].[DRILL_AFE]
	  where API10=@api10

	

