using Microsoft.EntityFrameworkCore.Migrations;

namespace Battery_Shop.Migrations
{
    public partial class BatteryHasStorageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batteries_Storages_StorageId",
                table: "Batteries");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Batteries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Batteries_Storages_StorageId",
                table: "Batteries",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batteries_Storages_StorageId",
                table: "Batteries");

            migrationBuilder.AlterColumn<int>(
                name: "StorageId",
                table: "Batteries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Batteries_Storages_StorageId",
                table: "Batteries",
                column: "StorageId",
                principalTable: "Storages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
