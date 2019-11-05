CREATE PROCEDURE [AFEDive].[Get_DRILL_VARIANCE_BY_DATE]
AS
	SELECT  variance.[API10]
	, well.BU
	,well.WELL_COMMON_NAME
	,variance.[ROW_CREATE_DATE]
	,[ACTIVITY_PHASE]   
	  
  FROM [AFEDive].[DRILL_VARIANCE_DURATION]as variance
  LEFT JOIN [AFEDive].[WELL_HEADER] well ON well.API10 = variance.API10
  WHERE  
  variance.ROW_CREATE_DATE > (SELECT [Value] FROM [AFEDive].[SETTINGS] WHERE [Name] = 'LastReadTime') 
  AND variance.API10 <> (SELECT [Value] FROM [AFEDive].[SETTINGS] WHERE [Name] = 'LastReadAPI10')

RETURN 0