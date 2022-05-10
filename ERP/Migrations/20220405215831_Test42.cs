using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.Migrations
{
    public partial class Test42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanApproveEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanFixMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanHandMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewEquipmentTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewMaintainance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewMaterialTransfer",
                table: "UserRoles");

            migrationBuilder.AlterColumn<bool>(
                name: "CanViewRecieve",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanViewPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanViewIssue",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanViewBuy",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanViewBorrow",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanReturnBorrow",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanRequestPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanRequestIssue",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanRequestBuy",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanRequestBorrow",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanRecieve",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanHandIssue",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanHandBorrow",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanCheckRecieve",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanCheckPurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanCheckBuy",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanApprovePurchase",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanApproveIssue",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanApproveBuy",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "CanApproveBorrow",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "CanApproveMaintenance",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanApproveTransfer",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanFixMaintenance",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanReceiveTransfer",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRequestMaintenance",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRequestTransfer",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanSendTransfer",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewMaintenance",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanViewTransfer",
                table: "UserRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "AssetDamageId",
                table: "EquipmentAssets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentAssets_AssetDamageId",
                table: "EquipmentAssets",
                column: "AssetDamageId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentAssets_AssetDamages_AssetDamageId",
                table: "EquipmentAssets",
                column: "AssetDamageId",
                principalTable: "AssetDamages",
                principalColumn: "AssetDamageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentAssets_AssetDamages_AssetDamageId",
                table: "EquipmentAssets");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentAssets_AssetDamageId",
                table: "EquipmentAssets");

            migrationBuilder.DropColumn(
                name: "CanApproveMaintenance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanApproveTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanFixMaintenance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanReceiveTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestMaintenance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanRequestTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanSendTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewMaintenance",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "CanViewTransfer",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "AssetDamageId",
                table: "EquipmentAssets");

            migrationBuilder.AlterColumn<int>(
                name: "CanViewRecieve",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanViewPurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanViewIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanViewBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanViewBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanReturnBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanRequestPurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanRequestIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanRequestBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanRequestBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanRecieve",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanHandIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanHandBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanCheckRecieve",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanCheckPurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanCheckBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanApprovePurchase",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanApproveIssue",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanApproveBuy",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "CanApproveBorrow",
                table: "UserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "CanApproveEquipmentTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanApproveMaintainance",
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
                name: "CanHandEquipmentTransfer",
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
                name: "CanRequestEquipmentTransfer",
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
                name: "CanViewEquipmentTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanViewMaintainance",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CanViewMaterialTransfer",
                table: "UserRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
