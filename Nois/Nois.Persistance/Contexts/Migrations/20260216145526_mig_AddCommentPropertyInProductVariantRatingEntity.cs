using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nois.Persistance.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class mig_AddCommentPropertyInProductVariantRatingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ProductVariantRatings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ProductVariantRatings");
        }
    }
}
