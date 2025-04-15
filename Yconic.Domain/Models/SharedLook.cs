using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class SharedLook
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid SuggestionId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Description { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
        public Suggestion? Suggestion { get ; set; }
        public User? User { get; set; }
        public ICollection<SharedLookReview>? Reviews { get; set; } = new List<SharedLookReview>();
        public ICollection<SharedLookLike>? Likes { get; set; } = new List<SharedLookLike>();
    }
}
