using System.Text.Json.Serialization;

namespace Yconic.Domain.Models;
public class Garderobe
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
    public ICollection<ClotheCategory>? ClothesCategory { get; set; } = new List<ClotheCategory>();
}
