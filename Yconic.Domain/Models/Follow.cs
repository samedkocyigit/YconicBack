using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Yconic.Domain.Models
{
    public class Follow
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FollowerId { get; set; }
        [JsonIgnore]
        public User? Follower { get; set; }

        public Guid FollowedId { get; set; }
        [JsonIgnore]
        public User? Followed { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
