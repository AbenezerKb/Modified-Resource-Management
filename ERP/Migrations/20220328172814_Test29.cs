using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test29 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Damage",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.AddColumn<int>(
                name: "AssetDamageId",
                table: "BorrowItemEquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AssetDamages",
                columns: table => new
                {
                    AssetDamageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false),
                    PenalityPercentage = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDamages", x => x.AssetDamageId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItemEquipmentAssets_AssetDamageId",
                table: "BorrowItemEquipmentAssets",
                column: "AssetDamageId");

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_AssetDamages_AssetDamageId",
                table: "BorrowItemEquipmentAssets",
                column: "AssetDamageId",
                principalTable: "AssetDamages",
                principalColumn: "AssetDamageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_AssetDamages_AssetDamageId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropTable(
                name: "AssetDamages");

            migrationBuilder.DropIndex(
                name: "IX_BorrowItemEquipmentAssets_AssetDamageId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropColumn(
                name: "AssetDamageId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.AddColumn<string>(
                name: "Damage",
                table: "BorrowItemEquipmentAssets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
