using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Yconic.Infrastructure.ApplicationDbContext;

namespace Yconic.Application.Services.MigrationServices
{
    public class MigrationService{
        public static void InitializeMigration(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.GetService<AppDbContext>()!.Database.Migrate();
        }
    }
}