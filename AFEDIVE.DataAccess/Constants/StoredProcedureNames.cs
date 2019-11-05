using System;
using System.Collections.Generic;
using System.Text;

namespace AFEDIVE.DataAccess.Constants
{
    public static class StoredProcedureNames
    {
        public const string EVENT_LIST = "[AFEDive].[EVENTS_GET_BY_VARIANT_ID]";
        public const string SAVE_EVENT = "[AFEDive].[SAVE_EVENT]";
        public const string SAVE_COMMENT = "[AFEDive].[SAVE_COMMENT]";
        public const string SAVE_ATTACHMENT = "[AFEDive].[SAVE_ATTACHMENT]";
        public const string GET_DRILL_AFE = "[AFEDive].[GET_DRILL_AFE_BY_API10]";
        public const string GET_DRILL_DAILY_COSTS = "[AFEDive].[GET_DRILL_DAILY_COSTS_BY_API10]";
        public const string GET_MEAN_DAILY_COST_FOR_OFFSET_WELLS = "[AFEDive].[GET_MEAN_DAILY_COST_FOR_OFFSET_WELLS]";
        public const string GET_DRILL_DAILY_COSTS_FOR_OFFSET_WELLS = "[AFEDive].[GET_DRILL_DAILY_COSTS_FOR_OFFSET_WELLS]";
        public const string GET_DRILL_TIME_SUMMARY = "[AFEDive].[GET_DRILL_TIME_SUMMARY_BY_API10]";
        public const string GET_DRILL_TIME_SUMMARY_FOR_OFFSET_WELLS = "[AFEDive].[GET_DRILL_TIME_SUMMARY_FOR_OFFSET_WELLS]";
        public const string GET_DRILL_MEAN_TIME_SUMMARY_FOR_OFFSET_WELLS = "[AFEDive].[GET_DRILL_MEAN_TIME_SUMMARY_FOR_OFFSET_WELLS]";
        public const string GET_WELLS = "[AFEDive].[GET_WELLS]";
        public const string GET_FUNCTION_PHASE = "[AFEDive].[GET_FUNCTION_PHASE]";
        public const string GET_WELL_BY_NAME = "[AFEDive].[GET_WELL_BY_NAME]";
        public const string GET_DRILL_VARIANCE_COST = "[AFEDive].[GET_DRILL_VARIANCE_COST_BY_API10]";
        public const string GET_DRILL_VARIANCE_DURATION = "[AFEDive].[GET_DRILL_VARIANCE_DURATION_BY_API10]";
        public const string Get_DRILL_VARIANCE_BY_DATE = "[AFEDive].[Get_DRILL_VARIANCE_BY_DATE]";
        public const string GET_LIST_ASSOCIATIONS = "[AFEDive].[GET_LIST_ASSOCIATION]";
        public const string GET_EVENT_DETAILS = "[AFEDive].[GET_EVENT_DETAILS_BY_ID]";
        public const string GET_COMMENTS_BY_EVENT_ID = "[AFEDive].[GET_COMMENTS_BY_EVENT_ID]";
        public const string GET_ATTACHMENTS_BY_EVENT_ID = "[AFEDive].[GET_ATTACHMENTS_BY_EVENT_ID]";
        public const string DELETE_EVENT_BY_EVENT_ID = "[AFEDive].[DELETE_EVENT_BY_EVENT_ID]";
        public const string GET_VARIANCE_AND_EVENT_BY_API10 = "[AFEDive].[GET_VARIANCE_AND_EVENT_BY_API10]";
        public const string DELETE_ATTACHMENT_BY_ATTACHMENT_ID = "[AFEDive].[DELETE_ATTACHMENT_BY_ATTACHMENT_ID]";
        public const string DELETE_COMMENT_BY_COMMENT_ID = "[AFEDive].[DELETE_COMMENT_BY_COMMENT_ID]";
        public const string GET_EVENTS_BY_FILTEREDCONDITIONS = "[AFEDive].[GET_EVENTS_BY_FILTEREDCONDITIONS]";
        public const string GET_STATE = "[AFEDive].[GET_STATE]";
        public const string SAVE_STATE = "[AFEDive].[SAVE_STATE]";

        public const string GET_PERMISSIONS_FOR_USER = "[AFEDive].[GET_PERMISSIONS_FOR_USER]";
        public const string GET_USER_PERMISSIONS_BY_BU = "[AFEDive].[GET_USER_PERMISSIONS]";
        public const string UPDATE_SETTINGS = "[AFEDive].[UPDATE_SETTINGS]";
    }
}
