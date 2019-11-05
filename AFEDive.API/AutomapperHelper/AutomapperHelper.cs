using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AFEDive.API.AutomapperHelper
{
    public class AutomapperHelper : Profile
    {
        public AutomapperHelper()
        {
            // Mapping for well object
            CreateMap<WellDTO, Well>()
                    .ForMember(destination => destination.Api10, opts => opts.MapFrom(source => source.API10))
                    .ForMember(destination => destination.WellCommonName, opts => opts.MapFrom(source => source.Well_Common_Name))
                     .ForMember(destination => destination.Area, opts => opts.MapFrom(source => source.BU))
                     .ForMember(destination => destination.Active, opts => opts.MapFrom(source => source.Active));


            // Mapping for Drill AFE
            CreateMap<DrillAFEDTO, DrillAFE>()
               .ForMember(destination => destination.Api10, opts => opts.MapFrom(source => source.API10))
               .ForMember(destination => destination.ActivityPhase, opts => opts.MapFrom(source => source.ACTIVITY_PHASE))
               .ForMember(destination => destination.DateYmd, opts => opts.MapFrom(source => source.DATE_YMD))
               .ForMember(destination => destination.Depth, opts => opts.MapFrom(source => source.DEPTH))
               .ForMember(destination => destination.CumWellDuration, opts => opts.MapFrom(source => source.CUM_Well_DURATION))
               .ForMember(destination => destination.CumWellCost, opts => opts.MapFrom(source => source.CUM_WELL_COST));

            // Mapping for Drill time summary
            CreateMap<DrillTimeSummaryDTO, DrillTimeSummary>()
              .ForMember(destination => destination.ActivityId, opts => opts.MapFrom(source => source.ACTIVITY_ID))
              .ForMember(destination => destination.ActivityPhase, opts => opts.MapFrom(source => source.ACTIVITY_PHASE))
              .ForMember(destination => destination.Depth, opts => opts.MapFrom(source => source.DEPTH))
              .ForMember(destination => destination.TimeFrom, opts => opts.MapFrom(source => source.TIME_FROM))
              .ForMember(destination => destination.Date_Ymd, opts => opts.MapFrom(source => source.DATE_YMD))
              .ForMember(destination => destination.ActivityDuration, opts => opts.MapFrom(source => source.ACTIVITY_DURATION))
              .ForMember(destination => destination.CumPhaseDuration, opts => opts.MapFrom(source => source.CUM_PHASE_DURATION))
              .ForMember(destination => destination.CumWellDuration, opts => opts.MapFrom(source => source.CUM_WELL_DURATION));

            // Mapping for drill mean time for summaries
            CreateMap<DrillMeanTimeSummaryDTO, DrillMeanTimeSummary>()
                .ForMember(destination => destination.Depth, opts => opts.MapFrom(source => source.DEPTH))
                .ForMember(destination => destination.CumWellDuration, opts => opts.MapFrom(source => source.CUM_WELL_DURATION));

            // Mapping for Drill Variance Duration
            CreateMap<DrillVarianceDurationDTO, DrillVarianceDuration>()
              .ForMember(destination => destination.Api10, opts => opts.MapFrom(source => source.API10))
              .ForMember(destination => destination.ActivityPhase, opts => opts.MapFrom(source => source.ACTIVITY_PHASE))
              .ForMember(destination => destination.Depth, opts => opts.MapFrom(source => source.DEPTH))
              .ForMember(destination => destination.CumPhaseDuration, opts => opts.MapFrom(source => source.CUM_PHASE_DURATION))
              .ForMember(destination => destination.AfeCumPhaseDuration, opts => opts.MapFrom(source => source.AFE_CUM_PHASE_DURATION))
              .ForMember(destination => destination.CumWellDuration, opts => opts.MapFrom(source => source.CUM_WELL_DURATION))
              .ForMember(destination => destination.ActivityId, opts => opts.MapFrom(source => source.ACTIVITY_ID))
              .ForMember(destination => destination.VarianceAmount, opts => opts.MapFrom(source => source.VARIANCE_AMOUNT))
              .ForMember(destination => destination.RecordedDate, opts => opts.MapFrom(source => source.ROW_CREATE_DATE))
              .ForMember(destination => destination.VarianceType, opts => opts.MapFrom(source => source.VARIANCE_TYPE))
              .ForMember(destination => destination.VarianceComment, opts => opts.MapFrom(source => source.VARIANCE_COMMENT))
              .ForMember(destination => destination.VarianceDurationId, opts => opts.MapFrom(source => source.VARIANCE_DURATION_ID));

            // Mapping of Drill Daily Cost
            CreateMap<DrillDailyCostDTO, DrillDailyCost>()
              .ForMember(destination => destination.Api10, opts => opts.MapFrom(source => source.API10))
              .ForMember(destination => destination.ActivityPhase, opts => opts.MapFrom(source => source.ACTIVITY_PHASE))
              .ForMember(destination => destination.DateYmd, opts => opts.MapFrom(source => source.DATE_YMD))
              .ForMember(destination => destination.MaxDepth, opts => opts.MapFrom(source => source.MAX_DEPTH))
              .ForMember(destination => destination.CumPhaseCost, opts => opts.MapFrom(source => source.CUM_PHASE_COST))
              .ForMember(destination => destination.CumWellCost, opts => opts.MapFrom(source => source.CUM_WELL_COST));

            // Mapping for Drill mean daily cost
            CreateMap<DrillMeanDailyCostDTO, DrillMeanDailyCost>()
                  .ForMember(destination => destination.MaxDepth, opts => opts.MapFrom(source => source.DEPTH))
                  .ForMember(destination => destination.CumWellCost, opts => opts.MapFrom(source => source.CUM_WELL_COST));

            // Mapping for Drill Variance Cost
            CreateMap<DrillVarianceCostDTO, DrillVarianceCost>()
              .ForMember(destination => destination.Api10, opts => opts.MapFrom(source => source.API10))
              .ForMember(destination => destination.ActivityPhase, opts => opts.MapFrom(source => source.ACTIVITY_PHASE))
              .ForMember(destination => destination.DateYmd, opts => opts.MapFrom(source => source.DATE_YMD))
              .ForMember(destination => destination.MaxDepth, opts => opts.MapFrom(source => source.MAX_DEPTH))
              .ForMember(destination => destination.CumPhaseCost, opts => opts.MapFrom(source => source.CUM_PHASE_COST))
              .ForMember(destination => destination.AfeCumPhaseCost, opts => opts.MapFrom(source => source.AFE_CUM_PHASE_COST))
              .ForMember(destination => destination.CumWellCost, opts => opts.MapFrom(source => source.CUM_WELL_COST))
              .ForMember(destination => destination.VarianceType, opts => opts.MapFrom(source => source.VARIANCE_TYPE))
              .ForMember(destination => destination.VarianceComment, opts => opts.MapFrom(source => source.VARIANCE_COMMENT))
              .ForMember(destination => destination.VarianceAmount, opts => opts.MapFrom(source => source.VARIANCE_AMOUNT))

              .ForMember(destination => destination.VarianceCostId, opts => opts.MapFrom(source => source.VARIANCE_COST_ID));

            //Mapping for List Association
            CreateMap<ListAssociationDTO, ListAssociation>()
              .ForMember(destination => destination.ListAssicationId, opts => opts.MapFrom(source => source.LIST_ASSOCIATION_ID))
              .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.DESCRIPTION))
              .ForMember(destination => destination.Type, opts => opts.MapFrom(source => source.TYPE))
              .ForMember(destination => destination.Order, opts => opts.MapFrom(source => source.ORDER))
              .ForMember(destination => destination.IsDeleted, opts => opts.MapFrom(source => source.IS_DELETED))
              .ForMember(destination => destination.ParentId, opts => opts.MapFrom(source => source.PARENT_ID));

            // Mapping for Events and Variances
            CreateMap<EventAndVarianceDTO, EventAndVariance>()
              .ForMember(destination => destination.API10, opts => opts.MapFrom(source => source.API10))
              .ForMember(destination => destination.EventId, opts => opts.MapFrom(source => source.EVENT_ID))
              .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.TITLE))
              .ForMember(destination => destination.VarianceComment, opts => opts.MapFrom(source => source.VARIANCE_COMMENT))
              .ForMember(destination => destination.VarianceType, opts => opts.MapFrom(source => source.VARIANCE_TYPE))
              .ForMember(destination => destination.VarianceId, opts => opts.MapFrom(source => source.VARIANCE_ID));

            // Mapping for Event
            CreateMap<EventDTO, Event>()
               .ForMember(destination => destination.EventId, opts => opts.MapFrom(source => source.EVENT_ID))
               .ForMember(destination => destination.WellName, opts => opts.MapFrom(source => source.WELL_COMMON_NAME))
               .ForMember(destination => destination.VarianceCostId, opts => opts.MapFrom(source => source.VARIANCE_COST_ID))
               .ForMember(destination => destination.VarianceDurationId, opts => opts.MapFrom(source => source.VARIANCE_DURATION_ID))
               .ForMember(destination => destination.VarianceType, opts => opts.MapFrom(source => source.VARIANCE_TYPE))
               .ForMember(destination => destination.Title, opts => opts.MapFrom(source => source.TITLE))
               .ForMember(destination => destination.Level1, opts => opts.MapFrom(source => source.LEVEL1))
               .ForMember(destination => destination.Level1_Text, opts => opts.MapFrom(source => source.LEVEL1_TEXT))
               .ForMember(destination => destination.Level2, opts => opts.MapFrom(source => source.LEVEL2))
               .ForMember(destination => destination.Level2_Text, opts => opts.MapFrom(source => source.LEVEL2_TEXT))
               .ForMember(destination => destination.ResponsibleParty, opts => opts.MapFrom(source => source.RESPONSIBLE_PARTY))
               .ForMember(destination => destination.ResponsibleParty_Text, opts => opts.MapFrom(source => source.RESPONSIBLE_PARTY_TEXT))
               .ForMember(destination => destination.VarianceType, opts => opts.MapFrom(source => source.VARIANCE_TYPE))
               .ForMember(destination => destination.CreatedDate, opts => opts.MapFrom(source => source.ROW_CREATE_DATE))
               .ForMember(destination => destination.EventStatus, opts => opts.MapFrom(source => source.EventStatus))
               .ForMember(destination => destination.ClosedStatus, opts => opts.MapFrom(source => source.ClosedStatus))
               .ForMember(destination => destination.CreatedBy, opts => opts.MapFrom(source => source.ROW_CREATE_ID));

            // Mapping for Comments
            CreateMap<CommentDTO, Comment>()
               .ForMember(destination => destination.CommentId, opts => opts.MapFrom(source => source.COMMENT_ID))
               .ForMember(destination => destination.Description, opts => opts.MapFrom(source => source.COMMENT))
               .ForMember(destination => destination.CreatedDate, opts => opts.MapFrom(source => source.ROW_CREATE_DATE))
               .ForMember(destination => destination.CreatedBy, opts => opts.MapFrom(source => source.ROW_CREATE_ID));

            // Mapping for Attachments
            CreateMap<AttachmentDTO, Attachment>()
               .ForMember(destination => destination.AttachmentId, opts => opts.MapFrom(source => source.ATTACHMENT_ID))
               .ForMember(destination => destination.Path, opts => opts.MapFrom(source => source.ATTACHMENT_PATH))
               .ForMember(destination => destination.CreatedDate, opts => opts.MapFrom(source => source.ROW_CREATE_DATE))
               .ForMember(destination => destination.CreatedBy, opts => opts.MapFrom(source => source.ROW_CREATE_ID));


            // Mapping for Event
            CreateMap<Event, EventDTO>()
              .ForMember(destination => destination.EVENT_ID, opts => opts.MapFrom(source => source.EventId))
              .ForMember(destination => destination.WELL_COMMON_NAME, opts => opts.MapFrom(source => source.WellName))
              .ForMember(destination => destination.VARIANCE_COST_ID, opts => opts.MapFrom(source => source.VarianceCostId))
              .ForMember(destination => destination.VARIANCE_DURATION_ID, opts => opts.MapFrom(source => source.VarianceDurationId))
              .ForMember(destination => destination.VARIANCE_TYPE, opts => opts.MapFrom(source => source.VarianceType))
              .ForMember(destination => destination.TITLE, opts => opts.MapFrom(source => source.Title))
              .ForMember(destination => destination.LEVEL1, opts => opts.MapFrom(source => source.Level1))
              .ForMember(destination => destination.LEVEL1_TEXT, opts => opts.MapFrom(source => source.Level1_Text))
              .ForMember(destination => destination.LEVEL2, opts => opts.MapFrom(source => source.Level2))
              .ForMember(destination => destination.LEVEL2_TEXT, opts => opts.MapFrom(source => source.Level2_Text))
              .ForMember(destination => destination.RESPONSIBLE_PARTY, opts => opts.MapFrom(source => source.ResponsibleParty))
              .ForMember(destination => destination.RESPONSIBLE_PARTY_TEXT, opts => opts.MapFrom(source => source.ResponsibleParty_Text))
              .ForMember(destination => destination.VARIANCE_TYPE, opts => opts.MapFrom(source => source.VarianceType))
              .ForMember(destination => destination.ROW_CREATE_DATE, opts => opts.MapFrom(source => source.CreatedDate))
              .ForMember(destination => destination.LOGGED_IN_USERNAME, opts => opts.MapFrom(source => source.LoggedInUserName))
              .ForMember(destination => destination.EventStatus, opts => opts.MapFrom(source => source.EventStatus))
              .ForMember(destination => destination.ClosedStatus, opts => opts.MapFrom(source => source.ClosedStatus))
              .ForMember(destination => destination.ROW_CREATE_ID, opts => opts.MapFrom(source => source.CreatedBy));

            // Mapping for Comments
            CreateMap<Comment, CommentDTO>()
               .ForMember(destination => destination.COMMENT_ID, opts => opts.MapFrom(source => source.CommentId))
               .ForMember(destination => destination.COMMENT, opts => opts.MapFrom(source => source.Description))
               .ForMember(destination => destination.ROW_CREATE_DATE, opts => opts.MapFrom(source => source.CreatedDate))
               .ForMember(destination => destination.ROW_CREATE_ID, opts => opts.MapFrom(source => source.CreatedBy));

            // Mapping for Attachments
            CreateMap<Attachment, AttachmentDTO>()
               .ForMember(destination => destination.ATTACHMENT_ID, opts => opts.MapFrom(source => source.AttachmentId))
               .ForMember(destination => destination.ATTACHMENT_PATH, opts => opts.MapFrom(source => source.Path))
               .ForMember(destination => destination.ROW_CREATE_DATE, opts => opts.MapFrom(source => source.CreatedDate))
               .ForMember(destination => destination.ROW_CREATE_ID, opts => opts.MapFrom(source => source.CreatedBy));

            // Mapping for function and phase
            CreateMap<FunctionAndPhase, FunctionAndPhaseDTO>()
             .ForMember(destination => destination.FUNCTION, opts => opts.MapFrom(source => source.Function))
             .ForMember(destination => destination.ACTIVITY_PHASE, opts => opts.MapFrom(source => source.Phase))
             .ForMember(destination => destination.DISPLAY_ORDER, opts => opts.MapFrom(source => source.DisplayOrder));
        }
    }
}
