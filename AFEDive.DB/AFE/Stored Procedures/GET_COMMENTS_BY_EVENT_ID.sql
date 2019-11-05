CREATE PROCEDURE [AFEDive].[GET_COMMENTS_BY_EVENT_ID]
	@eventId int

AS

 SELECT [COMMENT_ID]
      ,[COMMENT]
      ,[EVENT_ID]
      ,[ROW_CREATE_DATE]
      ,[ROW_CREATE_ID]
      ,[ROW_UPDATE_DATE]
      ,[ROW_UPDATE_ID]
      ,[IS_DELETED]
  FROM [AFEDive].[COMMENT] 
  where EVENT_ID=@eventId and IS_DELETED=0 order by ROW_CREATE_DATE desc
  GO


