using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QtyPurchased",
                table: "ReceiveItems");

            migrationBuilder.AddColumn<int>(
                name: "BulkPurchaseId",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BulkPurchases",
                columns: table => new
                {
                    BulkPurchaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    TotalPurchaseCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkPurchases", x => x.BulkPurchaseId);
                    table.ForeignKey(
                        name: "FK_BulkPurchases_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BulkPurchases_Employees_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateTable(
                name: "BulkPurchaseItems",
                columns: table => new
                {
                    BulkPurchaseId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    QtyRequested = table.Column<int>(type: "int", nullable: false),
                    QtyApproved = table.Column<int>(type: "int", nullable: false),
                    QtyPurchased = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BulkPurchaseItems", x => new { x.BulkPurchaseId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_BulkPurchaseItems_BulkPurchases_BulkPurchaseId",
                        column: x => x.BulkPurchaseId,
                        principalTable: "BulkPurchases",
                        principalColumn: "BulkPurchaseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BulkPurchaseItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BulkPurchaseId",
                table: "Purchases",
                column: "BulkPurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_BulkPurchaseItems_ItemId",
                table: "BulkPurchaseItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BulkPurchases_ApprovedById",
                table: "BulkPurchases",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_BulkPurchases_RequestedById",
                table: "BulkPurchases",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemSites_ItemId",
                table: "PurchaseItemSites",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemSites_RequestingSiteId",
                table: "PurchaseItemSites",
                column: "RequestingSiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_BulkPurchases_BulkPurchaseId",
                table: "Purchases",
                column: "BulkPurchaseId",
                principalTable: "BulkPurchases",
                principalColumn: "BulkPurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_BulkPurchases_BulkPurchaseId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "BulkPurchaseItems");

            migrationBuilder.DropTable(
                name: "PurchaseItemSites");

            migrationBuilder.DropTable(
                name: "BulkPurchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_BulkPurchaseId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BulkPurchaseId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "QtyPurchased",
                table: "ReceiveItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
