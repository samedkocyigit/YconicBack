using Yconic.Domain.Models;

namespace Yconic.Tests.Domain.TestClothe;

public class ClotheTests
{
    [Fact]
    public void Clothe_Creation_Should_Set_Properties_Correctly()
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
            UserId = user.Id
        };
        var clotheCategory = new ClotheCategories
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test Description",
            GarderobeId = garderobe.Id,
            Clothes = new List<Clothe>()
        };
    
        var clothe = new Clothe
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Brand = "Test",
            CategoryId = clotheCategory.Id,
            Photos = new List<ClothePhoto>()
        };

        clotheCategory.Clothes.Add(clothe);

        Assert.Equal(clothe.Id, clotheCategory.Clothes.First().Id);
        Assert.Equal(clothe.Name, clotheCategory.Clothes.First().Name);
        Assert.Equal(clothe.Brand, clotheCategory.Clothes.First().Brand);
        Assert.Equal(clothe.CategoryId, clotheCategory.Clothes.First().CategoryId);
        Assert.Equal(clotheCategory.Clothes.Count, 1);
        Assert.Equal(clotheCategory.Clothes.First().Id, clothe.Id);
        Assert.Equal(clothe.Photos.Count, 0);
    }
}