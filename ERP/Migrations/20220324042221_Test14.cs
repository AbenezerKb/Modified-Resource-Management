using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Borrows",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HandDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    HandedById = table.Column<int>(type: "int", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Borrows", x => x.BorrowId);
                    table.ForeignKey(
                        name: "FK_Borrows_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrows_Employees_HandedById",
                        column: x => x.HandedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrows_Employees_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Borrows_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowItems",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    QtyRequested = table.Column<int>(type: "int", nullable: false),
                    QtyApproved = table.Column<int>(type: "int", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApproveRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowItems", x => new { x.BorrowId, x.ItemId, x.EquipmentModelId });
                    table.ForeignKey(
                        name: "FK_BorrowItems_Borrows_BorrowId",
                        column: x => x.BorrowId,
                        principalTable: "Borrows",
                        principalColumn: "BorrowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BorrowItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BorrowItemEquipmentAsset",
                columns: table => new
                {
                    BorrowId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    EquipmentAssetId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowItemEquipmentAsset", x => new { x.BorrowId, x.ItemId, x.EquipmentModelId, x.EquipmentAssetId });
                    table.ForeignKey(
                        name: "FK_BorrowItemEquipmentAsset_BorrowItems_BorrowId_ItemId_EquipmentModelId",
                        columns: x => new { x.BorrowId, x.ItemId, x.EquipmentModelId },
                        principalTable: "BorrowItems",
                        principalColumns: new[] { "BorrowId", "ItemId", "EquipmentModelId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowItemEquipmentAsset_EquipmentAssets_EquipmentAssetId",
                        column: x => x.EquipmentAssetId,
                        principalTable: "EquipmentAssets",
                        principalColumn: "EquipmentAssetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItemEquipmentAsset_EquipmentAssetId",
                table: "BorrowItemEquipmentAsset",
                column: "EquipmentAssetId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowItems_ItemId",
                table: "BorrowItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_ApprovedById",
                table: "Borrows",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_HandedById",
                table: "Borrows",
                column: "HandedById");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_RequestedById",
                table: "Borrows",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_Borrows_SiteId",
                table: "Borrows",
                column: "SiteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowItemEquipmentAsset");

            migrationBuilder.DropTable(
                name: "BorrowItems");

            migrationBuilder.DropTable(
                name: "Borrows");
        }
    }
}
