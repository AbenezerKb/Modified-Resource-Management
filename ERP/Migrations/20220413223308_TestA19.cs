using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Employees_RequestedById",
                table: "PurchaseItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseItems_RequestedById",
                table: "PurchaseItems");

            migrationBuilder.DropColumn(
                name: "RequestedById",
                table: "PurchaseItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                columns: new[] { "PurchaseId", "ItemId" });

            migrationBuilder.CreateTable(
                name: "PurchaseItemEmployees",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    QtyRequested = table.Column<int>(type: "int", nullable: false),
                    RequestRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseItemEmployees", x => new { x.PurchaseId, x.ItemId, x.RequestedById });
                    table.ForeignKey(
                        name: "FK_PurchaseItemEmployees_Employees_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItemEmployees_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseItemEmployees_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemEmployees_ItemId",
                table: "PurchaseItemEmployees",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItemEmployees_RequestedById",
                table: "PurchaseItemEmployees",
                column: "RequestedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseItemEmployees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.AddColumn<int>(
                name: "RequestedById",
                table: "PurchaseItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                columns: new[] { "PurchaseId", "ItemId", "RequestedById" });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseItems_RequestedById",
                table: "PurchaseItems",
                column: "RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Employees_RequestedById",
                table: "PurchaseItems",
                column: "RequestedById",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
