using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EquipmentAssets");

            migrationBuilder.RenameColumn(
                name: "SiteId",
                table: "EquipmentAssets",
                newName: "CurrentEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentAssets_SiteId",
                table: "EquipmentAssets",
                newName: "IX_EquipmentAssets_CurrentEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_Employees_CurrentEmployeeId",
                table: "EquipmentAssets",
                column: "CurrentEmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_Employees_CurrentEmployeeId",
                table: "EquipmentAssets");

            migrationBuilder.RenameColumn(
                name: "CurrentEmployeeId",
                table: "EquipmentAssets",
                newName: "SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentAssets_CurrentEmployeeId",
                table: "EquipmentAssets",
                newName: "IX_EquipmentAssets_SiteId");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_EmployeeId",
                table: "EquipmentAssets",
                column: "EmployeeId");

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
    }
}
