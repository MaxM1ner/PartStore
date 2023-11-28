using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class PcBuilderMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_CustomerOrders_CustomerOrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CustomerOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CustomerOrderId",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "ProductTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CustomerOrderProduct",
                columns: table => new
                {
                    OrderProductsId = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderProduct", x => new { x.OrderProductsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_CustomerOrderProduct_CustomerOrders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "CustomerOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrderProduct_Products_OrderProductsId",
                        column: x => x.OrderProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcBuilds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PreBuild = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcBuilds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PcBuilds_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PcBuildProduct",
                columns: table => new
                {
                    BuildProductsId = table.Column<int>(type: "int", nullable: false),
                    BuildsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PcBuildProduct", x => new { x.BuildProductsId, x.BuildsId });
                    table.ForeignKey(
                        name: "FK_PcBuildProduct_PcBuilds_BuildsId",
                        column: x => x.BuildsId,
                        principalTable: "PcBuilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PcBuildProduct_Products_BuildProductsId",
                        column: x => x.BuildProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderProduct_OrdersId",
                table: "CustomerOrderProduct",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_PcBuildProduct_BuildsId",
                table: "PcBuildProduct",
                column: "BuildsId");

            migrationBuilder.CreateIndex(
                name: "IX_PcBuilds_CustomerId",
                table: "PcBuilds",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrderProduct");

            migrationBuilder.DropTable(
                name: "PcBuildProduct");

            migrationBuilder.DropTable(
                name: "PcBuilds");

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "ProductTypes");

            migrationBuilder.AddColumn<int>(
                name: "CustomerOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CustomerOrderId",
                table: "Products",
                column: "CustomerOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_CustomerOrders_CustomerOrderId",
                table: "Products",
                column: "CustomerOrderId",
                principalTable: "CustomerOrders",
                principalColumn: "Id");
        }
    }
}
