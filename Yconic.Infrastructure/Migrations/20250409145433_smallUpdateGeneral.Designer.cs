﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Yconic.Infrastructure.ApplicationDbContext;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250409145433_smallUpdateGeneral")]
    partial class smallUpdateGeneral
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Yconic.Domain.Models.Clothe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("MainPhoto")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid?>("SuggestionId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SuggestionId");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("Yconic.Domain.Models.ClotheCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GarderobeId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("GarderobeId");

                    b.ToTable("ClotheCategories");
                });

            modelBuilder.Entity("Yconic.Domain.Models.ClothePhoto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClotheId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClotheId");

                    b.ToTable("ClothePhotos");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Follow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("FollowedId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FollowerId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsFollowing")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FollowedId");

                    b.HasIndex("FollowerId");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("Yconic.Domain.Models.FollowRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("RequestStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RequestedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("RequesterId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("TargetUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RequesterId");

                    b.HasIndex("TargetUserId");

                    b.ToTable("FollowRequests");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Garderobe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Garderobes");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Persona", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("Usertype")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Suggestion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Suggestions");
                });

            modelBuilder.Entity("Yconic.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("character varying(40)");

                    b.Property<decimal?>("Height")
                        .HasColumnType("numeric");

                    b.Property<string>("IsActive")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("PasswordResetTokenExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("ProfilePhoto")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserGarderobeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserPersonaId")
                        .HasColumnType("uuid");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Clothe", b =>
                {
                    b.HasOne("Yconic.Domain.Models.ClotheCategory", "Category")
                        .WithMany("Clothes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yconic.Domain.Models.Suggestion", null)
                        .WithMany("SuggestedLook")
                        .HasForeignKey("SuggestionId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Yconic.Domain.Models.ClotheCategory", b =>
                {
                    b.HasOne("Yconic.Domain.Models.Garderobe", "Garderobe")
                        .WithMany("ClothesCategory")
                        .HasForeignKey("GarderobeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Garderobe");
                });

            modelBuilder.Entity("Yconic.Domain.Models.ClothePhoto", b =>
                {
                    b.HasOne("Yconic.Domain.Models.Clothe", "Clothe")
                        .WithMany("Photos")
                        .HasForeignKey("ClotheId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothe");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Follow", b =>
                {
                    b.HasOne("Yconic.Domain.Models.User", "Followed")
                        .WithMany("Followers")
                        .HasForeignKey("FollowedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yconic.Domain.Models.User", "Follower")
                        .WithMany("Following")
                        .HasForeignKey("FollowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Followed");

                    b.Navigation("Follower");
                });

            modelBuilder.Entity("Yconic.Domain.Models.FollowRequest", b =>
                {
                    b.HasOne("Yconic.Domain.Models.User", "Requester")
                        .WithMany("FollowRequestsSent")
                        .HasForeignKey("RequesterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Yconic.Domain.Models.User", "TargetUser")
                        .WithMany("FollowRequestsReceived")
                        .HasForeignKey("TargetUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Requester");

                    b.Navigation("TargetUser");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Garderobe", b =>
                {
                    b.HasOne("Yconic.Domain.Models.User", "User")
                        .WithOne("UserGarderobe")
                        .HasForeignKey("Yconic.Domain.Models.Garderobe", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Persona", b =>
                {
                    b.HasOne("Yconic.Domain.Models.User", "User")
                        .WithOne("UserPersona")
                        .HasForeignKey("Yconic.Domain.Models.Persona", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Suggestion", b =>
                {
                    b.HasOne("Yconic.Domain.Models.User", "User")
                        .WithMany("Suggestions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Clothe", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Yconic.Domain.Models.ClotheCategory", b =>
                {
                    b.Navigation("Clothes");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Garderobe", b =>
                {
                    b.Navigation("ClothesCategory");
                });

            modelBuilder.Entity("Yconic.Domain.Models.Suggestion", b =>
                {
                    b.Navigation("SuggestedLook");
                });

            modelBuilder.Entity("Yconic.Domain.Models.User", b =>
                {
                    b.Navigation("FollowRequestsReceived");

                    b.Navigation("FollowRequestsSent");

                    b.Navigation("Followers");

                    b.Navigation("Following");

                    b.Navigation("Suggestions");

                    b.Navigation("UserGarderobe");

                    b.Navigation("UserPersona");
                });
#pragma warning restore 612, 618
        }
    }
}
