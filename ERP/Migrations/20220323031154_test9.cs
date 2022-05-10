using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class test9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentSiteId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_CurrentSiteId",
                table: "EquipmentAssets",
                column: "CurrentSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_Sites_CurrentSiteId",
                table: "EquipmentAssets",
                column: "CurrentSiteId",
                principalTable: "Sites",
                principalColumn: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_Sites_CurrentSiteId",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_CurrentSiteId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "CurrentSiteId",
                table: "EquipmentAssets");
        }
    }
}
