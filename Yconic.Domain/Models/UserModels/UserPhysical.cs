namespace Yconic.Domain.Models.UserModels
{
    public class UserPhysical : BaseClass
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int? Age { get; set; }
        public DateTime? Birthday { get; set; }
    }
}