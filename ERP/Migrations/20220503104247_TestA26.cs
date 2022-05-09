using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA26 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CanApproveBulkPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanConfirmBulkPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRequestBulkPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewBulkPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApproveBulkPurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanConfirmBulkPurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestBulkPurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewBulkPurchase",
                table: "UserRoles");
        }
    }
}
