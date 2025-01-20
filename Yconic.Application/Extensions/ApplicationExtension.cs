using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Yconic.Application.Services.AuthServices;
using Yconic.Application.Services.ClothePhotoServices;
using Yconic.Application.Services.ClotheServices;
using Yconic.Application.Services.ClotheCategoriesServices;
using Yconic.Application.Services.GarderobeServices;
using Yconic.Application.Services.PersonasServices;
using Yconic.Application.Services.SuggestionService;
using Yconic.Application.Services.UserServices;
using Yconic.Application.Services.EmailServices;
using Yconic.Application.Services.TokenServices;

namespace Yconic.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClotheCategoriesService, ClotheCategoriesService>();
            services.AddScoped<IGarderobeService, GarderobeService>();
            services.AddScoped<ISuggestionService, SuggestionService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IPersonasService, PersonasService>();
            services.AddScoped<IClotheService, ClotheService>();
            services.AddScoped<IClothePhotoService, ClothePhotoService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITokenService, TokenService>();


            services.AddSwagger();
            return services;
        }
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Bearer Token Authentication için Security tanımı ekle
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Bearer Token. Örn: 'Bearer {token}'"
                });
            });
            return services;
        }
    }
}