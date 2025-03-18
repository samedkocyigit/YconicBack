using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Dtos.Ai;
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
    public DbSet<ClotheCategories> ClotheCategories { get; set; }
    public DbSet<Suggestions> Suggestions { get; set; }
    public DbSet<Clothe> Clothes { get; set; }
    public DbSet<ClothePhoto> ClothePhotos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(150);
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.IsActive).HasConversion<string>();
            entity.Property(u => u.Role).HasConversion<string>();
            entity.HasOne(u => u.UserGarderobe)
                  .WithOne(g => g.User)
                  .HasForeignKey<Garderobe>(g => g.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(u => u.UserPersona)
                  .WithOne(g => g.User)
                  .HasForeignKey<Persona>(g => g.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Suggestions)
                  .WithOne(s => s.User)
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        //Configure Persona
        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Usertype)
                  .HasConversion<string>(); 
            entity.HasOne(p => p.User)
                  .WithOne(u => u.UserPersona)
                  .HasForeignKey<Persona>(p => p.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Garderobe
        modelBuilder.Entity<Garderobe>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).HasMaxLength(100);
            entity.HasOne(g => g.User)
                  .WithOne(u => u.UserGarderobe)
                  .HasForeignKey<Garderobe>(g => g.UserId);
            entity.HasMany(g => g.ClothesCategory)
                  .WithOne(c => c.Garderobe)
                  .HasForeignKey(c => c.GarderobeId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure GarderobeCategory
        modelBuilder.Entity<ClotheCategories>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.HasMany(c => c.Clothes)
                  .WithOne(cl => cl.Category)
                  .HasForeignKey(cl => cl.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Clothe
        modelBuilder.Entity<Clothe>(entity =>
        {
            entity.HasKey(cl => cl.Id);
            entity.HasMany(cl => cl.Photos)
                  .WithOne(cp => cp.Clothe)
                  .HasForeignKey(cp => cp.ClotheId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ClothePhoto
        modelBuilder.Entity<ClothePhoto>(entity =>
        {
            entity.HasKey(cp => cp.Id);
            entity.Property(cp => cp.Url).IsRequired();
            entity.HasOne(cp => cp.Clothe)
                  .WithMany(cl => cl.Photos)
                  .HasForeignKey(cp => cp.ClotheId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

}
