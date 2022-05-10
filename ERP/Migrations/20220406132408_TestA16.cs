using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanViewRecieve",
                table: "UserRoles",
                newName: "CanViewReceive");

            migrationBuilder.RenameColumn(
                name: "CanRecieve",
                table: "UserRoles",
                newName: "CanReceive");

            migrationBuilder.RenameColumn(
                name: "CanCheckRecieve",
                table: "UserRoles",
                newName: "CanCheckReceive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanViewReceive",
                table: "UserRoles",
                newName: "CanViewRecieve");

            migrationBuilder.RenameColumn(
                name: "CanReceive",
                table: "UserRoles",
                newName: "CanRecieve");

            migrationBuilder.RenameColumn(
                name: "CanCheckReceive",
                table: "UserRoles",
                newName: "CanCheckRecieve");
        }
    }
}
