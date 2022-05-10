using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_EmployeeId",
                table: "EquipmentAssets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_SiteId",
                table: "EquipmentAssets",
                column: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_Employees_EmployeeId",
                table: "EquipmentAssets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_Sites_SiteId",
                table: "EquipmentAssets",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_Employees_EmployeeId",
                table: "EquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_Sites_SiteId",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_EmployeeId",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_SiteId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "EquipmentAssets");
        }
    }
}
