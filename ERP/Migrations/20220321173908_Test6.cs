using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_TransferItems_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "TransferItemItemId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "TransferItemTransferId",
                table: "EquipmentAssets");

            migrationBuilder.CreateTable(
                name: "TransferItemEquipmentAsset",
                columns: table => new
                {
                    TransferId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    EquipmentAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransferItemEquipmentAsset", x => new { x.TransferId, x.ItemId, x.EquipmentModelId, x.EquipmentAssetId });
                    table.ForeignKey(
                        name: "FK_TransferItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                        column: x => x.EquipmentAssetId,
                        principalTable: "EquipmentAssets",
                        principalColumn: "EquipmentAssetId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransferItemEquipmentAsset_TransferItems_TransferId_ItemId_EquipmentModelId",
                        columns: x => new { x.TransferId, x.ItemId, x.EquipmentModelId },
                        principalTable: "TransferItems",
                        principalColumns: new[] { "TransferId", "ItemId", "EquipmentModelId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TransferItemEquipmentAsset_EquipmentAssetId",
                table: "TransferItemEquipmentAsset",
                column: "EquipmentAssetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransferItemEquipmentAsset");

            migrationBuilder.AddColumn<int>(
                name: "TransferItemEquipmentModelId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferItemItemId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransferItemTransferId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets",
                columns: new[] { "TransferItemTransferId", "TransferItemItemId", "TransferItemEquipmentModelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_TransferItems_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets",
                columns: new[] { "TransferItemTransferId", "TransferItemItemId", "TransferItemEquipmentModelId" },
                principalTable: "TransferItems",
                principalColumns: new[] { "TransferId", "ItemId", "EquipmentModelId" });
        }
    }
}
