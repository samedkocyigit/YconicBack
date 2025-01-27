using Yconic.Domain.Models;

namespace Yconic.Tests.Domain.TestGarderobe
{
    public class GarderobeTests
    {
        [Fact]
        public void Garderobe_Creation_Should_Set_Properties_Correctly()
        {
            var userId = Guid.NewGuid();
            var userName = "Test";
            var userSurname = "TestSurname";
            var userPhoneNumber = "5555555555";
            var userEmail = "test@example.com";
            var userPassword = "123456";
            var user = new User{
                Id = userId,
                Name = userName,
                Surname = userSurname,
                PhoneNumber = userPhoneNumber,
                Email = userEmail,
                Password = userPassword
            };

            var expectedName = "Test";
            var garderobe = new Garderobe{
                Name = expectedName,
                UserId = user.Id
            };

            Assert.Equal(expectedName, garderobe.Name);
            Assert.Equal(user.Id, garderobe.UserId);
        }
    }
}