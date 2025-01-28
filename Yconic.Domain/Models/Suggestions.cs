using System.Text.Json.Serialization;

namespace Yconic.Domain.Models;
public class Suggestions
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<Clothe>? SuggestedLook { get; set; } = new List<Clothe>();
    [JsonIgnore]
    public User? User { get; set; }
}
