using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nois.Persistance.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class mig_ModifiedProductVariantEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId_SizeId_ColorId",
                table: "ProductVariants");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId_SizeId_ColorId",
                table: "ProductVariants",
                columns: new[] { "ProductId", "SizeId", "ColorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductVariants_ProductId_SizeId_ColorId",
                table: "ProductVariants");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId_SizeId_ColorId",
                table: "ProductVariants",
                columns: new[] { "ProductId", "SizeId", "ColorId" });
        }
    }
}
