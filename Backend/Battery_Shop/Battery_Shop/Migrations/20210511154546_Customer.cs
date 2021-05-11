using Microsoft.EntityFrameworkCore.Migrations;

namespace Battery_Shop.Migrations
{
    public partial class Customer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Batteries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    BatteryShopId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_BatteryShops_BatteryShopId",
                        column: x => x.BatteryShopId,
                        principalTable: "BatteryShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Batteries_CustomerId",
                table: "Batteries",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_BatteryShopId",
                table: "Customers",
                column: "BatteryShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Batteries_Customers_CustomerId",
                table: "Batteries",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Batteries_Customers_CustomerId",
                table: "Batteries");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Batteries_CustomerId",
                table: "Batteries");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Batteries");
        }
    }
}
