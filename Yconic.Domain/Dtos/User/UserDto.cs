using Yconic.Domain.Enums;
using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.User
{
    public class UserDto
    {
        public Guid id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public UserRoles role { get; set; }
        public int? age { get; set; }
        public decimal? weight { get; set; }
        public decimal? height { get; set; }
        public string? phoneNumber { get; set; }
        public Guid userPersonaId { get; set; }
        public Guid userGarderobeId { get; set; }
        public Garderobe? garderobe { get; set; }
        public Persona? persona { get; set; }
        public List<Suggestions>? suggestions {get; set;}


    }
}
