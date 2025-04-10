using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Yconic.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FollowRequestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "FollowRequests");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "FollowRequests");

            migrationBuilder.AddColumn<int>(
                name: "RequestStatus",
                table: "FollowRequests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestStatus",
                table: "FollowRequests");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "FollowRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "FollowRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
