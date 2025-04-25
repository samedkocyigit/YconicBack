using Microsoft.EntityFrameworkCore;
using Yconic.Domain.Dtos.Ai;
using Yconic.Domain.Models;
using Yconic.Domain.Models.UserModels;

namespace Yconic.Infrastructure.ApplicationDbContext;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<UserPersonal> UserPersonals { get; set; }
    public DbSet<UserPhysical> UserPhysicals { get; set; }
    public DbSet<Persona> Personas { get; set; }
    public DbSet<PersonaType> PersonaTypes { get; set; }
    public DbSet<Garderobe> Garderobes { get; set; }
    public DbSet<ClotheCategory> ClotheCategories { get; set; }
    public DbSet<ClotheCategoryType> ClotheCategoryTypes { get; set; }
    public DbSet<Clothe> Clothes { get; set; }
    public DbSet<ClothePhoto> ClothePhotos { get; set; }
    public DbSet<Suggestion> Suggestions { get; set; }
    public DbSet<SharedLook> SharedLooks { get; set; }
    public DbSet<SharedLookLike> SharedLookLikes { get; set; }
    public DbSet<SharedLookReview> SharedLookReviews { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<FollowRequest> FollowRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure UserAccount

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(ua => ua.Id);
            entity.Property(ua => ua.PhoneNumber).HasMaxLength(15);
            entity.Property(ua => ua.IsPrivate).HasConversion<string>();
            entity.Property(ua => ua.EmailVerified).HasConversion<string>();
            entity.Property(ua => ua.PhoneVerified).HasConversion<string>();
            entity.HasOne(ua => ua.User)
                  .WithOne(u => u.UserAccount)
                  .HasForeignKey<UserAccount>(ua => ua.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure UserPersonal
        modelBuilder.Entity<UserPersonal>(entity =>
        {
            entity.HasKey(up => up.Id);
            entity.Property(up => up.Name).HasMaxLength(30);
            entity.Property(up => up.Surname).HasMaxLength(30);
            entity.Property(up => up.Bio).HasMaxLength(200);
            entity.Property(up => up.ProfilePhoto).HasMaxLength(200);
            entity.HasOne(up => up.User)
                  .WithOne(u => u.UserPersonal)
                  .HasForeignKey<UserPersonal>(up => up.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure UserPhysical
        modelBuilder.Entity<UserPhysical>(entity =>
        {
            entity.HasKey(up => up.Id);
            entity.Property(up => up.Height).HasColumnType("decimal(5,2)");
            entity.Property(up => up.Weight).HasColumnType("decimal(5,2)");
            entity.Property(up => up.Age).HasColumnType("int");
            entity.Property(up => up.Birthday).HasColumnType("date");
            entity.HasOne(up => up.User)
                  .WithOne(u => u.UserPhysical)
                  .HasForeignKey<UserPhysical>(up => up.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Configure User
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(30);
            entity.HasIndex(u => u.Username).IsUnique();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(40);
            entity.HasIndex(u => u.Email).IsUnique();
            entity.Property(u => u.Password).IsRequired();
            entity.Property(u => u.IsActive).HasConversion<string>();
            entity.Property(u => u.Role).HasConversion<string>();
            entity.HasMany(entity => entity.FollowRequestsSent)
                  .WithOne(fr => fr.Requester)
                  .HasForeignKey(fr => fr.RequesterId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.FollowRequestsReceived)
                  .WithOne(fr => fr.TargetUser)
                  .HasForeignKey(fr => fr.TargetUserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Followers)
                  .WithOne(f => f.Followed)
                  .HasForeignKey(f => f.FollowedId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(u => u.Following)
                    .WithOne(f => f.Follower)
                    .HasForeignKey(f => f.FollowerId)
                    .OnDelete(DeleteBehavior.Cascade);
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

        // Configure Clothe Category
        modelBuilder.Entity<ClotheCategory>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.HasMany(c => c.Clothes)
                  .WithOne(cl => cl.Category)
                  .HasForeignKey(cl => cl.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Clothe Category Type
        modelBuilder.Entity<ClotheCategoryType>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);
            entity.HasMany(c => c.ClothesCategory)
                  .WithOne(cl => cl.ClotheCategoryType)
                  .HasForeignKey(cl => cl.ClotheCategoryTypeId)
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

        // Configure Suggestion
        modelBuilder.Entity<Suggestion>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.HasOne(s => s.User)
                  .WithMany(u => u.Suggestions)
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(s => s.SuggestedLook)
                  .WithMany();
        });

        // Configure SharedLook
        modelBuilder.Entity<SharedLook>(entity =>
        {
            entity.HasKey(sl => sl.Id);
            entity.HasOne(sl => sl.Suggestion)
                    .WithMany()
                    .HasForeignKey(sl => sl.SuggestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(sl => sl.User)
                    .WithMany(u => u.SharedLooks)
                    .HasForeignKey(sl => sl.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(sl => sl.Likes)
                    .WithOne(sll => sll.LikedSharedLook)
                    .HasForeignKey(sll => sll.LikedSharedLookId)
                    .OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(sl => sl.Reviews)
                    .WithOne(slr => slr.ReviewedSharedLook)
                    .HasForeignKey(slr => slr.ReviewedSharedLookId)
                    .OnDelete(DeleteBehavior.Cascade);
        });
    }
}