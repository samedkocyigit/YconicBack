using Moq;
using Yconic.Application.Services.ClotheCategoriesServices;
using Yconic.Domain.Models;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;


namespace Yconic.Tests.Application.TestClotheCategory
{
    public class ClotheCategoryTests
    {
        private readonly Mock<IClotheCategoriesRepository> _clotheCategoriesRepositoryMock;
        private readonly Mock<IGarderobeRepository> _garderobeRepositoryMock;
        private readonly ClotheCategoriesService _clotheCategoriesService;

        public ClotheCategoryTests()
        {
            _clotheCategoriesRepositoryMock = new Mock<IClotheCategoriesRepository>();
            _garderobeRepositoryMock = new Mock<IGarderobeRepository>();
            _clotheCategoriesService = new ClotheCategoriesService(_clotheCategoriesRepositoryMock.Object, _garderobeRepositoryMock.Object);
        }
        [Fact]
        public async Task CreateClotheCategories_Should_Return_List_Of_ClotheCategories()
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
                UserId = user.Id
            };
            var clotheCategory = new ClotheCategory{
                Id = Guid.NewGuid(),
                Name = "Test",
                Description = "Test Description",
                GarderobeId = garderobe.Id
            };
            

            _clotheCategoriesRepositoryMock.Setup(repo => repo.Add(clotheCategory)).ReturnsAsync(clotheCategory);
            _garderobeRepositoryMock.Setup(repo => repo.GetById(garderobe.Id)).ReturnsAsync(garderobe);

            var result = await _clotheCategoriesService.CreateClotheCategories(clotheCategory);

            Assert.Equal(clotheCategory.Id, result.Id);
            Assert.Equal(clotheCategory.Name, result.Name);
            Assert.Equal(clotheCategory.GarderobeId, result.GarderobeId);
            Assert.Equal(garderobe.ClothesCategory.Count, 1);
            Assert.Equal(garderobe.ClothesCategory.First().Id, clotheCategory.Id);
        }
    }
}