using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
public class Persona
{
    public Guid Id { get; set; }
    public Personas Usertype { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}
