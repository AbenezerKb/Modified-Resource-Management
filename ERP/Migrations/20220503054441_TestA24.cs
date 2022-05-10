using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BulkPurchaseItems",
                table: "BulkPurchaseItems");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentModelId",
                table: "BulkPurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BulkPurchaseItems",
                table: "BulkPurchaseItems",
                columns: new[] { "BulkPurchaseId", "ItemId", "EquipmentModelId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BulkPurchaseItems",
                table: "BulkPurchaseItems");

            migrationBuilder.DropColumn(
                name: "EquipmentModelId",
                table: "BulkPurchaseItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BulkPurchaseItems",
                table: "BulkPurchaseItems",
                columns: new[] { "BulkPurchaseId", "ItemId" });
        }
    }
}
