using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public string PhoneNumber { get; set; }
    public Status IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid UserPersonaId { get; set; }
    public Guid UserGarderobeId { get; set; }
    public Persona UserPersona { get; set; }
    public Garderobe UserGarderobe { get; set; }
}
