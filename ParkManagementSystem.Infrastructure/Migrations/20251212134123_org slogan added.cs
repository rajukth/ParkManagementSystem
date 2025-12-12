using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkManagementSystem.Infrastructure.Migrations
{
    public partial class orgsloganadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slogan",
                table: "Organizations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slogan",
                table: "Organizations");
        }
    }
}
