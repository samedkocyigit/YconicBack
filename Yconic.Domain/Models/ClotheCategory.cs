using System.Text.Json.Serialization;
using Yconic.Domain.Enums;

namespace Yconic.Domain.Models;

public class ClotheCategory : BaseClass
{
    public string Name { get; set; }

    public Guid ClotheCategoryTypeId { get; set; }
    public ClotheCategoryType? ClotheCategoryType { get; set; }
    public Guid GarderobeId { get; set; }
    public Garderobe? Garderobe { get; set; }

    public ICollection<Clothe>? Clothes { get; set; } = new List<Clothe>();
}