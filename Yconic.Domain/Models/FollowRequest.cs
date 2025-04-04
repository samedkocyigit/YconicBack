using System.Text.Json.Serialization;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Models
{
    public class FollowRequest
    {
        public Guid Id { get; set; }

        public Guid RequesterId { get; set; }
        [JsonIgnore]
        public User? Requester { get; set; }

        public Guid TargetUserId { get; set; }
        [JsonIgnore]
        public User? TargetUser { get; set; }

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public bool IsApproved { get; set; } = false;
        public bool IsRejected { get; set; } = false;
    }

}
