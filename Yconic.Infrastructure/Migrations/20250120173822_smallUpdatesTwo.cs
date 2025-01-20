using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class smallUpdatesTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_GarderobeCategories_CategoryId",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_GarderobeCategories_Garderobes_GarderobeId",
                table: "GarderobeCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GarderobeCategories",
                table: "GarderobeCategories");

            migrationBuilder.RenameTable(
                name: "GarderobeCategories",
                newName: "ClotheCategories");

            migrationBuilder.RenameIndex(
                name: "IX_GarderobeCategories_GarderobeId",
                table: "ClotheCategories",
                newName: "IX_ClotheCategories_GarderobeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClotheCategories",
                table: "ClotheCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClotheCategories_Garderobes_GarderobeId",
                table: "ClotheCategories",
                column: "GarderobeId",
                principalTable: "Garderobes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_ClotheCategories_CategoryId",
                table: "Clothes",
                column: "CategoryId",
                principalTable: "ClotheCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClotheCategories_Garderobes_GarderobeId",
                table: "ClotheCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_ClotheCategories_CategoryId",
                table: "Clothes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClotheCategories",
                table: "ClotheCategories");

            migrationBuilder.RenameTable(
                name: "ClotheCategories",
                newName: "GarderobeCategories");

            migrationBuilder.RenameIndex(
                name: "IX_ClotheCategories_GarderobeId",
                table: "GarderobeCategories",
                newName: "IX_GarderobeCategories_GarderobeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GarderobeCategories",
                table: "GarderobeCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_GarderobeCategories_CategoryId",
                table: "Clothes",
                column: "CategoryId",
                principalTable: "GarderobeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GarderobeCategories_Garderobes_GarderobeId",
                table: "GarderobeCategories",
                column: "GarderobeId",
                principalTable: "Garderobes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
