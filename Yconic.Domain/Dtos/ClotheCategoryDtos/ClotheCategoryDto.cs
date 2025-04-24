using Yconic.Domain.Dtos.ClotheDtos;
using Yconic.Domain.Enums;
using Yconic.Domain.Models;

namespace Yconic.Domain.Dtos.ClotheCategoryDtos;

public class ClotheCategoryDto
{
    public Guid id { get; set; }
    public string name { get; set; }
    public ClotheCategoryType categoryTypeId { get; set; }
    public Guid garderobeId { get; set; }

    public List<ClotheDto> clothes { get; set; }
}