﻿CREATE PROCEDURE [AFEDive].[GET_DRILL_TIME_SUMMARY_BY_API10]
	@api10 varchar(10)
	
AS
	SELECT [API10]
      ,MAX([DEPTH]) as DEPTH
      ,CONVERT(DateTime,CONVERT(VARCHAR(10), TIME_FROM, 111)) as TIME_FROM
      ,(MAX([CUM_WELL_DURATION]))/24 as CUM_WELL_DURATION
   
  FROM [AFEDive].[DRILL_TIME_SUMMARY]
  where API10=@api10
  Group By [API10],CONVERT(DateTime,CONVERT(VARCHAR(10), TIME_FROM, 111))


