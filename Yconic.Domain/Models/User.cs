using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpiry { get; set; }
        public UserRoles Role { get; set; } = UserRoles.User;
        public DateTime? Birthday { get; set; }
        public int? Age { get; set; }
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public string PhoneNumber { get; set; }
        public Status IsActive { get; set; } = Status.Active;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? UserPersonaId { get; set; }
        public Guid? UserGarderobeId { get; set; }
        public Persona? UserPersona { get; set; }
        public Garderobe? UserGarderobe { get; set; }
        public ICollection<Suggestions>? Suggestions { get; set; } = new List<Suggestions>();
    }
