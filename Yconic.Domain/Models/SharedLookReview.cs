using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class SharedLookReview
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ReviewerUserId { get; set; }
        public Guid ReviewedSharedLookId { get; set; }
        public required string Review { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public User? ReviewerUser { get; set; }
        public SharedLook? ReviewedSharedLook { get; set; }

    }
}
