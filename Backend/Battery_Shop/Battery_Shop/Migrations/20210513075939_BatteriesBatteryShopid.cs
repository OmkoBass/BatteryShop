using Microsoft.EntityFrameworkCore.Migrations;

namespace Battery_Shop.Migrations
{
    public partial class BatteriesBatteryShopid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BatteryShopId",
                table: "Batteries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Batteries_BatteryShopId",
                table: "Batteries",
                column: "BatteryShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batteries_BatteryShops_BatteryShopId",
                table: "Batteries",
                column: "BatteryShopId",
                principalTable: "BatteryShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batteries_BatteryShops_BatteryShopId",
                table: "Batteries");

            migrationBuilder.DropIndex(
                name: "IX_Batteries_BatteryShopId",
                table: "Batteries");

            migrationBuilder.DropColumn(
                name: "BatteryShopId",
                table: "Batteries");
        }
    }
}
