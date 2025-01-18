namespace Yconic.Domain.Models;
public class Garderobe
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public string Brand { get; set; }
    public string Material { get; set; }
    public string Season { get; set; }
    public string Style { get; set; }
    public string Type { get; set; }
    public string[] ClothesPhotos { get; set; }
    public Guid ClothesCategoryId { get; set; }
    public GarderobeCategories ClothesCategory { get; set; }
}
