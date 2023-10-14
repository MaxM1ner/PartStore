using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Customers_CustomerId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_Customers_CustomerId",
                table: "ProductComments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_AspNetUsers_CustomerId",
                table: "CartProducts",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_AspNetUsers_CustomerId",
                table: "ProductComments",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_AspNetUsers_CustomerId",
                table: "CartProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductComments_AspNetUsers_CustomerId",
                table: "ProductComments");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Customers_CustomerId",
                table: "CartProducts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductComments_Customers_CustomerId",
                table: "ProductComments",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
