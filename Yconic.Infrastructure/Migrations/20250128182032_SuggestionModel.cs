using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SuggestionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Suggestions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Suggestions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Suggestions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Suggestions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SuggestionsId",
                table: "Clothes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_SuggestionsId",
                table: "Clothes",
                column: "SuggestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionsId",
                table: "Clothes",
                column: "SuggestionsId",
                principalTable: "Suggestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suggestions_Users_UserId",
                table: "Suggestions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Suggestions_SuggestionsId",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_Suggestions_Users_UserId",
                table: "Suggestions");

            migrationBuilder.DropIndex(
                name: "IX_Suggestions_UserId",
                table: "Suggestions");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_SuggestionsId",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "SuggestionsId",
                table: "Clothes");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Suggestions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Suggestions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
