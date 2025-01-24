using Yconic.Domain.Models;

namespace Yconic.Tests.Services;
public class GarderobeTests{
    [Fact]
    public void Garderobe_Should_Have_ClotheCategories_And_EachCategory_Should_Have_Clothes_With_Photos()
    {
        var user = new User{
            Id=Guid.NewGuid(),
            Email = "temp@gmail.com",
            Password= "test123",
            Name ="test",
            Surname="testSurname",
            PhoneNumber="5555555555"
        };
        var garderobe = new Garderobe{
            Id=Guid.NewGuid(),
            UserId = user.Id,
        };
        user.UserGarderobeId = garderobe.Id;
        var category = new ClotheCategories{
            Id= Guid.NewGuid(),
            GarderobeId = garderobe.Id,
            Name = "TestCategory",
            Description= "Test Description"
        };

        garderobe.ClothesCategory.Add(category);
    
        var clothe = new Clothe{Id=Guid.NewGuid(),CategoryId=category.Id,Brand="Zara"};
        var clotheTwo = new Clothe{Id= Guid.NewGuid(),CategoryId=category.Id,Brand="Lcw"};

        category.Clothes = new List<Clothe> {clothe,clotheTwo};

        var clothePhoto = new ClothePhoto{Id = Guid.NewGuid(),ClotheId = clothe.Id, Url="photo1.jpeg"};
        var clotheTwoPhoto = new ClothePhoto{Id = Guid.NewGuid(),ClotheId = clotheTwo.Id , Url="photo2.jpeg"};

        clothe.Photos = new List<ClothePhoto> {clothePhoto};
        clotheTwo.Photos = new List<ClothePhoto> {clotheTwoPhoto};

        Assert.Single(garderobe.ClothesCategory);
        Assert.Equal(2,category.Clothes.Count);
        Assert.Equal("temp@gmail.com",user.Email);
        Assert.Equal("photo1.jpeg",clothe.Photos.First().Url);
    }
}