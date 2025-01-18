using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GarderobeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ClothesType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarderobeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Usertype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Garderobes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    Brand = table.Column<string>(type: "text", nullable: false),
                    Material = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<string>(type: "text", nullable: false),
                    Style = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ClothesPhotos = table.Column<string[]>(type: "text[]", nullable: false),
                    ClothesCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    GarderobeCategoriesId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Garderobes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Garderobes_GarderobeCategories_ClothesCategoryId",
                        column: x => x.ClothesCategoryId,
                        principalTable: "GarderobeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Garderobes_GarderobeCategories_GarderobeCategoriesId",
                        column: x => x.GarderobeCategoriesId,
                        principalTable: "GarderobeCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Height = table.Column<decimal>(type: "numeric", nullable: false),
                    Weight = table.Column<decimal>(type: "numeric", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserPersonaId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserGarderobeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Garderobes_UserGarderobeId",
                        column: x => x.UserGarderobeId,
                        principalTable: "Garderobes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Personas_UserPersonaId",
                        column: x => x.UserPersonaId,
                        principalTable: "Personas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Garderobes_ClothesCategoryId",
                table: "Garderobes",
                column: "ClothesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Garderobes_GarderobeCategoriesId",
                table: "Garderobes",
                column: "GarderobeCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserGarderobeId",
                table: "Users",
                column: "UserGarderobeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserPersonaId",
                table: "Users",
                column: "UserPersonaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Garderobes");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "GarderobeCategories");
        }
    }
}
