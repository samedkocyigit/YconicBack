using Moq;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Application.Services.UserServices;
using Microsoft.Extensions.Logging;
using Yconic.Domain.Models;

namespace Yconic.Tests.Application.TestUser.UserTests
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;
        private readonly Mock<ILogger<UserService>> _loggerMock;

        public UserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task CreateUser_Should_Return_Success()
        {
            var user = new User{
                Name = "Test",
                Email = "test@example.com",
                Password = "123456",
                Surname = "TestSurname",
                PhoneNumber = "5555555555"
            };
            _userRepositoryMock.Setup(x => x.Add(It.IsAny<User>()))
                .ReturnsAsync(user);

            var result = await _userService.CreateUser(user);

            Assert.True(result.IsSuccess);
            Assert.Equal(user.Name, result.Data.Name);
            Assert.Equal(user.Email, result.Data.Email);
            Assert.Equal(user.Password, result.Data.Password);
            Assert.Equal(user.Surname, result.Data.Surname);
            Assert.Equal(user.PhoneNumber, result.Data.PhoneNumber);
            _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }
    }
} 