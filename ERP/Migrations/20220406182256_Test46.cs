using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test46 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSiteQties");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedById",
                table: "Receives",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceiveDate",
                table: "Receives",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ReceiveRemark",
                table: "ReceiveItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "EquipmentSiteQties",
                columns: table => new
                {
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentSiteQties", x => new { x.EquipmentModelId, x.SiteId });
                    table.ForeignKey(
                        name: "FK_EquipmentSiteQties_EquipmentModels_EquipmentModelId",
                        column: x => x.EquipmentModelId,
                        principalTable: "EquipmentModels",
                        principalColumn: "EquipmentModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipmentSiteQties_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialSiteQties",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialSiteQties", x => new { x.ItemId, x.SiteId });
                    table.ForeignKey(
                        name: "FK_MaterialSiteQties_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialSiteQties_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentSiteQties_SiteId",
                table: "EquipmentSiteQties",
                column: "SiteId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialSiteQties_SiteId",
                table: "MaterialSiteQties",
                column: "SiteId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropTable(
                name: "EquipmentSiteQties");

            migrationBuilder.DropTable(
                name: "MaterialSiteQties");

            migrationBuilder.DropIndex(
                name: "IX_Receives_ReceivingSiteId",
                table: "Receives");

            migrationBuilder.DropColumn(
                name: "ReceivingSiteId",
                table: "Receives");

            migrationBuilder.DropColumn(
                name: "QtyPurchased",
                table: "ReceiveItems");

            migrationBuilder.AlterColumn<int>(
                name: "ReceivedById",
                table: "Receives",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReceiveDate",
                table: "Receives",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ReceiveRemark",
                table: "ReceiveItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ItemSiteQties",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    SiteId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSiteQties", x => new { x.ItemId, x.SiteId });
                    table.ForeignKey(
                        name: "FK_ItemSiteQties_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSiteQties_Sites_SiteId",
                        column: x => x.SiteId,
                        principalTable: "Sites",
                        principalColumn: "SiteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSiteQties_SiteId",
                table: "ItemSiteQties",
                column: "SiteId");
        }
    }
}
