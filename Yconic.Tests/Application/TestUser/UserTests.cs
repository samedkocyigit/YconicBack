using Moq;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Application.Services.UserServices;
using Microsoft.Extensions.Logging;
using Yconic.Domain.Models;
using AutoMapper;

namespace Yconic.Tests.Application.TestUser.UserTests
{
    public class UserTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly Mock<IMapper> _mapperMock; 
        public UserTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserService>>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object, _mapperMock.Object);
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
            Assert.Equal(user.Name, result.Data.name);
            Assert.Equal(user.Email, result.Data.email);
            Assert.Equal(user.Surname, result.Data.surname);
            Assert.Equal(user.PhoneNumber, result.Data.phoneNumber);
            _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
        }
    }
} 