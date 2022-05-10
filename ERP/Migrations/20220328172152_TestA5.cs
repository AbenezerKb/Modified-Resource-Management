using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Stores_RequestStoreId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Receives_Stores_ReceiveStoreId",
                table: "Receives");

            migrationBuilder.DropIndex(
                name: "IX_Receives_ReceiveStoreId",
                table: "Receives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "ReceiveStoreId",
                table: "Receives");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "ReceivedItems");

            migrationBuilder.RenameColumn(
                name: "CanTransferMaterial",
                table: "UserRoles",
                newName: "CanViewRecieve");

            migrationBuilder.RenameColumn(
                name: "CanTransferEquipment",
                table: "UserRoles",
                newName: "CanViewPurchase");

            migrationBuilder.RenameColumn(
                name: "CanPurchase",
                table: "UserRoles",
                newName: "CanViewMaterialTransfer");

            migrationBuilder.RenameColumn(
                name: "CanMaintainance",
                table: "UserRoles",
                newName: "CanViewMaintainance");

            migrationBuilder.RenameColumn(
                name: "CanIssue",
                table: "UserRoles",
                newName: "CanViewIssue");

            migrationBuilder.RenameColumn(
                name: "CanApproveTransfer",
                table: "UserRoles",
                newName: "CanViewEquipmentTransfer");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "ReceivedItems",
                newName: "ApproveRemark");

            migrationBuilder.RenameColumn(
                name: "Qty",
                table: "ReceivedItems",
                newName: "QtyReceived");

            migrationBuilder.RenameColumn(
                name: "RequestStoreId",
                table: "Purchases",
                newName: "ReceivingSiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_RequestStoreId",
                table: "Purchases",
                newName: "IX_Purchases_ReceivingSiteId");

            migrationBuilder.RenameColumn(
                name: "Remark",
                table: "PurchasedItems",
                newName: "RequestRemark");

            migrationBuilder.AddColumn<int>(
                name: "CanApproveBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApproveEquipmentTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApproveMaterialTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanFixMaintainance",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanHandBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanHandEquipmentTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanHandIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanHandMaterialTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestEquipmentTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestMaintainance",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestMaterialTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanRequestPurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanViewBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PettyCashLimit",
                table: "Sites",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceiveDate",
                table: "Receives",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveredById",
                table: "Receives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ReceiveRemark",
                table: "ReceivedItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPurchaseCost",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "PurchasedItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "QtyApproved",
                table: "PurchasedItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BuyId",
                table: "PurchasedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ApproveRemark",
                table: "PurchasedItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PurchaseRemark",
                table: "PurchasedItems",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QtyPurchased",
                table: "PurchasedItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeSiteId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems",
                columns: new[] { "PurchaseId", "ItemId", "BuyId" });

            migrationBuilder.CreateTable(
                name: "Buys",
                columns: table => new
                {
                    BuyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RequestedById = table.Column<int>(type: "int", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckedById = table.Column<int>(type: "int", nullable: true),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ApprovedById = table.Column<int>(type: "int", nullable: true),
                    BuyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PurchaseId = table.Column<int>(type: "int", nullable: true),
                    TotalBuyCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buys", x => x.BuyId);
                    table.ForeignKey(
                        name: "FK_Buys_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buys_Employees_CheckedById",
                        column: x => x.CheckedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buys_Employees_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Buys_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BuyItems",
                columns: table => new
                {
                    BuyId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    QtyRequested = table.Column<int>(type: "int", nullable: false),
                    QtyApproved = table.Column<int>(type: "int", nullable: false),
                    QtyBought = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RequestRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyRemark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyItems", x => new { x.BuyId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_BuyItems_Buys_BuyId",
                        column: x => x.BuyId,
                        principalTable: "Buys",
                        principalColumn: "BuyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BuyItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedItems_BuyId",
                table: "PurchasedItems",
                column: "BuyId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeSiteId",
                table: "Employees",
                column: "EmployeeSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_BuyItems_ItemId",
                table: "BuyItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Buys_ApprovedById",
                table: "Buys",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Buys_CheckedById",
                table: "Buys",
                column: "CheckedById");

            migrationBuilder.CreateIndex(
                name: "IX_Buys_PurchaseId",
                table: "Buys",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Buys_RequestedById",
                table: "Buys",
                column: "RequestedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Sites_EmployeeSiteId",
                table: "Employees",
                column: "EmployeeSiteId",
                principalTable: "Sites",
                principalColumn: "SiteId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Buys_BuyId",
                table: "PurchasedItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Sites_ReceivingSiteId",
                table: "Purchases",
                column: "ReceivingSiteId",
                principalTable: "Sites",
                principalColumn: "SiteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Sites_EmployeeSiteId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Buys_BuyId",
                table: "PurchasedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Sites_ReceivingSiteId",
                table: "Purchases");

            migrationBuilder.DropTable(
                name: "BuyItems");

            migrationBuilder.DropTable(
                name: "Buys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedItems_BuyId",
                table: "PurchasedItems");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeSiteId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CanApproveBorrow",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanFixMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandBorrow",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandIssue",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestBorrow",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestIssue",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestPurchase",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewBorrow",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "PettyCashLimit",
                table: "Sites");

            migrationBuilder.DropColumn(
                name: "ReceiveRemark",
                table: "ReceivedItems");

            migrationBuilder.DropColumn(
                name: "TotalPurchaseCost",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BuyId",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "ApproveRemark",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "PurchaseRemark",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "QtyPurchased",
                table: "PurchasedItems");

            migrationBuilder.DropColumn(
                name: "EmployeeSiteId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "CanViewRecieve",
                table: "UserRoles",
                newName: "CanTransferMaterial");

            migrationBuilder.RenameColumn(
                name: "CanViewPurchase",
                table: "UserRoles",
                newName: "CanTransferEquipment");

            migrationBuilder.RenameColumn(
                name: "CanViewMaterialTransfer",
                table: "UserRoles",
                newName: "CanPurchase");

            migrationBuilder.RenameColumn(
                name: "CanViewMaintainance",
                table: "UserRoles",
                newName: "CanMaintainance");

            migrationBuilder.RenameColumn(
                name: "CanViewIssue",
                table: "UserRoles",
                newName: "CanIssue");

            migrationBuilder.RenameColumn(
                name: "CanViewEquipmentTransfer",
                table: "UserRoles",
                newName: "CanApproveTransfer");

            migrationBuilder.RenameColumn(
                name: "QtyReceived",
                table: "ReceivedItems",
                newName: "Qty");

            migrationBuilder.RenameColumn(
                name: "ApproveRemark",
                table: "ReceivedItems",
                newName: "Remark");

            migrationBuilder.RenameColumn(
                name: "ReceivingSiteId",
                table: "Purchases",
                newName: "RequestStoreId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ReceivingSiteId",
                table: "Purchases",
                newName: "IX_Purchases_RequestStoreId");

            migrationBuilder.RenameColumn(
                name: "RequestRemark",
                table: "PurchasedItems",
                newName: "Remark");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceiveDate",
                table: "Receives",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveredById",
                table: "Receives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReceiveStoreId",
                table: "Receives",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "ReceivedItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalCost",
                table: "PurchasedItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "QtyApproved",
                table: "PurchasedItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems",
                columns: new[] { "PurchaseId", "ItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Receives_ReceiveStoreId",
                table: "Receives",
                column: "ReceiveStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Stores_RequestStoreId",
                table: "Purchases",
                column: "RequestStoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Receives_Stores_ReceiveStoreId",
                table: "Receives",
                column: "ReceiveStoreId",
                principalTable: "Stores",
                principalColumn: "StoreId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
