using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.Ai;
public class SuggestedLookResult
{
    public List<Clothe> Clothes { get; set; }
    public string MainImageUrl { get; set; }
}
