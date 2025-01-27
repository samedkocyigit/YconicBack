using Yconic.Domain.Models;

namespace Yconic.Tests.Domain.TestClotheCategory;

public class ClotheCategoryTests
{
    [Fact]
    public void ClotheCategory_Creation_Should_Set_Properties_Correctly()
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "test@test.com",
            Password = "test",
            Name = "Test",
            Surname = "Test"
        };
        var garderobe = new Garderobe
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            UserId = user.Id,
            ClothesCategory = new List<ClotheCategories>()
        };

        var clotheCategory = new ClotheCategories
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            GarderobeId = garderobe.Id,
            Clothes = new List<Clothe>()
        };
        garderobe.ClothesCategory.Add(clotheCategory);

        Assert.Equal(clotheCategory.Id, garderobe.ClothesCategory.First().Id);
        Assert.Equal(clotheCategory.Name, garderobe.ClothesCategory.First().Name);
        Assert.Equal(clotheCategory.GarderobeId, garderobe.ClothesCategory.First().GarderobeId);
        Assert.Equal(clotheCategory.Clothes.Count, 0);
        Assert.Equal(garderobe.ClothesCategory.Count, 1);
        Assert.Equal(garderobe.ClothesCategory.First().Id, clotheCategory.Id);
    }
}