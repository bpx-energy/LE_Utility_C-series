﻿Create Proc [AFEDive].[GET_FUNCTION_PHASE]
as 
 Select	[FUNCTION]
      ,[ACTIVITY_PHASE]
      ,[DISPLAY_ORDER]
      ,[ROW_CREATE_DATE]
      ,[ROW_CREATE_ID]
      ,[ROW_UPDATE_DATE]
      ,[ROW_UPDATE_ID]
  FROM [AFEDive].[FUNCTION_PHASE]
  Return 0