namespace Yconic.Domain.Models.UserModels
{
    public class UserPersonal : BaseClass
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Bio { get; set; }
        public string? ProfilePhoto { get; set; }
    }
}