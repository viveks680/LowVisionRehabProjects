using Microsoft.EntityFrameworkCore.Migrations;

namespace LVR_UserDetails.Data.Migrations
{
    public partial class Newcolumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");
        }
    }
}
