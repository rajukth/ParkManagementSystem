using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkManagementSystem.Infrastructure.Migrations
{
    public partial class userprofileadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileUrl",
                table: "Users");
        }
    }
}
