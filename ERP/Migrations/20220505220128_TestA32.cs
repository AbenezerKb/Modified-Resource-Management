using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA32 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApproveUser",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "CanDeleteUser",
                table: "UserRoles",
                newName: "CanEditUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanEditUser",
                table: "UserRoles",
                newName: "CanDeleteUser");

            migrationBuilder.AddColumn<bool>(
                name: "CanApproveUser",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
