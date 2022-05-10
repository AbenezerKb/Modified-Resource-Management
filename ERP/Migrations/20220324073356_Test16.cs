using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EquipmentAssetId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentModelId",
                table: "Maintenances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipmentAssetId",
                table: "Maintenances",
                column: "EquipmentAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenances_EquipmentModelId",
                table: "Maintenances",
                column: "EquipmentModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_EquipmentAssets_EquipmentAssetId",
                table: "Maintenances",
                column: "EquipmentAssetId",
                principalTable: "EquipmentAssets",
                principalColumn: "EquipmentAssetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Maintenances_EquipmentModels_EquipmentModelId",
                table: "Maintenances",
                column: "EquipmentModelId",
                principalTable: "EquipmentModels",
                principalColumn: "EquipmentModelId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_EquipmentAssets_EquipmentAssetId",
                table: "Maintenances");

            migrationBuilder.DropForeignKey(
                name: "FK_Maintenances_EquipmentModels_EquipmentModelId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_EquipmentAssetId",
                table: "Maintenances");

            migrationBuilder.DropIndex(
                name: "IX_Maintenances_EquipmentModelId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "EquipmentAssetId",
                table: "Maintenances");

            migrationBuilder.DropColumn(
                name: "EquipmentModelId",
                table: "Maintenances");
        }
    }
}
