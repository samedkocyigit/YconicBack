using Yconic.Application.Services.ClotheServices;
using Yconic.Infrastructure.Repositories.ClotheRepositories;
using Yconic.Domain.Models;
using Moq;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using System.Text;
using Yconic.Infrastructure.Repositories.ClotheCategoryRepositories;

namespace Yconic.Tests.Application.TestClothe;

public class ClotheTest
{
    private readonly Mock<IClotheRepository> _clotheRepositoryMock;
    private readonly Mock<IClotheCategoryRepository> _clotheCategoriesRepositoryMock;
    private readonly Mock<IClothePhotoRepository> _clothePhotoRepositoryMock;
    private readonly ClotheService _clotheService;

    public ClotheTest()
    {
        _clotheRepositoryMock = new Mock<IClotheRepository>();
        _clotheCategoriesRepositoryMock = new Mock<IClotheCategoryRepository>();
        _clothePhotoRepositoryMock = new Mock<IClothePhotoRepository>();
        _clotheService = new ClotheService(_clotheRepositoryMock.Object, _clothePhotoRepositoryMock.Object, _clotheCategoriesRepositoryMock.Object);
    }
    [Fact]
    public async Task CreateClothe_Should_Create_Clothe_Successfully()
    {
        var user = new User{    
            Id = Guid.NewGuid(),
            Name = "Test",
            Surname = "TestSurname",
            PhoneNumber = "5555555555",
            Email = "test@example.com",
            Password = "123456"
        };
        var garderobe = new Garderobe{
            Id = Guid.NewGuid(),
            Name = "Test",
            UserId = user.Id,
            ClothesCategory = new List<ClotheCategory>()
        };
        var clotheCategory = new ClotheCategory{
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test Description",
            GarderobeId = garderobe.Id,
            Clothes = new List<Clothe>()
        };

        var fakeFile = new Mock<IFormFile>();
        fakeFile.Setup(f => f.FileName).Returns("test.jpg");
        fakeFile.Setup(f => f.Length).Returns(1024);

        var clotheRequest = new AddClotheRequestDto
        {
            Name = "Test",
            Brand = "Test",
            CategoryId = clotheCategory.Id,
            Photos = new List<IFormFile> { fakeFile.Object }
        };

        var clothe = new Clothe
        {
            Id = Guid.NewGuid(),
            Name = clotheRequest.Name,
            Brand = clotheRequest.Brand,
            CategoryId = clotheRequest.CategoryId,
            Photos = new List<ClothePhoto>()
        };

        var clothePhoto = new ClothePhoto
        {
            Id = Guid.NewGuid(),
            Url = "/uploads/test.jpg",
            ClotheId = clothe.Id
        };

        clothe.Photos.Add(clothePhoto);

        _clotheRepositoryMock.Setup(x => x.Add(It.IsAny<Clothe>())).ReturnsAsync(clothe);
        _clotheCategoriesRepositoryMock.Setup(x => x.GetById(clotheCategory.Id)).ReturnsAsync(clotheCategory);
        _clothePhotoRepositoryMock.Setup(x => x.Add(It.IsAny<ClothePhoto>())).ReturnsAsync(clothePhoto);

        await _clotheService.CreateClothe(clotheRequest);

        _clotheRepositoryMock.Verify(x => x.Add(It.IsAny<Clothe>()), Times.Once);
        _clotheCategoriesRepositoryMock.Verify(x => x.GetById(clotheCategory.Id), Times.Once);
        _clothePhotoRepositoryMock.Verify(x => x.Add(It.IsAny<ClothePhoto>()), Times.Once);
        Assert.Equal(clothe.Photos.Count, 1);
        Assert.Equal(clothe.Photos.First().Url, clothePhoto.Url);
        Assert.Equal(clothe.Photos.First().ClotheId, clothePhoto.ClotheId);
    }
}