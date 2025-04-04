using Microsoft.Extensions.DependencyInjection;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Infrastructure.Repositories.SuggestionRepositories;
using Yconic.Infrastructure.Repositories.GenericRepositories;
using Yconic.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Infrastructure.Repositories.ClotheRepositories;
using Yconic.Infrastructure.Repositories.ClotheCategoriesRepositories;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;

namespace Yconic.Infrastructure.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=yconic;Username=postgres;Password=samed123"));
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IClotheCategoriesRepository, ClotheCategoriesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<IGarderobeRepository, GarderobeRepository>();
            services.AddScoped<IClotheRepository, ClotheRepository>();
            services.AddScoped<IClothePhotoRepository , ClothePhotoRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IFollowRequestRepository,FollowRequestRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
