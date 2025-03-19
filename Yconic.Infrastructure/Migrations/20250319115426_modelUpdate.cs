using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ClotheCategories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "ClotheCategories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "ClotheCategories");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ClotheCategories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
