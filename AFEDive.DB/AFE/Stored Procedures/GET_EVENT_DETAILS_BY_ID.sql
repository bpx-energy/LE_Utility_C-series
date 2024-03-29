CREATE PROCEDURE [AFEDive].[GET_EVENT_DETAILS_BY_ID]
	@eventId int

AS


  /* Get Event Information */
SELECT E.[EVENT_ID]
      ,E.[VARIANCE_DURATION_ID]
      ,E.[VARIANCE_COST_ID]
      ,E.[TITLE]
      ,E.[LEVEL1]
	  ,L.Description as LEVEL1_TEXT
      ,E.[LEVEL2]
	  ,L2.Description as LEVEL2_TEXT
      ,E.[RESPONSIBLE_PARTY]
	  ,L3.Description as RESPONSIBLE_PARTY_TEXT
      ,E.[VARIANCE_TYPE]
      ,E.[ROW_CREATE_DATE]
      ,E.[ROW_CREATE_ID]
      ,E.[ROW_UPDATE_DATE]
      ,E.[ROW_UPDATE_ID]
      ,E.[IS_DELETED]
	  ,E.EVENT_STATUS AS EventStatus
  FROM [AFEDive].[EVENT] E
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L on 
  E.LEVEL1=L.[LIST_ASSOCIATION_ID]
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L2 on 
  E.LEVEL2=L2.[LIST_ASSOCIATION_ID]
  Left JOIN
  [AFEDive].[LIST_ASSOCIATION] L3 on 
  E.RESPONSIBLE_PARTY=L3.[LIST_ASSOCIATION_ID]

  where EVENT_ID=@eventId and E.IS_DELETED=0
GO