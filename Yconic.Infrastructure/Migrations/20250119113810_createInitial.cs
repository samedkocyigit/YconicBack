﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class createInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Height = table.Column<decimal>(type: "numeric", nullable: true),
                    Weight = table.Column<decimal>(type: "numeric", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserPersonaId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserGarderobeId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garderobes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garderobes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garderobes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Usertype = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personas_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GarderobeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    GarderobeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarderobeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GarderobeCategories_Garderobes_GarderobeId",
                        column: x => x.GarderobeId,
                        principalTable: "Garderobes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clothes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    MainPhoto = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clothes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clothes_GarderobeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "GarderobeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothePhotos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ClotheId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothePhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothePhotos_Clothes_ClotheId",
                        column: x => x.ClotheId,
                        principalTable: "Clothes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothePhotos_ClotheId",
                table: "ClothePhotos",
                column: "ClotheId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CategoryId",
                table: "Clothes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GarderobeCategories_GarderobeId",
                table: "GarderobeCategories",
                column: "GarderobeId");

            migrationBuilder.CreateIndex(
                name: "IX_Garderobes_UserId",
                table: "Garderobes",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_UserId",
                table: "Personas",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothePhotos");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "Suggestions");

            migrationBuilder.DropTable(
                name: "Clothes");

            migrationBuilder.DropTable(
                name: "GarderobeCategories");

            migrationBuilder.DropTable(
                name: "Garderobes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
