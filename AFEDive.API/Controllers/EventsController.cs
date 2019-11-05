using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Interfaces.Respositories;
using AFEDIVE.DataAccess.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AFEDive.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        IEventRepository _eventRepository;
        IDrillingRepository _drillingRepository;
        private readonly ILogger _logger;
        public EventsController(IEventRepository eventRepository, IDrillingRepository drillingRepository, IMapper mapper,ILogger<EventsController> logger)
        {
            //  _configuration = configuration;
            _eventRepository = eventRepository;
            _drillingRepository = drillingRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [Route("GetEvent/{id}")]
        public async Task<Event> GetEvent(int id)
        {
            var eventResult = new Event();
            try
            {
                var eventRepositoryResult = await _eventRepository.Get(id);
                eventResult = _mapper.Map<EventDTO, Event>(eventRepositoryResult
                    .Item1);
                eventResult.Comments = _mapper.Map<List<CommentDTO>, List<Comment>>(eventRepositoryResult
                    .Item2);

                eventResult.Attachments = _mapper.Map<List<AttachmentDTO>, List<Attachment>>(eventRepositoryResult
                    .Item3);
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Get Event" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }

            return eventResult;

        }

        // POST: api/Events/SaveEvent
        [HttpPost]
        [Route("SaveEvent")]
        public async Task<int> SaveEvent([FromBody] Event data)
        {
            int eventId = 0;
            try
            {
                // mappper to convert to DTO objects
                var eventData = _mapper.Map<Event, EventDTO>(data);
                var comment = _mapper.Map<Comment, CommentDTO>(data.Comments[0]);
                var attachment = new AttachmentDTO();

                if (data.Attachments != null && data.Attachments.Count > 0)
                    attachment = _mapper.Map<Attachment, AttachmentDTO>(data.Attachments[0]);

                // call repository method to save an event 
                eventId = await _eventRepository.AddEvent(eventData, comment, attachment);

            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Save Event" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }
            return eventId;

        }

        // POST: api/Events/GetEventsWithFilters
        [HttpPost]
        [Route("GetEventsWithFilters")]
        public async Task<List<Event>> GetEventsWithFilters([FromBody] EventFilterParameters data)
        {
            var eventResult = new List<Event>();
            try
            {
                var eventRepositoryResult = await _eventRepository.GetEvents(data.WellId, data.Area, data.Function, data.Phase, data.Operation, data.Level1, data.Level2);

                eventResult = _mapper.Map<List<EventDTO>, List<Event>>(eventRepositoryResult);

            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Get Event with filters" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }

            return eventResult;

        }

        [HttpPost]
        [Route("SaveUserFilterState")]
        public async Task<bool> SaveUserFilterState([FromBody] EventFilterParameters filters)
        {
            var stateSaved = true;
            try
            {
               var eventRepositoryResult = await _eventRepository.SaveState(JsonConvert.SerializeObject(filters), filters.UserId, filters.Filter);

                

            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Save user filter state" } }))
                {
                    _logger.LogError(ex.Message);
                }

                //     throw ex;
            }
            return stateSaved;

        }




        // DELETE: api/Events/DeleteEvent/5
        [HttpDelete]
        [Route("DeleteEvent/{id}")]
        public async Task<bool> DeleteEvent(int id)
        {
            bool eventDeleted = true;
            try
            {
                eventDeleted= await _eventRepository.DeleteEvent(id);
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Save user filter state" } }))
                {
                    _logger.LogError(ex.Message);
                }

            }
            return eventDeleted;
        }

        [HttpGet]
        [Route("GetUserPreSavedFilters/{userId}/{filterType}")]
        public async Task<EventFilterParameters> GetUserPreSavedFilters(string userId, string filterType)
        {
            var result = new EventFilterParameters();
            try
            {
                var eventRepositoryResult = await _eventRepository.GetUserSavedFilters(userId, (Enums.Filter)Enum.Parse(typeof(Enums.Filter), filterType));
                result = JsonConvert.DeserializeObject<EventFilterParameters>(eventRepositoryResult);

            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "GetUserPreSavedFilters" } }))
                {
                    _logger.LogError(ex.Message);
                }
            }
            return result;
        }

        // GET: api/Events/GetListAssociations
        [HttpGet]
        [Route("GetListAssociations")]
        public async Task<List<ListAssociation>> GetListAssociations()
        {
            var listAssociationData = new List<ListAssociation>();
            try
            {
                //  Get data from repository
                listAssociationData = _mapper.Map<List<ListAssociationDTO>, List<ListAssociation>>(await _eventRepository.GetListAssociations());
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Get List Assoications" } }))
                {
                    _logger.LogError(ex.Message);
                }
            }
            return listAssociationData;
        }

        [HttpGet]
        [Route("GetFunctionAndPhases")]
        public async Task<List<FunctionAndPhase>> GetFunctionAndPhases()
        {
            var functionAndPhases = new List<FunctionAndPhase>();
            try
            {
                //  Get data from repository
                var functionAndPhasesDtos = await _drillingRepository.GetFunctionAndPhases();
                functionAndPhasesDtos.ForEach((fp) =>
                {
                    functionAndPhases.Add(new FunctionAndPhase()
                    {
                        DisplayOrder = fp.DISPLAY_ORDER,
                        Function = fp.FUNCTION,
                        Phase = fp.ACTIVITY_PHASE
                    });

                });
              
            }
            catch (Exception ex)
            {
                using (_logger.BeginScope(new Dictionary<string, object> { { "Events", "Get Function And Phases" } }))
                {
                    _logger.LogError(ex.Message);
                }
            }
            return functionAndPhases;
        }

        // DELETE: api/Events/DeleteAttachmentById/5
        [HttpDelete]
        [Route("DeleteAttachmentById/{id}")]
        public async Task<ActionResult> DeleteAttachmentById(Guid id)
        {
            try
            {
                // Reading Data from query parameter
                Guid attachmentId = Guid.Empty;
                attachmentId = id != null ? id : Guid.Empty;
                if (attachmentId != Guid.Empty)
                {
                    return new OkObjectResult(await _eventRepository.DeleteAttachment(attachmentId));
                }
                else
                {
                    return new BadRequestObjectResult("Please pass a attachment id on the query string or in the request body");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // DELETE: api/Events/DeleteCommentById/5
        [HttpDelete]
        [Route("DeleteCommentById/{id}")]
        public async Task<ActionResult> DeleteCommentById(Guid id)
        {
            try
            {
                // Reading Data from query parameter
                Guid commentId = Guid.Empty;
                commentId = id != null ? id : Guid.Empty;
                if (commentId != Guid.Empty)
                {
                    return new OkObjectResult(await _eventRepository.DeleteComment(commentId));
                }
                else
                {
                    return new BadRequestObjectResult("Please pass a comment id as route parameter");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
