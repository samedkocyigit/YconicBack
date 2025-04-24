using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models
{
    public class SharedLookLike : BaseClass
    {
        public Guid LikedUserId { get; set; }
        public User? LikedUser { get; set; }
        public Guid LikedSharedLookId { get; set; }
        public SharedLook? LikedSharedLook { get; set; }
        public bool IsLiked { get; set; } = true;
    }
}