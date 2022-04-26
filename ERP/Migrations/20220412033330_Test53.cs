using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "EquipmentSiteQties",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentSiteQties_ItemId",
                table: "EquipmentSiteQties",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentSiteQties_Items_ItemId",
                table: "EquipmentSiteQties",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentSiteQties_Items_ItemId",
                table: "EquipmentSiteQties");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentSiteQties_ItemId",
                table: "EquipmentSiteQties");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "EquipmentSiteQties");
        }
    }
}
