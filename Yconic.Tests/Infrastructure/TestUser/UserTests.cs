using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using Yconic.Infrastructure.ApplicationDbContext;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Domain.Models;

namespace Yconic.Tests.Infrastructure.TestUser.UserTests
{
    public class UserTests
    {
        protected readonly DbContextOptions<AppDbContext> _options;

        public UserTests()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }

        [Fact]
        public async Task UserRepository_Add_Should_Save_User()
        {
            using var context = new AppDbContext(_options);
            var repository = new UserRepository(context);
            var user = new User{
                Name = "Test",
                Email = "test@example.com",
                Password = "123456",
                Surname = "TestSurname",
                PhoneNumber = "5555555555"
            };

            await repository.Add(user);
            await context.SaveChangesAsync();

            var savedUser = await context.Users.FirstOrDefaultAsync();
            Assert.NotNull(savedUser);
            Assert.Equal(user.Name, savedUser.Name);
            Assert.Equal(user.Email, savedUser.Email);
            Assert.Equal(user.Password, savedUser.Password);
            Assert.Equal(user.Surname, savedUser.Surname);
            Assert.Equal(user.PhoneNumber, savedUser.PhoneNumber);
        }
    }
} 