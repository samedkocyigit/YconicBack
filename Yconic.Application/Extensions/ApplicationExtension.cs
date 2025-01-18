using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Application.Services.GarderobeCategoriesServices;
using Yconic.Application.Services.GarderobeServices;
using Yconic.Application.Services.SuggestionService;
using Yconic.Application.Services.UserServices;

namespace Yconic.Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGarderobeCategoriesService, GarderobeCategoriesService>();
            services.AddScoped<ISuggestionService, SuggestionService>();
            services.AddScoped<IUserService,UserService>();
            services.AddScoped<IGarderobeService, GarderobeService>();
            return services;
        }
    }
}
