using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FileRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionsId",
                table: "Clothes");

            migrationBuilder.RenameColumn(
                name: "SuggestionsId",
                table: "Clothes",
                newName: "SuggestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Clothes_SuggestionsId",
                table: "Clothes",
                newName: "IX_Clothes_SuggestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionId",
                table: "Clothes",
                column: "SuggestionId",
                principalTable: "Suggestions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionId",
                table: "Clothes");

            migrationBuilder.RenameColumn(
                name: "SuggestionId",
                table: "Clothes",
                newName: "SuggestionsId");

            migrationBuilder.RenameIndex(
                name: "IX_Clothes_SuggestionId",
                table: "Clothes",
                newName: "IX_Clothes_SuggestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionsId",
                table: "Clothes",
                column: "SuggestionsId",
                principalTable: "Suggestions",
                principalColumn: "Id");
        }
    }
}
