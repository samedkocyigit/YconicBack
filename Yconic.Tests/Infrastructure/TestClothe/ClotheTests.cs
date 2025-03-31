using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;

namespace Yconic.Tests.Infrastructure.TestClothe;

public class ClotheTests
{
    protected readonly DbContextOptions<AppDbContext> _options;

    public ClotheTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }
    [Fact]
    public async Task ClotheRepository_Add_Should_Save_Clothe()
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
            UserId = user.Id,
            ClothesCategory = new List<ClotheCategory>()
        };  
        await context.Garderobes.AddAsync(garderobe);
        await context.SaveChangesAsync();

        var clotheCategory = new ClotheCategory
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

        var savedClothe = await context.Clothes.FirstOrDefaultAsync(c => c.Id == clothe.Id);
        Assert.NotNull(savedClothe);
        Assert.Equal(savedClothe.Name, clothe.Name);
        Assert.Equal(savedClothe.Brand, clothe.Brand);
        Assert.Equal(savedClothe.CategoryId, clothe.CategoryId);    
        Assert.Equal(savedClothe.Photos.Count, 0);
    }
}
