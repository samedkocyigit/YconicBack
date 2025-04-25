using Microsoft.Extensions.DependencyInjection;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.SuggestionRepositories;
using Yconic.Infrastructure.Repositories.GenericRepositories;
using Yconic.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Yconic.Infrastructure.Repositories.ClothePhotoRepositories;
using Yconic.Infrastructure.Repositories.ClotheRepositories;
using Yconic.Infrastructure.Repositories.FollowRepositories;
using Yconic.Infrastructure.Repositories.FollowRequestRepositories;
using Yconic.Infrastructure.Repositories.SharedLookRepositories;
using Yconic.Infrastructure.Repositories.SharedLookLikeRepositories;
using Yconic.Infrastructure.Repositories.SharedLookReviewRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Infrastructure.Repositories.ClotheCategoryRepositories;
using Yconic.Infrastructure.Repositories.UserPersonalRepositories;
using Yconic.Infrastructure.Repositories.UserPhysicalRepositories;
using Yconic.Infrastructure.Repositories.UserAccountRepositories;
using Yconic.Infrastructure.Repositories.ClotheCategoryTypeRepositories;
using Yconic.Infrastructure.Repositories.PersonaTypeRopositories;

namespace Yconic.Infrastructure.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=yconic;Username=postgres;Password=samed123"));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserPersonalRepository, UserPersonalRepository>();
            services.AddScoped<IUserPhysicalRepository, UserPhysicalRepository>();
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IPersonaTypeRepository, PersonaTypeRepository>();
            services.AddScoped<IGarderobeRepository, GarderobeRepository>();
            services.AddScoped<IClotheCategoryRepository, ClotheCategoryRepository>();
            services.AddScoped<IClotheCategoryTypeRepository, ClotheCategoryTypeRepository>();
            services.AddScoped<IClotheRepository, ClotheRepository>();
            services.AddScoped<IClothePhotoRepository, ClothePhotoRepository>();
            services.AddScoped<ISuggestionRepository, SuggestionRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IFollowRequestRepository, FollowRequestRepository>();
            services.AddScoped<ISharedLookRepository, SharedLookRepository>();
            services.AddScoped<ISharedLookReviewRepository, SharedLookReviewRepository>();
            services.AddScoped<ISharedLookLikeRepository, SharedLookLikeRepository>();
            return services;
        }
    }
}