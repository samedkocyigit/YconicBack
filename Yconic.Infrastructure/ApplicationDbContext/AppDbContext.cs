using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Models;

namespace Yconic.Infrastructure.ApplicationDbContext;
public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Garderobe> Garderobes { get; set; }
    public DbSet<GarderobeCategories> GarderobeCategories { get; set; }
    public DbSet<Suggestions> Suggestions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.IsActive)
            .HasConversion<string>();
        modelBuilder.Entity<Persona>()
            .Property(x => x.Usertype)
            .HasConversion<string>();

        modelBuilder.Entity<User>()
            .HasOne(x => x.UserPersona)
            .WithMany()
            .HasForeignKey(x => x.UserPersonaId);
        
        modelBuilder.Entity<User>()
            .HasOne(x => x.UserGarderobe)
            .WithMany()
            .HasForeignKey(x => x.UserGarderobeId);
        
        modelBuilder.Entity<Garderobe>()
            .HasOne(x => x.ClothesCategory)
            .WithMany()
            .HasForeignKey(x => x.ClothesCategoryId);

        base.OnModelCreating(modelBuilder);
    }
}
