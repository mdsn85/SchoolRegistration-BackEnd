using Microsoft.EntityFrameworkCore.Migrations;

namespace SchoolRegistration.Migrations
{
    public partial class statusOfUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "AspNetUsers",
                newName: "UserStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserStatus",
                table: "AspNetUsers",
                newName: "Status");
        }
    }
}
