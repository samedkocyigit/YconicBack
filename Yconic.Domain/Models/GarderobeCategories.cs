using System.Text.Json.Serialization;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
public class GarderobeCategories
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public ClothesTypes ClothesType { get; set; }
    [JsonIgnore]
    public virtual ICollection<Garderobe> Garderobes { get; set; }
}
