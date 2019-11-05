using AFEDive.Common.Models;
using AFEDIVE.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AFEDIVE.DataAccess.Interfaces.Respositories
{
    public interface IEventRepository
    {
        Task<Tuple<EventDTO, List<CommentDTO>, List<AttachmentDTO>>> Get(int eventId);
        Task<List<ListAssociationDTO>> GetListAssociations();
        Task<bool> DeleteEvent(int eventId);
        Task<bool> DeleteAttachment(Guid attachmentId);
        Task<bool> DeleteComment(Guid commentId);
        Task<List<EventAndVarianceDTO>> GetEventAndVariancesForWell(string api, int varianceType);
        Task<int> AddEvent(EventDTO evnt, CommentDTO comment, AttachmentDTO attachment);
        Task<List<EventDTO>> GetEvents(string api10, string area, string function, string phase, string operation, Guid? variancelevel1, Guid? variancelevel2);
        Task<string> GetUserSavedFilters(string userId, Enums.Filter type);
        Task<bool> SaveState(string filterSettings, string userId, Enums.Filter type);
    }
}
