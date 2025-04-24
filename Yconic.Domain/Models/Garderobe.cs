using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models;

public class Garderobe : BaseClass
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string? Name { get; set; }
    public ICollection<ClotheCategory>? ClothesCategory { get; set; } = new List<ClotheCategory>();
}