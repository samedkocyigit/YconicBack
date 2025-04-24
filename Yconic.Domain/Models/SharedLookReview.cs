using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models
{
    public class SharedLookReview : BaseClass
    {
        public Guid ReviewerUserId { get; set; }
        public User? ReviewerUser { get; set; }
        public Guid ReviewedSharedLookId { get; set; }
        public SharedLook? ReviewedSharedLook { get; set; }

        public string Review { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}