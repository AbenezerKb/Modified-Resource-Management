using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModels_Equipments_EquipmentId",
                table: "EquipmentModels");

            migrationBuilder.RenameColumn(
                name: "EquipmentId",
                table: "EquipmentModels",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentModels_EquipmentId",
                table: "EquipmentModels",
                newName: "IX_EquipmentModels_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModels_Equipments_ItemId",
                table: "EquipmentModels",
                column: "ItemId",
                principalTable: "Equipments",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModels_Equipments_ItemId",
                table: "EquipmentModels");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "EquipmentModels",
                newName: "EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentModels_ItemId",
                table: "EquipmentModels",
                newName: "IX_EquipmentModels_EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModels_Equipments_EquipmentId",
                table: "EquipmentModels",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
