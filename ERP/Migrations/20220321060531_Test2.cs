using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Model",
                table: "Equipments");

            migrationBuilder.CreateTable(
                name: "EquipmentModel",
                columns: table => new
                {
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentModel", x => x.EquipmentModelId);
                    table.ForeignKey(
                        name: "FK_EquipmentModel_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentAsset",
                columns: table => new
                {
                    EquipmentAssetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EquipmentModelId = table.Column<int>(type: "int", nullable: false),
                    AssetNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentAsset", x => x.EquipmentAssetId);
                    table.ForeignKey(
                        name: "FK_EquipmentAsset_EquipmentModel_EquipmentModelId",
                        column: x => x.EquipmentModelId,
                        principalTable: "EquipmentModel",
                        principalColumn: "EquipmentModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAsset_EquipmentModelId",
                table: "EquipmentAsset",
                column: "EquipmentModelId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentModel_EquipmentId",
                table: "EquipmentModel",
                column: "EquipmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipmentAsset");

            migrationBuilder.DropTable(
                name: "EquipmentModel");

            migrationBuilder.AddColumn<int>(
                name: "Model",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
