using Yconic.Domain.Models;

namespace Yconic.Tests.Domain.TestClothePhoto;

public class ClothePhotoTests
{
    [Fact]
    public void ClothePhoto_Creation_Should_Set_Properties_Correctly()
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
        var clotheCategory = new ClotheCategory
        {
            Id = Guid.NewGuid(),
            Name = "Test",
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
        var clothePhoto = new ClothePhoto
        {
            Id = Guid.NewGuid(),
            Url = "Test",
            ClotheId = clothe.Id,
        };
        var clothePhoto2 = new ClothePhoto
        {
            Id = Guid.NewGuid(),
            Url = "Test",
            ClotheId = clothe.Id
        };
        clothe.Photos.Add(clothePhoto);
        clothe.Photos.Add(clothePhoto2);

        Assert.Equal(clothe.Photos.Count, 2);
        Assert.Equal(clothe.Photos.First().Id, clothePhoto.Id);
        Assert.Equal(clothe.Photos.First().Url, clothePhoto.Url);
        Assert.Equal(clothe.Photos.First().ClotheId, clothePhoto.ClotheId);
    }
}