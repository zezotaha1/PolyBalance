using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolyBalance.Migrations
{
    /// <inheritdoc />
    public partial class ReturnItmePriceToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UintId",
                table: "OrderDetails",
                newName: "ItemPriceId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId_UintId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId_ItemPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ItemPriceId",
                table: "OrderDetails",
                column: "ItemPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ItemsPrices_ItemPriceId",
                table: "OrderDetails",
                column: "ItemPriceId",
                principalTable: "ItemsPrices",
                principalColumn: "ItemPriceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ItemsPrices_ItemPriceId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ItemPriceId",
                table: "OrderDetails");

            migrationBuilder.RenameColumn(
                name: "ItemPriceId",
                table: "OrderDetails",
                newName: "UintId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId_ItemPriceId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId_UintId");
        }
    }
}
