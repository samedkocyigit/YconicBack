using Microsoft.Extensions.Logging;
using Moq;
using Yconic.Application.Services.GarderobeServices;
using Yconic.Application.Services.UserServices;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;

namespace Yconic.Tests.Application.TestGarderobe.GarderobeTests
{
    public class GarderobeTests
    {
        private readonly Mock<IGarderobeRepository> _garderobeRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IGarderobeService _garderobeService;
        private readonly IUserService _userService;
        private readonly Mock<ILogger<UserService>> _loggerMock;


        public GarderobeTests()
        {
            _garderobeRepositoryMock = new Mock<IGarderobeRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _garderobeService = new GarderobeService(_garderobeRepositoryMock.Object);
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public async Task CreateGarderobe_Should_Return_Garderobe()
        {
            var user = new User{
                Id = Guid.NewGuid(),
                Name = "Test",
                Email = "test@example.com",
                Password = "123456",
                Surname = "TestSurname",
                PhoneNumber = "5555555555"
            };
            await _userService.CreateUser(user);

            var garderobe = new Garderobe{
                Id = Guid.NewGuid(),
                Name = "Test",
                UserId = user.Id
            };
            _userRepositoryMock.Setup(x => x.GetById(user.Id))
                .ReturnsAsync(user);
            _garderobeRepositoryMock.Setup(x => x.Add(It.IsAny<Garderobe>()))
                .ReturnsAsync(garderobe);

            var result = await _garderobeService.CreateGarderobe(garderobe);
            var userResult = await _userService.GetUserById(user.Id);

            Assert.Equal(garderobe.Name, result.Name);
            Assert.Equal(garderobe.UserId, result.UserId);
            Assert.Equal(user.Id, userResult.Data.Id);
            Assert.Equal(user.Name, userResult.Data.Name);
            Assert.Equal(user.Email, userResult.Data.Email);
            Assert.Equal(user.Password, userResult.Data.Password);
            Assert.Equal(user.Surname, userResult.Data.Surname);
            Assert.Equal(user.PhoneNumber, userResult.Data.PhoneNumber);
        }
    }
}