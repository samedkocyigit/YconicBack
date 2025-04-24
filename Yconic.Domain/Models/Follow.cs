using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models
{
    public class Follow : BaseClass
    {
        public Guid FollowerId { get; set; }
        public User? Follower { get; set; }
        public Guid FollowedId { get; set; }
        public User? Followed { get; set; }

        public DateTime FollowedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; }
        public DateTime? UnfollowedAt { get; set; }
        public DateTime? BlockedAt { get; set; }
        public DateTime? UnblockedAt { get; set; }

        public bool IsFollowing { get; set; } = true;
    }
}