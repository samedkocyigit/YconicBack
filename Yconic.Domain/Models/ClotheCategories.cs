using System.Text.Json.Serialization;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;
public class ClotheCategories
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public CategoryTypes CategoryType {get; set;}
    public Guid GarderobeId { get; set; }
    [JsonIgnore]
    public Garderobe? Garderobe { get; set; }
    public  ICollection<Clothe>? Clothes { get; set; }
}
