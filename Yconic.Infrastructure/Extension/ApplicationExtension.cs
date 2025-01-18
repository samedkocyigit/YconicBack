using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yconic.Infrastructure.Repositories.GarderobeCategoriesRepositories;
using Yconic.Infrastructure.Repositories.GarderobeRepositories;
using Yconic.Infrastructure.Repositories.PersonaRepositories;
using Yconic.Infrastructure.Repositories.UserRepositories;
using Yconic.Infrastructure.Repositories.GenericRepositories;
using Yconic.Infrastructure.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Yconic.Infrastructure.Extension
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseNpgsql("Host=localhost;Port=5432;Database=yconic;Username=postgres;Password=samed123"));
            services.AddScoped<IPersonaRepository, PersonaRepository>();
            services.AddScoped<IGarderobeCategoriesRepository, GarderobeCategoriesRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGarderobeRepository, GarderobeRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
