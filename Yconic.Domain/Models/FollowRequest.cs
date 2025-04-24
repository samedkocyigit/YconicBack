using Yconic.Domain.Enums;
using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models
{
    public class FollowRequest : BaseClass
    {
        public Guid RequesterId { get; set; }
        public User? Requester { get; set; }
        public Guid TargetUserId { get; set; }
        public User? TargetUser { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RejectedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Pending;
    }
}