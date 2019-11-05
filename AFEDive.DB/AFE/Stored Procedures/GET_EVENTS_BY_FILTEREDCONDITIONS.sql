CREATE PROCEDURE [AFEDive].[GET_EVENTS_BY_FILTEREDCONDITIONS]
	(
	@api10 varchar(10), 
	@area varchar(25),
	@function varchar(50),
	@phase varchar(50),
	@operation varchar(50),
	@variancelevel1 varchar(50),
	@variancelevel2 varchar(50)
	)
AS

SELECT E.[EVENT_ID]
      ,WH.WELL_COMMON_NAME
      ,E.[TITLE]
	  ,E.[ROW_CREATE_DATE]
      ,E.[LEVEL1]
	  ,L.Description as LEVEL1_TEXT
      ,E.[LEVEL2]
	  ,L2.Description as LEVEL2_TEXT
      ,E.VARIANCE_COST_ID
      ,E.VARIANCE_DURATION_ID
	  ,E.EVENT_STATUS as EventStatus
  FROM [AFEDive].[EVENT] E
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L on 
  E.LEVEL1=L.[LIST_ASSOCIATION_ID]
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L2 on 
  E.LEVEL2=L2.[LIST_ASSOCIATION_ID]
  Inner Join 
  [AFEDive].[DRILL_VARIANCE_COST] VC
  on E.[VARIANCE_COST_ID]= VC.[VARIANCE_COST_ID]
  Left Join
  [AFEDive].[FUNCTION_PHASE] FP
  on FP.[ACTIVITY_PHASE]= VC.[ACTIVITY_PHASE]
  Inner Join
  [AFEDive].[WELL_HEADER] WH
  on VC.APi10=WH.API10
  where
  -- Check for well
  (@api10 is null or VC.APi10=@api10) and 
  -- Check for Area
  (@area is null or WH.BU=@area) and
  -- Check for Phase
   (@phase is null or VC.ACTIVITY_Phase=@phase) and
  -- Check for variance level
  (@variancelevel1 is null or E.LEVEL1=@variancelevel1) and
  (@variancelevel2 is null or E.LEVEL2=@variancelevel2) and
  -- check for function
  (@function is null or FP.[FUNCTION]=@function)
  and e.EVENT_STATUS<>0 and e.IS_DELETED=0
UNION
SELECT E.[EVENT_ID]
	  ,WH.WELL_COMMON_NAME
      ,E.[TITLE]
	  ,E.[ROW_CREATE_DATE]
      ,E.[LEVEL1]
      ,L.Description as LEVEL1_TEXT
      ,E.[LEVEL2]
	  ,L2.Description as LEVEL2_TEXT
      ,E.VARIANCE_COST_ID
      ,E.VARIANCE_DURATION_ID
	  ,E.EVENT_STATUS as EventStatus
  FROM [AFEDive].[EVENT] E
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L on 
  E.LEVEL1=L.[LIST_ASSOCIATION_ID]
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L2 on 
  E.LEVEL2=L2.[LIST_ASSOCIATION_ID]
  Inner Join 
  [AFEDive].[DRILL_VARIANCE_DURATION] VD
  on E.[VARIANCE_DURATION_ID]= VD.[VARIANCE_DURATION_ID]
  Left Join
  [AFEDive].[FUNCTION_PHASE] FP
  on FP.[ACTIVITY_PHASE]= VD.[ACTIVITY_PHASE]
  Inner Join
  [AFEDive].[WELL_HEADER] WH
  on VD.APi10=WH.API10
  where
  -- Check for well
  (@api10 is null or VD.APi10=@api10) and 
  -- Check for Area
  (@area is null or WH.BU=@area) and
  -- Check for Phase
   (@phase is null or VD.ACTIVITY_Phase=@phase) and
  -- Check for variance level
  (@variancelevel1 is null or E.LEVEL1=@variancelevel1) and
  (@variancelevel2 is null or E.LEVEL2=@variancelevel2) and
  -- check for function
  (@function is null or FP.[FUNCTION]=@function)
  and e.EVENT_STATUS<>0 and e.IS_DELETED=0
GO