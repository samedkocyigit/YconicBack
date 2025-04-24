using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models
{
    public class SharedLook : BaseClass
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid SuggestionId { get; set; }
        public Suggestion? Suggestion { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public ICollection<SharedLookReview>? Reviews { get; set; } = new List<SharedLookReview>();
        public ICollection<SharedLookLike>? Likes { get; set; } = new List<SharedLookLike>();
    }
}