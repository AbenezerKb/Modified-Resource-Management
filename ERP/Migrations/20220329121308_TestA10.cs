using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CanApproveBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanCheckBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanReturnBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanViewBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApproveBuy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanCheckBuy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestBuy",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanReturnBorrow",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewBuy",
                table: "UserRoles");
        }
    }
}
