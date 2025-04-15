using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class SharedLookLike
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid LikedUserId { get; set; }
        public Guid LikedSharedLookId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public bool IsLiked { get; set; } = true;   
        public User? LikedUser { get; set; }
        public SharedLook? LikedSharedLook { get; set; }
    }
}
