using Yconic.Domain.Models.UserModels;

namespace Yconic.Domain.Models;

public class Suggestion : BaseClass
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public ICollection<Clothe>? SuggestedLook { get; set; } = new List<Clothe>();
}