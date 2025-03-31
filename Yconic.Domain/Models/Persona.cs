using System.Text.Json.Serialization;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
public class Persona
{
    public Guid Id { get; set; }
    public Personas Usertype { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}
