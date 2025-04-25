using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Yconic.Application.Services.AuthServices;
using Yconic.Application.Services.ClothePhotoServices;
using Yconic.Application.Services.ClotheServices;
using Yconic.Application.Services.GarderobeServices;
using Yconic.Application.Services.SuggestionService;
using Yconic.Application.Services.UserServices;
using Yconic.Application.Services.EmailServices;
using Yconic.Application.Services.TokenServices;
using System.Reflection;
using Yconic.Application.Services.AiSuggestionServices;
using Microsoft.Extensions.Configuration;
using Yconic.Application.Services.MigrationServices;
using Yconic.Application.Services.FollowServices;
using Yconic.Application.Services.FollowRequestServices;
using Yconic.Application.Services.SharedLookServices;
using Yconic.Application.Services.SharedLookReviewServices;
using Yconic.Application.Services.SharedLookLikeServices;
using Yconic.Application.Services.ClotheCategoryServices;
using Yconic.Application.Services.PersonaServices;
using Yconic.Application.Services.ClotheCategoryTypeServices;
using Yconic.Infrastructure.Repositories.ClotheCategoryTypeRepositories;

namespace Yconic.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddHttpClient();
            services.AddLogging();

            services.AddScoped<MigrationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClotheCategoryTypeRepository, ClotheCategoryTypeRepository>();
            services.AddScoped<IClotheCategoryService, ClotheCategoryService>();
            services.AddScoped<IGarderobeService, GarderobeService>();
            services.AddScoped<ISuggestionService, SuggestionService>();
            services.AddScoped<ISharedLookService, SharedLookService>();
            services.AddScoped<ISharedLookReviewService, SharedLookReviewService>();
            services.AddScoped<ISharedLookLikeService, SharedLookLikeService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IPersonaService, PersonaService>();
            services.AddScoped<IClotheService, ClotheService>();
            services.AddScoped<IClothePhotoService, ClothePhotoService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IFollowRequestService, FollowRequestService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAiSuggestionService, AiSuggestionService>();

            services.AddSwagger();
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            return services;
        }
    }
}