using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class TestA6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Buys_BuyId",
                table: "PurchasedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Items_ItemId",
                table: "PurchasedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedItems_Purchases_PurchaseId",
                table: "PurchasedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivedItems_Items_ItemId",
                table: "ReceivedItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceivedItems_Receives_ReceiveId",
                table: "ReceivedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceivedItems",
                table: "ReceivedItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems");

            migrationBuilder.RenameTable(
                name: "ReceivedItems",
                newName: "ReceiveItems");

            migrationBuilder.RenameTable(
                name: "PurchasedItems",
                newName: "PurchaseItems");

            migrationBuilder.RenameIndex(
                name: "IX_ReceivedItems_ItemId",
                table: "ReceiveItems",
                newName: "IX_ReceiveItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItems_ItemId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchasedItems_BuyId",
                table: "PurchaseItems",
                newName: "IX_PurchaseItems_BuyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems",
                columns: new[] { "ReceiveId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems",
                columns: new[] { "PurchaseId", "ItemId", "BuyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Items_ItemId",
                table: "PurchaseItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId",
                table: "PurchaseItems",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveItems_Items_ItemId",
                table: "ReceiveItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceiveItems_Receives_ReceiveId",
                table: "ReceiveItems",
                column: "ReceiveId",
                principalTable: "Receives",
                principalColumn: "ReceiveId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Buys_BuyId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Items_ItemId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseItems_Purchases_PurchaseId",
                table: "PurchaseItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveItems_Items_ItemId",
                table: "ReceiveItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceiveItems_Receives_ReceiveId",
                table: "ReceiveItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceiveItems",
                table: "ReceiveItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PurchaseItems",
                table: "PurchaseItems");

            migrationBuilder.RenameTable(
                name: "ReceiveItems",
                newName: "ReceivedItems");

            migrationBuilder.RenameTable(
                name: "PurchaseItems",
                newName: "PurchasedItems");

            migrationBuilder.RenameIndex(
                name: "IX_ReceiveItems_ItemId",
                table: "ReceivedItems",
                newName: "IX_ReceivedItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_ItemId",
                table: "PurchasedItems",
                newName: "IX_PurchasedItems_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PurchaseItems_BuyId",
                table: "PurchasedItems",
                newName: "IX_PurchasedItems_BuyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceivedItems",
                table: "ReceivedItems",
                columns: new[] { "ReceiveId", "ItemId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_PurchasedItems",
                table: "PurchasedItems",
                columns: new[] { "PurchaseId", "ItemId", "BuyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Buys_BuyId",
                table: "PurchasedItems",
                column: "BuyId",
                principalTable: "Buys",
                principalColumn: "BuyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Items_ItemId",
                table: "PurchasedItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedItems_Purchases_PurchaseId",
                table: "PurchasedItems",
                column: "PurchaseId",
                principalTable: "Purchases",
                principalColumn: "PurchaseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivedItems_Items_ItemId",
                table: "ReceivedItems",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "ItemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceivedItems_Receives_ReceiveId",
                table: "ReceivedItems",
                column: "ReceiveId",
                principalTable: "Receives",
                principalColumn: "ReceiveId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
