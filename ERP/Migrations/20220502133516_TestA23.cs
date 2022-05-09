using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseItemSites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItemEmployees",
                table: "PurchaseItemEmployees");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentModelId",
                table: "PurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EquipmentModelId",
                table: "PurchaseItemEmployees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems",
                columns: new[] { "ReceiveId", "ItemId", "EquipmentModelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                columns: new[] { "PurchaseId", "ItemId", "EquipmentModelId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItemEmployees",
                table: "PurchaseItemEmployees",
                columns: new[] { "PurchaseId", "ItemId", "EquipmentModelId", "RequestedById" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItemEmployees",
                table: "PurchaseItemEmployees");

            migrationBuilder.DropColumn(
                name: "EquipmentModelId",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "EquipmentModelId",
                table: "PurchaseItemEmployees");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems",
                columns: new[] { "ReceiveId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                columns: new[] { "PurchaseId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItemEmployees",
                table: "PurchaseItemEmployees",
                columns: new[] { "PurchaseId", "ItemId", "RequestedById" });

            migrationBuilder.CreateTable(
                name: "PurchaseItemSites",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RequestSiteId = table.Column<int>(type: "int", nullable: false),
                    RequestingSiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItemSites", x => new { x.PurchaseId, x.ItemId, x.RequestSiteId });
                    table.ForeignKey(
                        name: "FK_PurchaseItemSites_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItemSites_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItemSites_Sites_RequestingSiteId",
                        column: x => x.RequestingSiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemSites_ItemId",
                table: "PurchaseItemSites",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemSites_RequestingSiteId",
                table: "PurchaseItemSites",
                column: "RequestingSiteId");
        }
    }
}
