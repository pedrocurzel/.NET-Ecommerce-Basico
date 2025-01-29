using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _net.Migrations
{
    /// <inheritdoc />
    public partial class ProductForeignKeyOnOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductModelId",
                table: "OrderItems",
                column: "ProductModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductModelId",
                table: "OrderItems",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductModelId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductModelId",
                table: "OrderItems");
        }
    }
}
