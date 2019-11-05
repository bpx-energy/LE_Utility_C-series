﻿CREATE PROCEDURE [AFEDive].[DELETE_ATTACHMENT_BY_ATTACHMENT_ID]
	@attachmentId uniqueidentifier

AS

Update  [AFEDive].[ATTACHMENT]
  SET IS_DELETED=1
  where ATTACHMENT_ID=@attachmentId
GO