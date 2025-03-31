using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;

namespace Yconic.Tests.Infrastructure.TestClotheCategory;

public class ClotheCategoryTests
{
    private readonly DbContextOptions<AppDbContext> _options;
    public ClotheCategoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [Fact]
    public async Task ClotheCategoryRepository_Add_Should_Save_ClotheCategory()
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

        var savedClotheCategory = await context.ClotheCategories.FirstOrDefaultAsync(c => c.Id == clotheCategory.Id);
        Assert.NotNull(savedClotheCategory);
        Assert.Equal(savedClotheCategory.Name, clotheCategory.Name);
        Assert.Equal(savedClotheCategory.GarderobeId, clotheCategory.GarderobeId);
        Assert.Equal(savedClotheCategory.Clothes.Count, 0);
    }
}