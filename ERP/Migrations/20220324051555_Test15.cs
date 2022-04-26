using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAsset_BorrowItems_BorrowId_ItemId_EquipmentModelId",
                table: "BorrowItemEquipmentAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                table: "BorrowItemEquipmentAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                table: "TransferItemEquipmentAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemEquipmentAsset_TransferItems_TransferId_ItemId_EquipmentModelId",
                table: "TransferItemEquipmentAsset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItemEquipmentAsset",
                table: "TransferItemEquipmentAsset");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowItemEquipmentAsset",
                table: "BorrowItemEquipmentAsset");

            migrationBuilder.RenameTable(
                name: "TransferItemEquipmentAsset",
                newName: "TransferItemEquipmentAssets");

            migrationBuilder.RenameTable(
                name: "BorrowItemEquipmentAsset",
                newName: "BorrowItemEquipmentAssets");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItemEquipmentAsset_EquipmentAssetId",
                table: "TransferItemEquipmentAssets",
                newName: "IX_TransferItemEquipmentAssets_EquipmentAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowItemEquipmentAsset_EquipmentAssetId",
                table: "BorrowItemEquipmentAssets",
                newName: "IX_BorrowItemEquipmentAssets_EquipmentAssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItemEquipmentAssets",
                table: "TransferItemEquipmentAssets",
                columns: new[] { "TransferId", "ItemId", "EquipmentModelId", "EquipmentAssetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowItemEquipmentAssets",
                table: "BorrowItemEquipmentAssets",
                columns: new[] { "BorrowId", "ItemId", "EquipmentModelId", "EquipmentAssetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_BorrowItems_BorrowId_ItemId_EquipmentModelId",
                table: "BorrowItemEquipmentAssets",
                columns: new[] { "BorrowId", "ItemId", "EquipmentModelId" },
                principalTable: "BorrowItems",
                principalColumns: new[] { "BorrowId", "ItemId", "EquipmentModelId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAssets_EquipmentAssets_EquipmentAssetId",
                table: "BorrowItemEquipmentAssets",
                column: "EquipmentAssetId",
                principalTable: "EquipmentAssets",
                principalColumn: "EquipmentAssetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemEquipmentAssets_EquipmentAssets_EquipmentAssetId",
                table: "TransferItemEquipmentAssets",
                column: "EquipmentAssetId",
                principalTable: "EquipmentAssets",
                principalColumn: "EquipmentAssetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemEquipmentAssets_TransferItems_TransferId_ItemId_EquipmentModelId",
                table: "TransferItemEquipmentAssets",
                columns: new[] { "TransferId", "ItemId", "EquipmentModelId" },
                principalTable: "TransferItems",
                principalColumns: new[] { "TransferId", "ItemId", "EquipmentModelId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_BorrowItems_BorrowId_ItemId_EquipmentModelId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_BorrowItemEquipmentAssets_EquipmentAssets_EquipmentAssetId",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemEquipmentAssets_EquipmentAssets_EquipmentAssetId",
                table: "TransferItemEquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_TransferItemEquipmentAssets_TransferItems_TransferId_ItemId_EquipmentModelId",
                table: "TransferItemEquipmentAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItemEquipmentAssets",
                table: "TransferItemEquipmentAssets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BorrowItemEquipmentAssets",
                table: "BorrowItemEquipmentAssets");

            migrationBuilder.RenameTable(
                name: "TransferItemEquipmentAssets",
                newName: "TransferItemEquipmentAsset");

            migrationBuilder.RenameTable(
                name: "BorrowItemEquipmentAssets",
                newName: "BorrowItemEquipmentAsset");

            migrationBuilder.RenameIndex(
                name: "IX_TransferItemEquipmentAssets_EquipmentAssetId",
                table: "TransferItemEquipmentAsset",
                newName: "IX_TransferItemEquipmentAsset_EquipmentAssetId");

            migrationBuilder.RenameIndex(
                name: "IX_BorrowItemEquipmentAssets_EquipmentAssetId",
                table: "BorrowItemEquipmentAsset",
                newName: "IX_BorrowItemEquipmentAsset_EquipmentAssetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItemEquipmentAsset",
                table: "TransferItemEquipmentAsset",
                columns: new[] { "TransferId", "ItemId", "EquipmentModelId", "EquipmentAssetId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BorrowItemEquipmentAsset",
                table: "BorrowItemEquipmentAsset",
                columns: new[] { "BorrowId", "ItemId", "EquipmentModelId", "EquipmentAssetId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAsset_BorrowItems_BorrowId_ItemId_EquipmentModelId",
                table: "BorrowItemEquipmentAsset",
                columns: new[] { "BorrowId", "ItemId", "EquipmentModelId" },
                principalTable: "BorrowItems",
                principalColumns: new[] { "BorrowId", "ItemId", "EquipmentModelId" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BorrowItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                table: "BorrowItemEquipmentAsset",
                column: "EquipmentAssetId",
                principalTable: "EquipmentAssets",
                principalColumn: "EquipmentAssetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                table: "TransferItemEquipmentAsset",
                column: "EquipmentAssetId",
                principalTable: "EquipmentAssets",
                principalColumn: "EquipmentAssetId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransferItemEquipmentAsset_TransferItems_TransferId_ItemId_EquipmentModelId",
                table: "TransferItemEquipmentAsset",
                columns: new[] { "TransferId", "ItemId", "EquipmentModelId" },
                principalTable: "TransferItems",
                principalColumns: new[] { "TransferId", "ItemId", "EquipmentModelId" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
