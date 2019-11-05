﻿CREATE PROCEDURE [AFEDive].[GET_LIST_ASSOCIATION]
	
AS
	SELECT  [LIST_ASSOCIATION_ID]
      ,[DESCRIPTION]
      ,[TYPE]
      ,[PARENT_ID]
      ,[IS_DELETED]
	  ,[ORDER]
  FROM [AFEDive].[LIST_ASSOCIATION]

RETURN 0
GO