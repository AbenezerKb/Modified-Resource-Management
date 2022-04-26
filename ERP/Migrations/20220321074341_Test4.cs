using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAsset_EquipmentModel_EquipmentModelId",
                table: "EquipmentAsset");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModel_Equipments_EquipmentId",
                table: "EquipmentModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentModel",
                table: "EquipmentModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentAsset",
                table: "EquipmentAsset");

            migrationBuilder.DropColumn(
                name: "AssetNo",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "SerialNo",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Equipments");

            migrationBuilder.RenameTable(
                name: "EquipmentModel",
                newName: "EquipmentModels");

            migrationBuilder.RenameTable(
                name: "EquipmentAsset",
                newName: "EquipmentAssets");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentModel_EquipmentId",
                table: "EquipmentModels",
                newName: "IX_EquipmentModels_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentAsset_EquipmentModelId",
                table: "EquipmentAssets",
                newName: "IX_EquipmentAssets_EquipmentModelId");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentModelId",
                table: "TransferItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "EquipmentModels",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems",
                columns: new[] { "TransferId", "ItemId", "EquipmentModelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentModels",
                table: "EquipmentModels",
                column: "EquipmentModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentAssets",
                table: "EquipmentAssets",
                column: "EquipmentAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets",
                columns: new[] { "TransferItemTransferId", "TransferItemItemId", "TransferItemEquipmentModelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_EquipmentModels_EquipmentModelId",
                table: "EquipmentAssets",
                column: "EquipmentModelId",
                principalTable: "EquipmentModels",
                principalColumn: "EquipmentModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_TransferItems_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets",
                columns: new[] { "TransferItemTransferId", "TransferItemItemId", "TransferItemEquipmentModelId" },
                principalTable: "TransferItems",
                principalColumns: new[] { "TransferId", "ItemId", "EquipmentModelId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModels_Equipments_EquipmentId",
                table: "EquipmentModels",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_EquipmentModels_EquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_TransferItems_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentModels_Equipments_EquipmentId",
                table: "EquipmentModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentModels",
                table: "EquipmentModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentAssets",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_TransferItemTransferId_TransferItemItemId_TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "EquipmentModelId",
                table: "TransferItems");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "EquipmentModels");

            migrationBuilder.DropColumn(
                name: "TransferItemEquipmentModelId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "TransferItemItemId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "TransferItemTransferId",
                table: "EquipmentAssets");

            migrationBuilder.RenameTable(
                name: "EquipmentModels",
                newName: "EquipmentModel");

            migrationBuilder.RenameTable(
                name: "EquipmentAssets",
                newName: "EquipmentAsset");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentModels_EquipmentId",
                table: "EquipmentModel",
                newName: "IX_EquipmentModel_EquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentAssets_EquipmentModelId",
                table: "EquipmentAsset",
                newName: "IX_EquipmentAsset_EquipmentModelId");

            migrationBuilder.AddColumn<string>(
                name: "AssetNo",
                table: "TransferItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TransferItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SerialNo",
                table: "TransferItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Equipments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransferItems",
                table: "TransferItems",
                columns: new[] { "TransferId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentModel",
                table: "EquipmentModel",
                column: "EquipmentModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentAsset",
                table: "EquipmentAsset",
                column: "EquipmentAssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAsset_EquipmentModel_EquipmentModelId",
                table: "EquipmentAsset",
                column: "EquipmentModelId",
                principalTable: "EquipmentModel",
                principalColumn: "EquipmentModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentModel_Equipments_EquipmentId",
                table: "EquipmentModel",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
