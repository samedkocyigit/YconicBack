using Yconic.Domain.Models;

namespace Yconic.Tests.Domain.TestUser
{
    public class UserTests
    {
        [Fact]
    public void User_Creation_Should_Set_Properties_Correctly()
    {
        var expectedName = "Test";
        var expectedSurname = "TestSurname";
        var expectedPhoneNumber = "5555555555";
        var expectedEmail = "test@example.com";
        var expectedPassword = "123456";

        var user = new User{
            Name = expectedName,
            Email = expectedEmail,
            Password = expectedPassword,
            Surname = expectedSurname,
            PhoneNumber = expectedPhoneNumber
        };

        Assert.Equal(expectedName, user.Name);
        Assert.Equal(expectedEmail, user.Email);
        Assert.Equal(expectedPassword, user.Password);
        Assert.Equal(expectedSurname, user.Surname);
        Assert.Equal(expectedPhoneNumber, user.PhoneNumber);
        }
    }
}