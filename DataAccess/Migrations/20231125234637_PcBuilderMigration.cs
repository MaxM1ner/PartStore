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
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "ProductTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

            migrationBuilder.InsertData(
                table: "ProductTypes",
                columns: new[] { "Id", "TypeImagepath", "Value", "Visible" },
                values: new object[] { 2147483647, "", "Services", false });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CustomerOrderId", "Description", "IsVisible", "Name", "Price", "ProductTypeId", "Quantity" },
                values: new object[] { 2147483647, null, "Our engineers will build your PC, so you can not worry about doing that by yourself", false, "Pre-built PC", 0m, 2147483647, 2147483647 });

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
                name: "PcBuildProduct");

            migrationBuilder.DropTable(
                name: "PcBuilds");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2147483647);

            migrationBuilder.DeleteData(
                table: "ProductTypes",
                keyColumn: "Id",
                keyValue: 2147483647);

            migrationBuilder.DropColumn(
                name: "Visible",
                table: "ProductTypes");
        }
    }
}
