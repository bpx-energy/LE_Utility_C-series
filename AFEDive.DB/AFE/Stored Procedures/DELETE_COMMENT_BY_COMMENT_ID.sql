CREATE PROCEDURE [AFEDive].[DELETE_COMMENT_BY_COMMENT_ID]
	@commentId uniqueidentifier

AS

Update  [AFEDive].[COMMENT]
  SET IS_DELETED=1
  where COMMENT_ID=@commentId
GO