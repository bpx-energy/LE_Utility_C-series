Create PROCEDURE [AFEDive].[DELETE_EVENT_BY_EVENT_ID]
	@eventId int

AS

Update  [AFEDive].[EVENT]
  SET IS_DELETED=1
  where EVENT_ID=@eventId
GO
