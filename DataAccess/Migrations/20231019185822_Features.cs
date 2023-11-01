using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class Features : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Features",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProductTypeId",
                table: "Features",
                column: "ProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_ProductTypes_ProductTypeId",
                table: "Features",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_ProductTypes_ProductTypeId",
                table: "Features");

            migrationBuilder.DropIndex(
                name: "IX_Features_ProductTypeId",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Features");
        }
    }
}
