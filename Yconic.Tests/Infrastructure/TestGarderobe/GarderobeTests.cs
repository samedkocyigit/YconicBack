using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;

namespace Yconic.Tests.Infrastructure.TestGarderobe
{
    public class GarderobeTests
    {
        protected readonly DbContextOptions<AppDbContext> _options;

        public GarderobeTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task GarderobeRepository_Add_Should_Save_Garderobe()
        {
            using var context = new AppDbContext(_options);
            var user = new User{
                Id = Guid.NewGuid(),
                Name = "Test",
                Surname = "TestSurname",
                PhoneNumber = "5555555555",
                Email = "test@example.com",
                Password = "123456"
            };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            var repository = new GarderobeRepository(context);
            var garderobe = new Garderobe{
                Id = Guid.NewGuid(),
                Name = "Test",
                UserId = user.Id
            };

            await repository.Add(garderobe);
            await context.SaveChangesAsync();

            var savedGarderobe = await context.Garderobes.FirstOrDefaultAsync(g => g.Id == garderobe.Id);
            Assert.NotNull(savedGarderobe);
            Assert.Equal(garderobe.Name, savedGarderobe.Name);
            Assert.Equal(garderobe.UserId, savedGarderobe.UserId);
        }
    }
}