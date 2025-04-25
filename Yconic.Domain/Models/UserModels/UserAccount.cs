using System.Text.Json.Serialization;

namespace Yconic.Domain.Models.UserModels
{
    public class UserAccount : BaseClass
    {
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        public string? PhoneNumber { get; set; }
        public bool IsPrivate { get; set; } = false;
        public bool EmailVerified { get; set; } = false;
        public bool PhoneVerified { get; set; } = false;
    }
}