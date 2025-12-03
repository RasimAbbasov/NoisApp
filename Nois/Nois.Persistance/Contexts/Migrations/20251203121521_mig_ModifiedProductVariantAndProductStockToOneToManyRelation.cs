using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nois.Persistance.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class mig_ModifiedProductVariantAndProductStockToOneToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks",
                column: "ProductVariantId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks");

            migrationBuilder.CreateIndex(
                name: "IX_ProductStocks_ProductVariantId",
                table: "ProductStocks",
                column: "ProductVariantId");
        }
    }
}
