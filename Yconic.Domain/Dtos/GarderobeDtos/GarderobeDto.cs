using Yconic.Domain.Dtos.ClotheCategoryDtos;

namespace Yconic.Domain.Dtos.GarderobeDtos;

public class GarderobeDto{
  public Guid id { get; set; }
  public string name { get; set; }
  public Guid userId { get; set; }  
  public List<ClotheCategoryDto> categories { get; set; }
}