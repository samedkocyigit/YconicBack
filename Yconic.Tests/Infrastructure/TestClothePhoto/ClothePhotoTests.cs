using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;

namespace Yconic.Tests.Infrastructure.TestClothePhoto;

public class ClothePhotoTests
{
    protected readonly DbContextOptions<AppDbContext> _options;

    public ClothePhotoTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public async Task ClothePhotoRepository_Add_Should_Save_ClothePhoto()
    {
        using var context = new AppDbContext(_options);
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Surname = "Test",
            Email = "test@test.com",
            Password = "test",
            PhoneNumber = "5555555555"
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        var garderobe = new Garderobe
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            UserId = user.Id
        };
        await context.Garderobes.AddAsync(garderobe);
        await context.SaveChangesAsync();

        var clotheCategory = new ClotheCategories
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test Description",
            GarderobeId = garderobe.Id,
            Clothes = new List<Clothe>()
        };
        await context.ClotheCategories.AddAsync(clotheCategory);
        await context.SaveChangesAsync();

        var clothe = new Clothe
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Brand = "Test",
            CategoryId = clotheCategory.Id,
            Photos = new List<ClothePhoto>()
        };
        await context.Clothes.AddAsync(clothe);
        await context.SaveChangesAsync();

        var clothePhoto = new ClothePhoto
        {
            Id = Guid.NewGuid(),
            ClotheId = clothe.Id,
            Url = "test.com"
        };
        var clothePhoto2 = new ClothePhoto
        {
            Id = Guid.NewGuid(),
            ClotheId = clothe.Id,
            Url = "test2.com"
        };
        clothe.Photos.Add(clothePhoto);
        clothe.Photos.Add(clothePhoto2);
        await context.ClothePhotos.AddAsync(clothePhoto);
        await context.ClothePhotos.AddAsync(clothePhoto2);
        await context.SaveChangesAsync();

        var savedClothePhoto = await context.ClothePhotos.FirstOrDefaultAsync(c => c.Id == clothePhoto.Id);
        var savedClothePhoto2 = await context.ClothePhotos.FirstOrDefaultAsync(c => c.Id == clothePhoto2.Id);
        Assert.NotNull(savedClothePhoto);
        Assert.Equal(savedClothePhoto.ClotheId, clothePhoto.ClotheId);
        Assert.Equal(savedClothePhoto.Url, clothePhoto.Url);
        Assert.NotNull(savedClothePhoto2);
        Assert.Equal(savedClothePhoto2.ClotheId, clothePhoto2.ClotheId);
        Assert.Equal(savedClothePhoto2.Url, clothePhoto2.Url);
    }
}