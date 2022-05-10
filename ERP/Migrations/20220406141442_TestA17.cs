using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA17 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CanCheckReceive",
                table: "UserRoles",
                newName: "CanConfirmPurchase");

            migrationBuilder.AddColumn<bool>(
                name: "CanApproveReceive",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanConfirmBuy",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApproveReceive",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanConfirmBuy",
                table: "UserRoles");

            migrationBuilder.RenameColumn(
                name: "CanConfirmPurchase",
                table: "UserRoles",
                newName: "CanCheckReceive");
        }
    }
}
