using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_UserRoles_UserRoleId",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_UserRoleId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "UserAccounts");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserRoleId",
                table: "Employees",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_UserRoles_UserRoleId",
                table: "Employees",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_UserRoles_UserRoleId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_UserRoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleId",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_UserRoleId",
                table: "UserAccounts",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_UserRoles_UserRoleId",
                table: "UserAccounts",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
