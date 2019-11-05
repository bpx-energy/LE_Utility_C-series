using AFEDIVE.DataAccess.Interfaces.Respositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using AFEDIVE.DataAccess.Constants;
using System.Data;
using AFEDIVE.DataAccess.Models;
using System.Transactions;
using AFEDive.Common.Models;
using Newtonsoft.Json;

namespace AFEDIVE.DataAccess.Repositories
{
    public class EventRepository : DapperDb, IEventRepository
    {
        public EventRepository(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Get Event Details by Event ID
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<Tuple<EventDTO, List<CommentDTO>, List<AttachmentDTO>>> Get(int eventId)
        {
            Tuple<EventDTO, List<CommentDTO>, List<AttachmentDTO>> result = null;
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventResult = await connection.QuerySingleAsync<EventDTO>(StoredProcedureNames.GET_EVENT_DETAILS, new { eventId = eventId }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                    var attachmentObject = await connection.QueryAsync<AttachmentDTO>(StoredProcedureNames.GET_ATTACHMENTS_BY_EVENT_ID, new { eventId = eventId }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    var commentObject = await connection.QueryAsync<CommentDTO>(StoredProcedureNames.GET_COMMENTS_BY_EVENT_ID, new { eventId = eventId }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    result = new Tuple<EventDTO, List<CommentDTO>, List<AttachmentDTO>>(eventResult, commentObject.AsList(), attachmentObject.AsList()); ;
                }

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        /// <summary>
        /// Delete an Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteEvent(int eventId)
        {
            bool eventDeleted = true;
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventResult = await connection.ExecuteAsync(StoredProcedureNames.DELETE_EVENT_BY_EVENT_ID, new { eventId = eventId }, commandType: CommandType.StoredProcedure);


                }

            }
            catch (Exception ex)
            {
                eventDeleted = false;
            }
            return eventDeleted;
        }

        /// <summary>
        /// Save an event with comment and attachment
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="comment"></param>
        /// <param name="attachment"></param>
        /// <returns></returns>
        public async Task<int> AddEvent(EventDTO evnt, CommentDTO comment, AttachmentDTO attachment)
        {
            using (var connection = CreateConnection())
            {
                // Get CST time
                DateTime timeUtc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

                // Save an event by calling stored proc
                var newEventId = await connection.QuerySingleAsync<int>(StoredProcedureNames.SAVE_EVENT,
                   new
                   {
                       event_id = evnt.EVENT_ID,
                       variance_duration_id = evnt.VARIANCE_DURATION_ID,
                       variance_cost_id = evnt.VARIANCE_COST_ID,
                       title = evnt.TITLE,
                       level1 = evnt.LEVEL1,
                       level2 = evnt.LEVEL2,
                       responsible_party = evnt.RESPONSIBLE_PARTY,
                       variance_type = evnt.VARIANCE_TYPE,
                       event_status = evnt.EventStatus,
                       dateTimeNow = cstTime,
                       userName = evnt.LOGGED_IN_USERNAME,
                       closed_status = evnt.ClosedStatus
                   },
                   commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                // Save comment for an event
                var commentId = await connection.ExecuteAsync(StoredProcedureNames.SAVE_COMMENT,
                   new
                   {
                       event_Id = newEventId,
                       comment_id = (evnt.EVENT_ID > 0 && comment.COMMENT_ID != Guid.Empty && comment.COMMENT_ID != null) ? comment.COMMENT_ID : Guid.NewGuid(),
                       comment_desc = comment.COMMENT,
                       dateTimeNow = cstTime,
                       userName = evnt.LOGGED_IN_USERNAME
                   },
                   commandType: CommandType.StoredProcedure).ConfigureAwait(false);


                var attachmentId = await connection.ExecuteAsync(StoredProcedureNames.SAVE_ATTACHMENT,
                  new
                  {
                      event_Id = newEventId,
                      attachment_Id = (evnt.EVENT_ID > 0 && attachment.ATTACHMENT_ID != Guid.Empty && attachment.ATTACHMENT_ID != null) ? attachment.ATTACHMENT_ID : Guid.NewGuid(),
                      path = attachment.ATTACHMENT_PATH,
                      dateTimeNow = cstTime,
                      userName = evnt.LOGGED_IN_USERNAME
                  },
                  commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                // return created eventid
                return newEventId;
            }
        }



        /// <summary>
        /// Delete an Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAttachment(Guid attachmentId)
        {
            bool attachmentDeleted = true;
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventResult = await connection.ExecuteAsync(StoredProcedureNames.DELETE_ATTACHMENT_BY_ATTACHMENT_ID, new { attachmentId = attachmentId }, commandType: CommandType.StoredProcedure);


                }

            }
            catch (Exception ex)
            {
                attachmentDeleted = false;
            }
            return attachmentDeleted;
        }

        /// <summary>
        /// Delete comment for an event
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteComment(Guid commentId)
        {
            bool commentDeleted = true;
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventResult = await connection.ExecuteAsync(StoredProcedureNames.DELETE_COMMENT_BY_COMMENT_ID, new { commentId = commentId }, commandType: CommandType.StoredProcedure);

                }

            }
            catch (Exception ex)
            {
                commentDeleted = false;
            }
            return commentDeleted;
        }


        /// <summary>
        /// Get all Dropdown list values
        /// </summary>
        /// <returns></returns>
        public async Task<List<ListAssociationDTO>> GetListAssociations()
        {
            var listassociations = new List<ListAssociationDTO>();
            try
            {
                using (var connection = CreateConnection())
                {
                    var listAssociationEntities = await connection.QueryAsync<ListAssociationDTO>(StoredProcedureNames.GET_LIST_ASSOCIATIONS, null, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    listassociations = listAssociationEntities.AsList();
                }
            }
            catch (Exception ex)
            {

            }
            return listassociations;
        }

        /// <summary>
        /// Get Event And Variances For Well
        /// </summary>
        /// <param name="api10"></param>
        /// <param name="varianceType"></param>
        /// <returns></returns>
        public async Task<List<EventAndVarianceDTO>> GetEventAndVariancesForWell(string api10, int varianceType)
        {
            var eventAndVarianceDTO = new List<EventAndVarianceDTO>();
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventAndVariancesEntities = await connection.QueryAsync<EventAndVarianceDTO>(StoredProcedureNames.GET_VARIANCE_AND_EVENT_BY_API10,
                        new { api10 = api10, varianceType = varianceType }
                        , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                    eventAndVarianceDTO = eventAndVariancesEntities.AsList();
                }
            }
            catch (Exception ex)
            {

            }
            return eventAndVarianceDTO;
        }

        /// <summary>
        /// Get events with filter parameters
        /// </summary>
        /// <param name="apiId"></param>
        /// <param name="area"></param>
        /// <param name="function"></param>
        /// <param name="operation"></param>
        /// <param name="variancelevel1"></param>
        /// <param name="variancelevel2"></param>
        /// <returns></returns>
        public async Task<List<EventDTO>> GetEvents(string api10, string area, string function, string phase, string operation, Guid? variancelevel1, Guid? variancelevel2)
        {
            using (var connection = CreateConnection())
            {
                var eventResult = await connection.QueryAsync<EventDTO>(StoredProcedureNames.GET_EVENTS_BY_FILTEREDCONDITIONS,
                        new
                        {
                            api10 = api10,
                            area = area,
                            function = function,
                            phase = phase,
                            operation = operation,
                            variancelevel1 = variancelevel1,
                            variancelevel2 = variancelevel2
                        }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return eventResult.AsList();
            }
        }

        /// <summary>
        /// Get users previously saved filter information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<string> GetUserSavedFilters(string userId, Enums.Filter type)
        {
            string userSavedFilters = string.Empty;
            try
            {
                using (var connection = CreateConnection())
                {
                    var savedState = await connection.QueryFirstOrDefaultAsync<StateDTO>(StoredProcedureNames.GET_STATE,
                              new
                              {
                                  username = userId,
                                  type = (int)type
                              }, commandType: CommandType.StoredProcedure).ConfigureAwait(false);

                    if (savedState != null)
                        userSavedFilters = savedState.STATE;


                }
            }
            catch (Exception ex)
            {
                // [Todo]
                throw ex;
            }
            return userSavedFilters;

        }


        /// <summary>
        /// Delete an Event
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public async Task<bool> SaveState(string filterSettings, string userId, Enums.Filter type)
        {
            bool StateSaved = true;
            try
            {
                using (var connection = CreateConnection())
                {
                    var eventResult = await connection.ExecuteAsync(StoredProcedureNames.SAVE_STATE, new
                    {
                        state_id = new Guid(),
                        state = filterSettings,
                        type = (int)type,
                        userName = userId
                    }, commandType: CommandType.StoredProcedure);


                }

            }
            catch (Exception ex)
            {
                StateSaved = false;
            }
            return StateSaved;
        }

    }
}

