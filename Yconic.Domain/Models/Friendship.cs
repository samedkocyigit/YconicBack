using Yconic.Domain.Enums;

namespace Yconic.Domain.Models
{
    public class Friendship
    {
        public Guid Id { get; set; }

        public Guid RequesterId { get; set; }
        public User Requester { get; set; }

        public Guid AddresseeId { get; set; }
        public User Addressee { get; set; }

        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
